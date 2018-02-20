using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DAL;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;

namespace ChamCong_v04.UI.QLNV {
	public partial class frmCapNhatNVHangLoat : Form {
		#region log tooltip và hàm ko quan trọng

		public frmCapNhatNVHangLoat() {
			InitializeComponent();
		}

		private void toolTipHint_Draw(object sender, DrawToolTipEventArgs e) {
			Font f = new Font("Arial", 10.0f);
			e.DrawBackground();
			e.DrawBorder();
			e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, new PointF(2f, 2f));
		}

		private void toolTipHint_Popup(object sender, PopupEventArgs e) {
			Size temp = TextRenderer.MeasureText(toolTipHint.GetToolTip(e.AssociatedControl), new Font("Arial", 10.0f));
			temp.Width += 4;
			temp.Height += 4;
			e.ToolTipSize = temp;
		}

		private void btnDong_Click(object sender, EventArgs e) {
			Close();
		}

		#endregion

		public bool IsReload;
		public List<DataRowView> RowViews;
		private void Checked_Changed(object sender, EventArgs e) {
			treePhongBan.Enabled = checkPhong.Checked;
			cbChucVu.Enabled = checkChucVu.Checked;
			cbLichTrinh.Enabled = checkLichtrinh.Checked;
			tbHSLCB.Enabled = checkHSLCB.Checked;
			tbHSLCV.Enabled = checkHSLCV.Checked;
            tbHSLCBTT17.Enabled = checkHSLCBTT17.Checked;
            tbHSPCCV.Enabled = checkHSPCCV.Checked;
            tbHSPCDH.Enabled = checkHSPCDH.Checked;
            tbHSPCTN.Enabled = checkHSPCTN.Checked;
			checkUserEnabled.Enabled = checkTinhtrangHoatdong.Checked;
		}



