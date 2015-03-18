using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CapNhatGioChamCong
{
    public partial class ThemGioChamCong : Form
    {
        private int _mCurrentUserID = -1;
        private List<clsUserInfo> _dsNVchecked;
        private List<clsUserInfo> _dsNVunchecked;
        private List<clsUserInfo> _dsNVThemGioVao_Checked;
        private List<clsUserInfo> _dsNVThemGioVao_Unchecked;
        private List<clsUserInfo> _dsNVThemGioRa_Checked;
        private List<clsUserInfo> _dsNVThemGioRa_Unchecked;
        private DateTime _gioBatDau;
        private DateTime _gioKetThuc;
        private DataTable mDataTableOriginal;


        string SelectStringDSGioVaoRa()
        {
            List<string> ds = new List<string>();
            foreach (clsUserInfo nhanvien in _dsNVchecked)
            {
                ds.Add(nhanvien.UserEnrollNumber.ToString());
            }

            string selectQueryString = @" SELECT BackupCheckInOut.UserEnrollNumber,UserInfo.UserFullName,TimeDate,TimeStr,OriginType,NewType,Source,MachineNo,WorkCode ";
            selectQueryString += @" from BackupCheckInOut, UserInfo ";
            selectQueryString += @" where (TimeStr between @ThoiGianBatDau and @ThoiGianKetThuc) and (UserInfo.UserEnrollNumber = BackupCheckInOut.UserEnrollNumber)
		                    and (BackupCheckInOut.UserEnrollNumber = {0} ";
            selectQueryString = String.Format(selectQueryString, String.Join(" or BackupCheckInOut.UserEnrollNumber = ", ds.ToArray()));
            selectQueryString += ")";
            selectQueryString += " group by BackupCheckInOut.UserEnrollNumber,TimeStr,TimeDate,MachineNo,UserInfo.UserFullName,OriginType,NewType,Source,WorkCode ";
            selectQueryString += " order by TimeStr asc";
            return selectQueryString;
        }

        //[TBD]
        string InsertStringThemGioVaoRa() {
            //[TBD]
            string kq = @"insert into BackupCheckInOut (
                                UserEnrollNumber
                               ,TimeDate
                               ,TimeStr
                               ,OriginType
                               ,NewType
                               ,Source
                               ,MachineNo
                               ,WorkCode
                        ) value (";
            kq += @"            @UserEnrollNumber
                               ,@TimeDate
                               ,@TimeStr
                               ,@OriginType
                               ,@NewType
                               ,@Source
                               ,@MachineNo
                               ,@WorkCode";
            kq += @")";
            return kq;
        }

        //[TBD]
        string InsertStringBackupThemGioVaoRa() {
            string kq = @"INSERT INTO dbo.LichSuSuaGioVaoRa
                               (UserEnrollNumber
                               ,TimeDate
                               ,TimeStrOld
                               ,TimeStrNew
                               ,OriginType
                               ,NewType
                               ,Source
                               ,MachineNo
                               ,WorkCode
                               ,UserID
                               ,Explain
                               ,Note
                               ,CommandType
                               ,ExecuteTime
                          ) VALUES (";
            kq += @"            @UserEnrollNumber
                               ,@TimeDate
                               ,@TimeStrOld
                               ,@TimeStrNew
                               ,@OriginType
                               ,@NewType
                               ,@Source
                               ,@MachineNo
                               ,@WorkCode
                               ,@UserID
                               ,@Explain
                               ,@Note
                               ,@CommandType
                               ,GetDate()"; //lấy giờ của sql server chứ ko lấy giờ client
            kq += @")";
            return kq;
        }


        public ThemGioChamCong()
        {
            InitializeComponent();
            DSNV.OnHienThiThemGio += new DSNV.handler1(DSNV_OnHienThiThemGio);
            dwf_raiseEventForm.OnLogInSuccess += new dwf_raiseEventForm.handler(dwf_raiseEventForm_OnLogInSuccess);
        }

        void dwf_raiseEventForm_OnLogInSuccess(int pUserID)
        {
            _mCurrentUserID = pUserID;
        }

        void DSNV_OnHienThiThemGio(List<clsUserInfo> pDSNVchecked, List<clsUserInfo> pDSNVunchecked, DateTime pGioBatDau, DateTime pGioKetThuc)
        {
            _dsNVchecked = pDSNVchecked;
            _dsNVunchecked = pDSNVunchecked;
            _gioBatDau = pGioBatDau;
            _gioKetThuc = pGioKetThuc;
        }

        private void checkBoxThemGio_CheckedChanged(object sender, EventArgs e)
        {
            dateTimeGioVao.Enabled = checkBoxThemGioVao.Checked;
            dateTimeGioRa.Enabled = checkBoxThemGioRa.Checked;
        }

        private void dateTimeGioRa_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ThemGioChamCong_Load(object sender, EventArgs e)
        {
            mDataTableOriginal = SqlDataAccessHelper.ExecuteQueryString(SelectStringDSGioVaoRa()
                                                                        , new string[] { "@ThoiGianBatDau", "@ThoiGianKetThuc" }
                                                                        , new object[] { _gioBatDau, _gioKetThuc });
            DataView dtv1 = new DataView(mDataTableOriginal, "MachineNo%2 = 1", string.Empty, DataViewRowState.CurrentRows);
            DataView dtv2 = new DataView(mDataTableOriginal, "MachineNo%2 = 0", string.Empty, DataViewRowState.CurrentRows);
            dataGridView1.DataSource = dtv1;
            dataGridView2.DataSource = dtv2;
            GanGiaTri(mDataTableOriginal);
        }

        private void GanGiaTri(DataTable pDataTableOriginal)
        {
            int ma_cu = -1;
            int ma_moi = -1;
            int may_cham = 0;
            DateTime thoigian_check;
            clsUserInfo nhanvien;
            foreach (DataRow dr in pDataTableOriginal.Rows)
            {
                ma_moi = (int)dr["UserEnrollNumber"];
                may_cham = (int)dr["MachineNo"];
                nhanvien = _dsNVchecked.Find(item => item.UserEnrollNumber == ma_moi);
                thoigian_check = (DateTime)dr["TimeStr"];
                // thêm giờ vào máy vào
                if (may_cham % 2 == 1)
                {
                    if (nhanvien.DSGioVao == null)
                    {
                        nhanvien.DSGioVao = new List<DateTime>();
                        nhanvien.GioVaoSauCung = thoigian_check;
                    }
                    else if (thoigian_check >= nhanvien.GioVaoSauCung)
                    {
                        nhanvien.GioVaoSauCung = thoigian_check;
                    }
                    nhanvien.DSGioVao.Add(thoigian_check);
                    
                }
                else // thêm giờ vào máy ra
                {
                    if (nhanvien.DSGioRa == null)
                    {
                        nhanvien.DSGioRa = new List<DateTime>();
                        nhanvien.GioRaSauCung = thoigian_check;
                    }
                    else if (thoigian_check >= nhanvien.GioRaSauCung)
                    {
                        nhanvien.GioRaSauCung = thoigian_check;
                    }
                    nhanvien.DSGioRa.Add(thoigian_check);
              
                }
            }
            foreach (clsUserInfo nv in _dsNVchecked)
            {
                if ((nv.DSGioRa != null && nv.DSGioRa.Count != 0) || (nv.DSGioVao != null && nv.DSGioVao.Count != 0))
                    dataGridView3.Rows.Add(
                        false
                        , (nv.DSGioVao != null) ? nv.GioVaoSauCung.ToString() : string.Empty
                        , (nv.DSGioRa != null) ? nv.GioRaSauCung.ToString() : string.Empty
                        , nv.UserEnrollNumber
                        , nv.UserFullName );
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int kq_insertGioVao = 0, kq_insertLichSuSuaGioVao = 0;
            int kq_insertGioRa = 0, kq_insertLichSuSuaGioRa = 0;

            if (checkBoxThemGioVao.Checked) { 
                //kiểm tra có chọn ai thêm giờ vào không
                foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                {
                    if ((bool)dgvr.Cells[0].FormattedValue == true)
                    {
                        _dsNVThemGioVao_Checked.Add(_dsNVchecked.Find(item => item.UserEnrollNumber == (int)(dgvr.Cells[1].FormattedValue)));
                    }
                    else { 
                        _dsNVThemGioVao_Unchecked.Add(_dsNVchecked.Find(item => item.UserEnrollNumber == (int)(dgvr.Cells[1].FormattedValue)));
                    }
                }

                foreach (clsUserInfo nhanvien in _dsNVThemGioVao_Checked)
                {
                    kq_insertGioVao = SqlDataAccessHelper.ExecuteNoneQueryString(
                        InsertStringThemGioVaoRa()
                        , new string[] { 
                                "@UserEnrollNumber"
                               ,"@TimeDate"
                               ,"@TimeStr"
                               ,"@OriginType"
                               ,"@NewType"
                               ,"@Source"
                               ,"@MachineNo"
                               ,"@WorkCode"}
                        , new object[] { 
                                nhanvien.UserEnrollNumber
                               , DateTime.Parse(dateTimeGioVao.Value.ToShortDateString())
                               , dateTimeGioVao.Value
                               , "I" // in
                               , DBNull.Value
                               , DBNull.Value
                               , 1 //mặc định thêm giờ vào là máy 1
                               , 0 //[TBD] giá trị này chưa biết nên để là 0
                        });
                    kq_insertLichSuSuaGioVao = SqlDataAccessHelper.ExecuteNoneQueryString(
                        InsertStringBackupThemGioVaoRa()
                        , new string[] { 
                                "@UserEnrollNumber"
                               ,"@TimeDate"
                               ,"@TimeStrOld"
                               ,"@TimeStrNew"
                               ,"@OriginType"
                               ,"@NewType"
                               ,"@Source"
                               ,"@MachineNo"
                               ,"@WorkCode"
                               ,"@UserID"
                               ,"@Explain"
                               ,"@Note"
                               ,"@CommandType"}
                        , new object[] { });
                }
            }

            if (checkBoxThemGioRa.Checked) { 
                // kiểm tra có chọn ai thêm giờ ra ko?
            }
        }
    }
}
