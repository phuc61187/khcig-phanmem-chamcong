using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CapNhatGioChamCong.DAO;
using CapNhatGioChamCong.DTO;
using CapNhatGioChamCong.Properties;
using System.Diagnostics;
using log4net;

namespace CapNhatGioChamCong {
    public partial class frm_main : Form {
        public readonly ILog log = LogManager.GetLogger("frm_main");

        #region LINH TINH
        private void CanhGiuaPanelDangNhap(Control ctrlToCenter)  //canh giữa
        {
            ctrlToCenter.Left = (ctrlToCenter.Parent.Width - ctrlToCenter.Width) / 2;
            ctrlToCenter.Top = (ctrlToCenter.Parent.Height - ctrlToCenter.Height) / 2;
        }
        #endregion
        public frm_main() {
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();
            // canh giữa và hiển thị panel đăng nhập
            CanhGiuaPanelDangNhap(panelDangNhap);
            panelDangNhap.Visible = true;
        }
        // nếu bung form main thì canh giữa lại panel đăng nhập
        private void frm_main_SizeChanged(object sender, EventArgs e) {
            CanhGiuaPanelDangNhap(panelDangNhap);
        }

        private void frm_main_Load(object sender, EventArgs e) {

            string tmpConnStr = KiemtraDocFileKetnoiDL(Resources.ConnectionStringPath);
            if (tmpConnStr == string.Empty) {
                frm_KetNoiCSDL frmKetNoiCsdl = new frm_KetNoiCSDL();
                if (frmKetNoiCsdl.ShowDialog() == DialogResult.Yes)
                    SqlDataAccessHelper.ConnectionString = frmKetNoiCsdl.fConnectionString;
                else return;
            }
            else {
                bool IsConnect = SqlDataAccessHelper.TestConnection(tmpConnStr);
                if (IsConnect) {
	                SqlDataAccessHelper.ConnectionString = tmpConnStr;
					//btnLogin_Click(btnLogin, null);//[TBD] nhớ bỏ khi release
                }
                else
                    MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại sau.", "Lỗi");
            }

        }

        private string KiemtraDocFileKetnoiDL(string FileName) {
            string kq = string.Empty;
            // Open the file into a StreamReader
            try {
                StreamReader file = File.OpenText(FileName);
                string s = file.ReadToEnd();
                kq = MyUtility.giaima(s);

                file.Close();
            } catch (Exception ex) {
                if (ex is FileNotFoundException || ex is DirectoryNotFoundException)
                    MessageBox.Show("Không tìm thấy file kết nối CSDL.", Resources.MessBoxTitle_ThongBao, MessageBoxButtons.OK);
                else if (ex is NotSupportedException || ex is PathTooLongException)
                    MessageBox.Show("File kết nối CSDL bị lỗi.", Resources.MessBoxTitle_ThongBao, MessageBoxButtons.OK);
                else if (ex is UnauthorizedAccessException)
                    MessageBox.Show("Không có quyền truy cập file kết nối CSDL.", Resources.MessBoxTitle_ThongBao, MessageBoxButtons.OK);
                else
                    MessageBox.Show("Không thể kết nối CSDL. \nLỗi:\n" + ex.Message, Resources.MessBoxTitle_ThongBao, MessageBoxButtons.OK);

                return string.Empty;
            }
            return kq;
        }

        /// <summary>
        /// phân quyền
        /// </summary>
        /// <param name="pAccountType">0: root, 1: account trong CSDL</param>
        private void PhanQuyenMenu(int pAccountType) {
            // tài khoản root thì xem nếu chưa tồn thì cho phép tạo tài khoản, còn các menu khác đều ẩn
            if (pAccountType == 0) {
                MenuAdmin.Visible = MenuAdmin.Enabled = true;
                MenuAdminSub_TaoAccount.Visible = MenuAdminSub_TaoAccount.Enabled = true;

                MenuDuLieu.Visible = MenuDuLieu.Enabled = true;
                SubMenu_ChonDL.Enabled = true;

                return;
            }
            // account thường

            DataTable dt = SqlDataAccessHelper.ExecuteQueryString(ThamSo.queryPhanQuyenMenu, new[] { "@UserID" }, new object[] { ThamSo.currUserID });
            for (int i = 0; i < dt.Rows.Count; i++) {
                bool enable = (bool)dt.Rows[i]["IsYes"];
                switch ((int)dt.Rows[i]["MenuID"]) {
                    case 1009:
                        MenuDuLieu.Enabled = enable;
                        SubMenu_ChonDL.Enabled = enable;
                        break;
                    case 4001:
                        MenuChamCong.Enabled = enable;
                        SubMenu_XemCongNV.Enabled = enable;
                        SubMenu_DiemDanh.Enabled = enable;
                        SubMenu_KhaiBaoVang.Enabled = enable;
                        MenuHoatDong.Enabled = enable;
                        SubMenu_SuaGioHangLoat.Enabled = enable;
                        SubMenu_xemHistory.Enabled = enable;
                        break;
                    case 5001:
                        if (enable) {
                            MenuTaiKhoan.Enabled = true;
                            SubMenu_TaoTK.Enabled = true;
                        }
                        else SubMenu_TaoTK.Enabled = false;
                        break;
                    case 5002:
                        if (enable) {
                            MenuTaiKhoan.Enabled = true;
                            SubMenu_DoiMK.Enabled = enable;
                        }
                        else SubMenu_DoiMK.Enabled = false;
                        break;
                    default:
                        break;
                }
            }

        }

