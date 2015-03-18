using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using log4net;

namespace ChamCong_v03.DTO {

	public class cPhongBan {
		public int ID;
		public string TenPhongBan;
		public int ViTri;
	}

	public class cChkComparer : IComparer<cChk> {
		public int Compare(cChk x, cChk y) {
			return x.Time.CompareTo(y.Time);
			//return 1;
		}
	}
	public class cLoaiVangComparer : IComparer<cLoaiVang> {
		public int Compare(cLoaiVang x, cLoaiVang y) {
			return x.Ngay.CompareTo(y.Ngay);
			//return 1;
		}
	}



	public class cChkInOutComparer : IComparer<cChkInOut> {
		public int Compare(cChkInOut x, cChkInOut y) {
			return x.TimeDaiDien.CompareTo(y.TimeDaiDien);
			//return 1;
		}
	}


	public class cUserInfo {
		public ILog log = LogManager.GetLogger("cUserInfo");

		#region Public Properties

		public string MaNV;
		public string TenNV;
		public int MaCC;
	    public bool IsDailySalary;
	    public bool IsUserEnabled;
		public int UserIDTitle;
		public string TitleName;
		public cPhongBan PBCap1;
		public cPhongBan PBCap2;

		public bool MacDinhTinhPC50;
		public cShiftSchedule LichTrinhLV = new cShiftSchedule();
		public List<cChk> DS_Check_A = new List<cChk>();
		public List<cChk> DS_Check_KoHopLe = new List<cChk>();
		public List<cChkInOut_A> DS_CIO_A = new List<cChkInOut_A>();
		public List<cChkInOut_V> DS_CIO_V = new List<cChkInOut_V>();
		public List<cChkInOut> DSVaoRa = new List<cChkInOut>();
		public List<cNgayCong> DSNgayCong = new List<cNgayCong>();
		public List<cLoaiVang> DSVang = new List<cLoaiVang>();
		public List<cTemp1> DSXNPhuCap50 = new List<cTemp1>();
		public List<cTemp> DSXNPhuCapDB= new List<cTemp>();
		public DateTime NgayCongBD_Bef2D;
		public DateTime NgayCongKT_Aft2D;

		public HeSo HeSo = new HeSo();


		public cChiTietLuong Luong = new cChiTietLuong();

		public TKCongThang Tong = new TKCongThang();

		#endregion





		public override string ToString() {
			return " UEN=" + MaCC + "; Ten=" + TenNV + "__\n";
		}

		public cUserInfo() {
		}

	}
	public struct cChiTietLuong
	{
		public Single MucDongBHXH;
		//-----lương cb, sp theo công, phụ cấp
		public Double LCB_TheoCongVaCV;
		public Double LCB_TheoPCap;
		public Double TongLuongCB_TheoCong;
		public Double TongLuongCB_TheoPCap;
		public Double LSP_TheoCong;
		public Double LSP_TheoPCap;
		public Double TongLuong_TheoCong_Choviec_ThangTruoc;
		public Double TongLuong_TheoPCap_BDCa3;
		public Double TongLuong_NghiDinh205CP;
		public Double TongLuong_TheoHSSP;

		public Double SP_LamRa_TheoCong;
		public Double SP_LamRa_TheoPCap;
		public Double LuongThangTruoc;
		public Double BoiDuongQuaDem;
		//------khấu trừ
		public Double TamUng;
		public Double KhauTruBH;
		public Double ThuChiKhac;
		public Double TongLuong;
		public Double ThucLanh;

	}

	public struct TKCongThang {
		public Double CongThang;
		public Double PCapThang;
		public int NgayQuaDem;
		public Double PC30;
		public Double PC50;
		public Double PCTCC3;
		public Double PC100;
		public Double PC160;
		public Double PC200;
		public Double PC290;
		public Double PCCus;
		public Double Le;
		public Double Phep;
		public Double CongCV;
		public Double BHXH;
		public Double H_CT_PT;
		public Double NghiRo;
	    public Double CongCVTreSom;
	}

	public struct HeSo {
		public Single LuongCB;
		public Single LuongCV;
		public Single BHXH_YT_TN;
	}


	public struct NgayCongChuan {
		public Double Cong50;
		public Double Phep60;
		public Double H_L_CT_PT70;
		public Double Ca3QuaDem80;
		public Double ChoViec90;
	}
	public struct PCLamThemGio
	{
		public Double pc30_100;
		public Double pc50_101;
		public Double pcTCC3_102;
		public Double pc100_110;
		public Double pc160_120;
		public Double pc200_130;
		public Double pc290_140;
		public Double pcCus_150;
	}
	public struct TienLuong {
		public Double LuongCB;
		public Double LuongSP;
		public Double ThangTruoc;
		public Double TongLuong;
	}
	public struct PCLuong {
		public Double TheoLuongCB;
		public Double TheoLuongSP;
		public Double BoiDuongCa3;
		public Double TongLuongPC;
	}

	public struct KhauTru
	{
		public Double TamUng;
		public Double BHXH;
		public Double ThuChiKhac;
	}
}
