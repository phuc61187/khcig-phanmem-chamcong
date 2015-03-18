using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DTO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ChamCong_v03 {
	partial class frm_41_TinhLuongNV {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

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
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.btnLuongCongNhat = new System.Windows.Forms.Button();
			this.btnKetCong = new System.Windows.Forms.Button();
			this.btnMoKetCong = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.label12 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.numChiTienGiaCong = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numSanLuong)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numDonGia)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numLuongTT)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numLuongCongNhat)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numBoiDuongCa3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numChiTienGiaCong)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label1.Location = new System.Drawing.Point(12, 47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Sản lượng";
			// 
			// btnTinhLuong
			// 
			this.btnTinhLuong.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnTinhLuong.ForeColor = System.Drawing.Color.Blue;
			this.btnTinhLuong.Location = new System.Drawing.Point(15, 278);
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
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label2.Location = new System.Drawing.Point(12, 75);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 15);
			this.label2.TabIndex = 0;
			this.label2.Text = "Đơn giá";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(281, 47);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "gói";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(281, 75);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "đồng";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label7.Location = new System.Drawing.Point(12, 187);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 15);
			this.label7.TabIndex = 0;
			this.label7.Text = "Công nhật";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(281, 187);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(35, 15);
			this.label8.TabIndex = 0;
			this.label8.Text = "đồng";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label9.Location = new System.Drawing.Point(12, 159);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(94, 15);
			this.label9.TabIndex = 0;
			this.label9.Text = "Bồi dưỡng ca 3";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(281, 159);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(35, 15);
			this.label10.TabIndex = 0;
			this.label10.Text = "đồng";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label13.Location = new System.Drawing.Point(12, 131);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(94, 15);
			this.label13.TabIndex = 0;
			this.label13.Text = "Lương tối thiểu";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(281, 131);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(35, 15);
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
			this.dtpThang.Size = new System.Drawing.Size(100, 21);
			this.dtpThang.TabIndex = 0;
			this.dtpThang.Value = new System.DateTime(2013, 7, 26, 7, 55, 0, 0);
			this.dtpThang.ValueChanged += new System.EventHandler(this.dtpThang_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label5.Location = new System.Drawing.Point(12, 21);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(42, 15);
			this.label5.TabIndex = 0;
			this.label5.Text = "Tháng";
			// 
			// numSanLuong
			// 
			this.numSanLuong.Location = new System.Drawing.Point(175, 44);
			this.numSanLuong.Maximum = new decimal(new int[] {
            20000000,
            0,
            0,
            0});
			this.numSanLuong.Name = "numSanLuong";
			this.numSanLuong.Size = new System.Drawing.Size(100, 21);
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
			this.numDonGia.Location = new System.Drawing.Point(175, 72);
			this.numDonGia.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.numDonGia.Name = "numDonGia";
			this.numDonGia.Size = new System.Drawing.Size(100, 21);
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
			this.numLuongTT.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numLuongTT.Location = new System.Drawing.Point(175, 128);
			this.numLuongTT.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.numLuongTT.Name = "numLuongTT";
			this.numLuongTT.Size = new System.Drawing.Size(100, 21);
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
			this.numLuongCongNhat.Enabled = false;
			this.numLuongCongNhat.Location = new System.Drawing.Point(175, 184);
			this.numLuongCongNhat.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
			this.numLuongCongNhat.Name = "numLuongCongNhat";
			this.numLuongCongNhat.Size = new System.Drawing.Size(100, 21);
			this.numLuongCongNhat.TabIndex = 4;
			this.numLuongCongNhat.ThousandsSeparator = true;
			// 
			// numBoiDuongCa3
			// 
			this.numBoiDuongCa3.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.numBoiDuongCa3.Location = new System.Drawing.Point(175, 156);
			this.numBoiDuongCa3.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numBoiDuongCa3.Name = "numBoiDuongCa3";
			this.numBoiDuongCa3.Size = new System.Drawing.Size(100, 21);
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
			this.btnDieuChinhLuong.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnDieuChinhLuong.ForeColor = System.Drawing.Color.Blue;
			this.btnDieuChinhLuong.Location = new System.Drawing.Point(15, 212);
			this.btnDieuChinhLuong.Name = "btnDieuChinhLuong";
			this.btnDieuChinhLuong.Size = new System.Drawing.Size(305, 27);
			this.btnDieuChinhLuong.TabIndex = 6;
			this.btnDieuChinhLuong.Text = "Điều chỉnh lương tháng trước cho NV";
			this.toolTip1.SetToolTip(this.btnDieuChinhLuong, "Nhập các khoản tạm ứng tháng, điều chỉnh lương tháng trước, và các khoản thu chi " +
        "khác");
			this.btnDieuChinhLuong.UseVisualStyleBackColor = true;
			this.btnDieuChinhLuong.Click += new System.EventHandler(this.btnDieuChinhLuong_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Title = "Chọn đường dẫn lưu bảng lương";
			// 
			// btnThayDoiHSL
			// 
			this.btnThayDoiHSL.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThayDoiHSL.ForeColor = System.Drawing.Color.Blue;
			this.btnThayDoiHSL.Location = new System.Drawing.Point(15, 311);
			this.btnThayDoiHSL.Name = "btnThayDoiHSL";
			this.btnThayDoiHSL.Size = new System.Drawing.Size(305, 27);
			this.btnThayDoiHSL.TabIndex = 7;
			this.btnThayDoiHSL.Text = "Thay đổi hệ số lương, phụ cấp";
			this.btnThayDoiHSL.UseVisualStyleBackColor = true;
			this.btnThayDoiHSL.Click += new System.EventHandler(this.btnThayDoiHSL_Click);
			// 
			// btnLuongCongNhat
			// 
			this.btnLuongCongNhat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnLuongCongNhat.ForeColor = System.Drawing.Color.Blue;
			this.btnLuongCongNhat.Location = new System.Drawing.Point(15, 245);
			this.btnLuongCongNhat.Name = "btnLuongCongNhat";
			this.btnLuongCongNhat.Size = new System.Drawing.Size(305, 27);
			this.btnLuongCongNhat.TabIndex = 6;
			this.btnLuongCongNhat.Text = "Thêm lương công nhật";
			this.btnLuongCongNhat.UseVisualStyleBackColor = true;
			this.btnLuongCongNhat.Click += new System.EventHandler(this.btnLuongCongNhat_Click);
			// 
			// btnKetCong
			// 
			this.btnKetCong.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnKetCong.ForeColor = System.Drawing.Color.Blue;
			this.btnKetCong.Location = new System.Drawing.Point(15, 344);
			this.btnKetCong.Name = "btnKetCong";
			this.btnKetCong.Size = new System.Drawing.Size(305, 27);
			this.btnKetCong.TabIndex = 7;
			this.btnKetCong.Text = "Đóng kết công tháng";
			this.btnKetCong.UseVisualStyleBackColor = true;
			this.btnKetCong.Click += new System.EventHandler(this.btnKetCong_Click);
			// 
			// btnMoKetCong
			// 
			this.btnMoKetCong.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnMoKetCong.ForeColor = System.Drawing.Color.Blue;
			this.btnMoKetCong.Location = new System.Drawing.Point(15, 377);
			this.btnMoKetCong.Name = "btnMoKetCong";
			this.btnMoKetCong.Size = new System.Drawing.Size(305, 27);
			this.btnMoKetCong.TabIndex = 7;
			this.btnMoKetCong.Text = "Mở kết công tháng";
			this.btnMoKetCong.UseVisualStyleBackColor = true;
			this.btnMoKetCong.Click += new System.EventHandler(this.btnMoKetCong_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(15, 410);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(305, 27);
			this.btnThoat.TabIndex = 7;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label12.Location = new System.Drawing.Point(12, 103);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100, 15);
			this.label12.TabIndex = 0;
			this.label12.Text = "Chi tiền gia công";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(281, 103);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(35, 15);
			this.label15.TabIndex = 0;
			this.label15.Text = "đồng";
			// 
			// numChiTienGiaCong
			// 
			this.numChiTienGiaCong.Location = new System.Drawing.Point(175, 100);
			this.numChiTienGiaCong.Maximum = new decimal(new int[] {
            705032704,
            1,
            0,
            0});
			this.numChiTienGiaCong.Name = "numChiTienGiaCong";
			this.numChiTienGiaCong.Size = new System.Drawing.Size(100, 21);
			this.numChiTienGiaCong.TabIndex = 2;
			this.numChiTienGiaCong.ThousandsSeparator = true;
			// 
			// frm_41_TinhLuongNV
			// 
			this.ClientSize = new System.Drawing.Size(330, 446);
			this.Controls.Add(this.numBoiDuongCa3);
			this.Controls.Add(this.numLuongCongNhat);
			this.Controls.Add(this.numLuongTT);
			this.Controls.Add(this.numChiTienGiaCong);
			this.Controls.Add(this.numDonGia);
			this.Controls.Add(this.numSanLuong);
			this.Controls.Add(this.dtpThang);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnLuongCongNhat);
			this.Controls.Add(this.btnDieuChinhLuong);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnMoKetCong);
			this.Controls.Add(this.btnKetCong);
			this.Controls.Add(this.btnThayDoiHSL);
			this.Controls.Add(this.btnTinhLuong);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_41_TinhLuongNV";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Tính lương cho NV";
			this.Load += new System.EventHandler(this.frm_41_TinhLuongNV_Load);
			((System.ComponentModel.ISupportInitialize)(this.numSanLuong)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numDonGia)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numLuongTT)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numLuongCongNhat)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numBoiDuongCa3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numChiTienGiaCong)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label label1;
        private Button btnTinhLuong;
        private Label label2;
		private Label label3;
		private Label label4;
		private Label label7;
        private Label label8;
		private Label label9;
        private Label label10;
		private Label label13;
        private Label label14;
		private DateTimePicker dtpThang;
		private Label label5;
        private NumericUpDown numSanLuong;
        private NumericUpDown numDonGia;
        private NumericUpDown numLuongTT;
        private NumericUpDown numLuongCongNhat;
        private NumericUpDown numBoiDuongCa3;
		private Button btnDieuChinhLuong;
		private SaveFileDialog saveFileDialog;
		private Button btnThayDoiHSL;
		private ToolTip toolTip1;
		private Button btnLuongCongNhat;
		private Button btnKetCong;
		private Button btnMoKetCong;
		private Button btnThoat;
		private Label label12;
		private Label label15;
		private NumericUpDown numChiTienGiaCong;
	}
}