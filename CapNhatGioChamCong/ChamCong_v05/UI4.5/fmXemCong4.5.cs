using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.Helper;
using ChamCong_v05.Properties;
using ChamCong_v05.DTO;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ChamCong_v05.UI4._5 {
	public partial class fmXemCong4 : Form {
		public List<int> m_listCurrentIDPhg = new List<int>();

		public fmXemCong4() {
			InitializeComponent();
			SqlDataAccessHelper.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=WiseEyeV5Express;Integrated Security=true";
			gridControl1.DataSource = new System.Collections.Generic.List<ChamCong_v05.DTO.cUserInfo>();
		}

		private void fmXemCong4_Load(object sender, EventArgs e) {
			#region kiểm tra kết nối csdl , nếu mất kết nối thì đóng
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}
			#endregion

			/* 1. load tree phòng ( datasource cho checkComboEdit chọn nv do hàm treeAfterselect tạo
			 * 2. 
			 * 3. set tháng khi load lần đầu
			 */
			XL.loadTreePhgBan(treePhongBan);
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;

		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			#region mỗi lần chọn node thì lấy ID node hiện tại và tất cả node con

			m_listCurrentIDPhg.Clear();
			e.Node.Expand();
			TreeNode topnode = XL.TopNode(e.Node); //đưa về root để thực hiện từ trên xuống
			if (topnode != null) XL.GetIDNodeAndChildNode1(e.Node, ref m_listCurrentIDPhg); // chỉ lấy các phòng ban được phép, 
			else {
				var temp = ((DataRow)e.Node.Tag);
				if ((int)temp["IsYes"] == 1) m_listCurrentIDPhg.Add((int)temp["ID"]);
			}

			#endregion

			/* 1. clear select chọn nhân viên trước
			 * 2. load datasource cho check chọn nhân viên 
			 * 3. gán lại tên cho các column ngày
			 */
			//			checkedComboBoxEdit1.Properties.Items.Clear();
			DataTable tableMaPhong = MyUtility.Array_To_DataTable("ArrUserIDD", m_listCurrentIDPhg);
			DataTable tableNhanVien = SqlDataAccessHelper.ExecSPQuery(SPName.UserInfo_DocDSNVThaoTac.ToString(), new SqlParameter("@ArrUserIDD", SqlDbType.Structured) { Value = tableMaPhong });
			checkedDSNV.Properties.DataSource = tableNhanVien;
			checkedDSNV.Properties.DisplayMember = "DisplayItem";
			checkedDSNV.Properties.ValueMember = "UserEnrollNumber";

		}

		private void btnChamCong_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;
			/* 1. lấy dsnv check, lấy tháng
			 * 
			 */
			string strChecked_ArrMaCC = checkedDSNV.EditValue.ToString();
			if (string.IsNullOrEmpty(strChecked_ArrMaCC)) { ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000); return; }
			List<int> arrMaCC = new List<int>();
			strChecked_ArrMaCC.Split(new char[] { ',' }).ToList().ForEach(item => arrMaCC.Add(int.Parse(item)));

			List<cUserInfo> listNhanVien = new List<cUserInfo>();
			DataTable tableDSNV = checkedDSNV.Properties.DataSource as DataTable;
			if (tableDSNV == null) return;
			foreach (int maCC in arrMaCC) {
				DataRow[] row = tableDSNV.Select("UserEnrollNumber=" + maCC);
				cUserInfo nhanvien = new cUserInfo {
					MaCC = (int)row[0]["UserEnrollNumber"],
					MaNV = row[0]["UserFullCode"].ToString(),
					TenNV = row[0]["UserFullName"].ToString(),
				};
				XL.GetLichTrinhNV(nhanvien, row[0]["SchID"] != DBNull.Value ? (int)row[0]["SchID"] : (int?)null);
				listNhanVien.Add(nhanvien);
			}
			DateTime ngaybd = MyUtility.FirstDayOfMonth(dateNavigator1.DateTime);
			DateTime ngaykt = MyUtility.LastDayOfMonth(ngaybd);
			XL.XemCongThoiGianChuaKetLuong(listNhanVien, ngaybd, ngaykt);
			int soTH_ThieuChamCong,
				soTH_KoNhanDienCa,
				soTH_OLaiChuaXN,
				soTH_VaoTreRaSom,
				soTH_VaoRaEdited,
				soTH_DaXN,
				soTH_DaXN_LamThem,
				soTH_ChoPhepTreSom,
				soTH_VaoTre_RaSom_CoLamBu,
				soTH_XinPhepVang;
			this.GhiNhanThongBao(listNhanVien, ngaybd, ngaykt,
								 out soTH_ThieuChamCong, out soTH_KoNhanDienCa, out soTH_OLaiChuaXN, out soTH_VaoTreRaSom,
								 out soTH_VaoRaEdited, out soTH_DaXN, out soTH_DaXN_LamThem, out soTH_ChoPhepTreSom, out soTH_VaoTre_RaSom_CoLamBu,
								 out soTH_XinPhepVang);
			lbThieuChamCong.Text = string.Format(Resources.LabelText_ThieuChamCong, soTH_ThieuChamCong);
			lbKhongNhanDienCa.Text = string.Format(Resources.LabelText_KoNhanDienCa, soTH_KoNhanDienCa);
			lbOLaiChuaXN.Text = string.Format(Resources.LabelText_OLaiChuaXN, soTH_OLaiChuaXN);
			lbVaoTreRaSom.Text = string.Format(Resources.LabelText_VaoTreRaSom, soTH_VaoTreRaSom);
			lbVaoRaBiChinhSua.Text = string.Format(Resources.LabelText_VaoRaBiChinhSua, soTH_VaoRaEdited);
			lbDaXacNhanCa.Text = string.Format(Resources.LabelText_DaXacNhanCa, soTH_DaXN);
			lbDaXacNhanLamThem.Text = string.Format(Resources.LabelText_DaXacNhanLamThemGio, soTH_DaXN_LamThem);
			lbChoPhepTreSom.Text = string.Format(Resources.LabelText_ChoPhepTreSom, soTH_ChoPhepTreSom);
			lbTreSomCoLamBu.Text = string.Format(Resources.LabelText_TreSomCoLamBu, soTH_VaoTre_RaSom_CoLamBu);lbXinPhepVang.Text = string.Format(Resources.LabelText_XinPhepVang, soTH_XinPhepVang);
			DataTable table = tao();
			populatedata(listNhanVien, table);
			//dataGridView1.DataSource = table;
			gridControl1.DataSource = table;
			lbThieuChamCong.Tag = listNhanVien;
		}

		private void GhiNhanThongBao(List<cUserInfo> listNhanVien, DateTime ngaybd, DateTime ngaykt, out int soTH_ThieuChamCong, out int soTH_KoNhanDienCa, out int soTH_OLaiChuaXN, out int soTH_VaoTreRaSom, out int soTH_VaoRaEdited, out int soTH_DaXN, out int soTH_DaXN_LamThem, out int soTH_ChoPhepTreSom, out int soTH_VaoTre_RaSom_CoLamBu, out int soTH_XinPhepVang) {
			#region init int =0

			soTH_ThieuChamCong = 0;
			soTH_KoNhanDienCa = 0;
			soTH_OLaiChuaXN = 0;
			soTH_VaoTreRaSom = 0;
			soTH_VaoRaEdited = 0;
			soTH_DaXN = 0;
			soTH_DaXN_LamThem = 0;
			soTH_ChoPhepTreSom = 0;
			soTH_VaoTre_RaSom_CoLamBu = 0;
			soTH_XinPhepVang = 0;

			#endregion

			foreach (var nhanvien in listNhanVien) {
				foreach (var ngayCong in nhanvien.DSNgayCong.Where(item => item.Ngay >= ngaybd && item.Ngay <= ngaykt).ToList())
				{
					soTH_ThieuChamCong += ngayCong.DSVaoRa.Count(item => item.HaveINOUT < 0);
					soTH_KoNhanDienCa += ngayCong.DSVaoRa.Count(item => item.HaveINOUT >= 0 && item.DaXN == false && item.ThuocCa.ID < int.MinValue + 100);
					soTH_OLaiChuaXN += ngayCong.DSVaoRa.Count(item => item.HaveINOUT >= 0 && item.DaXN == false && item.TG5.OLai > TimeSpan.Zero);
					soTH_VaoTreRaSom += ngayCong.DSVaoRa.Count(item => item.HaveINOUT >= 0 && item.DaXN == false && (item.TG5.VaoTre > TimeSpan.Zero || item.TG5.RaaSom > TimeSpan.Zero));

					soTH_VaoRaEdited += ngayCong.DSVaoRa.Count(item => (item.Vao != null && item.Vao.Source == "PC")
																		|| (item.Raa != null && item.Raa.Source == "PC"));
					soTH_DaXN += ngayCong.DSVaoRa.Count(item => item.DaXN);
					soTH_DaXN_LamThem += ngayCong.DSVaoRa.Count(item => item.DaXN && item.TG5.SoPhutLamThem5 > TimeSpan.Zero);
					soTH_ChoPhepTreSom += ngayCong.DSVaoRa.Count(item => item.DuyetChoPhepVaoTre || item.DuyetChoPhepRaSom);
					soTH_VaoTre_RaSom_CoLamBu += ngayCong.DSVaoRa.Count(item => item.VaoTreTinhCV || item.RaaSomTinhCV);
					soTH_XinPhepVang += ngayCong.DSVang.Count;

				}
			}
		}

		private void populatedata(List<cUserInfo> dsnv, DataTable table) {
			foreach (var nhanvien in dsnv) {
				foreach (cNgayCong ngayCong in nhanvien.DSNgayCong) {
					DataRow row = table.NewRow();
					row["UserEnrollNumber"] = nhanvien.MaCC;
					row["UserFullCode"] = nhanvien.MaNV;
					row["UserFullName"] = nhanvien.TenNV;
					row["Ngay"] = ngayCong.Ngay;
					row["TongGioThucTe"] = ngayCong.TG5.TongGioThucTe;
					row["TongGioLamViec"] = ngayCong.TG5.TongGioLamViec;
					row["TongVaoTre"] = ngayCong.TG5.VaoTre;
					row["TongRaSom"] = ngayCong.TG5.RaaSom;
					row["TongGioLamNgay"] = ngayCong.TG5.TongGioLamNgay;
					row["TongGioLamDem"] = ngayCong.TG5.TongGioLamDem;
					row["TongGioTangCuong"] = ngayCong.TG5.TongGioTangCuong;
					row["GioLamNgay_KoTC"] = ngayCong.TG5.GioLamNgay_KoTC;
					row["HuongPC_TangCuongNgay"] = ngayCong.TG5.HuongPC_TangCuongNgay;
					row["HuongPC_Dem"] = ngayCong.TG5.HuongPC_Dem;
					row["HuongPC_TangCuongDem"] = ngayCong.TG5.HuongPC_TangCuongDem;
					row["PCNgay5"] = ngayCong.PhuCaps.PCNgay5;
					row["PCTangCuongNgay5"] = ngayCong.PhuCaps.PCTangCuongNgay5;
					row["PCDem5"] = ngayCong.PhuCaps.PCDem5;
					row["PCTangCuongDem5"] = ngayCong.PhuCaps.PCTangCuongDem5;
					row["_TongPC"] = ngayCong.PhuCaps._TongPC;
					row["LoaiPhuCap"] = ngayCong.PhuCaps.LoaiPhuCap;
					row["CaQuyDinh"] = ngayCong.Cong5.CaQuyDinh;
					row["ThucTe"] = ngayCong.Cong5.ThucTe;
					row["TTTrongCa"] = ngayCong.Cong5.TTTrongCa;
					row["TTNgoaiCa"] = ngayCong.Cong5.TTNgoaiCa;
					row["TTCongTre"] = ngayCong.Cong5.TTCongTre;
					row["TTCongSom"] = ngayCong.Cong5.TTCongSom;
					row["TongCongBu"] = ngayCong.Cong5.TongCongBu; row["TongCongTru"] = ngayCong.Cong5.TongCongTru;
					row["DinhMuc"] = ngayCong.Cong5.DinhMuc;
					row["KyHieuCa"] = ngayCong.ExportKyHieuThuocCa5();
					row["DanhSachXPVang"] = ngayCong.ExportKyHieuVang();
					//row[""] = ngayCong.Cong5. ;
					//row[""] = ngayCong.Cong5. ;

					table.Rows.Add(row);

				}

			}
		}

		public DataTable tao() {
			DataTable table = new DataTable();
			table.Columns.Add("UserEnrollNumber", typeof(int));
			table.Columns.Add("UserFullCode", typeof(string));
			table.Columns.Add("UserFullName", typeof(string));
			table.Columns.Add("Ngay", typeof(DateTime));
			table.Columns.Add("Thu", typeof(DateTime));
			table.Columns.Add("TongGioThucTe", typeof(TimeSpan));
			table.Columns.Add("TongGioLamViec", typeof(TimeSpan));
			table.Columns.Add("TongVaoTre", typeof(TimeSpan));
			table.Columns.Add("TongRaSom", typeof(TimeSpan));
			table.Columns.Add("TongGioLamNgay", typeof(TimeSpan));
			table.Columns.Add("TongGioLamDem", typeof(TimeSpan));
			table.Columns.Add("TongGioTangCuong", typeof(TimeSpan));
			table.Columns.Add("GioLamNgay_KoTC", typeof(TimeSpan));
			table.Columns.Add("HuongPC_TangCuongNgay", typeof(TimeSpan));
			table.Columns.Add("HuongPC_Dem", typeof(TimeSpan));
			table.Columns.Add("HuongPC_TangCuongDem", typeof(TimeSpan));
			table.Columns.Add("PCNgay5", typeof(float));
			table.Columns.Add("PCTangCuongNgay5", typeof(float));
			table.Columns.Add("PCDem5", typeof(float));
			table.Columns.Add("PCTangCuongDem5", typeof(float));
			table.Columns.Add("_TongPC", typeof(float));
			table.Columns.Add("LoaiPhuCap", typeof(int));
			table.Columns.Add("CaQuyDinh", typeof(float));
			table.Columns.Add("ThucTe", typeof(float));
			table.Columns.Add("TTTrongCa", typeof(float));
			table.Columns.Add("TTNgoaiCa", typeof(float));
			table.Columns.Add("TTCongTre", typeof(float));
			table.Columns.Add("TTCongSom", typeof(float));
			table.Columns.Add("TongCongBu", typeof(float));
			table.Columns.Add("TongCongTru", typeof(float));
			table.Columns.Add("DinhMuc", typeof(float));
			table.Columns.Add("KyHieuCa", typeof(string)); table.Columns.Add("DanhSachXPVang", typeof(string));
			//table.Columns.Add("", typeof(float));
			//table.Columns.Add("", typeof(float));
			//table.Columns.Add("", typeof());

			return table;
		}

		private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e) {
/*
			if (e.SelectedControl != gridControl1) return;
			
			ToolTipControlInfo info = null;
			//Get the view at the current mouse position
			GridView view = gridControl1.GetViewAt(e.ControlMousePosition) as GridView;
			if (view == null) return;
			//Get the view's element information that resides at the current position
			GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
			//Display a hint for row indicator cells
			if (hi.HitTest == GridHitTest.RowCell) {
				//An object that uniquely identifies a row indicator cell
				object o = hi.HitTest.ToString() + hi.RowHandle.ToString();
				string text = "Row " + hi.RowHandle.ToString();
				info = new ToolTipControlInfo(o, text);
			}
			//Supply tooltip information if applicable, otherwise preserve default tooltip (if any)
			if (info != null)
				e.Info = info;
*/

		}

		private void lbThieuChamCong_Click(object sender, EventArgs e) {
			zMisc.fmXacNhanCa frm = new zMisc.fmXacNhanCa();
			frm.m_DSNV = (List<cUserInfo>) lbThieuChamCong.Tag;
			frm.m_NgayBD = 			 MyUtility.FirstDayOfMonth(dateNavigator1.DateTime);
			frm.m_NgayKT = 	 MyUtility.LastDayOfMonth(frm.m_NgayBD);

			frm.Show();
		}
	}
}
