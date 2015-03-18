namespace ChamCong_v03 {
	partial class frm_112_XacNhanPC50 {
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
			this.radKhongPC50 = new System.Windows.Forms.RadioButton();
			this.radPC50 = new System.Windows.Forms.RadioButton();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.btnXacNhan = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.gbChiTiet.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbChiTiet
			// 
			this.gbChiTiet.Controls.Add(this.radKhongPC50);
			this.gbChiTiet.Controls.Add(this.radPC50);
			this.gbChiTiet.Controls.Add(this.textBox4);
			this.gbChiTiet.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.gbChiTiet.Location = new System.Drawing.Point(10, 11);
			this.gbChiTiet.Name = "gbChiTiet";
			this.gbChiTiet.Size = new System.Drawing.Size(350, 74);
			this.gbChiTiet.TabIndex = 0;
			this.gbChiTiet.TabStop = false;
			this.gbChiTiet.Text = "Xác nhận";
			// 
			// radKhongPC50
			// 
			this.radKhongPC50.AutoSize = true;
			this.radKhongPC50.Checked = true;
			this.radKhongPC50.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.radKhongPC50.ForeColor = System.Drawing.Color.DarkRed;
			this.radKhongPC50.Location = new System.Drawing.Point(4, 18);
			this.radKhongPC50.Name = "radKhongPC50";
			this.radKhongPC50.Size = new System.Drawing.Size(197, 20);
			this.radKhongPC50.TabIndex = 0;
			this.radKhongPC50.TabStop = true;
			this.radKhongPC50.Text = "Không tính PC tăng cường";
			this.radKhongPC50.UseVisualStyleBackColor = true;
			// 
			// radPC50
			// 
			this.radPC50.AutoSize = true;
			this.radPC50.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.radPC50.ForeColor = System.Drawing.Color.DarkRed;
			this.radPC50.Location = new System.Drawing.Point(4, 44);
			this.radPC50.Name = "radPC50";
			this.radPC50.Size = new System.Drawing.Size(336, 20);
			this.radPC50.TabIndex = 1;
			this.radPC50.TabStop = true;
			this.radPC50.Text = "Tính PC tăng cường trường hợp làm trên 8 tiếng";
			this.radPC50.UseVisualStyleBackColor = true;
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
			this.btnXacNhan.Location = new System.Drawing.Point(28, 91);
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
			this.btnThoat.Location = new System.Drawing.Point(267, 91);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(83, 27);
			this.btnThoat.TabIndex = 1;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// frm_112_XacNhanPC50
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(372, 127);
			this.Controls.Add(this.gbChiTiet);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnXacNhan);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.Name = "frm_112_XacNhanPC50";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Xác nhận phụ cấp tăng cường";
			this.gbChiTiet.ResumeLayout(false);
			this.gbChiTiet.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbChiTiet;
		private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnXacNhan;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.RadioButton radPC50;
		private System.Windows.Forms.RadioButton radKhongPC50;

	}
}