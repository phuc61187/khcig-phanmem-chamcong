using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v02.BUS;
using ChamCong_v02.DAO;
using ChamCong_v02.DTO;
using log4net;

namespace ChamCong_v02 {
    public partial class frm_112_XacNhanTangCa : Form {
        private readonly ILog lg = LogManager.GetLogger("frm_112_XacNhanTangCa");

        public List<cUserInfo> m_DSNV;
        public DataRowView currView = null;
        public cUserInfo currNV = null;
        public cChkInOut tmpCurrChkINOUT = null;
        public cNgayCong currNgay = null;
        public int currRowIndex = -1;

	    public DateTime fNgayBD;
	    public DateTime fNgayKT;

        private DataTable TaoCauTrucDataTable() {
            DataTable kq = new DataTable();
            kq.Columns.Add("UserEnrollNumber", typeof(int)); //0
			kq.Columns.Add("UserFullCode", typeof(string)); //1
            kq.Columns.Add("UserFullName", typeof(string)); //1
            kq.Columns.Add("TimeStrNgay", typeof(DateTime)); //2
            kq.Columns.Add("TimeStrThu", typeof(DateTime)); //3
            kq.Columns.Add("TimeStrVao", typeof(DateTime)); //4
            kq.Columns.Add("TimeStrRa", typeof(DateTime)); //5
            kq.Columns.Add("ShiftCode", typeof(string)); //8
            kq.Columns.Add("ShiftID", typeof(int)); //9
            kq.Columns.Add("Cong", typeof(Double)); //20
            kq.Columns.Add("PhuCap", typeof(Double)); //20
            kq.Columns.Add("TongGioLam", typeof(Double));
            kq.Columns.Add("TongGioThuc", typeof(Double));
            kq.Columns.Add("obj", typeof(cChkInOut));
            return kq;
        }

        public frm_112_XacNhanTangCa() {
            InitializeComponent();
	        log4net.Config.XmlConfigurator.Configure();
	        dgrdGioCoLamThem.AutoGenerateColumns = false;
            dgrdGioCoLamThem.SelectionChanged += dgrdGioCoLamThem_SelectionChanged;
            cbChonCa.SelectedIndexChanged += cbChonCa_SelectedIndexChanged;

        }

