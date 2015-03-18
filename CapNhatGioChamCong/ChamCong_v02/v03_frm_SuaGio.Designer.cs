namespace ChamCong_v02 {
    partial class v03_frm_SuaGio {
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
			this.cbCa_Vao = new System.Windows.Forms.ComboBox();
			this.dtpGioMoi = new System.Windows.Forms.DateTimePicker();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.checkNgay_Vao = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.dtpNgay_Vao = new System.Windows.Forms.DateTimePicker();
			this.btnSuaGio = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.tbGhiChu = new System.Windows.Forms.TextBox();
			this.cbLyDo = new System.Windows.Forms.ComboBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.radRaa = new System.Windows.Forms.RadioButton();
			this.radVao = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.dgrdGioKDQD = new System.Windows.Forms.DataGridView();
			this.grid2check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.g1colmanv = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g3UserEnrollNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g3UserFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g3TimeDateNgayCong = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.textBoxColumnOfDataGrid6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.textBoxColumnOfDataGrid7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colobj2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colobj3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnDaoNguoc = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox3.SuspendLayout();
			this.groupBox6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdGioKDQD)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbCa_Vao
			// 
			this.cbCa_Vao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCa_Vao.Font = new System.Drawing.Font("Arial", 9.75F);
			this.cbCa_Vao.FormattingEnabled = true;
			this.cbCa_Vao.Location = new System.Drawing.Point(125, 52);
			this.cbCa_Vao.Name = "cbCa_Vao";
			this.cbCa_Vao.Size = new System.Drawing.Size(174, 24);
			this.cbCa_Vao.TabIndex = 0;
			this.cbCa_Vao.SelectionChangeCommitted += new System.EventHandler(this.cbCa_SelectionChangeCommitted);
			// 
			// dtpGioMoi
			// 
			this.dtpGioMoi.CustomFormat = "H:mm";
			this.dtpGioMoi.Font = new System.Drawing.Font("Arial", 9.75F);
			this.dtpGioMoi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpGioMoi.Location = new System.Drawing.Point(122, 31);
			this.dtpGioMoi.Name = "dtpGioMoi";
			this.dtpGioMoi.ShowUpDown = true;
			this.dtpGioMoi.Size = new System.Drawing.Size(174, 22);
			this.dtpGioMoi.TabIndex = 4;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.checkNgay_Vao);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.dtpGioMoi);
			this.groupBox3.Controls.Add(this.dtpNgay_Vao);
			this.groupBox3.Font = new System.Drawing.Font("Arial", 9.75F);
			this.groupBox3.Location = new System.Drawing.Point(3, 82);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(301, 90);
			this.groupBox3.TabIndex = 5;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Thời gian";
			// 
			// checkNgay_Vao
			// 
			this.checkNgay_Vao.AutoSize = true;
			this.checkNgay_Vao.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.checkNgay_Vao.ForeColor = System.Drawing.Color.DarkRed;
			this.checkNgay_Vao.Location = new System.Drawing.Point(6, 59);
			this.checkNgay_Vao.Name = "checkNgay_Vao";
			this.checkNgay_Vao.Size = new System.Drawing.Size(110, 20);
			this.checkNgay_Vao.TabIndex = 6;
			this.checkNgay_Vao.Text = "Ngày cố định";
			this.checkNgay_Vao.UseVisualStyleBackColor = true;
			this.checkNgay_Vao.CheckedChanged += new System.EventHandler(this.checkNgayVao_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(58, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Giờ mới";
			// 
			// dtpNgay_Vao
			// 
			this.dtpNgay_Vao.CustomFormat = "d/M/yyyy dddd";
			this.dtpNgay_Vao.Enabled = false;
			this.dtpNgay_Vao.Font = new System.Drawing.Font("Arial", 9.75F);
			this.dtpNgay_Vao.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgay_Vao.Location = new System.Drawing.Point(122, 59);
			this.dtpNgay_Vao.Name = "dtpNgay_Vao";
			this.dtpNgay_Vao.ShowUpDown = true;
			this.dtpNgay_Vao.Size = new System.Drawing.Size(174, 22);
			this.dtpNgay_Vao.TabIndex = 4;
			// 
			// btnSuaGio
			// 
			this.btnSuaGio.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.btnSuaGio.ForeColor = System.Drawing.Color.Blue;
			this.btnSuaGio.Location = new System.Drawing.Point(125, 304);
			this.btnSuaGio.Name = "btnSuaGio";
			this.btnSuaGio.Size = new System.Drawing.Size(101, 27);
			this.btnSuaGio.TabIndex = 6;
			this.btnSuaGio.Text = "Thực hiện";
			this.btnSuaGio.UseVisualStyleBackColor = true;
			this.btnSuaGio.Click += new System.EventHandler(this.btnSuaGio_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(701, 418);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(75, 27);
			this.btnThoat.TabIndex = 6;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// tbGhiChu
			// 
			this.tbGhiChu.Font = new System.Drawing.Font("Arial", 9.75F);
			this.tbGhiChu.Location = new System.Drawing.Point(125, 208);
			this.tbGhiChu.Multiline = true;
			this.tbGhiChu.Name = "tbGhiChu";
			this.tbGhiChu.Size = new System.Drawing.Size(174, 90);
			this.tbGhiChu.TabIndex = 0;
			// 
			// cbLyDo
			// 
			this.cbLyDo.Font = new System.Drawing.Font("Arial", 9.75F);
			this.cbLyDo.FormattingEnabled = true;
			this.cbLyDo.Items.AddRange(new object[] {
            "Lý do khác",
            "Thiếu giờ chấm công"});
			this.cbLyDo.Location = new System.Drawing.Point(125, 178);
			this.cbLyDo.Name = "cbLyDo";
			this.cbLyDo.Size = new System.Drawing.Size(174, 24);
			this.cbLyDo.TabIndex = 0;
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.radRaa);
			this.groupBox6.Controls.Add(this.radVao);
			this.groupBox6.Controls.Add(this.tbGhiChu);
			this.groupBox6.Controls.Add(this.cbLyDo);
			this.groupBox6.Controls.Add(this.btnSuaGio);
			this.groupBox6.Controls.Add(this.label5);
			this.groupBox6.Controls.Add(this.label2);
			this.groupBox6.Controls.Add(this.label4);
			this.groupBox6.Controls.Add(this.label3);
			this.groupBox6.Controls.Add(this.cbCa_Vao);
			this.groupBox6.Controls.Add(this.groupBox3);
			this.groupBox6.Location = new System.Drawing.Point(477, 8);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.groupBox6.Size = new System.Drawing.Size(317, 338);
			this.groupBox6.TabIndex = 8;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Thông tin giờ sửa mới";
			// 
			// radRaa
			// 
			this.radRaa.AutoSize = true;
			this.radRaa.Location = new System.Drawing.Point(207, 26);
			this.radRaa.Name = "radRaa";
			this.radRaa.Size = new System.Drawing.Size(42, 20);
			this.radRaa.TabIndex = 0;
			this.radRaa.Text = "Ra";
			this.radRaa.UseVisualStyleBackColor = true;
			// 
			// radVao
			// 
			this.radVao.AutoSize = true;
			this.radVao.Checked = true;
			this.radVao.Location = new System.Drawing.Point(125, 26);
			this.radVao.Name = "radVao";
			this.radVao.Size = new System.Drawing.Size(49, 20);
			this.radVao.TabIndex = 0;
			this.radVao.TabStop = true;
			this.radVao.Text = "Vào";
			this.radVao.UseVisualStyleBackColor = true;
			this.radVao.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.label5.Location = new System.Drawing.Point(22, 28);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(97, 16);
			this.label5.TabIndex = 6;
			this.label5.Text = "Chọn kiểu giờ";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(27, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Chọn theo ca";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.label4.Location = new System.Drawing.Point(62, 211);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(57, 16);
			this.label4.TabIndex = 5;
			this.label4.Text = "Ghi chú";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.label3.Location = new System.Drawing.Point(76, 181);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Lý do";
			// 
			// dgrdGioKDQD
			// 
			this.dgrdGioKDQD.AllowUserToAddRows = false;
			this.dgrdGioKDQD.AllowUserToDeleteRows = false;
			this.dgrdGioKDQD.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
			this.dgrdGioKDQD.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgrdGioKDQD.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgrdGioKDQD.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dgrdGioKDQD.ColumnHeadersHeight = 27;
			this.dgrdGioKDQD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.grid2check,
            this.g1colmanv,
            this.g3UserEnrollNumber,
            this.g3UserFullName,
            this.g3TimeDateNgayCong,
            this.textBoxColumnOfDataGrid6,
            this.textBoxColumnOfDataGrid7,
            this.colobj2,
            this.colobj3});
			this.dgrdGioKDQD.GridColor = System.Drawing.Color.Red;
			this.dgrdGioKDQD.Location = new System.Drawing.Point(8, 8);
			this.dgrdGioKDQD.MultiSelect = false;
			this.dgrdGioKDQD.Name = "dgrdGioKDQD";
			this.dgrdGioKDQD.RowHeadersVisible = false;
			this.dgrdGioKDQD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdGioKDQD.Size = new System.Drawing.Size(463, 437);
			this.dgrdGioKDQD.TabIndex = 9;
			// 
			// grid2check
			// 
			this.grid2check.DataPropertyName = "check";
			this.grid2check.FalseValue = "false";
			this.grid2check.HeaderText = "";
			this.grid2check.Name = "grid2check";
			this.grid2check.TrueValue = "true";
			this.grid2check.Width = 24;
			// 
			// g1colmanv
			// 
			this.g1colmanv.DataPropertyName = "UserFullCode";
			this.g1colmanv.HeaderText = "Mã NV";
			this.g1colmanv.Name = "g1colmanv";
			this.g1colmanv.ReadOnly = true;
			this.g1colmanv.Width = 55;
			// 
			// g3UserEnrollNumber
			// 
			this.g3UserEnrollNumber.DataPropertyName = "UserEnrollNumber";
			dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.g3UserEnrollNumber.DefaultCellStyle = dataGridViewCellStyle2;
			this.g3UserEnrollNumber.HeaderText = "Mã CC_hide";
			this.g3UserEnrollNumber.Name = "g3UserEnrollNumber";
			this.g3UserEnrollNumber.ReadOnly = true;
			this.g3UserEnrollNumber.ToolTipText = "Mã chấm công";
			this.g3UserEnrollNumber.Visible = false;
			this.g3UserEnrollNumber.Width = 55;
			// 
			// g3UserFullName
			// 
			this.g3UserFullName.DataPropertyName = "UserFullName";
			this.g3UserFullName.HeaderText = "Tên NV";
			this.g3UserFullName.Name = "g3UserFullName";
			this.g3UserFullName.ReadOnly = true;
			this.g3UserFullName.Width = 150;
			// 
			// g3TimeDateNgayCong
			// 
			this.g3TimeDateNgayCong.DataPropertyName = "TimeStrNgay";
			dataGridViewCellStyle3.Format = "d/M";
			this.g3TimeDateNgayCong.DefaultCellStyle = dataGridViewCellStyle3;
			this.g3TimeDateNgayCong.HeaderText = "Ngày";
			this.g3TimeDateNgayCong.Name = "g3TimeDateNgayCong";
			this.g3TimeDateNgayCong.ReadOnly = true;
			this.g3TimeDateNgayCong.Width = 45;
			// 
			// textBoxColumnOfDataGrid6
			// 
			this.textBoxColumnOfDataGrid6.DataPropertyName = "TimeStr";
			dataGridViewCellStyle4.Format = "H:mm d/M";
			this.textBoxColumnOfDataGrid6.DefaultCellStyle = dataGridViewCellStyle4;
			this.textBoxColumnOfDataGrid6.HeaderText = "Giờ";
			this.textBoxColumnOfDataGrid6.Name = "textBoxColumnOfDataGrid6";
			this.textBoxColumnOfDataGrid6.ReadOnly = true;
			this.textBoxColumnOfDataGrid6.Width = 120;
			// 
			// textBoxColumnOfDataGrid7
			// 
			this.textBoxColumnOfDataGrid7.DataPropertyName = "Kieu";
			dataGridViewCellStyle5.Format = "H:mm";
			this.textBoxColumnOfDataGrid7.DefaultCellStyle = dataGridViewCellStyle5;
			this.textBoxColumnOfDataGrid7.HeaderText = "Kiểu";
			this.textBoxColumnOfDataGrid7.Name = "textBoxColumnOfDataGrid7";
			this.textBoxColumnOfDataGrid7.ReadOnly = true;
			this.textBoxColumnOfDataGrid7.Width = 45;
			// 
			// colobj2
			// 
			this.colobj2.DataPropertyName = "objCIO";
			this.colobj2.HeaderText = "obj_hide";
			this.colobj2.Name = "colobj2";
			this.colobj2.ReadOnly = true;
			this.colobj2.Visible = false;
			// 
			// colobj3
			// 
			this.colobj3.DataPropertyName = "objCheck";
			this.colobj3.HeaderText = "obj_hide";
			this.colobj3.Name = "colobj3";
			this.colobj3.ReadOnly = true;
			this.colobj3.Visible = false;
			// 
			// btnDaoNguoc
			// 
			this.btnDaoNguoc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.btnDaoNguoc.ForeColor = System.Drawing.Color.Blue;
			this.btnDaoNguoc.Location = new System.Drawing.Point(6, 26);
			this.btnDaoNguoc.Name = "btnDaoNguoc";
			this.btnDaoNguoc.Size = new System.Drawing.Size(293, 27);
			this.btnDaoNguoc.TabIndex = 6;
			this.btnDaoNguoc.Text = "Đảo ngược kiểu giờ chấm công";
			this.btnDaoNguoc.UseVisualStyleBackColor = true;
			this.btnDaoNguoc.Click += new System.EventHandler(this.btnDaoNguoc_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnDaoNguoc);
			this.groupBox1.Location = new System.Drawing.Point(477, 352);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.groupBox1.Size = new System.Drawing.Size(317, 60);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Đảo ngược kiểu giờ chấm công vào ra";
			// 
			// v03_frm_SuaGio
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(803, 454);
			this.ControlBox = false;
			this.Controls.Add(this.dgrdGioKDQD);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.btnThoat);
			this.Font = new System.Drawing.Font("Arial", 9.75F);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "v03_frm_SuaGio";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Thông tin sửa giờ chấm công tự động";
			this.Load += new System.EventHandler(this.frm_SuaGio_Load);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdGioKDQD)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ComboBox cbCa_Vao;
		private System.Windows.Forms.DateTimePicker dtpGioMoi;
        private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker dtpNgay_Vao;
		private System.Windows.Forms.Button btnSuaGio;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.TextBox tbGhiChu;
		private System.Windows.Forms.ComboBox cbLyDo;
        private System.Windows.Forms.CheckBox checkNgay_Vao;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgrdGioKDQD;
        private System.Windows.Forms.Button btnDaoNguoc;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radRaa;
		private System.Windows.Forms.RadioButton radVao;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DataGridViewCheckBoxColumn grid2check;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1colmanv;
		private System.Windows.Forms.DataGridViewTextBoxColumn g3UserEnrollNumber;
		private System.Windows.Forms.DataGridViewTextBoxColumn g3UserFullName;
		private System.Windows.Forms.DataGridViewTextBoxColumn g3TimeDateNgayCong;
		private System.Windows.Forms.DataGridViewTextBoxColumn textBoxColumnOfDataGrid6;
		private System.Windows.Forms.DataGridViewTextBoxColumn textBoxColumnOfDataGrid7;
		private System.Windows.Forms.DataGridViewTextBoxColumn colobj2;
		private System.Windows.Forms.DataGridViewTextBoxColumn colobj3;
	}
}