namespace GiuLaiCacFileCu {
    partial class frm_ChamCongCaDai {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
            this.treePhongBan = new System.Windows.Forms.TreeView();
            this.gp1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridDSNVTrongPhg = new System.Windows.Forms.DataGridView();
            this.cChkDSNV2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cUserFullCodeDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUserEnrollNumberDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUserFullNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSchIDDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSchNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTitleNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUserIDDDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDescriptionDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnMove = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimeNgayChamCaDai = new System.Windows.Forms.DateTimePicker();
            this.btnDongY = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.gpChonPhongBan.SuspendLayout();
            this.gp1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDSNVTrongPhg)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gpChonPhongBan
            // 
            this.gpChonPhongBan.Controls.Add(this.treePhongBan);
            this.gpChonPhongBan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.gpChonPhongBan.Location = new System.Drawing.Point(3, 11);
            this.gpChonPhongBan.Name = "gpChonPhongBan";
            this.gpChonPhongBan.Padding = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.gpChonPhongBan.Size = new System.Drawing.Size(210, 556);
            this.gpChonPhongBan.TabIndex = 2;
            this.gpChonPhongBan.TabStop = false;
            this.gpChonPhongBan.Text = "Chọn phòng ban";
            // 
            // treePhongBan
            // 
            this.treePhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treePhongBan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.treePhongBan.Indent = 18;
            this.treePhongBan.ItemHeight = 20;
            this.treePhongBan.Location = new System.Drawing.Point(3, 20);
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
            this.treePhongBan.Size = new System.Drawing.Size(204, 533);
            this.treePhongBan.TabIndex = 0;
            // 
            // gp1
            // 
            this.gp1.Controls.Add(this.tableLayoutPanel1);
            this.gp1.Location = new System.Drawing.Point(219, 11);
            this.gp1.Name = "gp1";
            this.gp1.Size = new System.Drawing.Size(267, 556);
            this.gp1.TabIndex = 3;
            this.gp1.TabStop = false;
            this.gp1.Text = "Danh sách Nhân viên";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridDSNVTrongPhg, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 537F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(261, 535);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridDSNVTrongPhg
            // 
            this.dataGridDSNVTrongPhg.AllowUserToAddRows = false;
            this.dataGridDSNVTrongPhg.AllowUserToDeleteRows = false;
            this.dataGridDSNVTrongPhg.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridDSNVTrongPhg.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridDSNVTrongPhg.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridDSNVTrongPhg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDSNVTrongPhg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cChkDSNV2,
            this.cUserFullCodeDSNV2,
            this.cUserEnrollNumberDSNV2,
            this.cUserFullNameDSNV2,
            this.cSchIDDSNV2,
            this.cSchNameDSNV2,
            this.cTitleNameDSNV2,
            this.cUserIDDDSNV2,
            this.cDescriptionDSNV2});
            this.dataGridDSNVTrongPhg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridDSNVTrongPhg.GridColor = System.Drawing.Color.Red;
            this.dataGridDSNVTrongPhg.Location = new System.Drawing.Point(3, 3);
            this.dataGridDSNVTrongPhg.Name = "dataGridDSNVTrongPhg";
            this.dataGridDSNVTrongPhg.RowHeadersVisible = false;
            this.dataGridDSNVTrongPhg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridDSNVTrongPhg.Size = new System.Drawing.Size(255, 529);
            this.dataGridDSNVTrongPhg.TabIndex = 2;
            // 
            // cChkDSNV2
            // 
            this.cChkDSNV2.DataPropertyName = "check";
            this.cChkDSNV2.FalseValue = "false";
            this.cChkDSNV2.HeaderText = "";
            this.cChkDSNV2.Name = "cChkDSNV2";
            this.cChkDSNV2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cChkDSNV2.TrueValue = "true";
            this.cChkDSNV2.Visible = false;
            this.cChkDSNV2.Width = 22;
            // 
            // cUserFullCodeDSNV2
            // 
            this.cUserFullCodeDSNV2.DataPropertyName = "UserFullCode";
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.cUserFullCodeDSNV2.DefaultCellStyle = dataGridViewCellStyle2;
            this.cUserFullCodeDSNV2.HeaderText = "Mã NV";
            this.cUserFullCodeDSNV2.Name = "cUserFullCodeDSNV2";
            this.cUserFullCodeDSNV2.ReadOnly = true;
            this.cUserFullCodeDSNV2.ToolTipText = "Mã Nhân viên";
            this.cUserFullCodeDSNV2.Visible = false;
            this.cUserFullCodeDSNV2.Width = 75;
            // 
            // cUserEnrollNumberDSNV2
            // 
            this.cUserEnrollNumberDSNV2.DataPropertyName = "UserEnrollNumber";
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.cUserEnrollNumberDSNV2.DefaultCellStyle = dataGridViewCellStyle3;
            this.cUserEnrollNumberDSNV2.HeaderText = "Mã CC";
            this.cUserEnrollNumberDSNV2.Name = "cUserEnrollNumberDSNV2";
            this.cUserEnrollNumberDSNV2.ReadOnly = true;
            this.cUserEnrollNumberDSNV2.ToolTipText = "Mã Chấm công";
            this.cUserEnrollNumberDSNV2.Width = 75;
            // 
            // cUserFullNameDSNV2
            // 
            this.cUserFullNameDSNV2.DataPropertyName = "UserFullName";
            this.cUserFullNameDSNV2.HeaderText = "Tên NV";
            this.cUserFullNameDSNV2.Name = "cUserFullNameDSNV2";
            this.cUserFullNameDSNV2.ReadOnly = true;
            this.cUserFullNameDSNV2.ToolTipText = "Tên Nhân viên";
            this.cUserFullNameDSNV2.Width = 165;
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
            // cSchNameDSNV2
            // 
            this.cSchNameDSNV2.DataPropertyName = "SchName";
            this.cSchNameDSNV2.HeaderText = "Lịch trình";
            this.cSchNameDSNV2.Name = "cSchNameDSNV2";
            this.cSchNameDSNV2.ReadOnly = true;
            this.cSchNameDSNV2.ToolTipText = "Lịch trình";
            this.cSchNameDSNV2.Visible = false;
            this.cSchNameDSNV2.Width = 95;
            // 
            // cTitleNameDSNV2
            // 
            this.cTitleNameDSNV2.DataPropertyName = "TitleName";
            this.cTitleNameDSNV2.HeaderText = "Chức vụ";
            this.cTitleNameDSNV2.Name = "cTitleNameDSNV2";
            this.cTitleNameDSNV2.ReadOnly = true;
            this.cTitleNameDSNV2.ToolTipText = "Chức vụ";
            this.cTitleNameDSNV2.Visible = false;
            this.cTitleNameDSNV2.Width = 200;
            // 
            // cUserIDDDSNV2
            // 
            this.cUserIDDDSNV2.DataPropertyName = "UserIDD";
            this.cUserIDDDSNV2.HeaderText = "Mã Phòng ban";
            this.cUserIDDDSNV2.Name = "cUserIDDDSNV2";
            this.cUserIDDDSNV2.ReadOnly = true;
            this.cUserIDDDSNV2.Visible = false;
            // 
            // cDescriptionDSNV2
            // 
            this.cDescriptionDSNV2.DataPropertyName = "Description";
            this.cDescriptionDSNV2.HeaderText = "Phòng ban";
            this.cDescriptionDSNV2.Name = "cDescriptionDSNV2";
            this.cDescriptionDSNV2.ReadOnly = true;
            this.cDescriptionDSNV2.ToolTipText = "Phòng ban";
            this.cDescriptionDSNV2.Visible = false;
            this.cDescriptionDSNV2.Width = 105;
            // 
            // btnMove
            // 
            this.btnMove.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMove.Location = new System.Drawing.Point(492, 252);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(32, 26);
            this.btnMove.TabIndex = 5;
            this.btnMove.Text = "=>";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Location = new System.Drawing.Point(530, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 556);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nhân viên được chọn";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 537F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(238, 535);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.Red;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(232, 529);
            this.dataGridView1.TabIndex = 3;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "check";
            this.dataGridViewCheckBoxColumn1.FalseValue = "false";
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn1.TrueValue = "true";
            this.dataGridViewCheckBoxColumn1.Visible = false;
            this.dataGridViewCheckBoxColumn1.Width = 22;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "UserFullCode";
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn1.HeaderText = "Mã NV";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.ToolTipText = "Mã Nhân viên";
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 75;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "UserEnrollNumber";
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn2.HeaderText = "Mã CC";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.ToolTipText = "Mã Chấm công";
            this.dataGridViewTextBoxColumn2.Width = 75;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "UserFullName";
            this.dataGridViewTextBoxColumn3.HeaderText = "Tên NV";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.ToolTipText = "Tên Nhân viên";
            this.dataGridViewTextBoxColumn3.Width = 165;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "SchID";
            this.dataGridViewTextBoxColumn4.HeaderText = "Mã lịch trình";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.ToolTipText = "Mã lịch trình";
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "SchName";
            this.dataGridViewTextBoxColumn5.HeaderText = "Lịch trình";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.ToolTipText = "Lịch trình";
            this.dataGridViewTextBoxColumn5.Visible = false;
            this.dataGridViewTextBoxColumn5.Width = 95;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "TitleName";
            this.dataGridViewTextBoxColumn6.HeaderText = "Chức vụ";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.ToolTipText = "Chức vụ";
            this.dataGridViewTextBoxColumn6.Visible = false;
            this.dataGridViewTextBoxColumn6.Width = 200;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "UserIDD";
            this.dataGridViewTextBoxColumn7.HeaderText = "Mã Phòng ban";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn8.HeaderText = "Phòng ban";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.ToolTipText = "Phòng ban";
            this.dataGridViewTextBoxColumn8.Visible = false;
            this.dataGridViewTextBoxColumn8.Width = 105;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(780, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Chọn ngày";
            // 
            // dateTimeNgayChamCaDai
            // 
            this.dateTimeNgayChamCaDai.CustomFormat = "dddd dd/MM/yyyy";
            this.dateTimeNgayChamCaDai.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeNgayChamCaDai.Location = new System.Drawing.Point(783, 32);
            this.dateTimeNgayChamCaDai.Name = "dateTimeNgayChamCaDai";
            this.dateTimeNgayChamCaDai.Size = new System.Drawing.Size(160, 22);
            this.dateTimeNgayChamCaDai.TabIndex = 15;
            this.dateTimeNgayChamCaDai.Value = new System.DateTime(2013, 6, 20, 23, 30, 0, 0);
            // 
            // btnDongY
            // 
            this.btnDongY.Location = new System.Drawing.Point(783, 60);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(75, 26);
            this.btnDongY.TabIndex = 16;
            this.btnDongY.Text = "Đồng ý";
            this.btnDongY.UseVisualStyleBackColor = true;
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(868, 60);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 26);
            this.btnHuy.TabIndex = 16;
            this.btnHuy.Text = "Huỷ bỏ";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(492, 284);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(32, 26);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "<=";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // frm_ChamCongCaDai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 570);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDongY);
            this.Controls.Add(this.dateTimeNgayChamCaDai);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.gp1);
            this.Controls.Add(this.gpChonPhongBan);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frm_ChamCongCaDai";
            this.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.Text = "ChamCongCaDai";
            this.Load += new System.EventHandler(this.ChamCongCaDai_Load);
            this.gpChonPhongBan.ResumeLayout(false);
            this.gp1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDSNVTrongPhg)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpChonPhongBan;
        private System.Windows.Forms.TreeView treePhongBan;
        private System.Windows.Forms.GroupBox gp1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimeNgayChamCaDai;
        private System.Windows.Forms.Button btnDongY;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.DataGridView dataGridDSNVTrongPhg;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cChkDSNV2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullCodeDSNV2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUserEnrollNumberDSNV2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullNameDSNV2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSchIDDSNV2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSchNameDSNV2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTitleNameDSNV2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUserIDDDSNV2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDescriptionDSNV2;
    }
}