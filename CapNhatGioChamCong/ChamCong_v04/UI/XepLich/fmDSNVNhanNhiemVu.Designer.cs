namespace ChamCong_v04.UI.XepLich {
	partial class fmDSNVNhanNhiemVu {
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("px thành phẩm");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("bảo vệ");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("văn phòng", new System.Windows.Forms.TreeNode[] {
            treeNode6});
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Nhà máy thuốc lá khánh hội", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode7});
			this.gp1 = new System.Windows.Forms.GroupBox();
			this.dgrdDSNVNhanNhiemVu = new System.Windows.Forms.DataGridView();
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnDangKyNhiemVu = new System.Windows.Forms.Button();
			this.btnHuyNhiemVu = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.checkListNhiemVu = new System.Windows.Forms.CheckedListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.toolTipHint = new System.Windows.Forms.ToolTip(this.components);
			this.gp1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVNhanNhiemVu)).BeginInit();
			this.gpChonPhongBan.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gp1
			// 
			this.gp1.Controls.Add(this.dgrdDSNVNhanNhiemVu);
			this.gp1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.gp1.Location = new System.Drawing.Point(248, 36);
			this.gp1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gp1.Name = "gp1";
			this.gp1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gp1.Size = new System.Drawing.Size(514, 455);
			this.gp1.TabIndex = 0;
			this.gp1.TabStop = false;
			this.gp1.Text = "Danh sách Nhân viên nhận nhiệm vụ";
			// 
			// dgrdDSNVNhanNhiemVu
			// 
			this.dgrdDSNVNhanNhiemVu.AllowUserToAddRows = false;
			this.dgrdDSNVNhanNhiemVu.AllowUserToDeleteRows = false;
			this.dgrdDSNVNhanNhiemVu.AllowUserToResizeRows = false;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.dgrdDSNVNhanNhiemVu.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
			this.dgrdDSNVNhanNhiemVu.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgrdDSNVNhanNhiemVu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.g1c4,
            this.dataGridViewTextBoxColumn3});
			this.dgrdDSNVNhanNhiemVu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgrdDSNVNhanNhiemVu.Location = new System.Drawing.Point(3, 16);
			this.dgrdDSNVNhanNhiemVu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dgrdDSNVNhanNhiemVu.Name = "dgrdDSNVNhanNhiemVu";
			this.dgrdDSNVNhanNhiemVu.RowHeadersVisible = false;
			this.dgrdDSNVNhanNhiemVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdDSNVNhanNhiemVu.Size = new System.Drawing.Size(508, 437);
			this.dgrdDSNVNhanNhiemVu.TabIndex = 0;
			// 
			// dataGridViewCheckBoxColumn1
			// 
			this.dataGridViewCheckBoxColumn1.DataPropertyName = "check";
			this.dataGridViewCheckBoxColumn1.FalseValue = "false";
			this.dataGridViewCheckBoxColumn1.Frozen = true;
			this.dataGridViewCheckBoxColumn1.HeaderText = "";
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewCheckBoxColumn1.TrueValue = "true";
			this.dataGridViewCheckBoxColumn1.Width = 22;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "UserFullName";
			this.dataGridViewTextBoxColumn1.HeaderText = "Tên NV";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.ToolTipText = "Tên Nhân viên";
			this.dataGridViewTextBoxColumn1.Width = 150;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "UserFullCode";
			this.dataGridViewTextBoxColumn2.HeaderText = "Mã NV";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.ToolTipText = "Mã Nhân viên";
			this.dataGridViewTextBoxColumn2.Width = 55;
			// 
			// g1c4
			// 
			this.g1c4.DataPropertyName = "TenNhiemVu";
			this.g1c4.HeaderText = "Nhiệm vụ";
			this.g1c4.Name = "g1c4";
			this.g1c4.Width = 250;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "UserEnrollNumber";
			dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewTextBoxColumn3.HeaderText = "Mã CC_hide";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.ToolTipText = "Mã Chấm công";
			this.dataGridViewTextBoxColumn3.Visible = false;
			this.dataGridViewTextBoxColumn3.Width = 55;
			// 
			// btnDangKyNhiemVu
			// 
			this.btnDangKyNhiemVu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnDangKyNhiemVu.ForeColor = System.Drawing.Color.Blue;
			this.btnDangKyNhiemVu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDangKyNhiemVu.Location = new System.Drawing.Point(248, 4);
			this.btnDangKyNhiemVu.Name = "btnDangKyNhiemVu";
			this.btnDangKyNhiemVu.Size = new System.Drawing.Size(140, 27);
			this.btnDangKyNhiemVu.TabIndex = 46;
			this.btnDangKyNhiemVu.Text = "Đăng ký nhiệm vụ";
			this.btnDangKyNhiemVu.UseVisualStyleBackColor = true;
			this.btnDangKyNhiemVu.Click += new System.EventHandler(this.btnDangKyNhiemVu_Click);
			// 
			// btnHuyNhiemVu
			// 
			this.btnHuyNhiemVu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnHuyNhiemVu.ForeColor = System.Drawing.Color.Blue;
			this.btnHuyNhiemVu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnHuyNhiemVu.Location = new System.Drawing.Point(394, 4);
			this.btnHuyNhiemVu.Name = "btnHuyNhiemVu";
			this.btnHuyNhiemVu.Size = new System.Drawing.Size(140, 27);
			this.btnHuyNhiemVu.TabIndex = 46;
			this.btnHuyNhiemVu.Text = "Huỷ nhiệm vụ";
			this.btnHuyNhiemVu.UseVisualStyleBackColor = true;
			this.btnHuyNhiemVu.Click += new System.EventHandler(this.btnHuyNhiemVu_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnThoat.Location = new System.Drawing.Point(677, 4);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(85, 27);
			this.btnThoat.TabIndex = 46;
			this.btnThoat.Text = "Đóng";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// gpChonPhongBan
			// 
			this.gpChonPhongBan.Controls.Add(this.treePhongBan);
			this.gpChonPhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.gpChonPhongBan.Location = new System.Drawing.Point(3, 2);
			this.gpChonPhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gpChonPhongBan.Name = "gpChonPhongBan";
			this.gpChonPhongBan.Padding = new System.Windows.Forms.Padding(3, 6, 3, 2);
			this.gpChonPhongBan.Size = new System.Drawing.Size(233, 117);
			this.gpChonPhongBan.TabIndex = 0;
			this.gpChonPhongBan.TabStop = false;
			this.gpChonPhongBan.Text = "Chọn phòng ban";
			// 
			// treePhongBan
			// 
			this.treePhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treePhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.treePhongBan.Indent = 18;
			this.treePhongBan.ItemHeight = 20;
			this.treePhongBan.Location = new System.Drawing.Point(3, 20);
			this.treePhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.treePhongBan.Name = "treePhongBan";
			treeNode5.Name = "Node2";
			treeNode5.Text = "px thành phẩm";
			treeNode6.Name = "Node5";
			treeNode6.Text = "bảo vệ";
			treeNode7.Name = "Node4";
			treeNode7.Text = "văn phòng";
			treeNode8.Name = "Node0";
			treeNode8.Text = "Nhà máy thuốc lá khánh hội";
			this.treePhongBan.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8});
			this.treePhongBan.ShowNodeToolTips = true;
			this.treePhongBan.Size = new System.Drawing.Size(227, 95);
			this.treePhongBan.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
			this.tableLayoutPanel1.Controls.Add(this.gpChonPhongBan, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.checkListNhiemVu, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(240, 489);
			this.tableLayoutPanel1.TabIndex = 32;
			// 
			// checkListNhiemVu
			// 
			this.checkListNhiemVu.CheckOnClick = true;
			this.checkListNhiemVu.FormattingEnabled = true;
			this.checkListNhiemVu.HorizontalScrollbar = true;
			this.checkListNhiemVu.Location = new System.Drawing.Point(3, 145);
			this.checkListNhiemVu.Name = "checkListNhiemVu";
			this.checkListNhiemVu.ScrollAlwaysVisible = true;
			this.checkListNhiemVu.Size = new System.Drawing.Size(234, 340);
			this.checkListNhiemVu.TabIndex = 39;
			this.checkListNhiemVu.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkListNhiemVu_ItemCheck);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label1.Location = new System.Drawing.Point(3, 122);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(137, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Danh sách nhiệm vụ";
			// 
			// toolTipHint
			// 
			this.toolTipHint.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTipHint_Popup);
			// 
			// fmDSNVNhanNhiemVu
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(763, 494);
			this.Controls.Add(this.gp1);
			this.Controls.Add(this.btnHuyNhiemVu);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnDangKyNhiemVu);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "fmDSNVNhanNhiemVu";
			this.Text = "Danh sách các nhân viên đang nhận nhiệm vụ";
			this.Load += new System.EventHandler(this.fmDSNVNhanNhiemVu_Load);
			this.gp1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVNhanNhiemVu)).EndInit();
			this.gpChonPhongBan.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gp1;
		private System.Windows.Forms.DataGridView dgrdDSNVNhanNhiemVu;
		private System.Windows.Forms.Button btnDangKyNhiemVu;
		private System.Windows.Forms.Button btnHuyNhiemVu;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.GroupBox gpChonPhongBan;
		private System.Windows.Forms.TreeView treePhongBan;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.CheckedListBox checkListNhiemVu;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolTip toolTipHint;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
	}
}