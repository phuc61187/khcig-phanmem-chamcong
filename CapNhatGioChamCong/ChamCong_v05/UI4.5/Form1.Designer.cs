namespace ChamCong_v05.UI4._5 {
	partial class Form1 {
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
			System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("px thành phẩm");
			System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("bảo vệ");
			System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("văn phòng", new System.Windows.Forms.TreeNode[] {
			treeNode18});
			System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Nhà máy thuốc lá khánh hội", new System.Windows.Forms.TreeNode[] {
			treeNode17,
			treeNode19});
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.checkedComboBoxEdit1 = new DevExpress.XtraEditors.CheckedComboBoxEdit();
			this.dtpNgayBD = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dtpNgayKT = new System.Windows.Forms.DateTimePicker();
			this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
			this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
			this.dgrdTongHop = new System.Windows.Forms.DataGridView();
			this.g1c1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.gpChonPhongBan.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdTongHop)).BeginInit();
			this.SuspendLayout();
			// 
			// gpChonPhongBan
			// 
			this.gpChonPhongBan.Controls.Add(this.treePhongBan);
			this.gpChonPhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.gpChonPhongBan.Location = new System.Drawing.Point(1, 1);
			this.gpChonPhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gpChonPhongBan.Name = "gpChonPhongBan";
			this.gpChonPhongBan.Padding = new System.Windows.Forms.Padding(3, 6, 3, 2);
			this.gpChonPhongBan.Size = new System.Drawing.Size(233, 117);
			this.gpChonPhongBan.TabIndex = 0;
			this.gpChonPhongBan.TabStop = false;
			this.gpChonPhongBan.Text = "Chọn phòng ban";
			// 
			// treePhongBan
			// 
			this.treePhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treePhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.treePhongBan.Indent = 18;
			this.treePhongBan.ItemHeight = 20;
			this.treePhongBan.Location = new System.Drawing.Point(3, 20);
			this.treePhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.treePhongBan.Name = "treePhongBan";
			treeNode17.Name = "Node2";
			treeNode17.Text = "px thành phẩm";
			treeNode18.Name = "Node5";
			treeNode18.Text = "bảo vệ";
			treeNode19.Name = "Node4";
			treeNode19.Text = "văn phòng";
			treeNode20.Name = "Node0";
			treeNode20.Text = "Nhà máy thuốc lá khánh hội";
			this.treePhongBan.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
			treeNode20});
			this.treePhongBan.ShowNodeToolTips = true;
			this.treePhongBan.Size = new System.Drawing.Size(227, 95);
			this.treePhongBan.TabIndex = 0;
			// 
			// checkedComboBoxEdit1
			// 
			this.checkedComboBoxEdit1.Location = new System.Drawing.Point(245, 12);
			this.checkedComboBoxEdit1.Name = "checkedComboBoxEdit1";
			this.checkedComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.checkedComboBoxEdit1.Size = new System.Drawing.Size(230, 22);
			this.checkedComboBoxEdit1.TabIndex = 1;
			// 
			// dtpNgayBD
			// 
			this.dtpNgayBD.CustomFormat = "dddd dd/MM/yyyy";
			this.dtpNgayBD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgayBD.Location = new System.Drawing.Point(920, 75);
			this.dtpNgayBD.Name = "dtpNgayBD";
			this.dtpNgayBD.Size = new System.Drawing.Size(167, 22);
			this.dtpNgayBD.TabIndex = 22;
			this.dtpNgayBD.Value = new System.DateTime(2013, 8, 1, 0, 0, 0, 0);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Location = new System.Drawing.Point(443, 75);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 16);
			this.label1.TabIndex = 24;
			this.label1.Text = "0.99; 0.27; NP";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Location = new System.Drawing.Point(1163, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 16);
			this.label2.TabIndex = 25;
			this.label2.Text = "Đến ngày";
			// 
			// dtpNgayKT
			// 
			this.dtpNgayKT.CustomFormat = "dddd dd/MM/yyyy";
			this.dtpNgayKT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgayKT.Location = new System.Drawing.Point(933, 48);
			this.dtpNgayKT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dtpNgayKT.Name = "dtpNgayKT";
			this.dtpNgayKT.Size = new System.Drawing.Size(167, 22);
			this.dtpNgayKT.TabIndex = 23;
			this.dtpNgayKT.Value = new System.DateTime(2013, 8, 31, 0, 0, 0, 0);
			// 
			// dateEdit1
			// 
			this.dateEdit1.EditValue = new System.DateTime(2015, 4, 2, 22, 31, 21, 0);
			this.dateEdit1.Location = new System.Drawing.Point(308, 45);
			this.dateEdit1.Name = "dateEdit1";
			this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEdit1.Properties.DisplayFormat.FormatString = "dddd dd/MM/yyyy";
			this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.dateEdit1.Properties.EditFormat.FormatString = "dd/MM/yyyy";
			this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.dateEdit1.Properties.Mask.EditMask = "dd/MM/yyyy";
			this.dateEdit1.Size = new System.Drawing.Size(167, 22);
			this.dateEdit1.TabIndex = 27;
			// 
			// dateEdit2
			// 
			this.dateEdit2.EditValue = new System.DateTime(2015, 4, 2, 22, 31, 21, 0);
			this.dateEdit2.Location = new System.Drawing.Point(738, 84);
			this.dateEdit2.Name = "dateEdit2";
			this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEdit2.Properties.DisplayFormat.FormatString = "dddd dd/MM/yyyy";
			this.dateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.dateEdit2.Properties.EditFormat.FormatString = "dd/MM/yyyy";
			this.dateEdit2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.dateEdit2.Properties.Mask.EditMask = "dd/MM/yyyy";
			this.dateEdit2.Size = new System.Drawing.Size(167, 22);
			this.dateEdit2.TabIndex = 27;
			// 
			// dgrdTongHop
			// 
			this.dgrdTongHop.AllowUserToAddRows = false;
			this.dgrdTongHop.AllowUserToDeleteRows = false;
			this.dgrdTongHop.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
			this.dgrdTongHop.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgrdTongHop.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgrdTongHop.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dgrdTongHop.ColumnHeadersHeight = 25;
			this.dgrdTongHop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgrdTongHop.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.g1c1,
			this.g1c2,
			this.g1c3,
			this.Column1});
			this.dgrdTongHop.GridColor = System.Drawing.Color.White;
			this.dgrdTongHop.Location = new System.Drawing.Point(4, 127);
			this.dgrdTongHop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dgrdTongHop.Name = "dgrdTongHop";
			this.dgrdTongHop.RowHeadersVisible = false;
			this.dgrdTongHop.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdTongHop.Size = new System.Drawing.Size(1326, 477);
			this.dgrdTongHop.TabIndex = 26;
			// 
			// g1c1
			// 
			this.g1c1.DataPropertyName = "UserFullCode";
			this.g1c1.HeaderText = "Mã NV";
			this.g1c1.Name = "g1c1";
			this.g1c1.ReadOnly = true;
			this.g1c1.ToolTipText = "Mã Nhân viên";
			this.g1c1.Width = 55;
			// 
			// g1c2
			// 
			this.g1c2.DataPropertyName = "UserEnrollNumber";
			dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.g1c2.DefaultCellStyle = dataGridViewCellStyle2;
			this.g1c2.HeaderText = "Mã CC_hide";
			this.g1c2.Name = "g1c2";
			this.g1c2.ReadOnly = true;
			this.g1c2.ToolTipText = "Mã chấm công";
			this.g1c2.Visible = false;
			this.g1c2.Width = 45;
			// 
			// g1c3
			// 
			this.g1c3.DataPropertyName = "UserFullName";
			this.g1c3.HeaderText = "Tên NV";
			this.g1c3.Name = "g1c3";
			this.g1c3.ReadOnly = true;
			this.g1c3.ToolTipText = "Tên Nhân viên";
			this.g1c3.Width = 150;
			// 
			// Column1
			// 
			this.Column1.HeaderText = "Column1";
			this.Column1.Name = "Column1";
			this.Column1.Width = 45;
			// 
			// Form1
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1342, 615);
			this.Controls.Add(this.dateEdit2);
			this.Controls.Add(this.dateEdit1);
			this.Controls.Add(this.dtpNgayBD);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.dtpNgayKT);
			this.Controls.Add(this.checkedComboBoxEdit1);
			this.Controls.Add(this.gpChonPhongBan);
			this.Controls.Add(this.dgrdTongHop);
			this.Name = "Form1";
			this.Text = "Form1";
			this.gpChonPhongBan.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdTongHop)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox gpChonPhongBan;
		private System.Windows.Forms.TreeView treePhongBan;
		private DevExpress.XtraEditors.CheckedComboBoxEdit checkedComboBoxEdit1;
		private System.Windows.Forms.DateTimePicker dtpNgayBD;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dtpNgayKT;
		private DevExpress.XtraEditors.DateEdit dateEdit1;
		private DevExpress.XtraEditors.DateEdit dateEdit2;
		private System.Windows.Forms.DataGridView dgrdTongHop;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c1;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c2;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
	}
}