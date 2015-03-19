using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v04.UI.XepLich;

namespace ChamCong_v04 {
	public partial class testForm : Form {
		public testForm() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DateTime ngayBD = dtpBD.Value;
			DateTime ngayKT = dtpKT.Value;
			List<List<DateTime>> arr, tgDaKetCong, tgChuaKetCong;
			fmTKeCongTheoNVu.ChiaDoanThoiGian(ngayBD, ngayKT, out arr);
			fmTKeCongTheoNVu.TachKC_ChuaKC(arr, out tgChuaKetCong, out tgDaKetCong);
			string template1 = "From [{0}] To [{1}]   = [{2}] -> [{3}]\n";
			richTextBox1.Clear();
			for (int i = 0; i < arr.Count; i++)
			{
				List<DateTime> x = arr[i];
				richTextBox1.Text += string.Format(template1, ngayBD.ToString("dd/MM/yy"), ngayKT.ToString("dd/MM/yy"),
					x[0].ToString("dd/MM/yy"),x[1].ToString("dd/MM/yy"));
			}
			richTextBox1.Text += "Tình hình kết công: \n";
			string template2 = "Đã kết công từ [{0}] --> [{1}]\n";
			for (int i = 0; i < tgDaKetCong.Count; i++)
			{
				List<DateTime> x = tgDaKetCong[i];
				richTextBox1.Text += string.Format(template2, x[0].ToString("dd/MM/yy"), x[1].ToString("dd/MM/yy"));
			}
			string template3 = "Chưa kết công từ [{0}] --> [{1}]\n";
			for (int i = 0; i < tgChuaKetCong.Count; i++)
			{
				List<DateTime> x = tgChuaKetCong[i];
				richTextBox1.Text += string.Format(template3, x[0].ToString("dd/MM/yy"), x[1].ToString("dd/MM/yy"));
			}
		}
	}
}
