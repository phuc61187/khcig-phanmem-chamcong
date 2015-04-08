using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v05.DAL;
using ChamCong_v05.DTO;
using ChamCong_v05.Helper;
using ChamCong_v05.Properties;

namespace ChamCong_v05.BUS {
	public static partial class XL {
		#region hàm cũ ko dụng  chỉ giữk o bị lỗi
		public static void LoaiBoCheckKoHopLe1(List<cCheck> ds_Check_A, ref List<cCheck> ds_Check_Trong30ph) {

		}
		public static List<cUserInfo> KhoiTaoDSNV_ChamCong(List<cUserInfo> dsnv, List<int> m_listIDPhongBan, List<cPhongBan> dsphong) {
			return new List<cUserInfo>();
		}
		public static List<cUserInfo> KhoiTaoDSNV_TinhLuong(List<cUserInfo> dsnv, List<cPhongBan> dsphong) {
			return new List<cUserInfo>();
		}

		public static cUserInfo KhoiTaoNV(int uen, string ten, string manv,
			float? hslCB, float? hslCV, float? hsBHcongthem, List<cPhongBan> phongBans,
			int? schID = null, int? idChucVu = null, string ChucVu = null,
			int? maphong = null) {
			return new cUserInfo();
		}


		public static void DiemDanh_v08(List<cUserInfo> dsnv, DateTime ngayBD_Bef2D, DateTime ngayKT_Aft2D, DateTime ngaydiemdanh, DateTime currentTime) {
		}

		public static void DiemDanhTheoNgay(List<cNgayCong> dsNgayCong, DateTime dateDiemDanh, DateTime currentTime) {
		}

		public static void XetCa_ListCIO_A3(List<cCheckInOut> ds_CIO_A, cShiftSchedule lichtrinh, List<cCheck> ds_raa3_vao1, List<cCheck> ds_check_A) { }

		public static void XetCa_1_CIO_A_KoTachCa(cCheckInOut CIO, cShiftSchedule lichtrinh) { }

		public static void XetCa_1_CIO_V(cCheckInOut chkInOutV, cShiftSchedule lichtrinh) { }

		public static void TinhTG_LV_LVCa3_LamThem1Ca(DateTime ThuocNgayCong, int HaveINOUT, Boolean DaXN,
	bool KoTruVaoTre, bool KoTruRaaSom,
	bool VaotreTinhCV, bool RaaSomTinhCV, //ver 4.0.0.4	
	DateTime Vao, DateTime Raa,
	TimeSpan DutyOnn, TimeSpan DutyOff, TimeSpan chophepTreTS, TimeSpan chophepSomTS, TimeSpan batdaulamthemTS,
	TimeSpan LunchMin, TimeSpan SoPhutLamThem,
	TimeSpan startNT, TimeSpan endddNT, //ver 4.0.0.4
	out DateTime TD_BD_LV, out DateTime TD_KT_LV, out DateTime TD_KT_LV_TrongCa,
	out DateTime TD_BD_LV_Ca3, out DateTime TD_KT_LV_Ca3,
	out TimeSpan TGThucTe, out TimeSpan TGGioLamViec, out TimeSpan TGVaoTre, out TimeSpan TGRaaSom,
	out TimeSpan TGGioLamViecTrongCa, //ver 4.0.0.4	
	out TimeSpan TGOLai, out TimeSpan TGLamThem, out bool QuaDem, out TimeSpan TGLamBanDem) {
			#region khởi tạo biến

			TD_BD_LV = DateTime.MinValue;
			TD_KT_LV_TrongCa = DateTime.MinValue; // chưa cộng OT
			TD_KT_LV = DateTime.MinValue; // đã cộng OT
			TD_BD_LV_Ca3 = DateTime.MinValue;
			TD_KT_LV_Ca3 = DateTime.MinValue;
			TGThucTe = TimeSpan.Zero;
			TGVaoTre = TimeSpan.Zero;
			TGRaaSom = TimeSpan.Zero;
			TGOLai = TimeSpan.Zero;
			TGGioLamViec = TimeSpan.Zero; // tổng thời gian làm việc đã gồm OT
			TGGioLamViecTrongCa = TimeSpan.Zero; //ver 4.0.0.4	
			TGLamThem = TimeSpan.Zero;
			TGLamBanDem = TimeSpan.Zero;
			QuaDem = false;

			#endregion
		}
		public static void TinhCong_ListNgayCong8(List<cNgayCong> dsNgayCong, TimeSpan startNT, TimeSpan endddNT) {
		}
		public static void TinhCong_HangNgay(cNgayCong ngayCong, TimeSpan startNT, TimeSpan endddNT,
			out ThoiGian TG, out PhuCap PhuCaps, out float TongCong, out float TongNgayLV, out bool QuaDem) {
			TG = new ThoiGian();
			PhuCaps = new PhuCap();
			TongCong = 0f;
			TongNgayLV = 0f; //ver4.0.0.1
			QuaDem = false;
			ngayCong.TrangThaiDiemDanh = TrangThaiDiemDanh.VANG_NGHI;
			// tính công của từng ThuocCa làm việc, sau đó tổng hợp Công làm việc của 1 ngày
			if (ngayCong.DSVaoRa.Count == 0) return;
			foreach (var CIO in ngayCong.DSVaoRa) {
				CIO.TD = new ThoiDiem();
				CIO.TG = new ThoiGian();
				TinhTG_LV_LVCa3_LamThem1Ca(CIO.ThuocNgayCong, CIO.HaveINOUT, CIO.DaXN, CIO.DuyetChoPhepVaoTre, CIO.DuyetChoPhepRaSom, CIO.VaoTreTinhCV, CIO.RaaSomTinhCV,
					CIO.Vao.Time, CIO.Raa.Time, CIO.ThuocCa.Duty.Onn, CIO.ThuocCa.Duty.Off, CIO.ThuocCa.chophepTreTS, CIO.ThuocCa.chophepSomTS,
					CIO.ThuocCa.batdaulamthemTS, CIO.ThuocCa.LunchMin, new TimeSpan(0, CIO.OTMin, 0), startNT, endddNT,
					out CIO.TD.BD_LV, out CIO.TD.KT_LV, out CIO.TD.KT_LV_ChuaOT, out CIO.TD.BD_LV_Ca3, out CIO.TD.KT_LV_Ca3,
				out CIO.TG.GioThucTe5, out CIO.TG.GioLamViec5, out CIO.TG.VaoTre, out CIO.TG.RaaSom,
				out CIO.TG.GioLVTrongCa5,//ver 4.0.0.4	
				out CIO.TG.OLai, out CIO.TG.LamTangCuong, out CIO.QuaDem, out CIO.TG.LamBanDem);
				//if (CIO.QuaDem) QuaDem = true; // set qua đêm nếu có
				float cong_trong_ca = Convert.ToSingle(Math.Round(((CIO.TG.GioLVTrongCa5.TotalHours / CIO.ThuocCa.WorkingTimeTS.TotalHours) * CIO.ThuocCa.Workingday), 2));
				float cong_bi_tru_TreSom = CIO.ThuocCa.Workingday - cong_trong_ca;
				float cong_ngoai_ca = Convert.ToSingle(Math.Round((CIO.TG.SoPhutLamThem5.TotalHours / 8f), 2));// tương đương giờ làm việc ngoài ThuocCa, làm ngoài ThuocCa chưa chắc OT ví dụ nửa ThuocCa
				CIO.Cong = cong_trong_ca + cong_ngoai_ca;
				TG.GioThucTe5 += CIO.TG.GioThucTe5;
				TG.GioLamViec5 += CIO.TG.GioLamViec5;
				TG.GioLVTrongCa5 += CIO.TG.GioLVTrongCa5;
				TG.LamBanDem += CIO.TG.LamBanDem;
				TG.VaoTre += CIO.TG.VaoTre;
				TG.RaaSom += CIO.TG.RaaSom;
				TongCong += CIO.Cong; //công đã được làm tròn 2 số thập phân ở trên
				if ((CIO.DuyetChoPhepVaoTre && CIO.DuyetChoPhepRaSom)
					|| (CIO.DuyetChoPhepVaoTre == false && CIO.DuyetChoPhepRaSom == false && CIO.VaoTreTinhCV == false && CIO.RaaSomTinhCV == false)) {
					ngayCong.TongNgayLV += CIO.ThuocCa.Workingday;
				}
				else {
					if (CIO.DuyetChoPhepVaoTre == false && CIO.VaoTreTinhCV)
						ngayCong.TongNgayLV += cong_trong_ca;
					else if (CIO.DuyetChoPhepRaSom == false && CIO.RaaSomTinhCV)
						ngayCong.TongNgayLV += cong_trong_ca;
					else ngayCong.TongNgayLV += CIO.ThuocCa.Workingday;
				}
				ngayCong.TongNgayLV += cong_ngoai_ca;
				//ngayCong.TongNgayLV += (CIO.Cong > CIO.ThuocCa.Workingday) ? CIO.Cong : CIO.ThuocCa.Workingday;//ver4.0.0.1
			}
			ngayCong.TG.LamTangCuong = Tinh_TGLamTangCuong(ngayCong.TG.GioLamViec5);// (ngayCong.TG.GioLamViec - XL2._08gio > XL2._01phut) ? ngayCong.TG.GioLamViec - XL2._08gio : TimeSpan.Zero;			
			ngayCong.PhuCaps._30_dem = Convert.ToSingle(Math.Round((ngayCong.TG.LamBanDem.TotalHours / 8d) * (XL2.PC30 / 100f), 2, MidpointRounding.ToEven));
			ngayCong.PhuCaps._TongPC = ngayCong.PhuCaps._30_dem;
		}
		public static void Tinh_PCTC(bool TinhPC50, bool QuaDem, TimeSpan SoGioLamDemmm, TimeSpan SoGioLamThem,
			out TimeSpan tgTinh130, out  TimeSpan tgTinh150, out  TimeSpan tgTinhTCC3,
			out float PhuCap30, out float PhuCapTC, out float PhuCapTCC3, out float TongPhuCap) {
			tgTinh130 = tgTinh150 = tgTinhTCC3 = TimeSpan.Zero;
			PhuCap30 = 0f;
			PhuCapTC = 0f;
			PhuCapTCC3 = 0f;
			TongPhuCap = 0f;
		}


