using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChamCong_v04.UI.TinhLuong
{
    public partial class frm5NhapNgayCNTrungLe : Form
    {
        public int soNgayCNDieuChinh;
        public frm5NhapNgayCNTrungLe()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            soNgayCNDieuChinh = (int)numericUpDown1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
