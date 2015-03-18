namespace GiuLaiCacFileCu {
	partial class z_old_v12_frm_ThongTinSuaGio {
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
			this.label2 = new System.Windows.Forms.Label();
			this.cbLyDo = new System.Windows.Forms.ComboBox();
			this.cbCa = new System.Windows.Forms.ComboBox();
			this.gpNhapThongTin = new System.Windows.Forms.GroupBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.rdbtnRa = new System.Windows.Forms.RadioButton();
			this.rdbtnVao = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			this.tbGhichu = new System.Windows.Forms.TextBox();
			this.tbGioCu = new System.Windows.Forms.TextBox();
			this.tbTenNV = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.dateTimeGioMoi = new System.Windows.Forms.DateTimePicker();
			this.tbMaCC = new System.Windows.Forms.TextBox();
			this.btnDongY = new System.Windows.Forms.Button();
			this.btnHuyBo = new System.Windows.Forms.Button();
			this.gpNhapThongTin.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label2.Location = new System.Drawing.Point(50, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Tên nhân viên:";
			// 
			// cbLyDo
			// 
			this.cbLyDo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.cbLyDo.FormattingEnabled = true;
			this.cbLyDo.Items.AddRange(new object[] {
            "Vào ra có sự cho phép của cấp quản lý",
            "Chấm nhầm máy",
            "Lý do khác"});
			this.cbLyDo.Location = new System.Drawing.Point(148, 205);
			this.cbLyDo.Margin = new System.Windows.Forms.Padding(4);
			this.cbLyDo.Name = "cbLyDo";
			this.cbLyDo.Size = new System.Drawing.Size(200, 24);
			this.cbLyDo.TabIndex = 3;
			this.cbLyDo.Text = "Vào ra có cho phép của cấp quản lý";
			// 
			// cbCa
			// 
			this.cbCa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCa.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.cbCa.FormattingEnabled = true;
			this.cbCa.Items.AddRange(new object[] {
            "Ca 1"});
			this.cbCa.Location = new System.Drawing.Point(148, 111);
			this.cbCa.Margin = new System.Windows.Forms.Padding(4);
			this.cbCa.Name = "cbCa";
			this.cbCa.Size = new System.Drawing.Size(200, 24);
			this.cbCa.TabIndex = 3;
			// 
			// gpNhapThongTin
			// 
			this.gpNhapThongTin.Controls.Add(this.panel1);
			this.gpNhapThongTin.Controls.Add(this.label5);
			this.gpNhapThongTin.Controls.Add(this.label2);
			this.gpNhapThongTin.Controls.Add(this.cbLyDo);
			this.gpNhapThongTin.Controls.Add(this.cbCa);
			this.gpNhapThongTin.Controls.Add(this.tbGhichu);
			this.gpNhapThongTin.Controls.Add(this.tbGioCu);
			this.gpNhapThongTin.Controls.Add(this.tbTenNV);
			this.gpNhapThongTin.Controls.Add(this.label3);
			this.gpNhapThongTin.Controls.Add(this.label4);
			this.gpNhapThongTin.Controls.Add(this.label1);
			this.gpNhapThongTin.Controls.Add(this.label8);
			this.gpNhapThongTin.Controls.Add(this.label6);
			this.gpNhapThongTin.Controls.Add(this.label7);
			this.gpNhapThongTin.Controls.Add(this.dateTimeGioMoi);
			this.gpNhapThongTin.Controls.Add(this.tbMaCC);
			this.gpNhapThongTin.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gpNhapThongTin.Location = new System.Drawing.Point(12, 12);
			this.gpNhapThongTin.Name = "gpNhapThongTin";
			this.gpNhapThongTin.Size = new System.Drawing.Size(355, 337);
			this.gpNhapThongTin.TabIndex = 15;
			this.gpNhapThongTin.TabStop = false;
			this.gpNhapThongTin.Text = "Sửa giờ chấm công cho Nhân viên";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.rdbtnRa);
			this.panel1.Controls.Add(this.rdbtnVao);
			this.panel1.Location = new System.Drawing.Point(148, 175);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 23);
			this.panel1.TabIndex = 6;
			// 
			// rdbtnRa
			// 
			this.rdbtnRa.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.rdbtnRa.Location = new System.Drawing.Point(101, 0);
			this.rdbtnRa.Name = "rdbtnRa";
			this.rdbtnRa.Size = new System.Drawing.Size(60, 17);
			this.rdbtnRa.TabIndex = 0;
			this.rdbtnRa.Text = "Ra";
			this.rdbtnRa.UseVisualStyleBackColor = true;
			// 
			// rdbtnVao
			// 
			this.rdbtnVao.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.rdbtnVao.Location = new System.Drawing.Point(3, 0);
			this.rdbtnVao.Name = "rdbtnVao";
			this.rdbtnVao.Size = new System.Drawing.Size(60, 17);
			this.rdbtnVao.TabIndex = 0;
			this.rdbtnVao.Text = "Vào";
			this.rdbtnVao.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label5.Location = new System.Drawing.Point(89, 84);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(52, 16);
			this.label5.TabIndex = 2;
			this.label5.Text = "Giờ cũ:";
			// 
			// tbGhichu
			// 
			this.tbGhichu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.tbGhichu.Location = new System.Drawing.Point(148, 237);
			this.tbGhichu.Margin = new System.Windows.Forms.Padding(4);
			this.tbGhichu.Multiline = true;
			this.tbGhichu.Name = "tbGhichu";
			this.tbGhichu.Size = new System.Drawing.Size(200, 89);
			this.tbGhichu.TabIndex = 1;
			// 
			// tbGioCu
			// 
			this.tbGioCu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.tbGioCu.Location = new System.Drawing.Point(148, 81);
			this.tbGioCu.Margin = new System.Windows.Forms.Padding(4);
			this.tbGioCu.Name = "tbGioCu";
			this.tbGioCu.ReadOnly = true;
			this.tbGioCu.Size = new System.Drawing.Size(200, 22);
			this.tbGioCu.TabIndex = 1;
			this.tbGioCu.Text = "Chủ nhật 19/12/2013 21:45";
			// 
			// tbTenNV
			// 
			this.tbTenNV.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.tbTenNV.Location = new System.Drawing.Point(148, 51);
			this.tbTenNV.Margin = new System.Windows.Forms.Padding(4);
			this.tbTenNV.Name = "tbTenNV";
			this.tbTenNV.ReadOnly = true;
			this.tbTenNV.Size = new System.Drawing.Size(200, 22);
			this.tbTenNV.TabIndex = 1;
			this.tbTenNV.Text = "Huỳnh thị ngọc sương";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label3.Location = new System.Drawing.Point(43, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(98, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Mã chấm công:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label4.Location = new System.Drawing.Point(84, 240);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(57, 16);
			this.label4.TabIndex = 2;
			this.label4.Text = "Ghi chú:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label1.Location = new System.Drawing.Point(97, 208);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Lý do:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label8.Location = new System.Drawing.Point(80, 176);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(61, 16);
			this.label8.TabIndex = 2;
			this.label8.Text = "Kiểu giờ:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label6.Location = new System.Drawing.Point(80, 146);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(61, 16);
			this.label6.TabIndex = 2;
			this.label6.Text = "Giờ mới:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label7.Location = new System.Drawing.Point(6, 114);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(139, 16);
			this.label7.TabIndex = 2;
			this.label7.Text = "Nhập giờ mới theo ca:";
			// 
			// dateTimeGioMoi
			// 
			this.dateTimeGioMoi.CustomFormat = "dddd d/M/yyyy H:mm";
			this.dateTimeGioMoi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.dateTimeGioMoi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimeGioMoi.Location = new System.Drawing.Point(148, 143);
			this.dateTimeGioMoi.Margin = new System.Windows.Forms.Padding(4);
			this.dateTimeGioMoi.Name = "dateTimeGioMoi";
			this.dateTimeGioMoi.ShowUpDown = true;
			this.dateTimeGioMoi.Size = new System.Drawing.Size(200, 22);
			this.dateTimeGioMoi.TabIndex = 5;
			// 
			// tbMaCC
			// 
			this.tbMaCC.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.tbMaCC.Location = new System.Drawing.Point(148, 21);
			this.tbMaCC.Margin = new System.Windows.Forms.Padding(4);
			this.tbMaCC.Name = "tbMaCC";
			this.tbMaCC.ReadOnly = true;
			this.tbMaCC.Size = new System.Drawing.Size(85, 22);
			this.tbMaCC.TabIndex = 1;
			this.tbMaCC.Text = "01000";
			// 
			// btnDongY
			// 
			this.btnDongY.Location = new System.Drawing.Point(75, 356);
			this.btnDongY.Margin = new System.Windows.Forms.Padding(4);
			this.btnDongY.Name = "btnDongY";
			this.btnDongY.Size = new System.Drawing.Size(75, 26);
			this.btnDongY.TabIndex = 17;
			this.btnDongY.Text = "Đồng ý";
			this.btnDongY.UseVisualStyleBackColor = true;
			this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
			// 
			// btnHuyBo
			// 
			this.btnHuyBo.Location = new System.Drawing.Point(246, 356);
			this.btnHuyBo.Margin = new System.Windows.Forms.Padding(4);
			this.btnHuyBo.Name = "btnHuyBo";
			this.btnHuyBo.Size = new System.Drawing.Size(75, 26);
			this.btnHuyBo.TabIndex = 16;
			this.btnHuyBo.Text = "Hủy";
			this.btnHuyBo.UseVisualStyleBackColor = true;
			this.btnHuyBo.Click += new System.EventHandler(this.btnHuyBo_Click);
			// 
			// frm_ThongTinSuaGio
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(402, 393);
			this.Controls.Add(this.gpNhapThongTin);
			this.Controls.Add(this.btnDongY);
			this.Controls.Add(this.btnHuyBo);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "z_old_v12_frm_ThongTinSuaGio";
			this.Text = "frm_ThongTinSuaGio";
			this.Load += new System.EventHandler(this.frm_ThongTinSuaGio_Load);
			this.gpNhapThongTin.ResumeLayout(false);
			this.gpNhapThongTin.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbLyDo;
		private System.Windows.Forms.ComboBox cbCa;
		private System.Windows.Forms.GroupBox gpNhapThongTin;
		private System.Windows.Forms.TextBox tbGhichu;
		private System.Windows.Forms.TextBox tbTenNV;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DateTimePicker dateTimeGioMoi;
		private System.Windows.Forms.TextBox tbMaCC;
		private System.Windows.Forms.Button btnDongY;
		private System.Windows.Forms.Button btnHuyBo;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbGioCu;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton rdbtnRa;
		private System.Windows.Forms.RadioButton rdbtnVao;
		private System.Windows.Forms.Label label8;
	}
}