		#endregion
		public static void DocSetting() {
			var table = SqlDataAccessHelper.ExecuteQueryString("select * from Setting", null, null);
			for (int i = 0; i < table.Rows.Count; i++) {
				var row = table.Rows[i];
				var id = (int)row["ID"];
				var code = row["Code"].ToString();
				var value = row["Value"].ToString();
				switch (code) {
					#region phụ cấp

					case "PC30":
						var val30 = 0;
						if (int.TryParse(value, out val30)) {
							XL2.PC30 = val30;
						}
						break;
					case "PC50":
						var val50 = 0;
						if (int.TryParse(value, out val50)) {
							XL2.PC50 = val50;
						}
						break;
					case "PCTCC3":
						var valTCC3 = 0;
						if (int.TryParse(value, out valTCC3)) {
							XL2.PCTCC3 = valTCC3;
						}
						break;
					case "PC100":
						var val100 = 0;
						if (int.TryParse(value, out val100)) {
							XL2.PC100 = val100;
						}
						break;
					case "PC160":
						var val160 = 0;
						if (int.TryParse(value, out val160)) {
							XL2.PC160 = val160;
						}
						break;
					case "PC200":
						var val200 = 0;
						if (int.TryParse(value, out val200)) {
							XL2.PC200 = val200;
						}
						break;
					case "PC290":
						var val290 = 0;
						if (int.TryParse(value, out val290)) {
							XL2.PC290 = val290;
						}
						break;

					#endregion
					case "TGLamDemToiThieu":
						var valTGLamDemToiThieu = new TimeSpan(0, 0, 0);
						if (TimeSpan.TryParse(value, out valTGLamDemToiThieu)) {
							XL2.TGLamDemToiThieu = valTGLamDemToiThieu;
						}
						break;

					#region số phút cho phép trễ sớm afterot ca tự do

					case "ChoPhepTre":
						var valChoPhepTre = 0;
						if (int.TryParse(value, out valChoPhepTre)) {
							XL2.ChoPhepTre = new TimeSpan(0, valChoPhepTre, 0);
						}
						break;
					case "ChoPhepSom":
						var valChoPhepSom = 0;
						if (int.TryParse(value, out valChoPhepSom)) {
							XL2.ChoPhepSom = new TimeSpan(0, valChoPhepSom, 0);
						}
						break;
					case "LamThemAfterOT":
						var valLamThemAfterOT = 0;
						if (int.TryParse(value, out valLamThemAfterOT)) {
							XL2.LamThemAfterOT = new TimeSpan(0, valLamThemAfterOT, 0);
						}
						break;

					#endregion
				}
			}

		}

