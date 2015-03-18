using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapNhatGioChamCong.BUS;
using CapNhatGioChamCong.DTO;
using log4net;

namespace CapNhatGioChamCong {
    public partial class frm_DiemDanhNV : Form {
        public readonly ILog log = LogManager.GetLogger("frm_DiemDanhNV");

        #region local variable

        public List<int> flstIDPhongBan { get; set; }
        public List<int> flstMaCC { get; set; }
        /// <summary>
        /// chỉ lấy các phòng ban được phép thao tác
        /// </summary>
        public DataTable fTablePhongBan { get; set; }
        /// <summary>
        /// Danh sách tất cả các Nhân viên được phép thao tác, copy từ ThamSo.DSNV
        /// </summary>
        public DataTable fTableDSNV { get; set; }
        public DataTable fTableCheckIO { get; set; }
        /// <summary>
        ///  danh sách tất cả nhân viên trực thuộc 1 phòng ban
        /// </summary>
        public DataTable fTableDSNVTrongPhong { get; set; }

        /// <summary>
        /// danh sách tất cả các nhân viên được phép thao tác
        /// </summary>
        public List<cUserInfo> flstDSNV { get; set; }
        public List<cUserInfo> flstDSNVDiemDanh { get; set; }


        string SelStr_GetDSGioVaoRa(List<cUserInfo> plstDSNVChkXemCong) {
            List<string> ds = new List<string>();
            if (plstDSNVChkXemCong.Count == 0) return string.Empty;
            foreach (cUserInfo nhanvien in plstDSNVChkXemCong)
                ds.Add(nhanvien.UserEnrollNumber.ToString());

            string selectQueryString = @" SELECT CheckInOut.UserEnrollNumber,TimeStr,MachineNo,Source,IDXacNhanCaVaLamThem,UserInfo.UserFullName,
													ID,  ShiftID,  Onduty,  Offduty,
													  DayCount,  WorkingTime,  Workingday,
													  TimeStrIn,  TimeStrOut,
													  LateMin,EarlyMin,  OTMin ";
            selectQueryString += @" from CheckInOut, UserInfo, XacNhanCaVaLamThem ";
            selectQueryString += @" where (((TimeStr between @BatDauVao and @KetThucVao) and MachineNo % 2 = 1)
                                        or ((TimeStr between @BatDauRa and @KetThucRa) and MachineNo % 2 = 0))
                                        and (UserInfo.UserEnrollNumber = CheckInOut.UserEnrollNumber)
		                                and (CheckInOut.UserEnrollNumber = {0} ";
            selectQueryString = String.Format(selectQueryString, String.Join(" or CheckInOut.UserEnrollNumber = ", ds.ToArray()));
            selectQueryString += " ) ";
            selectQueryString += " and IDXacNhanCaVaLamThem = ID " +
                                 " group by CheckInOut.UserEnrollNumber,TimeStr,MachineNo,IDXacNhanCaVaLamThem,Source,UserInfo.UserFullName";
            selectQueryString += " order by CheckInOut.UserEnrollNumber asc, TimeStr asc";
            ds.Clear();
            return selectQueryString;
        }

        #endregion

        // hàm xử lý -----------------------------------------------------------------------------
        #region load treeview
        public TreeView loadTreePhgBan(TreeView tvDSPhongBan, DataTable pDataTable) {
            if (pDataTable.Rows.Count > 0) {
                foreach (DataRow dataRow in pDataTable.Select("RelationID = 0")) {
                    TreeNode ParentNode = new TreeNode { Text = dataRow["Description"].ToString(), Tag = (int)dataRow["ID"] };
                    tvDSPhongBan.Nodes.Add(ParentNode);
                    loadTreeSubNode(ref ParentNode, (int)(dataRow["ID"]), pDataTable);
                }
            }
            return tvDSPhongBan;
        }

        private void loadTreeSubNode(ref TreeNode ParentNode, int ParentId, DataTable dtMenu) {
            DataRow[] childs = dtMenu.Select("RelationID = " + ParentId);
            foreach (DataRow dRow in childs) {
                TreeNode child = new TreeNode { Text = dRow["Description"].ToString(), Tag = (int)dRow["ID"], ToolTipText = dRow["Description"].ToString() };
                ParentNode.Nodes.Add(child);
                //Recursion Call
                loadTreeSubNode(ref child, (int)dRow["ID"], dtMenu);
            }
        }

        void GetNodeID(TreeNode root) {
            if (root == null) return;
            if (root.FirstNode == null) {
                flstIDPhongBan.Add((int)root.Tag);
                root = root.NextNode;
                return;
            }
            if (root.FirstNode != null) {
                foreach (TreeNode treeNode in root.Nodes)
                    GetNodeID(treeNode);
            }
        }

