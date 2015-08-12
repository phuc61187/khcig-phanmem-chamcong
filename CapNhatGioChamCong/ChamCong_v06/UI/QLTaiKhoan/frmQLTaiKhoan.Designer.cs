namespace ChamCong_v06.UI.QLTaiKhoan {
	partial class frmQLTaiKhoan {
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
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("px thành phẩm");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("bảo vệ");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("văn phòng", new System.Windows.Forms.TreeNode[] {
            treeNode6});
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Nhà máy thuốc lá khánh hội", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode7});
			this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
			this.btnThem = new DevExpress.XtraEditors.SimpleButton();
			this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
			this.gridControlTaiKhoan = new DevExpress.XtraGrid.GridControl();
			this.gridViewTaiKhoan = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
			this.btnLuuPhong = new DevExpress.XtraEditors.SimpleButton();
			this.checkedListChucNang = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.btnLuuChucNang = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
			this.groupControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
			this.groupControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlTaiKhoan)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewTaiKhoan)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
			this.groupControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkedListChucNang)).BeginInit();
			this.SuspendLayout();
			// 
			// groupControl1
			// 
			this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.groupControl1.AppearanceCaption.Options.UseFont = true;
			this.groupControl1.Controls.Add(this.btnLuuChucNang);
			this.groupControl1.Controls.Add(this.checkedListChucNang);
			this.groupControl1.Location = new System.Drawing.Point(582, 12);
			this.groupControl1.Name = "groupControl1";
			this.groupControl1.Size = new System.Drawing.Size(357, 430);
			this.groupControl1.TabIndex = 10;
			this.groupControl1.Text = "Danh sách chức năng";
			// 
			// btnThem
			// 
			this.btnThem.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThem.Appearance.Options.UseFont = true;
			this.btnThem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnThem.Image = global::ChamCong_v06.Properties.Resources._1438546384_Add;
			this.btnThem.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
			this.btnThem.Location = new System.Drawing.Point(5, 25);
			this.btnThem.Name = "btnThem";
			this.btnThem.Size = new System.Drawing.Size(75, 23);
			this.btnThem.TabIndex = 7;
			this.btnThem.Text = "Tạo mới";
			// 
			// btnXoa
			// 
			this.btnXoa.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnXoa.Appearance.Options.UseFont = true;
			this.btnXoa.Image = global::ChamCong_v06.Properties.Resources._1438546065_Delete;
			this.btnXoa.Location = new System.Drawing.Point(86, 25);
			this.btnXoa.Name = "btnXoa";
			this.btnXoa.Size = new System.Drawing.Size(57, 23);
			this.btnXoa.TabIndex = 6;
			this.btnXoa.Text = "Xóa";
			this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
			// 
			// simpleButton1
			// 
			this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.simpleButton1.Appearance.Options.UseFont = true;
			this.simpleButton1.Image = global::ChamCong_v06.Properties.Resources._1438546065_Delete;
			this.simpleButton1.Location = new System.Drawing.Point(149, 25);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(84, 23);
			this.simpleButton1.TabIndex = 6;
			this.simpleButton1.Text = "Disable";
			// 
			// groupControl2
			// 
			this.groupControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.groupControl2.Appearance.Options.UseFont = true;
			this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.groupControl2.AppearanceCaption.Options.UseFont = true;
			this.groupControl2.Controls.Add(this.simpleButton1);
			this.groupControl2.Controls.Add(this.gridControlTaiKhoan);
			this.groupControl2.Controls.Add(this.btnXoa);
			this.groupControl2.Controls.Add(this.btnThem);
			this.groupControl2.Location = new System.Drawing.Point(12, 12);
			this.groupControl2.Name = "groupControl2";
			this.groupControl2.Size = new System.Drawing.Size(301, 430);
			this.groupControl2.TabIndex = 11;
			this.groupControl2.Text = "Danh sách tài khoản";
			// 
			// gridControlTaiKhoan
			// 
			this.gridControlTaiKhoan.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlTaiKhoan.Location = new System.Drawing.Point(5, 54);
			this.gridControlTaiKhoan.MainView = this.gridViewTaiKhoan;
			this.gridControlTaiKhoan.Name = "gridControlTaiKhoan";
			this.gridControlTaiKhoan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
			this.gridControlTaiKhoan.Size = new System.Drawing.Size(291, 374);
			this.gridControlTaiKhoan.TabIndex = 12;
			this.gridControlTaiKhoan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTaiKhoan});
			// 
			// gridViewTaiKhoan
			// 
			this.gridViewTaiKhoan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
			this.gridViewTaiKhoan.GridControl = this.gridControlTaiKhoan;
			this.gridViewTaiKhoan.Name = "gridViewTaiKhoan";
			this.gridViewTaiKhoan.OptionsBehavior.Editable = false;
			this.gridViewTaiKhoan.OptionsBehavior.ReadOnly = true;
			this.gridViewTaiKhoan.OptionsPrint.AutoWidth = false;
			this.gridViewTaiKhoan.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
			this.gridViewTaiKhoan.OptionsView.ColumnAutoWidth = false;
			this.gridViewTaiKhoan.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn3
			// 
			this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.gridColumn3.AppearanceCell.Options.UseFont = true;
			this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.gridColumn3.AppearanceHeader.Options.UseFont = true;
			this.gridColumn3.Caption = "Tài khoản";
			this.gridColumn3.FieldName = "UserAccount";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 0;
			this.gridColumn3.Width = 150;
			// 
			// gridColumn4
			// 
			this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.gridColumn4.AppearanceHeader.Options.UseFont = true;
			this.gridColumn4.Caption = "Sử dụng";
			this.gridColumn4.ColumnEdit = this.repositoryItemCheckEdit2;
			this.gridColumn4.FieldName = "Enable";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 1;
			// 
			// repositoryItemCheckEdit2
			// 
			this.repositoryItemCheckEdit2.AutoHeight = false;
			this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
			// 
			// gridColumn5
			// 
			this.gridColumn5.Caption = "UserID_hide";
			this.gridColumn5.FieldName = "UserID";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 2;
			// 
			// treePhongBan
			// 
			this.treePhongBan.CheckBoxes = true;
			this.treePhongBan.Font = new System.Drawing.Font("Tahoma", 9F);
			this.treePhongBan.HideSelection = false;
			this.treePhongBan.Indent = 18;
			this.treePhongBan.ItemHeight = 20;
			this.treePhongBan.Location = new System.Drawing.Point(2, 54);
			this.treePhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.treePhongBan.Name = "treePhongBan";
			treeNode5.Name = "Node2";
			treeNode5.Text = "px thành phẩm";
			treeNode6.Name = "Node5";
			treeNode6.Text = "bảo vệ";
			treeNode7.Name = "Node4";
			treeNode7.Text = "văn phòng";
			treeNode8.Name = "Node0";
			treeNode8.Text = "Nhà máy thuốc lá khánh hội";
			this.treePhongBan.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8});
			this.treePhongBan.ShowNodeToolTips = true;
			this.treePhongBan.Size = new System.Drawing.Size(255, 374);
			this.treePhongBan.TabIndex = 0;
			// 
			// groupControl3
			// 
			this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.groupControl3.AppearanceCaption.Options.UseFont = true;
			this.groupControl3.Controls.Add(this.btnLuuPhong);
			this.groupControl3.Controls.Add(this.treePhongBan);
			this.groupControl3.Location = new System.Drawing.Point(317, 12);
			this.groupControl3.Name = "groupControl3";
			this.groupControl3.Size = new System.Drawing.Size(259, 430);
			this.groupControl3.TabIndex = 13;
			this.groupControl3.Text = "Danh sách phòng ban được thao tác";
			// 
			// btnLuuPhong
			// 
			this.btnLuuPhong.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnLuuPhong.Appearance.Options.UseFont = true;
			this.btnLuuPhong.Image = global::ChamCong_v06.Properties.Resources._1438546670_Save;
			this.btnLuuPhong.Location = new System.Drawing.Point(179, 25);
			this.btnLuuPhong.Name = "btnLuuPhong";
			this.btnLuuPhong.Size = new System.Drawing.Size(75, 23);
			this.btnLuuPhong.TabIndex = 14;
			this.btnLuuPhong.Text = "Lưu";
			// 
			// checkedListChucNang
			// 
			this.checkedListChucNang.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.checkedListChucNang.Appearance.Options.UseFont = true;
			this.checkedListChucNang.Location = new System.Drawing.Point(5, 54);
			this.checkedListChucNang.Name = "checkedListChucNang";
			this.checkedListChucNang.Size = new System.Drawing.Size(345, 371);
			this.checkedListChucNang.TabIndex = 0;
			// 
			// btnLuuChucNang
			// 
			this.btnLuuChucNang.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnLuuChucNang.Appearance.Options.UseFont = true;
			this.btnLuuChucNang.Image = global::ChamCong_v06.Properties.Resources._1438546670_Save;
			this.btnLuuChucNang.Location = new System.Drawing.Point(275, 25);
			this.btnLuuChucNang.Name = "btnLuuChucNang";
			this.btnLuuChucNang.Size = new System.Drawing.Size(75, 23);
			this.btnLuuChucNang.TabIndex = 14;
			this.btnLuuChucNang.Text = "Lưu";
			// 
			// frmQLTaiKhoan
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1077, 504);
			this.Controls.Add(this.groupControl3);
			this.Controls.Add(this.groupControl2);
			this.Controls.Add(this.groupControl1);
			this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "frmQLTaiKhoan";
			this.Text = "Quản lý tài khoản";
			this.Load += new System.EventHandler(this.frmQLTaiKhoan_Load);
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
			this.groupControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
			this.groupControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlTaiKhoan)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewTaiKhoan)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
			this.groupControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkedListChucNang)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.GroupControl groupControl1;
		private DevExpress.XtraEditors.SimpleButton btnThem;
		private DevExpress.XtraEditors.SimpleButton btnXoa;
		private DevExpress.XtraEditors.SimpleButton simpleButton1;
		private DevExpress.XtraEditors.GroupControl groupControl2;
		private DevExpress.XtraGrid.GridControl gridControlTaiKhoan;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewTaiKhoan;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
		private System.Windows.Forms.TreeView treePhongBan;
		private DevExpress.XtraEditors.GroupControl groupControl3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraEditors.SimpleButton btnLuuChucNang;
		private DevExpress.XtraEditors.CheckedListBoxControl checkedListChucNang;
		private DevExpress.XtraEditors.SimpleButton btnLuuPhong;
	}
}