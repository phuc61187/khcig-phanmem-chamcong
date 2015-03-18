namespace ChamCong_v03 {
	partial class frm_112_XacNhanPC100 {
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
			this.components = new System.ComponentModel.Container();
			this.gbChiTiet = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.numPCCus2 = new System.Windows.Forms.NumericUpDown();
			this.numPCCus1 = new System.Windows.Forms.NumericUpDown();
			this.radPCCus2 = new System.Windows.Forms.RadioButton();
			this.radPCCus1 = new System.Windows.Forms.RadioButton();
			this.radPCNgayLe = new System.Windows.Forms.RadioButton();
			this.radKhongPC = new System.Windows.Forms.RadioButton();
			this.radPCNgayNghi = new System.Windows.Forms.RadioButton();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.btnXacNhan = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.gbChiTiet.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numPCCus2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numPCCus1)).BeginInit();
			this.SuspendLayout();
			// 
			// gbChiTiet
			// 
			this.gbChiTiet.Controls.Add(this.label2);
			this.gbChiTiet.Controls.Add(this.label1);
			this.gbChiTiet.Controls.Add(this.numPCCus2);
			this.gbChiTiet.Controls.Add(this.numPCCus1);
			this.gbChiTiet.Controls.Add(this.radPCCus2);
			this.gbChiTiet.Controls.Add(this.radPCCus1);
			this.gbChiTiet.Controls.Add(this.radPCNgayLe);
			this.gbChiTiet.Controls.Add(this.radKhongPC);
			this.gbChiTiet.Controls.Add(this.radPCNgayNghi);
			this.gbChiTiet.Controls.Add(this.textBox4);
			this.gbChiTiet.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.gbChiTiet.Location = new System.Drawing.Point(11, 10);
			this.gbChiTiet.Name = "gbChiTiet";
			this.gbChiTiet.Size = new System.Drawing.Size(385, 154);
			this.gbChiTiet.TabIndex = 0;
			this.gbChiTiet.TabStop = false;
			this.gbChiTiet.Text = "Xác nhận";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(361, 126);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(20, 16);
			this.label2.TabIndex = 25;
			this.label2.Text = "%";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(361, 97);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(20, 16);
			this.label1.TabIndex = 25;
			this.label1.Text = "%";
			// 
			// numPCCus2
			// 
			this.numPCCus2.Location = new System.Drawing.Point(305, 122);
			this.numPCCus2.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
			this.numPCCus2.Name = "numPCCus2";
			this.numPCCus2.Size = new System.Drawing.Size(53, 21);
			this.numPCCus2.TabIndex = 7;
			// 
			// numPCCus1
			// 
			this.numPCCus1.Location = new System.Drawing.Point(305, 96);
			this.numPCCus1.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
			this.numPCCus1.Name = "numPCCus1";
			this.numPCCus1.Size = new System.Drawing.Size(53, 21);
			this.numPCCus1.TabIndex = 5;
			// 
			// radPCCus2
			// 
			this.radPCCus2.AutoSize = true;
			this.radPCCus2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.radPCCus2.ForeColor = System.Drawing.Color.DarkRed;
			this.radPCCus2.Location = new System.Drawing.Point(3, 122);
			this.radPCCus2.Name = "radPCCus2";
			this.radPCCus2.Size = new System.Drawing.Size(221, 20);
			this.radPCCus2.TabIndex = 6;
			this.radPCCus2.TabStop = true;
			this.radPCCus2.Text = "Tính PC tuỳ chỉnh cho giờ làm";
			this.toolTip1.SetToolTip(this.radPCCus2, "Công 100% + PC tuỳ chỉnh cho toàn bộ giờ làm");
			this.radPCCus2.UseVisualStyleBackColor = true;
			// 
			// radPCCus1
			// 
			this.radPCCus1.AutoSize = true;
			this.radPCCus1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.radPCCus1.ForeColor = System.Drawing.Color.DarkRed;
			this.radPCCus1.Location = new System.Drawing.Point(3, 96);
			this.radPCCus1.Name = "radPCCus1";
			this.radPCCus1.Size = new System.Drawing.Size(297, 20);
			this.radPCCus1.TabIndex = 4;
			this.radPCCus1.TabStop = true;
			this.radPCCus1.Text = "Tính PC tuỳ chỉnh cho giờ làm trên 8 tiếng";
			this.toolTip1.SetToolTip(this.radPCCus1, "Công 100% + PC tuỳ chỉnh tính riêng cho khoảng làm thêm giờ");
			this.radPCCus1.UseVisualStyleBackColor = true;
			// 
			// radPCNgayLe
			// 
			this.radPCNgayLe.AutoSize = true;
			this.radPCNgayLe.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.radPCNgayLe.ForeColor = System.Drawing.Color.DarkRed;
			this.radPCNgayLe.Location = new System.Drawing.Point(3, 70);
			this.radPCNgayLe.Name = "radPCNgayLe";
			this.radPCNgayLe.Size = new System.Drawing.Size(274, 20);
			this.radPCNgayLe.TabIndex = 2;
			this.radPCNgayLe.TabStop = true;
			this.radPCNgayLe.Text = "Tính PC làm việc vào ngày lễ, tết 200%";
			this.toolTip1.SetToolTip(this.radPCNgayLe, "Công 100% + PC 200%");
			this.radPCNgayLe.UseVisualStyleBackColor = true;
			// 
			// radKhongPC
			// 
			this.radKhongPC.AutoSize = true;
			this.radKhongPC.Checked = true;
			this.radKhongPC.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.radKhongPC.ForeColor = System.Drawing.Color.DarkRed;
			this.radKhongPC.Location = new System.Drawing.Point(3, 20);
			this.radKhongPC.Name = "radKhongPC";
			this.radKhongPC.Size = new System.Drawing.Size(241, 20);
			this.radKhongPC.TabIndex = 0;
			this.radKhongPC.TabStop = true;
			this.radKhongPC.Text = "Huỷ tính PC vào ngày nghỉ, lễ, tết";
			this.radKhongPC.UseVisualStyleBackColor = true;
			// 
			// radPCNgayNghi
			// 
			this.radPCNgayNghi.AutoSize = true;
			this.radPCNgayNghi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.radPCNgayNghi.ForeColor = System.Drawing.Color.DarkRed;
			this.radPCNgayNghi.Location = new System.Drawing.Point(3, 46);
			this.radPCNgayNghi.Name = "radPCNgayNghi";
			this.radPCNgayNghi.Size = new System.Drawing.Size(266, 20);
			this.radPCNgayNghi.TabIndex = 1;
			this.radPCNgayNghi.TabStop = true;
			this.radPCNgayNghi.Text = "Tính PC làm việc vào ngày nghỉ 100%";
			this.toolTip1.SetToolTip(this.radPCNgayNghi, "Công 100% + PC 100%");
			this.radPCNgayNghi.UseVisualStyleBackColor = true;
			// 
			// textBox4
			// 
			this.textBox4.Enabled = false;
			this.textBox4.Location = new System.Drawing.Point(-71, -198);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(32, 21);
			this.textBox4.TabIndex = 5;
			// 
			// btnXacNhan
			// 
			this.btnXacNhan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnXacNhan.ForeColor = System.Drawing.Color.Blue;
			this.btnXacNhan.Location = new System.Drawing.Point(37, 170);
			this.btnXacNhan.Name = "btnXacNhan";
			this.btnXacNhan.Size = new System.Drawing.Size(83, 27);
			this.btnXacNhan.TabIndex = 0;
			this.btnXacNhan.Text = "Thực hiện";
			this.btnXacNhan.UseVisualStyleBackColor = true;
			this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(286, 170);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(83, 27);
			this.btnThoat.TabIndex = 1;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// frm_112_XacNhanPC100
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(407, 213);
			this.Controls.Add(this.gbChiTiet);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnXacNhan);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.Name = "frm_112_XacNhanPC100";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Xác nhận làm thêm";
			this.gbChiTiet.ResumeLayout(false);
			this.gbChiTiet.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numPCCus2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numPCCus1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbChiTiet;
		private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnXacNhan;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.RadioButton radPCNgayLe;
		private System.Windows.Forms.RadioButton radPCNgayNghi;
		private System.Windows.Forms.RadioButton radKhongPC;
		private System.Windows.Forms.NumericUpDown numPCCus2;
		private System.Windows.Forms.NumericUpDown numPCCus1;
		private System.Windows.Forms.RadioButton radPCCus2;
		private System.Windows.Forms.RadioButton radPCCus1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;

	}
}