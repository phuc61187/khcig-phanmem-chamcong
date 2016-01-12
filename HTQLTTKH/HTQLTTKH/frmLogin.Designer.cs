using System;

namespace HTQLTTKH {
	partial class frmLogin {
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

		private void simpleButtonThoat_Click(object sender, EventArgs e) {
			System.Windows.Forms.Application.Exit();
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.tbTaiKhoan = new DevExpress.XtraEditors.TextEdit();
			this.label1 = new System.Windows.Forms.Label();
			this.tbPass = new DevExpress.XtraEditors.TextEdit();
			this.label2 = new System.Windows.Forms.Label();
			this.simpleButtonDangNhap = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonThoat = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.tbTaiKhoan.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbPass.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// tbTaiKhoan
			// 
			this.tbTaiKhoan.Location = new System.Drawing.Point(125, 29);
			this.tbTaiKhoan.Name = "tbTaiKhoan";
			this.tbTaiKhoan.Size = new System.Drawing.Size(266, 20);
			this.tbTaiKhoan.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(28, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Tài khoản";
			// 
			// tbPass
			// 
			this.tbPass.Location = new System.Drawing.Point(125, 55);
			this.tbPass.Name = "tbPass";
			this.tbPass.Properties.UseSystemPasswordChar = true;
			this.tbPass.Size = new System.Drawing.Size(266, 20);
			this.tbPass.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(28, 58);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Mật khẩu";
			// 
			// simpleButtonDangNhap
			// 
			this.simpleButtonDangNhap.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.simpleButtonDangNhap.Appearance.Options.UseFont = true;
			this.simpleButtonDangNhap.Image = global::HTQLTTKH.Properties.Resources.apply_32x32;
			this.simpleButtonDangNhap.Location = new System.Drawing.Point(125, 83);
			this.simpleButtonDangNhap.LookAndFeel.SkinName = "Office 2010 Blue";
			this.simpleButtonDangNhap.LookAndFeel.UseDefaultLookAndFeel = false;
			this.simpleButtonDangNhap.Name = "simpleButtonDangNhap";
			this.simpleButtonDangNhap.Size = new System.Drawing.Size(130, 32);
			this.simpleButtonDangNhap.TabIndex = 2;
			this.simpleButtonDangNhap.Text = "Đăng nhập";
			this.simpleButtonDangNhap.Click += new System.EventHandler(this.simpleButtonDangNhap_Click);
			// 
			// simpleButtonThoat
			// 
			this.simpleButtonThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.simpleButtonThoat.Appearance.Options.UseFont = true;
			this.simpleButtonThoat.Image = global::HTQLTTKH.Properties.Resources.close_32x32;
			this.simpleButtonThoat.Location = new System.Drawing.Point(261, 83);
			this.simpleButtonThoat.LookAndFeel.SkinName = "Office 2010 Blue";
			this.simpleButtonThoat.LookAndFeel.UseDefaultLookAndFeel = false;
			this.simpleButtonThoat.Name = "simpleButtonThoat";
			this.simpleButtonThoat.Size = new System.Drawing.Size(130, 32);
			this.simpleButtonThoat.TabIndex = 4;
			this.simpleButtonThoat.Text = "Thoát";
			this.simpleButtonThoat.Click += new System.EventHandler(this.simpleButtonThoat_Click);
			// 
			// frmLogin
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.ClientSize = new System.Drawing.Size(427, 152);
			this.Controls.Add(this.simpleButtonThoat);
			this.Controls.Add(this.simpleButtonDangNhap);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbPass);
			this.Controls.Add(this.tbTaiKhoan);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "frmLogin";
			this.Text = "Đăng nhập";
			this.Load += new System.EventHandler(this.frmLogin_Load);
			((System.ComponentModel.ISupportInitialize)(this.tbTaiKhoan.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbPass.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.TextEdit tbTaiKhoan;
		private System.Windows.Forms.Label label1;
		private DevExpress.XtraEditors.TextEdit tbPass;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraEditors.SimpleButton simpleButtonDangNhap;
		private DevExpress.XtraEditors.SimpleButton simpleButtonThoat;
	}
}