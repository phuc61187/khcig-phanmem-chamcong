using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GiuLaiCacFileCu.DAO;

namespace GiuLaiCacFileCu {
    public partial class z_old_v11_frmTichluyBugio : Form {
        //public List<cUserInfo> DSNV;
        public bool ChkAlldataGridDSGTC;
        public bool ChkAlldataGridCTTreSom;

        private string InsertStringBackupThemGioVaoRa() {
            string kq = @"INSERT INTO LichSuSuaGioVaoRa(UserEnrollNumber
                               ,TimeDate,TimeStrOld,TimeStrNew
                               ,Source,MachineNo,WorkCode
                               ,UserID
                               ,Explain,Note
                               ,CommandType
                               ,ExecuteTime
                          ) VALUES (";
            kq += @"            @UserEnrollNumber
                               ,@TimeDate,@TimeStrOld,@TimeStrNew
                               ,@Source,@MachineNo,@WorkCode
                               ,@UserID
                               ,@Explain,@Note
                               ,@CommandType
                               ,GetDate()"; //lấy giờ của sql server chứ ko lấy giờ client
            kq += @")";
            return kq;
        }



        public z_old_v11_frmTichluyBugio() { InitializeComponent(); }

        private void LoadDataGridDSGTC() {
            throw new NotImplementedException();


            /*
                        foreach (cUserInfo nv in DSNV) {
                            foreach (clsNgayCong_v71 ngayCongV71 in nv.DSNgayCong_v71) {
                                for (int i = 0; i < ngayCongV71.DSRaHL_v71.Count; i++) {
                                    if (ngayCongV71.DSRaHL_v71[i].GioLamThem > new TimeSpan(0, 0, 0)) {
                                        DataGridViewRow dataGridViewRow = new DataGridViewRow();
                                        dataGridViewRow.CreateCells(dataGridDSGTC, new object[]
                                                                                       {
                                                                                           false
                                                                                           , nv.UserEnrollNumber
                                                                                           , nv.UserFullName
                                                                                           , ngayCongV71.NgayCong
                                                                                           , ngayCongV71.DSVaoHL_v71[i].TimeStr
                                                                                           , ngayCongV71.DSRaHL_v71[i].TimeStr
                                                                                           , ngayCongV71.DSVaoHL_v71[i].ThuocCa[0].ShiftCode
                                                                                           , ngayCongV71.DSRaHL_v71[i].GioLamThem
                                                                                       });
                                        dataGridDSGTC.Rows.Add(dataGridViewRow);
                                    }
                                }
                            }
                        }
            */

        }

        private void Form1_Load(object sender , EventArgs e) {
            throw new NotImplementedException();
            /*
                        if (DSNV == null || DSNV.Count == 0) return;
                        Parallel.ForEach(DSNV, info => info.ClearAll());
            */

        }


        private void TransferDataToObj_v3(DataTable pDataTableOriginal) {
            throw new NotImplementedException();

            /*
                       cUserInfo nhanvien = new cUserInfo();

                        // 1. duyệt qua từng dòng dữ liệu
                        foreach (DataRow dr in pDataTableOriginal.Rows) {
                            int tempEnrollNumber = (int)dr["UserEnrollNumber"];
                            int tempMachineNo = (int)dr["MachineNo"];
                            DateTime tempTimeStr = (DateTime)dr["TimeStr"];
                            // 1.1 tìm nhân viên tương ứng trong ds nhân viên đã chọn xem giờ công
                            if (nhanvien.UserEnrollNumber != tempEnrollNumber) {
                                nhanvien = DSNV.Find(item => item.UserEnrollNumber == tempEnrollNumber);
                            }
                            // giờ vào ra của cùng 1 người, xét có nằm trong khoảng 5ph hay ko, nếu trong 5ph thì add vào giờ liên quan, ko thì add giờ chính mới
                            cChk newObj = new cChk(tempEnrollNumber
                                                                     , tempTimeStr.Date
                                                                     , tempTimeStr
                                                                     , dr["OriginType"].ToString()
                                                                     , (dr["NewType"] != DBNull.Value)
                                                                         ? dr["NewType"].ToString()
                                                                         : string.Empty
                                                                     , dr["Source"].ToString()
                                                                     , tempMachineNo
                                                                     , (dr["WorkCode"] != DBNull.Value) ? (int)dr["WorkCode"] : -1);
                            //nhanvien.ThemGio_v3(newObj); 
                        }
            */
        }

