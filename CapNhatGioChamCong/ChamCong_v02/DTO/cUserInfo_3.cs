using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ChamCong_v02.BUS;
using ChamCong_v02.DAO;
using log4net;

namespace ChamCong_v02.DTO {

    public class cPhongBan {
        public int ID;
        public string TenPhongBan;
    }

    public class cChkComparer : IComparer<cChk> {
        public int Compare(cChk x, cChk y) {
            return x.TimeStr.CompareTo(y.TimeStr);
            //return 1;
        }
    }

    public class cChkInOutComparer : IComparer<cChkInOut> {
        public int Compare(cChkInOut x, cChkInOut y) {
            return x.TimeStrDaiDien.CompareTo(y.TimeStrDaiDien);
            //return 1;
        }
    }

    public class cUserInfo {
    		public static readonly ILog log = LogManager.GetLogger("XL");
        #region Public Properties

        public string UserFullCode { get; set; }
        public string UserFullName { get; set; }
        public int UserEnrollNumber { get; set; }
        public cPhongBan BoPhan { get; set; }
        //public int UserIDTitle { get; set; }
        //public int UserPrivilege { get; set; }
        //public bool UserEnabled { get; set; }
        //public int UserIDC { get; set; }
        //public int UserIDD { get; set; }
        //public int SchID { get; set; }
        //public int UserGroup { get; set; }

        #endregion
        #region sử dụng để tính công

        public Single HeSoLuongCB;
        public Single HeSoLuongSP;

        #endregion

        #region khai báo biến

        public bool MacDinhTinhPC150 { get; set; }
        public cShiftSchedule LichTrinhLV { get; set; }
        public List<cShift> DSCa { get; set; }
        public List<cShift> DSCaMoRong { get; set; }
        public List<cChk> ds_CheckInn_A { get; set; }
        public List<cChk> ds_CheckOut_A { get; set; }
        public List<cChk> ds_Check_A { get; set; }
        public List<cChkInOut> DSVaoRa { get; set; }
        public List<cNgayCong> DSNgayCong { get; set; }

        public List<cChkInOut_A> DS_CIO_A { get; set; }
        public List<cChkInOut_V> DS_CIO_V { get; set; }

        #endregion

        public void ArrayRowsToDSCheck_A(DataRow[] arrRows) { //cấu trúc datatble là cấu trúc bảng checkinout
            #region reset lại các danh sách
            if (ds_CheckInn_A == null) ds_CheckInn_A = new List<cChk>();
            else ds_CheckInn_A.Clear();
            if (ds_CheckOut_A == null) ds_CheckOut_A = new List<cChk>();
            else ds_CheckOut_A.Clear();
            if (ds_Check_A == null) ds_Check_A = new List<cChk>();
            else ds_Check_A.Clear();
            #endregion
            if (arrRows.Length != 0) {
                foreach (DataRow row in arrRows) {
                    int MachineNo = (int)row["MachineNo"];
                    string Source = (string)row["Source"];
                    DateTime TimeStr = (DateTime)row["TimeStr"];
                    if (MachineNo % 2 == 1) {
                        cChkInn_A check = new cChkInn_A() { TimeStr = TimeStr, Source = Source, GioLienQuan = null, MachineNo = MachineNo };
                        ds_CheckInn_A.Add(check);
                    }
                    else {
                        cChkOut_A check = new cChkOut_A() { TimeStr = TimeStr, Source = Source, GioLienQuan = null, MachineNo = MachineNo };
                        ds_CheckOut_A.Add(check);
                    }
                }
                LoaiBoCheck30phut(ds_CheckInn_A); //dataTable lấy lên đã sort sẵn nên dùng hàm này ko tích hợp sort
                LoaiBoCheck30phut(ds_CheckOut_A);
            }
        }

