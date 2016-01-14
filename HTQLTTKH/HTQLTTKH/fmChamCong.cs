using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTQLTTKH {
	public partial class fmChamCong : Form {
		private WEDatabaseDataContext db = new WEDatabaseDataContext();
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

			
				from	user in db.UserInfos
						join phong in db.RelationDepts on user.UserIDD equals phong.ID into JG_Phong
						join lichtrinh in db.Schedules on user.SchID equals lichtrinh.SchID into JG_LichTrinh
						from listDeptOfEachUser in JG_Phong.DefaultIfEmpty()
						from listScheOfEachUser in JG_LichTrinh.DefaultIfEmpty()
				where
						user.UserIDD != null &&
						(from	phanQuyen in db.DeptPrivileges
						where	phanQuyen.IsYes && phanQuyen.UserID == 21
						select	phanQuyen.IDD).Contains((int)user.UserIDD)
						&& listDeptOfEachUser != null
				select new {
					user.UserFullCode, user.UserFullName, user.UserEnrollNumber,
					UserIDDepartment = user.UserIDD, DepartmentDescription = listDeptOfEachUser.Description,
					ScheduleID = user.SchID, ScheduleName = listScheOfEachUser.SchName
				};
			//var kq6 = from user in 

			gridControl1.DataSource = kq5;

		}

		private void fmChamCong_Load(object sender, EventArgs e) {

		}
	}
}
