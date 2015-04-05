namespace ChamCong_v05.UI.TinhLuong {
	partial class frmHuyKetLuongThang {
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
			this.label5 = new System.Windows.Forms.Label();
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.btnThucHien = new System.Windows.Forms.Button();
			this.btnDong = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label5.Location = new System.Drawing.Point(12, 13);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(153, 15);
			this.label5.TabIndex = 2;
			this.label5.Text = "Nhập tháng huỷ kết lương";
			// 
			// dtpThang
			// 
			this.dtpThang.CustomFormat = "M / yyyy";
			this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpThang.Location = new System.Drawing.Point(12, 35);
			this.dtpThang.Name = "dtpThang";
			this.dtpThang.ShowUpDown = true;
			this.dtpThang.Size = new System.Drawing.Size(92, 21);
			this.dtpThang.TabIndex = 0;
			this.dtpThang.Value = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
			// 
			// btnThucHien
			// 
			this.btnThucHien.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThucHien.ForeColor = System.Drawing.Color.Blue;
			this.btnThucHien.Location = new System.Drawing.Point(12, 62);
			this.btnThucHien.Name = "btnThucHien";
			this.btnThucHien.Size = new System.Drawing.Size(76, 27);
			this.btnThucHien.TabIndex = 1;
			this.btnThucHien.Text = "Thực hiện";
			this.btnThucHien.UseVisualStyleBackColor = true;
			this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
			// 
			// btnDong
			// 
			this.btnDong.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnDong.ForeColor = System.Drawing.Color.Blue;
			this.btnDong.Location = new System.Drawing.Point(103, 62);
			this.btnDong.Name = "btnDong";
			this.btnDong.Size = new System.Drawing.Size(76, 27);
			this.btnDong.TabIndex = 2;
			this.btnDong.Text = "Đóng";
			this.btnDong.UseVisualStyleBackColor = true;
			this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
			// 
			// frmHuyKetLuongThang
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(196, 107);
			this.ControlBox = false;
			this.Controls.Add(this.btnDong);
			this.Controls.Add(this.btnThucHien);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.dtpThang);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.Name = "frmHuyKetLuongThang";
			this.Text = "Huỷ kết lương tháng";
			this.Load += new System.EventHandler(this.frm_KetCongBoPhan_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DateTimePicker dtpThang;
		private System.Windows.Forms.Button btnThucHien;
		private System.Windows.Forms.Button btnDong;
	}
}