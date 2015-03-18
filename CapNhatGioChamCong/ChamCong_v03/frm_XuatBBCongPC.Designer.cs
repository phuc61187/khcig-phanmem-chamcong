namespace ChamCong_v03 {
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
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
			this.tbTenTruongBP = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tbTenNVNhapLieu = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// radBB_ChamCongThang
			// 
			this.radBB_ChamCongThang.AutoSize = true;
			this.radBB_ChamCongThang.Checked = true;
			this.radBB_ChamCongThang.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.radBB_ChamCongThang.Location = new System.Drawing.Point(13, 12);
			this.radBB_ChamCongThang.Name = "radBB_ChamCongThang";
			this.radBB_ChamCongThang.Size = new System.Drawing.Size(223, 19);
			this.radBB_ChamCongThang.TabIndex = 0;
			this.radBB_ChamCongThang.TabStop = true;
			this.radBB_ChamCongThang.Text = "Xuất báo biểu chấm công tháng {0}";
			this.radBB_ChamCongThang.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.radioButton1.Location = new System.Drawing.Point(13, 37);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(281, 19);
			this.radioButton1.TabIndex = 1;
			this.radioButton1.Text = "Xuất báo biểu chấm công các ngày đang xem";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.button1.ForeColor = System.Drawing.Color.Blue;
			this.button1.Location = new System.Drawing.Point(188, 138);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 25);
			this.button1.TabIndex = 4;
			this.button1.Text = "Xuất";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.btnXuata_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.button2.ForeColor = System.Drawing.Color.Blue;
			this.button2.Location = new System.Drawing.Point(301, 138);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 25);
			this.button2.TabIndex = 5;
			this.button2.Text = "Thoát";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// tbTenTruongBP
			// 
			this.tbTenTruongBP.Location = new System.Drawing.Point(188, 61);
			this.tbTenTruongBP.Name = "tbTenTruongBP";
			this.tbTenTruongBP.Size = new System.Drawing.Size(188, 21);
			this.tbTenTruongBP.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(28, 65);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(145, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "Nhập tên trưởng bộ phận";
			// 
			// tbTenNVNhapLieu
			// 
			this.tbTenNVNhapLieu.Location = new System.Drawing.Point(188, 87);
			this.tbTenNVNhapLieu.Name = "tbTenNVNhapLieu";
			this.tbTenNVNhapLieu.Size = new System.Drawing.Size(188, 21);
			this.tbTenNVNhapLieu.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(28, 91);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(123, 15);
			this.label2.TabIndex = 3;
			this.label2.Text = "Nhập tên NV lập biểu";
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.radioButton2.Location = new System.Drawing.Point(12, 113);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(232, 19);
			this.radioButton2.TabIndex = 0;
			this.radioButton2.Text = "Xuất báo biểu thống kê đi trễ về sớm";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// frm_XuatBBCongPC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(388, 176);
			this.ControlBox = false;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbTenNVNhapLieu);
			this.Controls.Add(this.tbTenTruongBP);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.radioButton1);
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
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.SaveFileDialog saveFileDlg;
		private System.Windows.Forms.TextBox tbTenTruongBP;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbTenNVNhapLieu;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton radioButton2;
	}
}