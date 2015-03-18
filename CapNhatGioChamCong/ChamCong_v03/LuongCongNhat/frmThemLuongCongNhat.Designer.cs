namespace ChamCong_v03.LuongCongNhat {
	partial class frmThemLuongCongNhat {
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
			this.numDonGiaLuong = new System.Windows.Forms.NumericUpDown();
			this.numTamUng = new System.Windows.Forms.NumericUpDown();
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.btnThoat = new System.Windows.Forms.Button();
			this.btnThucHien = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tbTen = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tbChucVu = new System.Windows.Forms.TextBox();
			this.tbThanhTien = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.tbThucLanh = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.tbNgayCong = new System.Windows.Forms.MaskedTextBox();
			this.lbID = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numDonGiaLuong)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numTamUng)).BeginInit();
			this.SuspendLayout();
			// 
			// numDonGiaLuong
			// 
			this.numDonGiaLuong.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numDonGiaLuong.Location = new System.Drawing.Point(132, 91);
			this.numDonGiaLuong.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.numDonGiaLuong.Name = "numDonGiaLuong";
			this.numDonGiaLuong.Size = new System.Drawing.Size(179, 21);
			this.numDonGiaLuong.TabIndex = 3;
			this.numDonGiaLuong.ThousandsSeparator = true;
			this.numDonGiaLuong.ValueChanged += new System.EventHandler(this.numTamUng_ValueChanged);
			// 
			// numTamUng
			// 
			this.numTamUng.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numTamUng.Location = new System.Drawing.Point(132, 170);
			this.numTamUng.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.numTamUng.Name = "numTamUng";
			this.numTamUng.Size = new System.Drawing.Size(179, 21);
			this.numTamUng.TabIndex = 6;
			this.numTamUng.ThousandsSeparator = true;
			this.numTamUng.ValueChanged += new System.EventHandler(this.numTamUng_ValueChanged);
			// 
			// dtpThang
			// 
			this.dtpThang.CustomFormat = "M / yyyy";
			this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpThang.Location = new System.Drawing.Point(132, 11);
			this.dtpThang.Name = "dtpThang";
			this.dtpThang.ShowUpDown = true;
			this.dtpThang.Size = new System.Drawing.Size(83, 21);
			this.dtpThang.TabIndex = 0;
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(228, 222);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(83, 25);
			this.btnThoat.TabIndex = 9;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnThucHien
			// 
			this.btnThucHien.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThucHien.ForeColor = System.Drawing.Color.Blue;
			this.btnThucHien.Location = new System.Drawing.Point(131, 222);
			this.btnThucHien.Name = "btnThucHien";
			this.btnThucHien.Size = new System.Drawing.Size(83, 25);
			this.btnThucHien.TabIndex = 8;
			this.btnThucHien.Text = "Thực hiện";
			this.btnThucHien.UseVisualStyleBackColor = true;
			this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label4.Location = new System.Drawing.Point(62, 172);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(58, 15);
			this.label4.TabIndex = 8;
			this.label4.Text = "Tạm ứng";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label3.Location = new System.Drawing.Point(19, 13);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 15);
			this.label3.TabIndex = 9;
			this.label3.Text = "Tháng tính lương";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbTen
			// 
			this.tbTen.Location = new System.Drawing.Point(132, 38);
			this.tbTen.Name = "tbTen";
			this.tbTen.Size = new System.Drawing.Size(179, 21);
			this.tbTen.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label1.Location = new System.Drawing.Point(63, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 15);
			this.label1.TabIndex = 8;
			this.label1.Text = "Họ và tên";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbChucVu
			// 
			this.tbChucVu.Location = new System.Drawing.Point(132, 65);
			this.tbChucVu.Name = "tbChucVu";
			this.tbChucVu.Size = new System.Drawing.Size(179, 21);
			this.tbChucVu.TabIndex = 2;
			// 
			// tbThanhTien
			// 
			this.tbThanhTien.Enabled = false;
			this.tbThanhTien.Location = new System.Drawing.Point(132, 143);
			this.tbThanhTien.Name = "tbThanhTien";
			this.tbThanhTien.Size = new System.Drawing.Size(179, 21);
			this.tbThanhTien.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label2.Location = new System.Drawing.Point(68, 68);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 15);
			this.label2.TabIndex = 8;
			this.label2.Text = "Chức vụ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label5.Location = new System.Drawing.Point(32, 93);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(90, 15);
			this.label5.TabIndex = 8;
			this.label5.Text = "Đơn giá lương";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label8.Location = new System.Drawing.Point(39, 119);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(83, 15);
			this.label8.TabIndex = 8;
			this.label8.Text = "Số ngày công";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label9.Location = new System.Drawing.Point(55, 146);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(66, 15);
			this.label9.TabIndex = 8;
			this.label9.Text = "Thành tiền";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbThucLanh
			// 
			this.tbThucLanh.Enabled = false;
			this.tbThucLanh.Location = new System.Drawing.Point(132, 196);
			this.tbThucLanh.Name = "tbThucLanh";
			this.tbThucLanh.Size = new System.Drawing.Size(179, 21);
			this.tbThucLanh.TabIndex = 7;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label10.Location = new System.Drawing.Point(56, 199);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(64, 15);
			this.label10.TabIndex = 6;
			this.label10.Text = "Thực lãnh";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbNgayCong
			// 
			this.tbNgayCong.Location = new System.Drawing.Point(132, 117);
			this.tbNgayCong.Mask = "##.#";
			this.tbNgayCong.Name = "tbNgayCong";
			this.tbNgayCong.Size = new System.Drawing.Size(82, 21);
			this.tbNgayCong.TabIndex = 4;
			this.tbNgayCong.Text = "000";
			this.tbNgayCong.TextChanged += new System.EventHandler(this.maskedTextBox1_TextChanged);
			// 
			// lbID
			// 
			this.lbID.AutoSize = true;
			this.lbID.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lbID.Location = new System.Drawing.Point(221, 13);
			this.lbID.Name = "lbID";
			this.lbID.Size = new System.Drawing.Size(18, 15);
			this.lbID.TabIndex = 9;
			this.lbID.Text = "ID";
			this.lbID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbID.Visible = false;
			// 
			// frmThemLuongCongNhat
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(323, 261);
			this.ControlBox = false;
			this.Controls.Add(this.tbNgayCong);
			this.Controls.Add(this.tbThucLanh);
			this.Controls.Add(this.tbThanhTien);
			this.Controls.Add(this.tbChucVu);
			this.Controls.Add(this.tbTen);
			this.Controls.Add(this.numDonGiaLuong);
			this.Controls.Add(this.numTamUng);
			this.Controls.Add(this.dtpThang);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnThucHien);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lbID);
			this.Controls.Add(this.label3);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frmThemLuongCongNhat";
			this.Text = "Nhập khoản lương công nhật";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.numDonGiaLuong)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numTamUng)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown numDonGiaLuong;
		private System.Windows.Forms.NumericUpDown numTamUng;
		private System.Windows.Forms.DateTimePicker dtpThang;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.Button btnThucHien;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbTen;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbChucVu;
		private System.Windows.Forms.TextBox tbThanhTien;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox tbThucLanh;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.MaskedTextBox tbNgayCong;
		private System.Windows.Forms.Label lbID;
	}
}