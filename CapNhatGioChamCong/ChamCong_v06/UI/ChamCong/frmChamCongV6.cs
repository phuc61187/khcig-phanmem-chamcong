using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v06.BUS;
using ChamCong_v06.DTO;
using ChamCong_v06.Helper;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace ChamCong_v06.UI.ChamCong {
	public partial class frmChamCongV6 : Form
	{
		private List<DataRow> m_NhanVienDR = new List<DataRow>();// lưu danh sách nhân viên trong  tất cả phòng đang chọn
		private List<cPhongBan> m_AllPhong = new List<cPhongBan>();
		private List<cPhongBan> m_SelectedPhong = new List<cPhongBan>();
		private List<cNhomCa> m_AllNhomCa = new List<cNhomCa>();

		public frmChamCongV6() {
			InitializeComponent();
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
			loadTreePhgBan(treePhongBan);
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;

			// xác định lịch trình tổng quan
			BUS_LichTrinh_Ca busLichTrinhCa = new BUS_LichTrinh_Ca();
			busLichTrinhCa.LayTatCaLichTrinhVaCa(ref m_AllNhomCa);

		}
		#region cách làm có store procedure
		private TreeView loadTreePhgBan(TreeView tvDSPhongBan) {
			// load các phòng ban Enable được phép thao tác theo tài khoản hiện tại và sắp xếp theo vị trí
			tvDSPhongBan.Nodes.Clear();
			DataTable tableDSPhong = SqlDataAccessHelper.ExecSPQuery(SPName6.DeptPrivilege_DocPhongBanThaoTacV6.ToString(),
				new SqlParameter("@UserID", XL2.currUserID), new SqlParameter("@ChoPhepThaoTac", true),
				new SqlParameter("@RelationDeptEnable", true));
			var allPhongDR = (from DataRow row in tableDSPhong.Rows select row).OrderBy(s => (int)s["ViTri"]);
			// xác định root node là Node luôn có RelationID = 0(IDCha = 0 tức là gốc ko có cha nữa)
			// nếu ko tìm được node root này thì thoát form
			var relationID_0_DR = allPhongDR.Where(o => (int)o["RelationID"] == 0).ToList().OrderBy(s => (int)s["ViTri"]);
			if (!relationID_0_DR.Any()) return null;

			// sau khi xác định root thì lần lượt load từng subNode vào và gán tag là dataRow phòng
			m_AllPhong = new List<cPhongBan>();
			foreach (var dataRow in relationID_0_DR) {
				var string2 = dataRow["Description"].ToString();
				var phongROOT = new cPhongBan
				{
					TrucThuoc = null,
					Phong = new ID_Description {ID = (int) dataRow["IDDepartment"], Description = string2},
					LevelID = (int) dataRow["LevelID"],
					ViTri = (int) dataRow["ViTri"],
					Enable = (bool) dataRow["Enable"]
				};
				TreeNode parentNode = new TreeNode { Text = string2, Tag = phongROOT };
				tvDSPhongBan.Nodes.Add(parentNode);
				m_AllPhong.Add(phongROOT);
				loadTreeSubNode(ref parentNode, (int)dataRow["IDDepartment"], allPhongDR/*TatcaPhongban*/, phongROOT, m_AllPhong);
			}


			return tvDSPhongBan;
		}

		private void loadTreeSubNode(ref TreeNode ParentNode, int idPhongBanTrucThuoc, IOrderedEnumerable<DataRow> dsphongban, cPhongBan PhongTrucThuoc, List<cPhongBan> DSTatCaPhong) {
			IOrderedEnumerable<DataRow> childsDR = dsphongban.Where(item => (int)item["RelationID"] == idPhongBanTrucThuoc).ToList().OrderBy(item => (int)item["ViTri"]);
			foreach (DataRow dataRow in childsDR) {
				var enable = (bool)(dataRow["Enable"]);
				var string1 = enable == false ? "[Disable]" : string.Empty;
				var string2 = dataRow["Description"].ToString();

				var phongCon = new cPhongBan {
					TrucThuoc = PhongTrucThuoc,
					Phong = new ID_Description { ID = (int)dataRow["IDDepartment"], Description = string2 },
					LevelID = (int)dataRow["LevelID"],
					ViTri = (int)dataRow["ViTri"],
					Enable = (bool)dataRow["Enable"]
				};
				TreeNode child = new TreeNode { Text = string1 + string2, Tag = phongCon };
				ParentNode.Nodes.Add(child);
				DSTatCaPhong.Add(phongCon);
				loadTreeSubNode(ref child, (int)dataRow["IDDepartment"], dsphongban, phongCon, DSTatCaPhong);
			}
		}

		#endregion

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			#region mỗi lần chọn node thì lấy ID node hiện tại và tất cả node con

			this.m_SelectedPhong.Clear();
			this.m_NhanVienDR.Clear();
			e.Node.Expand();
			XacDinhPhongDangChon(e.Node, ref m_SelectedPhong);
			DataTable tableNhanVien;
			LayTableNhanVien(out tableNhanVien, this.m_SelectedPhong);
			m_NhanVienDR = (from DataRow dataRow in tableNhanVien.Rows select dataRow).ToList();

			//			checkedComboBoxEdit1.Properties.Items.Clear();
			checkedDSNV.Properties.DataSource = tableNhanVien;
			checkedDSNV.Properties.DisplayMember = "DisplayName_Code";// kết hợp của tên và mã nhân viên
			checkedDSNV.Properties.ValueMember = "UserEnrollNumber";

			#endregion

/*

			/* 1. clear select chọn nhân viên trước
			 * 2. load datasource cho check chọn nhân viên 
			 * 3. gán lại tên cho các column ngày
			 #1#
*/

		}

		private void LayTableNhanVien(out DataTable tableNhanVien, List<cPhongBan> SelectedPhong)
		{
			List<int> listIDPhong = (from cPhongBan phong in SelectedPhong select phong.Phong.ID).ToList();
			DataTable tableArrayIDPhong = MyUtility.Array_To_DataTable("tableArrayIDD", listIDPhong);
			tableNhanVien = SqlDataAccessHelper.ExecSPQuery(SPName6.UserInfo_DocNhanVienChamCongV6.ToString(),
			                                                new SqlParameter("@ArrayIDDepartment", SqlDbType.Structured) {Value = tableArrayIDPhong},
			                                                new SqlParameter("@DepartmentEnable", true),
			                                                new SqlParameter("@UserEnabled", true));
		}

		private void XacDinhPhongDangChon(TreeNode root, ref List<cPhongBan> DSPhongDangChon) {
			//logic, mỗi lần chọn 1 node là lấy danh sách phòng đang chọn và các phòng ban được phép thao tác bên dưới
			if (root == null) return;
			//DSPhongChonDR.Add((DataRow)root.Tag); // 
			DSPhongDangChon.Add((cPhongBan)root.Tag);
			for (int i = 0; i < root.Nodes.Count; i++)
			{
				XacDinhPhongDangChon(root.Nodes[i], ref DSPhongDangChon);
			}
		}

		private void btnChamCong_Click(object sender, EventArgs e)
		{
			DateTime Thang = dateNavigator1.DateTime;
			Thang = MyUtility.FirstDayOfMonth(Thang);
			//1. xác định danh sach nhan vien dang check;  LẬP DS nhân viên đó để chấm công
			List<int> listUEN;
			GetList_UEN_Checked(checkedDSNV, out listUEN);// lấy danh sách các mã nhân viên check vì checkcomboBox ko cho phép lấy datarowview
			List<cUserInfo> listDSNV;
			BUS_NhanVien busNhanVien = new BUS_NhanVien();
			busNhanVien.KhoiTaoDSNV_DuocChon(listUEN, this.m_NhanVienDR, this.m_SelectedPhong, this.m_AllNhomCa, out listDSNV);
			BUS_ChamCong busChamCong = new BUS_ChamCong();
			//BUS_ChamCong.ChamCong(listDSNV, Thang);
		}




		private void GetList_UEN_Checked(CheckedComboBoxEdit checkedComboBox_NhanVien, out List<int> List_UEN)
		{
			// lấy danh sách các mã nhân viên check
			List_UEN = (from CheckedListBoxItem item in checkedComboBox_NhanVien.Properties.Items
			            where item.CheckState == CheckState.Checked
			            select (int) item.Value).ToList();
		}

/*
		private void btnChamCong_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;
			/* 1. lấy dsnv check, lấy tháng
			 * 
			 #1#
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
#1#

		}

		private void lbThieuChamCong_Click(object sender, EventArgs e) {
			zMisc.fmXacNhanCa frm = new zMisc.fmXacNhanCa();
			frm.m_DSNV = (List<cUserInfo>) lbThieuChamCong.Tag;
			frm.m_NgayBD = 			 MyUtility.FirstDayOfMonth(dateNavigator1.DateTime);
			frm.m_NgayKT = 	 MyUtility.LastDayOfMonth(frm.m_NgayBD);

			frm.Show();
		}
*/
	}
}
