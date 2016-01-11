namespace HTQLTTKH {
	partial class MainForm {
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.chamCongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.diemDanhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nhanVienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tinhLuongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.taiKhoanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.heThongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.thoatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 9F);
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chamCongToolStripMenuItem,
            this.diemDanhToolStripMenuItem,
            this.nhanVienToolStripMenuItem,
            this.tinhLuongToolStripMenuItem,
            this.taiKhoanToolStripMenuItem,
            this.heThongToolStripMenuItem,
            this.thoatToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(1174, 44);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// chamCongToolStripMenuItem
			// 
			this.chamCongToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.chamCongToolStripMenuItem.Image = global::HTQLTTKH.Properties.Resources.iconChamCong;
			this.chamCongToolStripMenuItem.Name = "chamCongToolStripMenuItem";
			this.chamCongToolStripMenuItem.Size = new System.Drawing.Size(123, 40);
			this.chamCongToolStripMenuItem.Text = "Chấm công";
			// 
			// diemDanhToolStripMenuItem
			// 
			this.diemDanhToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.diemDanhToolStripMenuItem.Image = global::HTQLTTKH.Properties.Resources.iconQLNhanVien;
			this.diemDanhToolStripMenuItem.Name = "diemDanhToolStripMenuItem";
			this.diemDanhToolStripMenuItem.Size = new System.Drawing.Size(120, 40);
			this.diemDanhToolStripMenuItem.Text = "Điểm danh";
			// 
			// nhanVienToolStripMenuItem
			// 
			this.nhanVienToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.nhanVienToolStripMenuItem.Image = global::HTQLTTKH.Properties.Resources.person;
			this.nhanVienToolStripMenuItem.Name = "nhanVienToolStripMenuItem";
			this.nhanVienToolStripMenuItem.Size = new System.Drawing.Size(115, 40);
			this.nhanVienToolStripMenuItem.Text = "Nhân viên";
			// 
			// tinhLuongToolStripMenuItem
			// 
			this.tinhLuongToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.tinhLuongToolStripMenuItem.Image = global::HTQLTTKH.Properties.Resources.money;
			this.tinhLuongToolStripMenuItem.Name = "tinhLuongToolStripMenuItem";
			this.tinhLuongToolStripMenuItem.Size = new System.Drawing.Size(121, 40);
			this.tinhLuongToolStripMenuItem.Text = "Tính lương";
			// 
			// taiKhoanToolStripMenuItem
			// 
			this.taiKhoanToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.taiKhoanToolStripMenuItem.Image = global::HTQLTTKH.Properties.Resources.account;
			this.taiKhoanToolStripMenuItem.Name = "taiKhoanToolStripMenuItem";
			this.taiKhoanToolStripMenuItem.Size = new System.Drawing.Size(114, 40);
			this.taiKhoanToolStripMenuItem.Text = "Tài khoản";
			// 
			// heThongToolStripMenuItem
			// 
			this.heThongToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.heThongToolStripMenuItem.Image = global::HTQLTTKH.Properties.Resources.systemconfig;
			this.heThongToolStripMenuItem.Name = "heThongToolStripMenuItem";
			this.heThongToolStripMenuItem.Size = new System.Drawing.Size(113, 40);
			this.heThongToolStripMenuItem.Text = "Hệ thống";
			// 
			// thoatToolStripMenuItem
			// 
			this.thoatToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.thoatToolStripMenuItem.Image = global::HTQLTTKH.Properties.Resources.Exit;
			this.thoatToolStripMenuItem.Name = "thoatToolStripMenuItem";
			this.thoatToolStripMenuItem.Size = new System.Drawing.Size(91, 40);
			this.thoatToolStripMenuItem.Text = "Thoát";
			this.thoatToolStripMenuItem.Click += new System.EventHandler(this.thoatToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1174, 723);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Tahoma", 7.8F);
			this.IsMdiContainer = true;
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem chamCongToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem diemDanhToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem nhanVienToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tinhLuongToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem taiKhoanToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem heThongToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem thoatToolStripMenuItem;

	}
}