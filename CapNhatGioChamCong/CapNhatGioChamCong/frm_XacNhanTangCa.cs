using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapNhatGioChamCong.BUS;
using CapNhatGioChamCong.DAO;
using CapNhatGioChamCong.DTO;
using log4net;

namespace CapNhatGioChamCong {
    public partial class frm_XacNhanTangCa : Form {
		private readonly ILog log = LogManager.GetLogger("frm_XacNhanTangCa");

        public List<cUserInfo> fListNVChk;
        public DateTime fNgayBD { get; set; }
        public DateTime fNgayKT { get; set; }

        private DataTable TaoCauTrucDataTableTongHop() {
            DataTable kq = new DataTable();
            kq.Columns.Add("UserEnrollNumber", typeof(int)); //0
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
            return kq;
        }

        public frm_XacNhanTangCa() {
            InitializeComponent();
            foreach (DataGridViewColumn column in dgrdGioCoLamThem.Columns) {
                column.Name = ThamSo.prefixColNameGrid2 + column.DataPropertyName;
            }
        }

        private void frmXacNhanTangCa_Load(object sender, EventArgs e) {
            LoadDataGrid();
            dgrdGioCoLamThem.RowEnter += dgrdGioCoLamThem_RowEnter;
        }

        private void LoadDataGrid() {
            if (fListNVChk == null) return;

            DataTable tmpTableCoGioLamThem = dgrdGioCoLamThem.DataSource as DataTable;
            if (tmpTableCoGioLamThem == null) tmpTableCoGioLamThem = TaoCauTrucDataTableTongHop();
            else tmpTableCoGioLamThem.Rows.Clear();

            List<cChkInOut> dsGioLamTren8Gio;
            foreach (cUserInfo nhanvien in fListNVChk) {
                dsGioLamTren8Gio = nhanvien.DSVaoRa.FindAll(item => (item.HaveINOUT > 0 && item.DaXN == false &&
																		( (item.TGLamTinhCong > ThamSo._8gio)
																		|| (item.OLaiThem > ThamSo._0gio && item.TGLamTinhCong == ThamSo._8gio)
																		 ) ));
                if (dsGioLamTren8Gio.Count == 0) continue;
                foreach (cChkInOut gio in dsGioLamTren8Gio) {
                    DataRow row = tmpTableCoGioLamThem.NewRow();
                    #region fill datarow
                    row["UserEnrollNumber"] = nhanvien.UserEnrollNumber;
                    row["UserFullName"] = nhanvien.UserFullName;
                    row["TimeStrNgay"] = gio.ThuocNgayCong;
                    row["TimeStrThu"] = gio.ThuocNgayCong;
                    row["TimeStrVao"] = gio.Vao.TimeStr;
                    row["TimeStrRa"] = gio.Raa.TimeStr;
                    row["ShiftID"] = gio.ThuocCa.ShiftID; // điều kiện Find all đã loại hết giờ vào ko ra,ra ko vào nên ở đây khỏi check null
                    row["ShiftCode"] = gio.ThuocCa.ShiftCode;
                    row["Cong"] = gio.Cong;
                    row["PhuCap"] = gio.PhuCapDem;
                    row["TongGioLam"] = gio.TGLamTinhCong.TotalHours;
                    row["TongGioThuc"] = gio.TongGioThuc.TotalHours;

                    #endregion
                    tmpTableCoGioLamThem.Rows.Add(row);
                }
            }

            dgrdGioCoLamThem.DataSource = tmpTableCoGioLamThem;
        }

