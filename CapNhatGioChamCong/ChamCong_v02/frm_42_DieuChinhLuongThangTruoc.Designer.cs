namespace ChamCong_v02 {
    partial class frm_42_DieuChinhLuongThangTruoc {
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.label1 = new System.Windows.Forms.Label();
			this.tbMaCC = new System.Windows.Forms.TextBox();
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.tbTenNV = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnThucHien = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.gp1 = new System.Windows.Forms.GroupBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tbSearch = new System.Windows.Forms.TextBox();
			this.cbTieuChi = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnTim = new System.Windows.Forms.Button();
			this.dgrdDSNVTrgPhg = new System.Windows.Forms.DataGridView();
			this.cChkDSNV2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.cUserFullCodeDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cUserEnrollNumberDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cUserFullNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cTitleNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cDescriptionDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cSchIDDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cUserIDDDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cSchNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.gpChonPhongBan.SuspendLayout();
			this.gp1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label1.Location = new System.Drawing.Point(246, 311);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Mã Nhân viên";
			// 
			// tbMaCC
			// 
			this.tbMaCC.Location = new System.Drawing.Point(353, 308);
			this.tbMaCC.Name = "tbMaCC";
			this.tbMaCC.ReadOnly = true;
			this.tbMaCC.Size = new System.Drawing.Size(83, 22);
			this.tbMaCC.TabIndex = 0;
			// 
			// dtpThang
			// 
			this.dtpThang.CustomFormat = "M / yyyy";
			this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpThang.Location = new System.Drawing.Point(353, 336);
			this.dtpThang.Name = "dtpThang";
			this.dtpThang.ShowUpDown = true;
			this.dtpThang.Size = new System.Drawing.Size(83, 22);
			this.dtpThang.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label2.Location = new System.Drawing.Point(442, 311);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "Tên Nhân viên";
			// 
			// tbTenNV
			// 
			this.tbTenNV.Location = new System.Drawing.Point(569, 308);
			this.tbTenNV.Name = "tbTenNV";
			this.tbTenNV.ReadOnly = true;
			this.tbTenNV.Size = new System.Drawing.Size(247, 22);
			this.tbTenNV.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label3.Location = new System.Drawing.Point(246, 339);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(81, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Nhập tháng";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label4.Location = new System.Drawing.Point(442, 339);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(121, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Lương điều chỉnh";
			// 
			// btnThucHien
			// 
			this.btnThucHien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnThucHien.ForeColor = System.Drawing.Color.Blue;
			this.btnThucHien.Location = new System.Drawing.Point(353, 364);
			this.btnThucHien.Name = "btnThucHien";
			this.btnThucHien.Size = new System.Drawing.Size(83, 27);
			this.btnThucHien.TabIndex = 4;
			this.btnThucHien.Text = "Thực hiện";
			this.btnThucHien.UseVisualStyleBackColor = true;
			this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(569, 364);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(83, 27);
			this.btnThoat.TabIndex = 5;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDown1.Location = new System.Drawing.Point(569, 337);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.numericUpDown1.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147483648});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(120, 22);
			this.numericUpDown1.TabIndex = 3;
			this.numericUpDown1.ThousandsSeparator = true;
			// 
			// gpChonPhongBan
			// 
			this.gpChonPhongBan.Controls.Add(this.treePhongBan);
			this.gpChonPhongBan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gpChonPhongBan.Location = new System.Drawing.Point(12, 12);
			this.gpChonPhongBan.Name = "gpChonPhongBan";
			this.gpChonPhongBan.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.gpChonPhongBan.Size = new System.Drawing.Size(213, 377);
			this.gpChonPhongBan.TabIndex = 2;
			this.gpChonPhongBan.TabStop = false;
			this.gpChonPhongBan.Text = "Chọn phòng ban";
			// 
			// treePhongBan
			// 
			this.treePhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treePhongBan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.treePhongBan.Indent = 18;
			this.treePhongBan.ItemHeight = 20;
			this.treePhongBan.Location = new System.Drawing.Point(3, 21);
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
			this.treePhongBan.Size = new System.Drawing.Size(207, 353);
			this.treePhongBan.TabIndex = 0;
			// 
			// gp1
			// 
			this.gp1.Controls.Add(this.panel1);
			this.gp1.Controls.Add(this.dgrdDSNVTrgPhg);
			this.gp1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gp1.Location = new System.Drawing.Point(231, 12);
			this.gp1.Name = "gp1";
			this.gp1.Size = new System.Drawing.Size(591, 290);
			this.gp1.TabIndex = 1;
			this.gp1.TabStop = false;
			this.gp1.Text = "Danh sách Nhân viên";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tbSearch);
			this.panel1.Controls.Add(this.cbTieuChi);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.btnTim);
			this.panel1.Location = new System.Drawing.Point(6, 21);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(579, 30);
			this.panel1.TabIndex = 3;
			// 
			// tbSearch
			// 
			this.tbSearch.Location = new System.Drawing.Point(215, 3);
			this.tbSearch.Name = "tbSearch";
			this.tbSearch.Size = new System.Drawing.Size(237, 22);
			this.tbSearch.TabIndex = 2;
			// 
			// cbTieuChi
			// 
			this.cbTieuChi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTieuChi.FormattingEnabled = true;
			this.cbTieuChi.Items.AddRange(new object[] {
            "Mã Nhân viên",
            "Tên Nhân viên"});
			this.cbTieuChi.Location = new System.Drawing.Point(79, 3);
			this.cbTieuChi.Name = "cbTieuChi";
			this.cbTieuChi.Size = new System.Drawing.Size(130, 24);
			this.cbTieuChi.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label5.Location = new System.Drawing.Point(9, 6);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 16);
			this.label5.TabIndex = 0;
			this.label5.Text = "Tìm theo";
			// 
			// btnTim
			// 
			this.btnTim.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnTim.ForeColor = System.Drawing.Color.Blue;
			this.btnTim.Location = new System.Drawing.Point(458, 1);
			this.btnTim.Name = "btnTim";
			this.btnTim.Size = new System.Drawing.Size(51, 27);
			this.btnTim.TabIndex = 4;
			this.btnTim.Text = "Tìm";
			this.btnTim.UseVisualStyleBackColor = true;
			this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
			// 
			// dgrdDSNVTrgPhg
			// 
			this.dgrdDSNVTrgPhg.AllowUserToAddRows = false;
			this.dgrdDSNVTrgPhg.AllowUserToDeleteRows = false;
			this.dgrdDSNVTrgPhg.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.dgrdDSNVTrgPhg.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgrdDSNVTrgPhg.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgrdDSNVTrgPhg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cChkDSNV2,
            this.cUserFullCodeDSNV2,
            this.cUserEnrollNumberDSNV2,
            this.cUserFullNameDSNV2,
            this.cTitleNameDSNV2,
            this.cDescriptionDSNV2,
            this.cSchIDDSNV2,
            this.cUserIDDDSNV2,
            this.cSchNameDSNV2});
			this.dgrdDSNVTrgPhg.Location = new System.Drawing.Point(6, 52);
			this.dgrdDSNVTrgPhg.Name = "dgrdDSNVTrgPhg";
			this.dgrdDSNVTrgPhg.ReadOnly = true;
			this.dgrdDSNVTrgPhg.RowHeadersVisible = false;
			this.dgrdDSNVTrgPhg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdDSNVTrgPhg.Size = new System.Drawing.Size(579, 232);
			this.dgrdDSNVTrgPhg.TabIndex = 2;
			this.dgrdDSNVTrgPhg.SelectionChanged += new System.EventHandler(this.dgrdDSNVTrgPhg_SelectionChanged);
			// 
			// cChkDSNV2
			// 
			this.cChkDSNV2.DataPropertyName = "check";
			this.cChkDSNV2.FalseValue = "false";
			this.cChkDSNV2.Frozen = true;
			this.cChkDSNV2.HeaderText = "";
			this.cChkDSNV2.Name = "cChkDSNV2";
			this.cChkDSNV2.ReadOnly = true;
			this.cChkDSNV2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.cChkDSNV2.TrueValue = "true";
			this.cChkDSNV2.Visible = false;
			this.cChkDSNV2.Width = 22;
			// 
			// cUserFullCodeDSNV2
			// 
			this.cUserFullCodeDSNV2.DataPropertyName = "UserFullCode";
			dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.cUserFullCodeDSNV2.DefaultCellStyle = dataGridViewCellStyle2;
			this.cUserFullCodeDSNV2.HeaderText = "Mã NV";
			this.cUserFullCodeDSNV2.Name = "cUserFullCodeDSNV2";
			this.cUserFullCodeDSNV2.ReadOnly = true;
			this.cUserFullCodeDSNV2.ToolTipText = "Mã Nhân viên";
			this.cUserFullCodeDSNV2.Width = 55;
			// 
			// cUserEnrollNumberDSNV2
			// 
			this.cUserEnrollNumberDSNV2.DataPropertyName = "UserEnrollNumber";
			dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.cUserEnrollNumberDSNV2.DefaultCellStyle = dataGridViewCellStyle3;
			this.cUserEnrollNumberDSNV2.HeaderText = "Mã CC_hide";
			this.cUserEnrollNumberDSNV2.Name = "cUserEnrollNumberDSNV2";
			this.cUserEnrollNumberDSNV2.ReadOnly = true;
			this.cUserEnrollNumberDSNV2.ToolTipText = "Mã Chấm công";
			this.cUserEnrollNumberDSNV2.Visible = false;
			this.cUserEnrollNumberDSNV2.Width = 55;
			// 
			// cUserFullNameDSNV2
			// 
			this.cUserFullNameDSNV2.DataPropertyName = "UserFullName";
			this.cUserFullNameDSNV2.HeaderText = "Tên NV";
			this.cUserFullNameDSNV2.Name = "cUserFullNameDSNV2";
			this.cUserFullNameDSNV2.ReadOnly = true;
			this.cUserFullNameDSNV2.ToolTipText = "Tên Nhân viên";
			this.cUserFullNameDSNV2.Width = 150;
			// 
			// cTitleNameDSNV2
			// 
			this.cTitleNameDSNV2.DataPropertyName = "TitleName";
			this.cTitleNameDSNV2.HeaderText = "Chức vụ";
			this.cTitleNameDSNV2.Name = "cTitleNameDSNV2";
			this.cTitleNameDSNV2.ReadOnly = true;
			this.cTitleNameDSNV2.ToolTipText = "Chức vụ";
			this.cTitleNameDSNV2.Width = 200;
			// 
			// cDescriptionDSNV2
			// 
			this.cDescriptionDSNV2.DataPropertyName = "Description_1";
			this.cDescriptionDSNV2.HeaderText = "Phòng ban";
			this.cDescriptionDSNV2.Name = "cDescriptionDSNV2";
			this.cDescriptionDSNV2.ReadOnly = true;
			this.cDescriptionDSNV2.ToolTipText = "Phòng ban";
			this.cDescriptionDSNV2.Width = 150;
			// 
			// cSchIDDSNV2
			// 
			this.cSchIDDSNV2.DataPropertyName = "SchID";
			this.cSchIDDSNV2.HeaderText = "Mã lịch trình_hide";
			this.cSchIDDSNV2.Name = "cSchIDDSNV2";
			this.cSchIDDSNV2.ReadOnly = true;
			this.cSchIDDSNV2.ToolTipText = "Mã lịch trình";
			this.cSchIDDSNV2.Visible = false;
			// 
			// cUserIDDDSNV2
			// 
			this.cUserIDDDSNV2.DataPropertyName = "IDD_1";
			this.cUserIDDDSNV2.HeaderText = "Mã Phòng ban_hide";
			this.cUserIDDDSNV2.Name = "cUserIDDDSNV2";
			this.cUserIDDDSNV2.ReadOnly = true;
			this.cUserIDDDSNV2.Visible = false;
			// 
			// cSchNameDSNV2
			// 
			this.cSchNameDSNV2.DataPropertyName = "SchName";
			this.cSchNameDSNV2.HeaderText = "Lịch trình_hide";
			this.cSchNameDSNV2.Name = "cSchNameDSNV2";
			this.cSchNameDSNV2.ReadOnly = true;
			this.cSchNameDSNV2.ToolTipText = "Lịch trình";
			this.cSchNameDSNV2.Visible = false;
			this.cSchNameDSNV2.Width = 95;
			// 
			// frm_42_DieuChinhLuongThangTruoc
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(834, 401);
			this.Controls.Add(this.gpChonPhongBan);
			this.Controls.Add(this.gp1);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.dtpThang);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnThucHien);
			this.Controls.Add(this.tbTenNV);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbMaCC);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_42_DieuChinhLuongThangTruoc";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Điều chỉnh lương tháng trước cho nhân viên";
			this.Load += new System.EventHandler(this.frm_DieuChinhLuongThangTruoc_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.gpChonPhongBan.ResumeLayout(false);
			this.gp1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbMaCC;
		private System.Windows.Forms.DateTimePicker dtpThang;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbTenNV;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnThucHien;
		private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.GroupBox gpChonPhongBan;
		private System.Windows.Forms.TreeView treePhongBan;
		private System.Windows.Forms.GroupBox gp1;
		private System.Windows.Forms.DataGridView dgrdDSNVTrgPhg;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox tbSearch;
		private System.Windows.Forms.ComboBox cbTieuChi;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnTim;
		private System.Windows.Forms.DataGridViewCheckBoxColumn cChkDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullCodeDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserEnrollNumberDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cTitleNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cDescriptionDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cSchIDDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserIDDDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cSchNameDSNV2;
	}
}