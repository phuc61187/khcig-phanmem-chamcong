namespace ChamCong_v06.UI {
	partial class frmLogIn {
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
			this.label2 = new System.Windows.Forms.Label();
			this.btnDangnhap = new DevExpress.XtraEditors.SimpleButton();
			this.btnEditMatkhau = new DevExpress.XtraEditors.ButtonEdit();
			this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
			this.btnKetnoiCSDL = new DevExpress.XtraEditors.SimpleButton();
			this.btnEditTaikhoan = new DevExpress.XtraEditors.ButtonEdit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditMatkhau.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditTaikhoan.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label1.Location = new System.Drawing.Point(13, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 14);
			this.label1.TabIndex = 0;
			this.label1.Text = "Tài khoản";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label2.Location = new System.Drawing.Point(14, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 14);
			this.label2.TabIndex = 0;
			this.label2.Text = "Mật khẩu";
			// 
			// btnDangnhap
			// 
			this.btnDangnhap.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnDangnhap.Appearance.Options.UseFont = true;
			this.btnDangnhap.Location = new System.Drawing.Point(88, 64);
			this.btnDangnhap.Name = "btnDangnhap";
			this.btnDangnhap.Size = new System.Drawing.Size(100, 28);
			this.btnDangnhap.TabIndex = 2;
			this.btnDangnhap.Text = "Đăng nhập";
			this.btnDangnhap.Click += new System.EventHandler(this.btnDangnhap_Click);
			// 
			// btnEditMatkhau
			// 
			this.btnEditMatkhau.EditValue = "";
			this.btnEditMatkhau.Location = new System.Drawing.Point(88, 38);
			this.btnEditMatkhau.Name = "btnEditMatkhau";
			this.btnEditMatkhau.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnEditMatkhau.Properties.Appearance.Options.UseFont = true;
			this.btnEditMatkhau.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
			this.btnEditMatkhau.Properties.UseSystemPasswordChar = true;
			this.btnEditMatkhau.Properties.Click += new System.EventHandler(this.btnEdit_Clear_Click);
			this.btnEditMatkhau.Size = new System.Drawing.Size(206, 20);
			this.btnEditMatkhau.TabIndex = 1;
			// 
			// btnThoat
			// 
			this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.Appearance.Options.UseFont = true;
			this.btnThoat.Location = new System.Drawing.Point(194, 64);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(100, 28);
			this.btnThoat.TabIndex = 3;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnKetnoiCSDL
			// 
			this.btnKetnoiCSDL.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnKetnoiCSDL.Appearance.Options.UseFont = true;
			this.btnKetnoiCSDL.Location = new System.Drawing.Point(88, 98);
			this.btnKetnoiCSDL.Name = "btnKetnoiCSDL";
			this.btnKetnoiCSDL.Size = new System.Drawing.Size(206, 28);
			this.btnKetnoiCSDL.TabIndex = 4;
			this.btnKetnoiCSDL.Text = "Kết nối CSDL";
			this.btnKetnoiCSDL.Click += new System.EventHandler(this.btnKetnoiCSDL_Click);
			// 
			// btnEditTaikhoan
			// 
			this.btnEditTaikhoan.EditValue = "";
			this.btnEditTaikhoan.Location = new System.Drawing.Point(88, 12);
			this.btnEditTaikhoan.Name = "btnEditTaikhoan";
			this.btnEditTaikhoan.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnEditTaikhoan.Properties.Appearance.Options.UseFont = true;
			this.btnEditTaikhoan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
			this.btnEditTaikhoan.Size = new System.Drawing.Size(206, 20);
			this.btnEditTaikhoan.TabIndex = 0;
			this.btnEditTaikhoan.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEditTaikhoan_ButtonPressed);
			// 
			// frmLogIn
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(310, 139);
			this.Controls.Add(this.btnEditMatkhau);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnKetnoiCSDL);
			this.Controls.Add(this.btnDangnhap);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnEditTaikhoan);
			this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frmLogIn";
			this.Text = "Đăng nhập";
			((System.ComponentModel.ISupportInitialize)(this.btnEditMatkhau.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditTaikhoan.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraEditors.SimpleButton btnDangnhap;
		private DevExpress.XtraEditors.ButtonEdit btnEditMatkhau;
		private DevExpress.XtraEditors.SimpleButton btnThoat;
		private DevExpress.XtraEditors.SimpleButton btnKetnoiCSDL;
		private DevExpress.XtraEditors.ButtonEdit btnEditTaikhoan;
	}
}