        private void FillValueForControl(cChkInOut pChkInOut, cShift pcurrCa) {
	        
            lbCheckINOUT.Tag = pChkInOut;
            tbGioLam.Tag = pChkInOut.TGLamTinhCong;
            tbTreSom.Tag = (int)Math.Floor(((pChkInOut.VaoTre + pChkInOut.RaaSom).TotalMinutes));
            tbOLaiThem.Tag = (int)Math.Floor((pChkInOut.OLaiThem.TotalMinutes));//[TBD] Xem lại chỗ này vì phải ở lại trên 30ph mới có currỞ Lại thêm
            lbTre.Tag = (int)Math.Floor(pChkInOut.VaoTre.TotalMinutes);
            lbSom.Tag = (int)Math.Floor(pChkInOut.RaaSom.TotalMinutes);

            tbGioLam.Text = pChkInOut.TGLamTinhCong.TotalHours.ToString("#0.##");
            tbTreSom.Text = ((int)tbTreSom.Tag).ToString();
            tbOLaiThem.Text = ((int)tbOLaiThem.Tag).ToString();
            lbTre.Text = ((int)lbTre.Tag).ToString();
            lbSom.Text = ((int)lbSom.Tag).ToString();
            if ((int)tbOLaiThem.Tag == 0) {
                checkXacNhanLamThem.CheckedChanged -= checkXacNhanLamThem_CheckedChanged;
                checkXacNhanLamThem.Enabled = false;
                numPhutTinhLamThem.Enabled = false;
                numPhutTinhLamThem.Value = 0;
            }
            else {
                checkXacNhanLamThem.CheckedChanged += checkXacNhanLamThem_CheckedChanged;
                checkXacNhanLamThem.Enabled = true;
                int tmpMax = (int)tbOLaiThem.Tag;// (((int)tbOLaiThem.Tag - (int)tbTreSom.Tag) > ThamSo._maxPhutLamThem) ? ((int)tbOLaiThem.Tag - (int)tbTreSom.Tag) : 0;
                numPhutTinhLamThem.Value = 0;
                numPhutTinhLamThem.Maximum = tmpMax;
            }
            checkXacNhanLamThem.Checked = false;
        }

        private void checkXacNhanLamThem_CheckedChanged(object sender, EventArgs e) {
            numPhutTinhLamThem.Enabled = checkXacNhanLamThem.Checked;
            numPhutTinhLamThem.Value = 0;
        }

        private void cbChonCa_SelectedIndexChanged(object sender, EventArgs e) {
            cShift currCa = cbChonCa.SelectedItem as cShift;

            if (lbCheckINOUT.Tag == null) return;
            cChkInOut tmpCurrChkINOUT = (cChkInOut)lbCheckINOUT.Tag;

            if (currCa.LoaiCa == 0) {
                XL.TinhCongTheoCa(tmpCurrChkINOUT, currCa);
            }
            else {
                XL.TinhCongTheoCa(tmpCurrChkINOUT, currCa);

            }
            // gán giá trị cho các control chi tiết
            FillValueForControl(tmpCurrChkINOUT, currCa);


        }

        private void dgrdGioCoLamThem_RowEnter(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex == -1) {
                cbChonCa.SelectedIndexChanged -= cbChonCa_SelectedIndexChanged;
                cbChonCa.DataSource = null;
                tbTreSom.Tag = tbOLaiThem.Tag = lbTre.Tag = lbSom.Tag = null;
                tbTreSom.Text = tbOLaiThem.Text = lbTre.Text = lbSom.Text = string.Empty;
                return;
            }
            DataGridView tmpDataGrid = (DataGridView)sender;
            //1. lấy dòng đang chọn và dữ liệu được chọn
            DataRowView dataRowView = tmpDataGrid.Rows[e.RowIndex].DataBoundItem as DataRowView;
            tmpDataGrid.Tag = dataRowView;
            if (dataRowView == null) return;

            //2. lấy user đang chọn để load lại ds ca mở rộng
            int tmpUserEnrollNumber = (int)dataRowView["UserEnrollNumber"];
            cUserInfo tmpNV = fListNVChk.Find(item => item.UserEnrollNumber == tmpUserEnrollNumber);

