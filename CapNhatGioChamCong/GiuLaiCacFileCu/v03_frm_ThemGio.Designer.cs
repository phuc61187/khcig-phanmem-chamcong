namespace GiuLaiCacFileCu {
    partial class v03_frm_ThemGio {
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
			this.cbCa_Vao = new System.Windows.Forms.ComboBox();
			this.dtpGio_Vao = new System.Windows.Forms.DateTimePicker();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.checkNgay_Vao = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.dtpNgay_Vao = new System.Windows.Forms.DateTimePicker();
			this.btnThemGio_Vao = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.tbGhiChu_Vao = new System.Windows.Forms.TextBox();
			this.cbLyDo_Vao = new System.Windows.Forms.ComboBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tbGhiChu_Raa = new System.Windows.Forms.TextBox();
			this.cbLyDo_Raa = new System.Windows.Forms.ComboBox();
			this.btnThemGio_Raa = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.cbCa_Raa = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkNgay_Raa = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.dtpGio_Raa = new System.Windows.Forms.DateTimePicker();
			this.dtpNgay_Raa = new System.Windows.Forms.DateTimePicker();
			this.groupBox3.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbCa_Vao
			// 
			this.cbCa_Vao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCa_Vao.Font = new System.Drawing.Font("Arial", 9.75F);
			this.cbCa_Vao.FormattingEnabled = true;
			this.cbCa_Vao.Location = new System.Drawing.Point(128, 26);
			this.cbCa_Vao.Name = "cbCa_Vao";
			this.cbCa_Vao.Size = new System.Drawing.Size(174, 24);
			this.cbCa_Vao.TabIndex = 0;
			this.cbCa_Vao.SelectionChangeCommitted += new System.EventHandler(this.cbCa_Vao_SelectionChangeCommitted);
			// 
			// dtpGio_Vao
			// 
			this.dtpGio_Vao.CustomFormat = "H:mm";
			this.dtpGio_Vao.Font = new System.Drawing.Font("Arial", 9.75F);
			this.dtpGio_Vao.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpGio_Vao.Location = new System.Drawing.Point(122, 31);
			this.dtpGio_Vao.Name = "dtpGio_Vao";
			this.dtpGio_Vao.ShowUpDown = true;
			this.dtpGio_Vao.Size = new System.Drawing.Size(174, 22);
			this.dtpGio_Vao.TabIndex = 4;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.checkNgay_Vao);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.dtpGio_Vao);
			this.groupBox3.Controls.Add(this.dtpNgay_Vao);
			this.groupBox3.Font = new System.Drawing.Font("Arial", 9.75F);
			this.groupBox3.Location = new System.Drawing.Point(6, 56);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(301, 90);
			this.groupBox3.TabIndex = 5;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Thời gian";
			// 
			// checkNgay_Vao
			// 
			this.checkNgay_Vao.AutoSize = true;
			this.checkNgay_Vao.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.checkNgay_Vao.ForeColor = System.Drawing.Color.DarkRed;
			this.checkNgay_Vao.Location = new System.Drawing.Point(6, 59);
			this.checkNgay_Vao.Name = "checkNgay_Vao";
			this.checkNgay_Vao.Size = new System.Drawing.Size(110, 20);
			this.checkNgay_Vao.TabIndex = 6;
			this.checkNgay_Vao.Text = "Ngày cố định";
			this.checkNgay_Vao.UseVisualStyleBackColor = true;
			this.checkNgay_Vao.CheckedChanged += new System.EventHandler(this.checkNgayVao_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(58, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Giờ vào";
			// 
			// dtpNgay_Vao
			// 
			this.dtpNgay_Vao.CustomFormat = "d/M/yyyy dddd";
			this.dtpNgay_Vao.Enabled = false;
			this.dtpNgay_Vao.Font = new System.Drawing.Font("Arial", 9.75F);
			this.dtpNgay_Vao.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgay_Vao.Location = new System.Drawing.Point(122, 59);
			this.dtpNgay_Vao.Name = "dtpNgay_Vao";
			this.dtpNgay_Vao.ShowUpDown = true;
			this.dtpNgay_Vao.Size = new System.Drawing.Size(174, 22);
			this.dtpNgay_Vao.TabIndex = 4;
			// 
			// btnThemGio_Vao
			// 
			this.btnThemGio_Vao.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.btnThemGio_Vao.ForeColor = System.Drawing.Color.Blue;
			this.btnThemGio_Vao.Location = new System.Drawing.Point(128, 278);
			this.btnThemGio_Vao.Name = "btnThemGio_Vao";
			this.btnThemGio_Vao.Size = new System.Drawing.Size(83, 27);
			this.btnThemGio_Vao.TabIndex = 6;
			this.btnThemGio_Vao.Text = "Thực hiện";
			this.btnThemGio_Vao.UseVisualStyleBackColor = true;
			this.btnThemGio_Vao.Click += new System.EventHandler(this.btnThemGio_Vao_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(556, 332);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(81, 27);
			this.btnThoat.TabIndex = 6;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// tbGhiChu_Vao
			// 
			this.tbGhiChu_Vao.Font = new System.Drawing.Font("Arial", 9.75F);
			this.tbGhiChu_Vao.Location = new System.Drawing.Point(128, 182);
			this.tbGhiChu_Vao.Multiline = true;
			this.tbGhiChu_Vao.Name = "tbGhiChu_Vao";
			this.tbGhiChu_Vao.Size = new System.Drawing.Size(174, 90);
			this.tbGhiChu_Vao.TabIndex = 0;
			// 
			// cbLyDo_Vao
			// 
			this.cbLyDo_Vao.Font = new System.Drawing.Font("Arial", 9.75F);
			this.cbLyDo_Vao.FormattingEnabled = true;
			this.cbLyDo_Vao.Items.AddRange(new object[] {
            "Lý do khác",
            "Thiếu giờ chấm công"});
			this.cbLyDo_Vao.Location = new System.Drawing.Point(128, 152);
			this.cbLyDo_Vao.Name = "cbLyDo_Vao";
			this.cbLyDo_Vao.Size = new System.Drawing.Size(174, 24);
			this.cbLyDo_Vao.TabIndex = 0;
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.tbGhiChu_Vao);
			this.groupBox6.Controls.Add(this.cbLyDo_Vao);
			this.groupBox6.Controls.Add(this.btnThemGio_Vao);
			this.groupBox6.Controls.Add(this.label2);
			this.groupBox6.Controls.Add(this.label4);
			this.groupBox6.Controls.Add(this.label3);
			this.groupBox6.Controls.Add(this.cbCa_Vao);
			this.groupBox6.Controls.Add(this.groupBox3);
			this.groupBox6.Location = new System.Drawing.Point(12, 12);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.groupBox6.Size = new System.Drawing.Size(317, 314);
			this.groupBox6.TabIndex = 8;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Thêm giờ VÀO";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(30, 29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Chọn theo ca";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.label4.Location = new System.Drawing.Point(65, 185);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(57, 16);
			this.label4.TabIndex = 5;
			this.label4.Text = "Ghi chú";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.label3.Location = new System.Drawing.Point(79, 155);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Lý do";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.tbGhiChu_Raa);
			this.groupBox1.Controls.Add(this.cbLyDo_Raa);
			this.groupBox1.Controls.Add(this.btnThemGio_Raa);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.cbCa_Raa);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Location = new System.Drawing.Point(335, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.groupBox1.Size = new System.Drawing.Size(317, 314);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Thêm giờ RA";
			// 
			// tbGhiChu_Raa
			// 
			this.tbGhiChu_Raa.Font = new System.Drawing.Font("Arial", 9.75F);
			this.tbGhiChu_Raa.Location = new System.Drawing.Point(128, 182);
			this.tbGhiChu_Raa.Multiline = true;
			this.tbGhiChu_Raa.Name = "tbGhiChu_Raa";
			this.tbGhiChu_Raa.Size = new System.Drawing.Size(174, 90);
			this.tbGhiChu_Raa.TabIndex = 0;
			// 
			// cbLyDo_Raa
			// 
			this.cbLyDo_Raa.Font = new System.Drawing.Font("Arial", 9.75F);
			this.cbLyDo_Raa.FormattingEnabled = true;
			this.cbLyDo_Raa.Items.AddRange(new object[] {
            "Lý do khác",
            "Thiếu giờ chấm công"});
			this.cbLyDo_Raa.Location = new System.Drawing.Point(128, 152);
			this.cbLyDo_Raa.Name = "cbLyDo_Raa";
			this.cbLyDo_Raa.Size = new System.Drawing.Size(174, 24);
			this.cbLyDo_Raa.TabIndex = 0;
			// 
			// btnThemGio_Raa
			// 
			this.btnThemGio_Raa.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.btnThemGio_Raa.ForeColor = System.Drawing.Color.Blue;
			this.btnThemGio_Raa.Location = new System.Drawing.Point(128, 278);
			this.btnThemGio_Raa.Name = "btnThemGio_Raa";
			this.btnThemGio_Raa.Size = new System.Drawing.Size(81, 27);
			this.btnThemGio_Raa.TabIndex = 6;
			this.btnThemGio_Raa.Text = "Thực hiện";
			this.btnThemGio_Raa.UseVisualStyleBackColor = true;
			this.btnThemGio_Raa.Click += new System.EventHandler(this.btnThemGio_Raa_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.label5.Location = new System.Drawing.Point(30, 29);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(92, 16);
			this.label5.TabIndex = 6;
			this.label5.Text = "Chọn theo ca";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.label6.Location = new System.Drawing.Point(65, 185);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(57, 16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Ghi chú";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.label7.Location = new System.Drawing.Point(79, 155);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(43, 16);
			this.label7.TabIndex = 5;
			this.label7.Text = "Lý do";
			// 
			// cbCa_Raa
			// 
			this.cbCa_Raa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCa_Raa.Font = new System.Drawing.Font("Arial", 9.75F);
			this.cbCa_Raa.FormattingEnabled = true;
			this.cbCa_Raa.Location = new System.Drawing.Point(128, 26);
			this.cbCa_Raa.Name = "cbCa_Raa";
			this.cbCa_Raa.Size = new System.Drawing.Size(174, 24);
			this.cbCa_Raa.TabIndex = 0;
			this.cbCa_Raa.SelectionChangeCommitted += new System.EventHandler(this.cbCa_Vao_SelectionChangeCommitted);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkNgay_Raa);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.dtpGio_Raa);
			this.groupBox2.Controls.Add(this.dtpNgay_Raa);
			this.groupBox2.Font = new System.Drawing.Font("Arial", 9.75F);
			this.groupBox2.Location = new System.Drawing.Point(6, 56);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(301, 90);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Thời gian";
			// 
			// checkNgay_Raa
			// 
			this.checkNgay_Raa.AutoSize = true;
			this.checkNgay_Raa.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.checkNgay_Raa.ForeColor = System.Drawing.Color.DarkRed;
			this.checkNgay_Raa.Location = new System.Drawing.Point(6, 59);
			this.checkNgay_Raa.Name = "checkNgay_Raa";
			this.checkNgay_Raa.Size = new System.Drawing.Size(110, 20);
			this.checkNgay_Raa.TabIndex = 6;
			this.checkNgay_Raa.Text = "Ngày cố định";
			this.checkNgay_Raa.UseVisualStyleBackColor = true;
			this.checkNgay_Raa.CheckedChanged += new System.EventHandler(this.checkNgay_Raa_CheckedChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.label8.Location = new System.Drawing.Point(68, 36);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48, 16);
			this.label8.TabIndex = 5;
			this.label8.Text = "Giờ ra";
			// 
			// dtpGio_Raa
			// 
			this.dtpGio_Raa.CustomFormat = "H:mm";
			this.dtpGio_Raa.Font = new System.Drawing.Font("Arial", 9.75F);
			this.dtpGio_Raa.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpGio_Raa.Location = new System.Drawing.Point(122, 31);
			this.dtpGio_Raa.Name = "dtpGio_Raa";
			this.dtpGio_Raa.ShowUpDown = true;
			this.dtpGio_Raa.Size = new System.Drawing.Size(174, 22);
			this.dtpGio_Raa.TabIndex = 4;
			// 
			// dtpNgay_Raa
			// 
			this.dtpNgay_Raa.CustomFormat = "d/M/yyyy dddd";
			this.dtpNgay_Raa.Enabled = false;
			this.dtpNgay_Raa.Font = new System.Drawing.Font("Arial", 9.75F);
			this.dtpNgay_Raa.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgay_Raa.Location = new System.Drawing.Point(122, 59);
			this.dtpNgay_Raa.Name = "dtpNgay_Raa";
			this.dtpNgay_Raa.ShowUpDown = true;
			this.dtpNgay_Raa.Size = new System.Drawing.Size(174, 22);
			this.dtpNgay_Raa.TabIndex = 4;
			// 
			// v03_frm_ThemGio
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(664, 369);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.btnThoat);
			this.Font = new System.Drawing.Font("Arial", 9.75F);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "v03_frm_ThemGio";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Thông tin thêm giờ";
			this.Load += new System.EventHandler(this.frm_ThemGio2_Load);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ComboBox cbCa_Vao;
		private System.Windows.Forms.DateTimePicker dtpGio_Vao;
        private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker dtpNgay_Vao;
		private System.Windows.Forms.Button btnThemGio_Vao;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.TextBox tbGhiChu_Vao;
		private System.Windows.Forms.ComboBox cbLyDo_Vao;
        private System.Windows.Forms.CheckBox checkNgay_Vao;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbGhiChu_Raa;
        private System.Windows.Forms.ComboBox cbLyDo_Raa;
        private System.Windows.Forms.Button btnThemGio_Raa;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbCa_Raa;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkNgay_Raa;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpGio_Raa;
        private System.Windows.Forms.DateTimePicker dtpNgay_Raa;
	}
}