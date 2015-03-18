namespace ChamCong_v02 {
    partial class frm_22_KhaiBaoLamViecNgayNghi {
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("px thành phẩm");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("bảo vệ");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("văn phòng", new System.Windows.Forms.TreeNode[] {
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Nhà máy thuốc lá khánh hội", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3});
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.label1 = new System.Windows.Forms.Label();
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.dgrdNgayNghi = new System.Windows.Forms.DataGridView();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
			this.expandablePanel2 = new DevComponents.DotNetBar.ExpandablePanel();
			this.dgrdDSNVTrgPhg = new System.Windows.Forms.DataGridView();
			this.cChkDSNV2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.cUserFullNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cUserFullCodeDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cUserEnrollNumberDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cSchNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cTitleNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cDescriptionDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cSchIDDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cUserIDDDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
			this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.btnThem = new System.Windows.Forms.Button();
			this.btnXoa = new System.Windows.Forms.Button();
			this.btnLietKe = new System.Windows.Forms.Button();
			this.g1check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.g2macc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g3colManv = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1tennv = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1ngay = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1th = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1colduyet = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colPhucap = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colphantram = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgrdNgayNghi)).BeginInit();
			this.panelEx1.SuspendLayout();
			this.panelEx2.SuspendLayout();
			this.expandablePanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).BeginInit();
			this.expandablePanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(244, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Nhập ngày";
			// 
			// dtpThang
			// 
			this.dtpThang.CustomFormat = "ddd d/ M / yyyy";
			this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpThang.Location = new System.Drawing.Point(343, 5);
			this.dtpThang.Name = "dtpThang";
			this.dtpThang.ShowUpDown = true;
			this.dtpThang.Size = new System.Drawing.Size(156, 22);
			this.dtpThang.TabIndex = 0;
			this.dtpThang.Value = new System.DateTime(2014, 2, 1, 19, 11, 0, 0);
			// 
			// dgrdNgayNghi
			// 
			this.dgrdNgayNghi.AllowUserToAddRows = false;
			this.dgrdNgayNghi.AllowUserToDeleteRows = false;
			this.dgrdNgayNghi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdNgayNghi.ColumnHeadersHeight = 27;
			this.dgrdNgayNghi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgrdNgayNghi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.g1check,
            this.g2macc,
            this.g3colManv,
            this.g1tennv,
            this.g1ngay,
            this.g1th,
            this.g1colduyet,
            this.colPhucap,
            this.colphantram,
            this.g1ID});
			this.dgrdNgayNghi.Location = new System.Drawing.Point(247, 76);
			this.dgrdNgayNghi.Name = "dgrdNgayNghi";
			this.dgrdNgayNghi.RowHeadersVisible = false;
			this.dgrdNgayNghi.Size = new System.Drawing.Size(389, 521);
			this.dgrdNgayNghi.TabIndex = 8;
			// 
			// panelEx1
			// 
			this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.panelEx2);
			this.panelEx1.Controls.Add(this.expandableSplitter1);
			this.panelEx1.Controls.Add(this.expandablePanel1);
			this.panelEx1.Location = new System.Drawing.Point(1, 1);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(237, 596);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 9;
			this.panelEx1.Text = "panelEx1";
			// 
			// panelEx2
			// 
			this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx2.Controls.Add(this.expandablePanel2);
			this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx2.Location = new System.Drawing.Point(0, 212);
			this.panelEx2.Name = "panelEx2";
			this.panelEx2.Size = new System.Drawing.Size(237, 384);
			this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx2.Style.GradientAngle = 90;
			this.panelEx2.TabIndex = 5;
			this.panelEx2.Text = "panelEx2";
			// 
			// expandablePanel2
			// 
			this.expandablePanel2.CanvasColor = System.Drawing.SystemColors.Control;
			this.expandablePanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.expandablePanel2.Controls.Add(this.dgrdDSNVTrgPhg);
			this.expandablePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.expandablePanel2.Location = new System.Drawing.Point(0, 0);
			this.expandablePanel2.Name = "expandablePanel2";
			this.expandablePanel2.Size = new System.Drawing.Size(237, 384);
			this.expandablePanel2.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.expandablePanel2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.expandablePanel2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandablePanel2.Style.GradientAngle = 90;
			this.expandablePanel2.TabIndex = 3;
			this.expandablePanel2.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel2.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel2.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel2.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
			this.expandablePanel2.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandablePanel2.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.expandablePanel2.TitleStyle.GradientAngle = 90;
			this.expandablePanel2.TitleText = "Danh sách Nhân viên";
			// 
			// dgrdDSNVTrgPhg
			// 
			this.dgrdDSNVTrgPhg.AllowUserToAddRows = false;
			this.dgrdDSNVTrgPhg.AllowUserToDeleteRows = false;
			this.dgrdDSNVTrgPhg.AllowUserToResizeRows = false;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.dgrdDSNVTrgPhg.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
			this.dgrdDSNVTrgPhg.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgrdDSNVTrgPhg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cChkDSNV2,
            this.cUserFullNameDSNV2,
            this.cUserFullCodeDSNV2,
            this.cUserEnrollNumberDSNV2,
            this.cSchNameDSNV2,
            this.cTitleNameDSNV2,
            this.cDescriptionDSNV2,
            this.cSchIDDSNV2,
            this.cUserIDDDSNV2});
			this.dgrdDSNVTrgPhg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgrdDSNVTrgPhg.Location = new System.Drawing.Point(0, 26);
			this.dgrdDSNVTrgPhg.Name = "dgrdDSNVTrgPhg";
			this.dgrdDSNVTrgPhg.RowHeadersVisible = false;
			this.dgrdDSNVTrgPhg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdDSNVTrgPhg.Size = new System.Drawing.Size(237, 358);
			this.dgrdDSNVTrgPhg.TabIndex = 2;
			// 
			// cChkDSNV2
			// 
			this.cChkDSNV2.DataPropertyName = "check";
			this.cChkDSNV2.FalseValue = "false";
			this.cChkDSNV2.Frozen = true;
			this.cChkDSNV2.HeaderText = "";
			this.cChkDSNV2.Name = "cChkDSNV2";
			this.cChkDSNV2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.cChkDSNV2.TrueValue = "true";
			this.cChkDSNV2.Width = 22;
			// 
			// cUserFullNameDSNV2
			// 
			this.cUserFullNameDSNV2.DataPropertyName = "UserFullName";
			this.cUserFullNameDSNV2.HeaderText = "Tên NV";
			this.cUserFullNameDSNV2.Name = "cUserFullNameDSNV2";
			this.cUserFullNameDSNV2.ReadOnly = true;
			this.cUserFullNameDSNV2.ToolTipText = "Tên Nhân viên";
			this.cUserFullNameDSNV2.Width = 150;
			// 
			// cUserFullCodeDSNV2
			// 
			this.cUserFullCodeDSNV2.DataPropertyName = "UserFullCode";
			dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.cUserFullCodeDSNV2.DefaultCellStyle = dataGridViewCellStyle4;
			this.cUserFullCodeDSNV2.HeaderText = "Mã NV";
			this.cUserFullCodeDSNV2.Name = "cUserFullCodeDSNV2";
			this.cUserFullCodeDSNV2.ReadOnly = true;
			this.cUserFullCodeDSNV2.ToolTipText = "Mã Nhân viên";
			this.cUserFullCodeDSNV2.Width = 55;
			// 
			// cUserEnrollNumberDSNV2
			// 
			this.cUserEnrollNumberDSNV2.DataPropertyName = "UserEnrollNumber";
			dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.cUserEnrollNumberDSNV2.DefaultCellStyle = dataGridViewCellStyle5;
			this.cUserEnrollNumberDSNV2.HeaderText = "Mã CC";
			this.cUserEnrollNumberDSNV2.Name = "cUserEnrollNumberDSNV2";
			this.cUserEnrollNumberDSNV2.ReadOnly = true;
			this.cUserEnrollNumberDSNV2.ToolTipText = "Mã Chấm công";
			this.cUserEnrollNumberDSNV2.Width = 55;
			// 
			// cSchNameDSNV2
			// 
			this.cSchNameDSNV2.DataPropertyName = "SchName";
			this.cSchNameDSNV2.HeaderText = "Lịch trình";
			this.cSchNameDSNV2.Name = "cSchNameDSNV2";
			this.cSchNameDSNV2.ReadOnly = true;
			this.cSchNameDSNV2.ToolTipText = "Lịch trình";
			this.cSchNameDSNV2.Width = 95;
			// 
			// cTitleNameDSNV2
			// 
			this.cTitleNameDSNV2.DataPropertyName = "TitleName";
			this.cTitleNameDSNV2.HeaderText = "Chức vụ";
			this.cTitleNameDSNV2.Name = "cTitleNameDSNV2";
			this.cTitleNameDSNV2.ReadOnly = true;
			this.cTitleNameDSNV2.ToolTipText = "Chức vụ";
			this.cTitleNameDSNV2.Width = 200;
			// 
			// cDescriptionDSNV2
			// 
			this.cDescriptionDSNV2.DataPropertyName = "Description_1";
			this.cDescriptionDSNV2.HeaderText = "Phòng ban";
			this.cDescriptionDSNV2.Name = "cDescriptionDSNV2";
			this.cDescriptionDSNV2.ReadOnly = true;
			this.cDescriptionDSNV2.ToolTipText = "Phòng ban";
			this.cDescriptionDSNV2.Width = 105;
			// 
			// cSchIDDSNV2
			// 
			this.cSchIDDSNV2.DataPropertyName = "SchID";
			this.cSchIDDSNV2.HeaderText = "Mã lịch trình";
			this.cSchIDDSNV2.Name = "cSchIDDSNV2";
			this.cSchIDDSNV2.ReadOnly = true;
			this.cSchIDDSNV2.ToolTipText = "Mã lịch trình";
			this.cSchIDDSNV2.Visible = false;
			// 
			// cUserIDDDSNV2
			// 
			this.cUserIDDDSNV2.DataPropertyName = "IDD_1";
			this.cUserIDDDSNV2.HeaderText = "Mã Phòng ban";
			this.cUserIDDDSNV2.Name = "cUserIDDDSNV2";
			this.cUserIDDDSNV2.ReadOnly = true;
			this.cUserIDDDSNV2.Visible = false;
			// 
			// expandableSplitter1
			// 
			this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
			this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandableSplitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.expandableSplitter1.ExpandableControl = this.expandablePanel1;
			this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
			this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
			this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
			this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
			this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
			this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
			this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
			this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
			this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
			this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter1.Location = new System.Drawing.Point(0, 200);
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Size = new System.Drawing.Size(237, 12);
			this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
			this.expandableSplitter1.TabIndex = 4;
			this.expandableSplitter1.TabStop = false;
			// 
			// expandablePanel1
			// 
			this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
			this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.expandablePanel1.Controls.Add(this.treePhongBan);
			this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.expandablePanel1.Location = new System.Drawing.Point(0, 0);
			this.expandablePanel1.Name = "expandablePanel1";
			this.expandablePanel1.Size = new System.Drawing.Size(237, 200);
			this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandablePanel1.Style.GradientAngle = 90;
			this.expandablePanel1.TabIndex = 2;
			this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
			this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.expandablePanel1.TitleStyle.GradientAngle = 90;
			this.expandablePanel1.TitleText = "Phòng ban";
			// 
			// treePhongBan
			// 
			this.treePhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treePhongBan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.treePhongBan.Indent = 18;
			this.treePhongBan.ItemHeight = 20;
			this.treePhongBan.Location = new System.Drawing.Point(0, 26);
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
			this.treePhongBan.Size = new System.Drawing.Size(237, 174);
			this.treePhongBan.TabIndex = 0;
			// 
			// btnThem
			// 
			this.btnThem.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnThem.ForeColor = System.Drawing.Color.Blue;
			this.btnThem.Location = new System.Drawing.Point(343, 37);
			this.btnThem.Name = "btnThem";
			this.btnThem.Size = new System.Drawing.Size(70, 27);
			this.btnThem.TabIndex = 1;
			this.btnThem.Text = "Thêm";
			this.btnThem.UseVisualStyleBackColor = true;
			this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
			// 
			// btnXoa
			// 
			this.btnXoa.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnXoa.ForeColor = System.Drawing.Color.Blue;
			this.btnXoa.Location = new System.Drawing.Point(419, 37);
			this.btnXoa.Name = "btnXoa";
			this.btnXoa.Size = new System.Drawing.Size(70, 27);
			this.btnXoa.TabIndex = 2;
			this.btnXoa.Text = "Xóa ";
			this.btnXoa.UseVisualStyleBackColor = true;
			this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
			// 
			// btnLietKe
			// 
			this.btnLietKe.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnLietKe.ForeColor = System.Drawing.Color.Blue;
			this.btnLietKe.Location = new System.Drawing.Point(495, 37);
			this.btnLietKe.Name = "btnLietKe";
			this.btnLietKe.Size = new System.Drawing.Size(70, 27);
			this.btnLietKe.TabIndex = 3;
			this.btnLietKe.Text = "Liệt kê";
			this.btnLietKe.UseVisualStyleBackColor = true;
			this.btnLietKe.Click += new System.EventHandler(this.btnLietKe_Click);
			this.btnLietKe.MouseHover += new System.EventHandler(this.btnLietKe_MouseHover);
			// 
			// g1check
			// 
			this.g1check.DataPropertyName = "check";
			this.g1check.FalseValue = "false";
			this.g1check.HeaderText = "";
			this.g1check.Name = "g1check";
			this.g1check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.g1check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.g1check.TrueValue = "true";
			this.g1check.Width = 24;
			// 
			// g2macc
			// 
			this.g2macc.DataPropertyName = "UserEnrollNumber";
			this.g2macc.HeaderText = "Mã CC_hide";
			this.g2macc.Name = "g2macc";
			this.g2macc.ReadOnly = true;
			this.g2macc.Visible = false;
			this.g2macc.Width = 55;
			// 
			// g3colManv
			// 
			this.g3colManv.DataPropertyName = "UserFullCode";
			this.g3colManv.HeaderText = "Mã NV";
			this.g3colManv.Name = "g3colManv";
			this.g3colManv.ReadOnly = true;
			this.g3colManv.Width = 55;
			// 
			// g1tennv
			// 
			this.g1tennv.DataPropertyName = "UserFullName";
			this.g1tennv.HeaderText = "Tên NV";
			this.g1tennv.Name = "g1tennv";
			this.g1tennv.ReadOnly = true;
			this.g1tennv.Width = 150;
			// 
			// g1ngay
			// 
			this.g1ngay.DataPropertyName = "Ngay";
			dataGridViewCellStyle1.Format = "d/M";
			this.g1ngay.DefaultCellStyle = dataGridViewCellStyle1;
			this.g1ngay.HeaderText = "Ngày";
			this.g1ngay.Name = "g1ngay";
			this.g1ngay.ReadOnly = true;
			this.g1ngay.Width = 45;
			// 
			// g1th
			// 
			this.g1th.DataPropertyName = "Ngay";
			dataGridViewCellStyle2.Format = "ddd";
			this.g1th.DefaultCellStyle = dataGridViewCellStyle2;
			this.g1th.HeaderText = "Thứ";
			this.g1th.Name = "g1th";
			this.g1th.ReadOnly = true;
			this.g1th.Width = 45;
			// 
			// g1colduyet
			// 
			this.g1colduyet.DataPropertyName = "Duyet";
			this.g1colduyet.FalseValue = "false";
			this.g1colduyet.HeaderText = "Duyệt";
			this.g1colduyet.Name = "g1colduyet";
			this.g1colduyet.ReadOnly = true;
			this.g1colduyet.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.g1colduyet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.g1colduyet.TrueValue = "true";
			this.g1colduyet.Width = 50;
			// 
			// colPhucap
			// 
			this.colPhucap.DataPropertyName = "PCThem";
			this.colPhucap.HeaderText = "Phụ cấp thêm_hide";
			this.colPhucap.Name = "colPhucap";
			this.colPhucap.ReadOnly = true;
			this.colPhucap.Visible = false;
			// 
			// colphantram
			// 
			this.colphantram.DataPropertyName = "PCDem";
			this.colphantram.HeaderText = "Phụ cấp đêm_hide";
			this.colphantram.Name = "colphantram";
			this.colphantram.ReadOnly = true;
			this.colphantram.Visible = false;
			// 
			// g1ID
			// 
			this.g1ID.DataPropertyName = "ID";
			this.g1ID.HeaderText = "ID_hide";
			this.g1ID.Name = "g1ID";
			this.g1ID.ReadOnly = true;
			this.g1ID.Visible = false;
			// 
			// frm_22_KhaiBaoLamViecNgayNghi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(641, 600);
			this.Controls.Add(this.btnLietKe);
			this.Controls.Add(this.btnXoa);
			this.Controls.Add(this.btnThem);
			this.Controls.Add(this.panelEx1);
			this.Controls.Add(this.dgrdNgayNghi);
			this.Controls.Add(this.dtpThang);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_22_KhaiBaoLamViecNgayNghi";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Khai báo ngày làm việc tính phụ cấp 100%";
			this.Load += new System.EventHandler(this.frm_KhaiBaoVang_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdNgayNghi)).EndInit();
			this.panelEx1.ResumeLayout(false);
			this.panelEx2.ResumeLayout(false);
			this.expandablePanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).EndInit();
			this.expandablePanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpThang;
		private System.Windows.Forms.DataGridView dgrdNgayNghi;
		private System.Windows.Forms.ToolTip toolTip1;
		private DevComponents.DotNetBar.PanelEx panelEx1;
		private DevComponents.DotNetBar.PanelEx panelEx2;
		private DevComponents.DotNetBar.ExpandablePanel expandablePanel2;
		private System.Windows.Forms.DataGridView dgrdDSNVTrgPhg;
		private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
		private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private System.Windows.Forms.TreeView treePhongBan;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnXoa;
		private System.Windows.Forms.Button btnLietKe;
		private System.Windows.Forms.DataGridViewCheckBoxColumn cChkDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullCodeDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserEnrollNumberDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cSchNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cTitleNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cDescriptionDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cSchIDDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserIDDDSNV2;
		private System.Windows.Forms.DataGridViewCheckBoxColumn g1check;
		private System.Windows.Forms.DataGridViewTextBoxColumn g2macc;
		private System.Windows.Forms.DataGridViewTextBoxColumn g3colManv;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1tennv;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1ngay;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1th;
		private System.Windows.Forms.DataGridViewCheckBoxColumn g1colduyet;
		private System.Windows.Forms.DataGridViewTextBoxColumn colPhucap;
		private System.Windows.Forms.DataGridViewTextBoxColumn colphantram;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1ID;


    }
}