        public void ArrayRowsToDS_CIO_V(DataRow[] arrRows) { // cấu trúc là cấu trúc ghép 2 bảng checkinout và xác nhận
            if (DS_CIO_V == null) DS_CIO_V = new List<cChkInOut_V>();
            else DS_CIO_V.Clear();

            if (arrRows.Length != 0) {

                for (int i = 0; i < arrRows.Length; i = i + 2) {
                    DataRow rowInn = arrRows[i];
                    if (i + 1 >= arrRows.Length) continue; // ko có phần tử kế tiếp để xét
                    DataRow rowOut = arrRows[i + 1];
                    int IDinn = (int)rowInn["IDXacNhanCaVaLamThem"];
                    int IDout = (int)rowOut["IDXacNhanCaVaLamThem"];
                    if (IDinn != IDout)// bị mất cặp -> giảm 1 để tăng 2 là thằng kế tiếp
                    {
                        i = i - 1;
                        continue;
                    }
                    // đủ điều kiện ghép  cặp
                    #region lấy thông tin
                    int shiftID = (int)rowInn["ShiftID"];
                    string shiftCode = rowInn["ShiftCode"].ToString();
                    int dayCount = (int)rowInn["DayCount"];
                    Single workingday = (Single)rowInn["Workingday"];
                    TimeSpan onnDutyTs, offDutyTs;
                    TimeSpan.TryParse(rowInn["Onduty"].ToString(), out onnDutyTs);
                    TimeSpan.TryParse(rowInn["Offduty"].ToString(), out offDutyTs);

                    TimeSpan afterOTTs = new TimeSpan(0, (int)rowInn["AfterOT"], 0);
                    TimeSpan lateGraceTSS = new TimeSpan(0, (int)rowInn["LateGrace"], 0);
                    TimeSpan earlyGraceTS = new TimeSpan(0, (int)rowInn["EarlyGrace"], 0);

                    lateGraceTSS = onnDutyTs.Add(lateGraceTSS);
                    offDutyTs = offDutyTs.Add(new TimeSpan(dayCount, 0, 0, 0));
                    earlyGraceTS = offDutyTs.Subtract(earlyGraceTS);
                    afterOTTs = offDutyTs.Add(afterOTTs);
                    int pOTMin = (int)rowInn["OTMin"];
                    bool tempTinhPC150 = false;
                    if (rowInn["TinhPC150"] == DBNull.Value) tempTinhPC150 = false;
                    else tempTinhPC150 = (bool)rowInn["TinhPC150"];

                    int wkt = int.Parse(rowInn["WorkingTime"].ToString());
                    TimeSpan pWorkingTime = new TimeSpan(0, wkt, 0, 0);

                    string SourceInn = (string)rowInn["Source"];
                    string SourceOut = (string)rowOut["Source"];
                    int MachineNoInn = (int)rowInn["MachineNo"];
                    int MachineNoOut = (int)rowOut["MachineNo"];
                    DateTime timeInn = (DateTime)rowInn["TimeStr"];
                    DateTime timeOut = (DateTime)rowOut["TimeStr"];
                    #endregion

                    cChkInn_V chkInnV = new cChkInn_V() { GioLienQuan = null, ID = IDinn, MachineNo = MachineNoInn, Source = SourceInn, TimeStr = timeInn };
                    cChkOut_V chkOutV = new cChkOut_V() { GioLienQuan = null, ID = IDout, MachineNo = MachineNoOut, Source = SourceOut, TimeStr = timeOut };
                    cChkInOut_V chkInOutV = new cChkInOut_V() {
                        Vao = chkInnV, Raa = chkOutV, HaveINOUT = 1, TongGioThuc = chkOutV.TimeStr - chkInnV.TimeStr, TimeStrDaiDien = chkInnV.TimeStr,
                        TinhPC150 = tempTinhPC150, LamThem = new TimeSpan(0, 0, pOTMin, 0), ThuocNgayCong = ThamSo.GetDate(chkInnV.TimeStr),
                    }; // [TBD] xem lại thuộc ngày công

                    cShift tmpThuocCa;
                    if (shiftID > 0) {
                        tmpThuocCa = DSCa.Find(item => item.ShiftID == shiftID);
                    }
                    else if (shiftID > int.MinValue + 100 && shiftID < 0) // ca tách và ca kết hợp  [Chú ý] + 100 vì chừa khoảng này cho các loại khác
                        tmpThuocCa = new cShift() { ShiftID = shiftID, ShiftCode = shiftCode, OnnDutyTS = onnDutyTs, OffDutyTS = offDutyTs, chophepvaotreTS = lateGraceTSS, chopheprasomTS = earlyGraceTS, batdaulamthemTS = afterOTTs, WorkingTimeTS = pWorkingTime, Workingday = workingday, DayCount = dayCount, QuaDem = (dayCount == 1), LunchMinute = ThamSo._0gio, LoaiCa = 0 }; //[TBD]
                    else if (shiftID < int.MinValue + 100) // ca tự do 8 tiếng
                        tmpThuocCa = new cShift() { ShiftID = shiftID, ShiftCode = shiftCode, OnnDutyTS = onnDutyTs, OffDutyTS = offDutyTs, chophepvaotreTS = lateGraceTSS, chopheprasomTS = earlyGraceTS, batdaulamthemTS = afterOTTs, WorkingTimeTS = pWorkingTime, Workingday = workingday, DayCount = dayCount, QuaDem = (dayCount == 1), LunchMinute = ThamSo._0gio, LoaiCa = 1 };
                    else {
                        tmpThuocCa = new cShift() { ShiftID = shiftID, ShiftCode = "XacNhan8h", OnnDutyTS = onnDutyTs, OffDutyTS = offDutyTs, chophepvaotreTS = lateGraceTSS, chopheprasomTS = earlyGraceTS, batdaulamthemTS = afterOTTs, WorkingTimeTS = pWorkingTime, Workingday = workingday, DayCount = dayCount, QuaDem = (dayCount == 1), LunchMinute = ThamSo._0gio, LoaiCa = 1 }; //[TBD]
                    }
                    if (tmpThuocCa == null) {
                        log4net.ILog log = log4net.LogManager.GetLogger("ERROR function Check_GioDaXN ");
                        log.Fatal("ERROR function Check_GioDaXN");
                    }
                    chkInOutV.ThuocCa = tmpThuocCa;
                    chkInOutV.QuaDem = tmpThuocCa.QuaDem;
                    XL.TinhCongTheoCa(chkInOutV, chkInOutV.ThuocCa);//[TBD]
                    DS_CIO_V.Add(chkInOutV);

                }
            }
        }

