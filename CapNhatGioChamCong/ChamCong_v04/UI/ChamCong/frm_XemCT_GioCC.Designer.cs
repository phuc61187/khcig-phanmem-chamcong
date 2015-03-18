namespace ChamCong_v04.UI.ChamCong {
	partial class frm_XemCT_GioCC {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_XemCT_GioCC));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.btnSuaa = new System.Windows.Forms.Button();
			this.chkGioRaa = new System.Windows.Forms.CheckBox();
			this.chkGioVao = new System.Windows.Forms.CheckBox();
			this.dtpVao_Them = new System.Windows.Forms.DateTimePicker();
			this.dtpRaa_Them = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.tbTenNV = new System.Windows.Forms.TextBox();
			this.gpNhapThongTin = new System.Windows.Forms.GroupBox();
			this.cbLyDo_Them = new System.Windows.Forms.ComboBox();
			this.btnChonCa_Them = new System.Windows.Forms.Button();
			this.btnThem = new System.Windows.Forms.Button();
			this.tbGhichu_Them = new System.Windows.Forms.TextBox();
			this.tbCa_Them = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.dgrdGioKDQD = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c13 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.label3 = new System.Windows.Forms.Label();
			this.tbMaCC = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.btnChonCa_Suaa = new System.Windows.Forms.Button();
			this.cbLyDo_Suaa = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.dtpGioMoi_Sua = new System.Windows.Forms.DateTimePicker();
			this.label6 = new System.Windows.Forms.Label();
			this.tbGhiChu_Suaa = new System.Windows.Forms.TextBox();
			this.tbCa_Suaa = new System.Windows.Forms.TextBox();
			this.tbGioCu_Suaa = new System.Windows.Forms.TextBox();
			this.lbGioMoi = new System.Windows.Forms.Label();
			this.btnChuyenDoi = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.cbLyDo_Xoaa = new System.Windows.Forms.ComboBox();
			this.btnThoat = new System.Windows.Forms.Button();
			this.btnXoaa = new System.Windows.Forms.Button();
			this.tbGhiChu_Xoaa = new System.Windows.Forms.TextBox();
			this.tbGioCuu_ChuyenDoi = new System.Windows.Forms.TextBox();
			this.tbGioCu_Xoaa = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.toolTipHint = new System.Windows.Forms.ToolTip(this.components);
			this.gpNhapThongTin.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdGioKDQD)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnSuaa
			// 
			this.btnSuaa.ForeColor = System.Drawing.Color.Blue;
			this.btnSuaa.Location = new System.Drawing.Point(91, 188);
			this.btnSuaa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnSuaa.Name = "btnSuaa";
			this.btnSuaa.Size = new System.Drawing.Size(153, 25);
			this.btnSuaa.TabIndex = 4;
			this.btnSuaa.Text = "Sửa";
			this.toolTipHint.SetToolTip(this.btnSuaa, "1. Quét chọn 1 dòng giờ vào\r\nhoặc giờ ra cần sửa\r\n2. Chọn ca làm việc để điền\r\ngi" +
        "ờ vào ra ca bên dưới\r\n3. Thay đổi giờ nếu cần\r\n4. Nhập lý do, ghi chú\r\n5. Thực h" +
        "iện sửa giờ chấm công");
			this.btnSuaa.UseVisualStyleBackColor = true;
			this.btnSuaa.Click += new System.EventHandler(this.btnSuaa_Click);
			// 
			// chkGioRaa
			// 
			this.chkGioRaa.AutoSize = true;
			this.chkGioRaa.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.chkGioRaa.Location = new System.Drawing.Point(4, 85);
			this.chkGioRaa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.chkGioRaa.Name = "chkGioRaa";
			this.chkGioRaa.Size = new System.Drawing.Size(60, 19);
			this.chkGioRaa.TabIndex = 3;
			this.chkGioRaa.Text = "Giờ ra";
			this.chkGioRaa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTipHint.SetToolTip(this.chkGioRaa, "Đánh dấu mục này nếu muốn thêm giờ ra");
			this.chkGioRaa.UseVisualStyleBackColor = true;
			// 
			// chkGioVao
			// 
			this.chkGioVao.AutoSize = true;
			this.chkGioVao.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.chkGioVao.Location = new System.Drawing.Point(4, 58);
			this.chkGioVao.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.chkGioVao.Name = "chkGioVao";
			this.chkGioVao.Size = new System.Drawing.Size(68, 19);
			this.chkGioVao.TabIndex = 1;
			this.chkGioVao.Text = "Giờ vào";
			this.chkGioVao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTipHint.SetToolTip(this.chkGioVao, "Đánh dấu mục này nếu muốn thêm giờ vào");
			this.chkGioVao.UseVisualStyleBackColor = true;
			// 
			// dtpVao_Them
			// 
			this.dtpVao_Them.CustomFormat = "H:mm ddd d/M/yyyy";
			this.dtpVao_Them.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.dtpVao_Them.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpVao_Them.Location = new System.Drawing.Point(77, 56);
			this.dtpVao_Them.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dtpVao_Them.Name = "dtpVao_Them";
			this.dtpVao_Them.ShowUpDown = true;
			this.dtpVao_Them.Size = new System.Drawing.Size(155, 21);
			this.dtpVao_Them.TabIndex = 2;
			this.toolTipHint.SetToolTip(this.dtpVao_Them, "Nhập giờ vào");
			// 
			// dtpRaa_Them
			// 
			this.dtpRaa_Them.CustomFormat = "H:mm ddd d/M/yyyy";
			this.dtpRaa_Them.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.dtpRaa_Them.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpRaa_Them.Location = new System.Drawing.Point(77, 85);
			this.dtpRaa_Them.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dtpRaa_Them.Name = "dtpRaa_Them";
			this.dtpRaa_Them.ShowUpDown = true;
			this.dtpRaa_Them.Size = new System.Drawing.Size(155, 21);
			this.dtpRaa_Them.TabIndex = 4;
			this.toolTipHint.SetToolTip(this.dtpRaa_Them, "Nhập giờ ra");
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label2.Location = new System.Drawing.Point(189, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "Tên nhân viên";
			// 
			// tbTenNV
			// 
			this.tbTenNV.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.tbTenNV.Location = new System.Drawing.Point(283, 12);
			this.tbTenNV.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tbTenNV.Name = "tbTenNV";
			this.tbTenNV.ReadOnly = true;
			this.tbTenNV.Size = new System.Drawing.Size(200, 21);
			this.tbTenNV.TabIndex = 1;
			this.tbTenNV.Text = "Huỳnh thị ngọc sương";
			// 
			// gpNhapThongTin
			// 
			this.gpNhapThongTin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.gpNhapThongTin.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.gpNhapThongTin.Controls.Add(this.cbLyDo_Them);
			this.gpNhapThongTin.Controls.Add(this.btnChonCa_Them);
			this.gpNhapThongTin.Controls.Add(this.btnThem);
			this.gpNhapThongTin.Controls.Add(this.dtpVao_Them);
			this.gpNhapThongTin.Controls.Add(this.chkGioRaa);
			this.gpNhapThongTin.Controls.Add(this.tbGhichu_Them);
			this.gpNhapThongTin.Controls.Add(this.chkGioVao);
			this.gpNhapThongTin.Controls.Add(this.tbCa_Them);
			this.gpNhapThongTin.Controls.Add(this.label4);
			this.gpNhapThongTin.Controls.Add(this.label1);
			this.gpNhapThongTin.Controls.Add(this.dtpRaa_Them);
			this.gpNhapThongTin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.gpNhapThongTin.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.gpNhapThongTin.ForeColor = System.Drawing.SystemColors.ControlText;
			this.gpNhapThongTin.Location = new System.Drawing.Point(12, 231);
			this.gpNhapThongTin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gpNhapThongTin.Name = "gpNhapThongTin";
			this.gpNhapThongTin.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gpNhapThongTin.Size = new System.Drawing.Size(238, 251);
			this.gpNhapThongTin.TabIndex = 12;
			this.gpNhapThongTin.TabStop = false;
			this.gpNhapThongTin.Text = "BỔ SUNG THIẾU GIỜ CHẤM CÔNG";
			// 
			// cbLyDo_Them
			// 
			this.cbLyDo_Them.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.cbLyDo_Them.FormattingEnabled = true;
			this.cbLyDo_Them.Items.AddRange(new object[] {
            "Quên chấm công",
            "Lý do khác"});
			this.cbLyDo_Them.Location = new System.Drawing.Point(77, 113);
			this.cbLyDo_Them.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cbLyDo_Them.Name = "cbLyDo_Them";
			this.cbLyDo_Them.Size = new System.Drawing.Size(155, 23);
			this.cbLyDo_Them.TabIndex = 5;
			this.cbLyDo_Them.Text = "Quên chấm công";
			// 
			// btnChonCa_Them
			// 
			this.btnChonCa_Them.ForeColor = System.Drawing.Color.Blue;
			this.btnChonCa_Them.Location = new System.Drawing.Point(4, 22);
			this.btnChonCa_Them.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnChonCa_Them.Name = "btnChonCa_Them";
			this.btnChonCa_Them.Size = new System.Drawing.Size(65, 25);
			this.btnChonCa_Them.TabIndex = 0;
			this.btnChonCa_Them.Text = "Chọn ca";
			this.toolTipHint.SetToolTip(this.btnChonCa_Them, "Mở khung danh sách\r\ncác ca làm việc để chọn");
			this.btnChonCa_Them.UseVisualStyleBackColor = true;
			this.btnChonCa_Them.Click += new System.EventHandler(this.btnChonCa_Them_Click);
			// 
			// btnThem
			// 
			this.btnThem.ForeColor = System.Drawing.Color.Blue;
			this.btnThem.Location = new System.Drawing.Point(77, 188);
			this.btnThem.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnThem.Name = "btnThem";
			this.btnThem.Size = new System.Drawing.Size(154, 25);
			this.btnThem.TabIndex = 7;
			this.btnThem.Text = "Thêm";
			this.toolTipHint.SetToolTip(this.btnThem, resources.GetString("btnThem.ToolTip"));
			this.btnThem.UseVisualStyleBackColor = true;
			this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
			// 
			// tbGhichu_Them
			// 
			this.tbGhichu_Them.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.tbGhichu_Them.Location = new System.Drawing.Point(77, 143);
			this.tbGhichu_Them.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tbGhichu_Them.Multiline = true;
			this.tbGhichu_Them.Name = "tbGhichu_Them";
			this.tbGhichu_Them.Size = new System.Drawing.Size(155, 37);
			this.tbGhichu_Them.TabIndex = 6;
			// 
			// tbCa_Them
			// 
			this.tbCa_Them.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.tbCa_Them.Location = new System.Drawing.Point(77, 24);
			this.tbCa_Them.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tbCa_Them.Name = "tbCa_Them";
			this.tbCa_Them.ReadOnly = true;
			this.tbCa_Them.Size = new System.Drawing.Size(154, 21);
			this.tbCa_Them.TabIndex = 1;
			this.toolTipHint.SetToolTip(this.tbCa_Them, "Ca làm việc được chọn");
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label4.Location = new System.Drawing.Point(20, 146);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(49, 15);
			this.label4.TabIndex = 2;
			this.label4.Text = "Ghi chú";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label1.Location = new System.Drawing.Point(20, 116);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "Lý do";
			// 
			// dgrdGioKDQD
			// 
			this.dgrdGioKDQD.AllowUserToAddRows = false;
			this.dgrdGioKDQD.AllowUserToDeleteRows = false;
			this.dgrdGioKDQD.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
			this.dgrdGioKDQD.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgrdGioKDQD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.dgrdGioKDQD.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgrdGioKDQD.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dgrdGioKDQD.ColumnHeadersHeight = 25;
			this.dgrdGioKDQD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn14,
            this.g1c7,
            this.g1c3,
            this.g1c9,
            this.dataGridViewTextBoxColumn15,
            this.g1c10,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn28,
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewTextBoxColumn32,
            this.g1c13});
			this.dgrdGioKDQD.GridColor = System.Drawing.Color.LightGray;
			this.dgrdGioKDQD.Location = new System.Drawing.Point(12, 41);
			this.dgrdGioKDQD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dgrdGioKDQD.MultiSelect = false;
			this.dgrdGioKDQD.Name = "dgrdGioKDQD";
			this.dgrdGioKDQD.RowHeadersVisible = false;
			this.dgrdGioKDQD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdGioKDQD.Size = new System.Drawing.Size(702, 179);
			this.dgrdGioKDQD.TabIndex = 28;
			this.dgrdGioKDQD.SelectionChanged += new System.EventHandler(this.dgrdGioKDQD_SelectionChanged);
			// 
			// dataGridViewTextBoxColumn14
			// 
			this.dataGridViewTextBoxColumn14.DataPropertyName = "TimeStrNgay";
			dataGridViewCellStyle2.Format = "ddd d/M";
			this.dataGridViewTextBoxColumn14.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewTextBoxColumn14.HeaderText = "Ngày công";
			this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
			this.dataGridViewTextBoxColumn14.ReadOnly = true;
			this.dataGridViewTextBoxColumn14.ToolTipText = "Ngày";
			this.dataGridViewTextBoxColumn14.Width = 85;
			// 
			// g1c7
			// 
			this.g1c7.DataPropertyName = "TimeStr";
			dataGridViewCellStyle3.Format = "H:mm:ss ddd d/M";
			this.g1c7.DefaultCellStyle = dataGridViewCellStyle3;
			this.g1c7.HeaderText = "Thời gian";
			this.g1c7.Name = "g1c7";
			this.g1c7.ReadOnly = true;
			this.g1c7.Width = 140;
			// 
			// g1c3
			// 
			this.g1c3.DataPropertyName = "Loai";
			this.g1c3.HeaderText = "Loại";
			this.g1c3.Name = "g1c3";
			this.g1c3.ReadOnly = true;
			this.g1c3.Width = 55;
			// 
			// g1c9
			// 
			this.g1c9.DataPropertyName = "MachineNo";
			this.g1c9.HeaderText = "Máy";
			this.g1c9.Name = "g1c9";
			this.g1c9.ReadOnly = true;
			this.g1c9.ToolTipText = "Số hiệu máy chấm công";
			this.g1c9.Width = 55;
			// 
			// dataGridViewTextBoxColumn15
			// 
			this.dataGridViewTextBoxColumn15.DataPropertyName = "ShiftCode";
			this.dataGridViewTextBoxColumn15.HeaderText = "Ca";
			this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
			this.dataGridViewTextBoxColumn15.ReadOnly = true;
			this.dataGridViewTextBoxColumn15.ToolTipText = "Ca làm việc trong ngày";
			this.dataGridViewTextBoxColumn15.Width = 180;
			// 
			// g1c10
			// 
			this.g1c10.DataPropertyName = "Source";
			this.g1c10.HeaderText = "Nguồn";
			this.g1c10.Name = "g1c10";
			this.g1c10.ReadOnly = true;
			this.g1c10.ToolTipText = "Nguồn check vân tay: FP: finger print, PC: máy tính";
			this.g1c10.Width = 55;
			// 
			// dataGridViewTextBoxColumn13
			// 
			this.dataGridViewTextBoxColumn13.DataPropertyName = "TimeStrThu";
			dataGridViewCellStyle4.Format = "ddd";
			this.dataGridViewTextBoxColumn13.DefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewTextBoxColumn13.HeaderText = "Thứ_hide_suggest_delete";
			this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
			this.dataGridViewTextBoxColumn13.ReadOnly = true;
			this.dataGridViewTextBoxColumn13.ToolTipText = "Thứ";
			this.dataGridViewTextBoxColumn13.Visible = false;
			this.dataGridViewTextBoxColumn13.Width = 45;
			// 
			// dataGridViewTextBoxColumn28
			// 
			this.dataGridViewTextBoxColumn28.DataPropertyName = "cCheck";
			this.dataGridViewTextBoxColumn28.HeaderText = "cChk_hide";
			this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
			this.dataGridViewTextBoxColumn28.ReadOnly = true;
			this.dataGridViewTextBoxColumn28.Visible = false;
			// 
			// dataGridViewTextBoxColumn31
			// 
			this.dataGridViewTextBoxColumn31.DataPropertyName = "cNgayCong";
			this.dataGridViewTextBoxColumn31.HeaderText = "cNgayCong_hide";
			this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
			this.dataGridViewTextBoxColumn31.ReadOnly = true;
			this.dataGridViewTextBoxColumn31.Visible = false;
			// 
			// dataGridViewTextBoxColumn32
			// 
			this.dataGridViewTextBoxColumn32.DataPropertyName = "cUserInfo";
			this.dataGridViewTextBoxColumn32.HeaderText = "cUserInfo_hide";
			this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
			this.dataGridViewTextBoxColumn32.Visible = false;
			// 
			// g1c13
			// 
			this.g1c13.DataPropertyName = "IsEdited";
			this.g1c13.FalseValue = "false";
			this.g1c13.HeaderText = "IsEdited_hide";
			this.g1c13.Name = "g1c13";
			this.g1c13.ReadOnly = true;
			this.g1c13.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.g1c13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.g1c13.TrueValue = "true";
			this.g1c13.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label3.Location = new System.Drawing.Point(9, 15);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 15);
			this.label3.TabIndex = 30;
			this.label3.Text = "Mã nhân viên";
			// 
			// tbMaCC
			// 
			this.tbMaCC.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.tbMaCC.Location = new System.Drawing.Point(114, 12);
			this.tbMaCC.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tbMaCC.Name = "tbMaCC";
			this.tbMaCC.ReadOnly = true;
			this.tbMaCC.Size = new System.Drawing.Size(56, 21);
			this.tbMaCC.TabIndex = 29;
			this.tbMaCC.Text = "01000";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.btnChonCa_Suaa);
			this.groupBox1.Controls.Add(this.cbLyDo_Suaa);
			this.groupBox1.Controls.Add(this.btnSuaa);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.dtpGioMoi_Sua);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.tbGhiChu_Suaa);
			this.groupBox1.Controls.Add(this.tbCa_Suaa);
			this.groupBox1.Controls.Add(this.tbGioCu_Suaa);
			this.groupBox1.Controls.Add(this.lbGioMoi);
			this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.groupBox1.Location = new System.Drawing.Point(252, 231);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox1.Size = new System.Drawing.Size(251, 251);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "SỬA GIỜ";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.label9.Location = new System.Drawing.Point(6, 27);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(43, 15);
			this.label9.TabIndex = 2;
			this.label9.Text = "Giờ cũ";
			this.toolTipHint.SetToolTip(this.label9, "Giờ cũ cần sửa");
			// 
			// btnChonCa_Suaa
			// 
			this.btnChonCa_Suaa.ForeColor = System.Drawing.Color.Blue;
			this.btnChonCa_Suaa.Location = new System.Drawing.Point(7, 53);
			this.btnChonCa_Suaa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnChonCa_Suaa.Name = "btnChonCa_Suaa";
			this.btnChonCa_Suaa.Size = new System.Drawing.Size(65, 25);
			this.btnChonCa_Suaa.TabIndex = 0;
			this.btnChonCa_Suaa.Text = "Chọn ca";
			this.toolTipHint.SetToolTip(this.btnChonCa_Suaa, "Mở khung danh sách\r\ncác ca làm việc để chọn");
			this.btnChonCa_Suaa.UseVisualStyleBackColor = true;
			this.btnChonCa_Suaa.Click += new System.EventHandler(this.btnChonCa_Suaa_Click);
			// 
			// cbLyDo_Suaa
			// 
			this.cbLyDo_Suaa.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.cbLyDo_Suaa.FormattingEnabled = true;
			this.cbLyDo_Suaa.Items.AddRange(new object[] {
            "Theo yêu cầu",
            "Lý do khác"});
			this.cbLyDo_Suaa.Location = new System.Drawing.Point(89, 113);
			this.cbLyDo_Suaa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cbLyDo_Suaa.Name = "cbLyDo_Suaa";
			this.cbLyDo_Suaa.Size = new System.Drawing.Size(155, 23);
			this.cbLyDo_Suaa.TabIndex = 2;
			this.cbLyDo_Suaa.Text = "Quên chấm công";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.label5.Location = new System.Drawing.Point(5, 146);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(49, 15);
			this.label5.TabIndex = 2;
			this.label5.Text = "Ghi chú";
			// 
			// dtpGioMoi_Sua
			// 
			this.dtpGioMoi_Sua.CustomFormat = "H:mm ddd d/M/yyyy";
			this.dtpGioMoi_Sua.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.dtpGioMoi_Sua.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpGioMoi_Sua.Location = new System.Drawing.Point(91, 85);
			this.dtpGioMoi_Sua.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dtpGioMoi_Sua.Name = "dtpGioMoi_Sua";
			this.dtpGioMoi_Sua.ShowUpDown = true;
			this.dtpGioMoi_Sua.Size = new System.Drawing.Size(155, 21);
			this.dtpGioMoi_Sua.TabIndex = 1;
			this.toolTipHint.SetToolTip(this.dtpGioMoi_Sua, "Giờ chấm công mới");
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.label6.Location = new System.Drawing.Point(6, 116);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(36, 15);
			this.label6.TabIndex = 2;
			this.label6.Text = "Lý do";
			// 
			// tbGhiChu_Suaa
			// 
			this.tbGhiChu_Suaa.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.tbGhiChu_Suaa.Location = new System.Drawing.Point(90, 143);
			this.tbGhiChu_Suaa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tbGhiChu_Suaa.Multiline = true;
			this.tbGhiChu_Suaa.Name = "tbGhiChu_Suaa";
			this.tbGhiChu_Suaa.Size = new System.Drawing.Size(154, 37);
			this.tbGhiChu_Suaa.TabIndex = 3;
			// 
			// tbCa_Suaa
			// 
			this.tbCa_Suaa.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.tbCa_Suaa.Location = new System.Drawing.Point(91, 55);
			this.tbCa_Suaa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tbCa_Suaa.Name = "tbCa_Suaa";
			this.tbCa_Suaa.ReadOnly = true;
			this.tbCa_Suaa.Size = new System.Drawing.Size(153, 21);
			this.tbCa_Suaa.TabIndex = 1;
			this.toolTipHint.SetToolTip(this.tbCa_Suaa, "Ca làm việc được chọn");
			// 
			// tbGioCu_Suaa
			// 
			this.tbGioCu_Suaa.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.tbGioCu_Suaa.Location = new System.Drawing.Point(91, 24);
			this.tbGioCu_Suaa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tbGioCu_Suaa.Name = "tbGioCu_Suaa";
			this.tbGioCu_Suaa.ReadOnly = true;
			this.tbGioCu_Suaa.Size = new System.Drawing.Size(153, 21);
			this.tbGioCu_Suaa.TabIndex = 1;
			this.tbGioCu_Suaa.Text = "Vào 23:55 12/12";
			this.toolTipHint.SetToolTip(this.tbGioCu_Suaa, "Giờ chấm công trước khi sửa");
			// 
			// lbGioMoi
			// 
			this.lbGioMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lbGioMoi.AutoSize = true;
			this.lbGioMoi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.lbGioMoi.Location = new System.Drawing.Point(4, 89);
			this.lbGioMoi.Name = "lbGioMoi";
			this.lbGioMoi.Size = new System.Drawing.Size(76, 15);
			this.lbGioMoi.TabIndex = 2;
			this.lbGioMoi.Text = "Giờ Vào mới";
			this.lbGioMoi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnChuyenDoi
			// 
			this.btnChuyenDoi.ForeColor = System.Drawing.Color.Blue;
			this.btnChuyenDoi.Location = new System.Drawing.Point(4, 53);
			this.btnChuyenDoi.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnChuyenDoi.Name = "btnChuyenDoi";
			this.btnChuyenDoi.Size = new System.Drawing.Size(195, 25);
			this.btnChuyenDoi.TabIndex = 0;
			this.btnChuyenDoi.Text = "Chuyển VÀO=>RA ; RA=>VÀO";
			this.btnChuyenDoi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTipHint.SetToolTip(this.btnChuyenDoi, resources.GetString("btnChuyenDoi.ToolTip"));
			this.btnChuyenDoi.UseVisualStyleBackColor = true;
			this.btnChuyenDoi.Click += new System.EventHandler(this.btnChuyenDoi_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.cbLyDo_Xoaa);
			this.groupBox2.Controls.Add(this.btnChuyenDoi);
			this.groupBox2.Controls.Add(this.btnThoat);
			this.groupBox2.Controls.Add(this.btnXoaa);
			this.groupBox2.Controls.Add(this.tbGhiChu_Xoaa);
			this.groupBox2.Controls.Add(this.tbGioCuu_ChuyenDoi);
			this.groupBox2.Controls.Add(this.tbGioCu_Xoaa);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.groupBox2.Location = new System.Drawing.Point(506, 231);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox2.Size = new System.Drawing.Size(208, 251);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "CHUYỂN ĐỔI HOẶC XOÁ GIỜ";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.label7.Location = new System.Drawing.Point(7, 87);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(43, 15);
			this.label7.TabIndex = 2;
			this.label7.Text = "Giờ cũ";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.label10.Location = new System.Drawing.Point(6, 27);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(43, 15);
			this.label10.TabIndex = 2;
			this.label10.Text = "Giờ cũ";
			// 
			// cbLyDo_Xoaa
			// 
			this.cbLyDo_Xoaa.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.cbLyDo_Xoaa.FormattingEnabled = true;
			this.cbLyDo_Xoaa.Items.AddRange(new object[] {
            "Check vân tay sai quy định",
            "Chấm nhầm máy",
            "Lý do khác"});
			this.cbLyDo_Xoaa.Location = new System.Drawing.Point(64, 111);
			this.cbLyDo_Xoaa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cbLyDo_Xoaa.Name = "cbLyDo_Xoaa";
			this.cbLyDo_Xoaa.Size = new System.Drawing.Size(135, 23);
			this.cbLyDo_Xoaa.TabIndex = 1;
			this.cbLyDo_Xoaa.Text = "Chấm nhầm máy";
			// 
			// btnThoat
			// 
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(64, 217);
			this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(135, 25);
			this.btnThoat.TabIndex = 3;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnXoaa
			// 
			this.btnXoaa.ForeColor = System.Drawing.Color.Blue;
			this.btnXoaa.Location = new System.Drawing.Point(64, 186);
			this.btnXoaa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnXoaa.Name = "btnXoaa";
			this.btnXoaa.Size = new System.Drawing.Size(135, 25);
			this.btnXoaa.TabIndex = 2;
			this.btnXoaa.Text = "Xoá";
			this.toolTipHint.SetToolTip(this.btnXoaa, "1. Quét chọn giờ chấm công\r\ncần XOÁ\r\n2. Nhập lý do, ghi chú\r\n3. Thực hiện xoá giờ" +
        " chấm công");
			this.btnXoaa.UseVisualStyleBackColor = true;
			this.btnXoaa.Click += new System.EventHandler(this.btnXoaa_Click);
			// 
			// tbGhiChu_Xoaa
			// 
			this.tbGhiChu_Xoaa.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.tbGhiChu_Xoaa.Location = new System.Drawing.Point(64, 141);
			this.tbGhiChu_Xoaa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tbGhiChu_Xoaa.Multiline = true;
			this.tbGhiChu_Xoaa.Name = "tbGhiChu_Xoaa";
			this.tbGhiChu_Xoaa.Size = new System.Drawing.Size(135, 37);
			this.tbGhiChu_Xoaa.TabIndex = 1;
			// 
			// tbGioCuu_ChuyenDoi
			// 
			this.tbGioCuu_ChuyenDoi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.tbGioCuu_ChuyenDoi.Location = new System.Drawing.Point(64, 24);
			this.tbGioCuu_ChuyenDoi.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tbGioCuu_ChuyenDoi.Name = "tbGioCuu_ChuyenDoi";
			this.tbGioCuu_ChuyenDoi.ReadOnly = true;
			this.tbGioCuu_ChuyenDoi.Size = new System.Drawing.Size(135, 21);
			this.tbGioCuu_ChuyenDoi.TabIndex = 1;
			this.tbGioCuu_ChuyenDoi.Text = "Vào 22:30 Hai 30/12";
			this.toolTipHint.SetToolTip(this.tbGioCuu_ChuyenDoi, "Giờ chấm công đang chọn");
			// 
			// tbGioCu_Xoaa
			// 
			this.tbGioCu_Xoaa.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.tbGioCu_Xoaa.Location = new System.Drawing.Point(64, 83);
			this.tbGioCu_Xoaa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tbGioCu_Xoaa.Name = "tbGioCu_Xoaa";
			this.tbGioCu_Xoaa.ReadOnly = true;
			this.tbGioCu_Xoaa.Size = new System.Drawing.Size(135, 21);
			this.tbGioCu_Xoaa.TabIndex = 1;
			this.tbGioCu_Xoaa.Text = "Vào 22:30 Hai 30/12";
			this.toolTipHint.SetToolTip(this.tbGioCu_Xoaa, "Giờ chấm công đang chọn");
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.label11.Location = new System.Drawing.Point(6, 146);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(49, 15);
			this.label11.TabIndex = 2;
			this.label11.Text = "Ghi chú";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.label12.Location = new System.Drawing.Point(17, 114);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(36, 15);
			this.label12.TabIndex = 2;
			this.label12.Text = "Lý do";
			// 
			// toolTipHint
			// 
			this.toolTipHint.AutoPopDelay = 30000;
			this.toolTipHint.InitialDelay = 300;
			this.toolTipHint.OwnerDraw = true;
			this.toolTipHint.ReshowDelay = 100;
			this.toolTipHint.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipHint_Draw);
			this.toolTipHint.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTipHint_Popup);
			// 
			// frm_XemCT_GioCC
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(726, 492);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tbMaCC);
			this.Controls.Add(this.dgrdGioKDQD);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gpNhapThongTin);
			this.Controls.Add(this.tbTenNV);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.Name = "frm_XemCT_GioCC";
			this.Text = "Thêm, sửa, xoá giờ đơn giản";
			this.Load += new System.EventHandler(this.frm_ThongTinThemGio_Load);
			this.gpNhapThongTin.ResumeLayout(false);
			this.gpNhapThongTin.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdGioKDQD)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSuaa;
		private System.Windows.Forms.DateTimePicker dtpVao_Them;
		private System.Windows.Forms.DateTimePicker dtpRaa_Them;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbTenNV;
		private System.Windows.Forms.CheckBox chkGioRaa;
		private System.Windows.Forms.CheckBox chkGioVao;
		private System.Windows.Forms.GroupBox gpNhapThongTin;
		private System.Windows.Forms.ComboBox cbLyDo_Them;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbGhichu_Them;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridView dgrdGioKDQD;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbMaCC;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cbLyDo_Suaa;
		private System.Windows.Forms.TextBox tbGhiChu_Suaa;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.DateTimePicker dtpGioMoi_Sua;
		private System.Windows.Forms.Button btnThem;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox tbGioCu_Suaa;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox cbLyDo_Xoaa;
		private System.Windows.Forms.Button btnXoaa;
		private System.Windows.Forms.TextBox tbGhiChu_Xoaa;
		private System.Windows.Forms.TextBox tbGioCu_Xoaa;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ToolTip toolTipHint;
		private System.Windows.Forms.Label lbGioMoi;
		private System.Windows.Forms.Button btnChuyenDoi;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c7;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c3;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c9;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c10;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
		private System.Windows.Forms.DataGridViewCheckBoxColumn g1c13;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.Button btnChonCa_Them;
		private System.Windows.Forms.TextBox tbCa_Them;
		private System.Windows.Forms.Button btnChonCa_Suaa;
		private System.Windows.Forms.TextBox tbCa_Suaa;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox tbGioCuu_ChuyenDoi;
	}
}