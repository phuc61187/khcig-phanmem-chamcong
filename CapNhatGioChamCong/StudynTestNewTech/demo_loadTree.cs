using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using TreeView = System.Windows.Forms.TreeView;

namespace StudynTestNewTech
{
    public partial class demo_loadTree : Form
    {
        public demo_loadTree()
        {
            InitializeComponent();

            SqlDataAccessHelper.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=WiseEyeV5Express;Integrated Security=true";

        }

        private void demo_loadTree_Load(object sender, EventArgs e)
        {
            var tablePB = SqlDataAccessHelper.ExecuteQueryString("select * from RelationDept");
            loadTreePhgBan(treeView1, tablePB);
            treeView1.AfterSelect += treePhongBan_AfterSelect;
        }

        public TreeView loadTreePhgBan(TreeView tvDSPhongBan, DataTable pDataTable)
        {
            if (pDataTable.Rows.Count > 0)
            {
                foreach (var dataRow in pDataTable.Select("RelationID = 0", "ViTri asc"))
                {
                    var ParentNode = new TreeNode { Text = dataRow["Description"].ToString(), Tag = (int)dataRow["ID"] };
                    tvDSPhongBan.Nodes.Add(ParentNode);
                    loadTreeSubNode(ref ParentNode, (int)(dataRow["ID"]), pDataTable);
                }
            }
            return tvDSPhongBan;
        }

        private void loadTreeSubNode(ref TreeNode ParentNode, int ParentId, DataTable dtMenu)
        {
            var childs = dtMenu.Select("RelationID = " + ParentId, "ViTri asc");
            foreach (var dRow in childs)
            {
                var child = new TreeNode { Text = dRow["Description"].ToString(), Tag = (int)dRow["ID"], ToolTipText = dRow["Description"].ToString() };
                ParentNode.Nodes.Add(child);
                //Recursion Call
                loadTreeSubNode(ref child, (int)dRow["ID"], dtMenu);
            }
        }

        private void GetIDLeafNodeExceptParent(TreeNode root,ref List<int> listID)
        {
            if (root == null) return;

            listID.Add((int)root.Tag);

            if (root.Nodes.Count > 0)
            {
                foreach (TreeNode node in root.Nodes)
                {
                    GetIDLeafNodeExceptParent(node,ref listID);
                }
            }
            // xuốn đến đây tương đương root.Nodes.Count== 0; return
        }

        List<int> dsmaphong = new List<int>();
        private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dsmaphong.Clear();
            if (e.Node.FirstNode != null) GetIDLeafNodeExceptParent(e.Node, ref dsmaphong);
            else dsmaphong.Add((int)e.Node.Tag);
            e.Node.Expand();
            var temp = string.Empty;
            temp = dsmaphong.Aggregate(temp, (current, @out) => current + @out.ToString() + ", ");
            richTextBox1.Text += "\n" + temp;
            /*
                        if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false)
                        {
                            AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 4000);
                            this.Close();
                            return;
                        }
                        var table = DAL.LayDSNV(m_listIDPhongBan.ToArray());
                        if (table.Rows.Count == 0) return;
                        m_DSNV.Clear();
                        XL.KhoiTaoDSNV(m_DSNV, table);
                        m_Bang_DSNV.Rows.Clear();
                        XL.TaoTableDSNV(m_DSNV, m_Bang_DSNV);
                        var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
                        var Source = new AutoCompleteStringCollection();
                        Source.AddRange((from nv in m_DSNV select nv.TenNV.ToUpperInvariant()).ToArray());
                        tbSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        tbSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        tbSearch.AutoCompleteCustomSource = Source;
                        dataView.RowFilter = string.Empty;
                        m_Bang_TongHopXemCong.Rows.Clear();
                        m_Bang_GioKDQD.Rows.Clear();
                        m_Bang_GioThieuCheck.Rows.Clear();
                        m_Bang_ThK_TreSom.Rows.Clear();
                        checkAll_GridDSNV.Checked = false;*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("phuc61187@gmail.com", "rubynhi");
                MailMessage msg = new MailMessage();
                msg.To.Add("lehoangphuc87@gmail.com");
                msg.From = new MailAddress("phuc61187@gmail.com");
                msg.Subject = "subject";
                var temp = ConvertDataTableToHTMLTable();
                msg.Body = temp;
                //Attachment data = new Attachment(textBox_Attachment.Text);
                //msg.Attachments.Add(data);
                client.Send(msg);
                MessageBox.Show("Successfully Sent Message.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string ConvertDataTableToHTMLTable()
        {
            DataTable dt = SqlDataAccessHelper.ExecuteQueryString("select * from RelationDept");

         string messageBody = "<font>The following are the records: </font><br><br>";

         if (dt.Rows.Count == 0)
             return messageBody;
         string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
         string htmlTableEnd = "</table>";
         string htmlHeaderRowStart = "<tr style =\"background-color:#6FA1D2; color:#ffffff;\">";
         string htmlHeaderRowEnd = "</tr>";
         string htmlTrStart = "<tr style =\"color:#555555;\">";
         string htmlTrEnd = "</tr>";
         string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
         string htmlTdEnd = "</td>";

         messageBody+= htmlTableStart;
         messageBody += htmlHeaderRowStart;
         messageBody += htmlTdStart + "Description " + htmlTdEnd;
         messageBody += htmlHeaderRowEnd;

         foreach (DataRow Row in dt.Rows)
         {
             messageBody = messageBody + htmlTrStart;
             messageBody = messageBody + htmlTdStart + Row["Description"] + htmlTdEnd;
             messageBody = messageBody + htmlTrEnd;
         }
         messageBody = messageBody + htmlTableEnd;


         return messageBody;
        }

    }
}
