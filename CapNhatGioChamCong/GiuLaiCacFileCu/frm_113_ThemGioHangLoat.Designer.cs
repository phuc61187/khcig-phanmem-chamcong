namespace ChamCong_v03 {
	partial class frm_113_ThemGioHangLoat {
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
			this.chkGioRaa = new System.Windows.Forms.CheckBox();
			this.chkGioVao = new System.Windows.Forms.CheckBox();
			this.cbCa_Them = new System.Windows.Forms.ComboBox();
			this.dtpVao_Them = new System.Windows.Forms.DateTimePicker();
			this.label7 = new System.Windows.Forms.Label();
			this.dtpRaa_Them = new System.Windows.Forms.DateTimePicker();
			this.cbLyDo_Them = new System.Windows.Forms.ComboBox();
			this.btnThem = new System.Windows.Forms.Button();
			this.tbGhichu_Them = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// chkGioRaa
			// 
			this.chkGioRaa.AutoSize = true;
			this.chkGioRaa.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.chkGioRaa.Location = new System.Drawing.Point(9, 75);
			this.chkGioRaa.Name = "chkGioRaa";
			this.chkGioRaa.Size = new System.Drawing.Size(64, 20);
			this.chkGioRaa.TabIndex = 6;
			this.chkGioRaa.Text = "Giờ ra";
			this.chkGioRaa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip.SetToolTip(this.chkGioRaa, "Check mục này để thêm giờ ra");
			this.chkGioRaa.UseVisualStyleBackColor = true;
			// 
			// chkGioVao
			// 
			this.chkGioVao.AutoSize = true;
			this.chkGioVao.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.chkGioVao.Location = new System.Drawing.Point(9, 47);
			this.chkGioVao.Name = "chkGioVao";
			this.chkGioVao.Size = new System.Drawing.Size(72, 20);
			this.chkGioVao.TabIndex = 6;
			this.chkGioVao.Text = "Giờ vào";
			this.chkGioVao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip.SetToolTip(this.chkGioVao, "Check mục này để thêm giờ vào");
			this.chkGioVao.UseVisualStyleBackColor = true;
			// 
			// cbCa_Them
			// 
			this.cbCa_Them.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCa_Them.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.cbCa_Them.FormattingEnabled = true;
			this.cbCa_Them.Items.AddRange(new object[] {
            "Ca 1"});
			this.cbCa_Them.Location = new System.Drawing.Point(88, 13);
			this.cbCa_Them.Margin = new System.Windows.Forms.Padding(4);
			this.cbCa_Them.Name = "cbCa_Them";
			this.cbCa_Them.Size = new System.Drawing.Size(129, 24);
			this.cbCa_Them.TabIndex = 3;
			this.toolTip.SetToolTip(this.cbCa_Them, "Chọn giờ cần thêm theo giờ chuẩn vào ra ca");
			// 
			// dtpVao_Them
			// 
			this.dtpVao_Them.CustomFormat = "H:mm";
			this.dtpVao_Them.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.dtpVao_Them.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpVao_Them.Location = new System.Drawing.Point(88, 45);
			this.dtpVao_Them.Margin = new System.Windows.Forms.Padding(4);
			this.dtpVao_Them.Name = "dtpVao_Them";
			this.dtpVao_Them.ShowUpDown = true;
			this.dtpVao_Them.Size = new System.Drawing.Size(129, 22);
			this.dtpVao_Them.TabIndex = 5;
			this.dtpVao_Them.Value = new System.DateTime(2014, 6, 7, 0, 0, 0, 0);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label7.Location = new System.Drawing.Point(27, 16);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(54, 16);
			this.label7.TabIndex = 2;
			this.label7.Text = "Theo ca";
			this.toolTip.SetToolTip(this.label7, "Chọn giờ cần thêm theo giờ chuẩn vào ra ca");
			// 
			// dtpRaa_Them
			// 
			this.dtpRaa_Them.CustomFormat = "H:mm";
			this.dtpRaa_Them.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.dtpRaa_Them.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpRaa_Them.Location = new System.Drawing.Point(88, 75);
			this.dtpRaa_Them.Margin = new System.Windows.Forms.Padding(4);
			this.dtpRaa_Them.Name = "dtpRaa_Them";
			this.dtpRaa_Them.ShowUpDown = true;
			this.dtpRaa_Them.Size = new System.Drawing.Size(129, 22);
			this.dtpRaa_Them.TabIndex = 5;
			this.dtpRaa_Them.Value = new System.DateTime(2014, 6, 7, 0, 0, 0, 0);
			// 
			// cbLyDo_Them
			// 
			this.cbLyDo_Them.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.cbLyDo_Them.FormattingEnabled = true;
			this.cbLyDo_Them.Items.AddRange(new object[] {
            "Quên chấm công",
            "Lý do khác"});
			this.cbLyDo_Them.Location = new System.Drawing.Point(88, 105);
			this.cbLyDo_Them.Margin = new System.Windows.Forms.Padding(4);
			this.cbLyDo_Them.Name = "cbLyDo_Them";
			this.cbLyDo_Them.Size = new System.Drawing.Size(129, 24);
			this.cbLyDo_Them.TabIndex = 3;
			this.cbLyDo_Them.Text = "Quên chấm công";
			// 
			// btnThem
			// 
			this.btnThem.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnThem.Location = new System.Drawing.Point(88, 185);
			this.btnThem.Margin = new System.Windows.Forms.Padding(4);
			this.btnThem.Name = "btnThem";
			this.btnThem.Size = new System.Drawing.Size(129, 27);
			this.btnThem.TabIndex = 14;
			this.btnThem.Text = "Thêm";
			this.btnThem.UseVisualStyleBackColor = true;
			// 
			// tbGhichu_Them
			// 
			this.tbGhichu_Them.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.tbGhichu_Them.Location = new System.Drawing.Point(88, 137);
			this.tbGhichu_Them.Margin = new System.Windows.Forms.Padding(4);
			this.tbGhichu_Them.Multiline = true;
			this.tbGhichu_Them.Name = "tbGhichu_Them";
			this.tbGhichu_Them.Size = new System.Drawing.Size(129, 40);
			this.tbGhichu_Them.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label4.Location = new System.Drawing.Point(28, 140);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 16);
			this.label4.TabIndex = 2;
			this.label4.Text = "Ghi chú";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label1.Location = new System.Drawing.Point(41, 108);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Lý do";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.button1.Location = new System.Drawing.Point(88, 220);
			this.button1.Margin = new System.Windows.Forms.Padding(4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(129, 27);
			this.button1.TabIndex = 14;
			this.button1.Text = "Thoát";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// frm_113_ThemGioHangLoat
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(231, 261);
			this.ControlBox = false;
			this.Controls.Add(this.cbLyDo_Them);
			this.Controls.Add(this.cbCa_Them);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnThem);
			this.Controls.Add(this.dtpRaa_Them);
			this.Controls.Add(this.dtpVao_Them);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.chkGioRaa);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbGhichu_Them);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.chkGioVao);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_113_ThemGioHangLoat";
			this.Text = "Thông tin thêm giờ hàng loạt";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cbCa_Them;
		private System.Windows.Forms.DateTimePicker dtpVao_Them;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DateTimePicker dtpRaa_Them;
		private System.Windows.Forms.CheckBox chkGioRaa;
		private System.Windows.Forms.CheckBox chkGioVao;
		private System.Windows.Forms.ComboBox cbLyDo_Them;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbGhichu_Them;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnThem;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Button button1;
	}
}