        private void dgrdGioCoLamThem_SelectionChanged(object sender, EventArgs e) {
            if (dgrdGioCoLamThem.SelectedRows.Count == 0) {
                #region reset layout
                cbChonCa.DataSource = null;
                tbGioLam.Text = "0";
                tbTreSom.Text = "0";
                tbOLaiThem.Text = "0";
                checkXacNhanLamThem.Checked = false;
                checkXacNhanLamThem.Enabled = false;
                numPhutTinhLamThem.Value = 0;
                numPhutTinhLamThem.Enabled = false;
                #endregion
                // ngoài reset layout thì disable nút xác nhận để tránh ấn nhầm gây lỗi
                btnXacNhan.Enabled = false;
                return;
            }
            // vì disable nếu e.rowindex = -1 nên bật lại nếu >= 0
            btnXacNhan.Enabled = true;
            DataGridView tmpDataGrid = (DataGridView)sender;
            //1. lấy dòng đang chọn và dữ liệu được chọn. gán currView để có thể lấy được dữ liệu
            DataRowView dataRowView = tmpDataGrid.SelectedRows[0].DataBoundItem as DataRowView;
            currView = dataRowView;
            if (currView == null) return;

            //2. tìm user đang chọn để load lại ds ca mở rộng
            int tmpUserEnrollNumber = (int)dataRowView["UserEnrollNumber"];
            currNV = m_DSNV.Find(item => item.UserEnrollNumber == tmpUserEnrollNumber);

            //3. xác định ngày đang chọn

            //3. tìm tmpCurrChkINOUT đang chọn
            tmpCurrChkINOUT = dataRowView["obj"] as cChkInOut;
            if (tmpCurrChkINOUT == null) return;
            DateTime tmpTimeStrVao = tmpCurrChkINOUT.Vao.TimeStr;
            DateTime tmpTimeStrRa = tmpCurrChkINOUT.Raa.TimeStr;
            DateTime ngaydangchon = tmpCurrChkINOUT.ThuocNgayCong;

            // tạo bản copy để tính công chứ ko tính công trực tiếp trên tmpCurrChkINOUT vì sẽ làm thay đổi giá trị bên trong của nó
            //tmpCurrChkINOUTCopy = MyUtility.DeepClone(tmpCurrChkINOUT);

            // 4. nếu đã thuộc ca thì giữ nguyên ca, ko load những cái khác
            cShift tmpCa = MyUtility.DeepClone(tmpCurrChkINOUT.ThuocCa);
            List<cShift> tmpDSCa;
            if (tmpCa.ShiftID != int.MinValue) { // không cần kiểm tra tmpCa null vì DS đã lọc chỉ còn giữ HaveINOUT=true
                tmpDSCa = new List<cShift>() { tmpCa };
            }
            else { // tmpCa.ShiftID == int.MinValue ==> ca KĐQĐ =>load ds ca mở rộng
                tmpDSCa = new List<cShift>(currNV.DSCaMoRong);
                cShift caKDQD = tmpCa;
                // ca KDQD đã có sẵn shiftid = Minvalue
                XL.TaoCaTuDo(tmpCa, tmpTimeStrVao, ThamSo._8gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1f);
                cShift CaDaiA = new cShift() { ShiftID = int.MinValue + 1, ShiftCode = "Ca Dài 12 tiếng", LoaiCa = 1 };
                XL.TaoCaTuDo(CaDaiA, tmpTimeStrVao, ThamSo._12gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1.5f);

                tmpDSCa.Insert(0, caKDQD);
                tmpDSCa.Insert(1, CaDaiA);
                // loại bỏ những ca chắc chắn ko xảy ra: ra ca < check vào 30ph. check ra < vào ca 30ph
                tmpDSCa.RemoveAll(item => item.LoaiCa == 0 && (tmpTimeStrRa < ngaydangchon.Add(item.OnnDutyTS).Add(ThamSo._30phut) || tmpTimeStrVao > ngaydangchon.Add(item.OffDutyTS).Subtract(ThamSo._30phut)));
            }

            // fill dữ liệu 2 dòng giờ vào -giờ ra, fill comboBox danh sách ca, set mặc định item chọn là 0
            //, các giá trị còn lại thì do combo index changed fill
            tbGioVao.Text = tmpTimeStrVao.ToString("H:mm:ss d/M/yyyy");
            tbGioRaa.Text = tmpTimeStrRa.ToString("H:mm:ss d/M/yyyy");

            cbChonCa.ValueMember = "ShiftID";
            cbChonCa.DisplayMember = "ShiftCode";
            cbChonCa.DataSource = tmpDSCa;
            checkTinhPC150.Checked = currNV.MacDinhTinhPC150; // mặc định check tính pc 50% theo nhân viên phòng nào
            cbChonCa.Update();

        }

        private void frmXacNhanTangCa_Load(object sender, EventArgs e) {
            LoadDataGrid();
        }

