using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GiuLaiCacFileCu.DAO;

namespace GiuLaiCacFileCu {
	public partial class frm_ChamCongCaDai : Form {
		public DataTable dataTableNV { get; set; }
		public List<int> lstPhongBan { get; set; }
		public List<int> lstNVChamCaDai { get; set; }
		public DateTime NgayChamCaDai { get; set; }
		public bool AutoSet { get; set; }
		public DateTime TuGio { get; set; }
		public DateTime DenGio { get; set; }

		private string SelStr_GetDSNVThaoTac() {
			string selectQueryString = String.Empty;
			selectQueryString += @" select UserInfo.UserFullCode, UserInfo.UserFullName, UserInfo.UserEnrollNumber
                                    , UserInfo.SchID, Schedule.SchName
                                    , Title.TitleName
                                    , UserInfo.UserIDD, RelationDept.[Description]	 ";
			selectQueryString += @" from UserInfo , RelationDept, Schedule, Title ";
			selectQueryString += @" where UserInfo.UserIDD IN ( select dp.IDD from DeptPrivilege dp 
                                                         where dp.UserID = @UserID and IsYes = @IsYes )
			                       and (RelationDept.ID = UserInfo.UserIDD)
                                   and (UserInfo.SchID = Schedule.SchID)
                                   and (UserIDTitle = IDT)

                                    union  select UserInfo.UserFullCode, UserInfo.UserFullName, UserInfo.UserEnrollNumber
                                                                        , UserInfo.SchID, Schedule.SchName
                                                                        , N'Chưa SX'
                                                                        , UserInfo.UserIDD, RelationDept.[Description]	 
                                    from UserInfo , RelationDept, Schedule where UserInfo.UserIDD IN ( select dp.IDD from DeptPrivilege dp 
                                                                                             where dp.UserID = @UserID and IsYes = @IsYes )
			                                                           and (RelationDept.ID = UserInfo.UserIDD)
                                                                       and (UserInfo.SchID = Schedule.SchID) and UserIDTitle = 0  ";
			return selectQueryString;
		}

		#region load treeview
		public TreeView loadTreeNode(TreeView tvDSPhongBan, DataTable pDataTable) {
			if (pDataTable.Rows.Count > 0) {
				foreach (DataRow dataRow in pDataTable.Select("RelationID = 0")) {
					TreeNode ParentNode = new TreeNode { Text = dataRow["Description"].ToString(), Tag = (int)dataRow["ID"] };
					tvDSPhongBan.Nodes.Add(ParentNode);
					loadTreeSubNode(ref ParentNode, (int)(dataRow["ID"]), pDataTable);
				}
			}
			return tvDSPhongBan;
		}

		private void loadTreeSubNode(ref TreeNode ParentNode, int ParentId, DataTable dtMenu) {
			DataRow[] childs = dtMenu.Select("RelationID='" + ParentId + "'");
			foreach (DataRow dRow in childs) {
				TreeNode child = new TreeNode { Text = dRow["Description"].ToString(), Tag = (int)dRow["ID"], ToolTipText = dRow["Description"].ToString() };
				ParentNode.Nodes.Add(child);
				//Recursion Call
				loadTreeSubNode(ref child, (int)dRow["ID"], dtMenu);
			}
		}

		void GetNodeID(TreeNode root) {
			if (root == null) return;
			if (root.FirstNode == null) {
				lstPhongBan.Add((int)root.Tag);
				root = root.NextNode;
				return;
			}
			if (root.FirstNode != null) {
				foreach (TreeNode treeNode in root.Nodes)
					GetNodeID(treeNode);
			}
		}