        private void btnXem_Click(object sender , EventArgs e) {
            throw new NotImplementedException();

            
/*
                        if (DSNV == null || DSNV.Count == 0) return;
                        dateTimeThang.Update();
                        DateTime StartTime = new DateTime(dateTimeThang.Value.Year, dateTimeThang.Value.Month, 1);
                        DateTime EndTime = new DateTime(dateTimeThang.Value.Year, dateTimeThang.Value.Month, DateTime.DaysInMonth(dateTimeThang.Value.Year, dateTimeThang.Value.Month));
                        Parallel.ForEach(DSNV, info => info.ClearAll());
                        dataGridDSGTC.Rows.Clear();
                        dataGridCTTreSom.Rows.Clear();
                        LoadQuyGioChoNV();
                        mTableOrigin = SqlDataAccessHelper.ExecuteQueryString(SelStr_GetDSGioVaoRa()
                                                                              , new[] { "@BatDauVao", "@KetThucVao", "@BatDauRa", "@KetThucRa" }
                                                                              , new object[]
                                                                                    {
                                                                                        StartTime.Date.AddHours(4.5d)
                                                                                        , EndTime.Date.AddHours(23.5d)
                                                                                        , StartTime.Date.AddHours(7d)
                                                                                        , EndTime.Date.Add(new TimeSpan(1, 7, 0, 0))
                                                                                    });
                        TransferDataToObj_v3(mTableOrigin);

                        foreach (cUserInfo nv in DSNV) {
                            nv.XetGioHopLe_v10();

                            #region load vào datagridTongHop
                            if (nv.DSRaHopLe == null || nv.DSRaHopLe.Count == 0) continue;
                            for (int i = 0; i < nv.DSRaHopLe.Count; i++) {
                                if (nv.DSRaHopLe[i].GioLamThem > new TimeSpan(0, 0, 0)) {
                                    DataGridViewRow dataGridViewRow = new DataGridViewRow();
                                    dataGridViewRow.CreateCells(dataGridDSGTC, new object[]{ false
                                                                                           , nv.UserEnrollNumber
                                                                                           , nv.UserFullName
                                                                                           , nv.DSVaoHopLe[i].TimeDate
                                                                                           , nv.DSVaoHopLe[i].TimeStr
                                                                                           , nv.DSRaHopLe[i].TimeStr
                                                                                           , nv.DSVaoHopLe[i].ThuocCa[0].ShiftCode
                                                                                           , (int)Math.Floor(nv.DSRaHopLe[i].GioLamThem.TotalMinutes)
                                                                                       });
                                    dataGridDSGTC.Rows.Add(dataGridViewRow);
                                }

                                TimeSpan tempVaoTre = nv.DSVaoHopLe[i].VaoTre;
                                TimeSpan tempRaSom = nv.DSRaHopLe[i].RaaSom;
                                int tempintVaoTre = (int)Math.Floor(tempVaoTre.TotalMinutes);
                                int tempintRaSom = (int)Math.Floor(tempRaSom.TotalMinutes);
                                int tempintTong = tempintVaoTre + tempintRaSom;
                                if (tempintTong > 0) {
                                    DataGridViewRow dataGridViewRow = new DataGridViewRow();
                                    dataGridViewRow.CreateCells(dataGridCTTreSom, new object[]{false
                                                                                                , nv.UserEnrollNumber
                                                                                                , nv.UserFullName
                                                                                                , nv.DSVaoHopLe[i].TimeDate
                                                                                                , nv.DSVaoHopLe[i].TimeStr
                                                                                                , nv.DSRaHopLe[i].TimeStr
                                                                                                , tempintVaoTre
                                                                                                , tempintRaSom
                                                                                                , tempintTong
                                                                                                , nv.QuyGioTL.SumMinutes - nv.QuyGioTL.Used
                                                                                                , nv.QuyGioTL.Used
                                                                                                , nv.QuyGioTL.SumMinutes});
                                    dataGridCTTreSom.Rows.Add(dataGridViewRow);
                                }
                            }
                            #endregion

                        }


            
*/
                    }