        public void AddNewCheck_A(cChk check) {
            if (check.GetType() == typeof(cChkInn_A)) {
                if (ds_CheckInn_A == null) ds_CheckInn_A = new List<cChk>();
                ds_CheckInn_A.Add(check);
            }
            else if (check.GetType() == typeof(cChkOut_A)) {
                if (ds_CheckOut_A == null) ds_CheckOut_A = new List<cChk>();
                ds_CheckOut_A.Add(check);
            }
        }

        public void LoaiBoCheck30phut(List<cChk> dscheck) { // lọc này phải dảm bảo sort trước
            if (dscheck == null || dscheck.Count == 0 || dscheck.Count == 1) return;
            for (int i = 0; i < dscheck.Count - 1; i++) {
                cChk before = dscheck[i];
                cChk afterr = dscheck[i + 1];
                if (afterr.TimeStr - before.TimeStr <= ThamSo._30phut) {
                    if (before.GioLienQuan == null) before.GioLienQuan = new List<cChk>();
                    before.GioLienQuan.Add(afterr);
                }
            }
        }

        public List<cChkInOut> TronDS_CIO_A_V(List<cChkInOut_A> dsCIO_A, List<cChkInOut_V> dsCIO_V) {
            List<cChkInOut> kq = new List<cChkInOut>();
            kq.AddRange(dsCIO_A);
            kq.AddRange(dsCIO_V);
            kq.Sort(new cChkInOutComparer());
            return kq;
        }

