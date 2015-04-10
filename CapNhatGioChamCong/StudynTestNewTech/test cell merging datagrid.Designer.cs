namespace StudynTestNewTech {
	partial class test_cell_merging_datagrid {
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
			this.wiseEyeV5ExpressDataSet = new StudynTestNewTech.WiseEyeV5ExpressDataSet();
			this.nhatKyThaoTacBindingSource = new System.Windows.Forms.BindingSource();
			this.nhatKyThaoTacTableAdapter = new StudynTestNewTech.WiseEyeV5ExpressDataSetTableAdapters.NhatKyThaoTacTableAdapter();
			this.wiseEyeV5ExpressDataSet1 = new StudynTestNewTech.WiseEyeV5ExpressDataSet();
			this.nhatKyThaoTacTableAdapter1 = new StudynTestNewTech.WiseEyeV5ExpressDataSetTableAdapters.NhatKyThaoTacTableAdapter();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.wiseEyeV5ExpressDataSet2 = new StudynTestNewTech.WiseEyeV5ExpressDataSet();
			this.nhatKyThaoTacTableAdapter2 = new StudynTestNewTech.WiseEyeV5ExpressDataSetTableAdapters.NhatKyThaoTacTableAdapter();
			this.colLoai = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colNoiDung = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUserID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colThoiDiem = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUserEnrollNumber = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colMaPhong = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.wiseEyeV5ExpressDataSet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nhatKyThaoTacBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wiseEyeV5ExpressDataSet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wiseEyeV5ExpressDataSet2)).BeginInit();
			this.SuspendLayout();
			// 
			// wiseEyeV5ExpressDataSet
			// 
			this.wiseEyeV5ExpressDataSet.DataSetName = "WiseEyeV5ExpressDataSet";
			this.wiseEyeV5ExpressDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// nhatKyThaoTacBindingSource
			// 
			this.nhatKyThaoTacBindingSource.DataMember = "NhatKyThaoTac";
			this.nhatKyThaoTacBindingSource.DataSource = this.wiseEyeV5ExpressDataSet;
			// 
			// nhatKyThaoTacTableAdapter
			// 
			this.nhatKyThaoTacTableAdapter.ClearBeforeFill = true;
			// 
			// wiseEyeV5ExpressDataSet1
			// 
			this.wiseEyeV5ExpressDataSet1.DataSetName = "WiseEyeV5ExpressDataSet";
			this.wiseEyeV5ExpressDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// nhatKyThaoTacTableAdapter1
			// 
			this.nhatKyThaoTacTableAdapter1.ClearBeforeFill = true;
			// 
			// gridControl1
			// 
			this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControl1.DataMember = "NhatKyThaoTac";
			this.gridControl1.DataSource = this.wiseEyeV5ExpressDataSet2;
			this.gridControl1.Location = new System.Drawing.Point(89, 65);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.Size = new System.Drawing.Size(620, 200);
			this.gridControl1.TabIndex = 0;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLoai,
            this.colNoiDung,
            this.colUserID,
            this.colThoiDiem,
            this.colUserEnrollNumber,
            this.colMaPhong});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsView.AllowCellMerge = true;
			// 
			// wiseEyeV5ExpressDataSet2
			// 
			this.wiseEyeV5ExpressDataSet2.DataSetName = "WiseEyeV5ExpressDataSet";
			this.wiseEyeV5ExpressDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// nhatKyThaoTacTableAdapter2
			// 
			this.nhatKyThaoTacTableAdapter2.ClearBeforeFill = true;
			// 
			// colLoai
			// 
			this.colLoai.FieldName = "Loai";
			this.colLoai.Name = "colLoai";
			this.colLoai.Visible = true;
			this.colLoai.VisibleIndex = 0;
			// 
			// colNoiDung
			// 
			this.colNoiDung.FieldName = "NoiDung";
			this.colNoiDung.Name = "colNoiDung";
			this.colNoiDung.Visible = true;
			this.colNoiDung.VisibleIndex = 1;
			// 
			// colUserID
			// 
			this.colUserID.FieldName = "UserID";
			this.colUserID.Name = "colUserID";
			this.colUserID.Visible = true;
			this.colUserID.VisibleIndex = 2;
			// 
			// colThoiDiem
			// 
			this.colThoiDiem.FieldName = "ThoiDiem";
			this.colThoiDiem.Name = "colThoiDiem";
			this.colThoiDiem.Visible = true;
			this.colThoiDiem.VisibleIndex = 3;
			// 
			// colUserEnrollNumber
			// 
			this.colUserEnrollNumber.FieldName = "UserEnrollNumber";
			this.colUserEnrollNumber.Name = "colUserEnrollNumber";
			this.colUserEnrollNumber.Visible = true;
			this.colUserEnrollNumber.VisibleIndex = 4;
			// 
			// colMaPhong
			// 
			this.colMaPhong.FieldName = "MaPhong";
			this.colMaPhong.Name = "colMaPhong";
			this.colMaPhong.Visible = true;
			this.colMaPhong.VisibleIndex = 5;
			// 
			// test_cell_merging_datagrid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(798, 416);
			this.Controls.Add(this.gridControl1);
			this.Name = "test_cell_merging_datagrid";
			this.Text = "test_cell_merging_datagrid";
			this.Load += new System.EventHandler(this.test_cell_merging_datagrid_Load);
			((System.ComponentModel.ISupportInitialize)(this.wiseEyeV5ExpressDataSet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nhatKyThaoTacBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wiseEyeV5ExpressDataSet1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wiseEyeV5ExpressDataSet2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private WiseEyeV5ExpressDataSet wiseEyeV5ExpressDataSet;
		private System.Windows.Forms.BindingSource nhatKyThaoTacBindingSource;
		private WiseEyeV5ExpressDataSetTableAdapters.NhatKyThaoTacTableAdapter nhatKyThaoTacTableAdapter;
		private WiseEyeV5ExpressDataSet wiseEyeV5ExpressDataSet1;
		private WiseEyeV5ExpressDataSetTableAdapters.NhatKyThaoTacTableAdapter nhatKyThaoTacTableAdapter1;
		private DevExpress.XtraGrid.GridControl gridControl1;
		private WiseEyeV5ExpressDataSet wiseEyeV5ExpressDataSet2;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraGrid.Columns.GridColumn colLoai;
		private DevExpress.XtraGrid.Columns.GridColumn colNoiDung;
		private DevExpress.XtraGrid.Columns.GridColumn colUserID;
		private DevExpress.XtraGrid.Columns.GridColumn colThoiDiem;
		private DevExpress.XtraGrid.Columns.GridColumn colUserEnrollNumber;
		private DevExpress.XtraGrid.Columns.GridColumn colMaPhong;
		private WiseEyeV5ExpressDataSetTableAdapters.NhatKyThaoTacTableAdapter nhatKyThaoTacTableAdapter2;

	}
}