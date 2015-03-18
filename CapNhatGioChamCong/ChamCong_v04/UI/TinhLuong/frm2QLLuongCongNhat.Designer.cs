namespace ChamCong_v04.UI.TinhLuong {
	partial class frm2QLLuongCongNhat {
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.btnCapNhat = new System.Windows.Forms.Button();
			this.dialogOpenExcel = new System.Windows.Forms.OpenFileDialog();
			this.btnThoat = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dgrdDSLuongCongnhat = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnTiepTuc = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tbCong = new System.Windows.Forms.MaskedTextBox();
			this.lbUserEnrollNumber = new System.Windows.Forms.Label();
			this.tbChucvu = new System.Windows.Forms.TextBox();
			this.tbMaNV = new System.Windows.Forms.TextBox();
			this.tbPhongBan = new System.Windows.Forms.TextBox();
			this.tbTenNV = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.numTamUng = new System.Windows.Forms.NumericUpDown();
			this.numDonGiaLuong = new System.Windows.Forms.NumericUpDown();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSLuongCongnhat)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numTamUng)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numDonGiaLuong)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCapNhat
			// 
			this.btnCapNhat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnCapNhat.ForeColor = System.Drawing.Color.Blue;
			this.btnCapNhat.Location = new System.Drawing.Point(101, 151);
			this.btnCapNhat.Name = "btnCapNhat";
			this.btnCapNhat.Size = new System.Drawing.Size(83, 25);
			this.btnCapNhat.TabIndex = 3;
			this.btnCapNhat.Text = "Cập nhật";
			this.btnCapNhat.UseVisualStyleBackColor = true;
			this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
			// 
			// dialogOpenExcel
			// 
			this.dialogOpenExcel.FileName = "openFileDialog1";
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(516, 333);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(83, 25);
			this.btnThoat.TabIndex = 1;
			this.btnThoat.Text = "Đóng";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.dgrdDSLuongCongnhat);
			this.groupBox1.Location = new System.Drawing.Point(1, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(598, 133);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Danh sách các nhân viên tính lương công nhật tháng {0}";
			// 
			// dgrdDSLuongCongnhat
			// 
			this.dgrdDSLuongCongnhat.AllowUserToAddRows = false;
			this.dgrdDSLuongCongnhat.AllowUserToDeleteRows = false;
			this.dgrdDSLuongCongnhat.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.dgrdDSLuongCongnhat.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgrdDSLuongCongnhat.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgrdDSLuongCongnhat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.g1c8,
            this.g1c9,
            this.g1c5,
            this.g1c6,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn12});
			this.dgrdDSLuongCongnhat.Location = new System.Drawing.Point(6, 19);
			this.dgrdDSLuongCongnhat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dgrdDSLuongCongnhat.Name = "dgrdDSLuongCongnhat";
			this.dgrdDSLuongCongnhat.RowHeadersVisible = false;
			this.dgrdDSLuongCongnhat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdDSLuongCongnhat.Size = new System.Drawing.Size(585, 109);
			this.dgrdDSLuongCongnhat.TabIndex = 0;
			this.dgrdDSLuongCongnhat.SelectionChanged += new System.EventHandler(this.dgrdDSLuongCongnhat_SelectionChanged);
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "UserFullName";
			this.dataGridViewTextBoxColumn3.HeaderText = "Tên NV";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.ToolTipText = "Tên Nhân viên";
			this.dataGridViewTextBoxColumn3.Width = 150;
			// 
			// g1c8
			// 
			this.g1c8.DataPropertyName = "NgayBatDau";
			dataGridViewCellStyle2.Format = "d/M/yyyy";
			this.g1c8.DefaultCellStyle = dataGridViewCellStyle2;
			this.g1c8.HeaderText = "Ngày bắt đầu";
			this.g1c8.Name = "g1c8";
			this.g1c8.ReadOnly = true;
			this.g1c8.ToolTipText = "Ngày bắt đầu tính lương công nhật";
			// 
			// g1c9
			// 
			this.g1c9.DataPropertyName = "NgayKetThuc";
			dataGridViewCellStyle3.Format = "d/M/yyyy";
			this.g1c9.DefaultCellStyle = dataGridViewCellStyle3;
			this.g1c9.HeaderText = "Ngày kết thúc";
			this.g1c9.Name = "g1c9";
			this.g1c9.ReadOnly = true;
			this.g1c9.ToolTipText = "Ngày kết thúc tính lương công nhật";
			// 
			// g1c5
			// 
			this.g1c5.DataPropertyName = "DonGiaLuong";
			dataGridViewCellStyle4.Format = "##,###,###,##0";
			this.g1c5.DefaultCellStyle = dataGridViewCellStyle4;
			this.g1c5.HeaderText = "Đơn giá lương";
			this.g1c5.Name = "g1c5";
			this.g1c5.ReadOnly = true;
			// 
			// g1c6
			// 
			this.g1c6.DataPropertyName = "TamUng";
			dataGridViewCellStyle5.Format = "##,###,###,##0";
			this.g1c6.DefaultCellStyle = dataGridViewCellStyle5;
			this.g1c6.HeaderText = "Tạm ứng";
			this.g1c6.Name = "g1c6";
			this.g1c6.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.DataPropertyName = "UserFullCode";
			this.dataGridViewTextBoxColumn4.HeaderText = "Mã NV";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.ToolTipText = "Mã Nhân viên";
			this.dataGridViewTextBoxColumn4.Width = 55;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.DataPropertyName = "ChucVu";
			this.dataGridViewTextBoxColumn5.HeaderText = "Chức vụ";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.ToolTipText = "Chức vụ";
			this.dataGridViewTextBoxColumn5.Width = 120;
			// 
			// dataGridViewTextBoxColumn10
			// 
			this.dataGridViewTextBoxColumn10.DataPropertyName = "TenPhong";
			this.dataGridViewTextBoxColumn10.HeaderText = "Phòng ban";
			this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			this.dataGridViewTextBoxColumn10.ReadOnly = true;
			this.dataGridViewTextBoxColumn10.ToolTipText = "Phòng ban";
			this.dataGridViewTextBoxColumn10.Width = 105;
			// 
			// dataGridViewTextBoxColumn12
			// 
			this.dataGridViewTextBoxColumn12.DataPropertyName = "IDPhong";
			this.dataGridViewTextBoxColumn12.HeaderText = "Mã Phòng ban";
			this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
			this.dataGridViewTextBoxColumn12.ReadOnly = true;
			this.dataGridViewTextBoxColumn12.Visible = false;
			// 
			// btnTiepTuc
			// 
			this.btnTiepTuc.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnTiepTuc.ForeColor = System.Drawing.Color.Blue;
			this.btnTiepTuc.Location = new System.Drawing.Point(427, 333);
			this.btnTiepTuc.Name = "btnTiepTuc";
			this.btnTiepTuc.Size = new System.Drawing.Size(83, 25);
			this.btnTiepTuc.TabIndex = 0;
			this.btnTiepTuc.Text = "Tiếp tục";
			this.btnTiepTuc.UseVisualStyleBackColor = true;
			this.btnTiepTuc.Click += new System.EventHandler(this.btnTiepTuc_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.tbCong);
			this.groupBox2.Controls.Add(this.lbUserEnrollNumber);
			this.groupBox2.Controls.Add(this.tbChucvu);
			this.groupBox2.Controls.Add(this.tbMaNV);
			this.groupBox2.Controls.Add(this.tbPhongBan);
			this.groupBox2.Controls.Add(this.tbTenNV);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.btnCapNhat);
			this.groupBox2.Controls.Add(this.numTamUng);
			this.groupBox2.Controls.Add(this.numDonGiaLuong);
			this.groupBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.groupBox2.Location = new System.Drawing.Point(1, 139);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox2.Size = new System.Drawing.Size(598, 189);
			this.groupBox2.TabIndex = 15;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Nhập thông tin";
			// 
			// tbCong
			// 
			this.tbCong.Enabled = false;
			this.tbCong.Location = new System.Drawing.Point(101, 70);
			this.tbCong.Mask = "#0.0#";
			this.tbCong.Name = "tbCong";
			this.tbCong.Size = new System.Drawing.Size(83, 21);
			this.tbCong.TabIndex = 0;
			this.tbCong.Text = "0000";
			// 
			// lbUserEnrollNumber
			// 
			this.lbUserEnrollNumber.AutoSize = true;
			this.lbUserEnrollNumber.Location = new System.Drawing.Point(413, 130);
			this.lbUserEnrollNumber.Name = "lbUserEnrollNumber";
			this.lbUserEnrollNumber.Size = new System.Drawing.Size(142, 15);
			this.lbUserEnrollNumber.TabIndex = 15;
			this.lbUserEnrollNumber.Text = "UserEnrollNumber_hide";
			this.lbUserEnrollNumber.Visible = false;
			// 
			// tbChucvu
			// 
			this.tbChucvu.Enabled = false;
			this.tbChucvu.Location = new System.Drawing.Point(384, 43);
			this.tbChucvu.Name = "tbChucvu";
			this.tbChucvu.Size = new System.Drawing.Size(192, 21);
			this.tbChucvu.TabIndex = 13;
			// 
			// tbMaNV
			// 
			this.tbMaNV.Enabled = false;
			this.tbMaNV.Location = new System.Drawing.Point(384, 19);
			this.tbMaNV.Name = "tbMaNV";
			this.tbMaNV.Size = new System.Drawing.Size(192, 21);
			this.tbMaNV.TabIndex = 13;
			// 
			// tbPhongBan
			// 
			this.tbPhongBan.Enabled = false;
			this.tbPhongBan.Location = new System.Drawing.Point(101, 43);
			this.tbPhongBan.Name = "tbPhongBan";
			this.tbPhongBan.Size = new System.Drawing.Size(192, 21);
			this.tbPhongBan.TabIndex = 13;
			// 
			// tbTenNV
			// 
			this.tbTenNV.Enabled = false;
			this.tbTenNV.Location = new System.Drawing.Point(101, 19);
			this.tbTenNV.Name = "tbTenNV";
			this.tbTenNV.Size = new System.Drawing.Size(192, 21);
			this.tbTenNV.TabIndex = 13;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label7.Location = new System.Drawing.Point(299, 126);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(35, 15);
			this.label7.TabIndex = 0;
			this.label7.Text = "đồng";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label2.Location = new System.Drawing.Point(299, 99);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 15);
			this.label2.TabIndex = 0;
			this.label2.Text = "đồng / ngày";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label9.Location = new System.Drawing.Point(299, 46);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(52, 15);
			this.label9.TabIndex = 0;
			this.label9.Text = "Chức vụ";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label3.Location = new System.Drawing.Point(299, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "Mã nhân viên";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label5.Location = new System.Drawing.Point(5, 126);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(57, 15);
			this.label5.TabIndex = 0;
			this.label5.Text = "Tạm ứng";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label4.Location = new System.Drawing.Point(5, 99);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(87, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "Đơn giá lương";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label8.Location = new System.Drawing.Point(5, 72);
			this.label8.Margin = new System.Windows.Forms.Padding(3);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(85, 15);
			this.label8.TabIndex = 0;
			this.label8.Text = "Công làm việc";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label6.Location = new System.Drawing.Point(5, 46);
			this.label6.Margin = new System.Windows.Forms.Padding(3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(67, 15);
			this.label6.TabIndex = 0;
			this.label6.Text = "Phòng ban";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label1.Location = new System.Drawing.Point(5, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Nhân viên";
			// 
			// numTamUng
			// 
			this.numTamUng.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numTamUng.Location = new System.Drawing.Point(101, 124);
			this.numTamUng.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.numTamUng.Name = "numTamUng";
			this.numTamUng.Size = new System.Drawing.Size(192, 21);
			this.numTamUng.TabIndex = 2;
			this.numTamUng.ThousandsSeparator = true;
			// 
			// numDonGiaLuong
			// 
			this.numDonGiaLuong.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numDonGiaLuong.Location = new System.Drawing.Point(101, 97);
			this.numDonGiaLuong.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.numDonGiaLuong.Name = "numDonGiaLuong";
			this.numDonGiaLuong.Size = new System.Drawing.Size(192, 21);
			this.numDonGiaLuong.TabIndex = 1;
			this.numDonGiaLuong.ThousandsSeparator = true;
			// 
			// frm2QLLuongCongNhat
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(611, 370);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnTiepTuc);
			this.Controls.Add(this.btnThoat);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "frm2QLLuongCongNhat";
			this.Text = "Nhập thông tin làm việc công nhật";
			this.Load += new System.EventHandler(this.frmQLLuongCongNhat_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSLuongCongnhat)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numTamUng)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numDonGiaLuong)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Button btnCapNhat;
		private System.Windows.Forms.OpenFileDialog dialogOpenExcel;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnTiepTuc;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbMaNV;
		private System.Windows.Forms.TextBox tbPhongBan;
		private System.Windows.Forms.TextBox tbTenNV;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown numTamUng;
		private System.Windows.Forms.NumericUpDown numDonGiaLuong;
		private System.Windows.Forms.DataGridView dgrdDSLuongCongnhat;
		private System.Windows.Forms.TextBox tbChucvu;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lbUserEnrollNumber;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c8;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c9;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c5;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c6;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
		private System.Windows.Forms.MaskedTextBox tbCong;
		private System.Windows.Forms.Label label8;
	}
}