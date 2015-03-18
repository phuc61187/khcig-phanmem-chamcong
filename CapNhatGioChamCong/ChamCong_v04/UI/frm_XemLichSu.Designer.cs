namespace ChamCong_v04.UI {
    partial class frm_32_XemLichSu {
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dgrdLichSu = new System.Windows.Forms.DataGridView();
			this.g1tenthaotac = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colExecuteTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colUserAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colUserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tbSearch = new System.Windows.Forms.TextBox();
			this.btnTim = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.linkHienThiTatCaNV = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.cbLoaiThaoTac = new System.Windows.Forms.ComboBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.radThucHienBoiTatCaTaiKhoan = new System.Windows.Forms.RadioButton();
			this.radThucHienBoi1TaiKhoan = new System.Windows.Forms.RadioButton();
			this.cbTaiKhoanThucHien = new System.Windows.Forms.ComboBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.checkThoiDiemSua = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.dtpStartThoiDiemSua = new System.Windows.Forms.DateTimePicker();
			this.dtpEndddThoiDiemSua = new System.Windows.Forms.DateTimePicker();
			this.label7 = new System.Windows.Forms.Label();
			this.btnThoat = new System.Windows.Forms.Button();
			this.btnXem = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgrdLichSu)).BeginInit();
			this.panel2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgrdLichSu
			// 
			this.dgrdLichSu.AllowUserToAddRows = false;
			this.dgrdLichSu.AllowUserToDeleteRows = false;
			this.dgrdLichSu.AllowUserToOrderColumns = true;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.dgrdLichSu.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgrdLichSu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdLichSu.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgrdLichSu.ColumnHeadersHeight = 27;
			this.dgrdLichSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgrdLichSu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.g1tenthaotac,
            this.g1c6,
            this.g1c5,
            this.colExecuteTime,
            this.g1c7,
            this.colUserAccount,
            this.colUserID});
			this.dgrdLichSu.Location = new System.Drawing.Point(239, 3);
			this.dgrdLichSu.Name = "dgrdLichSu";
			this.dgrdLichSu.ReadOnly = true;
			this.dgrdLichSu.RowHeadersVisible = false;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgrdLichSu.RowsDefaultCellStyle = dataGridViewCellStyle3;
			this.dgrdLichSu.RowTemplate.Height = 50;
			this.dgrdLichSu.RowTemplate.ReadOnly = true;
			this.dgrdLichSu.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dgrdLichSu.Size = new System.Drawing.Size(850, 529);
			this.dgrdLichSu.TabIndex = 0;
			// 
			// g1tenthaotac
			// 
			this.g1tenthaotac.DataPropertyName = "Loai";
			this.g1tenthaotac.HeaderText = "Thao tác";
			this.g1tenthaotac.Name = "g1tenthaotac";
			this.g1tenthaotac.ReadOnly = true;
			this.g1tenthaotac.ToolTipText = "Loại thao tác";
			this.g1tenthaotac.Width = 150;
			// 
			// g1c6
			// 
			this.g1c6.DataPropertyName = "UserFullName";
			this.g1c6.HeaderText = "Thực hiện cho NV";
			this.g1c6.Name = "g1c6";
			this.g1c6.ReadOnly = true;
			this.g1c6.ToolTipText = "Thực hiện cho NV nào";
			this.g1c6.Width = 150;
			// 
			// g1c5
			// 
			this.g1c5.DataPropertyName = "NoiDung";
			this.g1c5.HeaderText = "Chi tiết thao tác";
			this.g1c5.Name = "g1c5";
			this.g1c5.ReadOnly = true;
			this.g1c5.Width = 450;
			// 
			// colExecuteTime
			// 
			this.colExecuteTime.DataPropertyName = "ThoiDiem";
			dataGridViewCellStyle2.Format = "H:mm d/M";
			this.colExecuteTime.DefaultCellStyle = dataGridViewCellStyle2;
			this.colExecuteTime.HeaderText = "Thời điểm";
			this.colExecuteTime.Name = "colExecuteTime";
			this.colExecuteTime.ReadOnly = true;
			this.colExecuteTime.ToolTipText = "Thời điểm thực hiện thao tác";
			this.colExecuteTime.Width = 80;
			// 
			// g1c7
			// 
			this.g1c7.DataPropertyName = "TenPhong";
			this.g1c7.HeaderText = "Thực hiện cho phòng";
			this.g1c7.Name = "g1c7";
			this.g1c7.ReadOnly = true;
			this.g1c7.ToolTipText = "Thực hiện cho bộ phận";
			this.g1c7.Width = 120;
			// 
			// colUserAccount
			// 
			this.colUserAccount.DataPropertyName = "UserAccount";
			this.colUserAccount.HeaderText = "Tài khoản";
			this.colUserAccount.Name = "colUserAccount";
			this.colUserAccount.ReadOnly = true;
			this.colUserAccount.ToolTipText = "Tài khoản thực hiện thao tác";
			this.colUserAccount.Width = 80;
			// 
			// colUserID
			// 
			this.colUserID.DataPropertyName = "UserID";
			this.colUserID.HeaderText = "UserID_hide";
			this.colUserID.Name = "colUserID";
			this.colUserID.ReadOnly = true;
			this.colUserID.Visible = false;
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1092, 0);
			this.panel1.TabIndex = 1;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.groupBox1);
			this.panel2.Controls.Add(this.groupBox4);
			this.panel2.Controls.Add(this.groupBox3);
			this.panel2.Controls.Add(this.btnThoat);
			this.panel2.Controls.Add(this.btnXem);
			this.panel2.Controls.Add(this.dgrdLichSu);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(1092, 534);
			this.panel2.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.tbSearch);
			this.groupBox1.Controls.Add(this.btnTim);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.linkHienThiTatCaNV);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.cbLoaiThaoTac);
			this.groupBox1.Location = new System.Drawing.Point(3, 231);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(230, 149);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Lọc và tìm kiếm";
			// 
			// tbSearch
			// 
			this.tbSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.tbSearch.Location = new System.Drawing.Point(6, 97);
			this.tbSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tbSearch.Name = "tbSearch";
			this.tbSearch.Size = new System.Drawing.Size(172, 21);
			this.tbSearch.TabIndex = 3;
			this.tbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
			// 
			// btnTim
			// 
			this.btnTim.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnTim.Location = new System.Drawing.Point(184, 97);
			this.btnTim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnTim.Name = "btnTim";
			this.btnTim.Size = new System.Drawing.Size(40, 23);
			this.btnTim.TabIndex = 4;
			this.btnTim.Text = "Tìm";
			this.btnTim.UseVisualStyleBackColor = true;
			this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label2.Location = new System.Drawing.Point(9, 79);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(103, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "Tìm theo từ khóa";
			// 
			// linkHienThiTatCaNV
			// 
			this.linkHienThiTatCaNV.AutoSize = true;
			this.linkHienThiTatCaNV.Location = new System.Drawing.Point(6, 123);
			this.linkHienThiTatCaNV.Name = "linkHienThiTatCaNV";
			this.linkHienThiTatCaNV.Size = new System.Drawing.Size(81, 15);
			this.linkHienThiTatCaNV.TabIndex = 5;
			this.linkHienThiTatCaNV.TabStop = true;
			this.linkHienThiTatCaNV.Text = "Hiển thị tất cả";
			this.linkHienThiTatCaNV.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHienThiTatCaNV_LinkClicked);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label1.Location = new System.Drawing.Point(9, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Loại thao tác";
			// 
			// cbLoaiThaoTac
			// 
			this.cbLoaiThaoTac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbLoaiThaoTac.FormattingEnabled = true;
			this.cbLoaiThaoTac.Location = new System.Drawing.Point(6, 42);
			this.cbLoaiThaoTac.Name = "cbLoaiThaoTac";
			this.cbLoaiThaoTac.Size = new System.Drawing.Size(215, 23);
			this.cbLoaiThaoTac.TabIndex = 1;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.radThucHienBoiTatCaTaiKhoan);
			this.groupBox4.Controls.Add(this.radThucHienBoi1TaiKhoan);
			this.groupBox4.Controls.Add(this.cbTaiKhoanThucHien);
			this.groupBox4.Location = new System.Drawing.Point(3, 95);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(230, 97);
			this.groupBox4.TabIndex = 16;
			this.groupBox4.TabStop = false;
			// 
			// radThucHienBoiTatCaTaiKhoan
			// 
			this.radThucHienBoiTatCaTaiKhoan.AutoSize = true;
			this.radThucHienBoiTatCaTaiKhoan.Checked = true;
			this.radThucHienBoiTatCaTaiKhoan.Location = new System.Drawing.Point(8, 17);
			this.radThucHienBoiTatCaTaiKhoan.Name = "radThucHienBoiTatCaTaiKhoan";
			this.radThucHienBoiTatCaTaiKhoan.Size = new System.Drawing.Size(208, 19);
			this.radThucHienBoiTatCaTaiKhoan.TabIndex = 2;
			this.radThucHienBoiTatCaTaiKhoan.TabStop = true;
			this.radThucHienBoiTatCaTaiKhoan.Text = "Thực hiện bởi tất cả các tài khoản";
			this.radThucHienBoiTatCaTaiKhoan.UseVisualStyleBackColor = true;
			// 
			// radThucHienBoi1TaiKhoan
			// 
			this.radThucHienBoi1TaiKhoan.AutoSize = true;
			this.radThucHienBoi1TaiKhoan.Location = new System.Drawing.Point(8, 42);
			this.radThucHienBoi1TaiKhoan.Name = "radThucHienBoi1TaiKhoan";
			this.radThucHienBoi1TaiKhoan.Size = new System.Drawing.Size(154, 19);
			this.radThucHienBoi1TaiKhoan.TabIndex = 2;
			this.radThucHienBoi1TaiKhoan.Text = "Thực hiện bởi tài khoản";
			this.radThucHienBoi1TaiKhoan.UseVisualStyleBackColor = true;
			// 
			// cbTaiKhoanThucHien
			// 
			this.cbTaiKhoanThucHien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTaiKhoanThucHien.FormattingEnabled = true;
			this.cbTaiKhoanThucHien.Location = new System.Drawing.Point(6, 67);
			this.cbTaiKhoanThucHien.Name = "cbTaiKhoanThucHien";
			this.cbTaiKhoanThucHien.Size = new System.Drawing.Size(215, 23);
			this.cbTaiKhoanThucHien.TabIndex = 1;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.checkThoiDiemSua);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.dtpStartThoiDiemSua);
			this.groupBox3.Controls.Add(this.dtpEndddThoiDiemSua);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Location = new System.Drawing.Point(3, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(230, 86);
			this.groupBox3.TabIndex = 16;
			this.groupBox3.TabStop = false;
			// 
			// checkThoiDiemSua
			// 
			this.checkThoiDiemSua.AutoSize = true;
			this.checkThoiDiemSua.Location = new System.Drawing.Point(6, 8);
			this.checkThoiDiemSua.Name = "checkThoiDiemSua";
			this.checkThoiDiemSua.Size = new System.Drawing.Size(107, 19);
			this.checkThoiDiemSua.TabIndex = 0;
			this.checkThoiDiemSua.Text = "Thời điểm sửa";
			this.checkThoiDiemSua.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(21, 38);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(22, 15);
			this.label6.TabIndex = 1;
			this.label6.Text = "Từ";
			// 
			// dtpStartThoiDiemSua
			// 
			this.dtpStartThoiDiemSua.CustomFormat = "H:mm ddd d/M/yyyy";
			this.dtpStartThoiDiemSua.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.dtpStartThoiDiemSua.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpStartThoiDiemSua.Location = new System.Drawing.Point(52, 34);
			this.dtpStartThoiDiemSua.Margin = new System.Windows.Forms.Padding(4);
			this.dtpStartThoiDiemSua.Name = "dtpStartThoiDiemSua";
			this.dtpStartThoiDiemSua.Size = new System.Drawing.Size(170, 21);
			this.dtpStartThoiDiemSua.TabIndex = 1;
			// 
			// dtpEndddThoiDiemSua
			// 
			this.dtpEndddThoiDiemSua.CustomFormat = "H:mm ddd d/M/yyyy";
			this.dtpEndddThoiDiemSua.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.dtpEndddThoiDiemSua.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpEndddThoiDiemSua.Location = new System.Drawing.Point(52, 62);
			this.dtpEndddThoiDiemSua.Margin = new System.Windows.Forms.Padding(4);
			this.dtpEndddThoiDiemSua.Name = "dtpEndddThoiDiemSua";
			this.dtpEndddThoiDiemSua.Size = new System.Drawing.Size(170, 21);
			this.dtpEndddThoiDiemSua.TabIndex = 2;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(21, 67);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(30, 15);
			this.label7.TabIndex = 1;
			this.label7.Text = "Đến";
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(158, 496);
			this.btnThoat.Margin = new System.Windows.Forms.Padding(4);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(74, 25);
			this.btnThoat.TabIndex = 1;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnXem
			// 
			this.btnXem.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnXem.ForeColor = System.Drawing.Color.Blue;
			this.btnXem.Location = new System.Drawing.Point(27, 199);
			this.btnXem.Margin = new System.Windows.Forms.Padding(4);
			this.btnXem.Name = "btnXem";
			this.btnXem.Size = new System.Drawing.Size(74, 25);
			this.btnXem.TabIndex = 0;
			this.btnXem.Text = "Xem";
			this.btnXem.UseVisualStyleBackColor = true;
			this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
			// 
			// frm_32_XemLichSu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1092, 534);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_32_XemLichSu";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Xem lịch sử chỉnh sửa giờ chấm công";
			this.Load += new System.EventHandler(this.frm_32_XemLichSu_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdLichSu)).EndInit();
			this.panel2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgrdLichSu;
        private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ComboBox cbTaiKhoanThucHien;
		private System.Windows.Forms.Button btnXem;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox checkThoiDiemSua;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.DateTimePicker dtpStartThoiDiemSua;
		private System.Windows.Forms.DateTimePicker dtpEndddThoiDiemSua;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.TextBox tbSearch;
		private System.Windows.Forms.Button btnTim;
		private System.Windows.Forms.LinkLabel linkHienThiTatCaNV;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cbLoaiThaoTac;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radThucHienBoiTatCaTaiKhoan;
		private System.Windows.Forms.RadioButton radThucHienBoi1TaiKhoan;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1tenthaotac;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c6;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c5;
		private System.Windows.Forms.DataGridViewTextBoxColumn colExecuteTime;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c7;
		private System.Windows.Forms.DataGridViewTextBoxColumn colUserAccount;
		private System.Windows.Forms.DataGridViewTextBoxColumn colUserID;


    }
}