        private void btnLogin_Click(object sender, EventArgs e) {
            #region lay du lieu tu form
            string tempUsername = tb_UserName.Text, tempPassword = tb_Password.Text;

            string passEncrypt = MyUtility.Mahoa(tb_Password.Text);

            string passdefault = DateTime.Now.Minute + "@" + DateTime.Now.Hour + "@" + DateTime.Now.Month + "@" + DateTime.Now.Day;
            #endregion

            if (tempUsername == Resources.rootAccount && tempPassword == passdefault) {
                ThamSo.currUserID = int.Parse(Resources.rootUserID);
                ThamSo.currUserAccount = Resources.rootAccount;
                panelDangNhap.Visible = false;
                PhanQuyenMenu(0);
            }
            else {

                string tmpConnStr = KiemtraDocFileKetnoiDL(Resources.ConnectionStringPath);

                if (string.IsNullOrEmpty(tmpConnStr)) { // đọc file bị lỗi
                    frm_KetNoiCSDL frmKetNoiCsdl = new frm_KetNoiCSDL();
                    if (frmKetNoiCsdl.ShowDialog() == DialogResult.Yes)
                        SqlDataAccessHelper.ConnectionString = frmKetNoiCsdl.fConnectionString;
                    else return;
                }
                else {
                    // đọc được file, test kết nối với CSDL

                    SqlConnection connection = new SqlConnection(tmpConnStr);
                    try {
                        connection.Open();
                    } catch (Exception) {
                        connection.Close();
                        AutoClosingMessageBox.Show("Mất kết nối đến Máy chủ. Vui lòng thử lại.", "Lỗi", 2000);
                        return;
                    }
                    // ra khỏi try catch nghĩa là mở kết nối thành công, nên đóng lại và gán chuỗi kết nối luôn
                    connection.Close();
                    SqlDataAccessHelper.ConnectionString = tmpConnStr;

                    try {

                        DataTable dt = SqlDataAccessHelper.ExecuteQueryString(ThamSo.queryLogIn,
                            new[] { "@UserAccount", "@Password" },
                            new object[] { tb_UserName.Text, passEncrypt });

                        if (dt.Rows.Count != 0) { // tài khoản thường -> 

                            ThamSo.currUserID = (int)dt.Rows[0]["UserID"];
                            ThamSo.currUserAccount = dt.Rows[0]["UserAccount"].ToString();
                            panelDangNhap.Visible = false;

                            PhanQuyenMenu(1);

                            ChuanBiDuLieu();
							//subMenuChamCongTay_Click(subMenuChamCongTay, null); //[TBD] nhớ bỏ khi release
                        }
                        else if (MessageBox.Show("Tài khoản hoặc mật khẩu chưa đúng. Vui lòng điền lại.", Resources.MessBoxTitle_ThongBao, MessageBoxButtons.OK) == DialogResult.OK) {
                            tb_UserName.Text = tb_Password.Text = string.Empty;
                        }

                    } catch (Exception) {
                        AutoClosingMessageBox.Show("Mất kết nối đến Máy chủ hoặc CSDL không đúng. Vui lòng thử lại.", "Lỗi", 2000);
                    }

                }
            }

        }

        #region chuẩn bị dữ liệu : DS tất cả các Ca làm việc, tất cả các Lịch trình, tất cả phòng ban được thao tác và tất cả nhân viên được phép thao tác
        private void ChuanBiDuLieu() {
            ChuanBiDSCaLamViec();
            ChuanBiDSLichTrinh();
            ChuanBiDataTablePhongBan();
            ChuanBiDataTableDSNhanVien();
        }

