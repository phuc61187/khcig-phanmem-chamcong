namespace ChamCong_v06.UI.QLLichTrinh {
	partial class frmQLCa {
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
			this.gridControl = new DevExpress.XtraGrid.GridControl();
			this.gridViewShift = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.btnSua = new DevExpress.XtraEditors.SimpleButton();
			this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
			this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
			this.btnThem = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewShift)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControl
			// 
			this.gridControl.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControl.Location = new System.Drawing.Point(15, 26);
			this.gridControl.MainView = this.gridViewShift;
			this.gridControl.Name = "gridControl";
			this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
			this.gridControl.Size = new System.Drawing.Size(900, 414);
			this.gridControl.TabIndex = 0;
			this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewShift});
			// 
			// gridViewShift
			// 
			this.gridViewShift.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.gridViewShift.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewShift.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridViewShift.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridViewShift.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 7.8F);
			this.gridViewShift.Appearance.Row.Options.UseFont = true;
			this.gridViewShift.ColumnPanelRowHeight = 40;
			this.gridViewShift.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn14,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn8,
            this.gridColumn6,
            this.gridColumn11,
            this.gridColumn7,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn15});
			this.gridViewShift.GridControl = this.gridControl;
			this.gridViewShift.Name = "gridViewShift";
			this.gridViewShift.OptionsView.ColumnAutoWidth = false;
			// 
			// gridColumn14
			// 
			this.gridColumn14.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.gridColumn14.AppearanceHeader.Options.UseFont = true;
			this.gridColumn14.Caption = "Enable";
			this.gridColumn14.ColumnEdit = this.repositoryItemCheckEdit1;
			this.gridColumn14.FieldName = "Enable";
			this.gridColumn14.Name = "gridColumn14";
			this.gridColumn14.OptionsColumn.AllowEdit = false;
			this.gridColumn14.OptionsColumn.ReadOnly = true;
			this.gridColumn14.Visible = true;
			this.gridColumn14.VisibleIndex = 0;
			this.gridColumn14.Width = 50;
			// 
			// repositoryItemCheckEdit1
			// 
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "Ký hiệu";
			this.gridColumn1.FieldName = "ShiftCode";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.OptionsColumn.ReadOnly = true;
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 1;
			this.gridColumn1.Width = 100;
			// 
			// gridColumn2
			// 
			this.gridColumn2.Caption = "BĐ Ca";
			this.gridColumn2.DisplayFormat.FormatString = "%h\\:mm";
			this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.gridColumn2.FieldName = "OnDuty";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.OptionsColumn.ReadOnly = true;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 2;
			this.gridColumn2.Width = 50;
			// 
			// gridColumn3
			// 
			this.gridColumn3.Caption = "KT Ca";
			this.gridColumn3.DisplayFormat.FormatString = "%h\\:mm";
			this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.gridColumn3.FieldName = "OffDuty";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.OptionsColumn.ReadOnly = true;
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 3;
			this.gridColumn3.Width = 50;
			// 
			// gridColumn9
			// 
			this.gridColumn9.Caption = "Chấm công";
			this.gridColumn9.DisplayFormat.FormatString = "#0.0#";
			this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn9.FieldName = "Workingday";
			this.gridColumn9.Name = "gridColumn9";
			this.gridColumn9.OptionsColumn.AllowEdit = false;
			this.gridColumn9.OptionsColumn.ReadOnly = true;
			this.gridColumn9.Visible = true;
			this.gridColumn9.VisibleIndex = 5;
			this.gridColumn9.Width = 50;
			// 
			// gridColumn10
			// 
			this.gridColumn10.Caption = "Tổng giờ làm";
			this.gridColumn10.DisplayFormat.FormatString = "%h\\:mm";
			this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.gridColumn10.FieldName = "WorkingTime";
			this.gridColumn10.Name = "gridColumn10";
			this.gridColumn10.OptionsColumn.AllowEdit = false;
			this.gridColumn10.OptionsColumn.ReadOnly = true;
			this.gridColumn10.Visible = true;
			this.gridColumn10.VisibleIndex = 4;
			this.gridColumn10.Width = 55;
			// 
			// gridColumn4
			// 
			this.gridColumn4.Caption = "Trễ (phút)";
			this.gridColumn4.FieldName = "LateGrace";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.AllowEdit = false;
			this.gridColumn4.OptionsColumn.ReadOnly = true;
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 6;
			this.gridColumn4.Width = 50;
			// 
			// gridColumn5
			// 
			this.gridColumn5.Caption = "Sớm (phút)";
			this.gridColumn5.FieldName = "EarlyGrace";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.OptionsColumn.AllowEdit = false;
			this.gridColumn5.OptionsColumn.ReadOnly = true;
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 7;
			this.gridColumn5.Width = 50;
			// 
			// gridColumn8
			// 
			this.gridColumn8.Caption = "Làm thêm tối thiểu (phút)";
			this.gridColumn8.FieldName = "AfterOT";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.OptionsColumn.AllowEdit = false;
			this.gridColumn8.OptionsColumn.ReadOnly = true;
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 8;
			this.gridColumn8.Width = 90;
			// 
			// gridColumn6
			// 
			this.gridColumn6.Caption = "Cho vào từ";
			this.gridColumn6.DisplayFormat.FormatString = "%h\\:mm";
			this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.gridColumn6.FieldName = "OnTimeIn";
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.OptionsColumn.AllowEdit = false;
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 9;
			this.gridColumn6.Width = 60;
			// 
			// gridColumn11
			// 
			this.gridColumn11.Caption = "Cho vào đến";
			this.gridColumn11.DisplayFormat.FormatString = "%h\\:mm";
			this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.gridColumn11.FieldName = "CutIn";
			this.gridColumn11.Name = "gridColumn11";
			this.gridColumn11.OptionsColumn.AllowEdit = false;
			this.gridColumn11.OptionsColumn.ReadOnly = true;
			this.gridColumn11.Visible = true;
			this.gridColumn11.VisibleIndex = 10;
			this.gridColumn11.Width = 60;
			// 
			// gridColumn7
			// 
			this.gridColumn7.Caption = "Cho ra từ";
			this.gridColumn7.DisplayFormat.FormatString = "%h\\:mm";
			this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.gridColumn7.FieldName = "OnTimeOut";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.OptionsColumn.AllowEdit = false;
			this.gridColumn7.OptionsColumn.ReadOnly = true;
			this.gridColumn7.Visible = true;
			this.gridColumn7.VisibleIndex = 11;
			this.gridColumn7.Width = 60;
			// 
			// gridColumn12
			// 
			this.gridColumn12.Caption = "Cho ra đến";
			this.gridColumn12.DisplayFormat.FormatString = "%h\\:mm";
			this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.gridColumn12.FieldName = "CutOut";
			this.gridColumn12.Name = "gridColumn12";
			this.gridColumn12.OptionsColumn.AllowEdit = false;
			this.gridColumn12.OptionsColumn.ReadOnly = true;
			this.gridColumn12.Visible = true;
			this.gridColumn12.VisibleIndex = 12;
			this.gridColumn12.Width = 60;
			// 
			// gridColumn13
			// 
			this.gridColumn13.Caption = "Ký hiệu bảng CC";
			this.gridColumn13.FieldName = "KyHieuCC";
			this.gridColumn13.Name = "gridColumn13";
			this.gridColumn13.OptionsColumn.AllowEdit = false;
			this.gridColumn13.OptionsColumn.ReadOnly = true;
			this.gridColumn13.Visible = true;
			this.gridColumn13.VisibleIndex = 13;
			this.gridColumn13.Width = 70;
			// 
			// gridColumn15
			// 
			this.gridColumn15.Caption = "ShiftID_hide";
			this.gridColumn15.FieldName = "ShiftID";
			this.gridColumn15.Name = "gridColumn15";
			this.gridColumn15.OptionsColumn.AllowEdit = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(139, 14);
			this.label1.TabIndex = 1;
			this.label1.Text = "Danh sách ca làm việc";
			// 
			// btnSua
			// 
			this.btnSua.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.btnSua.Appearance.Options.UseFont = true;
			this.btnSua.Image = global::ChamCong_v06.Properties.Resources._1438546263_Edit;
			this.btnSua.Location = new System.Drawing.Point(96, 446);
			this.btnSua.Name = "btnSua";
			this.btnSua.Size = new System.Drawing.Size(75, 23);
			this.btnSua.TabIndex = 2;
			this.btnSua.Text = "Sửa";
			this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.btnThoat.Appearance.Options.UseFont = true;
			this.btnThoat.Image = global::ChamCong_v06.Properties.Resources._1438546227_Log_Out;
			this.btnThoat.Location = new System.Drawing.Point(259, 446);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(75, 23);
			this.btnThoat.TabIndex = 3;
			this.btnThoat.Text = "Đóng";
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnXoa
			// 
			this.btnXoa.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.btnXoa.Appearance.Options.UseFont = true;
			this.btnXoa.Image = global::ChamCong_v06.Properties.Resources._1438546065_Delete;
			this.btnXoa.Location = new System.Drawing.Point(177, 446);
			this.btnXoa.Name = "btnXoa";
			this.btnXoa.Size = new System.Drawing.Size(75, 23);
			this.btnXoa.TabIndex = 4;
			this.btnXoa.Text = "Xóa";
			this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
			// 
			// btnThem
			// 
			this.btnThem.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
			this.btnThem.Appearance.Options.UseFont = true;
			this.btnThem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnThem.Image = global::ChamCong_v06.Properties.Resources._1438546384_Add;
			this.btnThem.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
			this.btnThem.Location = new System.Drawing.Point(15, 446);
			this.btnThem.Name = "btnThem";
			this.btnThem.Size = new System.Drawing.Size(75, 23);
			this.btnThem.TabIndex = 5;
			this.btnThem.Text = "Thêm";
			this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
			// 
			// frmQLCa
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(927, 481);
			this.Controls.Add(this.btnSua);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnXoa);
			this.Controls.Add(this.btnThem);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridControl);
			this.Font = new System.Drawing.Font("Tahoma", 7.8F);
			this.Name = "frmQLCa";
			this.Text = "Quản lý danh sách ca làm việc";
			this.Load += new System.EventHandler(this.frmQLCa_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewShift)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewShift;
		private System.Windows.Forms.Label label1;
		private DevExpress.XtraEditors.SimpleButton btnSua;
		private DevExpress.XtraEditors.SimpleButton btnThoat;
		private DevExpress.XtraEditors.SimpleButton btnXoa;
		private DevExpress.XtraEditors.SimpleButton btnThem;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
	}
}