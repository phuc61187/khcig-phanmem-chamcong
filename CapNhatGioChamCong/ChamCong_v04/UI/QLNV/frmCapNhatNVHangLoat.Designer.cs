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
			this.SuspendLayout();
			// 
			// treePhongBan
			// 
			this.treePhongBan.Enabled = false;
			this.treePhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.treePhongBan.Indent = 18;
			this.treePhongBan.ItemHeight = 20;
			this.treePhongBan.Location = new System.Drawing.Point(165, 11);
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
			this.treePhongBan.Size = new System.Drawing.Size(200, 80);
			this.treePhongBan.TabIndex = 1;
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
			this.checkChucVu.Location = new System.Drawing.Point(12, 98);
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
			this.cbChucVu.Location = new System.Drawing.Point(165, 96);
			this.cbChucVu.Name = "cbChucVu";
			this.cbChucVu.Size = new System.Drawing.Size(200, 23);
			this.cbChucVu.TabIndex = 3;
			// 
			// checkLichtrinh
			// 
			this.checkLichtrinh.AutoSize = true;
			this.checkLichtrinh.Location = new System.Drawing.Point(12, 123);
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
			this.cbLichTrinh.Location = new System.Drawing.Point(165, 125);
			this.cbLichTrinh.Name = "cbLichTrinh";
			this.cbLichTrinh.Size = new System.Drawing.Size(200, 23);
			this.cbLichTrinh.TabIndex = 5;
			// 
			// checkUserEnabled
			// 
			this.checkUserEnabled.AutoSize = true;
			this.checkUserEnabled.Enabled = false;
			this.checkUserEnabled.Location = new System.Drawing.Point(165, 206);
			this.checkUserEnabled.Name = "checkUserEnabled";
			this.checkUserEnabled.Size = new System.Drawing.Size(164, 19);
			this.checkUserEnabled.TabIndex = 11;
			this.checkUserEnabled.Text = "Cho phép (UserEnabled)";
			this.toolTipHint.SetToolTip(this.checkUserEnabled, "Đánh dấu mục này nếu NV đang làm việc\r\nvà chấm công. Bỏ đánh dấu nếu NV không\r\ncò" +
        "n làm việc.");
			this.checkUserEnabled.UseVisualStyleBackColor = true;
			// 
			// tbHSLCV
			// 
			this.tbHSLCV.Enabled = false;
			this.tbHSLCV.Location = new System.Drawing.Point(165, 179);
			this.tbHSLCV.Mask = "0.00";
			this.tbHSLCV.Name = "tbHSLCV";
			this.tbHSLCV.Size = new System.Drawing.Size(100, 21);
			this.tbHSLCV.TabIndex = 9;
			this.tbHSLCV.Text = "000";
			// 
			// tbHSLCB
			// 
			this.tbHSLCB.Enabled = false;
			this.tbHSLCB.Location = new System.Drawing.Point(165, 154);
			this.tbHSLCB.Mask = "0.00";
			this.tbHSLCB.Name = "tbHSLCB";
			this.tbHSLCB.Size = new System.Drawing.Size(100, 21);
			this.tbHSLCB.TabIndex = 7;
			this.tbHSLCB.Text = "000";
			// 
			// checkHSLCB
			// 
			this.checkHSLCB.AutoSize = true;
			this.checkHSLCB.Location = new System.Drawing.Point(12, 156);
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
			this.checkHSLCV.Location = new System.Drawing.Point(12, 181);
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
			this.checkTinhtrangHoatdong.Location = new System.Drawing.Point(12, 206);
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
			this.btnLuu.Location = new System.Drawing.Point(165, 231);
			this.btnLuu.Name = "btnLuu";
			this.btnLuu.Size = new System.Drawing.Size(90, 27);
			this.btnLuu.TabIndex = 12;
			this.btnLuu.Text = "Lưu";
			this.btnLuu.UseVisualStyleBackColor = true;
			this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
			// 
			// btnDong
			// 
			this.btnDong.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnDong.ForeColor = System.Drawing.Color.Blue;
			this.btnDong.Location = new System.Drawing.Point(282, 231);
			this.btnDong.Name = "btnDong";
			this.btnDong.Size = new System.Drawing.Size(90, 27);
			this.btnDong.TabIndex = 13;
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
			// frmCapNhatNVHangLoat
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(377, 267);
			this.Controls.Add(this.checkUserEnabled);
			this.Controls.Add(this.tbHSLCV);
			this.Controls.Add(this.btnDong);
			this.Controls.Add(this.tbHSLCB);
			this.Controls.Add(this.btnLuu);
			this.Controls.Add(this.cbLichTrinh);
			this.Controls.Add(this.cbChucVu);
			this.Controls.Add(this.treePhongBan);
			this.Controls.Add(this.checkLichtrinh);
			this.Controls.Add(this.checkTinhtrangHoatdong);
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
	}
}