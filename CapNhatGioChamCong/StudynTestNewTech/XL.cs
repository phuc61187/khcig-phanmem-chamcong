using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapNhatGioChamCong;

namespace StudynTestNewTech {
    public class XL {
        public DataTable tablePHONG;
        public DataTable LayDSPhong(int userID) {
            #region query

            string query = @"SELECT 
  r1.ID,
  r1.RelationID,
  r1.Description,
  r2.ID,
  r2.Description,
  r3.ID,
  r3.Description
FROM
  RelationDept r2
  LEFT OUTER JOIN RelationDept r1 ON (r1.RelationID = r2.ID)
  LEFT OUTER JOIN RelationDept r3 ON (r2.RelationID = r3.ID),
  DeptPrivilege
WHERE
  DeptPrivilege.UserID = 21 AND 
  DeptPrivilege.IsYes = 1 AND 
  DeptPrivilege.IDD = r1.ID ";
            #endregion

            return null;
        }
        public DataTable GetAllNV(int IDPhong) {
            #region
            string query = @"SELECT 
  UserFullCode,
  UserFullName,
  UserLastName,
  UserEnrollNumber,
  UserEnrollName,
  UserIDTitle,
  UserIDC,
  UserIDD,
  SchID,
  HeSoLuongCB,
  HeSoLuongSP
FROM 
  UserInfo where UserEnabled = 1";
            #endregion

            DataTable table = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
            return table;
        }
        public List<int> LayArrID(string colName, DataTable table) {
            List<int> kq = new List<int>();
            foreach (DataRow row in table.Rows) {
                kq.Add((int)row[colName]);
            }
            return kq;
        }
        public List<string> LayArrIDStr(string colName, DataTable table) {
            List<string> kq = new List<string>();
            foreach (DataRow row in table.Rows) {
                kq.Add((string)row[colName]);
            }
            return kq;
        }
        public void AddNewCol(string colName, Type type, DataTable table) {
            table.Columns.Add(colName, type);
        }

        /// <summary>
        /// nhớ thêm điều kiện trước chuỗi này AND, OR ....CheckInOut.UserEnrollNumber = {0}
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="tenThuoctinh">chú ý nhớ sử dụng tên tiền tố phía trước CheckInOut.UserEnrollNumber hay UserInfo.UserEnrollNumber</param>
        /// <returns></returns>
        public string TaoChuoiOR(List<int> ds, string tenThuoctinh) {
            string kq = string.Empty;
            kq += @"( ";
            kq += tenThuoctinh + @" = {0}";
            kq = String.Format(kq, String.Join(" or " + tenThuoctinh + " = ", ds.ToArray()));
            kq += @" )";
            return kq;

        }

        public DataTable DocDSCheck(List<int> dsnv, DateTime gioBD, DateTime gioKT) {
            #region query

            string query = @" select * from CheckInOut where TimeStr between (@giobd and @giokt) ";
            string chuoiOR = TaoChuoiOR(dsnv, "CheckInOut.UserEnrollNumber");
            query += " AND " + chuoiOR;

            #endregion

            DataTable tableCheckk = SqlDataAccessHelper.ExecuteQueryString(query, new[] { "@giobd", "@giokt" }, new object[] { gioBD, gioKT });
            return tableCheckk;

            /*
                        tableChkINN_Auto = tableCheckk.Clone();
                        tableChkOUT_Auto = tableCheckk.Clone();
                        DataRow[] arrChkINN_Auto = tableCheckk.Select("MachineNo % 2 = 1 and (IDXacNhanCaVaLamThem is null)");
                        DataRow[] arrChkOUT_Auto = tableCheckk.Select("MachineNo % 2 = 0 and (IDXacNhanCaVaLamThem is null) ");
                        DataRow[] arrChkINN_DaXN = tableCheckk.Select("MachineNo % 2 = 1 and (IDXacNhanCaVaLamThem is not null)");
                        DataRow[] arrChkOUT_DaXN = tableCheckk.Select("MachineNo % 2 = 0 and (IDXacNhanCaVaLamThem is not null) ");
                        Loc(arrChkINN_Auto, tableChkINN_Auto);
                        Loc(arrChkOUT_Auto, tableChkOUT_Auto);
            */
            //ToDataTable(arrChkINN_DaXN, ta);
        }

