namespace ChamCong_v03.DieuChinhLuong {
	partial class frmThemDieuChinhLuong {
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
			this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
			this.treePhongBan = new System.Windows.Forms.TreeView();
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
			this.numThuChiKhac = new System.Windows.Forms.NumericUpDown();
			this.numTamUngThang = new System.Windows.Forms.NumericUpDown();
			this.numLuongDieuChinh = new System.Windows.Forms.NumericUpDown();
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.btnThoat = new System.Windows.Forms.Button();
			this.btnThucHien = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tbSearch = new System.Windows.Forms.TextBox();
			this.btnTim = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.linkHienThiTatCaNV = new System.Windows.Forms.LinkLabel();
			this.tbMucDongBH = new System.Windows.Forms.MaskedTextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.gpChonPhongBan.SuspendLayout();
			this.gp1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numThuChiKhac)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numTamUngThang)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numLuongDieuChinh)).BeginInit();
			this.SuspendLayout();
			// 
			// gpChonPhongBan
			// 
			this.gpChonPhongBan.Controls.Add(this.treePhongBan);
			this.gpChonPhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.gpChonPhongBan.Location = new System.Drawing.Point(1, 1);
			this.gpChonPhongBan.Name = "gpChonPhongBan";
			this.gpChonPhongBan.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.gpChonPhongBan.Size = new System.Drawing.Size(207, 376);
			this.gpChonPhongBan.TabIndex = 3;
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
			this.treePhongBan.Size = new System.Drawing.Size(201, 355);
			this.treePhongBan.TabIndex = 0;
			this.treePhongBan.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treePhongBan_AfterSelect);
			// 
			// gp1
			// 
			this.gp1.Controls.Add(this.dgrdDSNVTrgPhg);
			this.gp1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.gp1.Location = new System.Drawing.Point(214, 1);
			this.gp1.Name = "gp1";
			this.gp1.Size = new System.Drawing.Size(239, 376);
			this.gp1.TabIndex = 4;
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
			this.dgrdDSNVTrgPhg.Location = new System.Drawing.Point(3, 17);
			this.dgrdDSNVTrgPhg.Name = "dgrdDSNVTrgPhg";
			this.dgrdDSNVTrgPhg.RowHeadersVisible = false;
			this.dgrdDSNVTrgPhg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdDSNVTrgPhg.Size = new System.Drawing.Size(233, 356);
			this.dgrdDSNVTrgPhg.TabIndex = 29;
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
			dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle2;
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
			// numThuChiKhac
			// 
			this.numThuChiKhac.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numThuChiKhac.Location = new System.Drawing.Point(579, 180);
			this.numThuChiKhac.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.numThuChiKhac.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147483648});
			this.numThuChiKhac.Name = "numThuChiKhac";
			this.numThuChiKhac.Size = new System.Drawing.Size(179, 21);
			this.numThuChiKhac.TabIndex = 6;
			this.numThuChiKhac.ThousandsSeparator = true;
			// 
			// numTamUngThang
			// 
			this.numTamUngThang.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numTamUngThang.Location = new System.Drawing.Point(579, 128);
			this.numTamUngThang.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.numTamUngThang.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147483648});
			this.numTamUngThang.Name = "numTamUngThang";
			this.numTamUngThang.Size = new System.Drawing.Size(179, 21);
			this.numTamUngThang.TabIndex = 5;
			this.numTamUngThang.ThousandsSeparator = true;
			// 
			// numLuongDieuChinh
			// 
			this.numLuongDieuChinh.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numLuongDieuChinh.Location = new System.Drawing.Point(579, 154);
			this.numLuongDieuChinh.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.numLuongDieuChinh.Name = "numLuongDieuChinh";
			this.numLuongDieuChinh.Size = new System.Drawing.Size(179, 21);
			this.numLuongDieuChinh.TabIndex = 4;
			this.numLuongDieuChinh.ThousandsSeparator = true;
			// 
			// dtpThang
			// 
			this.dtpThang.CustomFormat = "M / yyyy";
			this.dtpThang.Enabled = false;
			this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpThang.Location = new System.Drawing.Point(579, 101);
			this.dtpThang.Name = "dtpThang";
			this.dtpThang.ShowUpDown = true;
			this.dtpThang.Size = new System.Drawing.Size(83, 21);
			this.dtpThang.TabIndex = 3;
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(675, 232);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(83, 25);
			this.btnThoat.TabIndex = 8;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnThucHien
			// 
			this.btnThucHien.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThucHien.ForeColor = System.Drawing.Color.Blue;
			this.btnThucHien.Location = new System.Drawing.Point(579, 232);
			this.btnThucHien.Name = "btnThucHien";
			this.btnThucHien.Size = new System.Drawing.Size(83, 25);
			this.btnThucHien.TabIndex = 7;
			this.btnThucHien.Text = "Thực hiện";
			this.btnThucHien.UseVisualStyleBackColor = true;
			this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label7.Location = new System.Drawing.Point(484, 182);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(79, 15);
			this.label7.TabIndex = 6;
			this.label7.Text = "Thu chi khác";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label6.Location = new System.Drawing.Point(456, 156);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(107, 15);
			this.label6.TabIndex = 7;
			this.label6.Text = "Lương điều chỉnh";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label4.Location = new System.Drawing.Point(468, 129);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 15);
			this.label4.TabIndex = 8;
			this.label4.Text = "Tạm ứng tháng";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label3.Location = new System.Drawing.Point(459, 106);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 15);
			this.label3.TabIndex = 9;
			this.label3.Text = "Tháng tính lương";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbSearch
			// 
			this.tbSearch.Location = new System.Drawing.Point(579, 21);
			this.tbSearch.Name = "tbSearch";
			this.tbSearch.Size = new System.Drawing.Size(176, 21);
			this.tbSearch.TabIndex = 1;
			// 
			// btnTim
			// 
			this.btnTim.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnTim.ForeColor = System.Drawing.Color.Blue;
			this.btnTim.Location = new System.Drawing.Point(579, 47);
			this.btnTim.Name = "btnTim";
			this.btnTim.Size = new System.Drawing.Size(83, 25);
			this.btnTim.TabIndex = 2;
			this.btnTim.Text = "Tìm kiếm";
			this.btnTim.UseVisualStyleBackColor = true;
			this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label1.Location = new System.Drawing.Point(486, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Nhập tên NV";
			// 
			// linkHienThiTatCaNV
			// 
			this.linkHienThiTatCaNV.AutoSize = true;
			this.linkHienThiTatCaNV.Location = new System.Drawing.Point(576, 78);
			this.linkHienThiTatCaNV.Name = "linkHienThiTatCaNV";
			this.linkHienThiTatCaNV.Size = new System.Drawing.Size(206, 15);
			this.linkHienThiTatCaNV.TabIndex = 33;
			this.linkHienThiTatCaNV.TabStop = true;
			this.linkHienThiTatCaNV.Text = "Hiển thị tất cả nhân viên trong phòng";
			this.linkHienThiTatCaNV.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHienThiTatCaNV_LinkClicked);
			// 
			// tbMucDongBH
			// 
			this.tbMucDongBH.Location = new System.Drawing.Point(579, 206);
			this.tbMucDongBH.Mask = "##.#";
			this.tbMucDongBH.Name = "tbMucDongBH";
			this.tbMucDongBH.Size = new System.Drawing.Size(83, 21);
			this.tbMucDongBH.TabIndex = 36;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(668, 209);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(18, 15);
			this.label11.TabIndex = 34;
			this.label11.Text = "%";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label2.Location = new System.Drawing.Point(481, 202);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 30);
			this.label2.TabIndex = 35;
			this.label2.Text = "Mức đóng \r\nBHXH, YT, TN";
			// 
			// frmThemDieuChinhLuong
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(809, 380);
			this.ControlBox = false;
			this.Controls.Add(this.tbMucDongBH);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.linkHienThiTatCaNV);
			this.Controls.Add(this.btnTim);
			this.Controls.Add(this.tbSearch);
			this.Controls.Add(this.numThuChiKhac);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.numTamUngThang);
			this.Controls.Add(this.numLuongDieuChinh);
			this.Controls.Add(this.dtpThang);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnThucHien);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.gp1);
			this.Controls.Add(this.gpChonPhongBan);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frmThemDieuChinhLuong";
			this.Text = "Thêm điều chỉnh lương mới";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.gpChonPhongBan.ResumeLayout(false);
			this.gp1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numThuChiKhac)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numTamUngThang)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numLuongDieuChinh)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox gpChonPhongBan;
		private System.Windows.Forms.TreeView treePhongBan;
		private System.Windows.Forms.GroupBox gp1;
		private System.Windows.Forms.NumericUpDown numThuChiKhac;
		private System.Windows.Forms.NumericUpDown numTamUngThang;
		private System.Windows.Forms.NumericUpDown numLuongDieuChinh;
		private System.Windows.Forms.DateTimePicker dtpThang;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.Button btnThucHien;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbSearch;
		private System.Windows.Forms.Button btnTim;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel linkHienThiTatCaNV;
		private System.Windows.Forms.DataGridView dgrdDSNVTrgPhg;
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
		private System.Windows.Forms.MaskedTextBox tbMucDongBH;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label2;
	}
}