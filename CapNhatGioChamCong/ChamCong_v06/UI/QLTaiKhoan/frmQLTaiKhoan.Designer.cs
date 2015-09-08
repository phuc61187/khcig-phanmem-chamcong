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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("px thành phẩm");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("bảo vệ");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("văn phòng", new System.Windows.Forms.TreeNode[] {
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Nhà máy thuốc lá khánh hội", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3});
			this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
			this.btnLuuChucNang = new DevExpress.XtraEditors.SimpleButton();
			this.checkedListChucNang = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.btnThem = new DevExpress.XtraEditors.SimpleButton();
			this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
			this.btnEnableDisable = new DevExpress.XtraEditors.SimpleButton();
			this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
			this.btnSua = new DevExpress.XtraEditors.SimpleButton();
			this.btnResetPassword = new DevExpress.XtraEditors.SimpleButton();
			this.gridControlTaiKhoan = new DevExpress.XtraGrid.GridControl();
			this.gridViewTaiKhoan = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
			this.btnLuuPhong = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
			this.groupControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkedListChucNang)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
			this.groupControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlTaiKhoan)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewTaiKhoan)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
			this.groupControl3.SuspendLayout();
			this.SuspendLayout();
			// 
			// repositoryItemCheckEdit2
			// 
			this.repositoryItemCheckEdit2.AutoHeight = false;
			this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
			// 
			// groupControl1
			// 
			this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.groupControl1.AppearanceCaption.Options.UseFont = true;
			this.groupControl1.Controls.Add(this.btnLuuChucNang);
			this.groupControl1.Controls.Add(this.checkedListChucNang);
			this.groupControl1.Location = new System.Drawing.Point(575, 12);
			this.groupControl1.Name = "groupControl1";
			this.groupControl1.Size = new System.Drawing.Size(357, 430);
			this.groupControl1.TabIndex = 10;
			this.groupControl1.Text = "Danh sách chức năng";
			// 
			// btnLuuChucNang
			// 
			this.btnLuuChucNang.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.btnLuuChucNang.Appearance.Options.UseFont = true;
			this.btnLuuChucNang.Image = global::ChamCong_v06.Properties.Resources._1438546670_Save;
			this.btnLuuChucNang.Location = new System.Drawing.Point(275, 25);
			this.btnLuuChucNang.Name = "btnLuuChucNang";
			this.btnLuuChucNang.Size = new System.Drawing.Size(75, 23);
			this.btnLuuChucNang.TabIndex = 14;
			this.btnLuuChucNang.Text = "Lưu";
			this.btnLuuChucNang.Click += new System.EventHandler(this.btnLuuChucNang_Click);
			// 
			// checkedListChucNang
			// 
			this.checkedListChucNang.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F);
			this.checkedListChucNang.Appearance.Options.UseFont = true;
			this.checkedListChucNang.CheckOnClick = true;
			this.checkedListChucNang.Location = new System.Drawing.Point(5, 54);
			this.checkedListChucNang.Name = "checkedListChucNang";
			this.checkedListChucNang.Size = new System.Drawing.Size(345, 374);
			this.checkedListChucNang.TabIndex = 0;
			// 
			// btnThem
			// 
			this.btnThem.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.btnThem.Appearance.Options.UseFont = true;
			this.btnThem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnThem.Image = global::ChamCong_v06.Properties.Resources._1438546384_Add;
			this.btnThem.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
			this.btnThem.Location = new System.Drawing.Point(5, 25);
			this.btnThem.Name = "btnThem";
			this.btnThem.Size = new System.Drawing.Size(75, 23);
			this.btnThem.TabIndex = 7;
			this.btnThem.Text = "Tạo mới";
			this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
			// 
			// btnXoa
			// 
			this.btnXoa.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.btnXoa.Appearance.Options.UseFont = true;
			this.btnXoa.Image = global::ChamCong_v06.Properties.Resources._1438546065_Delete;
			this.btnXoa.Location = new System.Drawing.Point(192, 25);
			this.btnXoa.Name = "btnXoa";
			this.btnXoa.Size = new System.Drawing.Size(75, 23);
			this.btnXoa.TabIndex = 6;
			this.btnXoa.Text = "Xóa";
			this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
			// 
			// btnEnableDisable
			// 
			this.btnEnableDisable.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.btnEnableDisable.Appearance.Options.UseFont = true;
			this.btnEnableDisable.Location = new System.Drawing.Point(5, 52);
			this.btnEnableDisable.Name = "btnEnableDisable";
			this.btnEnableDisable.Size = new System.Drawing.Size(128, 23);
			this.btnEnableDisable.TabIndex = 6;
			this.btnEnableDisable.Text = "Enable/Disable";
			this.btnEnableDisable.Click += new System.EventHandler(this.btnDisable_Click);
			// 
			// groupControl2
			// 
			this.groupControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F);
			this.groupControl2.Appearance.Options.UseFont = true;
			this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.groupControl2.AppearanceCaption.Options.UseFont = true;
			this.groupControl2.Controls.Add(this.btnSua);
			this.groupControl2.Controls.Add(this.btnResetPassword);
			this.groupControl2.Controls.Add(this.btnEnableDisable);
			this.groupControl2.Controls.Add(this.gridControlTaiKhoan);
			this.groupControl2.Controls.Add(this.btnXoa);
			this.groupControl2.Controls.Add(this.btnThem);
			this.groupControl2.Location = new System.Drawing.Point(12, 12);
			this.groupControl2.Name = "groupControl2";
			this.groupControl2.Size = new System.Drawing.Size(275, 430);
			this.groupControl2.TabIndex = 11;
			this.groupControl2.Text = "Danh sách tài khoản";
			// 
			// btnSua
			// 
			this.btnSua.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.btnSua.Appearance.Options.UseFont = true;
			this.btnSua.Image = global::ChamCong_v06.Properties.Resources._1438546263_Edit;
			this.btnSua.Location = new System.Drawing.Point(98, 25);
			this.btnSua.Name = "btnSua";
			this.btnSua.Size = new System.Drawing.Size(75, 23);
			this.btnSua.TabIndex = 13;
			this.btnSua.Text = "Sửa";
			this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
			// 
			// btnResetPassword
			// 
			this.btnResetPassword.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.btnResetPassword.Appearance.Options.UseFont = true;
			this.btnResetPassword.Appearance.Options.UseTextOptions = true;
			this.btnResetPassword.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.btnResetPassword.Location = new System.Drawing.Point(139, 52);
			this.btnResetPassword.Name = "btnResetPassword";
			this.btnResetPassword.Size = new System.Drawing.Size(128, 23);
			this.btnResetPassword.TabIndex = 6;
			this.btnResetPassword.Text = "Reset Password";
			this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
			// 
			// gridControlTaiKhoan
			// 
			this.gridControlTaiKhoan.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlTaiKhoan.Location = new System.Drawing.Point(5, 83);
			this.gridControlTaiKhoan.MainView = this.gridViewTaiKhoan;
			this.gridControlTaiKhoan.Name = "gridControlTaiKhoan";
			this.gridControlTaiKhoan.Size = new System.Drawing.Size(262, 342);
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
			this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7.8F);
			this.gridColumn3.AppearanceCell.Options.UseFont = true;
			this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.gridColumn3.AppearanceHeader.Options.UseFont = true;
			this.gridColumn3.Caption = "Tài khoản";
			this.gridColumn3.FieldName = "UserAccount";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 0;
			this.gridColumn3.Width = 130;
			// 
			// gridColumn4
			// 
			this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.gridColumn4.AppearanceHeader.Options.UseFont = true;
			this.gridColumn4.Caption = "Sử dụng";
			this.gridColumn4.ColumnEdit = this.repositoryItemCheckEdit2;
			this.gridColumn4.FieldName = "Enable";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 1;
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
			this.treePhongBan.Font = new System.Drawing.Font("Tahoma", 7.8F);
			this.treePhongBan.HideSelection = false;
			this.treePhongBan.Indent = 18;
			this.treePhongBan.ItemHeight = 20;
			this.treePhongBan.Location = new System.Drawing.Point(2, 54);
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
			this.treePhongBan.Size = new System.Drawing.Size(269, 374);
			this.treePhongBan.TabIndex = 0;
			// 
			// groupControl3
			// 
			this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.groupControl3.AppearanceCaption.Options.UseFont = true;
			this.groupControl3.Controls.Add(this.btnLuuPhong);
			this.groupControl3.Controls.Add(this.treePhongBan);
			this.groupControl3.Location = new System.Drawing.Point(293, 12);
			this.groupControl3.Name = "groupControl3";
			this.groupControl3.Size = new System.Drawing.Size(276, 430);
			this.groupControl3.TabIndex = 13;
			this.groupControl3.Text = "Danh sách phòng ban được thao tác";
			// 
			// btnLuuPhong
			// 
			this.btnLuuPhong.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.btnLuuPhong.Appearance.Options.UseFont = true;
			this.btnLuuPhong.Image = global::ChamCong_v06.Properties.Resources._1438546670_Save;
			this.btnLuuPhong.Location = new System.Drawing.Point(196, 25);
			this.btnLuuPhong.Name = "btnLuuPhong";
			this.btnLuuPhong.Size = new System.Drawing.Size(75, 23);
			this.btnLuuPhong.TabIndex = 14;
			this.btnLuuPhong.Text = "Lưu";
			this.btnLuuPhong.Click += new System.EventHandler(this.btnLuuPhong_Click);
			// 
			// frmQLTaiKhoan
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(941, 451);
			this.Controls.Add(this.groupControl3);
			this.Controls.Add(this.groupControl2);
			this.Controls.Add(this.groupControl1);
			this.Font = new System.Drawing.Font("Tahoma", 7.8F);
			this.Name = "frmQLTaiKhoan";
			this.Text = "Quản lý tài khoản";
			this.Load += new System.EventHandler(this.frmQLTaiKhoan_Load);
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
			this.groupControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkedListChucNang)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
			this.groupControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlTaiKhoan)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewTaiKhoan)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
			this.groupControl3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.GroupControl groupControl1;
		private DevExpress.XtraEditors.SimpleButton btnThem;
		private DevExpress.XtraEditors.SimpleButton btnXoa;
		private DevExpress.XtraEditors.SimpleButton btnEnableDisable;
		private DevExpress.XtraEditors.GroupControl groupControl2;
		private DevExpress.XtraGrid.GridControl gridControlTaiKhoan;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewTaiKhoan;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private System.Windows.Forms.TreeView treePhongBan;
		private DevExpress.XtraEditors.GroupControl groupControl3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraEditors.SimpleButton btnLuuChucNang;
		private DevExpress.XtraEditors.CheckedListBoxControl checkedListChucNang;
		private DevExpress.XtraEditors.SimpleButton btnLuuPhong;
		private DevExpress.XtraEditors.SimpleButton btnResetPassword;
		private DevExpress.XtraEditors.SimpleButton btnSua;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
	}
}