		public static void SaveSetting(string connectionstring = null, string lastAccLogIn = null, string lastUserName = null,
			string lastDatabase = null, string lastServerName = null,
			DateTime? lastStartDate = null, DateTime? lastEndDate = null,
			int? lastSanLuong = null, int? lastDonGia = null,
			int? lastSanLuongGiaCongNoiBo = null, int? lastDonGiaGiaCongNoiBo = null,
			int? lastSanLuongGiaCongNgoai = null, int? lastDonGiaGiaCongNgoai = null,
			int? lastLuongToiThieu = null, int? lastBoiDuongCa3 = null,
			string lastTenNVLapBieuChamCong = null, string lastTenNVLapBieuLuong = null, string lastTenTruongBP = null
			) {
			if (string.IsNullOrEmpty(connectionstring) == false) Settings.Default.ConnectionStringPath = connectionstring;
			if (string.IsNullOrEmpty(lastAccLogIn) == false) Settings.Default.LastAccLogIn = lastAccLogIn;
			if (string.IsNullOrEmpty(lastUserName) == false) Settings.Default.LastUsername = lastUserName;
			if (string.IsNullOrEmpty(lastDatabase) == false) Settings.Default.LastDatabase = lastDatabase;
			if (string.IsNullOrEmpty(lastServerName) == false) Settings.Default.LastServerName = lastServerName;
			if (lastStartDate != null) Settings.Default.LastStartDate = new DateTime(lastStartDate.Value.Year, lastStartDate.Value.Month, lastStartDate.Value.Day);
			if (lastEndDate != null) Settings.Default.LastEndDate = new DateTime(lastEndDate.Value.Year, lastEndDate.Value.Month, lastEndDate.Value.Day);
			if (lastSanLuong != null) Settings.Default.LastSanLuong = (int)lastSanLuong;
			if (lastDonGia != null) Settings.Default.LastDonGia = (int)lastDonGia;
			if (lastSanLuongGiaCongNoiBo != null) Settings.Default.LastSanluongGiaCongNoibo = (int)lastSanLuongGiaCongNoiBo;
			if (lastDonGiaGiaCongNoiBo != null) Settings.Default.LastDongiaGiacongNoibo = (int)lastDonGiaGiaCongNoiBo;
			if (lastSanLuongGiaCongNgoai != null) Settings.Default.LastSanluongGiacongNgoai = (int)lastSanLuongGiaCongNgoai;
			if (lastDonGiaGiaCongNgoai != null) Settings.Default.LastDongiaGiacongNgoai = (int)lastDonGiaGiaCongNgoai;
			if (lastLuongToiThieu != null) Settings.Default.LastLuongToiThieu = (int)lastLuongToiThieu;
			if (lastBoiDuongCa3 != null) Settings.Default.LastBoiDuongCa3 = (int)lastBoiDuongCa3;
			if (lastTenNVLapBieuChamCong != null) Settings.Default.LastTenNVLapBieuChamCong = lastTenNVLapBieuChamCong;
			if (lastTenNVLapBieuLuong != null) Settings.Default.LastTenNVLapBieuLuong = lastTenNVLapBieuLuong;
			if (lastTenTruongBP != null) Settings.Default.LastTenTruongBP = lastTenTruongBP;
			//if ( != null) Settings.Default. = ();
			Settings.Default.Save();
		}

		#region xử lý load treeView dsnv ở các form

		#region cách làm không store procedure

		public static TreeView loadTreePhgBan(TreeView tvDSPhongBan, List<cPhongBan> TatcaPhongban, List<cPhongBan> dsPhongduocThaotac)
		{
			tvDSPhongBan.Nodes.Clear();

			//load tất cả phòng ban vào tree
			foreach (cPhongBan phong in TatcaPhongban.FindAll(item => item.idParent == 0).OrderBy(item => item.ViTri))
			{
				TreeNode parentNode = new TreeNode {Text = phong.Ten, Tag = phong};
				tvDSPhongBan.Nodes.Add(parentNode);
				loadTreeSubNode(ref parentNode, phong.ID, TatcaPhongban);
			}

			// lập ds các node chứa các phòng ban được thao tác
			TreeNode root = tvDSPhongBan.TopNode;

			List<TreeNode> dsNodeThaotac = new List<TreeNode>();
			GetListNode_Thaotac(root, dsPhongduocThaotac, ref dsNodeThaotac); // lấy tất cả các node chứa phòng ban được thao tác

			// thêm vào danh sách các node không được thao tác nhưng nằm trên đường dẫn đến node được thao tác
			for (int i = 0; i < dsNodeThaotac.Count; i++)
			{
				TreeNode node = dsNodeThaotac[i];
				GetNodesInPath(node.Parent, dsNodeThaotac);
			}

			// loại bỏ các node ko được thao tác và không thuộc đường dẫn đến node thao tác
			RemoveNodeNotInPath(ref root, dsNodeThaotac);

			return tvDSPhongBan;
		}

		private static bool RemoveNodeNotInPath(ref TreeNode root, List<TreeNode> nodesInPath)
		{
			if (root == null)
				return false;

			if (root.Nodes.Count > 0)
			{
				int i = 0;
				// sử dụng kiểu này vì khi xoá 1 node thì danh sách node bị thay đổi --> foreach ko cho phép
				// sử dụng for i thì khi xoá node, node sau dồn lên, index i phải giữ nguyên ko được ++
				while (i < root.Nodes.Count)
				{
					TreeNode node = root.Nodes[i];
					bool kq = RemoveNodeNotInPath(ref node, nodesInPath);
					if (kq == false) i++; // điều kiện tăng i là nếu xoá được node, ngược lại hết đường thì dừng, 
				}
			}

			bool found1 = false;
			for (int i = 0; i < nodesInPath.Count; i++)
			{
				TreeNode node = nodesInPath[i];
				if (root != node) continue;
				else
				{
					found1 = true;
					break;
				}
			}
			if (found1 == false)
			{
				root.Remove();
				return true;
			}
			return false;

		}

		private static void GetNodesInPath(TreeNode root, List<TreeNode> nodes)
		{
			if (root == null) return;

			// nếu curr đã nằm trong ds node được thao tác thì trùng ko add thêm 
			bool found = false;
			for (int i = nodes.Count - 1; i >= 0; i--)
			{
				if (nodes[i] != root) continue;
				found = true; //nodes[i] == root ==> đã tồn tại trong danh sách -> break vòng lặp, ko add mà tiếp tục 
				break;
			}
			if (found == false) nodes.Add(root); // ko tìm thấy --> đó là node mới--> add node mới
			GetNodesInPath(root.Parent, nodes); // tiếp tục kiểm tra node cha của node hiện tại
		}