        public List<cChkInOut_A> GhepCIO_A(List<cChk> DSchkInn, List<cChk> DSchkOut) {
            List<cChkInOut_A> kq = new List<cChkInOut_A>();
            int x1 = 0, x2 = 0;
            if (DSchkInn.Count == 0 && DSchkOut.Count != 0) {
                while (x2 < DSchkOut.Count) {
                    kq.Add(new cChkInOut_A() { Vao = null, Raa = DSchkOut[x2], HaveINOUT = -2, TongGioThuc = ThamSo._0gio, ThuocNgayCong = DSchkOut[x2].TimeStr.Date, TimeStrDaiDien = DSchkOut[x2].TimeStr });
                }
            }
            else if (DSchkInn.Count != 0 && DSchkOut.Count == 0) {
                while (x1 < DSchkInn.Count) {
                    kq.Add(new cChkInOut_A() { Vao = DSchkInn[x1], Raa = null, HaveINOUT = -1, TongGioThuc = ThamSo._0gio, ThuocNgayCong = DSchkInn[x1].TimeStr.Date, TimeStrDaiDien = DSchkInn[x1].TimeStr });
                }
            }
            else if (DSchkInn.Count != 0 && DSchkOut.Count != 0) {
                while (x1 < DSchkInn.Count && x2 < DSchkOut.Count) {
                    cChk chkinn = DSchkInn[x1];
                    cChk chkout = DSchkOut[x2];
                    DateTime timeInn = chkinn.TimeStr;
                    DateTime timeOut = chkout.TimeStr;
                    if (timeOut <= timeInn)//ra ko vào
					{
                        kq.Add(new cChkInOut_A() { Vao = null, Raa = DSchkOut[x2], HaveINOUT = -2, TongGioThuc = ThamSo._0gio, ThuocNgayCong = DSchkOut[x2].TimeStr.Date, TimeStrDaiDien = DSchkOut[x2].TimeStr });
                        x2++;
                    }
                    else {
                        TimeSpan duration = timeOut - timeInn;
                        if (duration <= ThamSo._30phut || duration > ThamSo._21h45) {
                            kq.Add(new cChkInOut_A() { Vao = DSchkInn[x1], Raa = null, HaveINOUT = -1, TongGioThuc = ThamSo._0gio, ThuocNgayCong = DSchkInn[x1].TimeStr.Date, TimeStrDaiDien = DSchkInn[x1].TimeStr });
                            kq.Add(new cChkInOut_A() { Vao = null, Raa = DSchkOut[x2], HaveINOUT = -2, TongGioThuc = ThamSo._0gio, ThuocNgayCong = DSchkOut[x2].TimeStr.Date, TimeStrDaiDien = DSchkOut[x2].TimeStr });
                        }
                        else {
                            kq.Add(new cChkInOut_A() { Vao = DSchkInn[x1], Raa = DSchkOut[x2], HaveINOUT = 1, TongGioThuc = duration, ThuocNgayCong = ThamSo.GetDate(DSchkInn[x1].TimeStr), TimeStrDaiDien = DSchkInn[x1].TimeStr });
                        }
                        x1++;
                        x2++;
                    }
                }
                if (x2 < DSchkOut.Count) {
                    while (x2 < DSchkOut.Count) {
                        kq.Add(new cChkInOut_A() { Vao = null, Raa = DSchkOut[x2], HaveINOUT = -2, TongGioThuc = ThamSo._0gio, ThuocNgayCong = DSchkOut[x2].TimeStr.Date, TimeStrDaiDien = DSchkOut[x2].TimeStr });
                    }
                }
                else if (x1 < DSchkInn.Count) {
                    while (x1 < DSchkInn.Count) {
                        kq.Add(new cChkInOut_A() { Vao = DSchkInn[x1], Raa = null, HaveINOUT = -1, TongGioThuc = ThamSo._0gio, ThuocNgayCong = DSchkInn[x1].TimeStr.Date, TimeStrDaiDien = DSchkInn[x1].TimeStr });
                    }
                }

            }

            return kq;

        }

        public cShift XetCa_1(cChkInOut_A CIO, List<cShift> dsCa) {
            DateTime t_vao = CIO.Vao.TimeStr;
            DateTime t_raa = CIO.Raa.TimeStr;
            return dsCa.FirstOrDefault(ca => t_vao >= t_vao.Date.Add(ca.OnTimeInTS) && t_vao <= t_vao.Date.Add(ca.CutInTS)
                                    && t_raa >= t_vao.Date.Add(ca.OnTimeOutTS) && t_raa <= t_vao.Date.Add(ca.CutOutTS));
        }

