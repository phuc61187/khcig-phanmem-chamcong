namespace ChamCong_v02 {
	partial class frm_41_TinhLuongNV {
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
			this.label1 = new System.Windows.Forms.Label();
			this.btnTinhLuong = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.numSanLuong = new System.Windows.Forms.NumericUpDown();
			this.numDonGia = new System.Windows.Forms.NumericUpDown();
			this.numLuongTT = new System.Windows.Forms.NumericUpDown();
			this.numLuongCongNhat = new System.Windows.Forms.NumericUpDown();
			this.numBoiDuongCa3 = new System.Windows.Forms.NumericUpDown();
			this.btnDieuChinhLuong = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.btnThayDoiHSL = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.tbMucDongBH = new System.Windows.Forms.MaskedTextBox();
			((System.ComponentModel.ISupportInitialize)(this.numSanLuong)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numDonGia)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numLuongTT)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numLuongCongNhat)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numBoiDuongCa3)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label1.Location = new System.Drawing.Point(12, 75);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Sản lượng";
			// 
			// btnTinhLuong
			// 
			this.btnTinhLuong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnTinhLuong.ForeColor = System.Drawing.Color.Blue;
			this.btnTinhLuong.Location = new System.Drawing.Point(15, 245);
			this.btnTinhLuong.Name = "btnTinhLuong";
			this.btnTinhLuong.Size = new System.Drawing.Size(305, 27);
			this.btnTinhLuong.TabIndex = 7;
			this.btnTinhLuong.Text = "Tính lương và xuất báo biểu";
			this.btnTinhLuong.UseVisualStyleBackColor = true;
			this.btnTinhLuong.Click += new System.EventHandler(this.btnTinhLuongVaXuatBB_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label2.Location = new System.Drawing.Point(12, 103);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "Đơn giá";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(281, 75);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(25, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "gói";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(281, 103);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(36, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "đồng";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label7.Location = new System.Drawing.Point(12, 159);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73, 16);
			this.label7.TabIndex = 0;
			this.label7.Text = "Công nhật";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(281, 159);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(36, 16);
			this.label8.TabIndex = 0;
			this.label8.Text = "đồng";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label9.Location = new System.Drawing.Point(12, 187);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(105, 16);
			this.label9.TabIndex = 0;
			this.label9.Text = "Bồi dưỡng ca 3";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(281, 187);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(36, 16);
			this.label10.TabIndex = 0;
			this.label10.Text = "đồng";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label13.Location = new System.Drawing.Point(12, 131);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(106, 16);
			this.label13.TabIndex = 0;
			this.label13.Text = "Lương tối thiểu";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(281, 131);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(36, 16);
			this.label14.TabIndex = 0;
			this.label14.Text = "đồng";
			// 
			// dtpThang
			// 
			this.dtpThang.CustomFormat = "M / yyyy";
			this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpThang.Location = new System.Drawing.Point(175, 16);
			this.dtpThang.Name = "dtpThang";
			this.dtpThang.ShowUpDown = true;
			this.dtpThang.Size = new System.Drawing.Size(100, 22);
			this.dtpThang.TabIndex = 0;
			this.dtpThang.Value = new System.DateTime(2013, 7, 26, 7, 55, 0, 0);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label5.Location = new System.Drawing.Point(12, 21);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 16);
			this.label5.TabIndex = 0;
			this.label5.Text = "Tháng";
			// 
			// numSanLuong
			// 
			this.numSanLuong.Location = new System.Drawing.Point(175, 72);
			this.numSanLuong.Maximum = new decimal(new int[] {
            20000000,
            0,
            0,
            0});
			this.numSanLuong.Name = "numSanLuong";
			this.numSanLuong.Size = new System.Drawing.Size(100, 22);
			this.numSanLuong.TabIndex = 1;
			this.numSanLuong.ThousandsSeparator = true;
			this.numSanLuong.Value = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			// 
			// numDonGia
			// 
			this.numDonGia.Location = new System.Drawing.Point(175, 100);
			this.numDonGia.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.numDonGia.Name = "numDonGia";
			this.numDonGia.Size = new System.Drawing.Size(100, 22);
			this.numDonGia.TabIndex = 2;
			this.numDonGia.ThousandsSeparator = true;
			this.numDonGia.Value = new decimal(new int[] {
            220,
            0,
            0,
            0});
			// 
			// numLuongTT
			// 
			this.numLuongTT.Location = new System.Drawing.Point(175, 128);
			this.numLuongTT.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.numLuongTT.Name = "numLuongTT";
			this.numLuongTT.Size = new System.Drawing.Size(100, 22);
			this.numLuongTT.TabIndex = 3;
			this.numLuongTT.ThousandsSeparator = true;
			this.numLuongTT.Value = new decimal(new int[] {
            1150000,
            0,
            0,
            0});
			// 
			// numLuongCongNhat
			// 
			this.numLuongCongNhat.Location = new System.Drawing.Point(175, 156);
			this.numLuongCongNhat.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
			this.numLuongCongNhat.Name = "numLuongCongNhat";
			this.numLuongCongNhat.Size = new System.Drawing.Size(100, 22);
			this.numLuongCongNhat.TabIndex = 4;
			this.numLuongCongNhat.ThousandsSeparator = true;
			// 
			// numBoiDuongCa3
			// 
			this.numBoiDuongCa3.Location = new System.Drawing.Point(175, 184);
			this.numBoiDuongCa3.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numBoiDuongCa3.Name = "numBoiDuongCa3";
			this.numBoiDuongCa3.Size = new System.Drawing.Size(100, 22);
			this.numBoiDuongCa3.TabIndex = 5;
			this.numBoiDuongCa3.ThousandsSeparator = true;
			this.numBoiDuongCa3.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
			// 
			// btnDieuChinhLuong
			// 
			this.btnDieuChinhLuong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnDieuChinhLuong.ForeColor = System.Drawing.Color.Blue;
			this.btnDieuChinhLuong.Location = new System.Drawing.Point(15, 212);
			this.btnDieuChinhLuong.Name = "btnDieuChinhLuong";
			this.btnDieuChinhLuong.Size = new System.Drawing.Size(305, 27);
			this.btnDieuChinhLuong.TabIndex = 6;
			this.btnDieuChinhLuong.Text = "Điều chỉnh lương tháng trước cho NV";
			this.btnDieuChinhLuong.UseVisualStyleBackColor = true;
			this.btnDieuChinhLuong.Click += new System.EventHandler(this.btnDieuChinhLuong_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Title = "Chọn đường dẫn lưu bảng lương";
			// 
			// btnThayDoiHSL
			// 
			this.btnThayDoiHSL.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnThayDoiHSL.ForeColor = System.Drawing.Color.Blue;
			this.btnThayDoiHSL.Location = new System.Drawing.Point(15, 278);
			this.btnThayDoiHSL.Name = "btnThayDoiHSL";
			this.btnThayDoiHSL.Size = new System.Drawing.Size(305, 27);
			this.btnThayDoiHSL.TabIndex = 7;
			this.btnThayDoiHSL.Text = "Thay đổi hệ số lương, phụ cấp";
			this.btnThayDoiHSL.UseVisualStyleBackColor = true;
			this.btnThayDoiHSL.Click += new System.EventHandler(this.btnThayDoiHSL_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label6.Location = new System.Drawing.Point(12, 47);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(159, 16);
			this.label6.TabIndex = 0;
			this.label6.Text = "Mức đóng BHXH, YT, TN";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(281, 47);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(20, 16);
			this.label11.TabIndex = 0;
			this.label11.Text = "%";
			// 
			// tbMucDongBH
			// 
			this.tbMucDongBH.Location = new System.Drawing.Point(175, 44);
			this.tbMucDongBH.Mask = "##.#";
			this.tbMucDongBH.Name = "tbMucDongBH";
			this.tbMucDongBH.Size = new System.Drawing.Size(100, 22);
			this.tbMucDongBH.TabIndex = 8;
			this.tbMucDongBH.Text = "105";
			// 
			// frm_41_TinhLuongNV
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(330, 356);
			this.Controls.Add(this.tbMucDongBH);
			this.Controls.Add(this.numBoiDuongCa3);
			this.Controls.Add(this.numLuongCongNhat);
			this.Controls.Add(this.numLuongTT);
			this.Controls.Add(this.numDonGia);
			this.Controls.Add(this.numSanLuong);
			this.Controls.Add(this.dtpThang);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnDieuChinhLuong);
			this.Controls.Add(this.btnThayDoiHSL);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.btnTinhLuong);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_41_TinhLuongNV";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Tính lương cho NV";
			((System.ComponentModel.ISupportInitialize)(this.numSanLuong)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numDonGia)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numLuongTT)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numLuongCongNhat)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numBoiDuongCa3)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTinhLuong;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
		private System.Windows.Forms.DateTimePicker dtpThang;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numSanLuong;
        private System.Windows.Forms.NumericUpDown numDonGia;
        private System.Windows.Forms.NumericUpDown numLuongTT;
        private System.Windows.Forms.NumericUpDown numLuongCongNhat;
        private System.Windows.Forms.NumericUpDown numBoiDuongCa3;
		private System.Windows.Forms.Button btnDieuChinhLuong;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Button btnThayDoiHSL;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.MaskedTextBox tbMucDongBH;
	}
}