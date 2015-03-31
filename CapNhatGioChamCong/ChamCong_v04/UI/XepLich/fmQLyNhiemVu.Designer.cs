namespace ChamCong_v04.UI.XepLich {
	partial class fmQLyNhiemVu {
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.gbDSNV = new System.Windows.Forms.GroupBox();
			this.listDSNhiemVu = new System.Windows.Forms.ListBox();
			this.btnXoaNhiemVu = new System.Windows.Forms.Button();
			this.btnHuy = new System.Windows.Forms.Button();
			this.btnLuuNhiemVu = new System.Windows.Forms.Button();
			this.btnTaoNhiemVu = new System.Windows.Forms.Button();
			this.tbTenNhiemVu = new System.Windows.Forms.TextBox();
			this.lbMaNhiemVu = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnThoat = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.gbDSNV.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.gbDSNV);
			this.groupBox1.Controls.Add(this.btnXoaNhiemVu);
			this.groupBox1.Controls.Add(this.btnHuy);
			this.groupBox1.Controls.Add(this.btnLuuNhiemVu);
			this.groupBox1.Controls.Add(this.btnTaoNhiemVu);
			this.groupBox1.Controls.Add(this.tbTenNhiemVu);
			this.groupBox1.Controls.Add(this.lbMaNhiemVu);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(316, 307);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// gbDSNV
			// 
			this.gbDSNV.Controls.Add(this.listDSNhiemVu);
			this.gbDSNV.Location = new System.Drawing.Point(6, 113);
			this.gbDSNV.Name = "gbDSNV";
			this.gbDSNV.Size = new System.Drawing.Size(304, 192);
			this.gbDSNV.TabIndex = 47;
			this.gbDSNV.TabStop = false;
			this.gbDSNV.Text = "Danh sách nhiệm vụ";
			// 
			// listDSNhiemVu
			// 
			this.listDSNhiemVu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.listDSNhiemVu.FormattingEnabled = true;
			this.listDSNhiemVu.ItemHeight = 15;
			this.listDSNhiemVu.Location = new System.Drawing.Point(0, 20);
			this.listDSNhiemVu.Name = "listDSNhiemVu";
			this.listDSNhiemVu.Size = new System.Drawing.Size(304, 169);
			this.listDSNhiemVu.TabIndex = 0;
			// 
			// btnXoaNhiemVu
			// 
			this.btnXoaNhiemVu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnXoaNhiemVu.ForeColor = System.Drawing.Color.Red;
			this.btnXoaNhiemVu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnXoaNhiemVu.Location = new System.Drawing.Point(241, 80);
			this.btnXoaNhiemVu.Name = "btnXoaNhiemVu";
			this.btnXoaNhiemVu.Size = new System.Drawing.Size(69, 27);
			this.btnXoaNhiemVu.TabIndex = 45;
			this.btnXoaNhiemVu.Text = "Xoá";
			this.btnXoaNhiemVu.UseVisualStyleBackColor = true;
			this.btnXoaNhiemVu.Click += new System.EventHandler(this.btnXoaNhiemVu_Click);
			// 
			// btnHuy
			// 
			this.btnHuy.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnHuy.ForeColor = System.Drawing.Color.Blue;
			this.btnHuy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnHuy.Location = new System.Drawing.Point(166, 80);
			this.btnHuy.Name = "btnHuy";
			this.btnHuy.Size = new System.Drawing.Size(69, 27);
			this.btnHuy.TabIndex = 45;
			this.btnHuy.Text = "Huỷ";
			this.btnHuy.UseVisualStyleBackColor = true;
			this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
			// 
			// btnLuuNhiemVu
			// 
			this.btnLuuNhiemVu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnLuuNhiemVu.ForeColor = System.Drawing.Color.Blue;
			this.btnLuuNhiemVu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnLuuNhiemVu.Location = new System.Drawing.Point(91, 80);
			this.btnLuuNhiemVu.Name = "btnLuuNhiemVu";
			this.btnLuuNhiemVu.Size = new System.Drawing.Size(69, 27);
			this.btnLuuNhiemVu.TabIndex = 45;
			this.btnLuuNhiemVu.Text = "Lưu";
			this.btnLuuNhiemVu.UseVisualStyleBackColor = true;
			this.btnLuuNhiemVu.Click += new System.EventHandler(this.btnLuuNhiemVu_Click);
			// 
			// btnTaoNhiemVu
			// 
			this.btnTaoNhiemVu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnTaoNhiemVu.ForeColor = System.Drawing.Color.Blue;
			this.btnTaoNhiemVu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnTaoNhiemVu.Location = new System.Drawing.Point(91, 20);
			this.btnTaoNhiemVu.Name = "btnTaoNhiemVu";
			this.btnTaoNhiemVu.Size = new System.Drawing.Size(144, 27);
			this.btnTaoNhiemVu.TabIndex = 45;
			this.btnTaoNhiemVu.Text = "Tạo nhiệm vụ mới";
			this.btnTaoNhiemVu.UseVisualStyleBackColor = true;
			this.btnTaoNhiemVu.Click += new System.EventHandler(this.btnTaoNhiemVu_Click);
			// 
			// tbTenNhiemVu
			// 
			this.tbTenNhiemVu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.tbTenNhiemVu.Location = new System.Drawing.Point(91, 53);
			this.tbTenNhiemVu.Name = "tbTenNhiemVu";
			this.tbTenNhiemVu.Size = new System.Drawing.Size(219, 21);
			this.tbTenNhiemVu.TabIndex = 1;
			// 
			// lbMaNhiemVu
			// 
			this.lbMaNhiemVu.AutoSize = true;
			this.lbMaNhiemVu.Location = new System.Drawing.Point(3, 26);
			this.lbMaNhiemVu.Name = "lbMaNhiemVu";
			this.lbMaNhiemVu.Size = new System.Drawing.Size(103, 15);
			this.lbMaNhiemVu.TabIndex = 0;
			this.lbMaNhiemVu.Text = "MaNVChinh_hide";
			this.lbMaNhiemVu.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Tên nhiệm vụ";
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnThoat.Location = new System.Drawing.Point(250, 314);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(69, 27);
			this.btnThoat.TabIndex = 45;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// fmQLyNhiemVu
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(324, 347);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnThoat);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "fmQLyNhiemVu";
			this.Text = "Quản lý các nhiệm vụ";
			this.Load += new System.EventHandler(this.fmQLyNhiemVu_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.gbDSNV.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox tbTenNhiemVu;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnXoaNhiemVu;
		private System.Windows.Forms.Button btnHuy;
		private System.Windows.Forms.Button btnLuuNhiemVu;
		private System.Windows.Forms.Button btnTaoNhiemVu;
		private System.Windows.Forms.GroupBox gbDSNV;
		private System.Windows.Forms.ListBox listDSNhiemVu;
		private System.Windows.Forms.Label lbMaNhiemVu;
		private System.Windows.Forms.Button btnThoat;
	}
}