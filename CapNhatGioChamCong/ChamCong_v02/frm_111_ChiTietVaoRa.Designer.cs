namespace ChamCong_v02 {
    partial class frm_111_ChiTietVaoRa {
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.dgrdTongHop = new System.Windows.Forms.DataGridView();
			this.grid1Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.g1colMaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colUserEnrollNumberTongHop = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.textBoxColumnOfDataGrid1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTimeDateTongHop = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colThu = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTimeStrInTongHop = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTimeStrOutTongHop = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.grid1CaShiftCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colGhiChuTongHop = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colobj = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabThemGio = new System.Windows.Forms.TabPage();
			this.dtpVao_Them = new System.Windows.Forms.DateTimePicker();
			this.cbLyDo_Them = new System.Windows.Forms.ComboBox();
			this.tbGhiChu_Them = new System.Windows.Forms.TextBox();
			this.tbTenNV_Them = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.checkRa_Them = new System.Windows.Forms.CheckBox();
			this.cbCaRa_Them = new System.Windows.Forms.ComboBox();
			this.btnThemGio = new System.Windows.Forms.Button();
			this.cbCaVao_Them = new System.Windows.Forms.ComboBox();
			this.dtpRa_Them = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.checkVao_Them = new System.Windows.Forms.CheckBox();
			this.tabSuaGio = new System.Windows.Forms.TabPage();
			this.dtpVao_Sua = new System.Windows.Forms.DateTimePicker();
			this.cbLyDo_Sua = new System.Windows.Forms.ComboBox();
			this.tbGhiChu_Sua = new System.Windows.Forms.TextBox();
			this.tbTenNV_Sua = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.checkRa_Sua = new System.Windows.Forms.CheckBox();
			this.checkVao_Sua = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cbCaRa_Sua = new System.Windows.Forms.ComboBox();
			this.btnSuaGio = new System.Windows.Forms.Button();
			this.cbCaVao_Sua = new System.Windows.Forms.ComboBox();
			this.dtpRa_Sua = new System.Windows.Forms.DateTimePicker();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdTongHop)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabThemGio.SuspendLayout();
			this.tabSuaGio.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.dgrdTongHop);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(1056, 570);
			this.splitContainer1.SplitterDistance = 570;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 5;
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
            this.grid1Check,
            this.g1colMaNV,
            this.colUserEnrollNumberTongHop,
            this.textBoxColumnOfDataGrid1,
            this.colTimeDateTongHop,
            this.colThu,
            this.colTimeStrInTongHop,
            this.colTimeStrOutTongHop,
            this.grid1CaShiftCode,
            this.colGhiChuTongHop,
            this.colobj});
			this.dgrdTongHop.GridColor = System.Drawing.Color.Red;
			this.dgrdTongHop.Location = new System.Drawing.Point(2, 2);
			this.dgrdTongHop.MultiSelect = false;
			this.dgrdTongHop.Name = "dgrdTongHop";
			this.dgrdTongHop.RowHeadersVisible = false;
			this.dgrdTongHop.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdTongHop.Size = new System.Drawing.Size(574, 567);
			this.dgrdTongHop.TabIndex = 1;
			this.dgrdTongHop.SelectionChanged += new System.EventHandler(this.dgrdTongHop_SelectionChanged);
			// 
			// grid1Check
			// 
			this.grid1Check.DataPropertyName = "check";
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.NullValue = false;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.grid1Check.DefaultCellStyle = dataGridViewCellStyle2;
			this.grid1Check.FalseValue = "false";
			this.grid1Check.Frozen = true;
			this.grid1Check.HeaderText = "";
			this.grid1Check.Name = "grid1Check";
			this.grid1Check.TrueValue = "true";
			this.grid1Check.Visible = false;
			this.grid1Check.Width = 24;
			// 
			// g1colMaNV
			// 
			this.g1colMaNV.DataPropertyName = "UserFullCode";
			this.g1colMaNV.HeaderText = "Mã NV";
			this.g1colMaNV.Name = "g1colMaNV";
			this.g1colMaNV.ReadOnly = true;
			this.g1colMaNV.ToolTipText = "Mã Nhân viên";
			this.g1colMaNV.Width = 55;
			// 
			// colUserEnrollNumberTongHop
			// 
			this.colUserEnrollNumberTongHop.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.colUserEnrollNumberTongHop.DataPropertyName = "UserEnrollNumber";
			dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.colUserEnrollNumberTongHop.DefaultCellStyle = dataGridViewCellStyle3;
			this.colUserEnrollNumberTongHop.HeaderText = "Mã CC_hide";
			this.colUserEnrollNumberTongHop.Name = "colUserEnrollNumberTongHop";
			this.colUserEnrollNumberTongHop.ReadOnly = true;
			this.colUserEnrollNumberTongHop.ToolTipText = "Mã chấm công";
			this.colUserEnrollNumberTongHop.Visible = false;
			this.colUserEnrollNumberTongHop.Width = 55;
			// 
			// textBoxColumnOfDataGrid1
			// 
			this.textBoxColumnOfDataGrid1.DataPropertyName = "UserFullName";
			this.textBoxColumnOfDataGrid1.HeaderText = "Tên NV";
			this.textBoxColumnOfDataGrid1.Name = "textBoxColumnOfDataGrid1";
			this.textBoxColumnOfDataGrid1.ReadOnly = true;
			this.textBoxColumnOfDataGrid1.Width = 150;
			// 
			// colTimeDateTongHop
			// 
			this.colTimeDateTongHop.DataPropertyName = "TimeStrNgay";
			dataGridViewCellStyle4.Format = "d/M";
			this.colTimeDateTongHop.DefaultCellStyle = dataGridViewCellStyle4;
			this.colTimeDateTongHop.HeaderText = "Ngày";
			this.colTimeDateTongHop.Name = "colTimeDateTongHop";
			this.colTimeDateTongHop.ReadOnly = true;
			this.colTimeDateTongHop.Width = 45;
			// 
			// colThu
			// 
			this.colThu.DataPropertyName = "TimeStrThu";
			dataGridViewCellStyle5.Format = "ddd";
			this.colThu.DefaultCellStyle = dataGridViewCellStyle5;
			this.colThu.HeaderText = "Thứ";
			this.colThu.Name = "colThu";
			this.colThu.ReadOnly = true;
			this.colThu.Width = 45;
			// 
			// colTimeStrInTongHop
			// 
			this.colTimeStrInTongHop.DataPropertyName = "TimeStrVao";
			dataGridViewCellStyle6.Format = "H:mm d/M";
			this.colTimeStrInTongHop.DefaultCellStyle = dataGridViewCellStyle6;
			this.colTimeStrInTongHop.HeaderText = "Vào";
			this.colTimeStrInTongHop.Name = "colTimeStrInTongHop";
			this.colTimeStrInTongHop.ReadOnly = true;
			this.colTimeStrInTongHop.Width = 80;
			// 
			// colTimeStrOutTongHop
			// 
			this.colTimeStrOutTongHop.DataPropertyName = "TimeStrRa";
			dataGridViewCellStyle7.Format = "H:mm d/M";
			this.colTimeStrOutTongHop.DefaultCellStyle = dataGridViewCellStyle7;
			this.colTimeStrOutTongHop.HeaderText = "Ra";
			this.colTimeStrOutTongHop.Name = "colTimeStrOutTongHop";
			this.colTimeStrOutTongHop.ReadOnly = true;
			this.colTimeStrOutTongHop.Width = 80;
			// 
			// grid1CaShiftCode
			// 
			this.grid1CaShiftCode.DataPropertyName = "ShiftCode";
			this.grid1CaShiftCode.HeaderText = "Ca";
			this.grid1CaShiftCode.Name = "grid1CaShiftCode";
			this.grid1CaShiftCode.ReadOnly = true;
			this.grid1CaShiftCode.Width = 120;
			// 
			// colGhiChuTongHop
			// 
			this.colGhiChuTongHop.DataPropertyName = "ShiftID";
			this.colGhiChuTongHop.HeaderText = "IDCa";
			this.colGhiChuTongHop.Name = "colGhiChuTongHop";
			this.colGhiChuTongHop.ReadOnly = true;
			this.colGhiChuTongHop.Visible = false;
			this.colGhiChuTongHop.Width = 50;
			// 
			// colobj
			// 
			this.colobj.DataPropertyName = "obj";
			this.colobj.HeaderText = "obj";
			this.colobj.Name = "colobj";
			this.colobj.ReadOnly = true;
			this.colobj.Visible = false;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabThemGio);
			this.tabControl1.Controls.Add(this.tabSuaGio);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(481, 570);
			this.tabControl1.TabIndex = 0;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.dgrdTongHop_TabIndexChanged);
			// 
			// tabThemGio
			// 
			this.tabThemGio.Controls.Add(this.dtpVao_Them);
			this.tabThemGio.Controls.Add(this.cbLyDo_Them);
			this.tabThemGio.Controls.Add(this.tbGhiChu_Them);
			this.tabThemGio.Controls.Add(this.tbTenNV_Them);
			this.tabThemGio.Controls.Add(this.label6);
			this.tabThemGio.Controls.Add(this.label9);
			this.tabThemGio.Controls.Add(this.checkRa_Them);
			this.tabThemGio.Controls.Add(this.cbCaRa_Them);
			this.tabThemGio.Controls.Add(this.btnThemGio);
			this.tabThemGio.Controls.Add(this.cbCaVao_Them);
			this.tabThemGio.Controls.Add(this.dtpRa_Them);
			this.tabThemGio.Controls.Add(this.label4);
			this.tabThemGio.Controls.Add(this.label3);
			this.tabThemGio.Controls.Add(this.label5);
			this.tabThemGio.Controls.Add(this.checkVao_Them);
			this.tabThemGio.Location = new System.Drawing.Point(4, 25);
			this.tabThemGio.Name = "tabThemGio";
			this.tabThemGio.Padding = new System.Windows.Forms.Padding(3);
			this.tabThemGio.Size = new System.Drawing.Size(473, 541);
			this.tabThemGio.TabIndex = 0;
			this.tabThemGio.Text = "Thêm giờ";
			this.tabThemGio.UseVisualStyleBackColor = true;
			// 
			// dtpVao_Them
			// 
			this.dtpVao_Them.CalendarFont = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.dtpVao_Them.CustomFormat = "H:mm ddd d/M/yyyy";
			this.dtpVao_Them.Enabled = false;
			this.dtpVao_Them.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpVao_Them.Location = new System.Drawing.Point(78, 45);
			this.dtpVao_Them.Margin = new System.Windows.Forms.Padding(4);
			this.dtpVao_Them.Name = "dtpVao_Them";
			this.dtpVao_Them.ShowUpDown = true;
			this.dtpVao_Them.Size = new System.Drawing.Size(165, 22);
			this.dtpVao_Them.TabIndex = 0;
			this.dtpVao_Them.Value = new System.DateTime(2013, 8, 9, 0, 0, 0, 0);
			// 
			// cbLyDo_Them
			// 
			this.cbLyDo_Them.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.cbLyDo_Them.FormattingEnabled = true;
			this.cbLyDo_Them.Items.AddRange(new object[] {
            "Lý do khác",
            "Quên chấm công"});
			this.cbLyDo_Them.Location = new System.Drawing.Point(78, 113);
			this.cbLyDo_Them.Margin = new System.Windows.Forms.Padding(4);
			this.cbLyDo_Them.Name = "cbLyDo_Them";
			this.cbLyDo_Them.Size = new System.Drawing.Size(254, 24);
			this.cbLyDo_Them.TabIndex = 4;
			// 
			// tbGhiChu_Them
			// 
			this.tbGhiChu_Them.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.tbGhiChu_Them.Location = new System.Drawing.Point(78, 147);
			this.tbGhiChu_Them.Margin = new System.Windows.Forms.Padding(4);
			this.tbGhiChu_Them.Multiline = true;
			this.tbGhiChu_Them.Name = "tbGhiChu_Them";
			this.tbGhiChu_Them.Size = new System.Drawing.Size(384, 44);
			this.tbGhiChu_Them.TabIndex = 5;
			// 
			// tbTenNV_Them
			// 
			this.tbTenNV_Them.Location = new System.Drawing.Point(78, 12);
			this.tbTenNV_Them.Name = "tbTenNV_Them";
			this.tbTenNV_Them.ReadOnly = true;
			this.tbTenNV_Them.Size = new System.Drawing.Size(165, 22);
			this.tbTenNV_Them.TabIndex = 26;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label6.Location = new System.Drawing.Point(25, 150);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53, 16);
			this.label6.TabIndex = 29;
			this.label6.Text = "Ghi chú";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label9.Location = new System.Drawing.Point(25, 116);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(40, 16);
			this.label9.TabIndex = 30;
			this.label9.Text = "Lý do";
			// 
			// checkRa_Them
			// 
			this.checkRa_Them.AutoSize = true;
			this.checkRa_Them.Location = new System.Drawing.Point(9, 80);
			this.checkRa_Them.Name = "checkRa_Them";
			this.checkRa_Them.Size = new System.Drawing.Size(64, 20);
			this.checkRa_Them.TabIndex = 27;
			this.checkRa_Them.Text = "Giờ ra";
			this.checkRa_Them.UseVisualStyleBackColor = true;
			this.checkRa_Them.CheckedChanged += new System.EventHandler(this.checkBox_Them_CheckedChanged);
			// 
			// cbCaRa_Them
			// 
			this.cbCaRa_Them.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCaRa_Them.Enabled = false;
			this.cbCaRa_Them.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.cbCaRa_Them.FormattingEnabled = true;
			this.cbCaRa_Them.Items.AddRange(new object[] {
            "Ca 1"});
			this.cbCaRa_Them.Location = new System.Drawing.Point(332, 79);
			this.cbCaRa_Them.Margin = new System.Windows.Forms.Padding(4);
			this.cbCaRa_Them.Name = "cbCaRa_Them";
			this.cbCaRa_Them.Size = new System.Drawing.Size(140, 24);
			this.cbCaRa_Them.TabIndex = 3;
			// 
			// btnThemGio
			// 
			this.btnThemGio.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnThemGio.ForeColor = System.Drawing.Color.Blue;
			this.btnThemGio.Location = new System.Drawing.Point(78, 199);
			this.btnThemGio.Margin = new System.Windows.Forms.Padding(4);
			this.btnThemGio.Name = "btnThemGio";
			this.btnThemGio.Size = new System.Drawing.Size(100, 27);
			this.btnThemGio.TabIndex = 6;
			this.btnThemGio.Text = "Thêm giờ";
			this.btnThemGio.UseVisualStyleBackColor = true;
			this.btnThemGio.Click += new System.EventHandler(this.btnThemGio_Click);
			// 
			// cbCaVao_Them
			// 
			this.cbCaVao_Them.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCaVao_Them.Enabled = false;
			this.cbCaVao_Them.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.cbCaVao_Them.FormattingEnabled = true;
			this.cbCaVao_Them.Items.AddRange(new object[] {
            "Ca 1"});
			this.cbCaVao_Them.Location = new System.Drawing.Point(332, 45);
			this.cbCaVao_Them.Margin = new System.Windows.Forms.Padding(4);
			this.cbCaVao_Them.Name = "cbCaVao_Them";
			this.cbCaVao_Them.Size = new System.Drawing.Size(140, 24);
			this.cbCaVao_Them.TabIndex = 1;
			// 
			// dtpRa_Them
			// 
			this.dtpRa_Them.CalendarFont = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.dtpRa_Them.CustomFormat = "H:mm ddd d/M/yyyy";
			this.dtpRa_Them.Enabled = false;
			this.dtpRa_Them.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpRa_Them.Location = new System.Drawing.Point(78, 79);
			this.dtpRa_Them.Margin = new System.Windows.Forms.Padding(4);
			this.dtpRa_Them.Name = "dtpRa_Them";
			this.dtpRa_Them.ShowUpDown = true;
			this.dtpRa_Them.Size = new System.Drawing.Size(165, 22);
			this.dtpRa_Them.TabIndex = 2;
			this.dtpRa_Them.Value = new System.DateTime(2013, 8, 9, 0, 0, 0, 0);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(247, 82);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(85, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Chọn theo ca";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(247, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(85, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Chọn theo ca";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(25, 15);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(51, 16);
			this.label5.TabIndex = 0;
			this.label5.Text = "Tên NV";
			// 
			// checkVao_Them
			// 
			this.checkVao_Them.AutoSize = true;
			this.checkVao_Them.Location = new System.Drawing.Point(9, 47);
			this.checkVao_Them.Name = "checkVao_Them";
			this.checkVao_Them.Size = new System.Drawing.Size(72, 20);
			this.checkVao_Them.TabIndex = 27;
			this.checkVao_Them.Text = "Giờ vào";
			this.checkVao_Them.UseVisualStyleBackColor = true;
			this.checkVao_Them.CheckedChanged += new System.EventHandler(this.checkBox_Them_CheckedChanged);
			// 
			// tabSuaGio
			// 
			this.tabSuaGio.Controls.Add(this.dtpVao_Sua);
			this.tabSuaGio.Controls.Add(this.cbLyDo_Sua);
			this.tabSuaGio.Controls.Add(this.tbGhiChu_Sua);
			this.tabSuaGio.Controls.Add(this.tbTenNV_Sua);
			this.tabSuaGio.Controls.Add(this.label10);
			this.tabSuaGio.Controls.Add(this.label11);
			this.tabSuaGio.Controls.Add(this.checkRa_Sua);
			this.tabSuaGio.Controls.Add(this.checkVao_Sua);
			this.tabSuaGio.Controls.Add(this.label1);
			this.tabSuaGio.Controls.Add(this.cbCaRa_Sua);
			this.tabSuaGio.Controls.Add(this.btnSuaGio);
			this.tabSuaGio.Controls.Add(this.cbCaVao_Sua);
			this.tabSuaGio.Controls.Add(this.dtpRa_Sua);
			this.tabSuaGio.Controls.Add(this.label7);
			this.tabSuaGio.Controls.Add(this.label8);
			this.tabSuaGio.Location = new System.Drawing.Point(4, 22);
			this.tabSuaGio.Name = "tabSuaGio";
			this.tabSuaGio.Padding = new System.Windows.Forms.Padding(3);
			this.tabSuaGio.Size = new System.Drawing.Size(473, 544);
			this.tabSuaGio.TabIndex = 1;
			this.tabSuaGio.Text = "Sửa giờ";
			this.tabSuaGio.UseVisualStyleBackColor = true;
			// 
			// dtpVao_Sua
			// 
			this.dtpVao_Sua.CalendarFont = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.dtpVao_Sua.CustomFormat = "H:mm ddd d/M/yyyy";
			this.dtpVao_Sua.Enabled = false;
			this.dtpVao_Sua.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpVao_Sua.Location = new System.Drawing.Point(78, 45);
			this.dtpVao_Sua.Margin = new System.Windows.Forms.Padding(4);
			this.dtpVao_Sua.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
			this.dtpVao_Sua.Name = "dtpVao_Sua";
			this.dtpVao_Sua.ShowUpDown = true;
			this.dtpVao_Sua.Size = new System.Drawing.Size(165, 22);
			this.dtpVao_Sua.TabIndex = 0;
			this.dtpVao_Sua.Value = new System.DateTime(2013, 8, 9, 0, 0, 0, 0);
			// 
			// cbLyDo_Sua
			// 
			this.cbLyDo_Sua.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.cbLyDo_Sua.FormattingEnabled = true;
			this.cbLyDo_Sua.Items.AddRange(new object[] {
            "Lý do khác",
            "Công tác",
            "Học, họp, phong trào",
            "Mua vật dụng cho nhà máy"});
			this.cbLyDo_Sua.Location = new System.Drawing.Point(78, 113);
			this.cbLyDo_Sua.Margin = new System.Windows.Forms.Padding(4);
			this.cbLyDo_Sua.Name = "cbLyDo_Sua";
			this.cbLyDo_Sua.Size = new System.Drawing.Size(254, 24);
			this.cbLyDo_Sua.TabIndex = 4;
			// 
			// tbGhiChu_Sua
			// 
			this.tbGhiChu_Sua.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.tbGhiChu_Sua.Location = new System.Drawing.Point(78, 147);
			this.tbGhiChu_Sua.Margin = new System.Windows.Forms.Padding(4);
			this.tbGhiChu_Sua.Multiline = true;
			this.tbGhiChu_Sua.Name = "tbGhiChu_Sua";
			this.tbGhiChu_Sua.Size = new System.Drawing.Size(384, 44);
			this.tbGhiChu_Sua.TabIndex = 5;
			// 
			// tbTenNV_Sua
			// 
			this.tbTenNV_Sua.Location = new System.Drawing.Point(78, 10);
			this.tbTenNV_Sua.Name = "tbTenNV_Sua";
			this.tbTenNV_Sua.ReadOnly = true;
			this.tbTenNV_Sua.Size = new System.Drawing.Size(165, 22);
			this.tbTenNV_Sua.TabIndex = 37;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label10.Location = new System.Drawing.Point(25, 150);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(53, 16);
			this.label10.TabIndex = 42;
			this.label10.Text = "Ghi chú";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.label11.Location = new System.Drawing.Point(25, 116);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(40, 16);
			this.label11.TabIndex = 43;
			this.label11.Text = "Lý do";
			// 
			// checkRa_Sua
			// 
			this.checkRa_Sua.AutoSize = true;
			this.checkRa_Sua.Location = new System.Drawing.Point(9, 80);
			this.checkRa_Sua.Name = "checkRa_Sua";
			this.checkRa_Sua.Size = new System.Drawing.Size(64, 20);
			this.checkRa_Sua.TabIndex = 40;
			this.checkRa_Sua.Text = "Giờ ra";
			this.checkRa_Sua.UseVisualStyleBackColor = true;
			this.checkRa_Sua.CheckedChanged += new System.EventHandler(this.checkBox_Sua_CheckedChanged);
			// 
			// checkVao_Sua
			// 
			this.checkVao_Sua.AutoSize = true;
			this.checkVao_Sua.Location = new System.Drawing.Point(9, 47);
			this.checkVao_Sua.Name = "checkVao_Sua";
			this.checkVao_Sua.Size = new System.Drawing.Size(72, 20);
			this.checkVao_Sua.TabIndex = 39;
			this.checkVao_Sua.Text = "Giờ vào";
			this.checkVao_Sua.UseVisualStyleBackColor = true;
			this.checkVao_Sua.CheckedChanged += new System.EventHandler(this.checkBox_Sua_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(25, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 16);
			this.label1.TabIndex = 38;
			this.label1.Text = "Tên NV";
			// 
			// cbCaRa_Sua
			// 
			this.cbCaRa_Sua.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCaRa_Sua.Enabled = false;
			this.cbCaRa_Sua.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.cbCaRa_Sua.FormattingEnabled = true;
			this.cbCaRa_Sua.Items.AddRange(new object[] {
            "Ca 1"});
			this.cbCaRa_Sua.Location = new System.Drawing.Point(332, 79);
			this.cbCaRa_Sua.Margin = new System.Windows.Forms.Padding(4);
			this.cbCaRa_Sua.Name = "cbCaRa_Sua";
			this.cbCaRa_Sua.Size = new System.Drawing.Size(140, 24);
			this.cbCaRa_Sua.TabIndex = 3;
			// 
			// btnSuaGio
			// 
			this.btnSuaGio.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSuaGio.ForeColor = System.Drawing.Color.Blue;
			this.btnSuaGio.Location = new System.Drawing.Point(78, 199);
			this.btnSuaGio.Name = "btnSuaGio";
			this.btnSuaGio.Size = new System.Drawing.Size(100, 27);
			this.btnSuaGio.TabIndex = 6;
			this.btnSuaGio.Text = "Sửa giờ";
			this.btnSuaGio.UseVisualStyleBackColor = true;
			this.btnSuaGio.Click += new System.EventHandler(this.btnSuaGio_Click);
			// 
			// cbCaVao_Sua
			// 
			this.cbCaVao_Sua.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCaVao_Sua.Enabled = false;
			this.cbCaVao_Sua.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.cbCaVao_Sua.FormattingEnabled = true;
			this.cbCaVao_Sua.Items.AddRange(new object[] {
            "Ca 1"});
			this.cbCaVao_Sua.Location = new System.Drawing.Point(332, 45);
			this.cbCaVao_Sua.Margin = new System.Windows.Forms.Padding(4);
			this.cbCaVao_Sua.Name = "cbCaVao_Sua";
			this.cbCaVao_Sua.Size = new System.Drawing.Size(140, 24);
			this.cbCaVao_Sua.TabIndex = 1;
			// 
			// dtpRa_Sua
			// 
			this.dtpRa_Sua.CalendarFont = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.dtpRa_Sua.CustomFormat = "H:mm ddd d/M/yyyy";
			this.dtpRa_Sua.Enabled = false;
			this.dtpRa_Sua.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpRa_Sua.Location = new System.Drawing.Point(78, 79);
			this.dtpRa_Sua.Margin = new System.Windows.Forms.Padding(4);
			this.dtpRa_Sua.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
			this.dtpRa_Sua.Name = "dtpRa_Sua";
			this.dtpRa_Sua.ShowUpDown = true;
			this.dtpRa_Sua.Size = new System.Drawing.Size(165, 22);
			this.dtpRa_Sua.TabIndex = 2;
			this.dtpRa_Sua.Value = new System.DateTime(2013, 8, 9, 0, 0, 0, 0);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(247, 82);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(85, 16);
			this.label7.TabIndex = 28;
			this.label7.Text = "Chọn theo ca";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(247, 48);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(85, 16);
			this.label8.TabIndex = 29;
			this.label8.Text = "Chọn theo ca";
			// 
			// frm_111_ChiTietVaoRa
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1056, 570);
			this.Controls.Add(this.splitContainer1);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_111_ChiTietVaoRa";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Chi tiết giờ vào ra";
			this.Load += new System.EventHandler(this.frm_ChiTietVaoRa_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrdTongHop)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabThemGio.ResumeLayout(false);
			this.tabThemGio.PerformLayout();
			this.tabSuaGio.ResumeLayout(false);
			this.tabSuaGio.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.DataGridView dgrdTongHop;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabThemGio;
		private System.Windows.Forms.DateTimePicker dtpVao_Them;
		private System.Windows.Forms.ComboBox cbLyDo_Them;
		private System.Windows.Forms.TextBox tbGhiChu_Them;
		private System.Windows.Forms.TextBox tbTenNV_Them;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox checkRa_Them;
		private System.Windows.Forms.ComboBox cbCaRa_Them;
		private System.Windows.Forms.Button btnThemGio;
		private System.Windows.Forms.ComboBox cbCaVao_Them;
		private System.Windows.Forms.DateTimePicker dtpRa_Them;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkVao_Them;
		private System.Windows.Forms.TabPage tabSuaGio;
		private System.Windows.Forms.DateTimePicker dtpVao_Sua;
		private System.Windows.Forms.ComboBox cbLyDo_Sua;
		private System.Windows.Forms.TextBox tbGhiChu_Sua;
		private System.Windows.Forms.TextBox tbTenNV_Sua;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.CheckBox checkRa_Sua;
		private System.Windows.Forms.CheckBox checkVao_Sua;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbCaRa_Sua;
		private System.Windows.Forms.Button btnSuaGio;
		private System.Windows.Forms.ComboBox cbCaVao_Sua;
		private System.Windows.Forms.DateTimePicker dtpRa_Sua;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.DataGridViewCheckBoxColumn grid1Check;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1colMaNV;
		private System.Windows.Forms.DataGridViewTextBoxColumn colUserEnrollNumberTongHop;
		private System.Windows.Forms.DataGridViewTextBoxColumn textBoxColumnOfDataGrid1;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTimeDateTongHop;
		private System.Windows.Forms.DataGridViewTextBoxColumn colThu;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTimeStrInTongHop;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTimeStrOutTongHop;
		private System.Windows.Forms.DataGridViewTextBoxColumn grid1CaShiftCode;
		private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChuTongHop;
		private System.Windows.Forms.DataGridViewTextBoxColumn colobj;
    }
}