namespace ChamCong_v05.UI.ChamCong {
	partial class frm_XuatBBCongPC {
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
			this.radBB_ChamCongThang = new System.Windows.Forms.RadioButton();
			this.btnXuatBB = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
			this.tbTenTruongBP = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tbTenNVNhapLieu = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.SuspendLayout();
			// 
			// radBB_ChamCongThang
			// 
			this.radBB_ChamCongThang.AutoSize = true;
			this.radBB_ChamCongThang.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.radBB_ChamCongThang.Location = new System.Drawing.Point(13, 63);
			this.radBB_ChamCongThang.Name = "radBB_ChamCongThang";
			this.radBB_ChamCongThang.Size = new System.Drawing.Size(189, 19);
			this.radBB_ChamCongThang.TabIndex = 1;
			this.radBB_ChamCongThang.Text = "Xuất báo biểu kết công tháng";
			this.radBB_ChamCongThang.UseVisualStyleBackColor = true;
			// 
			// btnXuatBB
			// 
			this.btnXuatBB.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnXuatBB.ForeColor = System.Drawing.Color.Blue;
			this.btnXuatBB.Location = new System.Drawing.Point(222, 140);
			this.btnXuatBB.Name = "btnXuatBB";
			this.btnXuatBB.Size = new System.Drawing.Size(75, 25);
			this.btnXuatBB.TabIndex = 5;
			this.btnXuatBB.Text = "Xuất";
			this.btnXuatBB.UseVisualStyleBackColor = true;
			this.btnXuatBB.Click += new System.EventHandler(this.btnXuatBB_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.button2.ForeColor = System.Drawing.Color.Blue;
			this.button2.Location = new System.Drawing.Point(335, 140);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 25);
			this.button2.TabIndex = 6;
			this.button2.Text = "Thoát";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// saveFileDlg
			// 
			this.saveFileDlg.CreatePrompt = true;
			this.saveFileDlg.DefaultExt = "xlsx";
			this.saveFileDlg.Filter = "Excel File|*.xlsx";
			// 
			// tbTenTruongBP
			// 
			this.tbTenTruongBP.Location = new System.Drawing.Point(222, 87);
			this.tbTenTruongBP.Name = "tbTenTruongBP";
			this.tbTenTruongBP.Size = new System.Drawing.Size(188, 21);
			this.tbTenTruongBP.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(29, 90);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(145, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "Nhập tên trưởng bộ phận";
			// 
			// tbTenNVNhapLieu
			// 
			this.tbTenNVNhapLieu.Location = new System.Drawing.Point(222, 113);
			this.tbTenNVNhapLieu.Name = "tbTenNVNhapLieu";
			this.tbTenNVNhapLieu.Size = new System.Drawing.Size(188, 21);
			this.tbTenNVNhapLieu.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(29, 116);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(123, 15);
			this.label2.TabIndex = 3;
			this.label2.Text = "Nhập tên NV lập biểu";
			// 
			// radioButton2
			// 
			this.radioButton2.Checked = true;
			this.radioButton2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.radioButton2.Location = new System.Drawing.Point(12, 13);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(398, 39);
			this.radioButton2.TabIndex = 0;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "Xuất báo biểu thống kê đi trễ về sớm, giờ vào ra thiếu chấm công, giờ vào ra khôn" +
    "g nhận diện được ca";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// dtpThang
			// 
			this.dtpThang.CustomFormat = "M / yyyy";
			this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpThang.Location = new System.Drawing.Point(222, 60);
			this.dtpThang.Name = "dtpThang";
			this.dtpThang.ShowUpDown = true;
			this.dtpThang.Size = new System.Drawing.Size(92, 21);
			this.dtpThang.TabIndex = 2;
			this.dtpThang.Value = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
			// 
			// frm_XuatBBCongPC
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(422, 179);
			this.ControlBox = false;
			this.Controls.Add(this.dtpThang);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbTenNVNhapLieu);
			this.Controls.Add(this.tbTenTruongBP);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.btnXuatBB);
			this.Controls.Add(this.radioButton2);
			this.Controls.Add(this.radBB_ChamCongThang);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "frm_XuatBBCongPC";
			this.Text = "Xuất báo biểu";
			this.Load += new System.EventHandler(this.Form2_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton radBB_ChamCongThang;
		private System.Windows.Forms.Button btnXuatBB;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.SaveFileDialog saveFileDlg;
		private System.Windows.Forms.TextBox tbTenTruongBP;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbTenNVNhapLieu;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.DateTimePicker dtpThang;
	}
}