                    private void btnTichLuy_Click(object sender, EventArgs e) {
                        throw new NotImplementedException();
/*
                        dataGridDSGTC.EndEdit();
                        //DataView dataView = new DataView(dataGridDSGTC.DataSource,"","", DataViewRowState.CurrentRows);
                        DataGridView temp = new DataGridView { ColumnCount = dataGridDSGTC.Columns.Count };

                        int j = 0;
                        for (int i = 0; i < dataGridDSGTC.Rows.Count; i++) {
                            if ((bool)dataGridDSGTC.Rows[i].Cells[0].Value) {
                                DataGridViewRow newrow = dataGridDSGTC.Rows[i].Clone() as DataGridViewRow;
                                for (int i1 = 0; i1 < newrow.Cells.Count; i1++) { newrow.Cells[i1].Value = dataGridDSGTC.Rows[i].Cells[i1].Value; }
                                temp.Rows.Add(newrow);
                            }
                        }
                        if (temp.Rows.Count == 1) return;
                        cUserInfo nhanvien = new cUserInfo();
                        int tempEnrollNumber = -1;
                        temp.Sort(temp.Columns[1], ListSortDirection.Ascending);
                        temp.EndEdit();
                        // Rows cuối cùng là Rows Header của dataGrid nên dòng for dưới chạy đến < count-1
                        for (int i = 0; i < temp.Rows.Count - 1; i++) {
                            if (tempEnrollNumber == -1) {
                                tempEnrollNumber = (int)temp.Rows[i].Cells[1].Value;
                                nhanvien = DSNV.Find(item => item.UserEnrollNumber == tempEnrollNumber);
                            }
                            tempEnrollNumber = (int)temp.Rows[i].Cells[1].Value;

                            if (tempEnrollNumber != nhanvien.UserEnrollNumber)
                                nhanvien = DSNV.Find(item => item.UserEnrollNumber == tempEnrollNumber);

                            // 1.update các giờ là đã duyệt
                            DateTime tempGioVao = (DateTime)temp.Rows[i].Cells[4].Value;
                            int kq = SqlDataAccessHelper.ExecNoneQueryString(" update CheckInOut " +
                                                                             " set Duyet=1 " +
                                                                             " where (TimeStr = @GioVao or TimeStr = @GioRa) " +
                                                                             " and UserEnrollNumber = @UserEnrollNumber"
                                                                             , new[] { "@UserEnrollNumber", "@GioVao", "@GioRa" }
                                                                             , new object[] { nhanvien.UserEnrollNumber, tempGioVao, temp.Rows[i].Cells[5].Value });
                            if (kq == 0) MessageBox.Show(" không thể update duyet");

                            int minutes = (int)temp.Rows[i].Cells[7].Value;
                            DateTime tempThang = new DateTime(tempGioVao.Year, tempGioVao.Month, 1);
                            int kq2 = SqlDataAccessHelper.ExecNoneQueryString(" update QuyGioTichLuy " +
                                                                              " set SumMinutes = SumMinutes + @SumMinutes " +
                                                                              " where UserEnrollNumber = @UserEnrollNumber " +
                                                                              " and Thang = @Thang " +
                                                                              " IF @@ROWCOUNT=0 INSERT INTO QuyGioTichLuy (UserEnrollNumber,Thang,SumMinutes,Used) " +
                                                                              " values (@UserEnrollNumber, @Thang, @SumMinutes, 0 )  "
                                                                              , new[] { "@UserEnrollNumber", "@Thang", "@SumMinutes" }
                                                                              , new object[] { nhanvien.UserEnrollNumber, tempThang, minutes });
                            if (kq2 == 0) MessageBox.Show(" không thể update tich luy");

                        }

                        btnXem_Click(null, null);
                        //dataGridGioBT.d
                        //dataGridGioBT.
*/
                    }

