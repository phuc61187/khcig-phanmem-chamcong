using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GiuLaiCacFileCu.DAO;

namespace GiuLaiCacFileCu {
    public partial class z_old_v12_frm_ThemGioHangLoat : Form {
        private int _mCurrentUserID = -1;
        private bool checkAllDSNV = false;
        private bool hasCheckDSNV = false;
        /*
                private List<cUserInfo> _dsNVchecked;
                private List<cUserInfo> _dsNVunchecked;
        */
        private DateTime _gioBatDau = DateTime.MinValue;
        private DateTime _gioKetThuc =DateTime.MinValue;
        


        //[BackupFunction02]
        //[BackupFunction03]

        private void GanGiaTri(DataTable pDataTableOriginal) { throw new NotImplementedException(); }
        /*
                        int ma_moi = -1;
                        int may_cham = 0;
                        DateTime thoigian_check;
                        cUserInfo nhanvien;

                        // 1. duyệt qua từng dòng dữ liệu
                        foreach (DataRow dr in pDataTableOriginal.Rows) {
                            ma_moi = (int)dr["UserEnrollNumber"];
                            may_cham = (int)dr["MachineNo"];
                            thoigian_check = (DateTime)dr["TimeStr"];
                            // 1.1 tìm nhân viên tương ứng trong ds nhân viên đã chọn xem giờ công
                            nhanvien = _dsNVchecked.Find(item => item.UserEnrollNumber == ma_moi);
                            cChk temp = new cChk(ma_moi
                                                                    , thoigian_check.Date
                                                                    , thoigian_check
                                                                    , (dr["OriginType"] != DBNull.Value) ? dr["OriginType"].ToString() : null
                                                                    , (dr["NewType"] != DBNull.Value) ? dr["NewType"].ToString() : null
                                                                    , (dr["Source"] != DBNull.Value) ? dr["Source"].ToString() : null
                                                                    , may_cham
                                                                    , 0);       
                            // 1.2 thêm chuỗi giờ check in
            /*                if (may_cham % 2 == 1) {
                                if (nhanvien.DsGioVao == null) {
                                    nhanvien.DsGioVao = new List<clsCheckInOut>();
                                    nhanvien.DsGioVao.Add(temp);
                                    nhanvien.GioVaoSauCung = temp;
                                }
                                else {
                                    nhanvien.DsGioVao.Add(temp);
                                    nhanvien.GioVaoSauCung = nhanvien.TimGioVaoSauCung();
                                }
                            }
                            else // thêm chuỗi giờ check out
                            {
                                if (nhanvien.DsGioRa == null) {
                                    nhanvien.DsGioRa = new List<clsCheckInOut>();
                                    nhanvien.DsGioRa.Add(temp);
                                    nhanvien.GioRaSauCung = temp;
                                }
                                else {
                                    nhanvien.DsGioRa.Add(temp);
                                    nhanvien.GioRaSauCung = nhanvien.TimGioRaSauCung();
                                }
                            }#2#
                        }
            #1#*/


        public z_old_v12_frm_ThemGioHangLoat() {
            InitializeComponent();
        }

        private void ThemGioChamCong_Load(object sender, EventArgs e) {
            throw new NotImplementedException();

        }
        void DSNV_OnGetUserIDFromFormDSNV(int pUserID) {
            _mCurrentUserID = pUserID;
        }


        private void dataGridTongHop_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            try {
                if (e.ColumnIndex == 0) {
                    checkAllDSNV = !checkAllDSNV;
                    hasCheckDSNV = checkAllDSNV;
                    foreach (DataGridViewRow dgvr in dataGridTongHop.Rows) {
                        dgvr.Cells[0].Value = checkAllDSNV;
                    }
                    dataGridTongHop.EndEdit();
                }
			} catch (Exception ex) { MessageBox.Show("Có lỗi trong quá trình thực hiện. Vui lòng liên hệ phòng kỹ thuật để được trợ giúp.\nLỗi:\n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK); }
        }

        private void checkBoxThemGio_CheckedChanged(object sender, EventArgs e) {
            dateTimeThemGioVao.Enabled = checkBoxThemGioVao.Checked;
            dateTimeThemGioRa.Enabled = checkBoxThemGioRa.Checked;
        }

        private void buttonThucHien_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
            //[BackupFunction04]
        }

    }
}

