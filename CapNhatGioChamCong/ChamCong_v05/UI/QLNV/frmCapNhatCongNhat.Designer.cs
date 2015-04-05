namespace ChamCong_v05.UI.QLNV {
	partial class frmCapNhatCongNhat {
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
			this.checkLamCongnhat = new System.Windows.Forms.CheckBox();
			this.dtpNgayKTCongnhat = new System.Windows.Forms.DateTimePicker();
			this.dtpNgayBDCongnhat = new System.Windows.Forms.DateTimePicker();
			this.label16 = new System.Windows.Forms.Label();
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCapNhat = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.tbMaNV = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tbTenNV = new System.Windows.Forms.TextBox();
			this.checkNVChinhThuc = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// checkLamCongnhat
			// 
			this.checkLamCongnhat.AutoSize = true;
			this.checkLamCongnhat.Location = new System.Drawing.Point(21, 89);
			this.checkLamCongnhat.Name = "checkLamCongnhat";
			this.checkLamCongnhat.Size = new System.Drawing.Size(146, 19);
			this.checkLamCongnhat.TabIndex = 3;
			this.checkLamCongnhat.Text = "Làm việc công nhật từ";
			this.checkLamCongnhat.UseVisualStyleBackColor = true;
			this.checkLamCongnhat.CheckedChanged += new System.EventHandler(this.checkLamCongnhat_CheckedChanged);
			// 
			// dtpNgayKTCongnhat
			// 
			this.dtpNgayKTCongnhat.CustomFormat = "dddd dd";
			this.dtpNgayKTCongnhat.Enabled = false;
			this.dtpNgayKTCongnhat.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgayKTCongnhat.Location = new System.Drawing.Point(171, 114);
			this.dtpNgayKTCongnhat.Name = "dtpNgayKTCongnhat";
			this.dtpNgayKTCongnhat.ShowUpDown = true;
			this.dtpNgayKTCongnhat.Size = new System.Drawing.Size(111, 21);
			this.dtpNgayKTCongnhat.TabIndex = 6;
			this.dtpNgayKTCongnhat.Value = new System.DateTime(2013, 8, 1, 0, 0, 0, 0);
			// 
			// dtpNgayBDCongnhat
			// 
			this.dtpNgayBDCongnhat.CustomFormat = "dddd dd";
			this.dtpNgayBDCongnhat.Enabled = false;
			this.dtpNgayBDCongnhat.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgayBDCongnhat.Location = new System.Drawing.Point(171, 87);
			this.dtpNgayBDCongnhat.Name = "dtpNgayBDCongnhat";
			this.dtpNgayBDCongnhat.ShowUpDown = true;
			this.dtpNgayBDCongnhat.Size = new System.Drawing.Size(111, 21);
			this.dtpNgayBDCongnhat.TabIndex = 4;
			this.dtpNgayBDCongnhat.Value = new System.DateTime(2013, 8, 1, 0, 0, 0, 0);
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(88, 119);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(77, 15);
			this.label16.TabIndex = 5;
			this.label16.Text = "đến hết ngày";
			// 
			// dtpThang
			// 
			this.dtpThang.CustomFormat = "MM / yyyy";
			this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpThang.Location = new System.Drawing.Point(171, 60);
			this.dtpThang.Name = "dtpThang";
			this.dtpThang.ShowUpDown = true;
			this.dtpThang.Size = new System.Drawing.Size(111, 21);
			this.dtpThang.TabIndex = 2;
			this.dtpThang.Value = new System.DateTime(2013, 8, 1, 0, 0, 0, 0);
			this.dtpThang.ValueChanged += new System.EventHandler(this.dtpThang_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 63);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 15);
			this.label1.TabIndex = 10;
			this.label1.Text = "Chọn tháng";
			// 
			// btnCapNhat
			// 
			this.btnCapNhat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnCapNhat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnCapNhat.ForeColor = System.Drawing.Color.Blue;
			this.btnCapNhat.Location = new System.Drawing.Point(171, 173);
			this.btnCapNhat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnCapNhat.Name = "btnCapNhat";
			this.btnCapNhat.Size = new System.Drawing.Size(75, 25);
			this.btnCapNhat.TabIndex = 8;
			this.btnCapNhat.Text = "Cập nhật";
			this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(286, 173);
			this.btnThoat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(75, 25);
			this.btnThoat.TabIndex = 9;
			this.btnThoat.Text = "Đóng";
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(18, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 15);
			this.label2.TabIndex = 15;
			this.label2.Text = "Mã nhân viên";
			// 
			// tbMaNV
			// 
			this.tbMaNV.Enabled = false;
			this.tbMaNV.Location = new System.Drawing.Point(171, 6);
			this.tbMaNV.Name = "tbMaNV";
			this.tbMaNV.Size = new System.Drawing.Size(75, 21);
			this.tbMaNV.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(18, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 15);
			this.label3.TabIndex = 15;
			this.label3.Text = "Tên nhân viên";
			// 
			// tbTenNV
			// 
			this.tbTenNV.Enabled = false;
			this.tbTenNV.Location = new System.Drawing.Point(171, 33);
			this.tbTenNV.Name = "tbTenNV";
			this.tbTenNV.Size = new System.Drawing.Size(190, 21);
			this.tbTenNV.TabIndex = 1;
			// 
			// checkNVChinhThuc
			// 
			this.checkNVChinhThuc.AutoSize = true;
			this.checkNVChinhThuc.Location = new System.Drawing.Point(21, 141);
			this.checkNVChinhThuc.Name = "checkNVChinhThuc";
			this.checkNVChinhThuc.Size = new System.Drawing.Size(317, 19);
			this.checkNVChinhThuc.TabIndex = 7;
			this.checkNVChinhThuc.Text = "Tính lương các ngày còn lại như nhân viên chính thức";
			this.checkNVChinhThuc.UseVisualStyleBackColor = true;
			this.checkNVChinhThuc.CheckedChanged += new System.EventHandler(this.checkLamCongnhat_CheckedChanged);
			// 
			// frmCapNhatCongNhat
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(377, 209);
			this.Controls.Add(this.tbTenNV);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tbMaNV);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnCapNhat);
			this.Controls.Add(this.checkNVChinhThuc);
			this.Controls.Add(this.checkLamCongnhat);
			this.Controls.Add(this.dtpNgayKTCongnhat);
			this.Controls.Add(this.dtpThang);
			this.Controls.Add(this.dtpNgayBDCongnhat);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label16);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.Name = "frmCapNhatCongNhat";
			this.Text = "Cập nhật ngày công nhật cho nhân viên";
			this.Load += new System.EventHandler(this.frmCapNhatCongNhat_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkLamCongnhat;
		private System.Windows.Forms.DateTimePicker dtpNgayKTCongnhat;
		private System.Windows.Forms.DateTimePicker dtpNgayBDCongnhat;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.DateTimePicker dtpThang;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnCapNhat;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbMaNV;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbTenNV;
		private System.Windows.Forms.CheckBox checkNVChinhThuc;
	}
}