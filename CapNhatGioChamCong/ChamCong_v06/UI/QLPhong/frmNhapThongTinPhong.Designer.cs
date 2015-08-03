namespace ChamCong_v06.UI.QLPhong {
	partial class frmNhapThongTinPhong {
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("px thành phẩm");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("bảo vệ");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("văn phòng", new System.Windows.Forms.TreeNode[] {
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Nhà máy thuốc lá khánh hội", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3});
			this.checkEnable = new System.Windows.Forms.CheckBox();
			this.btnTenPhong = new DevExpress.XtraEditors.ButtonEdit();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
			this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
			this.tbVitriPhong = new DevExpress.XtraEditors.TextEdit();
			this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			((System.ComponentModel.ISupportInitialize)(this.btnTenPhong.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbVitriPhong.Properties)).BeginInit();
			this.gpChonPhongBan.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkEnable
			// 
			this.checkEnable.AutoSize = true;
			this.checkEnable.Location = new System.Drawing.Point(95, 64);
			this.checkEnable.Name = "checkEnable";
			this.checkEnable.Size = new System.Drawing.Size(68, 20);
			this.checkEnable.TabIndex = 16;
			this.checkEnable.Text = "Enable";
			this.checkEnable.UseVisualStyleBackColor = true;
			// 
			// btnTenPhong
			// 
			this.btnTenPhong.Location = new System.Drawing.Point(95, 12);
			this.btnTenPhong.Name = "btnTenPhong";
			this.btnTenPhong.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnTenPhong.Properties.Appearance.Options.UseFont = true;
			this.btnTenPhong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
			this.btnTenPhong.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnTenPhong_Properties_ClearButtonClick);
			this.btnTenPhong.Size = new System.Drawing.Size(237, 20);
			this.btnTenPhong.TabIndex = 13;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 66);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(67, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Trạng thái";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 41);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 16);
			this.label3.TabIndex = 11;
			this.label3.Text = "Vị trí phòng";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 16);
			this.label1.TabIndex = 12;
			this.label1.Text = "Tên phòng";
			// 
			// btnHuy
			// 
			this.btnHuy.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnHuy.Appearance.Options.UseFont = true;
			this.btnHuy.Image = global::ChamCong_v06.Properties.Resources._1438546065_Delete;
			this.btnHuy.Location = new System.Drawing.Point(176, 380);
			this.btnHuy.Name = "btnHuy";
			this.btnHuy.Size = new System.Drawing.Size(75, 23);
			this.btnHuy.TabIndex = 7;
			this.btnHuy.Text = "Hủy";
			this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
			// 
			// btnLuu
			// 
			this.btnLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnLuu.Appearance.Options.UseFont = true;
			this.btnLuu.Image = global::ChamCong_v06.Properties.Resources._1438546670_Save;
			this.btnLuu.Location = new System.Drawing.Point(95, 380);
			this.btnLuu.Name = "btnLuu";
			this.btnLuu.Size = new System.Drawing.Size(75, 23);
			this.btnLuu.TabIndex = 8;
			this.btnLuu.Text = "Lưu";
			this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
			// 
			// tbVitriPhong
			// 
			this.tbVitriPhong.Location = new System.Drawing.Point(95, 38);
			this.tbVitriPhong.Name = "tbVitriPhong";
			this.tbVitriPhong.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.tbVitriPhong.Properties.Appearance.Options.UseFont = true;
			this.tbVitriPhong.Size = new System.Drawing.Size(75, 20);
			this.tbVitriPhong.TabIndex = 14;
			// 
			// gpChonPhongBan
			// 
			this.gpChonPhongBan.Controls.Add(this.treePhongBan);
			this.gpChonPhongBan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.gpChonPhongBan.Location = new System.Drawing.Point(12, 89);
			this.gpChonPhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gpChonPhongBan.Name = "gpChonPhongBan";
			this.gpChonPhongBan.Padding = new System.Windows.Forms.Padding(3, 6, 3, 2);
			this.gpChonPhongBan.Size = new System.Drawing.Size(320, 283);
			this.gpChonPhongBan.TabIndex = 17;
			this.gpChonPhongBan.TabStop = false;
			this.gpChonPhongBan.Text = "Chọn bộ phận trực thuộc";
			// 
			// treePhongBan
			// 
			this.treePhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treePhongBan.Font = new System.Drawing.Font("Tahoma", 9F);
			this.treePhongBan.HideSelection = false;
			this.treePhongBan.Indent = 18;
			this.treePhongBan.ItemHeight = 20;
			this.treePhongBan.Location = new System.Drawing.Point(3, 25);
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
			this.treePhongBan.Size = new System.Drawing.Size(314, 256);
			this.treePhongBan.TabIndex = 0;
			// 
			// frmNhapThongTinPhong
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(347, 414);
			this.Controls.Add(this.gpChonPhongBan);
			this.Controls.Add(this.checkEnable);
			this.Controls.Add(this.btnTenPhong);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnHuy);
			this.Controls.Add(this.btnLuu);
			this.Controls.Add(this.tbVitriPhong);
			this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "frmNhapThongTinPhong";
			this.Text = "Thông tin phòng ban";
			this.Load += new System.EventHandler(this.frmThemPhongBan_Load);
			((System.ComponentModel.ISupportInitialize)(this.btnTenPhong.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbVitriPhong.Properties)).EndInit();
			this.gpChonPhongBan.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkEnable;
		private DevExpress.XtraEditors.ButtonEdit btnTenPhong;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private DevExpress.XtraEditors.SimpleButton btnHuy;
		private DevExpress.XtraEditors.SimpleButton btnLuu;
		private DevExpress.XtraEditors.TextEdit tbVitriPhong;
		private System.Windows.Forms.GroupBox gpChonPhongBan;
		private System.Windows.Forms.TreeView treePhongBan;
	}
}