		private void btnLuu_Click(object sender, EventArgs e) {
			int idPhong = -1, idChucVu = -1, idLichtrinh = -1, userEnabled = -1;
            float hslcb = -1f, hslcv = -1f, hslcbtt17 = -1f, hspccv = -1f, hspcdh = -1f, hspctn = -1f;
			List<string> arrString = new List<string>();
			List<string> arrString2 = new List<string>();
			bool IsExist_1Check = false;
			if (checkPhong.Checked) {
				TreeNode node = treePhongBan.SelectedNode;
				if (node == null)
				{
					ACMessageBox.Show("chưa chọn node", "", 1000); return;
				}
				//node != null
				idPhong = ((cPhongBan)(node.Tag)).ID;
				arrString.Add(" UserIDD = @MaPhong ");
				arrString2.Add("bộ phận mới: [" + ((cPhongBan)node.Tag).Ten + "]; ");
				IsExist_1Check = true;
			}
			if (checkChucVu.Checked) {
				idChucVu = (int)cbChucVu.SelectedValue;
				arrString.Add(" UserIDTitle = @IDChucVu ");
				arrString2.Add("chức vụ mới: [" + ((cbChucVu.SelectedItem != null) ? ((DataRowView)cbChucVu.SelectedItem)["ChucVu"].ToString() : cbChucVu.Text) + "]; ");
				IsExist_1Check = true;
				
			}
			if (checkLichtrinh.Checked) {
				idLichtrinh = (int)cbLichTrinh.SelectedValue;
				arrString.Add(" SchID = @SchID ");// (cbXNLyDo.SelectedItem != null) ? cbXNLyDo.SelectedItem.ToString() : cbXNLyDo.Text;
				arrString2.Add("lịch trình làm việc mới: [" + ((cbLichTrinh.SelectedItem != null) ? ((DataRowView)cbLichTrinh.SelectedItem)["SchName"].ToString() : cbLichTrinh.Text) + "]; ");
				IsExist_1Check = true;
			}
			if (checkHSLCB.Checked) {
				if (float.TryParse(tbHSLCB.Text, out hslcb) == false) {
					ACMessageBox.Show(Resources.Text_HeSoLCB_CV_BHXH_ChuaHopLe, Resources.Caption_Loi, 2000);
					return;
				}
				else
				{
					arrString.Add(" HeSoLuongCB = @HeSoLuongCB ");
					arrString2.Add("hệ số lương cơ bản mới: [" + hslcb.ToString("0.00") + "]; ");
				}
				IsExist_1Check = true;
			}
			if (checkHSLCV.Checked) {
				if (float.TryParse(tbHSLCV.Text, out hslcv) == false) {
					ACMessageBox.Show(Resources.Text_HeSoLCB_CV_BHXH_ChuaHopLe, Resources.Caption_Loi, 2000);
					return;
				}
				else
				{
					arrString.Add(" HeSoLuongSP = @HeSoLuongSP ");
					arrString2.Add("hệ số lương sản phẩm mới: [" + hslcv.ToString("0.00") + "]; ");
				}
				IsExist_1Check = true;
			}

            if (checkHSLCBTT17.Checked)
            {
                if (float.TryParse(tbHSLCBTT17.Text, out hslcbtt17) == false)
                {
                    ACMessageBox.Show(Resources.Text_HeSoLCB_CV_BHXH_ChuaHopLe, Resources.Caption_Loi, 2000);
                    return;
                }
                else
                {
                    arrString.Add(" HSLCBTT17 = @HSLCBTT17 ");
                    arrString2.Add("hệ số lương cơ bản TT17 mới: [" + hslcbtt17.ToString("0.00") + "]; ");
                }
                IsExist_1Check = true;
            }
            if (checkHSPCCV.Checked)
            {
                if (float.TryParse(tbHSPCCV.Text, out hspccv) == false)
                {
                    ACMessageBox.Show(Resources.Text_HeSoLCB_CV_BHXH_ChuaHopLe, Resources.Caption_Loi, 2000);
                    return;
                }
                else
                {
                    arrString.Add(" HSPCCV = @HSPCCV ");
                    arrString2.Add("hệ số phụ cấp công việc mới: [" + hspccv.ToString("0.00") + "]; ");
                }
                IsExist_1Check = true;
            }
            if (checkHSPCDH.Checked)
            {
                if (float.TryParse(tbHSPCDH.Text, out hspcdh) == false)
                {
                    ACMessageBox.Show(Resources.Text_HeSoLCB_CV_BHXH_ChuaHopLe, Resources.Caption_Loi, 2000);
                    return;
                }
                else
                {
                    arrString.Add(" HSPCDH = @HSPCDH ");
                    arrString2.Add("hệ số phụ cấp độc hại mới: [" + hspcdh.ToString("0.00") + "]; ");
                }
                IsExist_1Check = true;
            }
            if (checkHSPCTN.Checked)
            {
                if (float.TryParse(tbHSPCTN.Text, out hspctn) == false)
                {
                    ACMessageBox.Show(Resources.Text_HeSoLCB_CV_BHXH_ChuaHopLe, Resources.Caption_Loi, 2000);
                    return;
                }
                else
                {
                    arrString.Add(" HSPCTN = @HSPCTN ");
                    arrString2.Add("hệ số phụ cấp trách nhiệm mới: [" + hspcdh.ToString("0.00") + "]; ");
                }
                IsExist_1Check = true;
            }


            if (checkTinhtrangHoatdong.Checked) {
				userEnabled = (checkUserEnabled.Checked) ? 1 : 0;
				arrString.Add(" UserEnabled = @UserEnabled ");
				arrString2.Add("tình trạng mới: [" + (checkUserEnabled.Checked ? "đang làm việc" : "ngưng việc") + "]; ");
				IsExist_1Check = true;
			}
			// chưa chọn thì báo
			if (IsExist_1Check == false)
			{
				ACMessageBox.Show("Bạn chưa chọn thông tin cần cập nhật nào.", Resources.Caption_ThongBao, 2000);
				return;
			}
			// xác nhận
			if (MessageBox.Show("Cập nhật thông tin hàng loại cho nhân viên?", Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) return;
			var listUEN = (from DataRowView row in RowViews select (int) row["UserEnrollNumber"]).ToList();
			string temp1 = string.Join(" , ", arrString.ToArray()); // nối các thuộc tính với nhau
			string temp2 = string.Join(" or UserEnrollNumber = ", listUEN.ToArray()); // nối các userenrollnumber với nhau
			string formatstring1 = @"	update UserInfo set {0} where ( UserEnrollNumber = {1} ) ";
			string temp3 = string.Join("", arrString2.ToArray());
			string formatstring2 = @"Cập nhật thông tin NV mã chấm công [{0}] {1}";
			string query = string.Format(formatstring1, temp1, temp2);
			int kq = SqlDataAccessHelper.ExecNoneQueryString(query,
				new string[] {"@MaPhong", "@IDChucVu", "@SchID",
						"@HeSoLuongCB", "@HeSoLuongSP", "@HSLCBTT17", "@HSPCCV", "@HSPCDH", "@HSPCTN", "@UserEnabled",},
				new object[] {idPhong, idChucVu, idLichtrinh, hslcb, hslcv, userEnabled});
			foreach (int uen in listUEN)
			{
				DAO.GhiNhatKyThaotac("Cập nhật thông tin NV hàng loạt", string.Format(formatstring2, uen, temp3), maCC:uen);
			}
			if (kq == 0)
			{
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
			}
			else
			{
				IsReload = true;
				Close();
			}
		}

		private string abc() {
			throw new NotImplementedException();
		}


		private void frmCapNhatNVHangLoat_Load(object sender, EventArgs e) {
			List<cPhongBan> dspb = new List<cPhongBan>();
			XL.KhoiTaoDSPhongBan(dspb);
			XL.loadTreePhgBan(treePhongBan, XL2.TatcaPhongban, dspb);

			cbChucVu.DataSource = DAO.LoadDataSourceChucVu(false);
			cbChucVu.DisplayMember = "ChucVu";
			cbChucVu.ValueMember = "IDChucVu";

			var tableLichTrinh = SqlDataAccessHelper.ExecuteQueryString("select * from Schedule");
			cbLichTrinh.DataSource = tableLichTrinh;
			cbLichTrinh.DisplayMember = "SchName";
			cbLichTrinh.ValueMember = "SchID";
		}


	}
}
