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
			var kq = from user in db.UserInfos
					 join phong in db.RelationDepts on user.UserIDD equals phong.ID into JG_Phong
					 join lichtrinh in db.Schedules on user.SchID equals lichtrinh.SchID into JG_LichTrinh
					 from item1 in JG_Phong
					 from item2 in JG_LichTrinh.DefaultIfEmpty()
					 //where db.RelationDepts.Contains(user.UserIDD)
					 select new { user.UserFullCode, user.UserFullName, user.UserEnrollNumber,
						 UserIDDepartment = user.UserIDD, DepartmentDescription = item1.Description,
						 ScheduleID = item2.SchID, ScheduleName = item2.SchName
					 };

			var kq2 = from phanQuyen in db.DeptPrivileges
			          where phanQuyen.IsYes && phanQuyen.UserID == 21
			          select phanQuyen.IDD;
			var kq3 = (from phong in db.RelationDepts
			          where kq2.Contains(phong.ID)
			          select phong.ID).ToList();
			var kq4 = from user in db.UserInfos
			          where kq3.Contains((int)user.UserIDD)
			          select user;

			var kq5 = //from phanQuyen in db.DeptPrivileges
				//from phong in db.RelationDepts
				from user in db.UserInfos
					 join phong in db.RelationDepts on user.UserIDD equals phong.ID into JG_Phong
					 join lichtrinh in db.Schedules on user.SchID equals lichtrinh.SchID into JG_LichTrinh
					 from item1 in JG_Phong
					 from item2 in JG_LichTrinh.DefaultIfEmpty()
				where (from phong in db.RelationDepts
				       where (from phanQuyen in db.DeptPrivileges
				              where phanQuyen.IsYes && phanQuyen.UserID == 21
				              select phanQuyen.IDD).Contains(phong.ID)
				       select phong.ID).Contains((int) user.UserIDD)
				select new {
					user.UserFullCode, user.UserFullName, user.UserEnrollNumber,
					UserIDDepartment = user.UserIDD, DepartmentDescription = item1.Description,
					ScheduleID = item2.SchID, ScheduleName = item2.SchName
				};
				

			gridControl1.DataSource = kq5;

		}

		private void fmChamCong_Load(object sender, EventArgs e) {

		}
	}
}