		private static void GetListNode_Thaotac(TreeNode root, List<cPhongBan> dsPhongduocThaotac, ref List<TreeNode> nodes)
		{
			if (root == null) return;

			// nếu currNode được phép thao tác thì add current node
			for (int i = 0; i < dsPhongduocThaotac.Count; i++)
			{
				cPhongBan phongDuocThaotac = dsPhongduocThaotac[i];
				cPhongBan phongDangXet = (cPhongBan) root.Tag;
				if (phongDangXet.ID == phongDuocThaotac.ID)
				{
					phongDangXet.ChoPhep = true;
					nodes.Add(root);
				}
			}

			if (root.Nodes.Count > 0)
			{
				// nếu có node con thì add các node con thoả điều kiện
				for (int i = 0; i < root.Nodes.Count; i++)
				{
					TreeNode node = root.Nodes[i];
					GetListNode_Thaotac(node, dsPhongduocThaotac, ref nodes);
				}
			}
			// ko có node con thì dừng

		}

		public static void loadTreeSubNode(ref TreeNode ParentNode, int idPhongBanTrucThuoc, List<cPhongBan> dsphongban)
		{
			List<cPhongBan> childs = dsphongban.Where(item => item.idParent == idPhongBanTrucThuoc).OrderBy(item => item.ViTri).ToList();
			foreach (cPhongBan phong in childs)
			{
				TreeNode child = new TreeNode {Text = phong.Ten, Tag = phong, ToolTipText = phong.Ten};
				ParentNode.Nodes.Add(child);
				loadTreeSubNode(ref child, phong.ID, dsphongban);
			}
		}

		public static void GetIDNodeAndChildNode(TreeNode root, ref List<int> listID){
			if (root == null) return;

			cPhongBan phong = (cPhongBan) root.Tag;
			if (phong != null && phong.ChoPhep) listID.Add(phong.ID);

			if (root.Nodes.Count > 0){
				foreach (TreeNode node in root.Nodes){
					GetIDNodeAndChildNode(node, ref listID);
				}
			}
			// xuốn đến đây tương đương root.Nodes.Count== 0; return
		}

		#endregion

		#region cách làm có store procedure
		public static TreeView loadTreePhgBan(TreeView tvDSPhongBan) {
			tvDSPhongBan.Nodes.Clear();
			DataTable tableDSPhong = SqlDataAccessHelper.ExecSPQuery(SPName.sp_RelationDept_DocTatCaPhongBan.ToString());
			DataTable tableDSPhongThacTac = SqlDataAccessHelper.ExecSPQuery(SPName.sp_RelationDept_DocDSPhongDuocThaoTac.ToString(),
				new SqlParameter("@UserID", XL2.currUserID));

			var rowsPhong = (from DataRow row in tableDSPhong.Rows select row).OrderBy(s => (int)s["ViTri"]);
			var relationID_0 = rowsPhong.Where(o => (int)o["RelationID"] == 0).ToList().OrderBy(s => (int)s["ViTri"]);
			var rowsPhongThaoTac = (from DataRow row in tableDSPhongThacTac.Rows select row).OrderBy(s => (int)s["ViTri"]);

			foreach (var dataRowView in relationID_0) {
				TreeNode parentNode = new TreeNode { Text = dataRowView["Description"].ToString(), Tag = dataRowView };
				tvDSPhongBan.Nodes.Add(parentNode);
				loadTreeSubNode(ref parentNode, (int)dataRowView["ID"], rowsPhong/*TatcaPhongban*/);
			}

			// lập ds các node chứa các phòng ban được thao tác
			TreeNode root = TopNode(tvDSPhongBan.TopNode);
			List<TreeNode> dsNodeThaotac = new List<TreeNode>();
			GetListNode_Thaotac(root, rowsPhongThaoTac.ToList(), ref dsNodeThaotac);// lấy tất cả các node chứa phòng ban được thao tác
			// loại bỏ các node ko được thao tác và không thuộc đường dẫn đến node thao tác
			RemoveNodeNotInPath(ref root, dsNodeThaotac);


			return tvDSPhongBan;
		}

		public static TreeNode TopNode(TreeNode root) {
			if (root.Parent != null) return root.Parent;
			if (root.PrevNode != null) return root.PrevNode;
			return root;
		}

		public static void loadTreeSubNode(ref TreeNode ParentNode, int idPhongBanTrucThuoc, IOrderedEnumerable<DataRow> dsphongban /*List<cPhongBan> dsphongban*/) {
			IOrderedEnumerable<DataRow> childs = dsphongban.Where(item => (int)item["RelationID"] == idPhongBanTrucThuoc).ToList().OrderBy(item => (int)item["ViTri"]);
			foreach (DataRow phong in childs) {
				TreeNode child = new TreeNode { Text = phong["Description"].ToString(), Tag = phong };
				ParentNode.Nodes.Add(child);
				loadTreeSubNode(ref child, (int)phong["ID"], dsphongban);
			}
		}

		private static void GetListNode_Thaotac(TreeNode root, List<DataRow> dsPhongduocThaotac, ref List<TreeNode> nodes) {
			if (root == null) return;

			// nếu currNode được phép thao tác thì add current node
			for (int i = 0; i < dsPhongduocThaotac.Count; i++) {
				DataRow phongDangXet = (DataRow)root.Tag; // isyes phongDangXet = 0
				DataRow phongDuocThaotac = dsPhongduocThaotac[i]; //isyes phongDuocThaotac =1
				if ((int)phongDangXet["ID"] == (int)phongDuocThaotac["ID"]) {
					phongDangXet["IsYes"] = 1;
					nodes.Add(root);
				}
			}

			if (root.Nodes.Count > 0) { // nếu có node con thì add các node con thoả điều kiện
				for (int i = 0; i < root.Nodes.Count; i++) {
					TreeNode node = root.Nodes[i];
					GetListNode_Thaotac(node, dsPhongduocThaotac, ref nodes);
				}
			}
			// ko có node con thì dừng

		}
		
		public static void GetIDNodeAndChildNode1(TreeNode root, ref List<int> listID) {
			if (root == null) return;

			DataRow phong = (DataRow)root.Tag;
			if (phong != null && (int)phong["IsYes"] == 1) listID.Add((int)phong["ID"]);

			if (root.Nodes.Count > 0) {
				foreach (TreeNode node in root.Nodes) {
					GetIDNodeAndChildNode1(node, ref listID);
				}
			}
			// xuốn đến đây tương đương root.Nodes.Count== 0; return
		}
		#endregion

		#endregion

