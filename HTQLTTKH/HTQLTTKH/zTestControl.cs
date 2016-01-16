using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTQLTTKH {
	public partial class zTestControl : Form {
		private WEDatabaseDataContext db = new WEDatabaseDataContext();
		public zTestControl() {
			InitializeComponent();
		}

		private void buttonInit_Click(object sender, EventArgs e) {
			//using (WE_LinqToSQLDataContext context = new WE_LinqToSQLDataContext())
			//{
			//	relationDeptBindingSource.DataSource = context.RelationDepts;
			//}
			//gridLookUpEdit1.Properties.DataSource = context.a();
/*
			var kq = from user in context.UserInfos
						join phong in context.RelationDepts.DefaultIfEmpty() on user.UserIDD equals phong.ID
				select user;
			gridControl1.DataSource = kq;
*/
			var kq =	from	user in db.UserInfos
								join phong in db.RelationDepts on user.UserIDD equals phong.ID into JG_Phong
								join lichtrinh in db.Schedules on user.SchID equals lichtrinh.SchID into JG_LichTrinh
								from listDeptOfEachUser in JG_Phong.DefaultIfEmpty()
								from listScheOfEachUser in JG_LichTrinh.DefaultIfEmpty()
				        where	user.UserIDD != null &&
								(from	phanQuyen in db.DeptPrivileges
								where	phanQuyen.IsYes && phanQuyen.UserID == 21
								select	phanQuyen.IDD).Contains((int)user.UserIDD)
								&& listDeptOfEachUser != null
						select new {
							user.UserFullCode, user.UserFullName, UserEnrollNumber=user.UserEnrollNumber, user.UserLastName,
							UserIDDepartment = user.UserIDD, DepartmentDescription = listDeptOfEachUser.Description,
							ScheduleID = user.SchID, ScheduleName = listScheOfEachUser.SchName
						};

			//popupContainerEdit1.DataBindings.Add("EditValue", kq.ToList(), "UserFullName");
			//popupContainerEdit1.DataBindings.Add("EditValue", kq.ToList(), "User");
			gridControl1.DataSource = kq;
		}

		void temp_Parse(object sender, ConvertEventArgs e) {
			
		}

		private void buttonProcess_Click(object sender, EventArgs e)
		{
			var tempList = (List<object>) gridLookUpEdit1.EditValue; // ko dùng List<int>
			var chuoi1 = tempList.Aggregate(string.Empty, (current, i) => current + i.ToString());
			richTextBox1.Text += string.Format("EditValueType: {0} \nValue: {1}\n", checkedComboBoxEdit1.EditValue.GetType(), chuoi1);
					
		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			richTextBox1.Text += string.Format("Type:{0} Value:{1}", timeEdit1.EditValue.GetType(), timeEdit1.EditValue);
		}

		private void popupContainerEdit1_Properties_QueryResultValue(object sender, DevExpress.XtraEditors.Controls.QueryResultValueEventArgs e)
		{
			int[] selectedRow_s = gridView1.GetSelectedRows();
			List<dynamic> selectedObject_s = selectedRow_s.Select(rowHandleIndex => gridView1.GetRow(rowHandleIndex)).Cast<dynamic>().ToList();
			string templateString = " {0} [{1}]";
			List<string> tempList = selectedObject_s.Select(@object => String.Format(templateString, @object.UserLastName, @object.UserEnrollNumber)).Cast<string>().ToList();

			string selectedObjectString_s = string.Join(";", tempList);
			//List<string> selectedObjectString_s = selectedObject_s.Aggregate(string.Empty, (current, item) => current +=)
			e.Value = selectedObjectString_s;
		}

		private void popupContainerEdit1_Properties_QueryCloseUp(object sender, CancelEventArgs e)
		{
			//string temp = ((List<dynamic>) popupContainerEdit1.EditValue).Aggregate(string.Empty, (current, item) => current += item.UserfullName);
			richTextBox1.Text += "\npopupContainerEdit1_Properties_QueryCloseUp";
		}

		private void popupContainerEdit1_EditValueChanged(object sender, EventArgs e) {
			richTextBox1.Text += "\npopupContainerEdit1_EditValueChanged";
			//string temp = ((List<dynamic>) popupContainerEdit1.EditValue).Aggregate(string.Empty, (current, item) => current += item.UserFullName);
			//richTextBox1.Text += string.Format("\n{0}", temp);
			//popupContainerEdit1.Text = temp;
		}

		private void popupContainerEdit1_Properties_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e) {

		}

		private void popupContainerEdit1_Properties_QueryPopUp(object sender, CancelEventArgs e)
		{
			var editValue = popupContainerEdit1.EditValue;
			if (editValue == null)
			{
				gridView1.ClearSelection();
			}
			else if (editValue.ToString() == string.Empty)
			{
				richTextBox1.Text += "None Item Selected";}
			else if (editValue.ToString() != string.Empty) {
				string[] selectedValue = editValue.ToString().Split(';');
				List<int> userEnrollNumber_s = (from s in selectedValue
												let a = s.LastIndexOf('[') + 1
												let b = s.LastIndexOf(']') - 1
												select s.Substring(a, b - a) into c
												select int.Parse(c)).ToList();
				//string[] selectedValue = editValue.ToString().Split(';');
				/*
								string s = "Phong [4698]";
								//selectedValue[0].Substring();
								int a = s.LastIndexOf('[') + 1;
								int b = s.LastIndexOf(']') - 1;
								string c = s.Substring(a, b - a);
								Console.WriteLine(string.Format("{0} {1} {2} {3}"), a, b, (b - a), c);
								Console.WriteLine("");
				*/

			}
		}
	}
}//-- =============================================
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