            //3. lấy tmpCurrChkINOUT đang chọn
            DateTime tmpTimeStrVao = (DateTime)dataRowView[ThamSo.nameVao];
            DateTime tmpTimeStrRa = (DateTime)dataRowView[ThamSo.nameRa];
            DateTime tmpNgay = ThamSo.GetDate(tmpTimeStrVao);
            cChkInOut tmpCurrChkINOUT = tmpNV.DSVaoRa.Find(item => (item.HaveINOUT > 0) && item.Vao.TimeStr == tmpTimeStrVao && item.Raa.TimeStr == tmpTimeStrRa);
            // tạo bản copy để tính công chứ ko tính công trực tiếp trên tmpCurrChkINOUT vì sẽ làm thay đổi giá trị bên trong của nó
            cChkInOut tmpCurrChkINOUTCopy = MyUtility.DeepClone(tmpCurrChkINOUT);

            // 4. nếu đã thuộc ca thì giữ nguyên ca, ko load những cái khác
            cShift tmpCa = tmpCurrChkINOUTCopy.ThuocCa;
            List<cShift> tmpDSCa;
            if (tmpCa.ShiftID != int.MinValue) { // không cần kiểm tra tmpCa null vì DS đã lọc chỉ còn giữ HaveINOUT=true
                tmpDSCa = new List<cShift>() { tmpCa };
            }
            else { // tmpCa.ShiftID == int.MinValue ==> ca KĐQĐ =>load ds ca mở rộng
                tmpDSCa = new List<cShift>(tmpNV.DSCaMoRong);
                cShift caKDQD = tmpCa;
                XL.TaoCaTuDo(tmpCa, tmpTimeStrVao, ThamSo._8gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1f);
                cShift CaDaiA = new cShift() { ShiftID = int.MinValue + 1, ShiftCode = "Ca Dài 12 giờ", LoaiCa = 1 };
                XL.TaoCaTuDo(CaDaiA, tmpTimeStrVao, ThamSo._12gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1.5f);
                log.Debug(CaDaiA.OnnDutyTS + "\t" + CaDaiA.OffDutyTS + "\t" + CaDaiA.WorkingTimeTS);
                tmpDSCa.Insert(0, caKDQD);
                tmpDSCa.Insert(1, CaDaiA);
                tmpDSCa.RemoveAll(item => item.LoaiCa == 0 && (tmpTimeStrRa < tmpNgay.Add(item.OnnDutyTS) || tmpTimeStrVao > tmpNgay.Add(item.OffDutyTS)));
            }
            tbGioVao.Tag = tmpTimeStrVao; tbGioRaa.Tag = tmpTimeStrRa;
            tbGioVao.Text = tmpTimeStrVao.ToString("H:mm:ss d/M/yyyy"); tbGioRaa.Text = tmpTimeStrRa.ToString("H:mm:ss d/M/yyyy");
            lbCheckINOUT.Tag = tmpCurrChkINOUTCopy;

            cbChonCa.SelectedIndexChanged -= cbChonCa_SelectedIndexChanged;
            cbChonCa.DataSource = tmpDSCa;
            cbChonCa.ValueMember = "ShiftID";
            cbChonCa.DisplayMember = "ShiftCode"; 
            cbChonCa.SelectedIndexChanged += cbChonCa_SelectedIndexChanged;
            cbChonCa.SelectedItem = tmpCa;
			checkTinhPC150.Checked = false;