        public void XetCa(List<cChkInOut_A> dsCIO, List<cShift> dsca) {
            for (int i = 0; i < dsCIO.Count; i++) {
                cChkInOut_A CIO = dsCIO[i];
                cShift ca = XetCa_1(CIO, dsca);
                if (ca != null) {
                    if (ca.Workingday == 2f && ca.OnnDutyTS > ThamSo._20h00) {
                        cChk raaca3 = new cChkOut_A() { GioLienQuan = null, MachineNo = 22, Source = "O", TimeStr = CIO.Vao.TimeStr.Date.Add(ca.OnnDutyTS).Add(ThamSo._8gio) };
                        cChk vaoca1 = new cChkInn_A() { GioLienQuan = null, MachineNo = 21, Source = "I", TimeStr = CIO.Vao.TimeStr.Date.Add(ca.OnnDutyTS).Add(ThamSo._8gio1giay) };
                        CIO.Raa = raaca3;
                        i = i - 1;
                        cChkInOut_A newCIO = new cChkInOut_A() { Vao = vaoca1, Raa = CIO.Raa, HaveINOUT = 1, };
                        dsCIO.Insert(i, newCIO);
                    }
                    else {
                        CIO.ThuocCa = ca;
                    }
                }
                else {
                    ca = new cShift() { ShiftID = int.MinValue, ShiftCode = "Ca 8 tiếng" };
                    TaoCaTuDo(ca, CIO.Vao.TimeStr, ThamSo._8gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1f);
                    CIO.ThuocCa = ca;
                }
            }

        }

        public static void TaoCaTuDo(cShift pCa, DateTime pCheckInTime, TimeSpan pWorkingTime, TimeSpan pchophepvaotreTS, TimeSpan pchopheprasomTS, TimeSpan pbatdaulamthemTS, float pWorkingDay) {
            pCa.OnnDutyTS = pCheckInTime.TimeOfDay;
            pCa.OffDutyTS = pCheckInTime.TimeOfDay + pWorkingTime;
            pCa.LateGraceTS = pchophepvaotreTS;
            pCa.EarlyGraceTS = pchopheprasomTS;
            pCa.AfterOTTS = pbatdaulamthemTS;
            pCa.chophepvaotreTS = pCa.OnnDutyTS.Add(pchophepvaotreTS);
            pCa.chopheprasomTS = pCa.OffDutyTS.Subtract(pchopheprasomTS);
            pCa.batdaulamthemTS = pCa.OffDutyTS.Add(pbatdaulamthemTS);
            pCa.DayCount = pCa.OffDutyTS.Days;
            pCa.QuaDem = (pCa.OffDutyTS.Days == 1);
            pCa.WorkingTimeTS = pWorkingTime;
            pCa.Workingday = pWorkingDay;
            pCa.LunchMinute = ThamSo._0gio;
        }

        internal void TaoDSCa(DataRow[] arrRow_LichTrinh) {
            int schID = (int)arrRow_LichTrinh[0]["SchID"];
            cShiftSchedule tmpLichTrinh = ThamSo.DSLichTrinh.Find(item => item.SchID == schID);
            if (DSCa == null) DSCa = new List<cShift>();
            DSCa = tmpLichTrinh.ListT1;
        }