                    private void btnBoQua_Click(object sender, EventArgs e) {
                        throw new NotImplementedException();
/*
                        dataGridDSGTC.EndEdit();
                        DataGridView temp = new DataGridView { ColumnCount = dataGridDSGTC.Columns.Count };

                        int j = 0;
                        Parallel.For(0, dataGridDSGTC.Rows.Count, i => {
                            if ((bool)dataGridDSGTC.Rows[i].Cells[0].Value) {
                                DataGridViewRow newrow = dataGridDSGTC.Rows[i].Clone() as DataGridViewRow;
                                Parallel.For(0, newrow.Cells.Count, i1 => { newrow.Cells[i1].Value = dataGridDSGTC.Rows[i].Cells[i1].Value; });
                                temp.Rows.Add(newrow);
                            }
                        });
                        if (temp.Rows.Count == 1) return;
                        cUserInfo nhanvien = new cUserInfo();
                        int tempEnrollNumber = -1;
                        temp.Sort(temp.Columns[1], ListSortDirection.Ascending);
                        temp.EndEdit();
                        // Rows cuối cùng là Rows Header của dataGrid nên dòng for dưới chạy đến < count-1
                        for (int i = 0; i < temp.Rows.Count - 1; i++) {
                            if (tempEnrollNumber == -1) {
                                tempEnrollNumber = (int)temp.Rows[i].Cells[1].Value;
                                nhanvien = DSNV.Find(item => item.UserEnrollNumber == tempEnrollNumber);
                            }
                            tempEnrollNumber = (int)temp.Rows[i].Cells[1].Value;

                            if (tempEnrollNumber != nhanvien.UserEnrollNumber)
                                nhanvien = DSNV.Find(item => item.UserEnrollNumber == tempEnrollNumber);

                            // 1.update các giờ là đã duyệt
                            DateTime tempGioVao = (DateTime)temp.Rows[i].Cells[4].Value;
                            int kq = SqlDataAccessHelper.ExecNoneQueryString(" update CheckInOut " +
                                                                             " set Duyet=1 " +
                                                                             " where (TimeStr = @GioVao or TimeStr = @GioRa) " +
                                                                             " and UserEnrollNumber = @UserEnrollNumber"
                                                                             , new[] { "@UserEnrollNumber", "@GioVao", "@GioRa" }
                                                                             , new object[] { nhanvien.UserEnrollNumber, tempGioVao, temp.Rows[i].Cells[5].Value });
                            if (kq == 0) MessageBox.Show(" không thể update duyet");
                        }
                        btnXem_Click(null, null);
*/
                    }