        private void ChuanBiDSCaLamViec() {
            if (ThamSo.DSCa == null) ThamSo.DSCa = new List<cShift>();
            else ThamSo.DSCa.Clear();

            //ko cần try catch ở đây
            DataTable dt = SqlDataAccessHelper.ExecuteQueryString(ThamSo.queryDSCa, null, null);
            if (dt.Rows.Count == 0) return;
            foreach (DataRow row in dt.Rows) {
                int iShiftID = (int)row["ShiftID"];
                string sShiftCode = row["ShiftCode"].ToString();

                TimeSpan tsOnDuty;
                TimeSpan.TryParse(row["Onduty"].ToString(), out tsOnDuty);
                TimeSpan tOnTimeIn = tsOnDuty.Subtract(new TimeSpan(0, (int)row["OnTimeIn"], 0));
                TimeSpan tCutIn = tsOnDuty.Add(new TimeSpan(0, (int)row["CutIn"], 0));

                int iDayCount = (int)row["DayCount"];
                int iShowPosition = (int)row["ShowPosition"];
                TimeSpan tOffDuty;
                TimeSpan.TryParse(row["Offduty"].ToString(), out tOffDuty);

                tOffDuty = tOffDuty.Add(new TimeSpan(iDayCount, 0, 0, 0));
                TimeSpan tOnTimeOut = tOffDuty.Subtract(new TimeSpan(0, (int)row["OnTimeOut"], 0));
                TimeSpan tCutOut = tOffDuty.Add(new TimeSpan(0, (int)row["CutOut"], 0));

                TimeSpan tAfterOT = new TimeSpan(0, (int)row["AfterOT"], 0);
                TimeSpan tLateGrace = new TimeSpan(0, (int)row["LateGrace"], 0);
                TimeSpan tEarlyGrace = new TimeSpan(0, (int)row["EarlyGrace"], 0);

                TimeSpan tOnLunch = ThamSo._0gio, tOffLunch = ThamSo._0gio;
                if (row["OnLunch"] != DBNull.Value && row["OffLunch"] != DBNull.Value) {
                    TimeSpan.TryParse(row["OnLunch"].ToString(), out tOnLunch);
                    TimeSpan.TryParse(row["OffLunch"].ToString(), out tOffLunch);
                }

                int tempWorkingTime = int.Parse(row["WorkingTime"].ToString());
                cShift tempShift = new cShift() {
                    LoaiCa = 0,
                    ShiftID = iShiftID, ShiftCode = sShiftCode,
                    DayCount = iDayCount, QuaDem = (iDayCount == 1),
                    OnnDutyTS = tsOnDuty, OffDutyTS = tOffDuty,
                    OnTimeInTS = tOnTimeIn, CutInTS = tCutIn, OnTimeOutTS = tOnTimeOut, CutOutTS = tCutOut,
                    AfterOTTS = tAfterOT,
                    LateGraceTS = tLateGrace, EarlyGraceTS = tEarlyGrace,
                    Workingday = Convert.ToSingle(row["Workingday"].ToString()),
                    ShowPosition = iShowPosition,
                    WorkingTimeTS = new TimeSpan(0, tempWorkingTime, 0),
                    chophepvaotreTS = tsOnDuty.Add(tLateGrace),
                    chopheprasomTS = tOffDuty.Subtract(tEarlyGrace),
                    batdaulamthemTS = tOffDuty.Add(ThamSo._30phut),//[TBD] tempOffDuty + tempAfterOT,
                    LunchMinute = tOffLunch.Subtract(tOnLunch)
                };
                ThamSo.DSCa.Add(tempShift);
            }

        }

