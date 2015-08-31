using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamCong_v06.Helper {
	public struct ID_Description
	{
		public int ID;
		public string Description;
	}
	public struct FromToTime
	{
		public DateTime From;
		public DateTime To;
	}

	#region UserInfo
	public struct HeSo {
		public float LuongCB;
		public float LuongCV;
		//public float BHXH_YT_TN;
		public float BHCongThem_ChoGD_PGD;

		//private float baoHiemXaHoi_YTe_ThatNghiep;
		public float BHXH_YT_TN {
			get { return LuongCB + BHCongThem_ChoGD_PGD; }
		}
	}

	#endregion
	public class ThoiDiem {
		public DateTime BD_LV;// vào làm ca
		public DateTime KT_LV_TrongCa;
		public DateTime KT_LV;// 
		public DateTime BD_LV_Ca3;
		public DateTime KT_LV_Ca3;
	}

	public struct StructTGCa
	{
		public TimeSpan HienDien;
		public TimeSpan TreVR;
		public TimeSpan SomVR;
		public TimeSpan LamViec;
		public TimeSpan LamDem;
		public TimeSpan OLaiVR;
		public TimeSpan LamTrongGio;
		public TimeSpan LamNgoaiGio;
		public TimeSpan VaoSauCa;
		public TimeSpan RaTruocCa;

	}
	public struct StructTGNgay
	{
		public TimeSpan HienDien;
		public TimeSpan TreVR;
		public TimeSpan SomVR;
		public TimeSpan VaoSauCa;
		public TimeSpan RaTruocCa;
		public TimeSpan LamViec;
		public TimeSpan LamDem;
		public TimeSpan LamNgay;//= lamViec - lamDem
		public TimeSpan LamThem;//= lamViec - 8
		public TimeSpan LamTCDem;//lamViec >8, LamDem >0, = LamThem
	}
	public struct StructTGThang
	{
		public TimeSpan TreVR;
		public TimeSpan SomVR;
		public TimeSpan VaoSauCa;
		public TimeSpan RaTruocCa;
		public TimeSpan LamViec;
		public TimeSpan LamDem;
		public TimeSpan LamNgay;
		public TimeSpan LamThem;
		public TimeSpan LamTCDem;
	}
	public struct StructCongCa
	{
		public float TrongGio;
		public float NgoaiGio;
		public float DinhMuc;
		public float Tong;
	}
	public struct StructCongNgay
	{
		public float DinhMuc;
		public float Tong;
		public float NghiVR;
		public float Phep;
		public float HocHop;
		public float CongTac;
		public float PhongTrao;
		public float BHXH;
		public float ThaiSan;
		public float LeTet;
	}
	public struct StructCongThang
	{
		public float NghiCoCC;//phép, học, họp, lễ, tết, phong trào
		public float NghiKoCC;//nghỉ bhxh, thai sản, ro
		public float NghiVR;
		public float Phep;
		public float HocHop;
		public float CongTac;
		public float PhongTrao;
		public float BHXH;
		public float ThaiSan;
		public float LeTet;
		public float CongCV;
		public float DinhMuc;
		public float Tong;
	}

	public struct StructPCNgay
	{
		public float Dem;
		public float TC;
		public float ThemNgayThuong;
		public float NgayNghi;
		public float ThemNgayNghi;
		public float NgayLeTet;
		public float ThemNgayLeTet;
	}

	public struct StructPCThang
	{
		public float Dem;
		public float TC;
		public float ThemNgayThuong;
		public float NgayNghi;
		public float ThemNgayNghi;
		public float NgayLeTet;
		public float ThemNgayLeTet;
	}
}
