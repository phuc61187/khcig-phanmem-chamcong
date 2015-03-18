namespace ChamCong_v04.UI.KhaiBao {
	partial class frmKBVang_DaiHan {
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
			this.label1 = new System.Windows.Forms.Label();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.btnThem = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.cbLoaiVang = new System.Windows.Forms.ComboBox();
			this.dtpNgayKT = new System.Windows.Forms.DateTimePicker();
			this.dtpNgayBD = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.btnDong = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(102, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(0, 15);
			this.label1.TabIndex = 0;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Enabled = false;
			this.richTextBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.richTextBox1.Location = new System.Drawing.Point(12, 12);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(332, 91);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "Theo quy định:\n- Với các trường hợp nghỉ BHXH do bệnh dài ngày, nghỉ việc riêng R" +
    "O dài hạn, nghỉ thai sản : trong khoảng thời gian này sẽ không được hưởng công L" +
    "ễ, tết Nhà máy chi trả";
			// 
			// btnThem
			// 
			this.btnThem.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnThem.ForeColor = System.Drawing.Color.Blue;
			this.btnThem.Location = new System.Drawing.Point(105, 185);
			this.btnThem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnThem.Name = "btnThem";
			this.btnThem.Size = new System.Drawing.Size(75, 25);
			this.btnThem.TabIndex = 3;
			this.btnThem.Text = "Thêm";
			this.btnThem.UseVisualStyleBackColor = true;
			this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(10, 161);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 15);
			this.label3.TabIndex = 33;
			this.label3.Text = "Loại vắng";
			// 
			// cbLoaiVang
			// 
			this.cbLoaiVang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbLoaiVang.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.cbLoaiVang.FormattingEnabled = true;
			this.cbLoaiVang.Location = new System.Drawing.Point(105, 158);
			this.cbLoaiVang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cbLoaiVang.Name = "cbLoaiVang";
			this.cbLoaiVang.Size = new System.Drawing.Size(239, 23);
			this.cbLoaiVang.TabIndex = 2;
			// 
			// dtpNgayKT
			// 
			this.dtpNgayKT.CustomFormat = "dddd d/M/yyyy";
			this.dtpNgayKT.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.dtpNgayKT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgayKT.Location = new System.Drawing.Point(105, 133);
			this.dtpNgayKT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dtpNgayKT.Name = "dtpNgayKT";
			this.dtpNgayKT.ShowUpDown = true;
			this.dtpNgayKT.Size = new System.Drawing.Size(239, 21);
			this.dtpNgayKT.TabIndex = 1;
			this.dtpNgayKT.Value = new System.DateTime(2014, 2, 1, 19, 11, 0, 0);
			// 
			// dtpNgayBD
			// 
			this.dtpNgayBD.CustomFormat = "dddd d/M/yyyy";
			this.dtpNgayBD.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.dtpNgayBD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgayBD.Location = new System.Drawing.Point(105, 108);
			this.dtpNgayBD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dtpNgayBD.Name = "dtpNgayBD";
			this.dtpNgayBD.ShowUpDown = true;
			this.dtpNgayBD.Size = new System.Drawing.Size(239, 21);
			this.dtpNgayBD.TabIndex = 0;
			this.dtpNgayBD.Value = new System.DateTime(2014, 2, 1, 19, 11, 0, 0);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label4.Location = new System.Drawing.Point(10, 136);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(81, 15);
			this.label4.TabIndex = 40;
			this.label4.Text = "Đến hết ngày";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label5.Location = new System.Drawing.Point(11, 111);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(82, 15);
			this.label5.TabIndex = 40;
			this.label5.Text = "Chọn từ ngày";
			// 
			// btnDong
			// 
			this.btnDong.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnDong.ForeColor = System.Drawing.Color.Blue;
			this.btnDong.Location = new System.Drawing.Point(269, 185);
			this.btnDong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnDong.Name = "btnDong";
			this.btnDong.Size = new System.Drawing.Size(75, 25);
			this.btnDong.TabIndex = 4;
			this.btnDong.Text = "Đóng";
			this.btnDong.UseVisualStyleBackColor = true;
			this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
			// 
			// frmKBVang_DaiHan
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(356, 221);
			this.Controls.Add(this.dtpNgayKT);
			this.Controls.Add(this.dtpNgayBD);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnDong);
			this.Controls.Add(this.btnThem);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cbLoaiVang);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "frmKBVang_DaiHan";
			this.Text = "Khai báo vắng dài hạn";
			this.Load += new System.EventHandler(this.frmKBVang_DaiHan_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button btnThem;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbLoaiVang;
		private System.Windows.Forms.DateTimePicker dtpNgayKT;
		private System.Windows.Forms.DateTimePicker dtpNgayBD;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnDong;
	}
}