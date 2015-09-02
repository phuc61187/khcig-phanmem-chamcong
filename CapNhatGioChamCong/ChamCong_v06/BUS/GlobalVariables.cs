using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamCong_v06.DTO;
using ChamCong_v06.Helper;

namespace ChamCong_v06.BUS {
	public class GlobalVariables
	{
	    public static int CurrentUserID = 0;
	    public static string CurrentUserAccount = string.Empty;
		public static List<cNhomCa> DSNhomCa = new List<cNhomCa>();
		public static readonly TimeSpan _0gio = TimeSpan.Zero;
		public static readonly TimeSpan _01giay = new TimeSpan(0, 0, 1);
		public static readonly TimeSpan _01phut = new TimeSpan(0, 1, 0);
		public static readonly TimeSpan _05phut = new TimeSpan(0, 5, 0);
		public static readonly TimeSpan _10phut = new TimeSpan(0, 10, 0);
		public static readonly TimeSpan _30phut = new TimeSpan(0, 30, 0);
		public static readonly TimeSpan _02gio = new TimeSpan(2, 0, 0);
		public static readonly TimeSpan _03gio = new TimeSpan(3, 0, 0);
		public static readonly TimeSpan _04gio = new TimeSpan(4, 0, 0);
		public static readonly TimeSpan _07gio45ph = new TimeSpan(7, 45, 0);
		public static readonly TimeSpan _08gio = new TimeSpan(8, 0, 0);
		public static readonly TimeSpan _08gio1giay = new TimeSpan(8, 0, 1);
		public static readonly TimeSpan _12gio = new TimeSpan(12, 0, 0);
		public static readonly TimeSpan _16gio = new TimeSpan(16, 0, 0);
		public static readonly TimeSpan _1ngay = new TimeSpan(1, 0, 0, 0);
		public static readonly TimeSpan _04h30 = new TimeSpan(4, 30, 0);
		public static readonly TimeSpan _05h45 = new TimeSpan(5, 45, 0);
		public static readonly TimeSpan _06h00 = new TimeSpan(6, 0, 0);
		public static readonly TimeSpan _07h00 = new TimeSpan(7, 0, 0);
		public static readonly TimeSpan _07h30 = new TimeSpan(7, 30, 0);
		public static readonly TimeSpan _13h45 = new TimeSpan(13, 45, 0);
		public static readonly TimeSpan _18h00 = new TimeSpan(18, 0, 0);
		public static readonly TimeSpan _20h00 = new TimeSpan(20, 0, 0);
		public static readonly TimeSpan _21h45 = new TimeSpan(21, 45, 0);
		public static readonly TimeSpan _22h00 = new TimeSpan(22, 0, 0);
		public static readonly TimeSpan _24h00 = new TimeSpan(24, 0, 0);
		public static readonly FromToTimeSpan NightTime22h = new FromToTimeSpan { From = _22h00, To = _06h00 };

        #region các hệ số phụ cấp áp dụng

        public static int HSPCDem=30;
        public static int HSPCTangCuong=50;
        public static int HSPCThem_NgayThuong=20;
        public static int HSPCNgayNghi=100;
        //public static int HSPCDem;
        public static int HSPCThem_NgayNghi=40;
        public static int HSPCNgayLe=200;
        //public static int HSPCDem;
        public static int HSPCThem_NgayLe=60;

        #endregion

        #region ca tự do
        public static int default_PhutAfterOTMin = -1;
        public static TimeSpan TS_Default_LamThemAfterOT
        {
            get { return default_PhutAfterOTMin == -1 ? _01phut : new TimeSpan(0, default_PhutAfterOTMin, 0); }
        }

        public static int default_PhutLamDemToiThieu = -1;
        public static TimeSpan TS_Default_LamDemToiThieu
        {
            get { return default_PhutLamDemToiThieu == -1 ? _01phut : new TimeSpan(0, default_PhutLamDemToiThieu, 0); }
        }

        public static int default_PhutChoTre = -1;
        public static TimeSpan TS_Default_PhutChoTre
        {
            get { return default_PhutChoTre == -1 ? _01phut : new TimeSpan(0, default_PhutChoTre, 0); }
        }

        public static int default_PhutChoSom = -1;
        public static TimeSpan TS_Default_PhutChoSom
        {
            get { return default_PhutChoSom == -1 ? _01phut : new TimeSpan(0, default_PhutChoSom, 0); }
        }

        public static cCa caTuDo = new cCa{ID = int.MinValue,};
        #endregion

        public static void DocServerSetting5()
        {
            var table = SqlDataAccessHelper.ExecuteQueryString("select * from Setting", null, null);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                var id = (int)row["ID"];
                var code = row["Code"].ToString();
                var value = row["Value"].ToString();

                #region setting phu cap

                if (code == SettingName.HSPCDem.ToString())
                {
                    HSPCDem = int.Parse(value);
                    continue;
                }
                if (code == SettingName.HSPCTangCuong.ToString())
                {
                    HSPCTangCuong = int.Parse(value);
                    continue;
                }
                if (code == SettingName.HSPCThem_NgayThuong.ToString())
                {
                    HSPCThem_NgayThuong = int.Parse(value);
                    continue;
                }
                if (code == SettingName.HSPCNgayNghi.ToString())
                {
                    HSPCNgayNghi = int.Parse(value);
                    continue;
                }
                if (code == SettingName.HSPCThem_NgayNghi.ToString())
                {
                    HSPCThem_NgayNghi = int.Parse(value);
                    continue;
                }
                if (code == SettingName.HSPCNgayLe.ToString())
                {
                    HSPCNgayLe = int.Parse(value);
                    continue;
                }
                if (code == SettingName.HSPCThem_NgayLe.ToString())
                {
                    HSPCThem_NgayLe = int.Parse(value);
                    continue;
                }

                #endregion

                #region số phút cho phép trễ sớm afterot ca tự do
                if (code == SettingName.TGLamDemToiThieu.ToString())
                {
                    TimeSpan temp = TimeSpan.Parse(value);
                    default_PhutLamDemToiThieu = (int)temp.TotalMinutes;
                    continue;
                }
                if (code == SettingName.ChoPhepTre.ToString())
                {
                    default_PhutChoTre = int.Parse(value);
                    continue;
                }
                if (code == SettingName.ChoPhepSom.ToString())
                {
                    default_PhutChoSom = int.Parse(value);
                    continue;
                }
                if (code == SettingName.LamThemAfterOT.ToString())
                {
                    default_PhutAfterOTMin = int.Parse(value);
                    continue;
                }

                #endregion
            }

        }

        public static void SettingCaTuDo()
        {
            caTuDo.AfterOTMin = TS_Default_LamThemAfterOT;
            caTuDo.chophepTreTS = TS_Default_PhutChoTre;
            caTuDo.chophepSomTS = TS_Default_PhutChoSom;
            caTuDo.WorkingTimeTS = _08gio;
            caTuDo.Workingday = 1f;


        }
	}
}