        private void ChuanBiDSLichTrinh() {
            if (ThamSo.DSLichTrinh == null) ThamSo.DSLichTrinh = new List<cShiftSchedule>();
            else ThamSo.DSLichTrinh.Clear();
            List<cShift> tmpDSCa = ThamSo.DSCa;

            DataTable table = SqlDataAccessHelper.ExecuteQueryString(ThamSo.queryDSLichTrinh, null, null);
            int tempScheduleID = -1;
            foreach (DataRow dataRow in table.Rows) {
                if (tempScheduleID == -1)
                    tempScheduleID = (int)dataRow["SchID"];
                else {
                    if (tempScheduleID == (int)dataRow["SchID"]) continue;
                    else tempScheduleID = (int)dataRow["SchID"];
                }

                //= (int)dataRow["SchID"]; // tempT1, tempT2, tempT3, tempT4, tempT5, tempT6, tempT7;
                DataRow[] arrSubRecord = table.Select("SchID = " + tempScheduleID, string.Empty, DataViewRowState.CurrentRows);
                cShiftSchedule tmpLichTrinh = new cShiftSchedule() { SchID = tempScheduleID };
                tmpLichTrinh.ListT1 = new List<cShift>();
                tmpLichTrinh.ListT2 = new List<cShift>();
                tmpLichTrinh.ListT3 = new List<cShift>();
                tmpLichTrinh.ListT4 = new List<cShift>();
                tmpLichTrinh.ListT5 = new List<cShift>();
                tmpLichTrinh.ListT6 = new List<cShift>();
                tmpLichTrinh.ListT7 = new List<cShift>();
                foreach (DataRow subRecord in arrSubRecord) {
                    int tempTChungCho1Hang = (int)subRecord["T1"]; // tempT1, tempT2, tempT3, tempT4, tempT5, tempT6, tempT7;
                    cShift tempShift = tmpDSCa.Find(item => item.ShiftID == tempTChungCho1Hang);

                    tmpLichTrinh.ListT1.Add(tempShift);
                    tmpLichTrinh.ListT2.Add(tempShift);
                    tmpLichTrinh.ListT3.Add(tempShift);
                    tmpLichTrinh.ListT4.Add(tempShift);
                    tmpLichTrinh.ListT5.Add(tempShift);
                    tmpLichTrinh.ListT6.Add(tempShift);
                    tmpLichTrinh.ListT7.Add(tempShift);

                }
                ThamSo.DSLichTrinh.Add(tmpLichTrinh);
            }
        }

        private void ChuanBiDataTablePhongBan() {
            ThamSo.TablePhongBan = SqlDataAccessHelper.ExecuteQueryString(ThamSo.queryDSPhongBan,
                new[] { "@UserID", "@IsYes" }, new object[] { ThamSo.currUserID, 1 });
        }

        private void ChuanBiDataTableDSNhanVien() {
            ThamSo.DataTableDSNV = SqlDataAccessHelper.ExecuteQueryString(ThamSo.queryDSNVThaoTac,
                new[] { "@UserID", "@IsYes" }, new object[] { ThamSo.currUserID, 1 });
        }
        #endregion

        private void btnThoat_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        /// <summary>
        /// TÀI KHOẢN ROOT tạo Account cho phần mềm chấm công từ account wiseeye
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuAdminSub_TaoAccount_Click(object sender, EventArgs e) {
            frmTaoTaiKhoan frmTaoTaiKhoan = new frmTaoTaiKhoan();
            frmTaoTaiKhoan.ShowDialog();
        }

        /// <summary>
        /// TÀI KHOẢN PMCC tạo Account cho phần mềm chấm công từ account wiseeye
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuHoatDongSub_TaoTK_Click(object sender, EventArgs e) {
            frmTaoTaiKhoan frmTaoTaiKhoan = new frmTaoTaiKhoan();
            frmTaoTaiKhoan.ShowDialog();
        }


        private void MenuDuLieuSub_ChonDL_Click(object sender, EventArgs e) {
            frm_KetNoiCSDL frmKetNoiCsdl = new frm_KetNoiCSDL();
            frmKetNoiCsdl.ShowDialog();
        }

        private void MenuHoatDongSub_DoiMK_Click(object sender, EventArgs e) {
            frm_DoiMatKhau frmDoiMatKhau = new frm_DoiMatKhau { CurrentAccount = ThamSo.currUserAccount };
            frmDoiMatKhau.ShowDialog();
        }

        /// <summary>
        /// ĐÓNG all các cửa sổ nếu đang mở ít nhất 1 cửa sổ VÀ RESET LẠI BIẾN TOÀN CỤC. LOGOUT nếu ko có cửa sổ nào đang mở
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// [TBD]
        private void MenuThoat_Click(object sender, EventArgs e) {
            panelDangNhap.Visible = true;
            MenuAdmin.Enabled = MenuDuLieu.Enabled = MenuHoatDong.Enabled = MenuChamCong.Enabled = MenuTaiKhoan.Enabled = false;
            MenuAdmin.Visible = false;
            tb_Password.Text = string.Empty;
            if (ActiveMdiChild == null) {
                this.Close();
            }
            while (ActiveMdiChild != null) {
                ActiveMdiChild.Close();
            }
        }


        private void SuaGioHangLoatToolStripMenuItem_Click(object sender, EventArgs e) {
            frm_SuaGioHangLoat2 frm = new frm_SuaGioHangLoat2() { MdiParent = this };
            frm.Show();
        }



