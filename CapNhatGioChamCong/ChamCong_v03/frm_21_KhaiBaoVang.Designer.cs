namespace ChamCong_v03 {
    partial class frm_21_KhaiBaoVang {
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("px thành phẩm");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("bảo vệ");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("văn phòng", new System.Windows.Forms.TreeNode[] {
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Nhà máy thuốc lá khánh hội", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3});
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.checklistNgay = new System.Windows.Forms.CheckedListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cbLoaiVang = new System.Windows.Forms.ComboBox();
			this.dgrdNgayVang = new System.Windows.Forms.DataGridView();
			this.g4colMaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g2macc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1tennv = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1ngay = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1cong = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1mota = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1userfullcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1absentcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnLietKe = new System.Windows.Forms.Button();
			this.btnXoa = new System.Windows.Forms.Button();
			this.btnThem = new System.Windows.Forms.Button();
			this.rdChonNgayTrongThang = new System.Windows.Forms.RadioButton();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chk1ExceptSun = new System.Windows.Forms.CheckBox();
			this.chk1ExceptSat = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cbSoBuoi = new System.Windows.Forms.ComboBox();
			this.btnThoat = new System.Windows.Forms.Button();
			this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tbSearch = new System.Windows.Forms.TextBox();
			this.btnTim = new System.Windows.Forms.Button();
			this.linkHienThiTatCaNV = new System.Windows.Forms.LinkLabel();
			this.gp1 = new System.Windows.Forms.GroupBox();
			this.dgrdDSNVTrgPhg = new System.Windows.Forms.DataGridView();
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colg200 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.rdChonNgayTrongKhoang = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.dtpNgayBD = new System.Windows.Forms.DateTimePicker();
			this.dtpNgayKT = new System.Windows.Forms.DateTimePicker();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chk2AllWKDays = new System.Windows.Forms.CheckBox();
			this.chk2ExceptSun = new System.Windows.Forms.CheckBox();
			this.chk2ExceptSat = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.dgrdNgayVang)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.gpChonPhongBan.SuspendLayout();
			this.panel1.SuspendLayout();
			this.gp1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dtpThang
			// 
			this.dtpThang.CustomFormat = "M/yyyy";
			this.dtpThang.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpThang.Location = new System.Drawing.Point(427, 67);
			this.dtpThang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dtpThang.Name = "dtpThang";
			this.dtpThang.ShowUpDown = true;
			this.dtpThang.Size = new System.Drawing.Size(98, 21);
			this.dtpThang.TabIndex = 2;
			this.dtpThang.Value = new System.DateTime(2014, 2, 1, 19, 11, 0, 0);
			this.dtpThang.ValueChanged += new System.EventHandler(this.dtpThang_ValueChanged);
			// 
			// checklistNgay
			// 
			this.checklistNgay.CheckOnClick = true;
			this.checklistNgay.ColumnWidth = 95;
			this.checklistNgay.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.checklistNgay.FormatString = "d - ddd";
			this.checklistNgay.FormattingEnabled = true;
			this.checklistNgay.Items.AddRange(new object[] {
            "1 - Hai",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "11",
            "12",
            "13",
            "14",
            "15",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
			this.checklistNgay.Location = new System.Drawing.Point(248, 123);
			this.checklistNgay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.checklistNgay.MultiColumn = true;
			this.checklistNgay.Name = "checklistNgay";
			this.checklistNgay.Size = new System.Drawing.Size(496, 116);
			this.checklistNgay.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(424, 248);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 15);
			this.label3.TabIndex = 1;
			this.label3.Text = "Loại vắng";
			// 
			// cbLoaiVang
			// 
			this.cbLoaiVang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbLoaiVang.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.cbLoaiVang.FormattingEnabled = true;
			this.cbLoaiVang.Location = new System.Drawing.Point(505, 245);
			this.cbLoaiVang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cbLoaiVang.Name = "cbLoaiVang";
			this.cbLoaiVang.Size = new System.Drawing.Size(239, 23);
			this.cbLoaiVang.TabIndex = 4;
			// 
			// dgrdNgayVang
			// 
			this.dgrdNgayVang.AllowUserToAddRows = false;
			this.dgrdNgayVang.AllowUserToDeleteRows = false;
			this.dgrdNgayVang.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.dgrdNgayVang.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgrdNgayVang.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdNgayVang.BackgroundColor = System.Drawing.Color.White;
			this.dgrdNgayVang.ColumnHeadersHeight = 27;
			this.dgrdNgayVang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgrdNgayVang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.g4colMaNV,
            this.g2macc,
            this.g1tennv,
            this.g1ngay,
            this.g1cong,
            this.g1mota,
            this.g1userfullcode,
            this.g1ID,
            this.g1absentcode});
			this.dgrdNgayVang.Location = new System.Drawing.Point(243, 306);
			this.dgrdNgayVang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dgrdNgayVang.Name = "dgrdNgayVang";
			this.dgrdNgayVang.RowHeadersVisible = false;
			this.dgrdNgayVang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdNgayVang.Size = new System.Drawing.Size(504, 255);
			this.dgrdNgayVang.TabIndex = 8;
			// 
			// g4colMaNV
			// 
			this.g4colMaNV.DataPropertyName = "UserFullCode";
			this.g4colMaNV.HeaderText = "Mã NV";
			this.g4colMaNV.Name = "g4colMaNV";
			this.g4colMaNV.ReadOnly = true;
			this.g4colMaNV.ToolTipText = "Mã Nhân viên";
			this.g4colMaNV.Width = 55;
			// 
			// g2macc
			// 
			this.g2macc.DataPropertyName = "UserEnrollNumber";
			this.g2macc.HeaderText = "Mã CC_hide";
			this.g2macc.Name = "g2macc";
			this.g2macc.ReadOnly = true;
			this.g2macc.Visible = false;
			this.g2macc.Width = 55;
			// 
			// g1tennv
			// 
			this.g1tennv.DataPropertyName = "UserFullName";
			this.g1tennv.HeaderText = "Tên NV";
			this.g1tennv.Name = "g1tennv";
			this.g1tennv.ReadOnly = true;
			this.g1tennv.Width = 150;
			// 
			// g1ngay
			// 
			this.g1ngay.DataPropertyName = "TimeDate";
			dataGridViewCellStyle2.Format = "ddd d/M";
			this.g1ngay.DefaultCellStyle = dataGridViewCellStyle2;
			this.g1ngay.HeaderText = "Ngày";
			this.g1ngay.Name = "g1ngay";
			this.g1ngay.ReadOnly = true;
			this.g1ngay.Width = 80;
			// 
			// g1cong
			// 
			this.g1cong.DataPropertyName = "Workingday";
			dataGridViewCellStyle3.Format = "#0.##";
			this.g1cong.DefaultCellStyle = dataGridViewCellStyle3;
			this.g1cong.HeaderText = "Số buổi";
			this.g1cong.Name = "g1cong";
			this.g1cong.ReadOnly = true;
			this.g1cong.ToolTipText = "Số buổi";
			this.g1cong.Width = 60;
			// 
			// g1mota
			// 
			this.g1mota.DataPropertyName = "AbsentDescription";
			this.g1mota.HeaderText = "Mô tả";
			this.g1mota.Name = "g1mota";
			this.g1mota.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.g1mota.Width = 125;
			// 
			// g1userfullcode
			// 
			this.g1userfullcode.DataPropertyName = "UserFullCode";
			this.g1userfullcode.HeaderText = "UserFullCode_hide";
			this.g1userfullcode.Name = "g1userfullcode";
			this.g1userfullcode.ReadOnly = true;
			this.g1userfullcode.Visible = false;
			// 
			// g1ID
			// 
			this.g1ID.DataPropertyName = "ID";
			this.g1ID.HeaderText = "ID_hide";
			this.g1ID.Name = "g1ID";
			this.g1ID.ReadOnly = true;
			this.g1ID.Visible = false;
			// 
			// g1absentcode
			// 
			this.g1absentcode.DataPropertyName = "AbsentCode";
			this.g1absentcode.HeaderText = "AbsentCodeHide";
			this.g1absentcode.Name = "g1absentcode";
			this.g1absentcode.Visible = false;
			// 
			// btnLietKe
			// 
			this.btnLietKe.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnLietKe.ForeColor = System.Drawing.Color.Blue;
			this.btnLietKe.Location = new System.Drawing.Point(267, 92);
			this.btnLietKe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnLietKe.Name = "btnLietKe";
			this.btnLietKe.Size = new System.Drawing.Size(257, 25);
			this.btnLietKe.TabIndex = 14;
			this.btnLietKe.Text = "Liệt kê các khai báo vắng trong tháng";
			this.toolTip1.SetToolTip(this.btnLietKe, "Liệt kê các ngày vắng đã khai báo trong tháng của các nhân viên");
			this.btnLietKe.UseVisualStyleBackColor = true;
			this.btnLietKe.Click += new System.EventHandler(this.btnLietKe_Click);
			// 
			// btnXoa
			// 
			this.btnXoa.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnXoa.ForeColor = System.Drawing.Color.Blue;
			this.btnXoa.Location = new System.Drawing.Point(588, 275);
			this.btnXoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnXoa.Name = "btnXoa";
			this.btnXoa.Size = new System.Drawing.Size(75, 25);
			this.btnXoa.TabIndex = 6;
			this.btnXoa.Text = "Xóa ";
			this.btnXoa.UseVisualStyleBackColor = true;
			this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
			// 
			// btnThem
			// 
			this.btnThem.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnThem.ForeColor = System.Drawing.Color.Blue;
			this.btnThem.Location = new System.Drawing.Point(505, 275);
			this.btnThem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnThem.Name = "btnThem";
			this.btnThem.Size = new System.Drawing.Size(75, 25);
			this.btnThem.TabIndex = 5;
			this.btnThem.Text = "Thêm";
			this.btnThem.UseVisualStyleBackColor = true;
			this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
			// 
			// rdChonNgayTrongThang
			// 
			this.rdChonNgayTrongThang.AutoSize = true;
			this.rdChonNgayTrongThang.Checked = true;
			this.rdChonNgayTrongThang.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.rdChonNgayTrongThang.Location = new System.Drawing.Point(248, 67);
			this.rdChonNgayTrongThang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.rdChonNgayTrongThang.Name = "rdChonNgayTrongThang";
			this.rdChonNgayTrongThang.Size = new System.Drawing.Size(149, 19);
			this.rdChonNgayTrongThang.TabIndex = 15;
			this.rdChonNgayTrongThang.TabStop = true;
			this.rdChonNgayTrongThang.Text = "Chọn ngày trong tháng";
			this.rdChonNgayTrongThang.UseVisualStyleBackColor = true;
			this.rdChonNgayTrongThang.CheckedChanged += new System.EventHandler(this.rdChonNgayTrongKhoang_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chk1ExceptSun);
			this.groupBox2.Controls.Add(this.chk1ExceptSat);
			this.groupBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.groupBox2.Location = new System.Drawing.Point(530, 14);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox2.Size = new System.Drawing.Size(214, 46);
			this.groupBox2.TabIndex = 43;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "TRỪ ngày";
			this.toolTip1.SetToolTip(this.groupBox2, "Bỏ qua các ngày thứ 7 hoặc chủ nhật trong tuần");
			// 
			// chk1ExceptSun
			// 
			this.chk1ExceptSun.AutoSize = true;
			this.chk1ExceptSun.Enabled = false;
			this.chk1ExceptSun.Location = new System.Drawing.Point(101, 24);
			this.chk1ExceptSun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.chk1ExceptSun.Name = "chk1ExceptSun";
			this.chk1ExceptSun.Size = new System.Drawing.Size(105, 19);
			this.chk1ExceptSun.TabIndex = 0;
			this.chk1ExceptSun.Text = "TRỪ Chủ nhật";
			this.chk1ExceptSun.UseVisualStyleBackColor = true;
			// 
			// chk1ExceptSat
			// 
			this.chk1ExceptSat.AutoSize = true;
			this.chk1ExceptSat.Enabled = false;
			this.chk1ExceptSat.Location = new System.Drawing.Point(6, 24);
			this.chk1ExceptSat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.chk1ExceptSat.Name = "chk1ExceptSat";
			this.chk1ExceptSat.Size = new System.Drawing.Size(87, 19);
			this.chk1ExceptSat.TabIndex = 0;
			this.chk1ExceptSat.Text = "TRỪ Thứ 7";
			this.chk1ExceptSat.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(250, 248);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Số ngày vắng";
			// 
			// cbSoBuoi
			// 
			this.cbSoBuoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSoBuoi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.cbSoBuoi.FormattingEnabled = true;
			this.cbSoBuoi.Items.AddRange(new object[] {
            "0.5",
            "1",
            "2"});
			this.cbSoBuoi.Location = new System.Drawing.Point(338, 245);
			this.cbSoBuoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cbSoBuoi.Name = "cbSoBuoi";
			this.cbSoBuoi.Size = new System.Drawing.Size(75, 23);
			this.cbSoBuoi.TabIndex = 32;
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(669, 275);
			this.btnThoat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(75, 25);
			this.btnThoat.TabIndex = 6;
			this.btnThoat.Text = "Thoát";
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
			this.gpChonPhongBan.Size = new System.Drawing.Size(229, 120);
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
			this.treePhongBan.Size = new System.Drawing.Size(223, 98);
			this.treePhongBan.TabIndex = 29;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tbSearch);
			this.panel1.Controls.Add(this.btnTim);
			this.panel1.Controls.Add(this.linkHienThiTatCaNV);
			this.panel1.Location = new System.Drawing.Point(3, 126);
			this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(229, 43);
			this.panel1.TabIndex = 0;
			// 
			// tbSearch
			// 
			this.tbSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.tbSearch.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.tbSearch.Location = new System.Drawing.Point(2, 2);
			this.tbSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tbSearch.Name = "tbSearch";
			this.tbSearch.Size = new System.Drawing.Size(184, 21);
			this.tbSearch.TabIndex = 31;
			// 
			// btnTim
			// 
			this.btnTim.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnTim.Location = new System.Drawing.Point(191, 1);
			this.btnTim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnTim.Name = "btnTim";
			this.btnTim.Size = new System.Drawing.Size(40, 22);
			this.btnTim.TabIndex = 32;
			this.btnTim.Text = "Tìm";
			this.btnTim.UseVisualStyleBackColor = true;
			this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
			// 
			// linkHienThiTatCaNV
			// 
			this.linkHienThiTatCaNV.AutoSize = true;
			this.linkHienThiTatCaNV.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.linkHienThiTatCaNV.Location = new System.Drawing.Point(2, 25);
			this.linkHienThiTatCaNV.Name = "linkHienThiTatCaNV";
			this.linkHienThiTatCaNV.Size = new System.Drawing.Size(206, 15);
			this.linkHienThiTatCaNV.TabIndex = 32;
			this.linkHienThiTatCaNV.TabStop = true;
			this.linkHienThiTatCaNV.Text = "Hiển thị tất cả nhân viên trong phòng";
			this.linkHienThiTatCaNV.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHienThiTatCaNV_LinkClicked);
			// 
			// gp1
			// 
			this.gp1.Controls.Add(this.dgrdDSNVTrgPhg);
			this.gp1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gp1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.gp1.Location = new System.Drawing.Point(3, 173);
			this.gp1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gp1.Name = "gp1";
			this.gp1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gp1.Size = new System.Drawing.Size(230, 385);
			this.gp1.TabIndex = 0;
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
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.colg200});
			this.dgrdDSNVTrgPhg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgrdDSNVTrgPhg.Location = new System.Drawing.Point(3, 16);
			this.dgrdDSNVTrgPhg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dgrdDSNVTrgPhg.Name = "dgrdDSNVTrgPhg";
			this.dgrdDSNVTrgPhg.RowHeadersVisible = false;
			this.dgrdDSNVTrgPhg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdDSNVTrgPhg.Size = new System.Drawing.Size(224, 367);
			this.dgrdDSNVTrgPhg.TabIndex = 28;
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
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "UserEnrollNumber";
			dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle5;
			this.dataGridViewTextBoxColumn3.HeaderText = "Mã CC";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.ToolTipText = "Mã Chấm công";
			this.dataGridViewTextBoxColumn3.Width = 55;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.DataPropertyName = "SchName";
			this.dataGridViewTextBoxColumn5.HeaderText = "Lịch trình";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.ToolTipText = "Lịch trình";
			this.dataGridViewTextBoxColumn5.Width = 95;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.DataPropertyName = "TitleName";
			this.dataGridViewTextBoxColumn6.HeaderText = "Chức vụ";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			this.dataGridViewTextBoxColumn6.ToolTipText = "Chức vụ";
			this.dataGridViewTextBoxColumn6.Width = 200;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.DataPropertyName = "Description_1";
			this.dataGridViewTextBoxColumn7.HeaderText = "Phòng ban";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			this.dataGridViewTextBoxColumn7.ToolTipText = "Phòng ban";
			this.dataGridViewTextBoxColumn7.Width = 105;
			// 
			// dataGridViewTextBoxColumn8
			// 
			this.dataGridViewTextBoxColumn8.DataPropertyName = "SchID";
			this.dataGridViewTextBoxColumn8.HeaderText = "Mã lịch trình";
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			this.dataGridViewTextBoxColumn8.ToolTipText = "Mã lịch trình";
			this.dataGridViewTextBoxColumn8.Visible = false;
			// 
			// dataGridViewTextBoxColumn9
			// 
			this.dataGridViewTextBoxColumn9.DataPropertyName = "IDD_1";
			this.dataGridViewTextBoxColumn9.HeaderText = "Mã Phòng ban";
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.ReadOnly = true;
			this.dataGridViewTextBoxColumn9.Visible = false;
			// 
			// colg200
			// 
			this.colg200.DataPropertyName = "cUserInfo";
			this.colg200.HeaderText = "cUserInfo_hide";
			this.colg200.Name = "colg200";
			this.colg200.Visible = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.gpChonPhongBan, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.gp1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 1);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(236, 560);
			this.tableLayoutPanel1.TabIndex = 34;
			// 
			// rdChonNgayTrongKhoang
			// 
			this.rdChonNgayTrongKhoang.AutoSize = true;
			this.rdChonNgayTrongKhoang.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.rdChonNgayTrongKhoang.Location = new System.Drawing.Point(248, 14);
			this.rdChonNgayTrongKhoang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.rdChonNgayTrongKhoang.Name = "rdChonNgayTrongKhoang";
			this.rdChonNgayTrongKhoang.Size = new System.Drawing.Size(98, 19);
			this.rdChonNgayTrongKhoang.TabIndex = 15;
			this.rdChonNgayTrongKhoang.Text = "Chọn từ ngày";
			this.rdChonNgayTrongKhoang.UseVisualStyleBackColor = true;
			this.rdChonNgayTrongKhoang.CheckedChanged += new System.EventHandler(this.rdChonNgayTrongKhoang_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label1.Location = new System.Drawing.Point(264, 44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Đến hết ngày";
			// 
			// dtpNgayBD
			// 
			this.dtpNgayBD.CustomFormat = "d/M/yyyy";
			this.dtpNgayBD.Enabled = false;
			this.dtpNgayBD.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.dtpNgayBD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgayBD.Location = new System.Drawing.Point(426, 13);
			this.dtpNgayBD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dtpNgayBD.Name = "dtpNgayBD";
			this.dtpNgayBD.ShowUpDown = true;
			this.dtpNgayBD.Size = new System.Drawing.Size(98, 21);
			this.dtpNgayBD.TabIndex = 0;
			this.dtpNgayBD.Value = new System.DateTime(2014, 2, 1, 19, 11, 0, 0);
			// 
			// dtpNgayKT
			// 
			this.dtpNgayKT.CustomFormat = "d/M/yyyy";
			this.dtpNgayKT.Enabled = false;
			this.dtpNgayKT.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.dtpNgayKT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgayKT.Location = new System.Drawing.Point(426, 39);
			this.dtpNgayKT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dtpNgayKT.Name = "dtpNgayKT";
			this.dtpNgayKT.ShowUpDown = true;
			this.dtpNgayKT.Size = new System.Drawing.Size(98, 21);
			this.dtpNgayKT.TabIndex = 1;
			this.dtpNgayKT.Value = new System.DateTime(2014, 2, 1, 19, 11, 0, 0);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chk2AllWKDays);
			this.groupBox1.Controls.Add(this.chk2ExceptSun);
			this.groupBox1.Controls.Add(this.chk2ExceptSat);
			this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.groupBox1.Location = new System.Drawing.Point(530, 67);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox1.Size = new System.Drawing.Size(214, 52);
			this.groupBox1.TabIndex = 43;
			this.groupBox1.TabStop = false;
			// 
			// chk2AllWKDays
			// 
			this.chk2AllWKDays.Location = new System.Drawing.Point(6, 1);
			this.chk2AllWKDays.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.chk2AllWKDays.Name = "chk2AllWKDays";
			this.chk2AllWKDays.Size = new System.Drawing.Size(180, 20);
			this.chk2AllWKDays.TabIndex = 0;
			this.chk2AllWKDays.Text = "Chọn từ thứ 2 đến thứ 6";
			this.chk2AllWKDays.UseVisualStyleBackColor = true;
			this.chk2AllWKDays.CheckedChanged += new System.EventHandler(this.chk2AllDays_CheckedChanged);
			// 
			// chk2ExceptSun
			// 
			this.chk2ExceptSun.AutoSize = true;
			this.chk2ExceptSun.Location = new System.Drawing.Point(101, 30);
			this.chk2ExceptSun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.chk2ExceptSun.Name = "chk2ExceptSun";
			this.chk2ExceptSun.Size = new System.Drawing.Size(111, 19);
			this.chk2ExceptSun.TabIndex = 0;
			this.chk2ExceptSun.Text = "Thêm Chủ nhật";
			this.chk2ExceptSun.UseVisualStyleBackColor = true;
			this.chk2ExceptSun.CheckedChanged += new System.EventHandler(this.chk2AllDays_CheckedChanged);
			// 
			// chk2ExceptSat
			// 
			this.chk2ExceptSat.AutoSize = true;
			this.chk2ExceptSat.Location = new System.Drawing.Point(6, 30);
			this.chk2ExceptSat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.chk2ExceptSat.Name = "chk2ExceptSat";
			this.chk2ExceptSat.Size = new System.Drawing.Size(93, 19);
			this.chk2ExceptSat.TabIndex = 0;
			this.chk2ExceptSat.Text = "Thêm Thứ 7";
			this.chk2ExceptSat.UseVisualStyleBackColor = true;
			this.chk2ExceptSat.CheckedChanged += new System.EventHandler(this.chk2AllDays_CheckedChanged);
			// 
			// frm_21_KhaiBaoVang
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(746, 563);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.cbSoBuoi);
			this.Controls.Add(this.rdChonNgayTrongThang);
			this.Controls.Add(this.btnLietKe);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnXoa);
			this.Controls.Add(this.btnThem);
			this.Controls.Add(this.dgrdNgayVang);
			this.Controls.Add(this.dtpNgayKT);
			this.Controls.Add(this.dtpNgayBD);
			this.Controls.Add(this.dtpThang);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checklistNgay);
			this.Controls.Add(this.cbLoaiVang);
			this.Controls.Add(this.rdChonNgayTrongKhoang);
			this.Controls.Add(this.label2);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Name = "frm_21_KhaiBaoVang";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Khai báo vắng cho Nhân viên";
			this.Load += new System.EventHandler(this.frm_KhaiBaoVang_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdNgayVang)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.gpChonPhongBan.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.gp1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.DateTimePicker dtpThang;
		private System.Windows.Forms.CheckedListBox checklistNgay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbLoaiVang;
		private System.Windows.Forms.DataGridView dgrdNgayVang;
        private System.Windows.Forms.Button btnLietKe;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.RadioButton rdChonNgayTrongThang;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbSoBuoi;
		private System.Windows.Forms.DataGridViewTextBoxColumn g4colMaNV;
		private System.Windows.Forms.DataGridViewTextBoxColumn g2macc;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1tennv;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1ngay;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1cong;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1mota;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1userfullcode;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1ID;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1absentcode;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.GroupBox gpChonPhongBan;
		private System.Windows.Forms.TreeView treePhongBan;
		private System.Windows.Forms.TextBox tbSearch;
		private System.Windows.Forms.Button btnTim;
		private System.Windows.Forms.LinkLabel linkHienThiTatCaNV;
		private System.Windows.Forms.GroupBox gp1;
		private System.Windows.Forms.DataGridView dgrdDSNVTrgPhg;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn colg200;
        private System.Windows.Forms.RadioButton rdChonNgayTrongKhoang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpNgayBD;
		private System.Windows.Forms.DateTimePicker dtpNgayKT;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox chk1ExceptSun;
		private System.Windows.Forms.CheckBox chk1ExceptSat;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chk2ExceptSun;
		private System.Windows.Forms.CheckBox chk2ExceptSat;
		private System.Windows.Forms.CheckBox chk2AllWKDays;


    }
}