		void LoadTreePhongBan() {
			treePhongBan.Nodes.Clear();
            loadTreeNode(treePhongBan, ThamSo.TablePhongBan);
		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			lstPhongBan.Clear();
			if (e.Node.FirstNode != null) GetNodeID(e.Node);
			else lstPhongBan.Add((int)e.Node.Tag);
			e.Node.Expand();

			string temp = "UserIDD = {0}";
			temp = String.Format(temp, String.Join(" or UserIDD = ", lstPhongBan.ToArray()));
			DataView dataView = new DataView(dataTableNV, temp, "", DataViewRowState.CurrentRows);
			dataGridDSNVTrongPhg.DataSource = dataView;
		}
		void LoadTableDSNV() {

			if (dataTableNV != null) {
				dataTableNV.Clear();
			}
			if (dataTableNV == null || dataTableNV.Rows.Count == 0) {
				dataTableNV = ThamSo.DataTableDSNV.Copy();
				dataTableNV.Columns.Add("check", typeof(bool));
			}
		}


		#endregion


		public frm_ChamCongCaDai() {
			InitializeComponent();
		}

		private void ChamCongCaDai_Load(object sender, EventArgs e) {
			lstPhongBan = new List<int>();
			lstNVChamCaDai = new List<int>();
			TuGio = DenGio = DateTime.MinValue;

			treePhongBan.AfterSelect -= treePhongBan_AfterSelect;
			LoadTreePhongBan();
			LoadTableDSNV();
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;
			treePhongBan.SelectedNode = treePhongBan.TopNode;

		}

		private void btnMove_Click(object sender, EventArgs e) {
			if (dataGridDSNVTrongPhg.SelectedRows.Count != 0) {
				DataTable desTable = dataGridView1.DataSource as DataTable;
				DataView sourceView = dataGridDSNVTrongPhg.DataSource as DataView;

				if (sourceView == null) return;
				DataTable sourceTable = sourceView.Table;
				if (sourceTable == null) return;
				sourceTable.PrimaryKey = new DataColumn[] { sourceTable.Columns["UserEnrollNumber"] };
				if (desTable == null) desTable = sourceTable.Clone();
				foreach (DataGridViewRow selectedRow in dataGridDSNVTrongPhg.SelectedRows) {
					int tempUserEnrollNumber = (int)selectedRow.Cells["cUserEnrollNumberDSNV2"].Value;
					DataRow sourceRow = sourceTable.Rows.Find(selectedRow.Cells["cUserEnrollNumberDSNV2"].Value);
					DataRow desRow = desTable.Rows.Find(selectedRow.Cells["cUserEnrollNumberDSNV2"].Value);
					if (desRow != null) continue;
					else {
						desRow = desTable.NewRow();
						desRow.ItemArray = sourceRow.ItemArray;
						desTable.Rows.Add(desRow);
					}
				}
				dataGridView1.DataSource = desTable;
			}
		}

		private void btnRemove_Click(object sender, EventArgs e) {
			if (dataGridView1.SelectedRows.Count != 0) {
				DataTable desTable = dataGridView1.DataSource as DataTable;
				if (desTable == null) return;
				foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows) {
					DataRow desRow = desTable.Rows.Find(selectedRow.Cells["dataGridViewTextBoxColumn2"].Value);
					desTable.Rows.Remove(desRow);
				}
			}
		}

		private void btnHuy_Click(object sender, EventArgs e) {
			this.Close();
			GC.Collect();
		}

		private void btnDongY_Click(object sender, EventArgs e) {
			//[TBD] tạm thời autoset = true
			AutoSet = false;
			if (AutoSet == false) {
				TuGio = new DateTime(2013, 8, 1, 12, 0, 0, 0);
				DenGio = new DateTime(2013, 8, 1, 23, 59, 0, 0);
			}

			foreach (DataGridViewRow selectedRow in dataGridView1.Rows)
				lstNVChamCaDai.Add((int)selectedRow.Cells["dataGridViewTextBoxColumn2"].Value);
			foreach (var i in lstNVChamCaDai) {
				Debug.WriteLine(" nv cham ca dai " + i);
			}
			NgayChamCaDai = dateTimeNgayChamCaDai.Value;
			Debug.WriteLine(NgayChamCaDai.ToString());
			this.Close();

		}
	}
}
