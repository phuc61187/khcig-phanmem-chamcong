namespace ChamCong_v04.UI.XepLich {
	partial class Form2 {
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.dtpNgayBD = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.dtpNgayKT = new System.Windows.Forms.DateTimePicker();
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.dtpQuyNam = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.numQuy = new System.Windows.Forms.NumericUpDown();
			this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.dgrdThongKe = new System.Windows.Forms.DataGridView();
			this.g4colMaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1tennv = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g2macc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.btnXNCa = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.button2 = new System.Windows.Forms.Button();
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
			this.treePhongBan.Size = new System.Drawing.Size(257, 100);
			this.treePhongBan.TabIndex = 0;
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Location = new System.Drawing.Point(7, 20);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(72, 19);
			this.radioButton1.TabIndex = 34;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "Từ ngày ";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(6, 64);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(60, 19);
			this.radioButton2.TabIndex = 34;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "Tháng";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.Location = new System.Drawing.Point(6, 85);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(46, 19);
			this.radioButton3.TabIndex = 34;
			this.radioButton3.TabStop = true;
			this.radioButton3.Text = "Quý";
			this.radioButton3.UseVisualStyleBackColor = true;
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
			this.label2.Size = new System.Drawing.Size(59, 15);
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
			this.label3.Size = new System.Drawing.Size(32, 15);
			this.label3.TabIndex = 36;
			this.label3.Text = "năm";
			// 
			// numQuy
			// 
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
			// checkedListBox1
			// 
			this.checkedListBox1.CheckOnClick = true;
			this.checkedListBox1.FormattingEnabled = true;
			this.checkedListBox1.HorizontalScrollbar = true;
			this.checkedListBox1.Items.AddRange(new object[] {
            "Máy Focke_Vận hành",
            "Máy Focke_Bảo trì",
            "Máy A_Hốt gói",
            "Máy A_Đổ sợi",
            "Máy B_Đóng thùng"});
			this.checkedListBox1.Location = new System.Drawing.Point(3, 20);
			this.checkedListBox1.Name = "checkedListBox1";
			this.checkedListBox1.ScrollAlwaysVisible = true;
			this.checkedListBox1.Size = new System.Drawing.Size(260, 164);
			this.checkedListBox1.TabIndex = 39;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.numQuy);
			this.groupBox1.Controls.Add(this.radioButton3);
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
			this.groupBox2.Controls.Add(this.checkedListBox1);
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
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.dgrdThongKe.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
			this.dgrdThongKe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdThongKe.BackgroundColor = System.Drawing.Color.White;
			this.dgrdThongKe.ColumnHeadersHeight = 27;
			this.dgrdThongKe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgrdThongKe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.g4colMaNV,
            this.g1tennv,
            this.g1c6,
            this.g1c4,
            this.g1c5,
            this.g1c9,
            this.g2macc});
			this.dgrdThongKe.Location = new System.Drawing.Point(3, 19);
			this.dgrdThongKe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dgrdThongKe.Name = "dgrdThongKe";
			this.dgrdThongKe.RowHeadersVisible = false;
			this.dgrdThongKe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdThongKe.Size = new System.Drawing.Size(744, 588);
			this.dgrdThongKe.TabIndex = 42;
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
			// g1c6
			// 
			this.g1c6.HeaderText = "Nhiệm vụ";
			this.g1c6.Name = "g1c6";
			this.g1c6.Width = 250;
			// 
			// g1c4
			// 
			this.g1c4.HeaderText = "Tổng Công";
			this.g1c4.Name = "g1c4";
			this.g1c4.Width = 80;
			// 
			// g1c5
			// 
			this.g1c5.HeaderText = "Tổng PC";
			this.g1c5.Name = "g1c5";
			this.g1c5.Width = 65;
			// 
			// g1c9
			// 
			this.g1c9.HeaderText = "T. Phép";
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
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.listBox1);
			this.groupBox4.Controls.Add(this.button1);
			this.groupBox4.Location = new System.Drawing.Point(2, 430);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(263, 152);
			this.groupBox4.TabIndex = 43;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Ngoại trừ các nhân viên";
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 15;
			this.listBox1.Items.AddRange(new object[] {
            "K222 Lê Hoàng Phúc",
            "K333 Nguyễn Khắc Điệp"});
			this.listBox1.Location = new System.Drawing.Point(3, 47);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(260, 94);
			this.listBox1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.button1.ForeColor = System.Drawing.Color.Blue;
			this.button1.Location = new System.Drawing.Point(230, 7);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(24, 34);
			this.button1.TabIndex = 44;
			this.button1.Text = "+";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// btnXNCa
			// 
			this.btnXNCa.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnXNCa.ForeColor = System.Drawing.Color.Blue;
			this.btnXNCa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnXNCa.Location = new System.Drawing.Point(2, 588);
			this.btnXNCa.Name = "btnXNCa";
			this.btnXNCa.Size = new System.Drawing.Size(94, 27);
			this.btnXNCa.TabIndex = 44;
			this.btnXNCa.Text = "Xem thống kê";
			this.btnXNCa.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.dgrdThongKe);
			this.groupBox3.Location = new System.Drawing.Point(268, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(747, 612);
			this.groupBox3.TabIndex = 41;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Danh sách nhân viên thực hiện nhiệm vụ chính";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.button2.ForeColor = System.Drawing.Color.Blue;
			this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button2.Location = new System.Drawing.Point(177, 588);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(85, 27);
			this.button2.TabIndex = 44;
			this.button2.Text = "Đóng";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// Form2
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1018, 621);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.btnXNCa);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.gpChonPhongBan);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "Form2";
			this.Text = "Form2";
			this.Load += new System.EventHandler(this.Form2_Load);
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
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.DateTimePicker dtpNgayBD;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dtpNgayKT;
		private System.Windows.Forms.DateTimePicker dtpThang;
		private System.Windows.Forms.DateTimePicker dtpQuyNam;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numQuy;
		private System.Windows.Forms.CheckedListBox checkedListBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.DataGridView dgrdThongKe;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button btnXNCa;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.DataGridViewTextBoxColumn g4colMaNV;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1tennv;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c6;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c4;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c5;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c9;
		private System.Windows.Forms.DataGridViewTextBoxColumn g2macc;
	}
}