        private void LoadDataGrid() {
            if (m_DSNV == null) return;

            //tạo cấu trúc table hiển thị hoặc lấy cấu trúc từ table đã tồn tại
            DataTable tmpTableCoGioLamThem = dgrdGioCoLamThem.DataSource as DataTable;
            if (tmpTableCoGioLamThem == null) tmpTableCoGioLamThem = TaoCauTrucDataTable();
            else tmpTableCoGioLamThem.Rows.Clear();


            List<cChkInOut> dsGioLamTren8Gio = new List<cChkInOut>();

            // Lọc ra các ds giờ vào ra 1.đủ inout  | 2. giờ chưa xn | tg làm tính công trên 8h
            foreach (cUserInfo nhanvien in m_DSNV) {
/*                dsGioLamTren8Gio = nhanvien.DSVaoRa.FindAll(item => (item.HaveINOUT > 0 && (item.GetType() != typeof(cChkInOut_V)) &&
                                                                         (item.OLaiThem > ThamSo._0gio && item.TG.LamTinhCong >= ThamSo._8gio))
																	);*/

			 dsGioLamTren8Gio = (from ngayCong in nhanvien.DSNgayCong
					   from vaora in ngayCong.DSVaoRa
					   where (vaora.HaveINOUT > 0 && vaora.GetType() == typeof(cChkInOut_A) && vaora.OLaiThem > ThamSo._0gio && vaora.TG.LamTinhCong >= ThamSo._8gio)
					   || (ngayCong.TongCong > 1 && ngayCong.DSVaoRa.TrueForAll(item => item.GetType() == typeof(cChkInOut_A) && item.TinhPC150 == false))
					   select vaora).ToList();
				if (dsGioLamTren8Gio.Count == 0) continue;
                foreach (cChkInOut gio in dsGioLamTren8Gio) {
                    DataRow row = tmpTableCoGioLamThem.NewRow();
                    #region fill datarow
                    row["UserEnrollNumber"] = nhanvien.UserEnrollNumber;
					row["UserFullCode"] = nhanvien.UserFullCode;
                    row["UserFullName"] = nhanvien.UserFullName;
                    row["TimeStrNgay"] = gio.ThuocNgayCong;
                    row["TimeStrThu"] = gio.ThuocNgayCong;
                    row["TimeStrVao"] = gio.Vao.TimeStr;
                    row["TimeStrRa"] = gio.Raa.TimeStr;
                    row["ShiftID"] = gio.ThuocCa.ShiftID; // điều kiện Find all đã loại hết giờ vào ko ra,ra ko vào nên ở đây khỏi check null
                    row["ShiftCode"] = gio.ThuocCa.ShiftCode;
                    row["Cong"] = gio.Cong;
                    row["PhuCap"] = gio.PhuCapDem;
                    row["TongGioLam"] = gio.TG.LamTinhCong.TotalHours;
                    row["TongGioThuc"] = gio.TongGioThuc.TotalHours;
                    row["obj"] = gio;
                    #endregion
                    tmpTableCoGioLamThem.Rows.Add(row);
                }
            }

            if (tmpTableCoGioLamThem.Rows.Count == 0) {
                AutoClosingMessageBox.Show("Không còn giờ cần xác nhận làm thêm hay tăng ca.", "Thông báo", 4000);
                this.Close();
                return;
            }

            dgrdGioCoLamThem.DataSource = tmpTableCoGioLamThem;
            if (currRowIndex != -1) {
                if (currRowIndex < dgrdGioCoLamThem.RowCount) {
                    dgrdGioCoLamThem.Rows[currRowIndex].Selected = true;
                    dgrdGioCoLamThem.Invalidate();
                }
                else {
                    currRowIndex = -1;
                }
            }
        }


        private void cbChonCa_SelectedIndexChanged(object sender, EventArgs e) {
            if (cbChonCa.DataSource == null || cbChonCa.Items.Count == 0) {
                #region reset layout
                tbGioLam.Text = "0";
                tbTreSom.Text = "0";
                tbOLaiThem.Text = "0";
                checkXacNhanLamThem.Checked = false;
                checkXacNhanLamThem.Enabled = false;
                numPhutTinhLamThem.Value = 0;
                numPhutTinhLamThem.Enabled = false;
                #endregion
                return;
            }

            // đang chọn 1 ca  nào đó, lấy ca đang chọn và fill thông tin
            cShift currCa = cbChonCa.SelectedItem as cShift;
            if (currCa == null) return;

            numPhutTinhLamThem.Value = 0;
            TimeSpan lamthem = new TimeSpan(0, 0, (int)numPhutTinhLamThem.Value, 0);
            DateTime ngaycong = tmpCurrChkINOUT.ThuocNgayCong;

            TimeSpan vaotre = XL.TinhVaoTre(tmpCurrChkINOUT.Vao.TimeStr, ngaycong.Add(currCa.OnnDutyTS), ngaycong.Add(currCa.chophepvaotreTS));
            TimeSpan raasom = XL.TinhRaaSom(tmpCurrChkINOUT.Raa.TimeStr, ngaycong.Add(currCa.OffDutyTS), ngaycong.Add(currCa.chopheprasomTS));
            tbTreSom.Text = Math.Round((vaotre + raasom).TotalMinutes, 2).ToString();
            DateTime vaolam = XL.TinhVaoLam(tmpCurrChkINOUT.Vao.TimeStr, ngaycong.Add(currCa.OnnDutyTS), vaotre);
            DateTime raalam = XL.TinhRaaLam(tmpCurrChkINOUT.Raa.TimeStr, ngaycong.Add(currCa.OffDutyTS), raasom, lamthem);
            tbGioLam.Text = Math.Round((raalam - vaolam - currCa.LunchMinute).TotalHours, 2).ToString();
            TimeSpan olaithem = XL.TinhOLaiThem(tmpCurrChkINOUT.Raa.TimeStr, ngaycong.Add(currCa.OffDutyTS), ngaycong.Add(currCa.batdaulamthemTS));

            checkXacNhanLamThem.Checked = false;
            if (olaithem == ThamSo._0gio) {
                tbOLaiThem.Text = "0";
                checkXacNhanLamThem.Enabled = false;
                numPhutTinhLamThem.Enabled = false;
                numPhutTinhLamThem.Value = 0;
            }
            else {
                tbOLaiThem.Text = Math.Floor(olaithem.TotalMinutes).ToString();
                checkXacNhanLamThem.Enabled = true;
                numPhutTinhLamThem.Enabled = true;
                numPhutTinhLamThem.Value = 0;
                numPhutTinhLamThem.Maximum = Convert.ToDecimal(Math.Floor(olaithem.TotalMinutes));
            }
        }

