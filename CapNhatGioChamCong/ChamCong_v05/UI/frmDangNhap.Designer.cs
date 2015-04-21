namespace ChamCong_v05.UI {
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
			this.tbTaikhoan = new System.Windows.Forms.TextBox();
			this.tb_Password = new System.Windows.Forms.TextBox();
			this.btnDangnhap = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.btnKetnoiCSDL = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tbTaikhoan
			// 
			this.tbTaikhoan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
			this.tbTaikhoan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.tbTaikhoan.Location = new System.Drawing.Point(124, 40);
			this.tbTaikhoan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tbTaikhoan.Name = "tbTaikhoan";
			this.tbTaikhoan.Size = new System.Drawing.Size(175, 21);
			this.tbTaikhoan.TabIndex = 1;
			this.tbTaikhoan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTaikhoan_Or_tbPassword_KeyDown);
			// 
			// tb_Password
			// 
			this.tb_Password.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.tb_Password.Location = new System.Drawing.Point(124, 67);
			this.tb_Password.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tb_Password.Name = "tb_Password";
			this.tb_Password.PasswordChar = '*';
			this.tb_Password.Size = new System.Drawing.Size(175, 21);
			this.tb_Password.TabIndex = 2;
			this.tb_Password.Text = "d@ingh1a";
			this.tb_Password.UseSystemPasswordChar = true;
			this.tb_Password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTaikhoan_Or_tbPassword_KeyDown);
			// 
			// btnDangnhap
			// 
			this.btnDangnhap.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnDangnhap.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnDangnhap.ForeColor = System.Drawing.Color.Blue;
			this.btnDangnhap.Location = new System.Drawing.Point(124, 92);
			this.btnDangnhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnDangnhap.Name = "btnDangnhap";
			this.btnDangnhap.Size = new System.Drawing.Size(85, 27);
			this.btnDangnhap.TabIndex = 3;
			this.btnDangnhap.Text = "Đăng nhập";
			this.btnDangnhap.Click += new System.EventHandler(this.btnDangnhap_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(214, 92);
			this.btnThoat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(85, 27);
			this.btnThoat.TabIndex = 4;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnKetnoiCSDL
			// 
			this.btnKetnoiCSDL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnKetnoiCSDL.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnKetnoiCSDL.ForeColor = System.Drawing.Color.Blue;
			this.btnKetnoiCSDL.Location = new System.Drawing.Point(124, 123);
			this.btnKetnoiCSDL.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnKetnoiCSDL.Name = "btnKetnoiCSDL";
			this.btnKetnoiCSDL.Size = new System.Drawing.Size(175, 27);
			this.btnKetnoiCSDL.TabIndex = 5;
			this.btnKetnoiCSDL.Text = "Kết nối CSDL";
			this.btnKetnoiCSDL.Click += new System.EventHandler(this.btnKetnoiCSDL_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(47, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Tài khoản";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(47, 69);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 15);
			this.label2.TabIndex = 6;
			this.label2.Text = "Mật khẩu";
			// 
			// frmDangNhap
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(345, 183);
			this.ControlBox = false;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnKetnoiCSDL);
			this.Controls.Add(this.btnDangnhap);
			this.Controls.Add(this.tb_Password);
			this.Controls.Add(this.tbTaikhoan);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Name = "frmDangNhap";
			this.Padding = new System.Windows.Forms.Padding(40, 37, 40, 37);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Đăng nhập";
			this.Load += new System.EventHandler(this.frmDangNhap_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbTaikhoan;
		private System.Windows.Forms.TextBox tb_Password;
		private System.Windows.Forms.Button btnDangnhap;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.Button btnKetnoiCSDL;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}