        public void Loc(DataRow[] arr, DataTable table) {
            DateTime lasttime = DateTime.MinValue, currtime = DateTime.MinValue;

            for (int i = 0; i < arr.Count(); i++) {
                DataRow row = arr[i];
                if (i == 0) {
                    lasttime = (DateTime)row["TimeStr"];
                    table.ImportRow(row);
                }
                else {
                    currtime = (DateTime)row["TimeStr"];
                    if (currtime - lasttime > ThamSo._30phut) {
                        table.ImportRow(row);
                        lasttime = currtime;
                    }
                }
            }

        }


        public void ToDataTable(DataRow[] arr, DataTable table) {
            if (!arr.Any()) return;
            foreach (DataRow row in arr) table.ImportRow(row);
        }
        public void TaoTable_CIO_A(DataTable tableChkINN, DataTable tableChkOUT, DataTable table_CIO) {
            //1. ko có out nào hết --> vào ko ra
            if ((tableChkOUT.Rows == null || tableChkOUT.Rows.Count == 0)
                && (tableChkINN.Rows != null && tableChkINN.Rows.Count != 0)) {
                for (int i = 0; i < tableChkINN.Rows.Count; i++) {
                    DataRow rowCI = tableChkINN.Rows[i];
                    DataRow row = table_CIO.NewRow();
                    fillKoRaa(rowCI, row);
                }
                return;
            }
            //2. ko có in nào hết
            if ((tableChkINN.Rows == null || tableChkINN.Rows.Count == 0)
                && (tableChkOUT.Rows != null && tableChkOUT.Rows.Count != 0)) {
                for (int i = 0; i < tableChkOUT.Rows.Count; i++) {
                    DataRow rowCO = tableChkOUT.Rows[i];
                    DataRow row = table_CIO.NewRow();
                    fillKoVao(rowCO, row);
                }
                return;
            }
            //3. đủ IO
            if ((tableChkINN.Rows != null && tableChkINN.Rows.Count != 0)
                && (tableChkOUT.Rows != null && tableChkOUT.Rows.Count != 0)) {
                bool flag = false;
                int i1 = 0, i2 = 0;
                while (i1 < tableChkINN.Rows.Count && i2 < tableChkOUT.Rows.Count) {
                    DataRow rowCI = tableChkINN.Rows[i1];
                    DataRow rowCO = tableChkOUT.Rows[i2];
                    DataRow row = table_CIO.NewRow();
                    DateTime t_vao = (DateTime)rowCI["TimeStr"];
                    DateTime t_raa = (DateTime)rowCO["TimeStr"];
                    TimeSpan duration = t_raa - t_vao;
                    if (duration < ThamSo._0gio) {//ko vào
                        fillKoVao(rowCO, row);
                        i2++;
                    }
                    else if (duration > ThamSo._0gio && duration <= ThamSo._30phut) { // vào ra liền
                        fillKoVao(rowCI, row);
                        fillKoRaa(rowCO, row);
                        i1++;
                        i2++;
                    }
                    else if (duration > ThamSo._30phut && duration <= ThamSo._21h45) { // vào ra ok
                        fillVaoRa(rowCI, rowCO, row);
                        i1++;
                        i2++;
                    }
                    else { // vào ra hơn 21 tiếng
                        fillKoVao(rowCI, row);
                        fillKoRaa(rowCO, row);
                        i1++;
                        i2++;
                    }
                }
                if (i1 == tableChkINN.Rows.Count && i2 < tableChkOUT.Rows.Count) // chỉ check in hết
				{
                    while (i2 < tableChkOUT.Rows.Count) {
                        DataRow rowCO = tableChkOUT.Rows[i2];
                        DataRow row = table_CIO.NewRow();
                        fillKoVao(rowCO, row);
                    }
                }
                else if (i1 < tableChkINN.Rows.Count && i2 == tableChkOUT.Rows.Count) // chỉ check out hết
				{
                    while (i1 < tableChkINN.Rows.Count) {
                        DataRow rowCI = tableChkINN.Rows[i1];
                        DataRow row = table_CIO.NewRow();
                        fillKoRaa(rowCI, row);
                    }
                }
                else {// cả 2 cùng hết, ko làm gì cả

                }
            }
            //4. thoát 1 là còn I, 2 là còn O
        }