        public List<cNgayCong> TinhCongTheoNgay(List<cChkInOut> pDSVaoRa, DateTime ngayBD, DateTime ngayKT, int UserEnrollNumber) {
            log.Debug("UserEnrollNumber "+ UserEnrollNumber);
            List<cNgayCong> kq = new List<cNgayCong>();
            DateTime ngaydem;
            if (pDSVaoRa.Count == 0) {
                ngaydem = ngayBD.Date;
                while (ngaydem <= ngayKT) { // <= vì lấy luôn ngày KT : vắng mặt
                    cNgayCong ngayKOcheck = new cNgayCong() { NgayCong = ngaydem, HasCheck = false, DSVaoRa = new List<cChkInOut>(), };
                    kq.Add(ngayKOcheck);
                    ngaydem = ngaydem.AddDays(1.0d);
                }
                return kq;
            }

            int vtriBD = 0;
            ngaydem = ngayBD.Date;

            while (ngaydem <= ngayKT.Date) {

                if (vtriBD >= pDSVaoRa.Count) { // hết DS nhưng ngaydem <= ngày KT ==> ghi lại những ngày sau là vắng mặt
                    while (ngaydem <= ngayKT.Date) {
                        cNgayCong ngayKOcheck = new cNgayCong() { NgayCong = ngaydem, HasCheck = false, DSVaoRa = new List<cChkInOut>(), };
                        kq.Add(ngayKOcheck);
                        ngaydem = ngaydem.AddDays(1d);
                    }
                    continue;
                }

                // chưa hết DS, bắt đầu từ ngày của DSVaoRa tại vtbd, đó là ngày có mặt
                cChkInOut CIO = pDSVaoRa[vtriBD];
                XL.TinhCongTheoCa(CIO, CIO.ThuocCa);
                DateTime ngayCoMat = CIO.ThuocNgayCong;

                // ghi lại những ngày vắng mặt trước ngày có mặt
                while (ngaydem < ngayCoMat) { // ko có = vì chỉ chạy đến trước ngày của tmpVaoRa thôi
                    cNgayCong ngayKOcheck = new cNgayCong() { NgayCong = ngaydem, HasCheck = false, DSVaoRa = new List<cChkInOut>(), };
                    kq.Add(ngayKOcheck);
                    ngaydem = ngaydem.AddDays(1.0d);
                }
                //ngaydem = ngày có mặt. stop, tmpVaoRa là vào ra đầu tiên trong ngày
                #region collapse
                cNgayCong ngayCOcheck = new cNgayCong() {
                    HasCheck = true, NgayCong = ngayCoMat, TinhPC150 = true, TinhPC200 = false,// mặc định khởi tạo ngày có tính PC 50%, nếu tồn tại ít nhất 1 ca nào ko tính pc 50% thì set cả ngày ko tính pc 50%
                    TongGioLam = CIO.TGLamTinhCong, TongGioLamDem = CIO.TGLamDem, TongGioThuc = CIO.TongGioThuc,
                    TongTre = CIO.VaoTre, TongSom = CIO.RaaSom,
                    TongCong = CIO.Cong,
                    PhuCapDem = ((CIO.TGLamDem.TotalHours > 0f)
                    ? ((float)(CIO.TGLamDem.TotalHours / 8f) * 0.3f)
                    : 0f)
                    // công thức cũ (tính phụ cấp tăng ca theo công) là : + (((tmpVaoRa.Cong > 1f) ? (tmpVaoRa.Cong - 1f) * 0.5f : 0f))
                    // đổi lại công thức mới (tính phụ cấp theo giờ) là : + (((tmpVaoRa.TGLamTinhCong.TotalHours > 8f) ? (((float)tmpVaoRa.TGLamTinhCong.TotalHours - 8f) / 8f) * 0.5f : 0f))
                };
                if (CIO.QuaDem == true) ngayCOcheck.QuaDem = true; // set qua đêm nếu có
                if (ngayCOcheck.TinhPC150) {
                    if (CIO.TinhPC150 == false) ngayCOcheck.TinhPC150 = false;

                    if (ngayCOcheck.TinhPC150)
                        ngayCOcheck.PhuCapThem = (((CIO.TGLamTinhCong.TotalHours > 8f)
                                                        ? ((((float)CIO.TGLamTinhCong.TotalHours - 8f) / 8f) * 0.5f)
                                                        : 0f));
                    else ngayCOcheck.PhuCapThem = 0f;
                }
                else ngayCOcheck.PhuCapThem = 0f;

                ngayCOcheck.TongPhuCap = ngayCOcheck.PhuCapDem + ngayCOcheck.PhuCapThem;

                ngayCOcheck.them(CIO);
                #endregion


                // sau khi tạo ngày công mới vào ra đó là vào ra đầu tiên thì chuyển sang VAORA next
                vtriBD++;
                // nếu hết ds thì ngưng, add tmpNgayCong1 vào danh sách ngày công (do chưa add)
                if (vtriBD >= pDSVaoRa.Count) {
                    kq.Add(ngayCOcheck);
                    ngaydem = ngayCOcheck.NgayCong.AddDays(1d);
                    while (ngaydem <= ngayKT.Date) {
                        cNgayCong ngayKOcheck = new cNgayCong() { NgayCong = ngaydem, HasCheck = false, DSVaoRa = new List<cChkInOut>(), };
                        kq.Add(ngayKOcheck);
                        ngaydem = ngaydem.AddDays(1.0d);
                    }
                    continue;
                }
                // chưa hết DSVàoRa, xem các vào ra kế tiếp, nếu cùng ngày công thì add thêm vào tmpNgayCong1
                while (vtriBD < pDSVaoRa.Count && ThamSo.GetDate(pDSVaoRa[vtriBD].TimeStrDaiDien) == ngayCOcheck.NgayCong) {
                    cChkInOut CIO_1 = pDSVaoRa[vtriBD];
                    XL.TinhCongTheoCa(CIO_1, CIO_1.ThuocCa);

                    #region collapse
                    ngayCOcheck.TongGioThuc += CIO_1.TongGioThuc;
                    ngayCOcheck.TongGioLam += CIO_1.TGLamTinhCong;
                    ngayCOcheck.TongGioLamDem += CIO_1.TGLamDem;
                    ngayCOcheck.TongTre += CIO_1.VaoTre;
                    ngayCOcheck.TongSom += CIO_1.RaaSom;
                    ngayCOcheck.TongCong += CIO_1.Cong;
                    ngayCOcheck.PhuCapDem = ((ngayCOcheck.TongGioLamDem.TotalHours > 0f)
                                                   ? ((float)(ngayCOcheck.TongGioLamDem.TotalHours / 8f) * 0.3f)
                                                   : 0f);
                    #endregion
                    if (CIO_1.QuaDem == true) ngayCOcheck.QuaDem = true; // set qua đêm nếu có

                    if (ngayCOcheck.TinhPC150) {
                        if (CIO.TinhPC150 == false) ngayCOcheck.TinhPC150 = false;

                        if (ngayCOcheck.TinhPC150)
                            ngayCOcheck.PhuCapThem = (((ngayCOcheck.TongGioLam.TotalHours > 8f)
                                                            ? ((((float)ngayCOcheck.TongGioLam.TotalHours - 8f) / 8f) * 0.5f)
                                                            : 0f));
                        else ngayCOcheck.PhuCapThem = 0f;
                    }
                    else ngayCOcheck.PhuCapThem = 0f;

                    ngayCOcheck.TongPhuCap = ngayCOcheck.PhuCapDem + ngayCOcheck.PhuCapThem;

                    ngayCOcheck.them(CIO_1);

                    vtriBD++;
                }
                //thoát khỏi vòng lặp: hoặc hết ds hoặc chuyển sang ngày mới
                if (vtriBD >= pDSVaoRa.Count) {
                    kq.Add(ngayCOcheck);
                    ngaydem = ngayCOcheck.NgayCong.AddDays(1d);
                    while (ngaydem <= ngayKT.Date) {
                        cNgayCong tmpNgayCong = new cNgayCong() { NgayCong = ngaydem, HasCheck = false, DSVaoRa = new List<cChkInOut>(), };
                        kq.Add(tmpNgayCong);
                        ngaydem = ngaydem.AddDays(1.0d);
                    }
                }
                else {
                    kq.Add(ngayCOcheck);
                    ngaydem = ngaydem.AddDays(1.0d);
                }
            }

            return kq;
        }

