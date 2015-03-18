namespace ChamCong_v03.DieuChinhLuong {
	partial class frm_CapNhat {
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
			this.tbMucDongBH = new System.Windows.Forms.MaskedTextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numThuChiKhac)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numTamUngThang)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numLuongDieuChinh)).BeginInit();
			this.SuspendLayout();
			// 
			// numThuChiKhac
			// 
			this.numThuChiKhac.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numThuChiKhac.Location = new System.Drawing.Point(129, 90);
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
			this.numThuChiKhac.TabIndex = 3;
			this.numThuChiKhac.ThousandsSeparator = true;
			// 
			// numTamUngThang
			// 
			this.numTamUngThang.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numTamUngThang.Location = new System.Drawing.Point(129, 38);
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
			this.numTamUngThang.TabIndex = 1;
			this.numTamUngThang.ThousandsSeparator = true;
			// 
			// numLuongDieuChinh
			// 
			this.numLuongDieuChinh.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numLuongDieuChinh.Location = new System.Drawing.Point(129, 64);
			this.numLuongDieuChinh.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.numLuongDieuChinh.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147483648});
			this.numLuongDieuChinh.Name = "numLuongDieuChinh";
			this.numLuongDieuChinh.Size = new System.Drawing.Size(179, 21);
			this.numLuongDieuChinh.TabIndex = 2;
			this.numLuongDieuChinh.ThousandsSeparator = true;
			// 
			// dtpThang
			// 
			this.dtpThang.CustomFormat = "M / yyyy";
			this.dtpThang.Enabled = false;
			this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpThang.Location = new System.Drawing.Point(129, 11);
			this.dtpThang.Name = "dtpThang";
			this.dtpThang.ShowUpDown = true;
			this.dtpThang.Size = new System.Drawing.Size(83, 21);
			this.dtpThang.TabIndex = 0;
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(225, 146);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(83, 25);
			this.btnThoat.TabIndex = 5;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnThucHien
			// 
			this.btnThucHien.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThucHien.ForeColor = System.Drawing.Color.Blue;
			this.btnThucHien.Location = new System.Drawing.Point(128, 146);
			this.btnThucHien.Name = "btnThucHien";
			this.btnThucHien.Size = new System.Drawing.Size(83, 25);
			this.btnThucHien.TabIndex = 4;
			this.btnThucHien.Text = "Thực hiện";
			this.btnThucHien.UseVisualStyleBackColor = true;
			this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label7.Location = new System.Drawing.Point(34, 92);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(79, 15);
			this.label7.TabIndex = 20;
			this.label7.Text = "Thu chi khác";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label6.Location = new System.Drawing.Point(6, 66);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(107, 15);
			this.label6.TabIndex = 21;
			this.label6.Text = "Lương điều chỉnh";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label4.Location = new System.Drawing.Point(18, 39);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 15);
			this.label4.TabIndex = 22;
			this.label4.Text = "Tạm ứng tháng";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label3.Location = new System.Drawing.Point(9, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 15);
			this.label3.TabIndex = 23;
			this.label3.Text = "Tháng tính lương";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbMucDongBH
			// 
			this.tbMucDongBH.Location = new System.Drawing.Point(129, 116);
			this.tbMucDongBH.Mask = "##.#";
			this.tbMucDongBH.Name = "tbMucDongBH";
			this.tbMucDongBH.Size = new System.Drawing.Size(83, 21);
			this.tbMucDongBH.TabIndex = 39;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(218, 119);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(18, 15);
			this.label11.TabIndex = 37;
			this.label11.Text = "%";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label2.Location = new System.Drawing.Point(31, 112);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 30);
			this.label2.TabIndex = 38;
			this.label2.Text = "Mức đóng \r\nBHXH, YT, TN";
			// 
			// frm_CapNhat
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(326, 183);
			this.ControlBox = false;
			this.Controls.Add(this.tbMucDongBH);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numThuChiKhac);
			this.Controls.Add(this.numTamUngThang);
			this.Controls.Add(this.numLuongDieuChinh);
			this.Controls.Add(this.dtpThang);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnThucHien);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_CapNhat";
			this.Text = "Cập nhật điều chỉnh lương";
			this.Load += new System.EventHandler(this.Form3_Load);
			((System.ComponentModel.ISupportInitialize)(this.numThuChiKhac)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numTamUngThang)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numLuongDieuChinh)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

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
		private System.Windows.Forms.MaskedTextBox tbMucDongBH;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label2;
	}
}