namespace ChamCong_v06.UI.QLLichTrinh {
	partial class frmQLLichTrinh {
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
			this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
			this.btnThem = new DevExpress.XtraEditors.SimpleButton();
			this.gridControlLichTrinh = new DevExpress.XtraGrid.GridControl();
			this.gridViewLichTrinh = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridControlCa = new DevExpress.XtraGrid.GridControl();
			this.gridViewCa = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.btnThemCa = new DevExpress.XtraEditors.SimpleButton();
			this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
			this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
			this.btnDong = new DevExpress.XtraEditors.SimpleButton();
			this.btnXoaCa = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.gridControlLichTrinh)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewLichTrinh)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlCa)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewCa)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
			this.groupControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
			this.groupControl2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnXoa
			// 
			this.btnXoa.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnXoa.Appearance.Options.UseFont = true;
			this.btnXoa.Image = global::ChamCong_v06.Properties.Resources._1438546065_Delete;
			this.btnXoa.Location = new System.Drawing.Point(86, 25);
			this.btnXoa.Name = "btnXoa";
			this.btnXoa.Size = new System.Drawing.Size(75, 23);
			this.btnXoa.TabIndex = 6;
			this.btnXoa.Text = "Xóa";
			this.btnXoa.Click += new System.EventHandler(this.btnXoaLichTrinh_Click);
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
			this.btnThem.Text = "Thêm";
			this.btnThem.Click += new System.EventHandler(this.btnThemLichTrinh_Click);
			// 
			// gridControlLichTrinh
			// 
			this.gridControlLichTrinh.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlLichTrinh.Location = new System.Drawing.Point(5, 54);
			this.gridControlLichTrinh.MainView = this.gridViewLichTrinh;
			this.gridControlLichTrinh.Name = "gridControlLichTrinh";
			this.gridControlLichTrinh.Size = new System.Drawing.Size(191, 371);
			this.gridControlLichTrinh.TabIndex = 8;
			this.gridControlLichTrinh.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLichTrinh});
			// 
			// gridViewLichTrinh
			// 
			this.gridViewLichTrinh.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
			this.gridViewLichTrinh.GridControl = this.gridControlLichTrinh;
			this.gridViewLichTrinh.Name = "gridViewLichTrinh";
			this.gridViewLichTrinh.OptionsBehavior.Editable = false;
			this.gridViewLichTrinh.OptionsBehavior.ReadOnly = true;
			this.gridViewLichTrinh.OptionsPrint.AutoWidth = false;
			this.gridViewLichTrinh.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
			this.gridViewLichTrinh.OptionsSelection.MultiSelect = true;
			this.gridViewLichTrinh.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
			this.gridViewLichTrinh.OptionsView.ColumnAutoWidth = false;
			this.gridViewLichTrinh.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn1
			// 
			this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.gridColumn1.AppearanceCell.Options.UseFont = true;
			this.gridColumn1.Caption = "ID_hide";
			this.gridColumn1.FieldName = "SchID";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 2;
			// 
			// gridColumn2
			// 
			this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.gridColumn2.AppearanceCell.Options.UseFont = true;
			this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.gridColumn2.AppearanceHeader.Options.UseFont = true;
			this.gridColumn2.Caption = "Lịch trình";
			this.gridColumn2.FieldName = "SchName";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.ReadOnly = true;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			this.gridColumn2.Width = 125;
			// 
			// gridControlCa
			// 
			this.gridControlCa.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlCa.Location = new System.Drawing.Point(5, 54);
			this.gridControlCa.MainView = this.gridViewCa;
			this.gridControlCa.Name = "gridControlCa";
			this.gridControlCa.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
			this.gridControlCa.Size = new System.Drawing.Size(314, 371);
			this.gridControlCa.TabIndex = 8;
			this.gridControlCa.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCa});
			// 
			// gridViewCa
			// 
			this.gridViewCa.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
			this.gridViewCa.GridControl = this.gridControlCa;
			this.gridViewCa.Name = "gridViewCa";
			this.gridViewCa.OptionsBehavior.ReadOnly = true;
			this.gridViewCa.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
			this.gridViewCa.OptionsSelection.MultiSelect = true;
			this.gridViewCa.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
			this.gridViewCa.OptionsView.ColumnAutoWidth = false;
			this.gridViewCa.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn3
			// 
			this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.gridColumn3.AppearanceCell.Options.UseFont = true;
			this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.gridColumn3.AppearanceHeader.Options.UseFont = true;
			this.gridColumn3.Caption = "Ca làm việc";
			this.gridColumn3.FieldName = "ShiftCode";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.ReadOnly = true;
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 1;
			this.gridColumn3.Width = 150;
			// 
			// gridColumn4
			// 
			this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.gridColumn4.AppearanceCell.Options.UseFont = true;
			this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.gridColumn4.AppearanceHeader.Options.UseFont = true;
			this.gridColumn4.Caption = "Đang sử dụng";
			this.gridColumn4.ColumnEdit = this.repositoryItemCheckEdit1;
			this.gridColumn4.FieldName = "Enable";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.ReadOnly = true;
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 2;
			this.gridColumn4.Width = 100;
			// 
			// repositoryItemCheckEdit1
			// 
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			// 
			// gridColumn5
			// 
			this.gridColumn5.Caption = "ShiftID_T1_Hide";
			this.gridColumn5.FieldName = "T1";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.OptionsColumn.ReadOnly = true;
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 3;
			// 
			// btnThemCa
			// 
			this.btnThemCa.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThemCa.Appearance.Options.UseFont = true;
			this.btnThemCa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnThemCa.Image = global::ChamCong_v06.Properties.Resources._1438546384_Add;
			this.btnThemCa.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
			this.btnThemCa.Location = new System.Drawing.Point(5, 25);
			this.btnThemCa.Name = "btnThemCa";
			this.btnThemCa.Size = new System.Drawing.Size(90, 23);
			this.btnThemCa.TabIndex = 7;
			this.btnThemCa.Text = "Thêm ca";
			this.btnThemCa.Click += new System.EventHandler(this.btnThemCa_Click);
			// 
			// groupControl1
			// 
			this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.groupControl1.AppearanceCaption.Options.UseFont = true;
			this.groupControl1.Controls.Add(this.gridControlLichTrinh);
			this.groupControl1.Controls.Add(this.btnThem);
			this.groupControl1.Controls.Add(this.btnXoa);
			this.groupControl1.Location = new System.Drawing.Point(12, 12);
			this.groupControl1.Name = "groupControl1";
			this.groupControl1.Size = new System.Drawing.Size(201, 430);
			this.groupControl1.TabIndex = 9;
			this.groupControl1.Text = "Lịch trình";
			// 
			// groupControl2
			// 
			this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.groupControl2.AppearanceCaption.Options.UseFont = true;
			this.groupControl2.Controls.Add(this.btnDong);
			this.groupControl2.Controls.Add(this.gridControlCa);
			this.groupControl2.Controls.Add(this.btnThemCa);
			this.groupControl2.Controls.Add(this.btnXoaCa);
			this.groupControl2.Location = new System.Drawing.Point(219, 12);
			this.groupControl2.Name = "groupControl2";
			this.groupControl2.Size = new System.Drawing.Size(324, 430);
			this.groupControl2.TabIndex = 10;
			this.groupControl2.Text = "Danh sách ca theo lịch trình";
			// 
			// btnDong
			// 
			this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnDong.Appearance.Options.UseFont = true;
			this.btnDong.Image = global::ChamCong_v06.Properties.Resources._1438546227_Log_Out;
			this.btnDong.Location = new System.Drawing.Point(197, 25);
			this.btnDong.Name = "btnDong";
			this.btnDong.Size = new System.Drawing.Size(75, 23);
			this.btnDong.TabIndex = 11;
			this.btnDong.Text = "Đóng";
			this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
			// 
			// btnXoaCa
			// 
			this.btnXoaCa.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnXoaCa.Appearance.Options.UseFont = true;
			this.btnXoaCa.Image = global::ChamCong_v06.Properties.Resources._1438546065_Delete;
			this.btnXoaCa.Location = new System.Drawing.Point(101, 25);
			this.btnXoaCa.Name = "btnXoaCa";
			this.btnXoaCa.Size = new System.Drawing.Size(90, 23);
			this.btnXoaCa.TabIndex = 6;
			this.btnXoaCa.Text = "Xóa ca";
			this.btnXoaCa.Click += new System.EventHandler(this.btnXoaCa_Click);
			// 
			// frmQLLichTrinh
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(555, 453);
			this.Controls.Add(this.groupControl2);
			this.Controls.Add(this.groupControl1);
			this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "frmQLLichTrinh";
			this.Text = "Quản lý lịch trình";
			this.Load += new System.EventHandler(this.frmQLLichTrinh_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridControlLichTrinh)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewLichTrinh)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlCa)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewCa)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
			this.groupControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
			this.groupControl2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton btnXoa;
		private DevExpress.XtraEditors.SimpleButton btnThem;
		private DevExpress.XtraGrid.GridControl gridControlLichTrinh;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewLichTrinh;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.GridControl gridControlCa;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewCa;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraEditors.SimpleButton btnThemCa;
		private DevExpress.XtraEditors.GroupControl groupControl1;
		private DevExpress.XtraEditors.GroupControl groupControl2;
		private DevExpress.XtraEditors.SimpleButton btnXoaCa;
		private DevExpress.XtraEditors.SimpleButton btnDong;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
	}
}