        public void fillKoVao(DataRow rowCO, DataRow row) {
            CopyValue(rowCO, row, new string[] { "UserEnrollNumber", "TimeStr", "MachineNo", }
                                , new string[] { "UserEnrollNumber", "TimeStrRaa", "MachineNoRaa", });
            row["HaveINOUT"] = -1;
        }
        public void fillKoRaa(DataRow rowCI, DataRow row) {
            CopyValue(rowCI, row, new string[] { "UserEnrollNumber", "TimeStr", "MachineNo", }
                                , new string[] { "UserEnrollNumber", "TimeStrVao", "MachineNoVao", });
            row["HaveINOUT"] = -2;
        }
        public void fillVaoRa(DataRow rowCI, DataRow rowCO, DataRow row) {
            CopyValue(rowCI, row, new string[] { "UserEnrollNumber", "TimeStr", "MachineNo", }
                                , new string[] { "UserEnrollNumber", "TimeStrVao", "MachineNoVao", });
            CopyValue(rowCO, row, new string[] { "UserEnrollNumber", "TimeStr", "MachineNo", }
                                , new string[] { "UserEnrollNumber", "TimeStrRaa", "MachineNoRaa", });
            row["HaveINOUT"] = 1;
        }

        public void CopyValue(DataRow Source, DataRow des, string[] colt1, string[] colt2) {
            for (int i = 0; i < colt1.Length; i++) {
                des[colt2[i]] = Source[colt2[i]];
            }
        }

        public void XemCong(List<int> dsnv, DateTime ngaybd, DateTime ngaykt) {
            DataTable table = SqlDataAccessHelper.ExecuteQueryString(
                @"select * from UserInfo, RelationDept where UserInfo.UserIDD = 294 and UserInfo.UserIDD = RelationDept.ID and UserInfo.Enabled = 1");
            List<string> iLstUserEnrollNumber = LayArrIDStr("UserEnrollNumber", table);

            DataTable tableCheck = DocDSCheck(dsnv, ngaybd, ngaykt);
            for (int i = 0; i < dsnv.Count; i++) {
                #region query

                string query = @"SELECT 
  dbo.UserInfo.UserEnrollNumber,
  dbo.Shifts.ShiftID,
  dbo.Shifts.ShiftCode,
  dbo.Shifts.Onduty,
  dbo.Shifts.Offduty,
  dbo.Shifts.DayCount,
  dbo.Shifts.OnTimeIn,
  dbo.Shifts.OnTimeOut,
  dbo.Shifts.CutIn,
  dbo.Shifts.CutOut,
  dbo.Shifts.OnLunch,
  dbo.Shifts.OffLunch,
  dbo.Shifts.WorkingTime,
  dbo.Shifts.Workingday,
  dbo.Shifts.LateGrace,
  dbo.Shifts.EarlyGrace,
  dbo.Shifts.AfterOT
FROM
  dbo.UserInfo,
  dbo.Schedule,
  dbo.Shifts,
  dbo.ShiftSch
WHERE
  dbo.UserInfo.SchID = dbo.Schedule.SchID AND 
  dbo.Schedule.SchID = dbo.ShiftSch.SchID AND 
  dbo.ShiftSch.T1 = dbo.Shifts.ShiftID AND 
  dbo.UserInfo.UserEnrollNumber = " + dsnv[i];
                #endregion
                DataTable table_Ca = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
                DataRow[] arrRow_INN_A = tableCheck.Select("UserEnrollNumber=" + dsnv[i] + " and MachineNo % 2 = 1 and (IDXacNhanCaVaLamThem is null)");
                DataRow[] arrRow_OUT_A = tableCheck.Select("UserEnrollNumber=" + dsnv[i] + " and MachineNo % 2 = 0 and (IDXacNhanCaVaLamThem is null)");
                DataTable tableINN_A = tableCheck.Clone();
                DataTable tableOUT_A = tableCheck.Clone();
                Loc(arrRow_INN_A, tableINN_A);
                Loc(arrRow_OUT_A, tableOUT_A);
                DataTable tableCIO_A = TaoCauTrucDataTable(
                      new[] { "UserEnrollNumber", "TimeStrVao", "MachineNoVao", "TimeStrRaa", "MachineNoRaa", "HaveINOUT", }
                    , new[] { typeof(int), typeof(DateTime), typeof(int), typeof(DateTime), typeof(int), typeof(int), });
                TaoTable_CIO_A(tableINN_A, tableOUT_A, tableCIO_A);

                XetCa_A(tableCIO_A, table_Ca);
            }

        }

