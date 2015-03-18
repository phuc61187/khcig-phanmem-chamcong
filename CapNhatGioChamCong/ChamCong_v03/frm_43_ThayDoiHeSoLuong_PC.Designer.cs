namespace ChamCong_v03 {
	partial class frm_43_ThayDoiHeSoLuong_PC {
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
			this.label1 = new System.Windows.Forms.Label();
			this.tbMaCC = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbTenNV = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnThucHien = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.gp1 = new System.Windows.Forms.GroupBox();
			this.linkHienThiTatCaNV = new System.Windows.Forms.LinkLabel();
			this.tbSearch = new System.Windows.Forms.TextBox();
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
			this.g1c10 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.g1c11 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.label5 = new System.Windows.Forms.Label();
			this.btnTim = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.tbHSBHCongThem = new System.Windows.Forms.MaskedTextBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.checkUserEnabled = new System.Windows.Forms.CheckBox();
			this.tbHSLCB = new System.Windows.Forms.MaskedTextBox();
			this.tbHSLCV = new System.Windows.Forms.MaskedTextBox();
			this.checkTinhLuongCongNhat = new System.Windows.Forms.CheckBox();
			this.gpChonPhongBan.SuspendLayout();
			this.gp1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label1.Location = new System.Drawing.Point(246, 292);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Mã Nhân viên";
			// 
			// tbMaCC
			// 
			this.tbMaCC.Location = new System.Drawing.Point(380, 289);
			this.tbMaCC.Name = "tbMaCC";
			this.tbMaCC.ReadOnly = true;
			this.tbMaCC.Size = new System.Drawing.Size(83, 21);
			this.tbMaCC.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label2.Location = new System.Drawing.Point(469, 292);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86, 15);
			this.label2.TabIndex = 0;
			this.label2.Text = "Tên Nhân viên";
			// 
			// tbTenNV
			// 
			this.tbTenNV.Location = new System.Drawing.Point(626, 289);
			this.tbTenNV.Name = "tbTenNV";
			this.tbTenNV.ReadOnly = true;
			this.tbTenNV.Size = new System.Drawing.Size(190, 21);
			this.tbTenNV.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label3.Location = new System.Drawing.Point(246, 318);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "Hệ số lương cơ bản";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label4.Location = new System.Drawing.Point(469, 318);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(136, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "Hệ số lương sản phẩm";
			// 
			// btnThucHien
			// 
			this.btnThucHien.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThucHien.ForeColor = System.Drawing.Color.Blue;
			this.btnThucHien.Location = new System.Drawing.Point(249, 392);
			this.btnThucHien.Name = "btnThucHien";
			this.btnThucHien.Size = new System.Drawing.Size(83, 25);
			this.btnThucHien.TabIndex = 4;
			this.btnThucHien.Text = "Thực hiện";
			this.btnThucHien.UseVisualStyleBackColor = true;
			this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(472, 392);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(83, 25);
			this.btnThoat.TabIndex = 5;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// gpChonPhongBan
			// 
			this.gpChonPhongBan.Controls.Add(this.treePhongBan);
			this.gpChonPhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.gpChonPhongBan.Location = new System.Drawing.Point(12, 11);
			this.gpChonPhongBan.Name = "gpChonPhongBan";
			this.gpChonPhongBan.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.gpChonPhongBan.Size = new System.Drawing.Size(213, 353);
			this.gpChonPhongBan.TabIndex = 2;
			this.gpChonPhongBan.TabStop = false;
			this.gpChonPhongBan.Text = "Chọn phòng ban";
			// 
			// treePhongBan
			// 
			this.treePhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treePhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.treePhongBan.Indent = 18;
			this.treePhongBan.ItemHeight = 20;
			this.treePhongBan.Location = new System.Drawing.Point(3, 19);
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
			this.treePhongBan.Size = new System.Drawing.Size(207, 332);
			this.treePhongBan.TabIndex = 0;
			// 
			// gp1
			// 
			this.gp1.Controls.Add(this.linkHienThiTatCaNV);
			this.gp1.Controls.Add(this.tbSearch);
			this.gp1.Controls.Add(this.dgrdDSNVTrgPhg);
			this.gp1.Controls.Add(this.label5);
			this.gp1.Controls.Add(this.btnTim);
			this.gp1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.gp1.Location = new System.Drawing.Point(231, 11);
			this.gp1.Name = "gp1";
			this.gp1.Size = new System.Drawing.Size(591, 272);
			this.gp1.TabIndex = 1;
			this.gp1.TabStop = false;
			this.gp1.Text = "Danh sách Nhân viên";
			// 
			// linkHienThiTatCaNV
			// 
			this.linkHienThiTatCaNV.AutoSize = true;
			this.linkHienThiTatCaNV.Location = new System.Drawing.Point(401, 24);
			this.linkHienThiTatCaNV.Name = "linkHienThiTatCaNV";
			this.linkHienThiTatCaNV.Size = new System.Drawing.Size(169, 15);
			this.linkHienThiTatCaNV.TabIndex = 33;
			this.linkHienThiTatCaNV.TabStop = true;
			this.linkHienThiTatCaNV.Text = "Hiển thị tất cả NV trong phòng";
			this.linkHienThiTatCaNV.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHienThiTatCaNV_LinkClicked);
			// 
			// tbSearch
			// 
			this.tbSearch.Location = new System.Drawing.Point(101, 22);
			this.tbSearch.Name = "tbSearch";
			this.tbSearch.Size = new System.Drawing.Size(237, 21);
			this.tbSearch.TabIndex = 2;
			// 
			// dgrdDSNVTrgPhg
			// 
			this.dgrdDSNVTrgPhg.AllowUserToAddRows = false;
			this.dgrdDSNVTrgPhg.AllowUserToDeleteRows = false;
			this.dgrdDSNVTrgPhg.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.dgrdDSNVTrgPhg.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgrdDSNVTrgPhg.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgrdDSNVTrgPhg.ColumnHeadersHeight = 25;
			this.dgrdDSNVTrgPhg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgrdDSNVTrgPhg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cChkDSNV2,
            this.cUserFullCodeDSNV2,
            this.cUserEnrollNumberDSNV2,
            this.cUserFullNameDSNV2,
            this.cTitleNameDSNV2,
            this.cDescriptionDSNV2,
            this.cSchIDDSNV2,
            this.cUserIDDDSNV2,
            this.cSchNameDSNV2,
            this.g1c10,
            this.g1c11});
			this.dgrdDSNVTrgPhg.Location = new System.Drawing.Point(6, 49);
			this.dgrdDSNVTrgPhg.Name = "dgrdDSNVTrgPhg";
			this.dgrdDSNVTrgPhg.ReadOnly = true;
			this.dgrdDSNVTrgPhg.RowHeadersVisible = false;
			this.dgrdDSNVTrgPhg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdDSNVTrgPhg.Size = new System.Drawing.Size(579, 218);
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
			// g1c10
			// 
			this.g1c10.DataPropertyName = "UserEnabled";
			this.g1c10.FalseValue = "false";
			this.g1c10.HeaderText = "UserEnabled_hide";
			this.g1c10.Name = "g1c10";
			this.g1c10.ReadOnly = true;
			this.g1c10.TrueValue = "true";
			this.g1c10.Visible = false;
			// 
			// g1c11
			// 
			this.g1c11.DataPropertyName = "TinhLuongCongNhat";
			this.g1c11.FalseValue = "false";
			this.g1c11.HeaderText = "TinhLuongCongNhat_hide";
			this.g1c11.Name = "g1c11";
			this.g1c11.ReadOnly = true;
			this.g1c11.TrueValue = "true";
			this.g1c11.Visible = false;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label5.Location = new System.Drawing.Point(6, 24);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(76, 15);
			this.label5.TabIndex = 0;
			this.label5.Text = "Nhập tên NV";
			// 
			// btnTim
			// 
			this.btnTim.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnTim.ForeColor = System.Drawing.Color.Blue;
			this.btnTim.Location = new System.Drawing.Point(344, 20);
			this.btnTim.Name = "btnTim";
			this.btnTim.Size = new System.Drawing.Size(51, 25);
			this.btnTim.TabIndex = 4;
			this.btnTim.Text = "Tìm";
			this.btnTim.UseVisualStyleBackColor = true;
			this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label6.Location = new System.Drawing.Point(246, 344);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(121, 15);
			this.label6.TabIndex = 0;
			this.label6.Text = "Hệ số BH cộng thêm";
			// 
			// tbHSBHCongThem
			// 
			this.tbHSBHCongThem.Location = new System.Drawing.Point(380, 341);
			this.tbHSBHCongThem.Mask = "0.00";
			this.tbHSBHCongThem.Name = "tbHSBHCongThem";
			this.tbHSBHCongThem.Size = new System.Drawing.Size(83, 21);
			this.tbHSBHCongThem.TabIndex = 8;
			this.tbHSBHCongThem.Text = "000";
			this.toolTip1.SetToolTip(this.tbHSBHCongThem, "Hệ số bảo hiểm cộng thêm cho chức danh Giám đốc và Phó Giám đốc.");
			// 
			// toolTip1
			// 
			this.toolTip1.ToolTipTitle = "Chọn mục này nếu nhân viên tính này làm việc công nhật";
			// 
			// checkUserEnabled
			// 
			this.checkUserEnabled.AutoSize = true;
			this.checkUserEnabled.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.checkUserEnabled.Location = new System.Drawing.Point(249, 368);
			this.checkUserEnabled.Name = "checkUserEnabled";
			this.checkUserEnabled.Size = new System.Drawing.Size(145, 19);
			this.checkUserEnabled.TabIndex = 9;
			this.checkUserEnabled.Text = "Cho phép chấm công";
			this.toolTip1.SetToolTip(this.checkUserEnabled, "Bỏ check mục này nếu đã nghỉ việc");
			this.checkUserEnabled.UseVisualStyleBackColor = true;
			// 
			// tbHSLCB
			// 
			this.tbHSLCB.Location = new System.Drawing.Point(380, 315);
			this.tbHSLCB.Mask = "0.00";
			this.tbHSLCB.Name = "tbHSLCB";
			this.tbHSLCB.Size = new System.Drawing.Size(83, 21);
			this.tbHSLCB.TabIndex = 8;
			this.tbHSLCB.Text = "000";
			// 
			// tbHSLCV
			// 
			this.tbHSLCV.Location = new System.Drawing.Point(626, 315);
			this.tbHSLCV.Mask = "0.00";
			this.tbHSLCV.Name = "tbHSLCV";
			this.tbHSLCV.Size = new System.Drawing.Size(83, 21);
			this.tbHSLCV.TabIndex = 8;
			this.tbHSLCV.Text = "000";
			// 
			// checkTinhLuongCongNhat
			// 
			this.checkTinhLuongCongNhat.AutoSize = true;
			this.checkTinhLuongCongNhat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.checkTinhLuongCongNhat.Location = new System.Drawing.Point(472, 368);
			this.checkTinhLuongCongNhat.Name = "checkTinhLuongCongNhat";
			this.checkTinhLuongCongNhat.Size = new System.Drawing.Size(147, 19);
			this.checkTinhLuongCongNhat.TabIndex = 9;
			this.checkTinhLuongCongNhat.Text = "Tính lương công nhật";
			this.checkTinhLuongCongNhat.UseVisualStyleBackColor = true;
			// 
			// frm_43_ThayDoiHeSoLuong_PC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(834, 427);
			this.Controls.Add(this.checkUserEnabled);
			this.Controls.Add(this.checkTinhLuongCongNhat);
			this.Controls.Add(this.tbHSLCV);
			this.Controls.Add(this.tbHSLCB);
			this.Controls.Add(this.tbHSBHCongThem);
			this.Controls.Add(this.gpChonPhongBan);
			this.Controls.Add(this.gp1);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnThucHien);
			this.Controls.Add(this.tbTenNV);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbMaCC);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_43_ThayDoiHeSoLuong_PC";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Thay đổi hệ số lương và hệ số bảo hiểm cộng thêm cho nhân viên";
			this.Load += new System.EventHandler(this.frm_DieuChinhLuongThangTruoc_Load);
			this.gpChonPhongBan.ResumeLayout(false);
			this.gp1.ResumeLayout(false);
			this.gp1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbMaCC;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbTenNV;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnThucHien;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.GroupBox gpChonPhongBan;
		private System.Windows.Forms.TreeView treePhongBan;
		private System.Windows.Forms.GroupBox gp1;
		private System.Windows.Forms.DataGridView dgrdDSNVTrgPhg;
		private System.Windows.Forms.TextBox tbSearch;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnTim;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.MaskedTextBox tbHSBHCongThem;
		private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MaskedTextBox tbHSLCB;
        private System.Windows.Forms.MaskedTextBox tbHSLCV;
		private System.Windows.Forms.LinkLabel linkHienThiTatCaNV;
		private System.Windows.Forms.CheckBox checkTinhLuongCongNhat;
		private System.Windows.Forms.DataGridViewCheckBoxColumn cChkDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullCodeDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserEnrollNumberDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cTitleNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cDescriptionDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cSchIDDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserIDDDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cSchNameDSNV2;
		private System.Windows.Forms.DataGridViewCheckBoxColumn g1c10;
		private System.Windows.Forms.DataGridViewCheckBoxColumn g1c11;
		private System.Windows.Forms.CheckBox checkUserEnabled;
	}
}