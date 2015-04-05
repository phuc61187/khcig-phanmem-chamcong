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
			var catruoc = currShift.catruoc;
			var casauuu = currShift.casauuu;
			var timevaoca3 = CIO.Vao.Time;
			var timeraaca3 = CIO.ThuocNgayCong.Add(catruoc.Duty.Off);
			var timevaoca1 = CIO.ThuocNgayCong.AddDays(1d).Add(casauuu.Duty.Onn).Add(XL2._01giay);
			var timeraaca1 = CIO.Raa.Time;
			int outputIDXNCa3, outputIDXNCa1;
			var checkOutCa3 = new cCheck { IsEdited = 1, MaCC = nv.MaCC, MachineNo = 22, Time = timeraaca3, Source = "PC", Type = "O", PhucHoi = new cPhucHoi { IDGioGoc = int.MinValue, Them = true, Xoaa = false } };
			var checkInnCa1 = new cCheck { IsEdited = 1, MaCC = nv.MaCC, MachineNo = 21, Time = timevaoca1, Source = "PC", Type = "I", PhucHoi = new cPhucHoi { IDGioGoc = int.MinValue, Them = true, Xoaa = false } };

			// 1. thêm giờ đệm vào csdl
			DAO5.ThemGioChoNV(nv.MaCC, checkOutCa3.Time, checkOutCa3.Type, checkOutCa3.MachineNo, "Hệ thống tự động thêm giờ đệm tách ca qua ngày", "Thực hiện tự động");
			DAO5.ThemGioChoNV(nv.MaCC, checkInnCa1.Time, checkInnCa1.Type, checkInnCa1.MachineNo, "Hệ thống tự động thêm giờ đệm tách ca qua ngày", "Thực hiện tự động");

			// 2. xác nhận ca dưới csdl
			DAO5.XacNhanCa(nv.MaCC, CIO.Vao.Time, CIO.Vao.MachineNo, CIO.Vao.Source,
				checkOutCa3.Time, checkOutCa3.MachineNo, checkOutCa3.Source,
				catruoc.ID, catruoc.Code, bDuyetCPTre, bDuyetCPSom, 0, lydo, ghichu, out outputIDXNCa3,// ca 3 ko có làm thêm OT
				bVaoTreLaCV, bRaaSomLaCV);//ver 4.0.0.4	
			DAO5.XacNhanCa(nv.MaCC, checkInnCa1.Time, checkInnCa1.MachineNo, checkInnCa1.Source,
				CIO.Raa.Time, CIO.Raa.MachineNo, CIO.Raa.Source,
				casauuu.ID, casauuu.Code, bDuyetCPTre, bDuyetCPSom, soPhutLamThem, lydo, ghichu, out outputIDXNCa1,
				bVaoTreLaCV, bRaaSomLaCV);//ver 4.0.0.4	

			// 3. bỏ Check In, Check Out khỏi DS_Check_A
			var checkInnCa3 = nv.DS_Check_A.Find(item => item.Time == timevaoca3 && item.MachineNo == CIO.Vao.MachineNo);
			var checkOutCa1 = nv.DS_Check_A.Find(item => item.Time == timeraaca1 && item.MachineNo == CIO.Raa.MachineNo);
			nv.DS_Check_A.Remove(checkInnCa3);
			nv.DS_Check_A.Remove(checkOutCa1);

			// 4.1 bỏ CIO_A khỏi DS_CIO_A;   4.2 bỏ CIO_A khỏi DSVaoRa;
			var CIO_A = nv.DS_CIO_A.Find(item => item.Vao == checkInnCa3 && item.Raa == checkOutCa1);
			nv.DS_CIO_A.Remove(CIO_A);
			nv.DSVaoRa.Remove(CIO_A);
			// 5 chuyển CIO_A thành CIO_V và xét ca CIO_V này
			checkInnCa3.ID = checkOutCa3.ID = outputIDXNCa3;
			var CIO_V_Ca3 = new cCheckInOut {
				/*ID = outputIDXNCa3,*/
				DaXN = true, Vao = checkInnCa3, Raa = checkOutCa3, TimeDaiDien = checkInnCa3.Time, //HaveINOUT mặc định =0
				ShiftID = catruoc.ID,
				ThuocNgayCong = ThuocNgayCong(checkInnCa3.Time),
				DuyetChoPhepVaoTre = bDuyetCPTre, DuyetChoPhepRaSom = bDuyetCPSom,
				VaoTreTinhCV = bVaoTreLaCV, RaaSomTinhCV = bRaaSomLaCV,//ver 4.0.0.4	
				TG = new ThoiGian { SoPhutLamThem5 = new TimeSpan(0, 0, 0) }, // ca 3 ko có OT
				TD = new ThoiDiem()
			};
			checkInnCa1.ID = checkOutCa1.ID = outputIDXNCa1;
			var CIO_V_Ca1 = new cCheckInOut {
				/*ID = outputIDXNCa1,*/
				DaXN = true, Vao = checkInnCa1, Raa = checkOutCa1, TimeDaiDien = checkInnCa1.Time, //HaveINOUT mặc định =0
				ShiftID = casauuu.ID,
				ThuocNgayCong = ThuocNgayCong(checkInnCa1.Time),
				DuyetChoPhepVaoTre = bDuyetCPTre, DuyetChoPhepRaSom = bDuyetCPSom,
				VaoTreTinhCV = bVaoTreLaCV, RaaSomTinhCV = bRaaSomLaCV,//ver 4.0.0.4	
				TG = new ThoiGian { SoPhutLamThem5 = new TimeSpan(0, soPhutLamThem, 0) }, // ca 1 có OT
				TD = new ThoiDiem()
			};
			XetCa_1_CIO_V(CIO_V_Ca3, nv.LichTrinhLV);
			XetCa_1_CIO_V(CIO_V_Ca1, nv.LichTrinhLV);

			// 6. bỏ CIO_A khỏi DS_CIO_A, DSVaoRa rồi nên phải add lại CIO_V vào DS_CIO_V, DSVaoRa;  sau đó nhớ sort lại
			nv.DS_CIO_V.Add(CIO_V_Ca3);
			nv.DS_CIO_V.Add(CIO_V_Ca1);
			nv.DS_CIO_V.Sort(new cCheckInOutComparer());
			nv.DSVaoRa.Add(CIO_V_Ca3);
			nv.DSVaoRa.Add(CIO_V_Ca1);
			nv.DSVaoRa.Sort(new cCheckInOutComparer());

			// 7. check tính PC 50%, sau đó sort lại
			XL.CheckTinhPC50_UpdORInsNew_Sort(nv, CIO_V_Ca3.ThuocNgayCong, choPhepTinhPc50);
			XL.CheckTinhPC50_UpdORInsNew_Sort(nv, CIO_V_Ca1.ThuocNgayCong, choPhepTinhPc50);

			// 8. xác định ngày mà loại bỏ CIO_A để xoá khỏi ngayCong.DSVaoRa, tính lại công, pctc, pcdb của ngày này luôn
			// do trong hàm tính công sẽ reset lại tất cả thông số trong đó có tính PCTC nên phải tính lại PCTC
			var ngayCong_cua_CIO_A = nv.DSNgayCong.Find(item => item.Ngay == CIO_A.ThuocNgayCong);
			ngayCong_cua_CIO_A.DSVaoRa.Remove(CIO_A);
			TinhCong_HangNgay(ngayCong_cua_CIO_A, startNT, endddNT);
			TinhPCTC_CuaNgay(ngayCong_cua_CIO_A, nv.DSXNPhuCap50);
			TinhPCDB_CuaNgay(ngayCong_cua_CIO_A, nv.DSXNPhuCapDB);

			// 9. xác định ngày của ca 3 để thêm ca 3 vào ngayCong.DSVaoRa, tính lại công, pctc, pcdb
			var ngayCong_cua_CIO_V_Ca3 = nv.DSNgayCong.Find(item => item.Ngay == CIO_V_Ca3.ThuocNgayCong);
			ngayCong_cua_CIO_V_Ca3.DSVaoRa.Add(CIO_V_Ca3);
			ngayCong_cua_CIO_V_Ca3.DSVaoRa.Sort(new cCheckInOutComparer());
			TinhCong_HangNgay(ngayCong_cua_CIO_V_Ca3, startNT, endddNT);
			TinhPCTC_CuaNgay(ngayCong_cua_CIO_V_Ca3, nv.DSXNPhuCap50);
			TinhPCDB_CuaNgay(ngayCong_cua_CIO_V_Ca3, nv.DSXNPhuCapDB);

			// 9. xác định ngày của ca 1 để thêm ca 1 vào ngayCong.DSVaoRa, tính lại công, pctc, pcdb
			var ngayCong_cua_CIO_V_Ca1 = ngayCong_cua_CIO_V_Ca3.next;
			ngayCong_cua_CIO_V_Ca1.DSVaoRa.Add(CIO_V_Ca1);
			ngayCong_cua_CIO_V_Ca1.DSVaoRa.Sort(new cCheckInOutComparer());
			TinhCong_HangNgay(ngayCong_cua_CIO_V_Ca1, startNT, endddNT);
			TinhPCTC_CuaNgay(ngayCong_cua_CIO_V_Ca1, nv.DSXNPhuCap50);
			TinhPCDB_CuaNgay(ngayCong_cua_CIO_V_Ca1, nv.DSXNPhuCapDB);
		}
		public static void XacNhan_CIO_V_CoTachCa(cUserInfo nv, cCheckInOut CIO, cCa currShift, bool bDuyetCPTre, bool bDuyetCPSom, int soPhutLamThem, bool choPhepTinhPc50, string lydo, string ghichu,
			bool bVaoTreLaCV, bool bRaaSomLaCV, TimeSpan startNT, TimeSpan endddNT) {//ver 4.0.0.4	
			var catruoc = currShift.catruoc;
			var casauuu = currShift.casauuu;
			var timevaoca3 = CIO.Vao.Time;
			var timeraaca3 = CIO.ThuocNgayCong.Add(catruoc.Duty.Off);
			var timevaoca1 = CIO.ThuocNgayCong.AddDays(1d).Add(casauuu.Duty.Onn).Add(XL2._01giay);
			var timeraaca1 = CIO.Raa.Time;
			int outputIDXNCa3, outputIDXNCa1;
			var checkOutCa3 = new cCheck { IsEdited = 1, MaCC = nv.MaCC, MachineNo = 22, Time = timeraaca3, Source = "PC", Type = "O", PhucHoi = new cPhucHoi { IDGioGoc = int.MinValue, Them = true, Xoaa = false } };
			var checkInnCa1 = new cCheck { IsEdited = 1, MaCC = nv.MaCC, MachineNo = 21, Time = timevaoca1, Source = "PC", Type = "I", PhucHoi = new cPhucHoi { IDGioGoc = int.MinValue, Them = true, Xoaa = false } };


			// 1. thêm giờ đệm vào csdl
			DAO5.ThemGioChoNV(nv.MaCC, checkOutCa3.Time, checkOutCa3.Type, checkOutCa3.MachineNo, "Hệ thống tự động thêm giờ đệm tách ca qua ngày", "Thực hiện tự động");
			DAO5.ThemGioChoNV(nv.MaCC, checkInnCa1.Time, checkInnCa1.Type, checkInnCa1.MachineNo, "Hệ thống tự động thêm giờ đệm tách ca qua ngày", "Thực hiện tự động");

			// 2. xác nhận ca dưới csdl
			DAO5.XacNhanCa(nv.MaCC, CIO.Vao.Time, CIO.Vao.MachineNo, CIO.Vao.Source,
				checkOutCa3.Time, checkOutCa3.MachineNo, checkOutCa3.Source,
				catruoc.ID, catruoc.Code, bDuyetCPTre, bDuyetCPSom, 0, lydo, ghichu, out outputIDXNCa3,// ca 3 ko có làm thêm OT
				bVaoTreLaCV, bRaaSomLaCV);//ver 4.0.0.4	
			DAO5.XacNhanCa(nv.MaCC, checkInnCa1.Time, checkInnCa1.MachineNo, checkInnCa1.Source,
				CIO.Raa.Time, CIO.Raa.MachineNo, CIO.Raa.Source,
				casauuu.ID, casauuu.Code, bDuyetCPTre, bDuyetCPSom, soPhutLamThem, lydo, ghichu, out outputIDXNCa1,
				bVaoTreLaCV, bRaaSomLaCV);//ver 4.0.0.4	
			// 3 CIO_V nên ko có DSCheck_V để loại bỏ
			// 4.1 bỏ CIO_V khỏi DS_CIO_V;   4.2 bỏ CIO_V khỏi DSVaoRa;
			var CIO_V_OLD = nv.DSVaoRa.Find(item => item.DaXN && item.Vao == CIO.Vao && item.Raa == CIO.Raa);
			nv.DS_CIO_V.Remove(CIO_V_OLD);
			nv.DSVaoRa.Remove(CIO_V_OLD);

			// 5.1 Tạo mới CIO_V_Ca3 lấy In là CIO_V.Vào cũ   5.2 Tạo mới CIO_V_Ca1 lấy Out là CIO_V.Raa cũ   5.3 xét ca 2 CIO_V này
			CIO_V_OLD.Vao.ID = checkOutCa3.ID = outputIDXNCa3;
			var CIO_V_NEW_Ca3 = new cCheckInOut {
				/*ID = outputIDXNCa3,*/
				DaXN = true, Vao = CIO_V_OLD.Vao, Raa = checkOutCa3, TimeDaiDien = CIO_V_OLD.Vao.Time, //HaveINOUT mặc định =0
				ShiftID = catruoc.ID,
				ThuocNgayCong = ThuocNgayCong(CIO.Vao.Time),
				DuyetChoPhepVaoTre = bDuyetCPTre, DuyetChoPhepRaSom = bDuyetCPSom,
				VaoTreTinhCV = bVaoTreLaCV, RaaSomTinhCV = bRaaSomLaCV,//ver 4.0.0.4	
				TG = new ThoiGian { SoPhutLamThem5 = new TimeSpan(0, 0, 0) }, // ca 3 ko có OT
				TD = new ThoiDiem()
			};
			checkInnCa1.ID = CIO.Raa.ID = outputIDXNCa1;
			var CIO_V_NEW_Ca1 = new cCheckInOut {
				/*ID = outputIDXNCa1,*/
				DaXN = true, Vao = checkInnCa1, Raa = CIO.Raa, TimeDaiDien = checkInnCa1.Time, //HaveINOUT mặc định =0
				ShiftID = casauuu.ID,
				ThuocNgayCong = CIO_V_NEW_Ca3.ThuocNgayCong.AddDays(1d),
				DuyetChoPhepVaoTre = bDuyetCPTre, DuyetChoPhepRaSom = bDuyetCPSom,
				VaoTreTinhCV = bVaoTreLaCV, RaaSomTinhCV = bRaaSomLaCV,//ver 4.0.0.4	
				TG = new ThoiGian { SoPhutLamThem5 = new TimeSpan(0, soPhutLamThem, 0) }, // ca 1 có OT
				TD = new ThoiDiem()
			};
			XetCa_1_CIO_V(CIO_V_NEW_Ca3, nv.LichTrinhLV);
			XetCa_1_CIO_V(CIO_V_NEW_Ca1, nv.LichTrinhLV);

			// 6. bỏ CIO_V khỏi DS_CIO_V, DSVaoRa rồi nên phải add lại CIO_V vào DS_CIO_V, DSVaoRa;  sau đó nhớ sort lại
			nv.DS_CIO_V.Add(CIO_V_NEW_Ca3);
			nv.DS_CIO_V.Add(CIO_V_NEW_Ca1);
			nv.DS_CIO_V.Sort(new cCheckInOutComparer());
			nv.DSVaoRa.Add(CIO_V_NEW_Ca3);
			nv.DSVaoRa.Add(CIO_V_NEW_Ca1);
			nv.DSVaoRa.Sort(new cCheckInOutComparer());

			// 7. check tính PC 50%, sau đó sort lại
			XL.CheckTinhPC50_UpdORInsNew_Sort(nv, CIO_V_NEW_Ca3.ThuocNgayCong, choPhepTinhPc50);
			XL.CheckTinhPC50_UpdORInsNew_Sort(nv, CIO_V_NEW_Ca1.ThuocNgayCong, choPhepTinhPc50);

			// 8. xác định ngày mà loại bỏ CIO_V để xoá khỏi ngayCong.DSVaoRa, tính lại công, pctc, pcdb của ngày này luôn
			// do trong hàm tính công sẽ reset lại tất cả thông số trong đó có tính PCTC nên phải tính lại PCTC
			var ngayCong_cua_CIO_V_OLD = nv.DSNgayCong.Find(item => item.Ngay == CIO_V_OLD.ThuocNgayCong);
			ngayCong_cua_CIO_V_OLD.DSVaoRa.Remove(CIO_V_OLD);
			TinhCong_HangNgay(ngayCong_cua_CIO_V_OLD, startNT, endddNT);
			TinhPCTC_CuaNgay(ngayCong_cua_CIO_V_OLD, nv.DSXNPhuCap50);
			TinhPCDB_CuaNgay(ngayCong_cua_CIO_V_OLD, nv.DSXNPhuCapDB);

			// 9. xác định ngày của ca 3 để thêm ca 3 vào ngayCong.DSVaoRa, tính lại công, pctc, pcdb
			var ngayCong_cua_CIO_V_Ca3 = nv.DSNgayCong.Find(item => item.Ngay == CIO_V_NEW_Ca3.ThuocNgayCong);
			ngayCong_cua_CIO_V_Ca3.DSVaoRa.Add(CIO_V_NEW_Ca3);
			ngayCong_cua_CIO_V_Ca3.DSVaoRa.Sort(new cCheckInOutComparer());
			TinhCong_HangNgay(ngayCong_cua_CIO_V_Ca3, startNT, endddNT);
			TinhPCTC_CuaNgay(ngayCong_cua_CIO_V_Ca3, nv.DSXNPhuCap50);
			TinhPCDB_CuaNgay(ngayCong_cua_CIO_V_Ca3, nv.DSXNPhuCapDB);

			// 9. xác định ngày của ca 1 để thêm ca 1 vào ngayCong.DSVaoRa, tính lại công, pctc, pcdb
			var ngayCong_cua_CIO_V_Ca1 = ngayCong_cua_CIO_V_Ca3.next;
			ngayCong_cua_CIO_V_Ca1.DSVaoRa.Add(CIO_V_NEW_Ca1);
			ngayCong_cua_CIO_V_Ca1.DSVaoRa.Sort(new cCheckInOutComparer());
			TinhCong_HangNgay(ngayCong_cua_CIO_V_Ca1, startNT, endddNT);
			TinhPCTC_CuaNgay(ngayCong_cua_CIO_V_Ca1, nv.DSXNPhuCap50);
			TinhPCDB_CuaNgay(ngayCong_cua_CIO_V_Ca1, nv.DSXNPhuCapDB);

		}

		public static void XacNhan_CIO_V(cUserInfo nv, cCheckInOut CIO, cCa currShift, bool bDuyetCPTre, bool bDuyetCPSom, int soPhutLamThem, bool choPhepTinhPc50, string lydo, string ghichu, 
			bool bVaoTreLaCV, bool bRaaSomLaCV, TimeSpan startNT, TimeSpan endddNT) {//ver 4.0.0.4	
			var timevao = CIO.Vao.Time;
			var timeraa = CIO.Raa.Time;
			int outputIDXN;
			DAO5.XacNhanCa(nv.MaCC, timevao, CIO.Vao.MachineNo, CIO.Vao.Source, timeraa, CIO.Raa.MachineNo, CIO.Raa.Source,
				currShift.ID, currShift.Code, bDuyetCPTre, bDuyetCPSom, soPhutLamThem, lydo, ghichu, out outputIDXN , 
				bVaoTreLaCV, bRaaSomLaCV);

			// update lại ID cho CIO_V này và cập nhật lại các thông tin. sau đó XÉT CA lại
			/*CIO.ID = outputIDXN;*/
			//CIO.DaXN = true;CIO.Vao = CIO.Vao; CIO.Raa = checkOut, TimeDaiDien = checkInn.Time, //HaveINOUT mặc định =0
			CIO.ShiftID = currShift.ID;
			CIO.DuyetChoPhepVaoTre = bDuyetCPTre;
			CIO.DuyetChoPhepRaSom = bDuyetCPSom;
			CIO.VaoTreTinhCV = bVaoTreLaCV;//ver 4.0.0.4	
			CIO.RaaSomTinhCV = bRaaSomLaCV;//ver 4.0.0.4	
			CIO.TG = new ThoiGian { SoPhutLamThem5 = new TimeSpan(0, soPhutLamThem, 0) };
			CIO.TD = new ThoiDiem();// phải new thời điểm vì còn các thời điểm Raa_tinhOT sẽ bị thay đổi do số phút OT thay đổi
			XetCa_1_CIO_V(CIO, nv.LichTrinhLV);

			// 7. check tính PC 50%, sau đó sort lại
			XL.CheckTinhPC50_UpdORInsNew_Sort(nv, CIO.ThuocNgayCong, choPhepTinhPc50);

			// 8. xác định ngày mà update lại CIO_V để  tính lại công, pctc, pcdb của ngày này 
			// do trong hàm tính công sẽ reset lại tất cả thông số trong đó có tính PCTC nên phải tính lại PCTC
			var ngayCong_cua_CIO_V = nv.DSNgayCong.Find(item => item.Ngay == CIO.ThuocNgayCong);
			//ngayCong_cua_CIO_V.DSVaoRa.Add(CIO); ko cần add lại, chỉ tính lại
			TinhCong_HangNgay(ngayCong_cua_CIO_V, startNT, endddNT);
			TinhPCTC_CuaNgay(ngayCong_cua_CIO_V, nv.DSXNPhuCap50);
			TinhPCDB_CuaNgay(ngayCong_cua_CIO_V, nv.DSXNPhuCapDB);

		}

		public static void XacNhan_CIO_A(cUserInfo nv, cCheckInOut CIO, cCa currShift, bool bDuyetCPTre, bool bDuyetCPSom, int soPhutLamThem, bool choPhepTinhPc50, string lydo, string ghichu,
			bool bVaoTreLaCV, bool bRaaSomLaCV, TimeSpan startNT, TimeSpan endddNT) {//ver 4.0.0.4	
			var timevao = CIO.Vao.Time;
			var timeraa = CIO.Raa.Time;
			if (timeraa - timevao <= TimeSpan.Zero || timeraa - timevao > XL2._24h00) {
				return; // ko đủ điều kiện xác nhận
			}
			int outputIDXN;

			// 2. xác nhận ca dưới csdl
			DAO5.XacNhanCa(nv.MaCC, timevao, CIO.Vao.MachineNo, CIO.Vao.Source, timeraa, CIO.Raa.MachineNo, CIO.Raa.Source,
				currShift.ID, currShift.Code, bDuyetCPTre, bDuyetCPSom, soPhutLamThem, lydo, ghichu, out outputIDXN,
				bVaoTreLaCV, bRaaSomLaCV);//ver 4.0.0.4	

			// 3. bỏ Check In, Check Out khỏi DS_Check_A
			var checkInn = nv.DS_Check_A.Find(item => item.Time == timevao && item.MachineNo == CIO.Vao.MachineNo);
			var checkOut = nv.DS_Check_A.Find(item => item.Time == timeraa && item.MachineNo == CIO.Raa.MachineNo);
			nv.DS_Check_A.Remove(checkInn);
			nv.DS_Check_A.Remove(checkOut);

			// 4.1 bỏ CIO_A khỏi DS_CIO_A;   4.2 bỏ CIO_A khỏi DSVaoRa;
			var CIO_A = nv.DS_CIO_A.Find(item => item.Vao == checkInn && item.Raa == checkOut);
			nv.DS_CIO_A.Remove(CIO_A);
			nv.DSVaoRa.Remove(CIO_A);

			// 5 chuyển CIO_A thành CIO_V và xét ca CIO_V này
			checkInn.ID = checkOut.ID = outputIDXN;
			var CIO_V = new cCheckInOut {
				/*ID = outputIDXN,*/
				DaXN = true, Vao = checkInn, Raa = checkOut, TimeDaiDien = checkInn.Time, //HaveINOUT mặc định =0
				ShiftID = currShift.ID,
				ThuocNgayCong = ThuocNgayCong(checkInn.Time),
				DuyetChoPhepVaoTre = bDuyetCPTre, DuyetChoPhepRaSom = bDuyetCPSom,
				VaoTreTinhCV = bVaoTreLaCV, RaaSomTinhCV = bRaaSomLaCV,//ver 4.0.0.4	
				TG = new ThoiGian { SoPhutLamThem5 = new TimeSpan(0, soPhutLamThem, 0) },
				TD = new ThoiDiem()
			};
			XetCa_1_CIO_V(CIO_V, nv.LichTrinhLV);

			// 6. bỏ CIO_A khỏi DS_CIO_A, DSVaoRa rồi nên phải add lại CIO_V vào DS_CIO_V, DSVaoRa;  sau đó nhớ sort lại
			nv.DS_CIO_V.Add(CIO_V);
			nv.DS_CIO_V.Sort(new cCheckInOutComparer());
			nv.DSVaoRa.Add(CIO_V);
			nv.DSVaoRa.Sort(new cCheckInOutComparer());

			// 7. check tính PC 50%, sau đó sort lại
			XL.CheckTinhPC50_UpdORInsNew_Sort(nv, CIO_V.ThuocNgayCong, choPhepTinhPc50);

			// 8. xác định ngày mà loại bỏ CIO_A để xoá khỏi ngayCong.DSVaoRa, tính lại công, pctc, pcdb của ngày này luôn
			// do trong hàm tính công sẽ reset lại tất cả thông số trong đó có tính PCTC nên phải tính lại PCTC
			var ngayCong_cua_CIO_A = nv.DSNgayCong.Find(item => item.Ngay == CIO_A.ThuocNgayCong);
			ngayCong_cua_CIO_A.DSVaoRa.Remove(CIO_A);
			TinhCong_HangNgay(ngayCong_cua_CIO_A, startNT, endddNT);
			TinhPCTC_CuaNgay(ngayCong_cua_CIO_A, nv.DSXNPhuCap50);
			TinhPCDB_CuaNgay(ngayCong_cua_CIO_A, nv.DSXNPhuCapDB);

			// 9. xác định ngày của CIO_V để thêm CIO_V vào ngayCong.DSVaoRa, tính lại công, pctc, pcdb
			var ngayCong_cua_CIO_V = nv.DSNgayCong.Find(item => item.Ngay == CIO_V.ThuocNgayCong);
			ngayCong_cua_CIO_V.DSVaoRa.Add(CIO_V);
			ngayCong_cua_CIO_V.DSVaoRa.Sort(new cCheckInOutComparer());
			TinhCong_HangNgay(ngayCong_cua_CIO_V, startNT, endddNT);//ver 4.0.0.4
			TinhPCTC_CuaNgay(ngayCong_cua_CIO_V, nv.DSXNPhuCap50);
			TinhPCDB_CuaNgay(ngayCong_cua_CIO_V, nv.DSXNPhuCapDB);

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
