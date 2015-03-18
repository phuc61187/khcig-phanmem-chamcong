namespace CapNhatGioChamCong
{
    partial class frm_main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.MenuAdmin = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuAdminSub_TaoAccount = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuDuLieu = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_ChonDL = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuChamCong = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_XemCongNV = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_DiemDanh = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_KhaiBaoVang = new System.Windows.Forms.ToolStripMenuItem();
			this.subMenuKhaiBaoLVNgayNghi = new System.Windows.Forms.ToolStripMenuItem();
			this.subMenuChamCongTay = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuHoatDong = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_SuaGioHangLoat = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_xemHistory = new System.Windows.Forms.ToolStripMenuItem();
			this.subMenuTinhLuong = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuTaiKhoan = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_DoiMK = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_TaoTK = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuThoat = new System.Windows.Forms.ToolStripMenuItem();
			this.label2 = new System.Windows.Forms.Label();
			this.tb_Password = new System.Windows.Forms.TextBox();
			this.tb_UserName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnThoat = new System.Windows.Forms.Button();
			this.btnLogin = new System.Windows.Forms.Button();
			this.panelDangNhap = new System.Windows.Forms.Panel();
			this.menuStrip1.SuspendLayout();
			this.panelDangNhap.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.BackColor = System.Drawing.Color.LightGray;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAdmin,
            this.MenuDuLieu,
            this.MenuChamCong,
            this.MenuHoatDong,
            this.MenuTaiKhoan,
            this.MenuThoat});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1144, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// MenuAdmin
			// 
			this.MenuAdmin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAdminSub_TaoAccount});
			this.MenuAdmin.Enabled = false;
			this.MenuAdmin.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MenuAdmin.ForeColor = System.Drawing.Color.Black;
			this.MenuAdmin.Name = "MenuAdmin";
			this.MenuAdmin.Size = new System.Drawing.Size(97, 20);
			this.MenuAdmin.Text = "Administrator";
			this.MenuAdmin.Visible = false;
			this.MenuAdmin.EnabledChanged += new System.EventHandler(this.MenuAdmin_EnabledChanged);
			// 
			// MenuAdminSub_TaoAccount
			// 
			this.MenuAdminSub_TaoAccount.Enabled = false;
			this.MenuAdminSub_TaoAccount.Name = "MenuAdminSub_TaoAccount";
			this.MenuAdminSub_TaoAccount.Size = new System.Drawing.Size(154, 22);
			this.MenuAdminSub_TaoAccount.Text = "Tạo tài khoản";
			this.MenuAdminSub_TaoAccount.Click += new System.EventHandler(this.MenuAdminSub_TaoAccount_Click);
			// 
			// MenuDuLieu
			// 
			this.MenuDuLieu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenu_ChonDL});
			this.MenuDuLieu.Enabled = false;
			this.MenuDuLieu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MenuDuLieu.ForeColor = System.Drawing.Color.Black;
			this.MenuDuLieu.Image = global::CapNhatGioChamCong.Properties.Resources.icon_ChonDuLieu24;
			this.MenuDuLieu.Name = "MenuDuLieu";
			this.MenuDuLieu.Size = new System.Drawing.Size(78, 20);
			this.MenuDuLieu.Text = "Dữ liệu";
			this.MenuDuLieu.EnabledChanged += new System.EventHandler(this.MenuDuLieu_EnabledChanged);
			// 
			// SubMenu_ChonDL
			// 
			this.SubMenu_ChonDL.Enabled = false;
			this.SubMenu_ChonDL.Image = global::CapNhatGioChamCong.Properties.Resources.icon_ChonDuLieu24;
			this.SubMenu_ChonDL.Name = "SubMenu_ChonDL";
			this.SubMenu_ChonDL.Size = new System.Drawing.Size(150, 22);
			this.SubMenu_ChonDL.Text = "Chọn dữ liệu";
			this.SubMenu_ChonDL.Click += new System.EventHandler(this.MenuDuLieuSub_ChonDL_Click);
			// 
			// MenuChamCong
			// 
			this.MenuChamCong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenu_XemCongNV,
            this.SubMenu_DiemDanh,
            this.SubMenu_KhaiBaoVang,
            this.subMenuKhaiBaoLVNgayNghi,
            this.subMenuChamCongTay});
			this.MenuChamCong.Enabled = false;
			this.MenuChamCong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.MenuChamCong.ForeColor = System.Drawing.Color.Black;
			this.MenuChamCong.Image = global::CapNhatGioChamCong.Properties.Resources.ico_xemcong;
			this.MenuChamCong.Name = "MenuChamCong";
			this.MenuChamCong.Size = new System.Drawing.Size(102, 20);
			this.MenuChamCong.Text = "Chấm công";
			this.MenuChamCong.EnabledChanged += new System.EventHandler(this.MenuChamCong_EnabledChanged);
			// 
			// SubMenu_XemCongNV
			// 
			this.SubMenu_XemCongNV.Enabled = false;
			this.SubMenu_XemCongNV.Name = "SubMenu_XemCongNV";
			this.SubMenu_XemCongNV.Size = new System.Drawing.Size(266, 22);
			this.SubMenu_XemCongNV.Text = "Xem công";
			this.SubMenu_XemCongNV.Click += new System.EventHandler(this.MenuBaoBieuSub_XemCongNV_Click);
			// 
			// SubMenu_DiemDanh
			// 
			this.SubMenu_DiemDanh.Enabled = false;
			this.SubMenu_DiemDanh.Name = "SubMenu_DiemDanh";
			this.SubMenu_DiemDanh.Size = new System.Drawing.Size(266, 22);
			this.SubMenu_DiemDanh.Text = "Điểm danh Nhân viên chấm công";
			this.SubMenu_DiemDanh.Click += new System.EventHandler(this.diemdanhToolStripMenuItem_Click);
			// 
			// SubMenu_KhaiBaoVang
			// 
			this.SubMenu_KhaiBaoVang.Enabled = false;
			this.SubMenu_KhaiBaoVang.Name = "SubMenu_KhaiBaoVang";
			this.SubMenu_KhaiBaoVang.Size = new System.Drawing.Size(266, 22);
			this.SubMenu_KhaiBaoVang.Text = "Khai báo vắng cho nhân viên";
			this.SubMenu_KhaiBaoVang.Click += new System.EventHandler(this.testToolStripMenuItem1_Click);
			// 
			// subMenuKhaiBaoLVNgayNghi
			// 
			this.subMenuKhaiBaoLVNgayNghi.Name = "subMenuKhaiBaoLVNgayNghi";
			this.subMenuKhaiBaoLVNgayNghi.Size = new System.Drawing.Size(266, 22);
			this.subMenuKhaiBaoLVNgayNghi.Text = "Khai báo làm việc ngày nghỉ";
			this.subMenuKhaiBaoLVNgayNghi.Click += new System.EventHandler(this.subMenuKhaiBaoLVNgayNghi_Click);
			// 
			// subMenuChamCongTay
			// 
			this.subMenuChamCongTay.Name = "subMenuChamCongTay";
			this.subMenuChamCongTay.Size = new System.Drawing.Size(266, 22);
			this.subMenuChamCongTay.Text = "Chấm công tay cho Nhân viên";
			this.subMenuChamCongTay.Click += new System.EventHandler(this.subMenuChamCongTay_Click);
			// 
			// MenuHoatDong
			// 
			this.MenuHoatDong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenu_SuaGioHangLoat,
            this.SubMenu_xemHistory,
            this.subMenuTinhLuong});
			this.MenuHoatDong.Enabled = false;
			this.MenuHoatDong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MenuHoatDong.ForeColor = System.Drawing.Color.Black;
			this.MenuHoatDong.Image = global::CapNhatGioChamCong.Properties.Resources.icon_HoatDongKhac;
			this.MenuHoatDong.Name = "MenuHoatDong";
			this.MenuHoatDong.Size = new System.Drawing.Size(127, 20);
			this.MenuHoatDong.Text = "Hoạt động khác";
			this.MenuHoatDong.EnabledChanged += new System.EventHandler(this.MenuHoatDong_EnabledChanged);
			// 
			// SubMenu_SuaGioHangLoat
			// 
			this.SubMenu_SuaGioHangLoat.Enabled = false;
			this.SubMenu_SuaGioHangLoat.Name = "SubMenu_SuaGioHangLoat";
			this.SubMenu_SuaGioHangLoat.Size = new System.Drawing.Size(263, 22);
			this.SubMenu_SuaGioHangLoat.Text = "Sửa giờ hàng loạt";
			this.SubMenu_SuaGioHangLoat.Click += new System.EventHandler(this.SuaGioHangLoatToolStripMenuItem_Click);
			// 
			// SubMenu_xemHistory
			// 
			this.SubMenu_xemHistory.Enabled = false;
			this.SubMenu_xemHistory.Image = global::CapNhatGioChamCong.Properties.Resources.iconHistory;
			this.SubMenu_xemHistory.Name = "SubMenu_xemHistory";
			this.SubMenu_xemHistory.Size = new System.Drawing.Size(263, 22);
			this.SubMenu_xemHistory.Text = "Xem lịch sử sửa giờ chấm công";
			this.SubMenu_xemHistory.Click += new System.EventHandler(this.xemHistoryToolStripMenuItem_Click);
			// 
			// subMenuTinhLuong
			// 
			this.subMenuTinhLuong.Name = "subMenuTinhLuong";
			this.subMenuTinhLuong.Size = new System.Drawing.Size(263, 22);
			this.subMenuTinhLuong.Text = "Tính lương cho NV";
			this.subMenuTinhLuong.Click += new System.EventHandler(this.subMenuTinhLuong_Click);
			// 
			// MenuTaiKhoan
			// 
			this.MenuTaiKhoan.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenu_DoiMK,
            this.SubMenu_TaoTK});
			this.MenuTaiKhoan.Enabled = false;
			this.MenuTaiKhoan.Name = "MenuTaiKhoan";
			this.MenuTaiKhoan.Size = new System.Drawing.Size(71, 20);
			this.MenuTaiKhoan.Text = "Tài khoản";
			this.MenuTaiKhoan.EnabledChanged += new System.EventHandler(this.MenuTaiKhoan_EnabledChanged);
			// 
			// SubMenu_DoiMK
			// 
			this.SubMenu_DoiMK.Enabled = false;
			this.SubMenu_DoiMK.Name = "SubMenu_DoiMK";
			this.SubMenu_DoiMK.Size = new System.Drawing.Size(209, 22);
			this.SubMenu_DoiMK.Text = "Đổi mật khẩu đăng nhập";
			this.SubMenu_DoiMK.Click += new System.EventHandler(this.MenuHoatDongSub_DoiMK_Click);
			// 
			// SubMenu_TaoTK
			// 
			this.SubMenu_TaoTK.Enabled = false;
			this.SubMenu_TaoTK.Name = "SubMenu_TaoTK";
			this.SubMenu_TaoTK.Size = new System.Drawing.Size(209, 22);
			this.SubMenu_TaoTK.Text = "Tạo Tài khoản đăng nhập";
			// 
			// MenuThoat
			// 
			this.MenuThoat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.MenuThoat.ForeColor = System.Drawing.Color.Black;
			this.MenuThoat.Image = global::CapNhatGioChamCong.Properties.Resources.icon_Exit;
			this.MenuThoat.Name = "MenuThoat";
			this.MenuThoat.Size = new System.Drawing.Size(68, 20);
			this.MenuThoat.Text = "Thoát";
			this.MenuThoat.Click += new System.EventHandler(this.MenuThoat_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label2.ForeColor = System.Drawing.Color.Blue;
			this.label2.Location = new System.Drawing.Point(56, 54);
			this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Mật khẩu";
			// 
			// tb_Password
			// 
			this.tb_Password.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.tb_Password.Location = new System.Drawing.Point(122, 51);
			this.tb_Password.Name = "tb_Password";
			this.tb_Password.PasswordChar = '*';
			this.tb_Password.Size = new System.Drawing.Size(163, 22);
			this.tb_Password.TabIndex = 1;
			this.tb_Password.Text = "123@456789";
			// 
			// tb_UserName
			// 
			this.tb_UserName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.tb_UserName.Location = new System.Drawing.Point(122, 23);
			this.tb_UserName.Name = "tb_UserName";
			this.tb_UserName.Size = new System.Drawing.Size(163, 22);
			this.tb_UserName.TabIndex = 0;
			this.tb_UserName.Text = "dainghia";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label1.ForeColor = System.Drawing.Color.Blue;
			this.label1.Location = new System.Drawing.Point(25, 26);
			this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Tên đăng nhập";
			// 
			// btnThoat
			// 
			this.btnThoat.BackColor = System.Drawing.Color.LightGray;
			this.btnThoat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Image = global::CapNhatGioChamCong.Properties.Resources.icon_Exit;
			this.btnThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnThoat.Location = new System.Drawing.Point(165, 85);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(120, 40);
			this.btnThoat.TabIndex = 3;
			this.btnThoat.Text = "     Thoát";
			this.btnThoat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnLogin
			// 
			this.btnLogin.BackColor = System.Drawing.Color.Silver;
			this.btnLogin.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnLogin.ForeColor = System.Drawing.Color.Blue;
			this.btnLogin.Image = global::CapNhatGioChamCong.Properties.Resources.icon_login;
			this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnLogin.Location = new System.Drawing.Point(18, 85);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(120, 40);
			this.btnLogin.TabIndex = 2;
			this.btnLogin.Text = "Đăng nhập";
			this.btnLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// panelDangNhap
			// 
			this.panelDangNhap.BackColor = System.Drawing.Color.LightGray;
			this.panelDangNhap.Controls.Add(this.btnLogin);
			this.panelDangNhap.Controls.Add(this.btnThoat);
			this.panelDangNhap.Controls.Add(this.label2);
			this.panelDangNhap.Controls.Add(this.label1);
			this.panelDangNhap.Controls.Add(this.tb_Password);
			this.panelDangNhap.Controls.Add(this.tb_UserName);
			this.panelDangNhap.ForeColor = System.Drawing.Color.Blue;
			this.panelDangNhap.Location = new System.Drawing.Point(340, 208);
			this.panelDangNhap.Name = "panelDangNhap";
			this.panelDangNhap.Size = new System.Drawing.Size(302, 135);
			this.panelDangNhap.TabIndex = 13;
			// 
			// frm_main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ClientSize = new System.Drawing.Size(1144, 650);
			this.Controls.Add(this.panelDangNhap);
			this.Controls.Add(this.menuStrip1);
			this.DoubleBuffered = true;
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "frm_main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Chương trình máy chấm công";
			this.Load += new System.EventHandler(this.frm_main_Load);
			this.SizeChanged += new System.EventHandler(this.frm_main_SizeChanged);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.panelDangNhap.ResumeLayout(false);
			this.panelDangNhap.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.TextBox tb_UserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ToolStripMenuItem MenuThoat;
        private System.Windows.Forms.Panel panelDangNhap;
        private System.Windows.Forms.ToolStripMenuItem MenuAdmin;
        private System.Windows.Forms.ToolStripMenuItem MenuAdminSub_TaoAccount;
        private System.Windows.Forms.ToolStripMenuItem MenuDuLieu;
        private System.Windows.Forms.ToolStripMenuItem SubMenu_ChonDL;
        private System.Windows.Forms.ToolStripMenuItem MenuHoatDong;
        private System.Windows.Forms.ToolStripMenuItem MenuChamCong;
        private System.Windows.Forms.ToolStripMenuItem SubMenu_XemCongNV;
        private System.Windows.Forms.ToolStripMenuItem SubMenu_SuaGioHangLoat;
        private System.Windows.Forms.ToolStripMenuItem SubMenu_xemHistory;
        private System.Windows.Forms.ToolStripMenuItem SubMenu_DiemDanh;
        private System.Windows.Forms.ToolStripMenuItem SubMenu_KhaiBaoVang;
        private System.Windows.Forms.ToolStripMenuItem MenuTaiKhoan;
        private System.Windows.Forms.ToolStripMenuItem SubMenu_DoiMK;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_TaoTK;
		private System.Windows.Forms.ToolStripMenuItem subMenuTinhLuong;
		private System.Windows.Forms.ToolStripMenuItem subMenuKhaiBaoLVNgayNghi;
		private System.Windows.Forms.ToolStripMenuItem subMenuChamCongTay;
    }
}