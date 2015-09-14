using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.BUS;
using ChamCong_v06.DTO;
using ChamCong_v06.Helper;

namespace ChamCong_v06.UI {
	public partial class zTestForm : Form
	{
		public List<cUserInfo> m_DSNV;
		public FromToDateTime KhoangTG;
		public zTestForm() {
			InitializeComponent();
		}

		private void zTestForm_Load(object sender, EventArgs e)
		{
			//DataTable tableCheck = SqlDataAccessHelper.ExecuteQueryString("select * from CheckInOut ");
			//gridControl1.DataSource = tableCheck;

			BUS.BUS_ChamCong busChamCong = new BUS_ChamCong();
			busChamCong.XemCong(m_DSNV, KhoangTG );
			var result = (from cNgayCong ngayCong in m_DSNV[0].DSNgayDaCC
			              select new
				              {
								  m_DSNV[0].MaCC,
					              ngayCong.Ngay,
					              ngayCong.Tre,
					              ngayCong.Som,
					              ngayCong.VaoSauCa,
					              ngayCong.RaTruocCa,
					              LamViec = ngayCong.LamViec.ToString(@"hh\:mm"),
					              LamDem = ngayCong.LamDem.ToString(@"hh\:mm"),
					              ngayCong.PhuCapDem,
								  ngayCong.DinhMuc,
								  ngayCong.Tong,
								  ngayCong.TongPhuCap
				              }).ToList();
			gridControl2.DataSource = result;
			DataTable tableCIO = SqlDataAccessHelper.ExecuteQueryString("Select * from CIO");
			gridControl1.DataSource = tableCIO;

		}
	}
}
