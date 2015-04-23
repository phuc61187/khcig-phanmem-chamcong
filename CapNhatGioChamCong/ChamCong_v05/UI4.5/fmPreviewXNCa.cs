using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.DTO;

namespace ChamCong_v05.UI4._5 {
	public partial class fmPreviewXNCa : Form {
		public List<DataRow> m_List_CIO_Input = new List<DataRow>();
		public cCa m_Ca;
		public bool m_CheckVaoTreTinhCV, m_CheckRaaSomTinhCV, m_ChoPhepVaoTre, m_ChoPhepRaaSom;
		public TimeSpan m_SoPhutLamThem;
		//public float 
		public fmPreviewXNCa() {
			InitializeComponent();
		}

		public void ValidateCIO(List<DataRow> List_CIO_Input, cCa ca, out List<DataRow> List_CIO_HaveError, out List<DataRow> List_CIO_HopLe) {
			List_CIO_HaveError = new List<DataRow>();
			List_CIO_HopLe = new List<DataRow>();
			foreach (DataRow row in List_CIO_Input) {
				int kq = ValidateCIO(row, ca, m_CheckVaoTreTinhCV, m_CheckRaaSomTinhCV, m_ChoPhepVaoTre,m_ChoPhepRaaSom,m_SoPhutLamThem);
				if (kq < 0) List_CIO_HaveError.Add(row);
				else List_CIO_HaveError.Add(row);
			}
			return;
		}

		public int ValidateCIO(DataRow row, cCa CaDuocChon, bool CheckVaoTreTinhCV, bool CheckRaaSomTinhCV, bool CheckChoPhepTre, bool CheckChoPhepSom, TimeSpan soPhutLamThemDaXN) {
			/* -1 : Làm thêm > Ở lại*/
			int kq = 0;
			cCheckInOut cio = (cCheckInOut)row["cCheckInOut"];
			if (soPhutLamThemDaXN > cio.TG5.OLai) return -1; // 
			TimeSpan OnnDuty = TimeSpan.Zero,OffDuty = TimeSpan.Zero;
			if (cio.ShiftID < 0)
			{
				OnnDuty = new TimeSpan(cio.Vao.Time.TimeOfDay.Hours, cio.Vao.Time.TimeOfDay.Minutes, 0);
				
			}
			//var temp1 = cio.ShiftID < 0 ? cio.Vao.Time.TimeOfDay cio.ThuocNgayCong.Add(CaDuocChon.Duty.Onn);
			var temp2 = cio.ThuocNgayCong.Add(CaDuocChon.Duty.Off);
			DateTime TD_BD_LV, TD_KT_LV, TD_KT_LV_TrongCa, TD_BD_LV_Ca3, TD_KT_LV_Ca3;
			bool QuaDem;
			TimeSpan TGThucTe, TGGioLamViec, TGVaoTre, TGRaaSom, TGGioLamViecTrongCa, TGOLai, TGLamBanDem;

			XL.TinhTG_LV_LVCa3_LamThem_1CIO5(cio.ThuocNgayCong, cio.HaveINOUT, true, CheckChoPhepTre, CheckChoPhepSom, cio.Vao.Time, cio.Raa.Time,
				CaDuocChon.Duty.Onn, CaDuocChon.Duty.Off, CaDuocChon.chophepTreTS, CaDuocChon.chophepSomTS, CaDuocChon.batdaulamthemTS, CaDuocChon.LunchMin,
				soPhutLamThemDaXN,
				XL2._22h00, XL2._06h00, //tbd start NT, endNT
				out TD_BD_LV, out TD_KT_LV, out  TD_KT_LV_TrongCa, out TD_BD_LV_Ca3, out TD_KT_LV_Ca3, out TGThucTe, out TGGioLamViec,
				out TGVaoTre, out TGRaaSom, out TGGioLamViecTrongCa, out TGOLai, out QuaDem, out TGLamBanDem
			);
			if (TGGioLamViec < XL2._10phut) return -2;
			float congCaQuyDinh, congTre, congSom, congThucTeTrongCa, congThucTeNgoaiCa, congThucTe, tongCongBu, tongCongTru, dinhMucCong;
			XL.TinhCong_1_CIO_5(CaDuocChon.Workingday, CaDuocChon.WorkingTimeTS, TGVaoTre, TGRaaSom,
				CheckVaoTreTinhCV, CheckRaaSomTinhCV, soPhutLamThemDaXN, out congCaQuyDinh, out congTre, out congSom,
				out congThucTeTrongCa, out congThucTeNgoaiCa, out congThucTe, out tongCongBu, out tongCongTru, out dinhMucCong);

			if (Math.Abs(congThucTe - 0f) < 0.001f) return -2;
			return kq;
		}
	}
}
