namespace ChamCong_v05.UI4._5 {
	partial class fmXemCongChiTiet {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("px thành phẩm");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("bảo vệ");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("văn phòng", new System.Windows.Forms.TreeNode[] {
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Nhà máy thuốc lá khánh hội", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3});
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panel1 = new System.Windows.Forms.Panel();
			this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.checkedDSNV = new DevExpress.XtraEditors.CheckedComboBoxEdit();
			this.label1 = new System.Windows.Forms.Label();
			this.dgrdTongHop = new System.Windows.Forms.DataGridView();
			this.btnChamCong = new DevExpress.XtraEditors.SimpleButton();
			this.g1c2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.g2c1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g2c2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g2c3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g2c7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g2c8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g2c4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g2c5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g2c6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel1.SuspendLayout();
			this.gpChonPhongBan.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkedDSNV.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdTongHop)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.gpChonPhongBan);
			this.panel1.Location = new System.Drawing.Point(1, 1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(229, 196);
			this.panel1.TabIndex = 0;
			// 
			// gpChonPhongBan
			// 
			this.gpChonPhongBan.Controls.Add(this.treePhongBan);
			this.gpChonPhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gpChonPhongBan.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.gpChonPhongBan.Location = new System.Drawing.Point(0, 0);
			this.gpChonPhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gpChonPhongBan.Name = "gpChonPhongBan";
			this.gpChonPhongBan.Padding = new System.Windows.Forms.Padding(3, 6, 3, 2);
			this.gpChonPhongBan.Size = new System.Drawing.Size(229, 196);
			this.gpChonPhongBan.TabIndex = 1;
			this.gpChonPhongBan.TabStop = false;
			this.gpChonPhongBan.Text = "Chọn phòng ban";
			// 
			// treePhongBan
			// 
			this.treePhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treePhongBan.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.treePhongBan.Indent = 18;
			this.treePhongBan.ItemHeight = 20;
			this.treePhongBan.Location = new System.Drawing.Point(3, 21);
			this.treePhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.treePhongBan.Name = "treePhongBan";
			treeNode1.Name = "Node2";
			treeNode1.Text = "px thành phẩm";
			treeNode2.Name = "Node5";
			treeNode2.Text = "bảo vệ";
			treeNode3.Name = "Node4";
			treeNode3.Text = "văn phòng";
			treeNode4.Name = "Node0";
			treeNode4.Text = "Nhà máy thuốc lá khánh hội";
			this.treePhongBan.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
			this.treePhongBan.ShowNodeToolTips = true;
			this.treePhongBan.Size = new System.Drawing.Size(223, 173);
			this.treePhongBan.TabIndex = 0;
			// 
			// checkedDSNV
			// 
			this.checkedDSNV.Location = new System.Drawing.Point(3, 218);
			this.checkedDSNV.Name = "checkedDSNV";
			this.checkedDSNV.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.checkedDSNV.Properties.Appearance.Options.UseFont = true;
			this.checkedDSNV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear, "", 20, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.checkedDSNV.Size = new System.Drawing.Size(189, 20);
			this.checkedDSNV.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label1.Location = new System.Drawing.Point(3, 200);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(103, 14);
			this.label1.TabIndex = 2;
			this.label1.Text = "Chọn nhân viên";
			// 
			// dgrdTongHop
			// 
			this.dgrdTongHop.AllowUserToAddRows = false;
			this.dgrdTongHop.AllowUserToDeleteRows = false;
			this.dgrdTongHop.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
			this.dgrdTongHop.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgrdTongHop.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgrdTongHop.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dgrdTongHop.ColumnHeadersHeight = 27;
			this.dgrdTongHop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgrdTongHop.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.g1c2,
            this.g1c1,
            this.g1c3,
            this.g1c4,
            this.g1c5,
            this.g1c6,
            this.g1c8,
            this.g1c24,
            this.g1c25});
			this.dgrdTongHop.GridColor = System.Drawing.Color.White;
			this.dgrdTongHop.Location = new System.Drawing.Point(245, 11);
			this.dgrdTongHop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dgrdTongHop.Name = "dgrdTongHop";
			this.dgrdTongHop.RowHeadersVisible = false;
			this.dgrdTongHop.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdTongHop.Size = new System.Drawing.Size(737, 203);
			this.dgrdTongHop.TabIndex = 27;
			// 
			// btnChamCong
			// 
			this.btnChamCong.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnChamCong.Appearance.Options.UseFont = true;
			this.btnChamCong.Location = new System.Drawing.Point(12, 257);
			this.btnChamCong.Name = "btnChamCong";
			this.btnChamCong.Size = new System.Drawing.Size(103, 46);
			this.btnChamCong.TabIndex = 28;
			this.btnChamCong.Text = "Chấm công";
			this.btnChamCong.Click += new System.EventHandler(this.btnChamCong_Click);
			// 
			// g1c2
			// 
			this.g1c2.DataPropertyName = "UserEnrollNumber";
			dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.g1c2.DefaultCellStyle = dataGridViewCellStyle2;
			this.g1c2.HeaderText = "Mã CC_hide";
			this.g1c2.Name = "g1c2";
			this.g1c2.ReadOnly = true;
			this.g1c2.ToolTipText = "Mã chấm công";
			this.g1c2.Visible = false;
			this.g1c2.Width = 45;
			// 
			// g1c1
			// 
			this.g1c1.DataPropertyName = "UserFullCode";
			this.g1c1.HeaderText = "Mã NV";
			this.g1c1.Name = "g1c1";
			this.g1c1.ReadOnly = true;
			this.g1c1.ToolTipText = "Mã Nhân viên";
			this.g1c1.Width = 55;
			// 
			// g1c3
			// 
			this.g1c3.DataPropertyName = "UserFullName";
			this.g1c3.HeaderText = "Tên NV";
			this.g1c3.Name = "g1c3";
			this.g1c3.ReadOnly = true;
			this.g1c3.ToolTipText = "Tên Nhân viên";
			this.g1c3.Width = 150;
			// 
			// g1c4
			// 
			this.g1c4.HeaderText = "Ngày";
			this.g1c4.Name = "g1c4";
			this.g1c4.Width = 85;
			// 
			// g1c5
			// 
			this.g1c5.HeaderText = "T.Công";
			this.g1c5.Name = "g1c5";
			this.g1c5.Width = 50;
			// 
			// g1c6
			// 
			this.g1c6.HeaderText = "T.PC";
			this.g1c6.Name = "g1c6";
			this.g1c6.Width = 45;
			// 
			// g1c8
			// 
			this.g1c8.HeaderText = "Giờ làm";
			this.g1c8.Name = "g1c8";
			this.g1c8.Width = 55;
			// 
			// g1c24
			// 
			this.g1c24.DataPropertyName = "cUserInfo";
			this.g1c24.HeaderText = "cUserInfo_hide";
			this.g1c24.Name = "g1c24";
			this.g1c24.ReadOnly = true;
			this.g1c24.Visible = false;
			// 
			// g1c25
			// 
			this.g1c25.DataPropertyName = "IsEdited";
			this.g1c25.HeaderText = "IsEdited_hide";
			this.g1c25.Name = "g1c25";
			this.g1c25.ReadOnly = true;
			this.g1c25.Visible = false;
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
			this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dataGridView1.ColumnHeadersHeight = 27;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.g2c1,
            this.g2c2,
            this.g2c3,
            this.g2c7,
            this.g2c8,
            this.g2c4,
            this.g2c5,
            this.g2c6});
			this.dataGridView1.GridColor = System.Drawing.Color.White;
			this.dataGridView1.Location = new System.Drawing.Point(245, 238);
			this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(1037, 203);
			this.dataGridView1.TabIndex = 27;
			// 
			// g2c1
			// 
			this.g2c1.HeaderText = "Vào";
			this.g2c1.Name = "g2c1";
			this.g2c1.Width = 140;
			// 
			// g2c2
			// 
			this.g2c2.HeaderText = "Ra";
			this.g2c2.Name = "g2c2";
			this.g2c2.Width = 140;
			// 
			// g2c3
			// 
			this.g2c3.HeaderText = "Ca";
			this.g2c3.Name = "g2c3";
			// 
			// g2c7
			// 
			this.g2c7.HeaderText = "Bắt đầu ca";
			this.g2c7.Name = "g2c7";
			// 
			// g2c8
			// 
			this.g2c8.HeaderText = "Kết thúc ca";
			this.g2c8.Name = "g2c8";
			// 
			// g2c4
			// 
			this.g2c4.HeaderText = "Giờ làm";
			this.g2c4.Name = "g2c4";
			this.g2c4.Width = 55;
			// 
			// g2c5
			// 
			this.g2c5.HeaderText = "G.Làm ca3";
			this.g2c5.Name = "g2c5";
			this.g2c5.Width = 65;
			// 
			// g2c6
			// 
			this.g2c6.HeaderText = "Làm thêm";
			this.g2c6.Name = "g2c6";
			this.g2c6.Width = 65;
			// 
			// fmXemCongChiTiet
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1344, 622);
			this.Controls.Add(this.btnChamCong);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.dgrdTongHop);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkedDSNV);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "fmXemCongChiTiet";
			this.Text = "Xem Chi tiet Cong";
			this.Load += new System.EventHandler(this.fmXemCong4_Load);
			this.panel1.ResumeLayout(false);
			this.gpChonPhongBan.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkedDSNV.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdTongHop)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox gpChonPhongBan;
		private System.Windows.Forms.TreeView treePhongBan;
		private DevExpress.XtraEditors.CheckedComboBoxEdit checkedDSNV;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView dgrdTongHop;
		private DevExpress.XtraEditors.SimpleButton btnChamCong;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c2;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c1;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c3;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c4;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c5;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c6;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c8;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c24;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c25;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn g2c1;
		private System.Windows.Forms.DataGridViewTextBoxColumn g2c2;
		private System.Windows.Forms.DataGridViewTextBoxColumn g2c3;
		private System.Windows.Forms.DataGridViewTextBoxColumn g2c7;
		private System.Windows.Forms.DataGridViewTextBoxColumn g2c8;
		private System.Windows.Forms.DataGridViewTextBoxColumn g2c4;
		private System.Windows.Forms.DataGridViewTextBoxColumn g2c5;
		private System.Windows.Forms.DataGridViewTextBoxColumn g2c6;
	}
}