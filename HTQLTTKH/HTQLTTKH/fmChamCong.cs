using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HTQLTTKH.Properties;

namespace HTQLTTKH {
	public partial class fmChamCong : Form {
		private WEDatabaseDataContext db = new WEDatabaseDataContext();
		private void XacDinhDSNVDuocChon(DevExpress.XtraEditors.PopupContainerEdit popupContainerEdit, out List<int> selectedUEN_s) {
			selectedUEN_s = new List<int>();
			var editValue = popupContainerEdit.EditValue;
			if (editValue == null)
			{
				gridView1.ClearSelection();
			}
			else if (editValue == string.Empty)
			{
				//todo add log command with id
			}
			else
			{
				string[] arrayItemValue = editValue.ToString().Split(';');
				selectedUEN_s = (from item in arrayItemValue
								 let a = item.LastIndexOf('[') + 1
								 let b = item.LastIndexOf(']') - 1
								 select item.Substring(a, b - a) into c
								select int.Parse(c)).ToList();
			}
		}
		public fmChamCong() {
			InitializeComponent();
		}

		private void simpleButtonInitData_Click(object sender, EventArgs e) {

			var kq5 = //from phanQuyen in db.DeptPrivileges
				//from phong in db.RelationDepts
				//from user in db.UserInfos
				//	 join phong in db.RelationDepts on user.UserIDD equals phong.ID into JG_Phong
				//	 join lichtrinh in db.Schedules on user.SchID equals lichtrinh.SchID into JG_LichTrinh
				//	 from item1 in JG_Phong.DefaultIfEmpty()
				//	 from item2 in JG_LichTrinh.DefaultIfEmpty()
				//where	item1 != null && item2 != null &&
				//		(from phong in db.RelationDepts
				//			   where 
				//			   select phong.ID).Contains((int) user.UserIDD)


				from user in db.UserInfos
				join phong in db.RelationDepts on user.UserIDD equals phong.ID into JG_Phong
				join lichtrinh in db.Schedules on user.SchID equals lichtrinh.SchID into JG_LichTrinh
				from listDeptOfEachUser in JG_Phong.DefaultIfEmpty()
				from listScheOfEachUser in JG_LichTrinh.DefaultIfEmpty()
				where
						user.UserIDD != null &&
						(from phanQuyen in db.DeptPrivileges
						 where phanQuyen.IsYes && phanQuyen.UserID == 21
						 select phanQuyen.IDD).Contains((int)user.UserIDD)
						&& listDeptOfEachUser != null
				select new {
					user.UserLastName, user.UserFullCode, user.UserFullName, user.UserEnrollNumber,
					UserIDDepartment = user.UserIDD, DepartmentDescription = listDeptOfEachUser.Description,
					ScheduleID = user.SchID, ScheduleName = listScheOfEachUser.SchName
				};
			//var kq6 = from user in 

			gridControl1.DataSource = kq5;

		}

		private void fmChamCong_Load(object sender, EventArgs e) {

		}

		private void simpleButtonChamCong_Click(object sender, EventArgs e) {
			DateTime date1 = dateEdit1.DateTime, date2 = dateEdit2.DateTime;
			DateTime ngayBD, ngayKT;
			if (date1 > date2) {
				ngayBD = date2;
				ngayKT = date1;
			}
			else {
				ngayBD = date1;
				ngayKT = date2;
			}
			LastState.Default.g1_NgayBD = ngayBD;
			LastState.Default.g1_NgayKT = ngayKT;
			LastState.Default.Save();

			List<int> selectedUEN_s;
			this.XacDinhDSNVDuocChon(popupContainerEdit1, out selectedUEN_s);
			CheckInOutBUS bus = new CheckInOutBUS();
			bus.ChamCong(ngayBD, ngayKT, selectedUEN_s);
		}

	}
}