                    private void btnBuGio_Click(object sender, EventArgs e) {
                        throw new NotImplementedException();
/*
                        dataGridCTTreSom.EndEdit();
                        DataGridView temp = new DataGridView() { ColumnCount = dataGridCTTreSom.Columns.Count };
                        for (int i = 0; i < dataGridCTTreSom.Rows.Count; i++) {
                            if ((bool)dataGridCTTreSom.Rows[i].Cells[0].Value) {
                                DataGridViewRow newrow = dataGridCTTreSom.Rows[i].Clone() as DataGridViewRow;
                                Parallel.For(0, newrow.Cells.Count, i1 => { newrow.Cells[i1].Value = dataGridCTTreSom.Rows[i].Cells[i1].Value; });
                                temp.Rows.Add(newrow);
                            }
                        }
                        if (temp.Rows.Count == 1) return;
                        cUserInfo nhanvien = new cUserInfo();
                        int tempEnrollNumber = -1;
                        temp.Sort(temp.Columns[1], ListSortDirection.Ascending);
                        temp.EndEdit();
                        // Rows cuối cùng là Rows Header của dataGrid nên dòng for dưới chạy đến < count-1
                        for (int i = 0; i < temp.Rows.Count - 1; i++) {
                            if (tempEnrollNumber == -1) {
                                tempEnrollNumber = (int)temp.Rows[i].Cells[1].Value;
                                nhanvien = DSNV.Find(item => item.UserEnrollNumber == tempEnrollNumber);
                            }
                            tempEnrollNumber = (int)temp.Rows[i].Cells[1].Value;

                            if (tempEnrollNumber != nhanvien.UserEnrollNumber)
                                nhanvien = DSNV.Find(item => item.UserEnrollNumber == tempEnrollNumber);

                            int tempQuyTon = nhanvien.QuyGioTL.SumMinutes - nhanvien.QuyGioTL.Used;
                            int tempTong = (int)temp.Rows[i].Cells[8].Value;
                            if (tempTong > tempQuyTon) continue;
                            int tempTre = (int)temp.Rows[i].Cells[6].Value;
                            if (tempTre > 0)
                            {
                                DateTime tempVaoOld = (DateTime) temp.Rows[i].Cells[4].Value;
                                DateTime tempVaoNew = tempVaoOld.Subtract(new TimeSpan(0, tempTre, 0));
                                int index = nhanvien.DSVaoHopLe.FindIndex(item => item.TimeStr == tempVaoOld);
                                int kq1 = SqlDataAccessHelper.ExecNoneQueryString(" update CheckInOut " +
                                                                                  " set TimeStr = @TimeStrNew " +
                                                                                  " where TimeStr = @TimeStrOld " +
                                                                                  " and MachineNo % 2 = 1 " +
                                                                                  " and UserEnrollNumber = @UserEnrollNumber "
                                                                                  , new[] { "@UserEnrollNumber", "@TimeStrOld", "@TimeStrNew"}
                                                                                  , new object[] {nhanvien.UserEnrollNumber, tempVaoOld , tempVaoNew});
                                int kq2 = ExecuteInsertLichSuSua(nhanvien.UserEnrollNumber, tempVaoOld, tempVaoNew, nhanvien.DSVaoHopLe[index].OriginType, nhanvien.DSVaoHopLe[index].Source, nhanvien.DSVaoHopLe[index].MachineNo, ThamSo.currUserID
                                    , "Bù giờ vào trễ", "Bù "+ tempTre + " phút: từ " + tempVaoOld + " thành " + tempVaoNew, 0);
                                nhanvien.QuyGioTL.Used += tempTre;
                                int kq3 = SqlDataAccessHelper.ExecNoneQueryString(" update QuyGioTichLuy " +
                                                                                  " set Used = @Used " +
                                                                                  " where UserEnrollNumber = @UserEnrollNumber " +
                                                                                  " and Thang = @Thang "
                                                                                  , new[] {"@UserEnrollNumber", "@Thang", "@Used"}
                                                                                  , new object[] {nhanvien.UserEnrollNumber, dateTimeThang.Value, nhanvien.QuyGioTL.Used});
                            }
                            int tempSom = (int)temp.Rows[i].Cells[7].Value;
                            if (tempSom > 0) {
                                DateTime tempRaOld = (DateTime)temp.Rows[i].Cells[5].Value;
                                DateTime tempRaNew = tempRaOld.Add(new TimeSpan(0, tempSom, 0));
                                int index = nhanvien.DSRaHopLe.FindIndex(item => item.TimeStr == tempRaOld);
                                int kq1 = SqlDataAccessHelper.ExecNoneQueryString(" update CheckInOut " +
                                                                                  " set TimeStr = @TimeStrNew " +
                                                                                  " where TimeStr = @TimeStrOld " +
                                                                                  " and MachineNo % 2 = 0 " +
                                                                                  " and UserEnrollNumber = @UserEnrollNumber "
                                                                                  , new[] { "@UserEnrollNumber", "@TimeStrOld", "@TimeStrNew" }
                                                                                  , new object[] { nhanvien.UserEnrollNumber, tempRaOld, tempRaNew });
                                int kq2 = ExecuteInsertLichSuSua(nhanvien.UserEnrollNumber, tempRaOld, tempRaNew, nhanvien.DSRaHopLe[index].OriginType, nhanvien.DSRaHopLe[index].Source, nhanvien.DSRaHopLe[index].MachineNo, ThamSo.currUserID
                                    , "Bù giờ ra sớm", "Bù " + tempSom + " phút: từ " + tempRaOld + " thành " + tempRaNew, 0);
                                nhanvien.QuyGioTL.Used += tempSom;
                                int kq3 = SqlDataAccessHelper.ExecNoneQueryString(" update QuyGioTichLuy " +
                                                                                  " set Used = @Used " +
                                                                                  " where UserEnrollNumber = @UserEnrollNumber " +
                                                                                  " and Thang = @Thang "
                                                                                  , new[] {"@UserEnrollNumber", "@Thang", "@Used"}
                                                                                  , new object[] {nhanvien.UserEnrollNumber, dateTimeThang.Value, nhanvien.QuyGioTL.Used});
                            }
                        }
                        btnXem_Click(null, null);
*/
                    }