        public void ClearAll() {
            if (ds_Check_A == null) ds_Check_A = new List<cChk>();
            else ds_Check_A.Clear();
            if (DSVaoRa == null) DSVaoRa = new List<cChkInOut>();
            else DSVaoRa.Clear();
            if (DSNgayCong == null) DSNgayCong = new List<cNgayCong>();
            else DSNgayCong.Clear();

        }




        public override string ToString() {
            return " UEN=" + UserEnrollNumber + "; Ten=" + UserFullName + "__\n";
        }



        // [BackupFunction06]

        public cUserInfo() {
        }


        public float TongCongThang { get; set; }

        public int TongNgayQuaDem { get; set; }

        public float TongCongPhep { get; set; }
        public float TongCongCV { get; set; }

        public float TongCongBH { get; set; }

        public float TongCongH_CT_PT { get; set; }

        public float TongCongRo { get; set; }

        public float TongPCapThang { get; set; }

        public double TienLuong { get; set; }

        public cLuongThang Luong { get; set; }

    }

    public class cLuongThang {
        public double LuongCB { get; set; }
        public double LuongSP { get; set; }
        public double BoiDuongQuaDem { get; set; }
        public double HSLuongSPQuyDoi { get; set; }
        public double LuongThangTruoc { get; set; }
        public double TongLuong { get; set; }
        public cLuongThang() {
            LuongCB = 0d; LuongSP = 0d;
            BoiDuongQuaDem = 0d;
            HSLuongSPQuyDoi = 0d; LuongThangTruoc = 0d; TongLuong = 0d;
        }
    }
}
