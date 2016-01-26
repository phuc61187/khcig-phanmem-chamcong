namespace ChamCong_v04.UI.KhaiBao {
	partial class frm_KBVang_Nhanh {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_KBVang_Nhanh));
			this.cbSoBuoi = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cbLoaiVang = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnThoat = new System.Windows.Forms.Button();
			this.btnThem = new System.Windows.Forms.Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
			this.SuspendLayout();
			// 
			// cbSoBuoi
			// 
			this.cbSoBuoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSoBuoi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.cbSoBuoi.FormattingEnabled = true;
			this.cbSoBuoi.Items.AddRange(new object[] {
            "0.5",
            "1",
            "2"});
			this.cbSoBuoi.Location = new System.Drawing.Point(97, 39);
			this.cbSoBuoi.Name = "cbSoBuoi";
			this.cbSoBuoi.Size = new System.Drawing.Size(75, 23);
			this.cbSoBuoi.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(12, 14);
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
			this.cbLoaiVang.Location = new System.Drawing.Point(97, 11);
			this.cbLoaiVang.Name = "cbLoaiVang";
			this.cbLoaiVang.Size = new System.Drawing.Size(239, 23);
			this.cbLoaiVang.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(12, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82, 15);
			this.label2.TabIndex = 34;
			this.label2.Text = "Số ngày vắng";
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(261, 67);
			this.btnThoat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(75, 25);
			this.btnThoat.TabIndex = 3;
			this.btnThoat.Text = "Đóng";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnThem
			// 
			this.btnThem.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnThem.ForeColor = System.Drawing.Color.Blue;
			this.btnThem.Location = new System.Drawing.Point(97, 67);
			this.btnThem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnThem.Name = "btnThem";
			this.btnThem.Size = new System.Drawing.Size(75, 25);
			this.btnThem.TabIndex = 2;
			this.btnThem.Text = "Thêm";
			this.btnThem.UseVisualStyleBackColor = true;
			this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Enabled = false;
			this.richTextBox1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.richTextBox1.Location = new System.Drawing.Point(15, 97);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(321, 221);
			this.richTextBox1.TabIndex = 39;
			this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(202, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 15);
			this.label1.TabIndex = 34;
			this.label1.Text = "Phụ cấp";
			// 
			// maskedTextBox1
			// 
			this.maskedTextBox1.BeepOnError = true;
			this.maskedTextBox1.Location = new System.Drawing.Point(261, 39);
			this.maskedTextBox1.Mask = "0.00";
			this.maskedTextBox1.Name = "maskedTextBox1";
			this.maskedTextBox1.RejectInputOnFirstFailure = true;
			this.maskedTextBox1.Size = new System.Drawing.Size(75, 21);
			this.maskedTextBox1.SkipLiterals = false;
			this.maskedTextBox1.TabIndex = 40;
			this.maskedTextBox1.Text = "000";
			this.maskedTextBox1.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
			// 
			// frm_KBVang_Nhanh
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(350, 328);
			this.Controls.Add(this.maskedTextBox1);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnThem);
			this.Controls.Add(this.cbSoBuoi);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cbLoaiVang);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_KBVang_Nhanh";
			this.Text = "Khai báo vắng trong ngày";
			this.Load += new System.EventHandler(this.frm_KBVang_Nhanh_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cbSoBuoi;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbLoaiVang;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.Button btnThem;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.MaskedTextBox maskedTextBox1;
	}
}