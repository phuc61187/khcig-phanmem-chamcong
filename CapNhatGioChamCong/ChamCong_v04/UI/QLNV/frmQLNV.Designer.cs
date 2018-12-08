namespace ChamCong_v04.UI.QLNV {
	partial class frmQLNV {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
            this.treePhongBan = new System.Windows.Forms.TreeView();
            this.gp1 = new System.Windows.Forms.GroupBox();
            this.dgrdDSNVTrgPhg = new System.Windows.Forms.DataGridView();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.linkHienThiTatCaNV = new System.Windows.Forms.LinkLabel();
            this.btnCapnhatCongnhat = new System.Windows.Forms.Button();
            this.toolTipHint = new System.Windows.Forms.ToolTip(this.components);
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.g1c1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g1c2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g1c3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g1c11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g1c13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g1c14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HSLCBTT17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HSPCCV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HSPCDH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HSPCTN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g1c17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g1c16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g1c19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g1c20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g1c23 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colNVNhaMay = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.g1c24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.G1C22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g1c12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g1c21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpChonPhongBan.SuspendLayout();
            this.gp1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).BeginInit();
            this.SuspendLayout();
            // 
            // gpChonPhongBan
            // 
            this.gpChonPhongBan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gpChonPhongBan.Controls.Add(this.treePhongBan);
            this.gpChonPhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
            this.gpChonPhongBan.Location = new System.Drawing.Point(1, 1);
            this.gpChonPhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpChonPhongBan.Name = "gpChonPhongBan";
            this.gpChonPhongBan.Padding = new System.Windows.Forms.Padding(3, 7, 3, 2);
            this.gpChonPhongBan.Size = new System.Drawing.Size(213, 526);
            this.gpChonPhongBan.TabIndex = 1;
            this.gpChonPhongBan.TabStop = false;
            this.gpChonPhongBan.Text = "Chọn phòng ban";
            // 
            // treePhongBan
            // 
            this.treePhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treePhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
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
            this.treePhongBan.Size = new System.Drawing.Size(207, 503);
            this.treePhongBan.TabIndex = 0;
            // 
            // gp1
            // 
            this.gp1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gp1.Controls.Add(this.dgrdDSNVTrgPhg);
            this.gp1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
            this.gp1.Location = new System.Drawing.Point(220, 70);
            this.gp1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gp1.Name = "gp1";
            this.gp1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gp1.Size = new System.Drawing.Size(865, 457);
            this.gp1.TabIndex = 2;
            this.gp1.TabStop = false;
            this.gp1.Text = "Danh sách Nhân viên";
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
            this.check,
            this.g1c1,
            this.g1c2,
            this.g1c3,
            this.g1c11,
            this.dataGridViewTextBoxColumn7,
            this.g1c13,
            this.g1c14,
            this.HSLCBTT17,
            this.HSPCCV,
            this.HSPCDH,
            this.HSPCTN,
            this.g1c17,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.g1c16,
            this.g1c19,
            this.g1c20,
            this.g1c23,
            this.colNVNhaMay,
            this.g1c24,
            this.G1C22,
            this.g1c12,
            this.g1c21,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9});
            this.dgrdDSNVTrgPhg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrdDSNVTrgPhg.Location = new System.Drawing.Point(3, 16);
            this.dgrdDSNVTrgPhg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgrdDSNVTrgPhg.Name = "dgrdDSNVTrgPhg";
            this.dgrdDSNVTrgPhg.RowHeadersVisible = false;
            this.dgrdDSNVTrgPhg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrdDSNVTrgPhg.Size = new System.Drawing.Size(859, 439);
            this.dgrdDSNVTrgPhg.TabIndex = 28;
            // 
            // btnThem
            // 
            this.btnThem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnThem.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnThem.ForeColor = System.Drawing.Color.Blue;
            this.btnThem.Location = new System.Drawing.Point(492, 9);
            this.btnThem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(66, 25);
            this.btnThem.TabIndex = 3;
            this.btnThem.Text = "Thêm NV";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnXoa.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnXoa.ForeColor = System.Drawing.Color.Blue;
            this.btnXoa.Location = new System.Drawing.Point(822, 9);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(66, 25);
            this.btnXoa.TabIndex = 6;
            this.btnXoa.Text = "Xoá NV";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCapNhat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnCapNhat.ForeColor = System.Drawing.Color.Blue;
            this.btnCapNhat.Location = new System.Drawing.Point(564, 9);
            this.btnCapNhat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(66, 25);
            this.btnCapNhat.TabIndex = 4;
            this.btnCapNhat.Text = "Cập nhật";
            this.toolTipHint.SetToolTip(this.btnCapNhat, "Đánh dấu các NV cần \r\ncập nhật thông tin\r\nđể thực hiện");
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // tbSearch
            // 
            this.tbSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbSearch.Location = new System.Drawing.Point(219, 11);
            this.tbSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(214, 21);
            this.tbSearch.TabIndex = 0;
            this.toolTipHint.SetToolTip(this.tbSearch, "Nhập TÊN hoặc Mã NV để tìm kiếm nhân viên");
            this.tbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
            // 
            // btnTim
            // 
            this.btnTim.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnTim.Location = new System.Drawing.Point(439, 9);
            this.btnTim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(47, 25);
            this.btnTim.TabIndex = 1;
            this.btnTim.Text = "Tìm";
            this.toolTipHint.SetToolTip(this.btnTim, "Nhập TÊN hoặc Mã NV để tìm kiếm nhân viên");
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // linkHienThiTatCaNV
            // 
            this.linkHienThiTatCaNV.AutoSize = true;
            this.linkHienThiTatCaNV.Location = new System.Drawing.Point(219, 41);
            this.linkHienThiTatCaNV.Name = "linkHienThiTatCaNV";
            this.linkHienThiTatCaNV.Size = new System.Drawing.Size(206, 15);
            this.linkHienThiTatCaNV.TabIndex = 2;
            this.linkHienThiTatCaNV.TabStop = true;
            this.linkHienThiTatCaNV.Text = "Hiển thị tất cả nhân viên trong phòng";
            this.toolTipHint.SetToolTip(this.linkHienThiTatCaNV, "Nhấn vào đây để hiển thị danh sách \r\nNV trong phòng ban được chọn");
            this.linkHienThiTatCaNV.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHienThiTatCaNV_LinkClicked);
            // 
            // btnCapnhatCongnhat
            // 
            this.btnCapnhatCongnhat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCapnhatCongnhat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnCapnhatCongnhat.ForeColor = System.Drawing.Color.Blue;
            this.btnCapnhatCongnhat.Location = new System.Drawing.Point(636, 9);
            this.btnCapnhatCongnhat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCapnhatCongnhat.Name = "btnCapnhatCongnhat";
            this.btnCapnhatCongnhat.Size = new System.Drawing.Size(180, 25);
            this.btnCapnhatCongnhat.TabIndex = 5;
            this.btnCapnhatCongnhat.Text = "Cập nhật làm việc công nhật";
            this.btnCapnhatCongnhat.Click += new System.EventHandler(this.btnCapnhatCongnhat_Click);
            // 
            // toolTipHint
            // 
            this.toolTipHint.AutoPopDelay = 30000;
            this.toolTipHint.InitialDelay = 300;
            this.toolTipHint.OwnerDraw = true;
            this.toolTipHint.ReshowDelay = 100;
            this.toolTipHint.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipHint_Draw);
            this.toolTipHint.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTipHint_Popup);
            // 
            // check
            // 
            this.check.DataPropertyName = "check";
            this.check.FalseValue = "false";
            this.check.Frozen = true;
            this.check.HeaderText = "";
            this.check.Name = "check";
            this.check.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.check.TrueValue = "true";
            this.check.Width = 22;
            // 
            // g1c1
            // 
            this.g1c1.DataPropertyName = "UserFullCode";
            this.g1c1.Frozen = true;
            this.g1c1.HeaderText = "Mã NV";
            this.g1c1.Name = "g1c1";
            this.g1c1.ReadOnly = true;
            this.g1c1.ToolTipText = "Mã Nhân viên";
            this.g1c1.Width = 55;
            // 
            // g1c2
            // 
            this.g1c2.DataPropertyName = "UserFullName";
            this.g1c2.Frozen = true;
            this.g1c2.HeaderText = "Họ và tên NV";
            this.g1c2.Name = "g1c2";
            this.g1c2.ReadOnly = true;
            this.g1c2.ToolTipText = "Họ và tên Nhân viên";
            this.g1c2.Width = 150;
            // 
            // g1c3
            // 
            this.g1c3.DataPropertyName = "UserEnrollNumber";
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.g1c3.DefaultCellStyle = dataGridViewCellStyle2;
            this.g1c3.Frozen = true;
            this.g1c3.HeaderText = "Mã CC";
            this.g1c3.Name = "g1c3";
            this.g1c3.ReadOnly = true;
            this.g1c3.ToolTipText = "Mã Chấm công";
            this.g1c3.Width = 55;
            // 
            // g1c11
            // 
            this.g1c11.DataPropertyName = "UserLastName";
            this.g1c11.Frozen = true;
            this.g1c11.HeaderText = "Tên NV";
            this.g1c11.Name = "g1c11";
            this.g1c11.ReadOnly = true;
            this.g1c11.ToolTipText = "Tên NV";
            this.g1c11.Width = 80;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "TenPhong";
            this.dataGridViewTextBoxColumn7.Frozen = true;
            this.dataGridViewTextBoxColumn7.HeaderText = "Phòng ban";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.ToolTipText = "Phòng ban";
            this.dataGridViewTextBoxColumn7.Width = 105;
            // 
            // g1c13
            // 
            this.g1c13.DataPropertyName = "HeSoLuongCB";
            dataGridViewCellStyle3.Format = "0.00";
            this.g1c13.DefaultCellStyle = dataGridViewCellStyle3;
            this.g1c13.Frozen = true;
            this.g1c13.HeaderText = "HSLCB";
            this.g1c13.Name = "g1c13";
            this.g1c13.ReadOnly = true;
            this.g1c13.ToolTipText = "Hệ số lương cơ bản";
            this.g1c13.Width = 55;
            // 
            // g1c14
            // 
            this.g1c14.DataPropertyName = "HeSoLuongSP";
            dataGridViewCellStyle4.Format = "0.00";
            this.g1c14.DefaultCellStyle = dataGridViewCellStyle4;
            this.g1c14.Frozen = true;
            this.g1c14.HeaderText = "HSLCV";
            this.g1c14.Name = "g1c14";
            this.g1c14.ReadOnly = true;
            this.g1c14.ToolTipText = "Hệ số lương công việc";
            this.g1c14.Width = 55;
            // 
            // HSLCBTT17
            // 
            this.HSLCBTT17.DataPropertyName = "HSLCBTT17";
            this.HSLCBTT17.Frozen = true;
            this.HSLCBTT17.HeaderText = "HSLCB TT17";
            this.HSLCBTT17.Name = "HSLCBTT17";
            this.HSLCBTT17.ReadOnly = true;
            this.HSLCBTT17.ToolTipText = "Hệ số Lương CB theo Thông tư 17";
            this.HSLCBTT17.Width = 85;
            // 
            // HSPCCV
            // 
            this.HSPCCV.DataPropertyName = "HSPCCV";
            this.HSPCCV.Frozen = true;
            this.HSPCCV.HeaderText = "HSPCCV";
            this.HSPCCV.Name = "HSPCCV";
            this.HSPCCV.ReadOnly = true;
            this.HSPCCV.ToolTipText = "Hệ số Phụ cấp công việc";
            this.HSPCCV.Width = 60;
            // 
            // HSPCDH
            // 
            this.HSPCDH.DataPropertyName = "HSPCDH";
            this.HSPCDH.Frozen = true;
            this.HSPCDH.HeaderText = "HSPCĐH";
            this.HSPCDH.Name = "HSPCDH";
            this.HSPCDH.ReadOnly = true;
            this.HSPCDH.ToolTipText = "Hệ số Phụ cấp độc hại";
            this.HSPCDH.Width = 60;
            // 
            // HSPCTN
            // 
            this.HSPCTN.DataPropertyName = "HSPCTN";
            this.HSPCTN.Frozen = true;
            this.HSPCTN.HeaderText = "HSPCTN";
            this.HSPCTN.Name = "HSPCTN";
            this.HSPCTN.ReadOnly = true;
            this.HSPCTN.ToolTipText = "Hệ số Phụ cấp trách nhiệm";
            this.HSPCTN.Width = 60;
            // 
            // g1c17
            // 
            this.g1c17.DataPropertyName = "HSBHCongThem";
            dataGridViewCellStyle5.Format = "0.00";
            this.g1c17.DefaultCellStyle = dataGridViewCellStyle5;
            this.g1c17.Frozen = true;
            this.g1c17.HeaderText = "Hệ Số BHXH";
            this.g1c17.Name = "g1c17";
            this.g1c17.ReadOnly = true;
            this.g1c17.ToolTipText = "Hệ số BHXH của lãnh đạo";
            this.g1c17.Width = 90;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "SchName";
            this.dataGridViewTextBoxColumn5.Frozen = true;
            this.dataGridViewTextBoxColumn5.HeaderText = "Lịch trình";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.ToolTipText = "Lịch trình";
            this.dataGridViewTextBoxColumn5.Width = 95;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ChucVu";
            this.dataGridViewTextBoxColumn6.Frozen = true;
            this.dataGridViewTextBoxColumn6.HeaderText = "Chức vụ";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.ToolTipText = "Chức vụ";
            this.dataGridViewTextBoxColumn6.Width = 200;
            // 
            // g1c16
            // 
            this.g1c16.DataPropertyName = "UserCardNo";
            this.g1c16.Frozen = true;
            this.g1c16.HeaderText = "Mã thẻ từ";
            this.g1c16.Name = "g1c16";
            this.g1c16.ReadOnly = true;
            this.g1c16.ToolTipText = "Mã thẻ từ";
            // 
            // g1c19
            // 
            this.g1c19.DataPropertyName = "UserSex";
            this.g1c19.Frozen = true;
            this.g1c19.HeaderText = "Giới tính";
            this.g1c19.Name = "g1c19";
            this.g1c19.ReadOnly = true;
            this.g1c19.ToolTipText = "Giới tính";
            this.g1c19.Width = 60;
            // 
            // g1c20
            // 
            this.g1c20.DataPropertyName = "UserBirthDay";
            this.g1c20.Frozen = true;
            this.g1c20.HeaderText = "Ngày sinh";
            this.g1c20.Name = "g1c20";
            this.g1c20.ReadOnly = true;
            this.g1c20.ToolTipText = "Ngày sinh";
            this.g1c20.Width = 80;
            // 
            // g1c23
            // 
            this.g1c23.DataPropertyName = "UserEnabled";
            this.g1c23.FalseValue = "False";
            this.g1c23.Frozen = true;
            this.g1c23.HeaderText = "Enabled";
            this.g1c23.Name = "g1c23";
            this.g1c23.ReadOnly = true;
            this.g1c23.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.g1c23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.g1c23.ToolTipText = "Cho phép chấm công";
            this.g1c23.TrueValue = "True";
            this.g1c23.Width = 55;
            // 
            // colNVNhaMay
            // 
            this.colNVNhaMay.DataPropertyName = "NVNhaMay";
            this.colNVNhaMay.FalseValue = "False";
            this.colNVNhaMay.Frozen = true;
            this.colNVNhaMay.HeaderText = "NV Nhà máy";
            this.colNVNhaMay.Name = "colNVNhaMay";
            this.colNVNhaMay.ToolTipText = "Nhân viên Nhà máy hay NV Nhân Kiệt";
            this.colNVNhaMay.TrueValue = "True";
            this.colNVNhaMay.Width = 55;
            // 
            // g1c24
            // 
            this.g1c24.DataPropertyName = "UserHireDay";
            this.g1c24.Frozen = true;
            this.g1c24.HeaderText = "Ngày vào làm";
            this.g1c24.Name = "g1c24";
            this.g1c24.ReadOnly = true;
            // 
            // G1C22
            // 
            this.G1C22.DataPropertyName = "UserPrivilege";
            this.G1C22.HeaderText = "Loại NV_hide";
            this.G1C22.Name = "G1C22";
            this.G1C22.ReadOnly = true;
            this.G1C22.ToolTipText = "Loại nhân viên";
            this.G1C22.Visible = false;
            this.G1C22.Width = 75;
            // 
            // g1c12
            // 
            this.g1c12.DataPropertyName = "UserEnrollName";
            this.g1c12.HeaderText = "UserEnrollName_hide";
            this.g1c12.Name = "g1c12";
            this.g1c12.ReadOnly = true;
            this.g1c12.Visible = false;
            // 
            // g1c21
            // 
            this.g1c21.HeaderText = "USERBIRTHPLACE_HIDE";
            this.g1c21.Name = "g1c21";
            this.g1c21.ReadOnly = true;
            this.g1c21.Visible = false;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "SchID";
            this.dataGridViewTextBoxColumn8.HeaderText = "Mã lịch trình_HIDE";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.ToolTipText = "Mã lịch trình";
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "MaPhong";
            this.dataGridViewTextBoxColumn9.HeaderText = "Mã Phòng ban_HIDE";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Visible = false;
            // 
            // frmQLNV
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1088, 528);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.linkHienThiTatCaNV);
            this.Controls.Add(this.btnCapnhatCongnhat);
            this.Controls.Add(this.btnCapNhat);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.gp1);
            this.Controls.Add(this.gpChonPhongBan);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
            this.Name = "frmQLNV";
            this.Text = "Quản lý nhân viên";
            this.Load += new System.EventHandler(this.frmQLNV_Load);
            this.gpChonPhongBan.ResumeLayout(false);
            this.gp1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox gpChonPhongBan;
		private System.Windows.Forms.TreeView treePhongBan;
		private System.Windows.Forms.GroupBox gp1;
		private System.Windows.Forms.DataGridView dgrdDSNVTrgPhg;
		private System.Windows.Forms.Button btnThem;
		private System.Windows.Forms.Button btnXoa;
		private System.Windows.Forms.Button btnCapNhat;
		private System.Windows.Forms.TextBox tbSearch;
		private System.Windows.Forms.Button btnTim;
		private System.Windows.Forms.LinkLabel linkHienThiTatCaNV;
		private System.Windows.Forms.Button btnCapnhatCongnhat;
		private System.Windows.Forms.ToolTip toolTipHint;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1c1;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1c2;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1c3;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1c11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1c13;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1c14;
        private System.Windows.Forms.DataGridViewTextBoxColumn HSLCBTT17;
        private System.Windows.Forms.DataGridViewTextBoxColumn HSPCCV;
        private System.Windows.Forms.DataGridViewTextBoxColumn HSPCDH;
        private System.Windows.Forms.DataGridViewTextBoxColumn HSPCTN;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1c17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1c16;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1c19;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1c20;
        private System.Windows.Forms.DataGridViewCheckBoxColumn g1c23;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colNVNhaMay;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1c24;
        private System.Windows.Forms.DataGridViewTextBoxColumn G1C22;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1c12;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1c21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
    }
}