            FillValueForControl(tmpCurrChkINOUTCopy, tmpCa);
            cbChonCa.Update();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnXacNhan_Click(object sender, EventArgs e) {
            #region lấy dữ liệu từ form

            DataGridView tmpDatagrid = dgrdGioCoLamThem;
            DataGridViewRow tmpSelectedRow = tmpDatagrid.SelectedRows[0];
            DataRowView rowView = tmpSelectedRow.DataBoundItem as DataRowView;
            int tmpUserEnrollNumber = (int)rowView["UserEnrollNumber"];
            DateTime tmpOldChkInTime = (DateTime)rowView["TimeStrVao"];
            DateTime tmpOldChkOutTime = (DateTime)rowView["TimeStrRa"];
            cUserInfo tmpNV = fListNVChk.Find(item => item.UserEnrollNumber == tmpUserEnrollNumber);
            cChkInOut tmpOldChkINOUT = tmpNV.DSVaoRa.Find(item => (item.HaveINOUT > 0) && item.Vao.TimeStr == tmpOldChkInTime && item.Raa.TimeStr == tmpOldChkOutTime);
            cShift tmpOldShift = tmpOldChkINOUT.ThuocCa;

            if (lbCheckINOUT.Tag == null) { AutoClosingMessageBox.Show("lbCheckINOUT.Tag == null", "Error", 1000); return; }
            cShift tmpNewShift = (cShift)cbChonCa.SelectedItem;
            cChkInOut tmpNewChkINOUT = (cChkInOut)lbCheckINOUT.Tag;
            // lúc tính công chưa có gán THUOCCA lại nên ở đây phải gán lại
            tmpNewChkINOUT.ThuocCa = tmpNewShift;

            int tmpNewShiftID = tmpNewShift.ShiftID; // shiftID của DSCa mở rộng
            int tmpTre = int.Parse(lbTre.Tag.ToString());
            TimeSpan tmpTreTS = new TimeSpan(0, tmpTre, 0);
            int tmpSom = int.Parse(lbSom.Tag.ToString());
            TimeSpan tmpSomTS = new TimeSpan(0, tmpSom, 0);
            int tmpSoPhutTreSom = int.Parse(tbTreSom.Tag.ToString()); // > 0 nếu (chấm tay < 8h => ra sớm), đúng ca nhưng có vào trễ ra sớm
            TimeSpan tmpSoPhutTreSomTS = new TimeSpan(0, tmpSoPhutTreSom, 0);
            int tmpSoPhutOLaiThem = int.Parse(tbOLaiThem.Tag.ToString());
            TimeSpan tmpSoPhutOLaiThemTS = new TimeSpan(0, tmpSoPhutOLaiThem, 0);
            bool tmpIsOT = checkXacNhanLamThem.Checked;
            int tmpSoPhutLamThem = (tmpIsOT) ? (int)numPhutTinhLamThem.Value : 0;
            TimeSpan tmpSoPhutLamThemTS = new TimeSpan(0, tmpSoPhutLamThem, 0);
            #endregion

	        bool pTinhPC150=checkTinhPC150.Checked;
            if (tmpNewShift.OnnDutyTS > ThamSo._20h00 && tmpNewShift.Workingday > 1f) {
                cChkInOut[] arrCIO = XL.TachGio2Ca3Va1(tmpNV.DSCa, tmpNewChkINOUT, tmpNewChkINOUT.ThuocCa);
                XL.TinhCongTheoCa(arrCIO[0], arrCIO[0].ThuocCa);
                XL.TinhCongTheoCa(arrCIO[1], arrCIO[1].ThuocCa);

                XL.BUS_TachCaVaXacNhan(tmpNV, tmpOldChkINOUT, arrCIO, tmpIsOT, tmpSoPhutLamThem, tmpSoPhutLamThemTS,pTinhPC150);
                XL.XemCong(new List<cUserInfo>() { tmpNV }, fNgayBD, fNgayKT);
            }
            else {
                XL.BUS_XacNhan(tmpNV, tmpOldChkINOUT, tmpNewChkINOUT, tmpIsOT, tmpSoPhutLamThem, tmpSoPhutLamThemTS,pTinhPC150);
                XL.XemCong(new List<cUserInfo>() { tmpNV }, fNgayBD, fNgayKT);
            }
            LoadDataGrid();

        }

        private void checkTinhPC150_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Check mục này để tính phụ cấp 50% lương cho ngày công trường hợp làm trên 8 tiếng.", checkTinhPC150, 5000);
        }




    }
}
