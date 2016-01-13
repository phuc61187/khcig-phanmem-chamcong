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
	public partial class zTestControl : Form {
		public zTestControl() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			//using (WE_LinqToSQLDataContext context = new WE_LinqToSQLDataContext())
			//{
			//	relationDeptBindingSource.DataSource = context.RelationDepts;
			//}
			WEDatabaseDataContext context = new WEDatabaseDataContext();
			//gridLookUpEdit1.Properties.DataSource = context.a();
			var kq = from user in context.UserInfos
						join phong in context.RelationDepts.DefaultIfEmpty() on user.UserIDD equals phong.ID
				select user;
			gridControl1.DataSource = kq;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var tempList = (List<object>) gridLookUpEdit1.EditValue; // ko dùng List<int>
			var chuoi1 = tempList.Aggregate(string.Empty, (current, i) => current + i.ToString());
			richTextBox1.Text += string.Format("EditValueType: {0} \nValue: {1}\n", checkedComboBoxEdit1.EditValue.GetType(), chuoi1);

			//gridLookUpEdit1View.DataSource = 

		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			richTextBox1.Text += string.Format("Type:{0} Value:{1}", timeEdit1.EditValue.GetType(), timeEdit1.EditValue);
		}
	}
}

//-- =============================================
//-- Author:		Name
//-- Create date: 
//-- Description:	
//-- =============================================
//CREATE PROCEDURE [dbo].[v5_UserInfo_DocDSNVThaoTac] 
	
//AS
//BEGIN
//	SELECT		UserFullCode, UserFullName, UserEnrollNumber, UserLastName, 
//				UserIDD as UserIDDepartment,RelationDept.Description as DepartmentDescription, 
//				--RelationDept.TinhPC50, RelationDept.ViTri,
//				--UserIDTitle as IDChucVu, UserInfo.UserIDTitle as ChucVu, 
//				UserInfo.SchID as ScheduleID, Schedule.SchName as ScheduleName
//	From		UserInfo inner join RelationDept on UserInfo.UserIDD = RelationDept.ID
//						left join Schedule on UserInfo.SchID = Schedule.SchID
//	where		UserIDD > 0
//	order by	UserLastName
//END


//--select	UserFullCode, UserFullName, UserEnrollNumber, UserLastName, UserEnrollName,
//--									UserIDD as MaPhong, 
//--									case when (UserIDD is null or UserIDD = 0) then N'--' else RelationDept.Description end as TenPhong, RelationDept.TinhPC50, RelationDept.ViTri,
 
//--									UserIDTitle as IDChucVu, case when (UserInfo.UserIDTitle is null or UserInfo.UserIDTitle = 0)  then N'Chưa SX' else TitleName end as ChucVu, 

//--									UserInfo.SchID, case when (UserInfo.SchID is null or UserInfo.SchID = 0)  then N'--' else SchName end as SchName,   

//--									HeSoLuongCB, HeSoLuongSP, 
//--									UserCardNo, HSBHCongThem, 
//--									case when ( UserSex = 0 ) then N'Nam' else N'Nữ' end as UserSex, 
//--									UserBirthDay, UserEnabled, UserHireDay, UserPrivilege 

//--							FROM	UserInfo left join Title on UserInfo.UserIDTitle = Title.IDT
//--									
//--									left join Schedule on UserInfo.SchID = Schedule.SchID
//--							{0}
//--							order by UserFullCode