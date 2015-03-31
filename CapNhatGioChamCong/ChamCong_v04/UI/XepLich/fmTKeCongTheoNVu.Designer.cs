namespace ChamCong_v04.UI.XepLich {
	partial class fmTKeCongTheoNVu {
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.radDate = new System.Windows.Forms.RadioButton();
			this.radMonth = new System.Windows.Forms.RadioButton();
			this.radQuarter = new System.Windows.Forms.RadioButton();
			this.dtpNgayBD = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.dtpNgayKT = new System.Windows.Forms.DateTimePicker();
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.dtpQuyNam = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.numQuy = new System.Windows.Forms.NumericUpDown();
			this.checkedListNhiemVu = new System.Windows.Forms.CheckedListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.dgrdThongKe = new System.Windows.Forms.DataGridView();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.listExcludeNV = new System.Windows.Forms.ListBox();
			this.btnRemoveExcludeNV = new System.Windows.Forms.Button();
			this.btnAddExcludeNV = new System.Windows.Forms.Button();
			this.btnXem = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.btnThoat = new System.Windows.Forms.Button();
			this.g4colMaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1tennv = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g2macc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.gpChonPhongBan.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numQuy)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdThongKe)).BeginInit();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// gpChonPhongBan
			// 
			this.gpChonPhongBan.Controls.Add(this.treePhongBan);
			this.gpChonPhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.gpChonPhongBan.Location = new System.Drawing.Point(2, 3);
			this.gpChonPhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gpChonPhongBan.Name = "gpChonPhongBan";
			this.gpChonPhongBan.Padding = new System.Windows.Forms.Padding(3, 6, 3, 2);
			this.gpChonPhongBan.Size = new System.Drawing.Size(263, 122);
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
			this.treePhongBan.Size = new System.Drawing.Size(257, 100);
			this.treePhongBan.TabIndex = 0;
			// 
			// radDate
			// 
			this.radDate.AutoSize = true;
			this.radDate.Checked = true;
			this.radDate.Location = new System.Drawing.Point(7, 20);
			this.radDate.Name = "radDate";
			this.radDate.Size = new System.Drawing.Size(81, 20);
			this.radDate.TabIndex = 34;
			this.radDate.TabStop = true;
			this.radDate.Text = "Từ ngày ";
			this.radDate.UseVisualStyleBackColor = true;
			this.radDate.CheckedChanged += new System.EventHandler(this.radDate_CheckedChanged);
			// 
			// radMonth
			// 
			this.radMonth.AutoSize = true;
			this.radMonth.Location = new System.Drawing.Point(6, 64);
			this.radMonth.Name = "radMonth";
			this.radMonth.Size = new System.Drawing.Size(64, 20);
			this.radMonth.TabIndex = 34;
			this.radMonth.Text = "Tháng";
			this.radMonth.UseVisualStyleBackColor = true;
			this.radMonth.CheckedChanged += new System.EventHandler(this.radMonth_CheckedChanged);
			// 
			// radQuarter
			// 
			this.radQuarter.AutoSize = true;
			this.radQuarter.Location = new System.Drawing.Point(6, 85);
			this.radQuarter.Name = "radQuarter";
			this.radQuarter.Size = new System.Drawing.Size(53, 20);
			this.radQuarter.TabIndex = 34;
			this.radQuarter.Text = "Quý";
			this.radQuarter.UseVisualStyleBackColor = true;
			this.radQuarter.CheckedChanged += new System.EventHandler(this.radQuarter_CheckedChanged);
			// 
			// dtpNgayBD
			// 
			this.dtpNgayBD.CustomFormat = "dddd dd/MM/yyyy";
			this.dtpNgayBD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgayBD.Location = new System.Drawing.Point(88, 20);
			this.dtpNgayBD.Name = "dtpNgayBD";
			this.dtpNgayBD.Size = new System.Drawing.Size(167, 21);
			this.dtpNgayBD.TabIndex = 35;
			this.dtpNgayBD.Value = new System.DateTime(2013, 8, 1, 0, 0, 0, 0);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Location = new System.Drawing.Point(22, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 16);
			this.label2.TabIndex = 36;
			this.label2.Text = "Đến ngày";
			// 
			// dtpNgayKT
			// 
			this.dtpNgayKT.CustomFormat = "dddd dd/MM/yyyy";
			this.dtpNgayKT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgayKT.Location = new System.Drawing.Point(88, 42);
			this.dtpNgayKT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dtpNgayKT.Name = "dtpNgayKT";
			this.dtpNgayKT.Size = new System.Drawing.Size(167, 21);
			this.dtpNgayKT.TabIndex = 37;
			this.dtpNgayKT.Value = new System.DateTime(2013, 8, 31, 0, 0, 0, 0);
			// 
			// dtpThang
			// 
			this.dtpThang.CustomFormat = "MM/yyyy";
			this.dtpThang.Enabled = false;
			this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpThang.Location = new System.Drawing.Point(88, 63);
			this.dtpThang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dtpThang.Name = "dtpThang";
			this.dtpThang.ShowUpDown = true;
			this.dtpThang.Size = new System.Drawing.Size(167, 21);
			this.dtpThang.TabIndex = 37;
			this.dtpThang.Value = new System.DateTime(2013, 8, 31, 0, 0, 0, 0);
			// 
			// dtpQuyNam
			// 
			this.dtpQuyNam.CustomFormat = "yyyy";
			this.dtpQuyNam.Enabled = false;
			this.dtpQuyNam.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpQuyNam.Location = new System.Drawing.Point(195, 85);
			this.dtpQuyNam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dtpQuyNam.Name = "dtpQuyNam";
			this.dtpQuyNam.ShowUpDown = true;
			this.dtpQuyNam.Size = new System.Drawing.Size(60, 21);
			this.dtpQuyNam.TabIndex = 37;
			this.dtpQuyNam.Value = new System.DateTime(2013, 8, 31, 0, 0, 0, 0);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label3.Location = new System.Drawing.Point(157, 87);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(33, 16);
			this.label3.TabIndex = 36;
			this.label3.Text = "năm";
			// 
			// numQuy
			// 
			this.numQuy.Enabled = false;
			this.numQuy.Location = new System.Drawing.Point(88, 85);
			this.numQuy.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.numQuy.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numQuy.Name = "numQuy";
			this.numQuy.Size = new System.Drawing.Size(49, 21);
			this.numQuy.TabIndex = 38;
			this.numQuy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// checkedListNhiemVu
			// 
			this.checkedListNhiemVu.CheckOnClick = true;
			this.checkedListNhiemVu.FormattingEnabled = true;
			this.checkedListNhiemVu.HorizontalScrollbar = true;
			this.checkedListNhiemVu.Items.AddRange(new object[] {
            "Máy Focke_Vận hành",
            "Máy Focke_Bảo trì",
            "Máy A_Hốt gói",
            "Máy A_Đổ sợi",
            "Máy B_Đóng thùng"});
			this.checkedListNhiemVu.Location = new System.Drawing.Point(3, 20);
			this.checkedListNhiemVu.Name = "checkedListNhiemVu";
			this.checkedListNhiemVu.ScrollAlwaysVisible = true;
			this.checkedListNhiemVu.Size = new System.Drawing.Size(260, 164);
			this.checkedListNhiemVu.TabIndex = 39;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radDate);
			this.groupBox1.Controls.Add(this.radMonth);
			this.groupBox1.Controls.Add(this.numQuy);
			this.groupBox1.Controls.Add(this.radQuarter);
			this.groupBox1.Controls.Add(this.dtpQuyNam);
			this.groupBox1.Controls.Add(this.dtpNgayBD);
			this.groupBox1.Controls.Add(this.dtpThang);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.dtpNgayKT);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(5, 128);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(260, 108);
			this.groupBox1.TabIndex = 40;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Chọn khoảng thời gian";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkedListNhiemVu);
			this.groupBox2.Location = new System.Drawing.Point(2, 238);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(263, 186);
			this.groupBox2.TabIndex = 41;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Chọn nhiệm vụ";
			// 
			// dgrdThongKe
			// 
			this.dgrdThongKe.AllowUserToAddRows = false;
			this.dgrdThongKe.AllowUserToDeleteRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.dgrdThongKe.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgrdThongKe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdThongKe.BackgroundColor = System.Drawing.Color.White;
			this.dgrdThongKe.ColumnHeadersHeight = 27;
			this.dgrdThongKe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgrdThongKe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.g4colMaNV,
            this.g1tennv,
            this.g1c4,
            this.g1c5,
            this.g1c9,
            this.g2macc});
			this.dgrdThongKe.Location = new System.Drawing.Point(3, 19);
			this.dgrdThongKe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dgrdThongKe.Name = "dgrdThongKe";
			this.dgrdThongKe.RowHeadersVisible = false;
			this.dgrdThongKe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdThongKe.Size = new System.Drawing.Size(446, 561);
			this.dgrdThongKe.TabIndex = 42;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.listExcludeNV);
			this.groupBox4.Controls.Add(this.btnRemoveExcludeNV);
			this.groupBox4.Controls.Add(this.btnAddExcludeNV);
			this.groupBox4.Location = new System.Drawing.Point(2, 430);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(263, 125);
			this.groupBox4.TabIndex = 43;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Ngoại trừ các nhân viên";
			// 
			// listExcludeNV
			// 
			this.listExcludeNV.FormattingEnabled = true;
			this.listExcludeNV.ItemHeight = 15;
			this.listExcludeNV.Items.AddRange(new object[] {
            "K222 Lê Hoàng Phúc",
            "K333 Nguyễn Khắc Điệp"});
			this.listExcludeNV.Location = new System.Drawing.Point(3, 40);
			this.listExcludeNV.Name = "listExcludeNV";
			this.listExcludeNV.Size = new System.Drawing.Size(260, 79);
			this.listExcludeNV.TabIndex = 0;
			// 
			// btnRemoveExcludeNV
			// 
			this.btnRemoveExcludeNV.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnRemoveExcludeNV.ForeColor = System.Drawing.Color.Blue;
			this.btnRemoveExcludeNV.Location = new System.Drawing.Point(198, 0);
			this.btnRemoveExcludeNV.Name = "btnRemoveExcludeNV";
			this.btnRemoveExcludeNV.Size = new System.Drawing.Size(34, 34);
			this.btnRemoveExcludeNV.TabIndex = 44;
			this.btnRemoveExcludeNV.Text = "-";
			this.btnRemoveExcludeNV.UseVisualStyleBackColor = true;
			this.btnRemoveExcludeNV.Click += new System.EventHandler(this.btnRemoveExcludeNV_Click);
			// 
			// btnAddExcludeNV
			// 
			this.btnAddExcludeNV.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnAddExcludeNV.ForeColor = System.Drawing.Color.Blue;
			this.btnAddExcludeNV.Location = new System.Drawing.Point(160, 0);
			this.btnAddExcludeNV.Name = "btnAddExcludeNV";
			this.btnAddExcludeNV.Size = new System.Drawing.Size(34, 34);
			this.btnAddExcludeNV.TabIndex = 44;
			this.btnAddExcludeNV.Text = "+";
			this.btnAddExcludeNV.UseVisualStyleBackColor = true;
			this.btnAddExcludeNV.Click += new System.EventHandler(this.btnAddExcludeNV_Click);
			// 
			// btnXem
			// 
			this.btnXem.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnXem.ForeColor = System.Drawing.Color.Blue;
			this.btnXem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnXem.Location = new System.Drawing.Point(2, 561);
			this.btnXem.Name = "btnXem";
			this.btnXem.Size = new System.Drawing.Size(107, 27);
			this.btnXem.TabIndex = 44;
			this.btnXem.Text = "Xem thống kê";
			this.btnXem.UseVisualStyleBackColor = true;
			this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.dgrdThongKe);
			this.groupBox3.Location = new System.Drawing.Point(268, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(449, 585);
			this.groupBox3.TabIndex = 41;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Danh sách thống kê công, phụ cấp, ngày phép nhân viên thực hiện nhiệm vụ";
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnThoat.Location = new System.Drawing.Point(180, 561);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(85, 27);
			this.btnThoat.TabIndex = 44;
			this.btnThoat.Text = "Đóng";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
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
			// g1tennv
			// 
			this.g1tennv.DataPropertyName = "UserFullName";
			this.g1tennv.HeaderText = "Tên NV";
			this.g1tennv.Name = "g1tennv";
			this.g1tennv.ReadOnly = true;
			this.g1tennv.Width = 150;
			// 
			// g1c4
			// 
			this.g1c4.DataPropertyName = "TongCong3";
			dataGridViewCellStyle2.Format = "0.##";
			this.g1c4.DefaultCellStyle = dataGridViewCellStyle2;
			this.g1c4.HeaderText = "Tổng Công";
			this.g1c4.Name = "g1c4";
			this.g1c4.Width = 80;
			// 
			// g1c5
			// 
			this.g1c5.DataPropertyName = "TongPC3";
			dataGridViewCellStyle3.Format = "0.##";
			this.g1c5.DefaultCellStyle = dataGridViewCellStyle3;
			this.g1c5.HeaderText = "Tổng PC";
			this.g1c5.Name = "g1c5";
			this.g1c5.Width = 65;
			// 
			// g1c9
			// 
			this.g1c9.DataPropertyName = "TongPhep3";
			dataGridViewCellStyle4.Format = "0.##";
			this.g1c9.DefaultCellStyle = dataGridViewCellStyle4;
			this.g1c9.HeaderText = "T.Phép";
			this.g1c9.Name = "g1c9";
			this.g1c9.Width = 55;
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
			// fmTKeCongTheoNVu
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(726, 592);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnXem);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.gpChonPhongBan);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "fmTKeCongTheoNVu";
			this.Text = "Thống kê công, phụ cấp, số ngày nghỉ phép";
			this.Load += new System.EventHandler(this.fmTKeCongTheoNVu_Load);
			this.gpChonPhongBan.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numQuy)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrdThongKe)).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gpChonPhongBan;
		private System.Windows.Forms.TreeView treePhongBan;
		private System.Windows.Forms.RadioButton radDate;
		private System.Windows.Forms.RadioButton radMonth;
		private System.Windows.Forms.RadioButton radQuarter;
		private System.Windows.Forms.DateTimePicker dtpNgayBD;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dtpNgayKT;
		private System.Windows.Forms.DateTimePicker dtpThang;
		private System.Windows.Forms.DateTimePicker dtpQuyNam;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numQuy;
		private System.Windows.Forms.CheckedListBox checkedListNhiemVu;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.DataGridView dgrdThongKe;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button btnXem;
		private System.Windows.Forms.ListBox listExcludeNV;
		private System.Windows.Forms.Button btnAddExcludeNV;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.Button btnRemoveExcludeNV;
		private System.Windows.Forms.DataGridViewTextBoxColumn g4colMaNV;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1tennv;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c4;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c5;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c9;
		private System.Windows.Forms.DataGridViewTextBoxColumn g2macc;
	}
}