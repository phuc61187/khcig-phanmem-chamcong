using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GiuLaiCacFileCu.DAO;

namespace GiuLaiCacFileCu {
    public partial class XuatReport : Form {
        public XuatReport() {
            InitializeComponent();
        }

        private void XuatReport_Load(object sender, EventArgs e) {
            try {
                SqlDataAccessHelper.ConnectionString = SqlDataAccessHelper.ReadEncryptConnectionString1(@"C:\PathDataPage.txt");
                DataTable dataTable = SqlDataAccessHelper.ExecuteQueryString("select * from UserInfo");
                 Export(dataTable);
            } catch (Exception) {
                Application.Exit();
                throw;
            }

        }


        private void Export(DataTable dataTable) {

        }


    }
}



