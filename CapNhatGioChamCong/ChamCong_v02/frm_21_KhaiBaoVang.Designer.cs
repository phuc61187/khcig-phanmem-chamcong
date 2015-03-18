namespace ChamCong_v02 {
    partial class frm_21_KhaiBaoVang {
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("px thành phẩm");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("bảo vệ");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("văn phòng", new System.Windows.Forms.TreeNode[] {
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Nhà máy thuốc lá khánh hội", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3});
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.checklistNgay = new System.Windows.Forms.CheckedListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cbLoaiVang = new System.Windows.Forms.ComboBox();
			this.dgrdNgayVang = new System.Windows.Forms.DataGridView();
			this.g1check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.g4colMaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g2macc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1tennv = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1ngay = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1th = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1mota = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1cong = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1userfullcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1absentcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
			this.btnLietKe = new System.Windows.Forms.Button();
			this.btnXoa = new System.Windows.Forms.Button();
			this.btnThem = new System.Windows.Forms.Button();
			this.rdChonNgayTrongKhoang = new System.Windows.Forms.RadioButton();
			this.rdChonNgayTrongThang = new System.Windows.Forms.RadioButton();
			this.dtpNgayBD = new System.Windows.Forms.DateTimePicker();
			this.dtpNgayKT = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgrdNgayVang)).BeginInit();
			this.panelEx1.SuspendLayout();
			this.panelEx2.SuspendLayout();
			this.expandablePanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).BeginInit();
			this.expandablePanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dtpThang
			// 
			this.dtpThang.CustomFormat = "M/yyyy";
			this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpThang.Location = new System.Drawing.Point(428, 45);
			this.dtpThang.Name = "dtpThang";
			this.dtpThang.ShowUpDown = true;
			this.dtpThang.Size = new System.Drawing.Size(98, 22);
			this.dtpThang.TabIndex = 2;
			this.dtpThang.Value = new System.DateTime(2014, 2, 1, 19, 11, 0, 0);
			this.dtpThang.ValueChanged += new System.EventHandler(this.dtpThang_ValueChanged);
			// 
			// checklistNgay
			// 
			this.checklistNgay.CheckOnClick = true;
			this.checklistNgay.ColumnWidth = 95;
			this.checklistNgay.FormatString = "d - ddd";
			this.checklistNgay.FormattingEnabled = true;
			this.checklistNgay.Items.AddRange(new object[] {
            "1 - Hai",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "11",
            "12",
            "13",
            "14",
            "15",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
			this.checklistNgay.Location = new System.Drawing.Point(245, 80);
			this.checklistNgay.MultiColumn = true;
			this.checklistNgay.Name = "checklistNgay";
			this.checklistNgay.Size = new System.Drawing.Size(496, 123);
			this.checklistNgay.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(247, 214);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71, 16);
			this.label3.TabIndex = 1;
			this.label3.Text = "Loại vắng";
			// 
			// cbLoaiVang
			// 
			this.cbLoaiVang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbLoaiVang.FormattingEnabled = true;
			this.cbLoaiVang.Location = new System.Drawing.Point(341, 211);
			this.cbLoaiVang.Name = "cbLoaiVang";
			this.cbLoaiVang.Size = new System.Drawing.Size(327, 24);
			this.cbLoaiVang.TabIndex = 4;
			// 
			// dgrdNgayVang
			// 
			this.dgrdNgayVang.AllowUserToAddRows = false;
			this.dgrdNgayVang.AllowUserToDeleteRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.dgrdNgayVang.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgrdNgayVang.ColumnHeadersHeight = 27;
			this.dgrdNgayVang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgrdNgayVang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.g1check,
            this.g4colMaNV,
            this.g2macc,
            this.g1tennv,
            this.g1ngay,
            this.g1th,
            this.g1mota,
            this.g1cong,
            this.g1userfullcode,
            this.g1ID,
            this.g1absentcode});
			this.dgrdNgayVang.Location = new System.Drawing.Point(245, 281);
			this.dgrdNgayVang.Name = "dgrdNgayVang";
			this.dgrdNgayVang.RowHeadersVisible = false;
			this.dgrdNgayVang.Size = new System.Drawing.Size(496, 316);
			this.dgrdNgayVang.TabIndex = 8;
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
			// g4colMaNV
			// 
			this.g4colMaNV.DataPropertyName = "UserFullCode";
			this.g4colMaNV.HeaderText = "Mã NV";
			this.g4colMaNV.Name = "g4colMaNV";
			this.g4colMaNV.ReadOnly = true;
			this.g4colMaNV.ToolTipText = "Mã Nhân viên";
			this.g4colMaNV.Width = 55;
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
			this.g1ngay.DataPropertyName = "TimeDate";
			dataGridViewCellStyle2.Format = "d/M";
			this.g1ngay.DefaultCellStyle = dataGridViewCellStyle2;
			this.g1ngay.HeaderText = "Ngày";
			this.g1ngay.Name = "g1ngay";
			this.g1ngay.ReadOnly = true;
			this.g1ngay.Width = 45;
			// 
			// g1th
			// 
			this.g1th.DataPropertyName = "TimeDate";
			dataGridViewCellStyle3.Format = "ddd";
			this.g1th.DefaultCellStyle = dataGridViewCellStyle3;
			this.g1th.HeaderText = "Thứ";
			this.g1th.Name = "g1th";
			this.g1th.ReadOnly = true;
			this.g1th.Width = 45;
			// 
			// g1mota
			// 
			this.g1mota.DataPropertyName = "AbsentDescription";
			this.g1mota.HeaderText = "Mô tả";
			this.g1mota.Name = "g1mota";
			this.g1mota.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.g1mota.Width = 170;
			// 
			// g1cong
			// 
			this.g1cong.DataPropertyName = "Workingday";
			dataGridViewCellStyle4.Format = "#0.##";
			this.g1cong.DefaultCellStyle = dataGridViewCellStyle4;
			this.g1cong.HeaderText = "Công_hide";
			this.g1cong.Name = "g1cong";
			this.g1cong.ReadOnly = true;
			this.g1cong.Visible = false;
			this.g1cong.Width = 45;
			// 
			// g1userfullcode
			// 
			this.g1userfullcode.DataPropertyName = "UserFullCode";
			this.g1userfullcode.HeaderText = "UserFullCode_hide";
			this.g1userfullcode.Name = "g1userfullcode";
			this.g1userfullcode.ReadOnly = true;
			this.g1userfullcode.Visible = false;
			// 
			// g1ID
			// 
			this.g1ID.DataPropertyName = "ID";
			this.g1ID.HeaderText = "ID_hide";
			this.g1ID.Name = "g1ID";
			this.g1ID.ReadOnly = true;
			this.g1ID.Visible = false;
			// 
			// g1absentcode
			// 
			this.g1absentcode.DataPropertyName = "AbsentCode";
			this.g1absentcode.HeaderText = "AbsentCodeHide";
			this.g1absentcode.Name = "g1absentcode";
			this.g1absentcode.Visible = false;
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
			this.panelEx1.Size = new System.Drawing.Size(236, 596);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 10;
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
			this.panelEx2.Size = new System.Drawing.Size(236, 384);
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
			this.expandablePanel2.Size = new System.Drawing.Size(236, 384);
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
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.dgrdDSNVTrgPhg.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
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
			this.dgrdDSNVTrgPhg.Size = new System.Drawing.Size(236, 358);
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
			dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.cUserFullCodeDSNV2.DefaultCellStyle = dataGridViewCellStyle6;
			this.cUserFullCodeDSNV2.HeaderText = "Mã NV";
			this.cUserFullCodeDSNV2.Name = "cUserFullCodeDSNV2";
			this.cUserFullCodeDSNV2.ReadOnly = true;
			this.cUserFullCodeDSNV2.ToolTipText = "Mã Nhân viên";
			this.cUserFullCodeDSNV2.Width = 55;
			// 
			// cUserEnrollNumberDSNV2
			// 
			this.cUserEnrollNumberDSNV2.DataPropertyName = "UserEnrollNumber";
			dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.cUserEnrollNumberDSNV2.DefaultCellStyle = dataGridViewCellStyle7;
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
			this.expandableSplitter1.Size = new System.Drawing.Size(236, 12);
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
			this.expandablePanel1.Size = new System.Drawing.Size(236, 200);
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
			this.treePhongBan.Size = new System.Drawing.Size(236, 174);
			this.treePhongBan.TabIndex = 0;
			// 
			// btnLietKe
			// 
			this.btnLietKe.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnLietKe.ForeColor = System.Drawing.Color.Blue;
			this.btnLietKe.Location = new System.Drawing.Point(535, 42);
			this.btnLietKe.Name = "btnLietKe";
			this.btnLietKe.Size = new System.Drawing.Size(204, 27);
			this.btnLietKe.TabIndex = 14;
			this.btnLietKe.Text = "Liệt kê các khai báo vắng";
			this.btnLietKe.UseVisualStyleBackColor = true;
			this.btnLietKe.Click += new System.EventHandler(this.btnLietKe_Click);
			// 
			// btnXoa
			// 
			this.btnXoa.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnXoa.ForeColor = System.Drawing.Color.Blue;
			this.btnXoa.Location = new System.Drawing.Point(434, 244);
			this.btnXoa.Name = "btnXoa";
			this.btnXoa.Size = new System.Drawing.Size(75, 27);
			this.btnXoa.TabIndex = 6;
			this.btnXoa.Text = "Xóa ";
			this.btnXoa.UseVisualStyleBackColor = true;
			this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
			// 
			// btnThem
			// 
			this.btnThem.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnThem.ForeColor = System.Drawing.Color.Blue;
			this.btnThem.Location = new System.Drawing.Point(341, 244);
			this.btnThem.Name = "btnThem";
			this.btnThem.Size = new System.Drawing.Size(75, 27);
			this.btnThem.TabIndex = 5;
			this.btnThem.Text = "Thêm";
			this.btnThem.UseVisualStyleBackColor = true;
			this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
			// 
			// rdChonNgayTrongKhoang
			// 
			this.rdChonNgayTrongKhoang.AutoSize = true;
			this.rdChonNgayTrongKhoang.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rdChonNgayTrongKhoang.Location = new System.Drawing.Point(245, 14);
			this.rdChonNgayTrongKhoang.Name = "rdChonNgayTrongKhoang";
			this.rdChonNgayTrongKhoang.Size = new System.Drawing.Size(182, 20);
			this.rdChonNgayTrongKhoang.TabIndex = 15;
			this.rdChonNgayTrongKhoang.Text = "Chọn ngày trong khoảng";
			this.rdChonNgayTrongKhoang.UseVisualStyleBackColor = true;
			this.rdChonNgayTrongKhoang.CheckedChanged += new System.EventHandler(this.rdChonNgayTrongKhoang_CheckedChanged);
			// 
			// rdChonNgayTrongThang
			// 
			this.rdChonNgayTrongThang.AutoSize = true;
			this.rdChonNgayTrongThang.Checked = true;
			this.rdChonNgayTrongThang.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rdChonNgayTrongThang.Location = new System.Drawing.Point(245, 45);
			this.rdChonNgayTrongThang.Name = "rdChonNgayTrongThang";
			this.rdChonNgayTrongThang.Size = new System.Drawing.Size(171, 20);
			this.rdChonNgayTrongThang.TabIndex = 15;
			this.rdChonNgayTrongThang.TabStop = true;
			this.rdChonNgayTrongThang.Text = "Chọn ngày trong tháng";
			this.rdChonNgayTrongThang.UseVisualStyleBackColor = true;
			this.rdChonNgayTrongThang.CheckedChanged += new System.EventHandler(this.rdChonNgayTrongKhoang_CheckedChanged);
			// 
			// dtpNgayBD
			// 
			this.dtpNgayBD.CustomFormat = "d/M/yyyy";
			this.dtpNgayBD.Enabled = false;
			this.dtpNgayBD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgayBD.Location = new System.Drawing.Point(428, 14);
			this.dtpNgayBD.Name = "dtpNgayBD";
			this.dtpNgayBD.ShowUpDown = true;
			this.dtpNgayBD.Size = new System.Drawing.Size(98, 22);
			this.dtpNgayBD.TabIndex = 0;
			this.dtpNgayBD.Value = new System.DateTime(2014, 2, 1, 19, 11, 0, 0);
			// 
			// dtpNgayKT
			// 
			this.dtpNgayKT.CustomFormat = "d/M/yyyy";
			this.dtpNgayKT.Enabled = false;
			this.dtpNgayKT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgayKT.Location = new System.Drawing.Point(629, 14);
			this.dtpNgayKT.Name = "dtpNgayKT";
			this.dtpNgayKT.ShowUpDown = true;
			this.dtpNgayKT.Size = new System.Drawing.Size(98, 22);
			this.dtpNgayKT.TabIndex = 1;
			this.dtpNgayKT.Value = new System.DateTime(2014, 2, 1, 19, 11, 0, 0);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(532, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "đến hết ngày";
			// 
			// frm_21_KhaiBaoVang
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(743, 600);
			this.Controls.Add(this.rdChonNgayTrongThang);
			this.Controls.Add(this.rdChonNgayTrongKhoang);
			this.Controls.Add(this.btnLietKe);
			this.Controls.Add(this.btnXoa);
			this.Controls.Add(this.btnThem);
			this.Controls.Add(this.panelEx1);
			this.Controls.Add(this.dgrdNgayVang);
			this.Controls.Add(this.cbLoaiVang);
			this.Controls.Add(this.dtpNgayKT);
			this.Controls.Add(this.dtpNgayBD);
			this.Controls.Add(this.dtpThang);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checklistNgay);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_21_KhaiBaoVang";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Khai báo vắng cho Nhân viên";
			this.Load += new System.EventHandler(this.frm_KhaiBaoVang_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdNgayVang)).EndInit();
			this.panelEx1.ResumeLayout(false);
			this.panelEx2.ResumeLayout(false);
			this.expandablePanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).EndInit();
			this.expandablePanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.DateTimePicker dtpThang;
		private System.Windows.Forms.CheckedListBox checklistNgay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbLoaiVang;
		private System.Windows.Forms.DataGridView dgrdNgayVang;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel2;
		private System.Windows.Forms.DataGridView dgrdDSNVTrgPhg;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private System.Windows.Forms.TreeView treePhongBan;
        private System.Windows.Forms.Button btnLietKe;
        private System.Windows.Forms.Button btnXoa;
		private System.Windows.Forms.Button btnThem;
		private System.Windows.Forms.DataGridViewCheckBoxColumn g1check;
		private System.Windows.Forms.DataGridViewTextBoxColumn g4colMaNV;
		private System.Windows.Forms.DataGridViewTextBoxColumn g2macc;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1tennv;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1ngay;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1th;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1mota;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1cong;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1userfullcode;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1ID;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1absentcode;
		private System.Windows.Forms.DataGridViewCheckBoxColumn cChkDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullCodeDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserEnrollNumberDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cSchNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cTitleNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cDescriptionDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cSchIDDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserIDDDSNV2;
		private System.Windows.Forms.RadioButton rdChonNgayTrongKhoang;
		private System.Windows.Forms.RadioButton rdChonNgayTrongThang;
		private System.Windows.Forms.DateTimePicker dtpNgayBD;
		private System.Windows.Forms.DateTimePicker dtpNgayKT;
		private System.Windows.Forms.Label label1;


    }
}