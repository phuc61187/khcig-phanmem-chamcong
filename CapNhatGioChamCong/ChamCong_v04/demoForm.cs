using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChamCong_v04
{
    public partial class demoForm : Form
    {
        public demoForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = string.Empty;
            DateTime ngayCuoiKy = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 25);
            textBox1.Text += string.Format("CK: {0}/{1}/{2}\n     ", ngayCuoiKy.Day, ngayCuoiKy.Month, ngayCuoiKy.Year);
            DateTime ngayDauKy = ngayCuoiKy.AddMonths(-1).AddDays(1);
            textBox1.Text += string.Format("\nCK: {0}/{1}/{2}\n   ", ngayCuoiKy.Day, ngayCuoiKy.Month, ngayCuoiKy.Year);
            textBox1.Text += string.Format("\nDK: {0}/{1}/{2}\n   ", ngayDauKy.Day, ngayDauKy.Month, ngayDauKy.Year);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