        private void XetCa_A(DataTable tableCIO_A, DataTable tableCa) {
            TimeSpan onnduty, offduty, cutinn, cutout, onninn, onnout, earlyGrace, LateGrace, afterOT;
            int daycount = 0;
            Single workingDayy = 0f, workingTime = 0f;
            bool flag = false;
            DateTime t_vao, t_raa, ngay, vaocaa, raacaa, bd_hieuvao, kt_hieuvao, bd_hieuraa, kt_hieuraa;
            for (int i = 0; i < tableCIO_A.Rows.Count; i++) {
                DataRow rowCIO = tableCIO_A.Rows[i];
                int haveinout = (int)rowCIO["HaveINOUT"];
                if (haveinout == -1) { }
                else if (haveinout == -2) { }
                else {// (haveinout > 0)
                    t_vao = (DateTime)rowCIO["TimeStrVao"];
                    t_raa = (DateTime)rowCIO["TimeStrRaa"];
                    ngay = ThamSo.GetDate(t_vao);
                    foreach (DataRow rowCaa in tableCa.Rows) {
                        #region
                        TimeSpan.TryParse(rowCaa["Onduty"].ToString(), out onnduty);
                        TimeSpan.TryParse(rowCaa["Offduty"].ToString(), out offduty);
                        TimeSpan.TryParse(rowCaa["OnTimeIn"].ToString(), out onninn);
                        TimeSpan.TryParse(rowCaa["OnTimeOut"].ToString(), out onnout);
                        TimeSpan.TryParse(rowCaa["CutIn"].ToString(), out cutinn);
                        TimeSpan.TryParse(rowCaa["CutOut"].ToString(), out cutout);
                        TimeSpan.TryParse(rowCaa["EarlyGrace"].ToString(), out earlyGrace);
                        TimeSpan.TryParse(rowCaa["LateGrace"].ToString(), out LateGrace);
                        TimeSpan.TryParse(rowCaa["AfterOT"].ToString(), out afterOT);
                        workingDayy = (Single)rowCaa["Workingday"];
                        workingTime = (Single)rowCaa["WorkingTime"];
                        daycount = (int)(rowCaa["DayCount"]);
                        #endregion
                        vaocaa = ngay.Add(onnduty);
                        raacaa = ngay.AddDays(daycount).Add(offduty);
                        bd_hieuvao = vaocaa - onninn;
                        kt_hieuvao = vaocaa.Add(cutinn);
                        bd_hieuraa = raacaa - onnout;
                        kt_hieuraa = raacaa.Add(cutout);
                        if (t_vao >= bd_hieuvao && t_vao <= kt_hieuvao && t_raa >= bd_hieuraa && t_raa <= kt_hieuraa) {
                            flag = true;
                            if (onnduty > ThamSo._20h00 && workingDayy == 2f && daycount == 1) {
                                DataRow[] ca3_ca1 = Tim(tableCa);
                                DataRow rowca3 = ca3_ca1[0];
                                TimeSpan.TryParse(rowca3["Onduty"].ToString(), out onnduty);
                                TimeSpan.TryParse(rowca3["Offduty"].ToString(), out offduty);
                                TimeSpan.TryParse(rowca3["OnTimeIn"].ToString(), out onninn);
                                TimeSpan.TryParse(rowca3["OnTimeOut"].ToString(), out onnout);
                                TimeSpan.TryParse(rowca3["CutIn"].ToString(), out cutinn);
                                TimeSpan.TryParse(rowca3["CutOut"].ToString(), out cutout);
                                TimeSpan.TryParse(rowca3["EarlyGrace"].ToString(), out earlyGrace);
                                TimeSpan.TryParse(rowca3["LateGrace"].ToString(), out LateGrace);
                                TimeSpan.TryParse(rowca3["AfterOT"].ToString(), out afterOT);
                                workingDayy = (Single)rowca3["Workingday"];
                                workingTime = (Single)rowca3["WorkingTime"];
                                daycount = (int)(rowca3["DayCount"]);
                                rowCIO["VaoCa"] = vaocaa;
                                rowCIO["RaaCa"] = raacaa;
                                rowCIO["TreCa"] = vaocaa.Add(LateGrace);
                                rowCIO["SomCa"] = raacaa - earlyGrace;
                                rowCIO["AfterOT"] = raacaa.Add(afterOT);
                                rowCIO["Workingday"] = workingDayy;
                                rowCIO["WorkingTime"] = workingTime;
                                rowCIO["QuaDem"] = daycount;
                                rowCIO["ShiftID"] = (int)rowCaa["ShiftID"];
                                rowCIO["ShiftCode"] = rowCaa["ShiftCode"].ToString();
                                // update lại giờ ra thành giờ ra ca 3
                                rowCIO["TimeStrRaa"] = raacaa;
                                rowCIO["MachineNoRaa"] = 22;

                                DataRow rowcatach = CopyRow(tableCIO_A, rowCIO);
                                tableCIO_A.Rows.InsertAt(rowcatach, i);
                                DataRow rowca1 = ca3_ca1[1];
                                // sau insert tăng i lên 
                                i++;
                                rowCIO = tableCIO_A.Rows[i];
                                TimeSpan.TryParse(rowca1["Onduty"].ToString(), out onnduty);
                                TimeSpan.TryParse(rowca1["Offduty"].ToString(), out offduty);
                                TimeSpan.TryParse(rowca1["OnTimeIn"].ToString(), out onninn);
                                TimeSpan.TryParse(rowca1["OnTimeOut"].ToString(), out onnout);
                                TimeSpan.TryParse(rowca1["CutIn"].ToString(), out cutinn);
                                TimeSpan.TryParse(rowca1["CutOut"].ToString(), out cutout);
                                TimeSpan.TryParse(rowca1["EarlyGrace"].ToString(), out earlyGrace);
                                TimeSpan.TryParse(rowca1["LateGrace"].ToString(), out LateGrace);
                                TimeSpan.TryParse(rowca1["AfterOT"].ToString(), out afterOT);
                                workingDayy = (Single)rowca1["Workingday"];
                                workingTime = (Single)rowca1["WorkingTime"];
                                daycount = (int)(rowca1["DayCount"]);
                                rowCIO["VaoCa"] = vaocaa;
                                rowCIO["RaaCa"] = raacaa;
                                rowCIO["TreCa"] = vaocaa.Add(LateGrace);
                                rowCIO["SomCa"] = raacaa - earlyGrace;
                                rowCIO["AfterOT"] = raacaa.Add(afterOT);
                                rowCIO["Workingday"] = workingDayy;
                                rowCIO["WorkingTime"] = workingTime;
                                rowCIO["QuaDem"] = daycount;
                                rowCIO["ShiftID"] = (int)rowCaa["ShiftID"];
                                rowCIO["ShiftCode"] = rowCaa["ShiftCode"].ToString();
                                // update lại giờ ra thành giờ ra ca 1
                                rowCIO["TimeStrVao"] = vaocaa;
                                rowCIO["MachineNoVao"] = 21;
                            }
                            else {
                                rowCIO["VaoCa"] = vaocaa;
                                rowCIO["RaaCa"] = raacaa;
                                rowCIO["TreCa"] = vaocaa.Add(LateGrace);
                                rowCIO["SomCa"] = raacaa - earlyGrace;
                                rowCIO["AfterOT"] = raacaa.Add(afterOT);
                                rowCIO["Workingday"] = workingDayy;
                                rowCIO["WorkingTime"] = workingTime;
                                rowCIO["QuaDem"] = daycount;
                                rowCIO["ShiftID"] = (int)rowCaa["ShiftID"];
                                rowCIO["ShiftCode"] = rowCaa["ShiftCode"].ToString();
                            }
                        }
                    }
                }
            }
        }