		public static void ChuanBiDSLichTrinhVaCa() {

			XL.DSCaMoRong.Clear();
			XL.DSCa.Clear();

			//lấy danh sách tất cả các ca
			var tableDSCa = DAO5.LayDSCa();
			List<cCa> tempList = new List<cCa>();
			foreach (DataRow row in tableDSCa.Rows) {
				#region transfer dữ liệu từ row sang đối tượng
				var iShiftID = (int)row["ShiftID"];
				var sShiftCode = row["ShiftCode"].ToString();

				TimeSpan tsOnDuty;
				TimeSpan.TryParse(row["Onduty"].ToString(), out tsOnDuty);
				TimeSpan tOffDuty;
				TimeSpan.TryParse(row["Offduty"].ToString(), out tOffDuty);
				var iDayCount = (int)row["DayCount"];
				tOffDuty = tOffDuty.Add(new TimeSpan(iDayCount, 0, 0, 0));
				TimeSpan timespanStartNightTime, timespanEndddNightTime;
				if (row["StartNT"] == DBNull.Value || TimeSpan.TryParse(row["StartNT"].ToString(), out timespanStartNightTime) == false) timespanStartNightTime = XL2._22h00;//ver 4.0.0.4	
				if (row["EndNT"] == DBNull.Value || TimeSpan.TryParse(row["EndNT"].ToString(), out timespanEndddNightTime) == false) timespanEndddNightTime = XL2._06h00;//ver 4.0.0.4	

				var tOnTimeIn = tsOnDuty.Subtract(new TimeSpan(0, (int)row["OnTimeIn"], 0));
				var tCutIn = tsOnDuty.Add(new TimeSpan(0, (int)row["CutIn"], 0));

				// phải add thêm 1 ngày daycount vì trong dữ liệu chỉ có chuỗi giờ thô : 05:45 không có ngày
				var tOnTimeOut = tOffDuty.Subtract(new TimeSpan(0, (int)row["OnTimeOut"], 0));
				var tCutOut = tOffDuty.Add(new TimeSpan(0, (int)row["CutOut"], 0));

				var tAfterOT = new TimeSpan(0, (int)row["AfterOT"], 0);
				var tLateGrace = new TimeSpan(0, (int)row["LateGrace"], 0);
				var tEarlyGrace = new TimeSpan(0, (int)row["EarlyGrace"], 0);

				var tOnLunch = XL2._0gio;
				var tOffLunch = XL2._0gio;
				if (row["OnLunch"] != DBNull.Value && row["OffLunch"] != DBNull.Value) {
					TimeSpan.TryParse(row["OnLunch"].ToString(), out tOnLunch);
					TimeSpan.TryParse(row["OffLunch"].ToString(), out tOffLunch);
				}

				var iShowPosition = (int)row["ShowPosition"];
				var tempWorkingTime = int.Parse(row["WorkingTime"].ToString());
				var kyhieucc = row["KyHieuCC"].ToString();
				var tachcadem = (row["IsSplited"] != DBNull.Value) && (bool)row["IsSplited"];
				var idCaTruoc = (row["ShiftID1"] != DBNull.Value) ? (int)row["ShiftID1"] : -1;
				var idCaSauuu = (row["ShiftID2"] != DBNull.Value) ? (int)row["ShiftID2"] : -1;
				var isextend = (row["IsExtended"] != DBNull.Value) && (bool)row["IsExtended"];
				var tempShift = new cCa {
					ID = iShiftID,
					Code = sShiftCode,
					DayCount = iDayCount,
					QuaDem = (iDayCount == 1),
					Duty = new TS { Onn = tsOnDuty, Off = tOffDuty },
					NhanDienVao = new TS { Onn = tOnTimeIn, Off = tCutIn },
					NhanDienRaa = new TS { Onn = tOnTimeOut, Off = tCutOut },
					AfterOTMin = tAfterOT,
					LateeMin = tLateGrace,
					EarlyMin = tEarlyGrace,
					Workingday = (Single)row["Workingday"],
					WorkingTimeTS = new TimeSpan(0, tempWorkingTime, 0),
					ShowPosition = iShowPosition,
					chophepTreTS = tsOnDuty + (tLateGrace),
					chophepSomTS = tOffDuty - tEarlyGrace,
					batdaulamthemTS = tOffDuty + tAfterOT,
					LunchMin = tOffLunch.Subtract(tOnLunch),
					TachCaDem = tachcadem,
					idCaTruoc = idCaTruoc,
					idCaSauuu = idCaSauuu,
					KyHieuCC = kyhieucc,
					MoTa = row["Description"].ToString(),
					IsExtended = isextend,
					Is_CaTuDo = false,
					StartNT = timespanStartNightTime,
					EndddNT = timespanEndddNightTime
				};
				#endregion

				tempList.Add(tempShift);

			}
			// ra khỏi vòng lặp,ca 3&1, ca 3&1A chưa cập nhật ca trước, ca sau--> cập nhật
			#region update lại ca tách qua đêm trong ds ca
			foreach (cCa ca in tempList.Where(item => item.TachCaDem)) {
				var catruoc = tempList.Find(item => item.ID == ca.idCaTruoc);
				var casauuu = tempList.Find(item => item.ID == ca.idCaSauuu);
				ca.catruoc = catruoc;
				ca.casauuu = casauuu;
			}
			#endregion

			// có được toàn bộ ds ca --> gán cho DSCa toàn cục, DSCaMR toàn cục
			XL.DSCa = tempList;
			XL.DSCaMoRong = tempList.Where(item => item.IsExtended).ToList();
			//DSCaMoRong chỉ bao gồm các ca ghép Ca1&2A, Ca1B&2, mỗi ca có caTrước caSau

			// gán lịch trình
			XL.DSLichTrinh.Clear();
			// lấy danh sách lịch trình, mỗi dòng là 1 ca thuộc lịch trình => để duyệt từng lịch trình thì phải lấy dicstint
			var tableLichTrinh = DAO5.LayDSLichTrinh();
			var arrRows_Distinct = (from DataRow row in tableLichTrinh.Rows
									select new { SchID = (int)(row["SchID"]), SchName = row["SchName"].ToString() })
									.Distinct();
			foreach (var row in arrRows_Distinct) {
				var arrRows1 = tableLichTrinh.Select("SchID=" + row.SchID, "");
				var lichtrinh = new cShiftSchedule {
					SchID = row.SchID,
					TenLichTrinh = row.SchName,
					DSCaThu = new List<List<cCa>>(7),
					DSCaMRThu = new List<List<cCa>>(7),
				};
				// mỗi lịch trình chứa 7T, mỗi T là 1 danh sách ca
				for (int i = 0; i < 7; i++) {
					lichtrinh.DSCaThu.Add(new List<cCa>());
					lichtrinh.DSCaMRThu.Add(new List<cCa>());
				}
				foreach (var row1 in arrRows1) {
					for (int i = 0; i < 7; i++) {
						if (row1["T" + (i + 1)] == DBNull.Value)
							continue;
						var shiftid = (int)row1["T" + (i + 1)];
						var ca = XL.DSCa.Find(o => o.ID == shiftid);
						if (ca != null)
							lichtrinh.DSCaThu[i].Add(ca);
					}
				}
				for (int i = 0; i < 7; i++) {
					List<cCa> dscathu = lichtrinh.DSCaThu[i];
					foreach (var ca in from ca in XL.DSCaMoRong
									   let catruoc = ca.catruoc
									   let casauuu = ca.casauuu
									   where dscathu.Exists(item => item.ID == catruoc.ID)
											&& dscathu.Exists(item => item.ID == casauuu.ID)
									   select ca) {
						lichtrinh.DSCaMRThu[i].Add(ca);
					}
				}
				var ca3 = lichtrinh.DSCaThu[0].FirstOrDefault(o => o.QuaDem && Math.Abs(o.Workingday - 1f) < 0.01f);//ver 4.0.0.4 //xác định ca 3 có giờ qua đêm 21h45 hay 22 giờ để gán
				if (ca3 == null) lichtrinh.TGLamDemTheoQuyDinh = TGLamDemTheoQuyDinh._22h00;//ver 4.0.0.4
				else if (ca3.StartNT == XL2._21h45) lichtrinh.TGLamDemTheoQuyDinh = TGLamDemTheoQuyDinh._21h45;//ver 4.0.0.4
				else lichtrinh.TGLamDemTheoQuyDinh = TGLamDemTheoQuyDinh._22h00;//ver 4.0.0.4
				XL.DSLichTrinh.Add(lichtrinh);
			}


			if (XL.DSLichTrinh.Count == 0)
				throw new NullReferenceException("Không có ds lịch trình");

		}