        private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
            flstIDPhongBan.Clear();
            if (e.Node.FirstNode != null) GetNodeID(e.Node);
            else flstIDPhongBan.Add((int)e.Node.Tag);
            e.Node.Expand();

            string temp = "UserIDD = {0}";
            temp = String.Format(temp, String.Join(" or UserIDD = ", flstIDPhongBan.ToArray()));

            if (fTableDSNVTrongPhong == null) fTableDSNVTrongPhong = fTableDSNV.Clone();
            else fTableDSNVTrongPhong.Rows.Clear();

            foreach (DataRow row in fTableDSNV.Select(temp, "UserIDD asc, UserEnrollNumber asc", DataViewRowState.CurrentRows))
                fTableDSNVTrongPhong.ImportRow(row);

        }

        #endregion

        public frm_DiemDanhNV() {
            InitializeComponent();
            dgrdTongHop.AutoGenerateColumns = false;
        }

        private void btnDiemDanh_Click(object sender, EventArgs e) {
            //1. lấy dữ liệu từ form
            #region lấy ngày BD và kết thúc, và update lại Ngày BD = 1 ngày trước 31/08 12:00 AM, ngày KT là 1 ngày sau ngay 1 23:59:59
            dtpNgay.Update();
            DateTime ngayBD = dtpNgay.Value.Date;
            ngayBD = ngayBD.AddDays(-1d);
            DateTime ngayKT = ngayBD.AddDays(2d).Subtract(new TimeSpan(0, 0, 1));
            #endregion
            //-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

            //2. lấy danh sách nhân viên check
            if (flstDSNVDiemDanh == null) flstDSNVDiemDanh = new List<cUserInfo>();
            else flstDSNVDiemDanh.Clear();

            LayDSNVXemCong(fTableDSNVTrongPhong, flstDSNVDiemDanh, flstDSNV);
            //3. lấy dữ liệu chấm công của các nhân viên
            //[CHÚ Ý] ngày bắt đầu và kết thúc đã cộng trừ thêm 1 ngày trước sau ở bở trên
            try {
                XL.XemCong(flstDSNVDiemDanh, ngayBD, ngayKT);
            } catch (Exception exception) {
                log.Info(exception);
                MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại sau.", "Lỗi");
                GC.Collect();
                return;
            }

            //4. xử lý dữ liệu để đưa lên lưới tổng hợp
            DataTable tableCTDiemDanh = dgrdTongHop.DataSource as DataTable;
            if (tableCTDiemDanh == null)
                tableCTDiemDanh = XL.TaoCauTrucDataTable(
                    new[] { "UserEnrollNumber", "UserFullName", "TimeStrVao1", "TimeStrRa1", "TimeStrVao2", "TimeStrRa2", "TimeStrVao3", "TimeStrRa3", "ShiftID1", "ShiftID2", "ShiftID3", "Ca", "TrangThai"},
                    new[] { typeof(int), typeof(string), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(int), typeof(int), typeof(int), typeof(string), typeof(string), });
            else tableCTDiemDanh.Rows.Clear();

            int SoNVDangLamViec = 0 , SoNVDaRaVe = 0, SoNVVang = 0;
            foreach (var nhanvien in flstDSNVDiemDanh) {
                DataRow row = tableCTDiemDanh.NewRow();
                row["UserEnrollNumber"] = nhanvien.UserEnrollNumber;
                row["UserFullName"] = nhanvien.UserFullName;
                cNgayCong ngayCong = nhanvien.DSNgayCong[1];
                //nếu có check thì khỏi ghi vắng
                string ChuoiCa = string.Empty;
                string ChuoiTrangThai = string.Empty;
                if (ngayCong.HasCheck) {
                    for (int i = 0; i < ngayCong.DSVaoRa.Count; i++) {
                        if (i >= 3) break;
                        row["TimeStrVao" + (i + 1)] = (ngayCong.DSVaoRa[i].Vao != null) ? ngayCong.DSVaoRa[i].Vao.TimeStr : (object)DBNull.Value;
                        row["TimeStrRa" + (i + 1)] = (ngayCong.DSVaoRa[i].Raa != null) ? ngayCong.DSVaoRa[i].Raa.TimeStr : (object)DBNull.Value;
                        if (ngayCong.DSVaoRa[i].HaveINOUT > 0)
                            ChuoiCa += ngayCong.DSVaoRa[i].ThuocCa.ShiftCode + "; ";
                        else if (ngayCong.DSVaoRa[i].HaveINOUT == -1)
                            ChuoiCa += "KV; ";
                        else if (ngayCong.DSVaoRa[i].HaveINOUT == -2)
                            ChuoiCa += "KR; ";
                    }
                    cChkInOut lastCIO1 = ngayCong.DSVaoRa[ngayCong.DSVaoRa.Count - 1];
                    // xét vào ra cuối để ghi trạng thái
                    if (lastCIO1.HaveINOUT == -2) { 
                        ChuoiTrangThai = "Đang làm việc; ";
                        SoNVDangLamViec++;
                    }
                    else if (lastCIO1.HaveINOUT > 0 || lastCIO1.HaveINOUT == -1) {
                        ChuoiTrangThai = "Đã ra về; ";
                        SoNVDaRaVe++;
                    }

                }
                else { // không có check, kiểm tra có khai báo vắng ko, nếu có thì ghi
                    SoNVVang++;
                    ChuoiCa = string.Empty;
                    if (ngayCong.DSVang != null && ngayCong.DSVang.Count != 0) {
                        foreach (var loaiVang in ngayCong.DSVang) {
                            ChuoiTrangThai += "Vắng " + loaiVang.KyHieu + "; ";
                        }
                    } else {
                        ChuoiTrangThai = "Vắng";
                    }
                }
                row["Ca"] = ChuoiCa;
                row["TrangThai"] = ChuoiTrangThai;
                tableCTDiemDanh.Rows.Add(row);
            }
            tbTongSoNV.Text = flstDSNVDiemDanh.Count.ToString();
            tbSoNVDangLamViec.Text = SoNVDangLamViec.ToString();
            tbSoNVDaRaVe.Text = SoNVDaRaVe.ToString();
            tbSoNVVang.Text = SoNVVang.ToString();
            dgrdTongHop.DataSource = tableCTDiemDanh;
        }


