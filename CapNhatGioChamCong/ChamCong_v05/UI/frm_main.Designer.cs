namespace ChamCong_v05.UI {
    partial class frm_main {
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
			this.MenuChamCong = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_XemCongNV = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_DiemDanh = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_ChamcongTayQL = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuKhaiBaoVang = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuHoatDong = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_SuaGioHangLoat = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_XemHistory = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_QLNhiemVuNhanVien = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_XemTKeCongVaPCTheoNVu = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_XemDSNhiemVu = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuQLNV = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuTinhLuong = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_KetLuongThang = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_HuyKetluong = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuTaiKhoan = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_DoiMK = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuHeThong = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_PhanQuyen = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_TaoTK = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_Setting = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuThoat = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.BackColor = System.Drawing.Color.LightGray;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuChamCong,
            this.MenuKhaiBaoVang,
            this.MenuHoatDong,
            this.MenuQLNV,
            this.MenuTinhLuong,
            this.MenuTaiKhoan,
            this.MenuHeThong,
            this.MenuThoat});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1344, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// MenuChamCong
			// 
			this.MenuChamCong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenu_XemCongNV,
            this.SubMenu_DiemDanh,
            this.SubMenu_ChamcongTayQL});
			this.MenuChamCong.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.MenuChamCong.ForeColor = System.Drawing.Color.Black;
			this.MenuChamCong.Name = "MenuChamCong";
			this.MenuChamCong.Size = new System.Drawing.Size(83, 20);
			this.MenuChamCong.Text = "Chấm công";
			// 
			// SubMenu_XemCongNV
			// 
			this.SubMenu_XemCongNV.Name = "SubMenu_XemCongNV";
			this.SubMenu_XemCongNV.Size = new System.Drawing.Size(257, 22);
			this.SubMenu_XemCongNV.Text = "Xem công";
			this.SubMenu_XemCongNV.Click += new System.EventHandler(this.SubMenu_XemCongNV_Click);
			// 
			// SubMenu_DiemDanh
			// 
			this.SubMenu_DiemDanh.Name = "SubMenu_DiemDanh";
			this.SubMenu_DiemDanh.Size = new System.Drawing.Size(257, 22);
			this.SubMenu_DiemDanh.Text = "Điểm danh Nhân viên chấm công";
			this.SubMenu_DiemDanh.Click += new System.EventHandler(this.SubMenu_DiemDanh_Click);
			// 
			// SubMenu_ChamcongTayQL
			// 
			this.SubMenu_ChamcongTayQL.Name = "SubMenu_ChamcongTayQL";
			this.SubMenu_ChamcongTayQL.Size = new System.Drawing.Size(257, 22);
			this.SubMenu_ChamcongTayQL.Text = "Chấm công tay cho Quản lý";
			this.SubMenu_ChamcongTayQL.Click += new System.EventHandler(this.SubMenu_ChamcongTayQL_Click);
			// 
			// MenuKhaiBaoVang
			// 
			this.MenuKhaiBaoVang.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.MenuKhaiBaoVang.Name = "MenuKhaiBaoVang";
			this.MenuKhaiBaoVang.Size = new System.Drawing.Size(97, 20);
			this.MenuKhaiBaoVang.Text = "Khai báo vắng";
			this.MenuKhaiBaoVang.Click += new System.EventHandler(this.Menu_KhaiBaoVang_Click);
			// 
			// MenuHoatDong
			// 
			this.MenuHoatDong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenu_SuaGioHangLoat,
            this.SubMenu_XemHistory,
            this.SubMenu_QLNhiemVuNhanVien,
            this.SubMenu_XemTKeCongVaPCTheoNVu,
            this.SubMenu_XemDSNhiemVu});
			this.MenuHoatDong.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.MenuHoatDong.ForeColor = System.Drawing.Color.Black;
			this.MenuHoatDong.Name = "MenuHoatDong";
			this.MenuHoatDong.Size = new System.Drawing.Size(105, 20);
			this.MenuHoatDong.Text = "Hoạt động khác";
			// 
			// SubMenu_SuaGioHangLoat
			// 
			this.SubMenu_SuaGioHangLoat.Name = "SubMenu_SuaGioHangLoat";
			this.SubMenu_SuaGioHangLoat.Size = new System.Drawing.Size(316, 22);
			this.SubMenu_SuaGioHangLoat.Text = "Sửa giờ hàng loạt";
			this.SubMenu_SuaGioHangLoat.Click += new System.EventHandler(this.SubMenu_SuaGioHangLoat_Click);
			// 
			// SubMenu_XemHistory
			// 
			this.SubMenu_XemHistory.Name = "SubMenu_XemHistory";
			this.SubMenu_XemHistory.Size = new System.Drawing.Size(316, 22);
			this.SubMenu_XemHistory.Text = "Xem lịch sử sửa giờ chấm công";
			this.SubMenu_XemHistory.Click += new System.EventHandler(this.SubMenu_xemHistory_Click);
			// 
			// SubMenu_QLNhiemVuNhanVien
			// 
			this.SubMenu_QLNhiemVuNhanVien.Name = "SubMenu_QLNhiemVuNhanVien";
			this.SubMenu_QLNhiemVuNhanVien.Size = new System.Drawing.Size(316, 22);
			this.SubMenu_QLNhiemVuNhanVien.Text = "Quản lý nhiệm vụ của nhân viên";
			this.SubMenu_QLNhiemVuNhanVien.Click += new System.EventHandler(this.SubMenu_QLNhiemVu_Click);
			// 
			// SubMenu_XemTKeCongVaPCTheoNVu
			// 
			this.SubMenu_XemTKeCongVaPCTheoNVu.Name = "SubMenu_XemTKeCongVaPCTheoNVu";
			this.SubMenu_XemTKeCongVaPCTheoNVu.Size = new System.Drawing.Size(316, 22);
			this.SubMenu_XemTKeCongVaPCTheoNVu.Text = "Xem thống kê công, PC, phép theo nhiệm vụ";
			this.SubMenu_XemTKeCongVaPCTheoNVu.Click += new System.EventHandler(this.SubMenu_XemTKeCongVaPCTheoNVu_Click);
			// 
			// SubMenu_XemDSNhiemVu
			// 
			this.SubMenu_XemDSNhiemVu.Name = "SubMenu_XemDSNhiemVu";
			this.SubMenu_XemDSNhiemVu.Size = new System.Drawing.Size(316, 22);
			this.SubMenu_XemDSNhiemVu.Text = "Xem danh sách nhiệm vụ";
			this.SubMenu_XemDSNhiemVu.Click += new System.EventHandler(this.SubMenu_XemDSNhiemVu_Click);
			// 
			// MenuQLNV
			// 
			this.MenuQLNV.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.MenuQLNV.Name = "MenuQLNV";
			this.MenuQLNV.Size = new System.Drawing.Size(116, 20);
			this.MenuQLNV.Text = "Quản lý nhân viên";
			this.MenuQLNV.Click += new System.EventHandler(this.MenuQLNV_Click);
			// 
			// MenuTinhLuong
			// 
			this.MenuTinhLuong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenu_KetLuongThang,
            this.SubMenu_HuyKetluong});
			this.MenuTinhLuong.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.MenuTinhLuong.Name = "MenuTinhLuong";
			this.MenuTinhLuong.Size = new System.Drawing.Size(79, 20);
			this.MenuTinhLuong.Text = "Tính lương";
			// 
			// SubMenu_KetLuongThang
			// 
			this.SubMenu_KetLuongThang.Name = "SubMenu_KetLuongThang";
			this.SubMenu_KetLuongThang.Size = new System.Drawing.Size(184, 22);
			this.SubMenu_KetLuongThang.Text = "Kết lương tháng";
			this.SubMenu_KetLuongThang.Click += new System.EventHandler(this.SubMenu_KetLuongThang_Click);
			// 
			// SubMenu_HuyKetluong
			// 
			this.SubMenu_HuyKetluong.Name = "SubMenu_HuyKetluong";
			this.SubMenu_HuyKetluong.Size = new System.Drawing.Size(184, 22);
			this.SubMenu_HuyKetluong.Text = "Hủy kết lương tháng";
			this.SubMenu_HuyKetluong.Click += new System.EventHandler(this.SubMenu_HuyKetluong_Click);
			// 
			// MenuTaiKhoan
			// 
			this.MenuTaiKhoan.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenu_DoiMK});
			this.MenuTaiKhoan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.MenuTaiKhoan.Name = "MenuTaiKhoan";
			this.MenuTaiKhoan.Size = new System.Drawing.Size(73, 20);
			this.MenuTaiKhoan.Text = "Tài khoản";
			// 
			// SubMenu_DoiMK
			// 
			this.SubMenu_DoiMK.Name = "SubMenu_DoiMK";
			this.SubMenu_DoiMK.Size = new System.Drawing.Size(209, 22);
			this.SubMenu_DoiMK.Text = "Đổi mật khẩu đăng nhập";
			this.SubMenu_DoiMK.Click += new System.EventHandler(this.SubMenu_DoiMK_Click);
			// 
			// MenuHeThong
			// 
			this.MenuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenu_PhanQuyen,
            this.SubMenu_TaoTK,
            this.SubMenu_Setting});
			this.MenuHeThong.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.MenuHeThong.Name = "MenuHeThong";
			this.MenuHeThong.Size = new System.Drawing.Size(69, 20);
			this.MenuHeThong.Text = "Hệ thống";
			// 
			// SubMenu_PhanQuyen
			// 
			this.SubMenu_PhanQuyen.Name = "SubMenu_PhanQuyen";
			this.SubMenu_PhanQuyen.Size = new System.Drawing.Size(210, 22);
			this.SubMenu_PhanQuyen.Text = "Phân Quyền";
			this.SubMenu_PhanQuyen.Click += new System.EventHandler(this.SubMenu_PhanQuyen_Click);
			// 
			// SubMenu_TaoTK
			// 
			this.SubMenu_TaoTK.Name = "SubMenu_TaoTK";
			this.SubMenu_TaoTK.Size = new System.Drawing.Size(210, 22);
			this.SubMenu_TaoTK.Text = "Tạo tài khoản đăng nhập";
			this.SubMenu_TaoTK.Click += new System.EventHandler(this.SubMenu_TaoTK_Click);
			// 
			// SubMenu_Setting
			// 
			this.SubMenu_Setting.Name = "SubMenu_Setting";
			this.SubMenu_Setting.Size = new System.Drawing.Size(210, 22);
			this.SubMenu_Setting.Text = "Cài đặt";
			this.SubMenu_Setting.Click += new System.EventHandler(this.SubMenu_Setting_Click);
			// 
			// MenuThoat
			// 
			this.MenuThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.MenuThoat.ForeColor = System.Drawing.Color.Black;
			this.MenuThoat.Name = "MenuThoat";
			this.MenuThoat.Size = new System.Drawing.Size(50, 20);
			this.MenuThoat.Text = "Thoát";
			this.MenuThoat.Click += new System.EventHandler(this.MenuThoat_Click);
			// 
			// frm_main
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1344, 652);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.IsMdiContainer = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "frm_main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Phần mềm ứng dụng chấm công vân tay";
			this.Load += new System.EventHandler(this.frm_main_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuChamCong;
        private System.Windows.Forms.ToolStripMenuItem SubMenu_XemCongNV;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_DiemDanh;
        private System.Windows.Forms.ToolStripMenuItem MenuHoatDong;
        private System.Windows.Forms.ToolStripMenuItem SubMenu_SuaGioHangLoat;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_XemHistory;
        private System.Windows.Forms.ToolStripMenuItem MenuTaiKhoan;
        private System.Windows.Forms.ToolStripMenuItem SubMenu_DoiMK;
        private System.Windows.Forms.ToolStripMenuItem MenuThoat;
		private System.Windows.Forms.ToolStripMenuItem MenuKhaiBaoVang;
		private System.Windows.Forms.ToolStripMenuItem MenuTinhLuong;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_KetLuongThang;
		private System.Windows.Forms.ToolStripMenuItem MenuQLNV;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_HuyKetluong;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_ChamcongTayQL;
		private System.Windows.Forms.ToolStripMenuItem MenuHeThong;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_PhanQuyen;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_TaoTK;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_Setting;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_XemTKeCongVaPCTheoNVu;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_QLNhiemVuNhanVien;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_XemDSNhiemVu;

    }
}