using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GiuLaiCacFileCu.DAO;

namespace GiuLaiCacFileCu
{
    public partial class y_testingform_raiseEventForm : Form
    {

        private void XuLyException(Exception ex) {
            if (ex is System.Data.SqlClient.SqlException) {
                MessageBox.Show("Có lỗi trong quá trình kết nối Cơ sở dữ liệu.\nVui lòng liên hệ bộ phận kỹ thuật để được trợ giúp.", "Lỗi", MessageBoxButtons.OK);
                // [ TBD ] [ important ] xóa các dòng dưới trước khi release
                if ((ex as System.Data.SqlClient.SqlException).Data.Contains("incorrect syntax")) { MessageBox.Show("sai cú pháp" + ex.Data.ToString()); }
                if ((ex as System.Data.SqlClient.SqlException).Data.Contains("must declare")) { MessageBox.Show("xem lại tham số.thiếu định nghĩa biến" + ex.Data.ToString()); }
                if ((ex as System.Data.SqlClient.SqlException).Data.Contains("invalid column")) { MessageBox.Show("biến trên form truyền xuống ko khớp kiểu dữ liệu dưới sql" + ex.Data.ToString()); }
                if ((ex as System.Data.SqlClient.SqlException).Data.Contains("not supplied")) { MessageBox.Show("kiểm tra lại giá trị bị null trên form, truyền xuống phải là DBNull.Value" + ex.Data.ToString()); }
                if ((ex as System.Data.SqlClient.SqlException).Data.Contains("would be truncated")) { MessageBox.Show("giá trị truyền xuống vượt ngoài giá trị cho phép dưới sql" + ex.Data.ToString()); }
                this.Close();
                // [ TBD ]
            }
            else if (ex is IndexOutOfRangeException) { 
                MessageBox.Show("Có lỗi trong quá trình kết nối Cơ sở dữ liệu.\nVui lòng liên hệ bộ phận kỹ thuật để được trợ giúp.", "Lỗi", MessageBoxButtons.OK);
                // [ TBD ] [ important ] xóa các dòng dưới trước khi release
                MessageBox.Show("kiểm tra lại số lượng tham số truyền xuống và số lượng giá trị tương ứng");
                this.Close();
                // [ TBD ]
            }
        }


        // ------------------------- xử lý sự kiện phát sinh ----------------------------------
        public y_testingform_raiseEventForm()
        {
            InitializeComponent();
        }

        private void dwf_raiseEventForm_Load(object sender, EventArgs e)
        {
            try {
                listBox1.DataSource = SqlDataAccessHelper.ExecuteQueryString("select * from UserAccount");
            } catch (Exception ex) {
                XuLyException(ex);
                return;
            }
            listBox1.DisplayMember = "UserAccount";
            listBox1.ValueMember = "UserID";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SuaGioHangLoat frmSuaGioHangLoat = new SuaGioHangLoat();
            //frmSuaGioHangLoat.Show();
            //this.Close();
            z_old_v11_DSNV frmDSNV = new z_old_v11_DSNV();
            //frmDSNV.CurrentUserID = (int)listBox1.SelectedValue;
            //if (OnLogInSuccess != null)
            //    OnLogInSuccess(int.Parse(listBox1.SelectedValue.ToString()));
            frmDSNV.ShowDialog();
        }
    }
}