        public void SetValuesForRow(DataRow row, string[] colName, object[] value) {
            for (int i = 0; i < colName.Length; i++) {
                row[colName[i]] = value[i];
            }
        }

        private DataRow[] Tim(DataTable tableCa) {
            DataRow[] kq = new DataRow[2];
            for (int i = 0, j = 0; i < tableCa.Rows.Count || j >= 2; i++) {
                DataRow row = tableCa.Rows[i];
                TimeSpan onduty;
                TimeSpan.TryParse(row["Onduty"].ToString(), out onduty);
                if ((Single)row["Workingday"] == 1f && (int)row["DayCount"] == 1) {
                    kq[0] = row;
                    j++;
                }
                else if ((Single)row["Workingday"] == 1f && onduty < ThamSo._7gio45ph) {
                    kq[1] = row;
                    j++;
                }
            }
            return kq;
        }

        private DataRow CopyRow(DataTable tableCIO_A, DataRow rowCIO) {
            DataRow row = tableCIO_A.NewRow();
            for (int i = 0; i < rowCIO.ItemArray.Count(); i++) {
                row.ItemArray[i] = rowCIO.ItemArray[i];
            }
            return row;
        }

        public DataTable TaoCauTrucDataTable(string[] colName, Type[] colType) {
            DataTable kq = new DataTable();
            for (int i = 0; i < colName.Length; i++) {
                kq.Columns.Add(colName[i], colType[i]);
            }
            return kq;
        }
    }
}
