namespace ChamCong_v04.UI.QLNV {
	partial class frmCapNhatNVHangLoat {
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("px thành phẩm");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("bảo vệ");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("văn phòng", new System.Windows.Forms.TreeNode[] {
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Nhà máy thuốc lá khánh hội", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3});
            this.treePhongBan = new System.Windows.Forms.TreeView();
            this.checkPhong = new System.Windows.Forms.CheckBox();
            this.checkChucVu = new System.Windows.Forms.CheckBox();
            this.cbChucVu = new System.Windows.Forms.ComboBox();
            this.checkLichtrinh = new System.Windows.Forms.CheckBox();
            this.cbLichTrinh = new System.Windows.Forms.ComboBox();
            this.checkUserEnabled = new System.Windows.Forms.CheckBox();
            this.tbHSLCV = new System.Windows.Forms.MaskedTextBox();
            this.tbHSLCB = new System.Windows.Forms.MaskedTextBox();
            this.checkHSLCB = new System.Windows.Forms.CheckBox();
            this.checkHSLCV = new System.Windows.Forms.CheckBox();
            this.checkTinhtrangHoatdong = new System.Windows.Forms.CheckBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.toolTipHint = new System.Windows.Forms.ToolTip(this.components);
            this.checkHSLCBTT17 = new System.Windows.Forms.CheckBox();
            this.checkHSPCTN = new System.Windows.Forms.CheckBox();
            this.tbHSLCBTT17 = new System.Windows.Forms.MaskedTextBox();
            this.tbHSPCTN = new System.Windows.Forms.MaskedTextBox();
            this.checkHSPCDH = new System.Windows.Forms.CheckBox();
            this.tbHSPCDH = new System.Windows.Forms.MaskedTextBox();
            this.checkHSPCCV = new System.Windows.Forms.CheckBox();
            this.tbHSPCCV = new System.Windows.Forms.MaskedTextBox();
            this.checkNVNhanKiet = new System.Windows.Forms.CheckBox();
            this.checkPhanNhomNhanVien = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // treePhongBan
            // 
            this.treePhongBan.Enabled = false;
            this.treePhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
            this.treePhongBan.Indent = 18;
            this.treePhongBan.ItemHeight = 20;
            this.treePhongBan.Location = new System.Drawing.Point(197, 11);
            this.treePhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treePhongBan.Name = "treePhongBan";
            treeNode1.Name = "Node2";
            treeNode1.Text = "px thành phẩm";
            treeNode2.Name = "Node5";
            treeNode2.Text = "bảo vệ";
            treeNode3.Name = "Node4";
            treeNode3.Text = "văn phòng";
            treeNode4.Name = "Node0";
            treeNode4.Text = "Nhà máy thuốc lá khánh hội";
            this.treePhongBan.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treePhongBan.ShowNodeToolTips = true;
            this.treePhongBan.Size = new System.Drawing.Size(242, 252);
            this.treePhongBan.TabIndex = 0;
            // 
            // checkPhong
            // 
            this.checkPhong.AutoSize = true;
            this.checkPhong.Location = new System.Drawing.Point(12, 12);
            this.checkPhong.Name = "checkPhong";
            this.checkPhong.Size = new System.Drawing.Size(86, 19);
            this.checkPhong.TabIndex = 0;
            this.checkPhong.Text = "Phòng ban";
            this.checkPhong.UseVisualStyleBackColor = true;
            this.checkPhong.CheckedChanged += new System.EventHandler(this.Checked_Changed);
            // 
            // checkChucVu
            // 
            this.checkChucVu.AutoSize = true;
            this.checkChucVu.Location = new System.Drawing.Point(12, 270);
            this.checkChucVu.Name = "checkChucVu";
            this.checkChucVu.Size = new System.Drawing.Size(71, 19);
            this.checkChucVu.TabIndex = 2;
            this.checkChucVu.Text = "Chức vụ";
            this.checkChucVu.UseVisualStyleBackColor = true;
            this.checkChucVu.CheckedChanged += new System.EventHandler(this.Checked_Changed);
            // 
            // cbChucVu
            // 
            this.cbChucVu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChucVu.Enabled = false;
            this.cbChucVu.FormattingEnabled = true;
            this.cbChucVu.Location = new System.Drawing.Point(197, 268);
            this.cbChucVu.Name = "cbChucVu";
            this.cbChucVu.Size = new System.Drawing.Size(242, 23);
            this.cbChucVu.TabIndex = 1;
            // 
            // checkLichtrinh
            // 
            this.checkLichtrinh.AutoSize = true;
            this.checkLichtrinh.Location = new System.Drawing.Point(12, 299);
            this.checkLichtrinh.Name = "checkLichtrinh";
            this.checkLichtrinh.Size = new System.Drawing.Size(76, 19);
            this.checkLichtrinh.TabIndex = 4;
            this.checkLichtrinh.Text = "Lịch trình";
            this.checkLichtrinh.UseVisualStyleBackColor = true;
            this.checkLichtrinh.CheckedChanged += new System.EventHandler(this.Checked_Changed);
            // 
            // cbLichTrinh
            // 
            this.cbLichTrinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLichTrinh.Enabled = false;
            this.cbLichTrinh.FormattingEnabled = true;
            this.cbLichTrinh.Location = new System.Drawing.Point(197, 297);
            this.cbLichTrinh.Name = "cbLichTrinh";
            this.cbLichTrinh.Size = new System.Drawing.Size(242, 23);
            this.cbLichTrinh.TabIndex = 2;
            // 
            // checkUserEnabled
            // 
            this.checkUserEnabled.AutoSize = true;
            this.checkUserEnabled.Enabled = false;
            this.checkUserEnabled.Location = new System.Drawing.Point(197, 481);
            this.checkUserEnabled.Name = "checkUserEnabled";
            this.checkUserEnabled.Size = new System.Drawing.Size(164, 19);
            this.checkUserEnabled.TabIndex = 9;
            this.checkUserEnabled.Text = "Cho phép (UserEnabled)";
            this.toolTipHint.SetToolTip(this.checkUserEnabled, "Đánh dấu mục này nếu NV đang làm việc\r\nvà chấm công. Bỏ đánh dấu nếu NV không\r\ncò" +
        "n làm việc.");
            this.checkUserEnabled.UseVisualStyleBackColor = true;
            // 
            // tbHSLCV
            // 
            this.tbHSLCV.Enabled = false;
            this.tbHSLCV.Location = new System.Drawing.Point(197, 351);
            this.tbHSLCV.Mask = "0.00";
            this.tbHSLCV.Name = "tbHSLCV";
            this.tbHSLCV.Size = new System.Drawing.Size(100, 21);
            this.tbHSLCV.TabIndex = 4;
            this.tbHSLCV.Text = "000";
            // 
            // tbHSLCB
            // 
            this.tbHSLCB.Enabled = false;
            this.tbHSLCB.Location = new System.Drawing.Point(197, 326);
            this.tbHSLCB.Mask = "0.00";
            this.tbHSLCB.Name = "tbHSLCB";
            this.tbHSLCB.Size = new System.Drawing.Size(100, 21);
            this.tbHSLCB.TabIndex = 3;
            this.tbHSLCB.Text = "000";
            // 
            // checkHSLCB
            // 
            this.checkHSLCB.AutoSize = true;
            this.checkHSLCB.Location = new System.Drawing.Point(12, 328);
            this.checkHSLCB.Name = "checkHSLCB";
            this.checkHSLCB.Size = new System.Drawing.Size(136, 19);
            this.checkHSLCB.TabIndex = 6;
            this.checkHSLCB.Text = "Hệ số lương cơ bản";
            this.checkHSLCB.UseVisualStyleBackColor = true;
            this.checkHSLCB.CheckedChanged += new System.EventHandler(this.Checked_Changed);
            // 
            // checkHSLCV
            // 
            this.checkHSLCV.AutoSize = true;
            this.checkHSLCV.Location = new System.Drawing.Point(12, 353);
            this.checkHSLCV.Name = "checkHSLCV";
            this.checkHSLCV.Size = new System.Drawing.Size(149, 19);
            this.checkHSLCV.TabIndex = 8;
            this.checkHSLCV.Text = "Hệ số lương công việc";
            this.checkHSLCV.UseVisualStyleBackColor = true;
            this.checkHSLCV.CheckedChanged += new System.EventHandler(this.Checked_Changed);
            // 
            // checkTinhtrangHoatdong
            // 
            this.checkTinhtrangHoatdong.AutoSize = true;
            this.checkTinhtrangHoatdong.Location = new System.Drawing.Point(12, 481);
            this.checkTinhtrangHoatdong.Name = "checkTinhtrangHoatdong";
            this.checkTinhtrangHoatdong.Size = new System.Drawing.Size(139, 19);
            this.checkTinhtrangHoatdong.TabIndex = 10;
            this.checkTinhtrangHoatdong.Text = "Tình trạng hoạt động";
            this.checkTinhtrangHoatdong.UseVisualStyleBackColor = true;
            this.checkTinhtrangHoatdong.CheckedChanged += new System.EventHandler(this.Checked_Changed);
            // 
            // btnLuu
            // 
            this.btnLuu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnLuu.ForeColor = System.Drawing.Color.Blue;
            this.btnLuu.Location = new System.Drawing.Point(197, 529);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(90, 27);
            this.btnLuu.TabIndex = 10;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnDong
            // 
            this.btnDong.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnDong.ForeColor = System.Drawing.Color.Blue;
            this.btnDong.Location = new System.Drawing.Point(349, 529);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(90, 27);
            this.btnDong.TabIndex = 10;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // toolTipHint
            // 
            this.toolTipHint.AutoPopDelay = 30000;
            this.toolTipHint.InitialDelay = 300;
            this.toolTipHint.OwnerDraw = true;
            this.toolTipHint.ReshowDelay = 100;
            this.toolTipHint.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipHint_Draw);
            this.toolTipHint.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTipHint_Popup);
            // 
            // checkHSLCBTT17
            // 
            this.checkHSLCBTT17.AutoSize = true;
            this.checkHSLCBTT17.Location = new System.Drawing.Point(12, 378);
            this.checkHSLCBTT17.Name = "checkHSLCBTT17";
            this.checkHSLCBTT17.Size = new System.Drawing.Size(167, 19);
            this.checkHSLCBTT17.TabIndex = 6;
            this.checkHSLCBTT17.Text = "Hệ số lương cơ bản TT17";
            this.checkHSLCBTT17.UseVisualStyleBackColor = true;
            this.checkHSLCBTT17.CheckedChanged += new System.EventHandler(this.Checked_Changed);
            // 
            // checkHSPCTN
            // 
            this.checkHSPCTN.AutoSize = true;
            this.checkHSPCTN.Location = new System.Drawing.Point(12, 456);
            this.checkHSPCTN.Name = "checkHSPCTN";
            this.checkHSPCTN.Size = new System.Drawing.Size(175, 19);
            this.checkHSPCTN.TabIndex = 8;
            this.checkHSPCTN.Text = "Hệ số Phụ cấp trách nhiệm";
            this.checkHSPCTN.UseVisualStyleBackColor = true;
            this.checkHSPCTN.CheckedChanged += new System.EventHandler(this.Checked_Changed);
            // 
            // tbHSLCBTT17
            // 
            this.tbHSLCBTT17.Enabled = false;
            this.tbHSLCBTT17.Location = new System.Drawing.Point(197, 376);
            this.tbHSLCBTT17.Mask = "0.00";
            this.tbHSLCBTT17.Name = "tbHSLCBTT17";
            this.tbHSLCBTT17.Size = new System.Drawing.Size(100, 21);
            this.tbHSLCBTT17.TabIndex = 5;
            this.tbHSLCBTT17.Text = "000";
            // 
            // tbHSPCTN
            // 
            this.tbHSPCTN.Enabled = false;
            this.tbHSPCTN.Location = new System.Drawing.Point(197, 454);
            this.tbHSPCTN.Mask = "0.00";
            this.tbHSPCTN.Name = "tbHSPCTN";
            this.tbHSPCTN.Size = new System.Drawing.Size(100, 21);
            this.tbHSPCTN.TabIndex = 8;
            this.tbHSPCTN.Text = "000";
            // 
            // checkHSPCDH
            // 
            this.checkHSPCDH.AutoSize = true;
            this.checkHSPCDH.Location = new System.Drawing.Point(12, 430);
            this.checkHSPCDH.Name = "checkHSPCDH";
            this.checkHSPCDH.Size = new System.Drawing.Size(150, 19);
            this.checkHSPCDH.TabIndex = 8;
            this.checkHSPCDH.Text = "Hệ số Phụ cấp độc hại";
            this.checkHSPCDH.UseVisualStyleBackColor = true;
            this.checkHSPCDH.CheckedChanged += new System.EventHandler(this.Checked_Changed);
            // 
            // tbHSPCDH
            // 
            this.tbHSPCDH.Enabled = false;
            this.tbHSPCDH.Location = new System.Drawing.Point(197, 428);
            this.tbHSPCDH.Mask = "0.00";
            this.tbHSPCDH.Name = "tbHSPCDH";
            this.tbHSPCDH.Size = new System.Drawing.Size(100, 21);
            this.tbHSPCDH.TabIndex = 7;
            this.tbHSPCDH.Text = "000";
            // 
            // checkHSPCCV
            // 
            this.checkHSPCCV.AutoSize = true;
            this.checkHSPCCV.Location = new System.Drawing.Point(12, 404);
            this.checkHSPCCV.Name = "checkHSPCCV";
            this.checkHSPCCV.Size = new System.Drawing.Size(152, 19);
            this.checkHSPCCV.TabIndex = 8;
            this.checkHSPCCV.Text = "Hệ số Phụ cấp chức vụ";
            this.checkHSPCCV.UseVisualStyleBackColor = true;
            this.checkHSPCCV.CheckedChanged += new System.EventHandler(this.Checked_Changed);
            // 
            // tbHSPCCV
            // 
            this.tbHSPCCV.Enabled = false;
            this.tbHSPCCV.Location = new System.Drawing.Point(197, 402);
            this.tbHSPCCV.Mask = "0.00";
            this.tbHSPCCV.Name = "tbHSPCCV";
            this.tbHSPCCV.Size = new System.Drawing.Size(100, 21);
            this.tbHSPCCV.TabIndex = 6;
            this.tbHSPCCV.Text = "000";
            // 
            // checkNVNhanKiet
            // 
            this.checkNVNhanKiet.AutoSize = true;
            this.checkNVNhanKiet.Enabled = false;
            this.checkNVNhanKiet.Location = new System.Drawing.Point(197, 506);
            this.checkNVNhanKiet.Name = "checkNVNhanKiet";
            this.checkNVNhanKiet.Size = new System.Drawing.Size(175, 19);
            this.checkNVNhanKiet.TabIndex = 11;
            this.checkNVNhanKiet.Text = "Là Nhân viên Cty Nhân Kiệt";
            this.toolTipHint.SetToolTip(this.checkNVNhanKiet, "Đánh dấu mục này nếu NV đang làm việc\r\nvà chấm công. Bỏ đánh dấu nếu NV không\r\ncò" +
        "n làm việc.");
            this.checkNVNhanKiet.UseVisualStyleBackColor = true;
            // 
            // checkPhanNhomNhanVien
            // 
            this.checkPhanNhomNhanVien.AutoSize = true;
            this.checkPhanNhomNhanVien.Location = new System.Drawing.Point(12, 506);
            this.checkPhanNhomNhanVien.Name = "checkPhanNhomNhanVien";
            this.checkPhanNhomNhanVien.Size = new System.Drawing.Size(146, 19);
            this.checkPhanNhomNhanVien.TabIndex = 12;
            this.checkPhanNhomNhanVien.Text = "Phân nhóm nhân viên";
            this.checkPhanNhomNhanVien.UseVisualStyleBackColor = true;
            // 
            // frmCapNhatNVHangLoat
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(451, 568);
            this.Controls.Add(this.checkNVNhanKiet);
            this.Controls.Add(this.checkPhanNhomNhanVien);
            this.Controls.Add(this.checkUserEnabled);
            this.Controls.Add(this.tbHSPCCV);
            this.Controls.Add(this.tbHSPCDH);
            this.Controls.Add(this.tbHSPCTN);
            this.Controls.Add(this.tbHSLCV);
            this.Controls.Add(this.tbHSLCBTT17);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.tbHSLCB);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.cbLichTrinh);
            this.Controls.Add(this.cbChucVu);
            this.Controls.Add(this.checkHSPCCV);
            this.Controls.Add(this.treePhongBan);
            this.Controls.Add(this.checkHSPCDH);
            this.Controls.Add(this.checkLichtrinh);
            this.Controls.Add(this.checkHSPCTN);
            this.Controls.Add(this.checkTinhtrangHoatdong);
            this.Controls.Add(this.checkHSLCBTT17);
            this.Controls.Add(this.checkHSLCV);
            this.Controls.Add(this.checkHSLCB);
            this.Controls.Add(this.checkChucVu);
            this.Controls.Add(this.checkPhong);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmCapNhatNVHangLoat";
            this.Text = "Cập nhật thông tin nhân viên hàng loạt";
            this.Load += new System.EventHandler(this.frmCapNhatNVHangLoat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView treePhongBan;
		private System.Windows.Forms.CheckBox checkPhong;
		private System.Windows.Forms.CheckBox checkChucVu;
		private System.Windows.Forms.ComboBox cbChucVu;
		private System.Windows.Forms.CheckBox checkLichtrinh;
		private System.Windows.Forms.ComboBox cbLichTrinh;
		private System.Windows.Forms.CheckBox checkUserEnabled;
		private System.Windows.Forms.MaskedTextBox tbHSLCV;
		private System.Windows.Forms.MaskedTextBox tbHSLCB;
		private System.Windows.Forms.CheckBox checkHSLCB;
		private System.Windows.Forms.CheckBox checkHSLCV;
		private System.Windows.Forms.CheckBox checkTinhtrangHoatdong;
		private System.Windows.Forms.Button btnLuu;
		private System.Windows.Forms.Button btnDong;
		private System.Windows.Forms.ToolTip toolTipHint;
        private System.Windows.Forms.CheckBox checkHSLCBTT17;
        private System.Windows.Forms.CheckBox checkHSPCTN;
        private System.Windows.Forms.MaskedTextBox tbHSLCBTT17;
        private System.Windows.Forms.MaskedTextBox tbHSPCTN;
        private System.Windows.Forms.CheckBox checkHSPCDH;
        private System.Windows.Forms.MaskedTextBox tbHSPCDH;
        private System.Windows.Forms.CheckBox checkHSPCCV;
        private System.Windows.Forms.MaskedTextBox tbHSPCCV;
        private System.Windows.Forms.CheckBox checkNVNhanKiet;
        private System.Windows.Forms.CheckBox checkPhanNhomNhanVien;
    }
}