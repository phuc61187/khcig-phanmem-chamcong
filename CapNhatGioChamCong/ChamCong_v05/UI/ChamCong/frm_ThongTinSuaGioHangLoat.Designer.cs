namespace ChamCong_v05.UI.ChamCong
{
	partial class frm_ThongTinSuaGioHangLoat {
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
			this.tbGhichu = new System.Windows.Forms.TextBox();
			this.dateTimeGioMoi = new System.Windows.Forms.DateTimePicker();
			this.labelX2 = new System.Windows.Forms.Label();
			this.labelX4 = new System.Windows.Forms.Label();
			this.labelX3 = new System.Windows.Forms.Label();
			this.labelX1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radRaa = new System.Windows.Forms.RadioButton();
			this.radVao = new System.Windows.Forms.RadioButton();
			this.cbCa = new System.Windows.Forms.ComboBox();
			this.cbLyDo = new System.Windows.Forms.ComboBox();
			this.lbKieuGio = new System.Windows.Forms.Label();
			this.btnDongY = new System.Windows.Forms.Button();
			this.btnHuy = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbGhichu
			// 
			this.tbGhichu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.tbGhichu.Location = new System.Drawing.Point(148, 140);
			this.tbGhichu.Margin = new System.Windows.Forms.Padding(4);
			this.tbGhichu.Multiline = true;
			this.tbGhichu.Name = "tbGhichu";
			this.tbGhichu.Size = new System.Drawing.Size(203, 84);
			this.tbGhichu.TabIndex = 5;
			// 
			// dateTimeGioMoi
			// 
			this.dateTimeGioMoi.CustomFormat = "H:mm   d/M/yyyy dddd";
			this.dateTimeGioMoi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.dateTimeGioMoi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimeGioMoi.Location = new System.Drawing.Point(148, 82);
			this.dateTimeGioMoi.Margin = new System.Windows.Forms.Padding(4);
			this.dateTimeGioMoi.Name = "dateTimeGioMoi";
			this.dateTimeGioMoi.ShowUpDown = true;
			this.dateTimeGioMoi.Size = new System.Drawing.Size(203, 21);
			this.dateTimeGioMoi.TabIndex = 3;
			// 
			// labelX2
			// 
			this.labelX2.AutoSize = true;
			this.labelX2.BackColor = System.Drawing.Color.Transparent;
			this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.labelX2.Location = new System.Drawing.Point(99, 112);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(37, 15);
			this.labelX2.TabIndex = 4;
			this.labelX2.Text = "Lý do";
			// 
			// labelX4
			// 
			this.labelX4.AutoSize = true;
			this.labelX4.BackColor = System.Drawing.Color.Transparent;
			this.labelX4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.labelX4.Location = new System.Drawing.Point(84, 142);
			this.labelX4.Name = "labelX4";
			this.labelX4.Size = new System.Drawing.Size(49, 15);
			this.labelX4.TabIndex = 4;
			this.labelX4.Text = "Ghi chú";
			// 
			// labelX3
			// 
			this.labelX3.AutoSize = true;
			this.labelX3.BackColor = System.Drawing.Color.Transparent;
			this.labelX3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.labelX3.Location = new System.Drawing.Point(80, 54);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(54, 15);
			this.labelX3.TabIndex = 4;
			this.labelX3.Text = "Kiểu giờ";
			// 
			// labelX1
			// 
			this.labelX1.AutoSize = true;
			this.labelX1.BackColor = System.Drawing.Color.Transparent;
			this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.labelX1.Location = new System.Drawing.Point(0, 27);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(129, 15);
			this.labelX1.TabIndex = 4;
			this.labelX1.Text = "Nhập giờ mới theo ca";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radRaa);
			this.groupBox1.Controls.Add(this.radVao);
			this.groupBox1.Controls.Add(this.cbCa);
			this.groupBox1.Controls.Add(this.cbLyDo);
			this.groupBox1.Controls.Add(this.tbGhichu);
			this.groupBox1.Controls.Add(this.dateTimeGioMoi);
			this.groupBox1.Controls.Add(this.labelX1);
			this.groupBox1.Controls.Add(this.lbKieuGio);
			this.groupBox1.Controls.Add(this.labelX3);
			this.groupBox1.Controls.Add(this.labelX2);
			this.groupBox1.Controls.Add(this.labelX4);
			this.groupBox1.Location = new System.Drawing.Point(12, 11);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.groupBox1.Size = new System.Drawing.Size(361, 232);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Thông tin giờ sửa";
			// 
			// radRaa
			// 
			this.radRaa.AutoSize = true;
			this.radRaa.Location = new System.Drawing.Point(235, 52);
			this.radRaa.Name = "radRaa";
			this.radRaa.Size = new System.Drawing.Size(41, 19);
			this.radRaa.TabIndex = 2;
			this.radRaa.Text = "Ra";
			this.radRaa.UseVisualStyleBackColor = true;
			this.radRaa.CheckedChanged += new System.EventHandler(this.switchButton1_ValueChanged);
			// 
			// radVao
			// 
			this.radVao.AutoSize = true;
			this.radVao.Checked = true;
			this.radVao.Location = new System.Drawing.Point(148, 52);
			this.radVao.Name = "radVao";
			this.radVao.Size = new System.Drawing.Size(46, 19);
			this.radVao.TabIndex = 1;
			this.radVao.TabStop = true;
			this.radVao.Text = "Vào";
			this.radVao.UseVisualStyleBackColor = true;
			this.radVao.CheckedChanged += new System.EventHandler(this.switchButton1_ValueChanged);
			// 
			// cbCa
			// 
			this.cbCa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCa.FormattingEnabled = true;
			this.cbCa.Items.AddRange(new object[] {
            "Lý do khác",
            "Cúp điện"});
			this.cbCa.Location = new System.Drawing.Point(148, 24);
			this.cbCa.Name = "cbCa";
			this.cbCa.Size = new System.Drawing.Size(203, 23);
			this.cbCa.TabIndex = 0;
			// 
			// cbLyDo
			// 
			this.cbLyDo.FormattingEnabled = true;
			this.cbLyDo.Items.AddRange(new object[] {
            "Lý do khác",
            "Cúp điện"});
			this.cbLyDo.Location = new System.Drawing.Point(148, 109);
			this.cbLyDo.Name = "cbLyDo";
			this.cbLyDo.Size = new System.Drawing.Size(203, 23);
			this.cbLyDo.TabIndex = 4;
			// 
			// lbKieuGio
			// 
			this.lbKieuGio.AutoSize = true;
			this.lbKieuGio.BackColor = System.Drawing.Color.Transparent;
			this.lbKieuGio.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lbKieuGio.Location = new System.Drawing.Point(83, 84);
			this.lbKieuGio.Name = "lbKieuGio";
			this.lbKieuGio.Size = new System.Drawing.Size(50, 15);
			this.lbKieuGio.TabIndex = 4;
			this.lbKieuGio.Text = "Giờ vào";
			// 
			// btnDongY
			// 
			this.btnDongY.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnDongY.ForeColor = System.Drawing.Color.Blue;
			this.btnDongY.Location = new System.Drawing.Point(83, 249);
			this.btnDongY.Name = "btnDongY";
			this.btnDongY.Size = new System.Drawing.Size(75, 25);
			this.btnDongY.TabIndex = 0;
			this.btnDongY.Text = "Đồng ý";
			this.btnDongY.UseVisualStyleBackColor = true;
			this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
			// 
			// btnHuy
			// 
			this.btnHuy.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnHuy.ForeColor = System.Drawing.Color.Blue;
			this.btnHuy.Location = new System.Drawing.Point(226, 249);
			this.btnHuy.Name = "btnHuy";
			this.btnHuy.Size = new System.Drawing.Size(75, 25);
			this.btnHuy.TabIndex = 1;
			this.btnHuy.Text = "Hủy";
			this.btnHuy.UseVisualStyleBackColor = true;
			this.btnHuy.Click += new System.EventHandler(this.btnHuyBo_Click);
			// 
			// frm_ThongTinSuaGioHangLoat
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 286);
			this.Controls.Add(this.btnHuy);
			this.Controls.Add(this.btnDongY);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_ThongTinSuaGioHangLoat";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Thông tin sửa giờ chấm công hàng loạt";
			this.Load += new System.EventHandler(this.frm_ThongTinSuaGio_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox tbGhichu;
		private System.Windows.Forms.DateTimePicker dateTimeGioMoi;
        private System.Windows.Forms.Label labelX1;
		private System.Windows.Forms.Label labelX2;
		private System.Windows.Forms.Label labelX4;
        private System.Windows.Forms.Label labelX3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbLyDo;
        private System.Windows.Forms.ComboBox cbCa;
        private System.Windows.Forms.Button btnDongY;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.RadioButton radRaa;
        private System.Windows.Forms.RadioButton radVao;
        private System.Windows.Forms.Label lbKieuGio;
	}
}