        //[TBD]         kiểm lại các menu tạo form khác
        private void testToolStripMenuItem1_Click(object sender, EventArgs e) {
            frm_KhaiBaoVang frm = new frm_KhaiBaoVang() { MdiParent = this };
            //frm.WindowState = FormWindowState.Maximized;
            frm.Show();
            frm_KhaiBaoVang frm1 = new frm_KhaiBaoVang();
            int indexForm = LayVitriForm(this, frm1.GetType());
            if (indexForm != -1) {
                frm1 = this.MdiChildren[indexForm] as frm_KhaiBaoVang;
                frm1.BringToFront();
            }
            else {
                frm1.MdiParent = this;
                frm1.Show();
            }
        }

        private void xemHistoryToolStripMenuItem_Click(object sender, EventArgs e) {
            frm_XemLichSu frm1 = new frm_XemLichSu();
            int indexForm = LayVitriForm(this, frm1.GetType());
            if (indexForm != -1) {
                frm1 = this.MdiChildren[indexForm] as frm_XemLichSu;
                frm1.BringToFront();
            }
            else {
                frm1.MdiParent = this;
                frm1.WindowState = FormWindowState.Maximized;
                frm1.Show();
            }
            frm_XemLichSu frm = new frm_XemLichSu() { MdiParent = this };
            frm.Location = new Point(0, 0);
            frm.Show();
        }

        private void MenuBaoBieuSub_XemCongNV_Click(object sender, EventArgs e) {

            frm_XemCong frm1 = new frm_XemCong();
            int indexForm = LayVitriForm(this, frm1.GetType());
            if (indexForm != -1) {
                frm1 = this.MdiChildren[indexForm] as frm_XemCong;
                frm1.BringToFront();
            }
            else {
                frm1.MdiParent = this;
                frm1.WindowState = FormWindowState.Maximized;
                frm1.Show();
            }
        }

        private void diemdanhToolStripMenuItem_Click(object sender, EventArgs e) {
            frm_DiemDanhNV frm = new frm_DiemDanhNV();
            frm.Show();
        }

        int LayVitriForm(Form mainForm, Type childType) {
            if (mainForm.MdiChildren.Length == 0) return -1;
            for (int i = 0; i < mainForm.MdiChildren.Length; i++) {
                if (mainForm.MdiChildren[i].GetType() == childType)
                    return i;
            }
            return -1;
        }

        #region nếu bị disable thì disable hết. còn enable thì tùy phần phân quyền xử lý

        private void MenuAdmin_EnabledChanged(object sender, EventArgs e) {
            if (MenuAdmin.Enabled == false)
                foreach (ToolStripItem item in MenuAdmin.DropDownItems)
                    item.Enabled = false;

        }

        private void MenuTaiKhoan_EnabledChanged(object sender, EventArgs e) {
            if (MenuTaiKhoan.Enabled == false)
                foreach (ToolStripItem item in MenuTaiKhoan.DropDownItems)
                    item.Enabled = false;

        }

        private void MenuDuLieu_EnabledChanged(object sender, EventArgs e) {
            if (MenuDuLieu.Enabled == false)
                foreach (ToolStripItem item in MenuDuLieu.DropDownItems)
                    item.Enabled = false;

        }

        private void MenuChamCong_EnabledChanged(object sender, EventArgs e) {
            if (MenuChamCong.Enabled == false)
                foreach (ToolStripItem item in MenuChamCong.DropDownItems)
                    item.Enabled = false;

        }

        private void MenuHoatDong_EnabledChanged(object sender, EventArgs e) {
            if (MenuHoatDong.Enabled == false)
                foreach (ToolStripItem item in MenuHoatDong.DropDownItems)
                    item.Enabled = false;

        }
        #endregion

		private void testToolStripMenuItem_Click(object sender, EventArgs e) {

		}

		private void subMenuTinhLuong_Click(object sender, EventArgs e) {
			frm_TinhLuongNV frm1 = new frm_TinhLuongNV();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_TinhLuongNV;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.StartPosition = FormStartPosition.CenterParent; 
				frm1.Show();
			}
		}

		private void subMenuKhaiBaoLVNgayNghi_Click(object sender, EventArgs e) {
			frm_KhaiBaoLamViecNgayNghi frm1 = new frm_KhaiBaoLamViecNgayNghi();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_KhaiBaoLamViecNgayNghi;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.StartPosition = FormStartPosition.CenterParent;
				frm1.Show();
			}
		}

		private void subMenuChamCongTay_Click(object sender, EventArgs e) {
			frm_ChamCongTay frm1 = new frm_ChamCongTay();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_ChamCongTay;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.StartPosition = FormStartPosition.CenterParent;
				frm1.Show();
			}
		}




    }
}
