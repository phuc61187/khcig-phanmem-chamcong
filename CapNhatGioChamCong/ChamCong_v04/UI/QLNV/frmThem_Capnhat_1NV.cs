using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DAL;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;

namespace ChamCong_v04.UI.QLNV {
	public partial class frmThem_Capnhat_1NV : Form {
		#region log tooltip và hàm ko quan trọng

		public frmThem_Capnhat_1NV()
		{
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

		private void btnDong_Click(object sender, EventArgs e)
		{
			Close();
		}

		#endregion

		public int mode;
		public bool IsReload;
		public DataRowView RowView;


		private void frmThemNV_Load(object sender, EventArgs e) {
			dtpNgayVaoLam.Value = DateTime.Today;

			#region load chức vụ
			
			cbChucVu.DataSource = DAO.LoadDataSourceChucVu(true);
			cbChucVu.ValueMember = "IDChucVu";
			cbChucVu.DisplayMember = "ChucVu";

			#endregion

			#region load phòng ban

			cbPhongBan.DataSource = DAO.LoadDataSourcePhongBan(true);
			cbPhongBan.ValueMember = "ID";
			cbPhongBan.DisplayMember = "Description";

			#endregion

			#region load lịch trình làm việc

			var tableLichTrinh = SqlDataAccessHelper.ExecuteQueryString("select * from Schedule");
			cbLichTrinh.DataSource = tableLichTrinh;
			cbLichTrinh.ValueMember = "SchID";
			cbLichTrinh.DisplayMember = "SchName";

			#endregion

			if (mode == 1) // chế độ  thêm mới, cho phép thực hiện phần công nhật
			{
				MyUtility.EnableDisableControl(true, tbMaCC, checkLamCongnhat, checkNVChinhThuc, checkNVNhanKiet, dtpNgayBDCongnhat, dtpNgayKTCongnhat);
			}
			else if (mode == 0) // chế độ cập nhật ko cho phép thực hiện phần công nhật
			{
				MyUtility.EnableDisableControl(false, tbMaCC,checkLamCongnhat,checkNVChinhThuc, dtpNgayBDCongnhat,dtpNgayKTCongnhat);

				tbMaNV.Text = RowView["UserFullCode"].ToString();
				tbHoTenNV.Text = RowView["UserFullName"].ToString();
				radNam.Checked = (RowView["UserSex"].ToString().ToLower() == "nam");
				radNuu.Checked = !radNam.Checked;
				tbMaCC.Text = ((int)RowView["UserEnrollNumber"]).ToString("0000");
				tbUserEnrollName.Text = RowView["UserEnrollName"].ToString();
				tbMaTheTu.Text = (RowView["UserCardNo"] != DBNull.Value) ? RowView["UserCardNo"].ToString() : "0000000000";
				dtpNgayVaoLam.Value = (RowView["UserHireDay"] != DBNull.Value) ? (DateTime)RowView["UserHireDay"] : DateTime.MinValue;
				if (RowView["IDChucVu"] == DBNull.Value || (int)RowView["IDChucVu"] == 0) cbChucVu.SelectedIndex = 0;
				else cbChucVu.SelectedValue = (int)RowView["IDChucVu"];
				if (RowView["MaPhong"] == DBNull.Value || (int)RowView["MaPhong"] == 0) cbPhongBan.SelectedIndex = 0;
				else cbPhongBan.SelectedValue = (int)RowView["MaPhong"];
				if (RowView["SchID"] == DBNull.Value || (int)RowView["SchID"] == 0) cbLichTrinh.SelectedIndex = 0;
				else cbLichTrinh.SelectedValue = (int)RowView["SchID"];
				tbHSLCB.Text = (RowView["HeSoLuongCB"] != DBNull.Value) ? ((float)RowView["HeSoLuongCB"]).ToString("0.00") : "000";
				tbHSLCV.Text = (RowView["HeSoLuongSP"] != DBNull.Value) ? ((float)RowView["HeSoLuongSP"]).ToString("0.00") : "000";
				tbHSBHXHCongThem.Text = (RowView["HSBHCongThem"] != DBNull.Value) ? ((float)RowView["HSBHCongThem"]).ToString("0.00") : "000";
                tbHSLCBTT17.Text = (RowView["HSLCBTT17"] != DBNull.Value) ? ((float)RowView["HSLCBTT17"]).ToString("0.00") : "000";
                tbHSPCCV.Text = (RowView["HSPCCV"] != DBNull.Value) ? ((float)RowView["HSPCCV"]).ToString("0.00") : "000";
                tbHSPCDH.Text = (RowView["HSPCDH"] != DBNull.Value) ? ((float)RowView["HSPCDH"]).ToString("0.00") : "000";
                tbHSPCTN.Text = (RowView["HSPCTN"] != DBNull.Value) ? ((float)RowView["HSPCTN"]).ToString("0.00") : "000";
                checkNVNhanKiet.Checked = (RowView["NVNhanKiet"] != DBNull.Value) ? (bool)RowView["NVNhanKiet"] : false;
                checkUserEnabled.Checked = (RowView["UserEnabled"] != DBNull.Value && (bool)RowView["UserEnabled"]);
			}
		}

		private void btnLuu_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			string maNV = tbMaNV.Text;
			string hoten = tbHoTenNV.Text.TrimEnd('?', '.', ',');
			string[] words = hoten.Split(' ');
			string ten = (words.Last(item => item != string.Empty));
			int gioitinh = (radNam.Checked) ? 0 : 1;
			int maCC;
			if (int.TryParse(tbMaCC.Text, out maCC) == false) {
				ACMessageBox.Show("Mã chấm công phải là số hợp lệ.", Resources.Caption_Loi, 2000);
				return;
			}
			string tenCC = tbUserEnrollName.Text;
			string mathetu = tbMaTheTu.Text;
			DateTime ngayvaolam = dtpNgayVaoLam.Value.Date;
			int idchucvu = (int)cbChucVu.SelectedValue;
			int idphong = (int)cbPhongBan.SelectedValue;
			string tenphong = (cbPhongBan.SelectedItem != null) ? cbPhongBan.SelectedItem.ToString() : cbPhongBan.Text;
			int idlichtrinh = (int)cbLichTrinh.SelectedValue;
			string tenLichTrinh = (cbLichTrinh.SelectedItem != null) ? cbLichTrinh.SelectedItem.ToString() : cbLichTrinh.Text;
			float hslcb, hslcv, hsbhxhcongthem, hslcbtt17, hspccv, hspcdh, hspctn;
			if (float.TryParse(tbHSLCB.Text, out hslcb) == false
				|| float.TryParse(tbHSLCV.Text, out hslcv) == false
				|| float.TryParse(tbHSBHXHCongThem.Text, out hsbhxhcongthem) == false
                || float.TryParse(tbHSLCBTT17.Text, out hslcbtt17) == false
                || float.TryParse(tbHSPCCV.Text, out hspccv) == false
                || float.TryParse(tbHSPCDH.Text, out hspcdh) == false
                || float.TryParse(tbHSPCTN.Text, out hspctn) == false
                ) {
				ACMessageBox.Show(Resources.Text_HeSoLCB_CV_BHXH_ChuaHopLe, Resources.Caption_Loi, 2000);
				return;
			}
			bool userEnabled = checkUserEnabled.Checked;
            bool nvNhanKiet = checkNVNhanKiet.Checked;

			if (mode == 1) {
				var tableKiemTraTonTai = SqlDataAccessHelper.ExecuteQueryString(
					"select * from UserInfo where UserEnrollNumber = @UserEnrollNumber",
					new string[] { "@UserFullCode", "@UserEnrollNumber" },
					new object[] { maNV, maCC });
				if (tableKiemTraTonTai.Rows.Count > 0) {
					ACMessageBox.Show("Mã nhân viên hoặc mã chấm công đã tồn tại trong CSDL. Vui lòng chọn mã khác.", Resources.Caption_Loi, 2000);
					return;
				}
			}
			#region xác nhận trước khi lưu
			if (MessageBox.Show(Resources.Text_XacNhanLuuThongTinNV, Resources.Caption_XacNhan, MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
			#endregion 

			#region execute query 

			int kq1 = SqlDataAccessHelper.ExecNoneQueryString(
                @" UPDATE UserInfo Set 
								UserFullCode = @UserFullCode, UserFullName = @UserFullName, UserLastName = @UserLastName, UserEnrollName = @UserEnrollName,
								  UserCardNo = @UserCardNo, UserHireDay = @UserHireDay, UserIDTitle = @IDChucVu, UserSex = @UserSex, 
								  UserEnabled = @UserEnabled, UserIDD = @UserIDD, SchID = @SchID, 
								  HeSoLuongCB = @HeSoLuongCB, HeSoLuongSP = @HeSoLuongSP, HSBHCongThem = @HSBHCongThem,
                                  HSLCBTT17 = @HSLCBTT17, HSPCCV = @HSPCCV, HSPCDH = @HSPCDH, HSPCTN = @HSPCTN, NVNhanKiet = @NVNhanKiet
								where UserEnrollNumber = @UserEnrollNumber

							if ( @@ROWCOUNT = 0 )  
							INSERT INTO UserInfo (  UserFullCode,  UserFullName,  UserLastName,  UserEnrollNumber,  UserEnrollName,
									UserCardNo,  UserHireDay,  UserIDTitle,  UserSex,  UserPrivilege,  UserEnabled,
									UserIDD,  SchID,  HeSoLuongCB,  HeSoLuongSP,  HSBHCongThem,  
                                    HSLCBTT17, HSPCCV, HSPCDH, HSPCTN, NVNhanKiet,
									PushCardID,  UserPW, UserGroup, UserTZ) 
							VALUES (  @UserFullCode,  @UserFullName,  @UserLastName,  @UserEnrollNumber,  @UserEnrollName,
									  @UserCardNo,  @UserHireDay,  @IDChucVu,  @UserSex,  @UserPrivilege,  @UserEnabled,
									  @UserIDD,  @SchID,  @HeSoLuongCB,  @HeSoLuongSP,  @HSBHCongThem,  
                                      @HSLCBTT17, @HSPCCV, @HSPCDH, @HSPCTN, @NVNhanKiet,
									  @PushCardID,  @UserPW, @UserGroup, @UserTZ )",
				new string[]{
						"@UserFullCode", "@UserFullName", "@UserLastName", "@UserEnrollNumber", "@UserEnrollName",
						"@UserCardNo", "@UserHireDay", "@IDChucVu", "@UserSex", "@UserPrivilege", "@UserEnabled", 
						"@UserIDD", "@SchID",
						"@HeSoLuongCB", "@HeSoLuongSP", "@HSBHCongThem",
                        "@HSLCBTT17", "@HSPCCV", "@HSPCDH", "@HSPCTN", "@NVNhanKiet",
						"@PushCardID", "@UserPW", "@UserGroup", "@UserTZ"},
				new object[]{
						maNV, hoten, ten, maCC, tenCC,
						mathetu, ngayvaolam, idchucvu, gioitinh, 0, userEnabled, 
						idphong, idlichtrinh,
						hslcb, hslcv, hsbhxhcongthem, 
                        hslcbtt17, hspccv, hspcdh, hspctn, nvNhanKiet,
						"[0000000000]", string.Empty, 1, "0000000000000000",});
			string noidung =
				@"Lưu thông tin nhân viên có mã chấm công [{0}], mã nhân viên [{1}], hệ số lương cơ bản [{2}], hệ số lương sản phẩm [{3}], hệ số bảo hiểm cộng thêm cho lãnh đạo [{4}], hệ số lương cơ bản TT17 [{5}], hệ số phụ cấp công việc [{6}], hệ số phụ cấp độc hại [{7}], hệ số phụ cấp trách nhiệm [{8}], tình trạng hoạt động [{9}], phòng ban [{10}], lịch trình [{11}]";
			DAO.GhiNhatKyThaotac("Lưu thông tin nhân viên", string.Format(noidung,
				maCC, maNV, hslcb.ToString("0.00"), hslcv.ToString("0.00"), hsbhxhcongthem.ToString("0.00"), hslcbtt17.ToString("0.00"), hspccv.ToString("0.00"), hspcdh.ToString("0.00"), hspctn.ToString("0.00"), userEnabled, tenphong,tenLichTrinh), maCC:maCC);

            #endregion
            if (checkLamCongnhat.Enabled && checkLamCongnhat.Checked)
			{
				var ngayBD = dtpNgayBDCongnhat.Value.Date;
				var ngayKT = dtpNgayKTCongnhat.Value.Date;
				var laNVChinhthuc = checkNVChinhThuc.Checked;
				if (ngayBD > ngayKT) // hoán vị ngày bắt đầu kết thúc nếu bị ngược
				{
					var temp = ngayBD;
					ngayKT = ngayBD;
					ngayBD = temp;
				}
				//thực hiện tách nếu làm công nhật ngày bd và kt là nhiều tháng liên tiếp
					List<DateTime> arrNgayBD, arrNgayKT;
					XL2.TachThang(ngayBD, ngayKT, out arrNgayBD, out arrNgayKT);
				for (int i = 0; i < arrNgayBD.Count; i++)
				{
					int kq2 = DAO.UpdIns_ThuChiNVCongNhat(new DateTime(arrNgayBD[i].Year, arrNgayBD[i].Month, 1), maCC, idphong, tenphong, arrNgayBD[i], arrNgayKT[i], 0, 0d, laNVChinhthuc);
					if (kq2 == 0)
					{
						MessageBox.Show(Resources.Text_CoLoi);
						break;
					}
				}
			}

			if (kq1 == 0) {
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
			else {
				IsReload = true;
				if (mode == 1) {
					ACMessageBox.Show("Thêm nhân viên thành công.", Resources.Caption_ThongBao, 2000);
					MyUtility.ClearControlText(tbMaNV, tbHoTenNV, tbMaCC, tbUserEnrollName);
					radNam.Checked = true;
					tbMaTheTu.Text = "0000000000";
					tbHSLCB.Text = "000";
					tbHSLCV.Text = "000";
					tbHSBHXHCongThem.Text = "000";
                    tbHSLCBTT17.Text = "000";
                    tbHSPCCV.Text = "000";
                    tbHSPCDH.Text = "000";
                    tbHSPCTN.Text = "000";
					cbChucVu.SelectedIndex = 0;
					cbPhongBan.SelectedIndex = 0;
					cbLichTrinh.SelectedIndex = 0;
					checkUserEnabled.Checked = true;
                    checkNVNhanKiet.Checked = false;
				}
				else {
					Close();
				}
			}

		}


		private void checkLamCongnhat_CheckedChanged(object sender, EventArgs e)
		{
			dtpNgayBDCongnhat.Enabled = dtpNgayKTCongnhat.Enabled = checkLamCongnhat.Checked;
		}

	}
}