        private void frm_DiemDanhNV_Load(object sender, EventArgs e) {
            // 1. khởi tạo các biến cục bộ
            flstIDPhongBan = new List<int>();

            //2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan : xoá dữ liệu trước và load
            fTablePhongBan = ThamSo.TablePhongBan.Copy();
            treePhongBan.Nodes.Clear();
            loadTreePhgBan(treePhongBan, fTablePhongBan);

            // 3. Duyệt  dữ liệu toàn bộ danh sách nhân viên được phép thao tác(và thêm cột check) 			
            // và  khởi tạo các giá trị mặc định cho từng nhân viên  
            fTableDSNV = ThamSo.DataTableDSNV.Copy();
            flstDSNV = new List<cUserInfo>();
            KhoitaoDSNV(fTableDSNV, flstDSNV);

            // đăng ký sự kiện cho tree và chọn topNode
            treePhongBan.AfterSelect += treePhongBan_AfterSelect;
            treePhongBan.SelectedNode = treePhongBan.TopNode;
        }

        private void KhoitaoDSNV(DataTable TableDSNV, List<cUserInfo> dsnv) {
            if (TableDSNV == null || TableDSNV.Rows.Count == 0) return;

            foreach (DataRow row in TableDSNV.Rows) {
                cShiftSchedule tmpLichTrinh = ThamSo.DSLichTrinh.Find(item => item.SchID == (int)row["SchID"]);
                List<cShift> tmpDSCa = tmpLichTrinh.ListT1;
                //List<cShift> tmpDSCaChonGio = new List<cShift>(ThamSo.DSCa);
                cUserInfo nhanvien = new cUserInfo() {
                    UserEnrollNumber = (int)row["UserEnrollNumber"], UserFullName = row["UserFullName"].ToString(),
                    LichTrinhLV = tmpLichTrinh, DSCa = tmpDSCa,
					HeSoLuongCB = (Single)row["HeSoLuongCB"], HeSoLuongSP = (Single)row["HeSoLuongSP"],
					BoPhan = new cPhongBan() { ID = (int)row["UserIDD"], TenPhongBan = row["Description"].ToString() },
                };
                dsnv.Add(nhanvien);
            }
        }

        public void LayDSNVXemCong(DataTable pTableDSNVTrongPhong, List<cUserInfo> plstDSNVDiemDanh, List<cUserInfo> plstDSNV) {

            if (pTableDSNVTrongPhong == null || pTableDSNVTrongPhong.Rows.Count == 0) return;
            foreach (DataRow row in pTableDSNVTrongPhong.Rows) {
                cUserInfo nhanvien = plstDSNV.Find(info => info.UserEnrollNumber == (int)row["UserEnrollNumber"]);
                nhanvien.ClearAll();
                plstDSNVDiemDanh.Add(nhanvien);
            }

        }

    }
}