        private void dgrdGioCoLamThem_RowEnter(object sender, DataGridViewCellEventArgs e) {
            /*
                        if (e.RowIndex == -1) {
                            #region reset layout
                            cbChonCa.DataSource = null;
                            tbGioLam.Text = "0";
                            tbTreSom.Text = "0";
                            tbOLaiThem.Text = "0";
                            checkXacNhanLamThem.Checked = false;
                            checkXacNhanLamThem.Enabled = false;
                            numPhutTinhLamThem.Value = 0;
                            numPhutTinhLamThem.Enabled = false;
                            #endregion
                            // ngoài reset layout thì disable nút xác nhận để tránh ấn nhầm gây lỗi
                            btnXacnhan.Enabled = false;
                            return;
                        }
                        // vì disable nếu e.rowindex = -1 nên bật lại nếu >= 0
                        btnXacnhan.Enabled = true;
                        DataGridView tmpDataGrid = (DataGridView)sender;
                        //1. lấy dòng đang chọn và dữ liệu được chọn. gán currView để có thể lấy được dữ liệu
                        DataRowView dataRowView = tmpDataGrid.Rows[e.RowIndex].DataBoundItem as DataRowView;
                        currView = dataRowView;
                        if (currView == null) return;

                        //2. tìm user đang chọn để load lại ds ca mở rộng
                        int tmpUserEnrollNumber = (int)dataRowView["UserEnrollNumber"];
                        currNV = m_DSNV.Find(item => item.UserEnrollNumber == tmpUserEnrollNumber);

                        //3. xác định ngày đang chọn
                        DateTime ngaydangchon = (DateTime)dataRowView["TimeStrNgay"];
                        currNgay = currNV.DSNgayCong.Find(o => o.NgayCong == ngaydangchon);

                        //3. tìm tmpCurrChkINOUT đang chọn
                        DateTime tmpTimeStrVao = (DateTime)dataRowView[ThamSo.nameVao];
                        DateTime tmpTimeStrRa = (DateTime)dataRowView[ThamSo.nameRa];
                        tmpCurrChkINOUT = currNgay.DSVaoRa.Find(item => (item.HaveINOUT > 0) && item.Vao.TimeStr == tmpTimeStrVao && item.Raa.TimeStr == tmpTimeStrRa);

                        // tạo bản copy để tính công chứ ko tính công trực tiếp trên tmpCurrChkINOUT vì sẽ làm thay đổi giá trị bên trong của nó
                        //tmpCurrChkINOUTCopy = MyUtility.DeepClone(tmpCurrChkINOUT);

                        // 4. nếu đã thuộc ca thì giữ nguyên ca, ko load những cái khác
                        cShift tmpCa = MyUtility.DeepClone(tmpCurrChkINOUT.ThuocCa);
                        List<cShift> tmpDSCa;
                        if (tmpCa.ShiftID != int.MinValue) { // không cần kiểm tra tmpCa null vì DS đã lọc chỉ còn giữ HaveINOUT=true
                            tmpDSCa = new List<cShift>() { tmpCa };
                        }
                        else { // tmpCa.ShiftID == int.MinValue ==> ca KĐQĐ =>load ds ca mở rộng
                            tmpDSCa = new List<cShift>(currNV.DSCaMoRong);
                            cShift caKDQD = tmpCa;
                            // ca KDQD đã có sẵn shiftid = Minvalue
                            XL.TaoCaTuDo(tmpCa, tmpTimeStrVao, ThamSo._8gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1f);
                            cShift CaDaiA = new cShift() { ShiftID = int.MinValue + 1, ShiftCode = "Ca Dài 12 tiếng", LoaiCa = 1 };
                            XL.TaoCaTuDo(CaDaiA, tmpTimeStrVao, ThamSo._12gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1.5f);

                            tmpDSCa.Insert(0, caKDQD);
                            tmpDSCa.Insert(1, CaDaiA);
                            // loại bỏ những ca chắc chắn ko xảy ra: ra ca < check vào 30ph. check ra < vào ca 30ph
                            tmpDSCa.RemoveAll(item => item.LoaiCa == 0 && (tmpTimeStrRa < ngaydangchon.Add(item.OnnDutyTS).Add(ThamSo._30phut) || tmpTimeStrVao > ngaydangchon.Add(item.OffDutyTS).Subtract(ThamSo._30phut)));
                        }

                        // fill dữ liệu 2 dòng giờ vào -giờ ra, fill comboBox danh sách ca, set mặc định item chọn là 0
                        //, các giá trị còn lại thì do combo index changed fill
                        tbGioVao.Text = tmpTimeStrVao.ToString("H:mm:ss d/M/yyyy");
                        tbGioRaa.Text = tmpTimeStrRa.ToString("H:mm:ss d/M/yyyy");

                        cbChonCa.ValueMember = "ShiftID";
                        cbChonCa.DisplayMember = "ShiftCode";
                        cbChonCa.DataSource = tmpDSCa;
                        checkTinhPC150.Checked = currNV.MacDinhTinhPC150; // mặc định check tính pc 50% theo nhân viên phòng nào
                        cbChonCa.Update();
            */
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnXacNhan_Click(object sender, EventArgs e) {
            #region lấy dữ liệu từ form

            currRowIndex = dgrdGioCoLamThem.SelectedRows[0].Index;
            numPhutTinhLamThem.Update();
            int tmpSoPhutLamThem = (checkXacNhanLamThem.Checked) ? Convert.ToInt32(numPhutTinhLamThem.Value) : 0;
            int iUserEnrollNumber = currNV.UserEnrollNumber;

            cShift tmpNewShift = cbChonCa.SelectedItem as cShift;
            if (tmpNewShift == null) return;
            // lúc tính công chưa có gán THUOCCA lại nên ở đây phải gán lại

            #endregion

            bool pTinhPC150 = checkTinhPC150.Checked;
            if (tmpNewShift.OnnDutyTS > ThamSo._20h00 && tmpNewShift.Workingday > 1f) {

                cChkInOut[] arrCIO = XL.TachGio2Ca3Va1(currNV.DSCa, tmpCurrChkINOUT, tmpNewShift);
                DAL.ThemGioChoNV(iUserEnrollNumber, arrCIO[0].Raa.TimeStr, false, arrCIO[0].Raa.MachineNo, ThamSo.currUserID, "", "tách giờ ca 3&1, ca 3&1A");
                DAL.ThemGioChoNV(iUserEnrollNumber, arrCIO[1].Vao.TimeStr, true, arrCIO[1].Vao.MachineNo, ThamSo.currUserID, "", "tách giờ ca 3&1, ca 3&1A");
                XL.BUS_XacNhan(iUserEnrollNumber, arrCIO[0].ThuocCa, pTinhPC150, 0, arrCIO[0]); // vì ca 3 ko có làm thêm nên 0
                XL.BUS_XacNhan(iUserEnrollNumber, arrCIO[1].ThuocCa, pTinhPC150, tmpSoPhutLamThem, arrCIO[1]); // vì ca 1 mới có làm thêm 

                XL.XemCong(currNV, m_DSNV, fNgayBD, fNgayKT);
            }
            else {
                XL.BUS_XacNhan(iUserEnrollNumber, tmpNewShift, pTinhPC150, tmpSoPhutLamThem, tmpCurrChkINOUT);
                XL.XemCong(currNV, m_DSNV, fNgayBD, fNgayKT);
            }
            LoadDataGrid();

        }

        private void checkTinhPC150_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Check mục này để tính phụ cấp 50% lương cho ngày công trường hợp làm trên 8 tiếng.", checkTinhPC150, 5000);
        }


        private void numPhutTinhLamThem_ValueChanged(object sender, EventArgs e) {
            if (numPhutTinhLamThem.Value > 0) checkXacNhanLamThem.Checked = true;
            else checkXacNhanLamThem.Checked = false;

        }





    }
}
