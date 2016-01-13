namespace HTQLTTKH {
	partial class fmChamCong {
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
			this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
			this.label3 = new System.Windows.Forms.Label();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colUserEnrollNumber = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUserFullCode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUserFullName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUserIDDepartment = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDepartmentDescription = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colScheduleID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colScheduleName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
			this.popupContainerEdit1 = new DevExpress.XtraEditors.PopupContainerEdit();
			this.simpleButtonInitData = new DevExpress.XtraEditors.SimpleButton();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
			this.popupContainerControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// dateEdit1
			// 
			this.dateEdit1.EditValue = new System.DateTime(2016, 1, 12, 0, 0, 0, 0);
			this.dateEdit1.Location = new System.Drawing.Point(75, 12);
			this.dateEdit1.Name = "dateEdit1";
			this.dateEdit1.Properties.Appearance.Options.UseFont = true;
			this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEdit1.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Classic;
			this.dateEdit1.Properties.DisplayFormat.FormatString = "dddd dd/MM/yyyy";
			this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEdit1.Properties.EditFormat.FormatString = "d/M/yyyy";
			this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEdit1.Properties.Mask.EditMask = "d/M/yyyy";
			this.dateEdit1.Properties.MaxValue = new System.DateTime(2050, 1, 1, 0, 0, 0, 0);
			this.dateEdit1.Properties.MinValue = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
			this.dateEdit1.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
			this.dateEdit1.Size = new System.Drawing.Size(189, 20);
			this.dateEdit1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Từ ngày";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(270, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(28, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "đến ";
			// 
			// dateEdit2
			// 
			this.dateEdit2.EditValue = new System.DateTime(2016, 1, 12, 0, 0, 0, 0);
			this.dateEdit2.Location = new System.Drawing.Point(311, 12);
			this.dateEdit2.Name = "dateEdit2";
			this.dateEdit2.Properties.Appearance.Options.UseFont = true;
			this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEdit2.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Classic;
			this.dateEdit2.Properties.DisplayFormat.FormatString = "dddd dd/MM/yyyy";
			this.dateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEdit2.Properties.EditFormat.FormatString = "d/M/yyyy";
			this.dateEdit2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEdit2.Properties.Mask.EditMask = "d/M/yyyy";
			this.dateEdit2.Properties.MaxValue = new System.DateTime(2050, 1, 1, 0, 0, 0, 0);
			this.dateEdit2.Properties.MinValue = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
			this.dateEdit2.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
			this.dateEdit2.Size = new System.Drawing.Size(189, 20);
			this.dateEdit2.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(83, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Chọn Nhân viên";
			// 
			// gridControl1
			// 
			this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControl1.DataSource = this.bindingSource1;
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl1.Location = new System.Drawing.Point(0, 0);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.Size = new System.Drawing.Size(449, 254);
			this.gridControl1.TabIndex = 3;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// bindingSource1
			// 
			this.bindingSource1.DataSource = typeof(HTQLTTKH.v5_UserInfo_DocDSNVThaoTacResult);
			// 
			// gridView1
			// 
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserEnrollNumber,
            this.colUserFullCode,
            this.colUserFullName,
            this.colUserIDDepartment,
            this.colDepartmentDescription,
            this.colScheduleID,
            this.colScheduleName,
            this.gridColumn1});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "UserFullName", this.colUserFullName, "")});
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsBehavior.Editable = false;
			this.gridView1.OptionsFilter.AllowFilterEditor = false;
			this.gridView1.OptionsFilter.FilterEditorUseMenuForOperandsAndOperators = false;
			this.gridView1.OptionsFind.AlwaysVisible = true;
			this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
			this.gridView1.OptionsSelection.MultiSelect = true;
			this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
			this.gridView1.OptionsView.ColumnAutoWidth = false;
			this.gridView1.OptionsView.ShowFooter = true;
			// 
			// colUserEnrollNumber
			// 
			this.colUserEnrollNumber.Caption = "Mã CC";
			this.colUserEnrollNumber.FieldName = "UserEnrollNumber";
			this.colUserEnrollNumber.Name = "colUserEnrollNumber";
			// 
			// colUserFullCode
			// 
			this.colUserFullCode.Caption = "Mã NV";
			this.colUserFullCode.FieldName = "UserFullCode";
			this.colUserFullCode.Name = "colUserFullCode";
			this.colUserFullCode.Visible = true;
			this.colUserFullCode.VisibleIndex = 1;
			// 
			// colUserFullName
			// 
			this.colUserFullName.Caption = "Họ tên NV";
			this.colUserFullName.FieldName = "UserFullName";
			this.colUserFullName.Name = "colUserFullName";
			this.colUserFullName.Visible = true;
			this.colUserFullName.VisibleIndex = 2;
			// 
			// colUserIDDepartment
			// 
			this.colUserIDDepartment.Caption = "Mã Phòng";
			this.colUserIDDepartment.FieldName = "UserIDDepartment";
			this.colUserIDDepartment.Name = "colUserIDDepartment";
			// 
			// colDepartmentDescription
			// 
			this.colDepartmentDescription.Caption = "Phòng";
			this.colDepartmentDescription.FieldName = "DepartmentDescription";
			this.colDepartmentDescription.Name = "colDepartmentDescription";
			this.colDepartmentDescription.Visible = true;
			this.colDepartmentDescription.VisibleIndex = 3;
			// 
			// colScheduleID
			// 
			this.colScheduleID.Caption = "Mã lịch trình";
			this.colScheduleID.FieldName = "ScheduleID";
			this.colScheduleID.Name = "colScheduleID";
			// 
			// colScheduleName
			// 
			this.colScheduleName.Caption = "Lịch trình";
			this.colScheduleName.FieldName = "ScheduleName";
			this.colScheduleName.Name = "colScheduleName";
			this.colScheduleName.Visible = true;
			this.colScheduleName.VisibleIndex = 4;
			// 
			// popupContainerControl1
			// 
			this.popupContainerControl1.Controls.Add(this.gridControl1);
			this.popupContainerControl1.Location = new System.Drawing.Point(209, 112);
			this.popupContainerControl1.Name = "popupContainerControl1";
			this.popupContainerControl1.Size = new System.Drawing.Size(449, 254);
			this.popupContainerControl1.TabIndex = 4;
			// 
			// popupContainerEdit1
			// 
			this.popupContainerEdit1.Location = new System.Drawing.Point(124, 46);
			this.popupContainerEdit1.Name = "popupContainerEdit1";
			this.popupContainerEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.popupContainerEdit1.Properties.PopupControl = this.popupContainerControl1;
			this.popupContainerEdit1.Size = new System.Drawing.Size(376, 20);
			this.popupContainerEdit1.TabIndex = 5;
			// 
			// simpleButtonInitData
			// 
			this.simpleButtonInitData.Location = new System.Drawing.Point(92, 140);
			this.simpleButtonInitData.Name = "simpleButtonInitData";
			this.simpleButtonInitData.Size = new System.Drawing.Size(75, 23);
			this.simpleButtonInitData.TabIndex = 6;
			this.simpleButtonInitData.Text = "Init Data";
			this.simpleButtonInitData.Click += new System.EventHandler(this.simpleButtonInitData_Click);
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "gridColumn1";
			this.gridColumn1.Name = "gridColumn1";
			// 
			// fmChamCong
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.ClientSize = new System.Drawing.Size(1000, 574);
			this.Controls.Add(this.simpleButtonInitData);
			this.Controls.Add(this.popupContainerEdit1);
			this.Controls.Add(this.popupContainerControl1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dateEdit2);
			this.Controls.Add(this.dateEdit1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "fmChamCong";
			this.Text = "fmChamCong";
			this.Load += new System.EventHandler(this.fmChamCong_Load);
			((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
			this.popupContainerControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit1.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.DateEdit dateEdit1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraEditors.DateEdit dateEdit2;
		private System.Windows.Forms.Label label3;
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
		private DevExpress.XtraEditors.PopupContainerEdit popupContainerEdit1;
		private System.Windows.Forms.BindingSource bindingSource1;
		private DevExpress.XtraGrid.Columns.GridColumn colUserEnrollNumber;
		private DevExpress.XtraGrid.Columns.GridColumn colUserFullCode;
		private DevExpress.XtraGrid.Columns.GridColumn colUserFullName;
		private DevExpress.XtraGrid.Columns.GridColumn colUserIDDepartment;
		private DevExpress.XtraGrid.Columns.GridColumn colDepartmentDescription;
		private DevExpress.XtraGrid.Columns.GridColumn colScheduleID;
		private DevExpress.XtraGrid.Columns.GridColumn colScheduleName;
		private DevExpress.XtraEditors.SimpleButton simpleButtonInitData;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
	}
}