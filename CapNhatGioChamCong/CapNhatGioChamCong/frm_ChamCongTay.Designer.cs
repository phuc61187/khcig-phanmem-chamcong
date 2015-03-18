namespace CapNhatGioChamCong {
	partial class frm_ChamCongTay {
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
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("px thành phẩm");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("bảo vệ");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("văn phòng", new System.Windows.Forms.TreeNode[] {
            treeNode6});
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Nhà máy thuốc lá khánh hội", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode7});
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.gp1 = new System.Windows.Forms.GroupBox();
			this.dgrdDSNVTrgPhg = new System.Windows.Forms.DataGridView();
			this.cChkDSNV2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.cUserFullNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cUserEnrollNumberDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cUserFullCodeDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cSchNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cTitleNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cDescriptionDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cSchIDDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cUserIDDDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cbCa = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.dtpBDLam = new System.Windows.Forms.DateTimePicker();
			this.label8 = new System.Windows.Forms.Label();
			this.dtpKTLam = new System.Windows.Forms.DateTimePicker();
			this.label9 = new System.Windows.Forms.Label();
			this.checkXNLamThem = new System.Windows.Forms.CheckBox();
			this.checkTinhPC150 = new System.Windows.Forms.CheckBox();
			this.label13 = new System.Windows.Forms.Label();
			this.dtpNgay = new System.Windows.Forms.DateTimePicker();
			this.numSoPhutOT = new System.Windows.Forms.NumericUpDown();
			this.tbGioLam = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.tbCong = new System.Windows.Forms.TextBox();
			this.btnThucHien = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label11 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cbLyDo = new System.Windows.Forms.ComboBox();
			this.tbGhiChu = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.gpChonPhongBan.SuspendLayout();
			this.gp1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSoPhutOT)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.panel1.Controls.Add(this.splitContainer1);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(213, 513);
			this.panel1.TabIndex = 5;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.gpChonPhongBan);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.gp1);
			this.splitContainer1.Size = new System.Drawing.Size(213, 513);
			this.splitContainer1.SplitterDistance = 206;
			this.splitContainer1.TabIndex = 0;
			// 
			// gpChonPhongBan
			// 
			this.gpChonPhongBan.Controls.Add(this.treePhongBan);
			this.gpChonPhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gpChonPhongBan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gpChonPhongBan.Location = new System.Drawing.Point(0, 0);
			this.gpChonPhongBan.Name = "gpChonPhongBan";
			this.gpChonPhongBan.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.gpChonPhongBan.Size = new System.Drawing.Size(213, 206);
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
			this.treePhongBan.Size = new System.Drawing.Size(207, 182);
			this.treePhongBan.TabIndex = 0;
			this.treePhongBan.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treePhongBan_AfterSelect);
			// 
			// gp1
			// 
			this.gp1.Controls.Add(this.dgrdDSNVTrgPhg);
			this.gp1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gp1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gp1.Location = new System.Drawing.Point(0, 0);
			this.gp1.Name = "gp1";
			this.gp1.Size = new System.Drawing.Size(213, 303);
			this.gp1.TabIndex = 1;
			this.gp1.TabStop = false;
			this.gp1.Text = "Danh sách Nhân viên";
			// 
			// dgrdDSNVTrgPhg
			// 
			this.dgrdDSNVTrgPhg.AllowUserToAddRows = false;
			this.dgrdDSNVTrgPhg.AllowUserToDeleteRows = false;
			this.dgrdDSNVTrgPhg.AllowUserToResizeRows = false;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.dgrdDSNVTrgPhg.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dgrdDSNVTrgPhg.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgrdDSNVTrgPhg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cChkDSNV2,
            this.cUserFullNameDSNV2,
            this.cUserEnrollNumberDSNV2,
            this.cUserFullCodeDSNV2,
            this.cSchNameDSNV2,
            this.cTitleNameDSNV2,
            this.cDescriptionDSNV2,
            this.cSchIDDSNV2,
            this.cUserIDDDSNV2});
			this.dgrdDSNVTrgPhg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgrdDSNVTrgPhg.Location = new System.Drawing.Point(3, 18);
			this.dgrdDSNVTrgPhg.Name = "dgrdDSNVTrgPhg";
			this.dgrdDSNVTrgPhg.RowHeadersVisible = false;
			this.dgrdDSNVTrgPhg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdDSNVTrgPhg.Size = new System.Drawing.Size(207, 282);
			this.dgrdDSNVTrgPhg.TabIndex = 2;
			// 
			// cChkDSNV2
			// 
			this.cChkDSNV2.DataPropertyName = "check";
			this.cChkDSNV2.FalseValue = "false";
			this.cChkDSNV2.Frozen = true;
			this.cChkDSNV2.HeaderText = "";
			this.cChkDSNV2.Name = "cChkDSNV2";
			this.cChkDSNV2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.cChkDSNV2.TrueValue = "true";
			this.cChkDSNV2.Width = 22;
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
			// cUserEnrollNumberDSNV2
			// 
			this.cUserEnrollNumberDSNV2.DataPropertyName = "UserEnrollNumber";
			dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.cUserEnrollNumberDSNV2.DefaultCellStyle = dataGridViewCellStyle5;
			this.cUserEnrollNumberDSNV2.HeaderText = "Mã CC";
			this.cUserEnrollNumberDSNV2.Name = "cUserEnrollNumberDSNV2";
			this.cUserEnrollNumberDSNV2.ReadOnly = true;
			this.cUserEnrollNumberDSNV2.ToolTipText = "Mã Chấm công";
			this.cUserEnrollNumberDSNV2.Width = 55;
			// 
			// cUserFullCodeDSNV2
			// 
			this.cUserFullCodeDSNV2.DataPropertyName = "UserFullCode";
			dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.cUserFullCodeDSNV2.DefaultCellStyle = dataGridViewCellStyle6;
			this.cUserFullCodeDSNV2.HeaderText = "Mã NV";
			this.cUserFullCodeDSNV2.Name = "cUserFullCodeDSNV2";
			this.cUserFullCodeDSNV2.ReadOnly = true;
			this.cUserFullCodeDSNV2.ToolTipText = "Mã Nhân viên";
			this.cUserFullCodeDSNV2.Visible = false;
			this.cUserFullCodeDSNV2.Width = 55;
			// 
			// cSchNameDSNV2
			// 
			this.cSchNameDSNV2.DataPropertyName = "SchName";
			this.cSchNameDSNV2.HeaderText = "Lịch trình";
			this.cSchNameDSNV2.Name = "cSchNameDSNV2";
			this.cSchNameDSNV2.ReadOnly = true;
			this.cSchNameDSNV2.ToolTipText = "Lịch trình";
			this.cSchNameDSNV2.Width = 95;
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
			this.cDescriptionDSNV2.DataPropertyName = "Description";
			this.cDescriptionDSNV2.HeaderText = "Phòng ban";
			this.cDescriptionDSNV2.Name = "cDescriptionDSNV2";
			this.cDescriptionDSNV2.ReadOnly = true;
			this.cDescriptionDSNV2.ToolTipText = "Phòng ban";
			this.cDescriptionDSNV2.Width = 105;
			// 
			// cSchIDDSNV2
			// 
			this.cSchIDDSNV2.DataPropertyName = "SchID";
			this.cSchIDDSNV2.HeaderText = "Mã lịch trình";
			this.cSchIDDSNV2.Name = "cSchIDDSNV2";
			this.cSchIDDSNV2.ReadOnly = true;
			this.cSchIDDSNV2.ToolTipText = "Mã lịch trình";
			this.cSchIDDSNV2.Visible = false;
			// 
			// cUserIDDDSNV2
			// 
			this.cUserIDDDSNV2.DataPropertyName = "UserIDD";
			this.cUserIDDDSNV2.HeaderText = "Mã Phòng ban";
			this.cUserIDDDSNV2.Name = "cUserIDDDSNV2";
			this.cUserIDDDSNV2.ReadOnly = true;
			this.cUserIDDDSNV2.Visible = false;
			// 
			// cbCa
			// 
			this.cbCa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCa.FormattingEnabled = true;
			this.cbCa.Location = new System.Drawing.Point(344, 49);
			this.cbCa.Name = "cbCa";
			this.cbCa.Size = new System.Drawing.Size(172, 24);
			this.cbCa.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label6.Location = new System.Drawing.Point(278, 52);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60, 16);
			this.label6.TabIndex = 0;
			this.label6.Text = "Chọn ca";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label7.Location = new System.Drawing.Point(224, 81);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(115, 16);
			this.label7.TabIndex = 0;
			this.label7.Text = "Bắt đầu làm việc";
			// 
			// dtpBDLam
			// 
			this.dtpBDLam.CustomFormat = "H:mm ddd d/M/yyyy";
			this.dtpBDLam.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpBDLam.Location = new System.Drawing.Point(344, 79);
			this.dtpBDLam.Name = "dtpBDLam";
			this.dtpBDLam.ShowUpDown = true;
			this.dtpBDLam.Size = new System.Drawing.Size(172, 22);
			this.dtpBDLam.TabIndex = 2;
			this.dtpBDLam.Value = new System.DateTime(2014, 3, 11, 0, 0, 0, 0);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label8.Location = new System.Drawing.Point(221, 109);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(118, 16);
			this.label8.TabIndex = 0;
			this.label8.Text = "Kết thúc làm việc";
			// 
			// dtpKTLam
			// 
			this.dtpKTLam.CustomFormat = "H:mm ddd d/M/yyyy";
			this.dtpKTLam.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpKTLam.Location = new System.Drawing.Point(344, 107);
			this.dtpKTLam.Name = "dtpKTLam";
			this.dtpKTLam.ShowUpDown = true;
			this.dtpKTLam.Size = new System.Drawing.Size(172, 22);
			this.dtpKTLam.TabIndex = 3;
			this.dtpKTLam.Value = new System.DateTime(2014, 3, 11, 0, 0, 0, 0);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label9.Location = new System.Drawing.Point(279, 138);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(59, 16);
			this.label9.TabIndex = 0;
			this.label9.Text = "Giờ làm";
			// 
			// checkXNLamThem
			// 
			this.checkXNLamThem.AutoSize = true;
			this.checkXNLamThem.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.checkXNLamThem.Location = new System.Drawing.Point(344, 191);
			this.checkXNLamThem.Name = "checkXNLamThem";
			this.checkXNLamThem.Size = new System.Drawing.Size(151, 20);
			this.checkXNLamThem.TabIndex = 9;
			this.checkXNLamThem.Text = "Xác nhận làm thêm";
			this.checkXNLamThem.UseVisualStyleBackColor = true;
			// 
			// checkTinhPC150
			// 
			this.checkTinhPC150.AutoSize = true;
			this.checkTinhPC150.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.checkTinhPC150.Location = new System.Drawing.Point(344, 217);
			this.checkTinhPC150.Name = "checkTinhPC150";
			this.checkTinhPC150.Size = new System.Drawing.Size(138, 20);
			this.checkTinhPC150.TabIndex = 11;
			this.checkTinhPC150.Text = "Tính phụ cấp 50%";
			this.checkTinhPC150.UseVisualStyleBackColor = true;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label13.Location = new System.Drawing.Point(262, 24);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(76, 16);
			this.label13.TabIndex = 0;
			this.label13.Text = "Nhập ngày";
			// 
			// dtpNgay
			// 
			this.dtpNgay.CustomFormat = "d/M/yyyy ddd";
			this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgay.Location = new System.Drawing.Point(344, 21);
			this.dtpNgay.Name = "dtpNgay";
			this.dtpNgay.Size = new System.Drawing.Size(172, 22);
			this.dtpNgay.TabIndex = 0;
			// 
			// numSoPhutOT
			// 
			this.numSoPhutOT.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numSoPhutOT.Location = new System.Drawing.Point(514, 190);
			this.numSoPhutOT.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
			this.numSoPhutOT.Name = "numSoPhutOT";
			this.numSoPhutOT.Size = new System.Drawing.Size(70, 22);
			this.numSoPhutOT.TabIndex = 10;
			// 
			// tbGioLam
			// 
			this.tbGioLam.Location = new System.Drawing.Point(344, 135);
			this.tbGioLam.Name = "tbGioLam";
			this.tbGioLam.ReadOnly = true;
			this.tbGioLam.Size = new System.Drawing.Size(83, 22);
			this.tbGioLam.TabIndex = 1;
			this.tbGioLam.Text = "0";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label10.Location = new System.Drawing.Point(267, 166);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(71, 16);
			this.label10.TabIndex = 0;
			this.label10.Text = "Tính công";
			// 
			// tbCong
			// 
			this.tbCong.Location = new System.Drawing.Point(344, 163);
			this.tbCong.Name = "tbCong";
			this.tbCong.ReadOnly = true;
			this.tbCong.Size = new System.Drawing.Size(83, 22);
			this.tbCong.TabIndex = 1;
			this.tbCong.Text = "0";
			// 
			// btnThucHien
			// 
			this.btnThucHien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnThucHien.ForeColor = System.Drawing.Color.Blue;
			this.btnThucHien.Location = new System.Drawing.Point(344, 355);
			this.btnThucHien.Name = "btnThucHien";
			this.btnThucHien.Size = new System.Drawing.Size(83, 27);
			this.btnThucHien.TabIndex = 12;
			this.btnThucHien.Text = "Thực hiện";
			this.btnThucHien.UseVisualStyleBackColor = true;
			this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.button2.ForeColor = System.Drawing.Color.Blue;
			this.button2.Location = new System.Drawing.Point(433, 355);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(83, 27);
			this.button2.TabIndex = 13;
			this.button2.Text = "Thoát";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label11.Location = new System.Drawing.Point(590, 192);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(36, 16);
			this.label11.TabIndex = 0;
			this.label11.Text = "phút";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label1.Location = new System.Drawing.Point(296, 247);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Lý do";
			// 
			// cbLyDo
			// 
			this.cbLyDo.FormattingEnabled = true;
			this.cbLyDo.Items.AddRange(new object[] {
            "Lý do khác",
            "Chấm công cho khối quản lý",
            ""});
			this.cbLyDo.Location = new System.Drawing.Point(344, 244);
			this.cbLyDo.Name = "cbLyDo";
			this.cbLyDo.Size = new System.Drawing.Size(172, 24);
			this.cbLyDo.TabIndex = 14;
			// 
			// tbGhiChu
			// 
			this.tbGhiChu.Location = new System.Drawing.Point(344, 274);
			this.tbGhiChu.Multiline = true;
			this.tbGhiChu.Name = "tbGhiChu";
			this.tbGhiChu.Size = new System.Drawing.Size(172, 75);
			this.tbGhiChu.TabIndex = 15;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label2.Location = new System.Drawing.Point(281, 277);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "Ghi chú";
			// 
			// frm_ChamCongTay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(636, 513);
			this.Controls.Add(this.tbGhiChu);
			this.Controls.Add(this.cbLyDo);
			this.Controls.Add(this.numSoPhutOT);
			this.Controls.Add(this.checkTinhPC150);
			this.Controls.Add(this.checkXNLamThem);
			this.Controls.Add(this.cbCa);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.dtpKTLam);
			this.Controls.Add(this.dtpBDLam);
			this.Controls.Add(this.dtpNgay);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.btnThucHien);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.tbGioLam);
			this.Controls.Add(this.tbCong);
			this.Controls.Add(this.label6);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_ChamCongTay";
			this.Text = "Chấm công tay";
			this.Load += new System.EventHandler(this.frm_ChamCongTay_Load);
			this.panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.gpChonPhongBan.ResumeLayout(false);
			this.gp1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSoPhutOT)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox gpChonPhongBan;
		private System.Windows.Forms.TreeView treePhongBan;
		private System.Windows.Forms.GroupBox gp1;
		private System.Windows.Forms.DataGridView dgrdDSNVTrgPhg;
		private System.Windows.Forms.DataGridViewCheckBoxColumn cChkDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserEnrollNumberDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullCodeDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cSchNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cTitleNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cDescriptionDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cSchIDDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserIDDDSNV2;
		private System.Windows.Forms.ComboBox cbCa;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DateTimePicker dtpBDLam;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.DateTimePicker dtpKTLam;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox checkXNLamThem;
		private System.Windows.Forms.CheckBox checkTinhPC150;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.DateTimePicker dtpNgay;
		private System.Windows.Forms.NumericUpDown numSoPhutOT;
		private System.Windows.Forms.TextBox tbGioLam;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox tbCong;
		private System.Windows.Forms.Button btnThucHien;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbLyDo;
		private System.Windows.Forms.TextBox tbGhiChu;
		private System.Windows.Forms.Label label2;
	}
}