		public static bool KiemtraDulieuCapnhatTuServer(DateTime now) {
			//logic lấy thời gian MAX nguồn từ máy chấm công, nếu ko có dữ liệu trả về MinValue
			// trong vòng 7 tiếng trước ko có chấm công nghĩa là chưa lấy dữ liệu từ máy chấm công về
			const string query = @"	SELECT	MAX(CheckInOut.TimeStr) FROM CheckInOut WHERE OriginType != N'PC' and MachineNo != 21 and MachineNo != 22";
			var kq = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			var lastCheckTime = DateTime.MinValue;
			if (kq.Rows.Count != 0)
				if (kq.Rows[0][0] != DBNull.Value)
					lastCheckTime = (DateTime)kq.Rows[0][0];

			return (lastCheckTime != DateTime.MinValue && now - lastCheckTime <= XL2._07h00);
		}

		public static bool KiemtraDocFileKetnoiDL(string FileName, ref string connectionString) {
			var kq = true;
			// Open the file into a StreamReader
			try {
				StreamReader file = File.OpenText(FileName);
				string s = file.ReadToEnd();
				connectionString = MyUtility.giaima(s);

				file.Close();
			} catch (Exception ex) {
				kq = false;
				if (ex is FileNotFoundException || ex is DirectoryNotFoundException)
					MessageBox.Show(string.Format(Resources.Text_KoTimThayFileX, "kết nối CSDL"), Resources.Caption_Loi);
				else if (ex is NotSupportedException || ex is PathTooLongException)
					MessageBox.Show(string.Format(Resources.Text_UnsupportedFile_PathTooLong, "kết nối CSDL"), Resources.Caption_Loi);
				else if (ex is UnauthorizedAccessException)
					MessageBox.Show(string.Format(Resources.Text_KoCoQuyenTruyCapFileX, "kết nối CSDL"), Resources.Caption_Loi);
				else {
					lg.Error(string.Format("[{0}]_[{1}]\n", "XL", System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
					MessageBox.Show(Resources.Text_KoTheKetNoiCSDL + ex.Message, Resources.Caption_Loi);
				}
			}
			return kq;
		}
		public static bool CheckLogIn(string tempUsername, string tempPassword, string passroot,
			ref string tmpConnStr, ref int loaiTK, ref int currUserID, ref string currUserAccount) {
			var kqDocFile = KiemtraDocFileKetnoiDL(Settings.Default.ConnectionStringPath, ref tmpConnStr);
			var kq = false;

			if (!kqDocFile) // ko đọc được file thì trả về false
			{
				return false;
			}

			// đọc được file thành công, kiểm tra kết nối csdl
			if (SqlDataAccessHelper.TestConnection(tmpConnStr) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return false;
			}

			// kết nối csdl thành công, kiểm tra loại tài khoản
			SqlDataAccessHelper.ConnectionString = tmpConnStr;
			if (tempUsername == "root" && tempPassword == passroot) { //log in bằng tài khoản root
				currUserID = int.MaxValue;
				currUserAccount = tempUsername;
				loaiTK = 1;
				kq = true;
			}
			else {// kiểm tra login bằng tài khoản thường
				string passEncrypt = MyUtility.Mahoa(tempPassword);
				DataTable dt = DAO5.LogIn(tempUsername, passEncrypt);
				if (dt.Rows.Count != 0) { // tài khoản thường -> 
					currUserID = (int)dt.Rows[0]["UserID"];
					currUserAccount = dt.Rows[0]["UserAccount"].ToString();
					loaiTK = 0;
					kq = true;
				}
				else {
					MessageBox.Show(Resources.Text_Acc_Pass_incorrect, Resources.Caption_ThongBao, MessageBoxButtons.OK);
					kq = false;
				}
			}
			return kq;
		}


		public static void CheckTinhPC50_UpdORInsNew_Sort(cUserInfo nhanvien, DateTime ngay, bool giatri) {
			var n1 = DAO5.CheckTinhPC50(nhanvien.MaCC, ngay, giatri);
			var index = nhanvien.DSXNPhuCap50.FindIndex(o => o.Ngay == ngay);
			if (index < 0) { // chưa có --> tạo mới
				var ngayXN_PCTC = new structPCTC { Ngay = ngay, TinhPC50 = giatri };
				nhanvien.DSXNPhuCap50.Add(ngayXN_PCTC);
				nhanvien.DSXNPhuCap50.Sort(new cTemp1Comparer());
			}
			else { // đã có --> cập nhật
				nhanvien.DSXNPhuCap50[index] = new structPCTC { Ngay = ngay, TinhPC50 = giatri };
			}
		}


		public static void TinhPCDB(cUserInfo nhanvien, cNgayCong ngayCong, DateTime ngay, int loai, int pcBanNgay, int pcBanDem, string noidungLog) {
			var n1 = DAO5.UpdIns_TinhPCDB(nhanvien.MaCC, ngay, loai, pcBanNgay, pcBanDem, noidungLog);
			var index = nhanvien.DSXNPhuCapDB.FindIndex(o => o.Ngay == ngay);

			if (index < 0) { // chưa có --> tạo mới trong DSXNPhuCapDB
				var structpc = new structPCDB { LoaiPC = loai, PCNgay = pcBanNgay, PCDem = pcBanDem, Ngay = ngay, Duyet = true };
				nhanvien.DSXNPhuCapDB.Add(structpc);
				nhanvien.DSXNPhuCapDB.Sort(new cTempComparer());
			}
			else { // đã có --> update trong DSXNPhuCapDB
				nhanvien.DSXNPhuCapDB[index] = new structPCDB { LoaiPC = loai, PCNgay = pcBanNgay, PCDem = pcBanDem, Ngay = ngay, Duyet = true };
			}
			// sau khi thực hiện trong DSXNPhuCapDB xong thì tính phụ cấp cho ngày ngày
			TinhPCDB_CuaNgay(ngayCong, loai, pcBanNgay, pcBanDem);

		}




		public static void KhoiTaoDSPhongBan(List<cPhongBan> phongBans, int currentUserID) {
			// khởi tạo chỉ các phòng ban userID này được phép thao tác
			phongBans.Clear();
			DataTable table = DAO5.LayDSPhong(currentUserID);
			phongBans.AddRange(from DataRow row in table.Rows
							   select new cPhongBan {
								   ID = (int)row["ID"],
								   Ten = row["Description"].ToString(),
								   ViTri = (row["ViTri"] != DBNull.Value) ? (int)row["ViTri"] : int.MaxValue, // các phòng chưa được set thứ tự thì nằm bên dưới
								   idParent = (int)row["RelationID"],
								   //info ở đây sẽ ko xảy ra TH = null vì từ csdl sẽ ko null nhưng vẫn làm đúng, chỉ group phòng ban theo nv là bị null
								   ChoPhep = true
							   });
			foreach (cPhongBan phong in phongBans) {
				cPhongBan parent = phongBans.FirstOrDefault(item => item.ID == phong.idParent); // info idparent = 0 nghĩa là node gốc
				phong.parent = parent;
			}
		}

		public static void KhoiTaoDSPhongBan(List<cPhongBan> phongBans) {
			phongBans.Clear();
			DataTable table = DAO5.LayDSTatCaPhongBan();
			phongBans.AddRange(from DataRow row in table.Rows
							   select new cPhongBan {
								   ID = (int)row["ID"],
								   Ten = row["Description"].ToString(),
								   ViTri = (row["ViTri"] != DBNull.Value) ? (int)row["ViTri"] : int.MaxValue,// các phòng chưa được set thứ tự thì nằm bên dưới
								   idParent = (int)row["RelationID"],
								   //info ở đây sẽ ko xảy ra TH = null vì từ csdl sẽ ko null nhưng vẫn làm đúng, chỉ group phòng ban theo nv là bị null
								   ChoPhep = false
							   });
			foreach (cPhongBan phong in phongBans) {
				cPhongBan parent = phongBans.FirstOrDefault(item => item.ID == phong.idParent);// info idparent = 0 nghĩa là node gốc
				phong.parent = parent;
			}
		}

		public static DataTable LayDSNV(bool? UserEnabled = null, int[] arrIDPhongBan = null) {
			var tableNV = DAO5.LayDSNV(arrIDPhongBan);
			var tableKQ = tableNV.Clone();
			foreach (DataRow dataRow in tableNV.Rows) {
				if (UserEnabled != null) // nếu có truyền trạng thái thì lấy theo trạng thái
				{
					if (dataRow["UserEnabled"] == DBNull.Value || (bool)dataRow["UserEnabled"] != UserEnabled)
						continue;
				}
				tableKQ.ImportRow(dataRow); // ko truyền trạng thái thì lấy hết
			}

			return tableKQ;
		}
		public static DataTable LayDSNVChuaSX(bool? UserEnabled) {
			var tableNV = DAO5.LayDSNV();
			var tableKQ = tableNV.Clone();
			foreach (DataRow dataRow in tableNV.Rows) {
				if (UserEnabled != null) // nếu có truyền trạng thái thì
				{
					if (dataRow["UserEnabled"] == DBNull.Value || (bool)dataRow["UserEnabled"] != UserEnabled)
						continue;
				}
				if (dataRow["MaPhong"] == DBNull.Value || (int)dataRow["MaPhong"] == 0) // chỉ lấy các nhân viên mới, ko lấy các nv khác
				{
					tableKQ.ImportRow(dataRow);
				}
			}

			return tableKQ;
		}


		public static void XacNhanCa(cUserInfo nv, cCheckInOut CIO, cCa currShift,
			bool bDuyetCPTre, bool bDuyetCPSom, int soPhutLamThem, bool choPhepTinhPc50, string lydo, string ghichu,
			bool bVaoTreLaCV, bool bRaaSomLaCV, TimeSpan startNT, TimeSpan endddNT) {//ver 4.0.0.4	

			//if (CIO.Vao.Time <= XL2.NgayCuoiThangKetCong) return;

			if (currShift.TachCaDem) {
				if (CIO.DaXN) {
					XacNhan_CIO_V_CoTachCa(nv, CIO, currShift, bDuyetCPTre, bDuyetCPSom, soPhutLamThem, choPhepTinhPc50, lydo, ghichu,
						bVaoTreLaCV, bRaaSomLaCV, startNT, endddNT);//ver 4.0.0.4	
				}
				else {
					XacNhan_CIO_A_CoTachCa(nv, CIO, currShift, bDuyetCPTre, bDuyetCPSom, soPhutLamThem, choPhepTinhPc50, lydo, ghichu,
						bVaoTreLaCV, bRaaSomLaCV, startNT, endddNT);//ver 4.0.0.4	
				}
			}

			else {
				if (CIO.DaXN) {
					XacNhan_CIO_V(nv, CIO, currShift, bDuyetCPTre, bDuyetCPSom, soPhutLamThem, choPhepTinhPc50, lydo, ghichu,
						bVaoTreLaCV, bRaaSomLaCV, startNT, endddNT);//ver 4.0.0.4	
				}
				else {
					XacNhan_CIO_A(nv, CIO, currShift, bDuyetCPTre, bDuyetCPSom, soPhutLamThem, choPhepTinhPc50, lydo, ghichu,
						bVaoTreLaCV, bRaaSomLaCV, startNT, endddNT);//ver 4.0.0.4	
				}

			}
		}

		public static void XacNhan_CIO_A_CoTachCa(cUserInfo nv, cCheckInOut CIO, cCa currShift, bool bDuyetCPTre, bool bDuyetCPSom, int soPhutLamThem, bool choPhepTinhPc50, string lydo, string ghichu,
			bool bVaoTreLaCV, bool bRaaSomLaCV, TimeSpan startNT, TimeSpan endddNT) {//ver 4.0.0.4	
		}
		public static void XacNhan_CIO_V_CoTachCa(cUserInfo nv, cCheckInOut CIO, cCa currShift, bool bDuyetCPTre, bool bDuyetCPSom, int soPhutLamThem, bool choPhepTinhPc50, string lydo, string ghichu,
			bool bVaoTreLaCV, bool bRaaSomLaCV, TimeSpan startNT, TimeSpan endddNT) {//ver 4.0.0.4	
		}

		public static void XacNhan_CIO_V(cUserInfo nv, cCheckInOut CIO, cCa currShift, bool bDuyetCPTre, bool bDuyetCPSom, int soPhutLamThem, bool choPhepTinhPc50, string lydo, string ghichu, 
			bool bVaoTreLaCV, bool bRaaSomLaCV, TimeSpan startNT, TimeSpan endddNT) {//ver 4.0.0.4	

		}

		public static void XacNhan_CIO_A(cUserInfo nv, cCheckInOut CIO, cCa currShift, bool bDuyetCPTre, bool bDuyetCPSom, int soPhutLamThem, bool choPhepTinhPc50, string lydo, string ghichu,
			bool bVaoTreLaCV, bool bRaaSomLaCV, TimeSpan startNT, TimeSpan endddNT) {//ver 4.0.0.4	

		}
		public static void XacNhan_CIO_A(int MaCC, DateTime timevao, DateTime timeraa, cCa currShift, bool bDuyetCPTre, bool bDuyetCPSom, int soPhutLamThem, bool choPhepTinhPc50, string lydo, string ghichu,
			bool bVaoTreLaCV, bool bRaaSomLaCV) {//ver 4.0.0.4	
			if (timeraa - timevao <= TimeSpan.Zero || timeraa - timevao > XL2._24h00) {
				return; // ko đủ điều kiện xác nhận
			}
			int outputIDXN;

			// 2. xác nhận ca dưới csdl
			DAO5.XacNhanCa(MaCC, timevao, 21, "PC", timeraa, 22, "PC",
				currShift.ID, currShift.Code, bDuyetCPTre, bDuyetCPSom, soPhutLamThem, lydo, ghichu, out outputIDXN,
				bVaoTreLaCV, bRaaSomLaCV);//ver 4.0.0.4	
			DAO5.CheckTinhPC50(MaCC, timevao.Date, choPhepTinhPc50);
		}

		public static void HuyBo_TinhPCDB(cUserInfo nhanvien, cNgayCong ngayCong, List<structPCDB> dsxnPhuCapDb) {
			var cTemp = dsxnPhuCapDb.FindIndex(item => item.Ngay == ngayCong.Ngay);
			if (cTemp < 0) { }
			else {
				var n1 = DAO5.DeleteTinhPCDB(nhanvien.MaCC, ngayCong.Ngay); //info đã log
				dsxnPhuCapDb.Remove(dsxnPhuCapDb[cTemp]);
				ngayCong.TinhPCDB = false;
				ngayCong.PhuCaps._100_LVNN_Ngay = 0f;
				ngayCong.PhuCaps._150_LVNN_Dem = 0f;
				ngayCong.PhuCaps._200_LeTet_Ngay = 0f;
				ngayCong.PhuCaps._250_LeTet_Dem = 0f;
				ngayCong.PhuCaps._Cus = 0f;
				ngayCong.PhuCaps._TongPC = 0f;
				ngayCong.TG.Tinh200 = TimeSpan.Zero;
				ngayCong.TG.Tinh260 = TimeSpan.Zero;
				ngayCong.TG.Tinh300 = TimeSpan.Zero;
				ngayCong.TG.Tinh390 = TimeSpan.Zero;
				ngayCong.TG.TinhPCCus = TimeSpan.Zero;
				TinhPCTC_CuaNgay(ngayCong, nhanvien.DSXNPhuCap50);
			}
		}



		public static DataTable LayDSThuchiThang(DateTime m_thang) {
			return DAO5.LayDSThuchiThang(m_thang);
		}

		/*
				public static void UpdInsDSNVKetluongThang(List<cUserInfo> mDsnv, DateTime ngaydauthang) {
					foreach (var nv in mDsnv) {
						int kq = DAO.UpdInsNVKetluongThang(ngaydauthang, nv.MaCC, nv.HeSo.LuongCB, nv.HeSo.LuongCV, nv.HeSo.BHCongThem_ChoGD_PGD, //tbd BHXH_YT_TN
							nv.PhongBan.ID, nv.PhongBan.Ten, nv.PhongBan.ViTri, nv.PhongBan.idParent, nv.IDChucVu, nv.ChucVu);
					}
				}

		*/

		public static bool Kiemtra(DateTime ngayBD, DateTime ngayKT) {
			ngayBD = new DateTime(ngayBD.Year, ngayBD.Month, 1);
			ngayKT = new DateTime(ngayKT.Year, ngayKT.Month, 1);
			if (ngayBD > ngayKT) MyUtility.Swap(ref ngayBD, ref ngayKT);
			var query = " select Thang from ThongSoKetLuongThang where Thang >= @ThangBD and Thang <= @ThangKT ";
			var table = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@ThangBD", "@ThangKT" }, new object[] { ngayBD, ngayKT });
			if (table.Rows.Count == 0) return false;
			return true;
		}

		public static bool HuyKetLuongThang(DateTime ngaydauthang) {
			var query = @" delete from ThongSoKetLuongThang where Thang=@Thang 
						   delete from KetLuongThang where Thang=@Thang ";
			var kq = SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@Thang" }, new object[] { ngaydauthang });
			DAO5.GhiNhatKyThaotac("Huỷ kết lương tháng", string.Format("Huỷ kết lương tháng [{0}]", ngaydauthang.ToString("MM/yyyy")));
			if (kq == 0) return false;
			return true;
		}

		public static float TinhCongChuanCuaThang(DateTime ngaydauthang) {
			return (DateTime.DaysInMonth(ngaydauthang.Year, ngaydauthang.Month) - XL.DemSoNgayNghiChunhat(ngaydauthang, true, false));
		}


		internal static List<int> LayPhanQuyen() {
			List<int> kq = new List<int>();
			DataTable dt = DAO5.PhanQuyenMenu(XL2.currUserID);
			for (int i = 0; i < dt.Rows.Count; i++) {
				bool enable = (bool)dt.Rows[i]["IsYes"];
				if (enable) kq.Add((int)dt.Rows[i]["MenuID"]);
			}
			return kq;
		}

		public static bool KiemTraDieuKienCongCVAuto_VuotNguong(float CongCV_Auto, int soNgayThu7, int soNgayChuNhat) {
			if (soNgayThu7 == 5 && soNgayChuNhat == 4) {
				if (CongCV_Auto > soNgayThu7) return true; // info trường hợp đặc biệt 5 ngày T7 nhưng chỉ 4 ngày CN thì tối đa 5 ngày CV, chứ ko phải 4 ngày CN
			}
			else {
				if (CongCV_Auto > soNgayChuNhat) return true;
			}
			return false;
		}
	}

}
