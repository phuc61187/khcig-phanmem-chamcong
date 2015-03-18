namespace ChamCong_v02 {
	partial class frmDangNhap {
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
			this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
			this.tbTaikhoan = new System.Windows.Forms.TextBox();
			this.tb_Password = new System.Windows.Forms.TextBox();
			this.btnDangnhap = new DevComponents.DotNetBar.ButtonX();
			this.btnThoat = new DevComponents.DotNetBar.ButtonX();
			this.btnKetnoiCSDL = new DevComponents.DotNetBar.ButtonX();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// styleManager1
			// 
			this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
			// 
			// tbTaikhoan
			// 
			this.tbTaikhoan.Location = new System.Drawing.Point(124, 43);
			this.tbTaikhoan.Multiline = true;
			this.tbTaikhoan.Name = "tbTaikhoan";
			this.tbTaikhoan.Size = new System.Drawing.Size(175, 22);
			this.tbTaikhoan.TabIndex = 0;
			this.tbTaikhoan.Text = "dainghia";
			// 
			// tb_Password
			// 
			this.tb_Password.Location = new System.Drawing.Point(124, 71);
			this.tb_Password.Name = "tb_Password";
			this.tb_Password.PasswordChar = '*';
			this.tb_Password.Size = new System.Drawing.Size(175, 22);
			this.tb_Password.TabIndex = 1;
			this.tb_Password.Text = "123@456789";
			this.tb_Password.UseSystemPasswordChar = true;
			// 
			// btnDangnhap
			// 
			this.btnDangnhap.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnDangnhap.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnDangnhap.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDangnhap.Location = new System.Drawing.Point(124, 99);
			this.btnDangnhap.Name = "btnDangnhap";
			this.btnDangnhap.Size = new System.Drawing.Size(85, 27);
			this.btnDangnhap.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnDangnhap.TabIndex = 2;
			this.btnDangnhap.Text = "Đăng nhập";
			this.btnDangnhap.Click += new System.EventHandler(this.btnDangnhap_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnThoat.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnThoat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnThoat.Location = new System.Drawing.Point(214, 99);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(85, 27);
			this.btnThoat.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnThoat.TabIndex = 3;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnKetnoiCSDL
			// 
			this.btnKetnoiCSDL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnKetnoiCSDL.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnKetnoiCSDL.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnKetnoiCSDL.Location = new System.Drawing.Point(124, 132);
			this.btnKetnoiCSDL.Name = "btnKetnoiCSDL";
			this.btnKetnoiCSDL.Size = new System.Drawing.Size(175, 27);
			this.btnKetnoiCSDL.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnKetnoiCSDL.TabIndex = 4;
			this.btnKetnoiCSDL.Text = "Kết nối CSDL";
			this.btnKetnoiCSDL.Click += new System.EventHandler(this.btnKetnoiCSDL_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(47, 46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Tài khoản";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(47, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Mật khẩu";
			// 
			// frmDangNhap
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(345, 196);
			this.ControlBox = false;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnKetnoiCSDL);
			this.Controls.Add(this.btnDangnhap);
			this.Controls.Add(this.tb_Password);
			this.Controls.Add(this.tbTaikhoan);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frmDangNhap";
			this.Padding = new System.Windows.Forms.Padding(40);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Đăng nhập";
			this.Load += new System.EventHandler(this.frmDangNhap_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.TextBox tbTaikhoan;
        private System.Windows.Forms.TextBox tb_Password;
        private DevComponents.DotNetBar.ButtonX btnDangnhap;
        private DevComponents.DotNetBar.ButtonX btnThoat;
        private DevComponents.DotNetBar.ButtonX btnKetnoiCSDL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
	}
}