                    private void dataGridCTTreSom_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
                        if (e.ColumnIndex == 0) {
                            dataGridCTTreSom.EndEdit();
                            ChkAlldataGridCTTreSom = !ChkAlldataGridCTTreSom;
                            Parallel.For(0, dataGridCTTreSom.Rows.Count, i => dataGridCTTreSom.Rows[i].Cells[0].Value = ChkAlldataGridCTTreSom);
                            dataGridCTTreSom.EndEdit();
                        }
                    }

                    private void dataGridDSGTC_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
                        if (e.ColumnIndex == 0) {
                            dataGridDSGTC.EndEdit();
                            ChkAlldataGridDSGTC = !ChkAlldataGridDSGTC;
                            Parallel.For(0, dataGridDSGTC.Rows.Count, i => dataGridDSGTC.Rows[i].Cells[0].Value = ChkAlldataGridDSGTC);
                            dataGridDSGTC.EndEdit();
                        }
                    }

                    private int ExecuteInsertLichSuSua(int temp_UserEnrollNumber, DateTime temp_TimeStrOld, DateTime temp_TimeStrNew, string temp_Source, int temp_MachineNo, int pCurrentUserID, string pLydo, string pGhiChu, int pCommandType) {
                        object pWorkCode = new object();

                        return SqlDataAccessHelper.ExecNoneQueryString(ThamSo.InsStrBackupThemGioVaoRa()
                                                                                , new[] { "@UserEnrollNumber"
                                                                    ,"@TimeDate","@TimeStrOld","@TimeStrNew"
                                                                    ,"@Source","@MachineNo","@WorkCode"
                                                                    ,"@UserID"
                                                                    ,"@Explain","@Note"
                                                                    //, Execute time lấy giờ của sqlserver
                                                                    ,"@CommandType"}
                                , new object[] {  temp_UserEnrollNumber
                                                                    , temp_TimeStrOld.Date, temp_TimeStrOld, temp_TimeStrNew
                                                                    , temp_Source, temp_MachineNo // cell 9 [TBD] giá trị này chưa biết nên để là 0"@WorkCode"
                                                                    , pCurrentUserID //[TBD] lấy current user id"@UserID"
                                                                    , pLydo , pGhiChu 
                                                                    , pCommandType // [TBD] @CommandType +1 nếu là thêm, 0 nếu là sửa, -1 nếu là xóa
                                                });
                    }


            
        }
    }

