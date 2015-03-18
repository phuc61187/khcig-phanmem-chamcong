namespace ChamCong_v03.DieuChinhLuong {
    partial class frm_42_DieuChinhLuongThangTruoc {
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
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.btnThemMoi = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.tbSearch = new System.Windows.Forms.TextBox();
			this.btnTim = new System.Windows.Forms.Button();
			this.btnXem = new System.Windows.Forms.Button();
			this.linkHienThiTatCaNV = new System.Windows.Forms.LinkLabel();
			this.btnDocExcel = new System.Windows.Forms.Button();
			this.dgrdDSNVTrgPhg = new System.Windows.Forms.DataGridView();
			this.cChkDSNV2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.cUserFullCodeDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cUserFullNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cUserEnrollNumberDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cTitleNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cSchIDDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cUserIDDDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cSchNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnCapNhat = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.dialogOpenExcel = new System.Windows.Forms.OpenFileDialog();
			this.gp1 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).BeginInit();
			this.gp1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dtpThang
			// 
			this.dtpThang.CustomFormat = "M / yyyy";
			this.dtpThang.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpThang.Location = new System.Drawing.Point(60, 22);
			this.dtpThang.Name = "dtpThang";
			this.dtpThang.ShowUpDown = true;
			this.dtpThang.Size = new System.Drawing.Size(83, 21);
			this.dtpThang.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label3.Location = new System.Drawing.Point(6, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "Tháng";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnThemMoi
			// 
			this.btnThemMoi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThemMoi.ForeColor = System.Drawing.Color.Blue;
			this.btnThemMoi.Location = new System.Drawing.Point(238, 20);
			this.btnThemMoi.Name = "btnThemMoi";
			this.btnThemMoi.Size = new System.Drawing.Size(83, 25);
			this.btnThemMoi.TabIndex = 2;
			this.btnThemMoi.Text = "Thêm mới";
			this.btnThemMoi.UseVisualStyleBackColor = true;
			this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(657, 377);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(83, 25);
			this.btnThoat.TabIndex = 0;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// tbSearch
			// 
			this.tbSearch.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.tbSearch.Location = new System.Drawing.Point(60, 51);
			this.tbSearch.Name = "tbSearch";
			this.tbSearch.Size = new System.Drawing.Size(261, 21);
			this.tbSearch.TabIndex = 1;
			// 
			// btnTim
			// 
			this.btnTim.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnTim.ForeColor = System.Drawing.Color.Blue;
			this.btnTim.Location = new System.Drawing.Point(327, 49);
			this.btnTim.Name = "btnTim";
			this.btnTim.Size = new System.Drawing.Size(83, 25);
			this.btnTim.TabIndex = 2;
			this.btnTim.Text = "Tìm";
			this.btnTim.UseVisualStyleBackColor = true;
			this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
			// 
			// btnXem
			// 
			this.btnXem.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnXem.ForeColor = System.Drawing.Color.Blue;
			this.btnXem.Location = new System.Drawing.Point(149, 20);
			this.btnXem.Name = "btnXem";
			this.btnXem.Size = new System.Drawing.Size(83, 25);
			this.btnXem.TabIndex = 1;
			this.btnXem.Text = "Xem";
			this.btnXem.UseVisualStyleBackColor = true;
			this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
			// 
			// linkHienThiTatCaNV
			// 
			this.linkHienThiTatCaNV.AutoSize = true;
			this.linkHienThiTatCaNV.Location = new System.Drawing.Point(422, 53);
			this.linkHienThiTatCaNV.Name = "linkHienThiTatCaNV";
			this.linkHienThiTatCaNV.Size = new System.Drawing.Size(81, 15);
			this.linkHienThiTatCaNV.TabIndex = 35;
			this.linkHienThiTatCaNV.TabStop = true;
			this.linkHienThiTatCaNV.Text = "Hiển thị tất cả";
			this.linkHienThiTatCaNV.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHienThiTatCaNV_LinkClicked);
			// 
			// btnDocExcel
			// 
			this.btnDocExcel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnDocExcel.ForeColor = System.Drawing.Color.Blue;
			this.btnDocExcel.Location = new System.Drawing.Point(416, 20);
			this.btnDocExcel.Name = "btnDocExcel";
			this.btnDocExcel.Size = new System.Drawing.Size(140, 25);
			this.btnDocExcel.TabIndex = 4;
			this.btnDocExcel.Text = "Đọc từ file excel";
			this.btnDocExcel.UseVisualStyleBackColor = true;
			this.btnDocExcel.Click += new System.EventHandler(this.btnDocTuExcel_Click);
			// 
			// dgrdDSNVTrgPhg
			// 
			this.dgrdDSNVTrgPhg.AllowUserToAddRows = false;
			this.dgrdDSNVTrgPhg.AllowUserToDeleteRows = false;
			this.dgrdDSNVTrgPhg.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.dgrdDSNVTrgPhg.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgrdDSNVTrgPhg.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgrdDSNVTrgPhg.ColumnHeadersHeight = 25;
			this.dgrdDSNVTrgPhg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgrdDSNVTrgPhg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cChkDSNV2,
            this.cUserFullCodeDSNV2,
            this.cUserFullNameDSNV2,
            this.g1c10,
            this.g1c9,
            this.g1c11,
            this.g1c21,
            this.cUserEnrollNumberDSNV2,
            this.cTitleNameDSNV2,
            this.cSchIDDSNV2,
            this.cUserIDDDSNV2,
            this.cSchNameDSNV2});
			this.dgrdDSNVTrgPhg.Location = new System.Drawing.Point(6, 83);
			this.dgrdDSNVTrgPhg.Name = "dgrdDSNVTrgPhg";
			this.dgrdDSNVTrgPhg.ReadOnly = true;
			this.dgrdDSNVTrgPhg.RowHeadersVisible = false;
			this.dgrdDSNVTrgPhg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdDSNVTrgPhg.Size = new System.Drawing.Size(722, 275);
			this.dgrdDSNVTrgPhg.TabIndex = 2;
			// 
			// cChkDSNV2
			// 
			this.cChkDSNV2.DataPropertyName = "check";
			this.cChkDSNV2.FalseValue = "false";
			this.cChkDSNV2.Frozen = true;
			this.cChkDSNV2.HeaderText = "";
			this.cChkDSNV2.Name = "cChkDSNV2";
			this.cChkDSNV2.ReadOnly = true;
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
			this.cUserFullCodeDSNV2.Width = 55;
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
			// g1c10
			// 
			this.g1c10.DataPropertyName = "TamUng";
			dataGridViewCellStyle3.Format = "###,###,###,##0.000";
			this.g1c10.DefaultCellStyle = dataGridViewCellStyle3;
			this.g1c10.HeaderText = "Tạm ứng tháng";
			this.g1c10.Name = "g1c10";
			this.g1c10.ReadOnly = true;
			this.g1c10.Width = 120;
			// 
			// g1c9
			// 
			this.g1c9.DataPropertyName = "LuongDieuChinh";
			dataGridViewCellStyle4.Format = "###,###,###,##0.000";
			this.g1c9.DefaultCellStyle = dataGridViewCellStyle4;
			this.g1c9.HeaderText = "Lương điều chỉnh";
			this.g1c9.Name = "g1c9";
			this.g1c9.ReadOnly = true;
			this.g1c9.Width = 120;
			// 
			// g1c11
			// 
			this.g1c11.DataPropertyName = "ThuChiKhac";
			dataGridViewCellStyle5.Format = "###,###,###,##0.000";
			this.g1c11.DefaultCellStyle = dataGridViewCellStyle5;
			this.g1c11.HeaderText = "Thu chi khác";
			this.g1c11.Name = "g1c11";
			this.g1c11.ReadOnly = true;
			this.g1c11.Width = 120;
			// 
			// g1c21
			// 
			this.g1c21.DataPropertyName = "MucDongBHXH";
			this.g1c21.HeaderText = "Mức đóng BHXH";
			this.g1c21.Name = "g1c21";
			this.g1c21.ReadOnly = true;
			this.g1c21.Width = 120;
			// 
			// cUserEnrollNumberDSNV2
			// 
			this.cUserEnrollNumberDSNV2.DataPropertyName = "UserEnrollNumber";
			dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.cUserEnrollNumberDSNV2.DefaultCellStyle = dataGridViewCellStyle6;
			this.cUserEnrollNumberDSNV2.HeaderText = "Mã CC_hide";
			this.cUserEnrollNumberDSNV2.Name = "cUserEnrollNumberDSNV2";
			this.cUserEnrollNumberDSNV2.ReadOnly = true;
			this.cUserEnrollNumberDSNV2.ToolTipText = "Mã Chấm công";
			this.cUserEnrollNumberDSNV2.Visible = false;
			this.cUserEnrollNumberDSNV2.Width = 55;
			// 
			// cTitleNameDSNV2
			// 
			this.cTitleNameDSNV2.DataPropertyName = "TitleName";
			this.cTitleNameDSNV2.HeaderText = "Chức vụ_hide";
			this.cTitleNameDSNV2.Name = "cTitleNameDSNV2";
			this.cTitleNameDSNV2.ReadOnly = true;
			this.cTitleNameDSNV2.ToolTipText = "Chức vụ";
			this.cTitleNameDSNV2.Visible = false;
			this.cTitleNameDSNV2.Width = 200;
			// 
			// cSchIDDSNV2
			// 
			this.cSchIDDSNV2.DataPropertyName = "SchID";
			this.cSchIDDSNV2.HeaderText = "Mã lịch trình_hide";
			this.cSchIDDSNV2.Name = "cSchIDDSNV2";
			this.cSchIDDSNV2.ReadOnly = true;
			this.cSchIDDSNV2.ToolTipText = "Mã lịch trình";
			this.cSchIDDSNV2.Visible = false;
			// 
			// cUserIDDDSNV2
			// 
			this.cUserIDDDSNV2.DataPropertyName = "IDD_1";
			this.cUserIDDDSNV2.HeaderText = "Mã Phòng ban_hide";
			this.cUserIDDDSNV2.Name = "cUserIDDDSNV2";
			this.cUserIDDDSNV2.ReadOnly = true;
			this.cUserIDDDSNV2.Visible = false;
			// 
			// cSchNameDSNV2
			// 
			this.cSchNameDSNV2.DataPropertyName = "SchName";
			this.cSchNameDSNV2.HeaderText = "Lịch trình_hide";
			this.cSchNameDSNV2.Name = "cSchNameDSNV2";
			this.cSchNameDSNV2.ReadOnly = true;
			this.cSchNameDSNV2.ToolTipText = "Lịch trình";
			this.cSchNameDSNV2.Visible = false;
			this.cSchNameDSNV2.Width = 95;
			// 
			// btnCapNhat
			// 
			this.btnCapNhat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnCapNhat.ForeColor = System.Drawing.Color.Blue;
			this.btnCapNhat.Location = new System.Drawing.Point(327, 20);
			this.btnCapNhat.Name = "btnCapNhat";
			this.btnCapNhat.Size = new System.Drawing.Size(83, 25);
			this.btnCapNhat.TabIndex = 3;
			this.btnCapNhat.Text = "Cập nhật";
			this.btnCapNhat.UseVisualStyleBackColor = true;
			this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
			// 
			// dialogOpenExcel
			// 
			this.dialogOpenExcel.FileName = "openFileDialog1";
			// 
			// gp1
			// 
			this.gp1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.gp1.Controls.Add(this.tbSearch);
			this.gp1.Controls.Add(this.btnXem);
			this.gp1.Controls.Add(this.label3);
			this.gp1.Controls.Add(this.btnTim);
			this.gp1.Controls.Add(this.btnThemMoi);
			this.gp1.Controls.Add(this.linkHienThiTatCaNV);
			this.gp1.Controls.Add(this.btnDocExcel);
			this.gp1.Controls.Add(this.btnCapNhat);
			this.gp1.Controls.Add(this.dgrdDSNVTrgPhg);
			this.gp1.Controls.Add(this.dtpThang);
			this.gp1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.gp1.Location = new System.Drawing.Point(12, 11);
			this.gp1.Name = "gp1";
			this.gp1.Size = new System.Drawing.Size(734, 360);
			this.gp1.TabIndex = 1;
			this.gp1.TabStop = false;
			this.gp1.Text = "Danh sách chi các khoản tạm ứng, lương điều chỉnh, thu chi khác cho NV";
			// 
			// frm_42_DieuChinhLuongThangTruoc
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(758, 406);
			this.Controls.Add(this.gp1);
			this.Controls.Add(this.btnThoat);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_42_DieuChinhLuongThangTruoc";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Chi các khoản tạm ứng, lương điều chỉnh, thu chi khác";
			this.Load += new System.EventHandler(this.frm_DieuChinhLuongThangTruoc_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).EndInit();
			this.gp1.ResumeLayout(false);
			this.gp1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DateTimePicker dtpThang;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnThemMoi;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.DataGridView dgrdDSNVTrgPhg;
        private System.Windows.Forms.TextBox tbSearch;
		private System.Windows.Forms.Button btnTim;
		private System.Windows.Forms.Button btnDocExcel;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog dialogOpenExcel;
		private System.Windows.Forms.Button btnXem;
		private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.LinkLabel linkHienThiTatCaNV;
        private System.Windows.Forms.GroupBox gp1;
		private System.Windows.Forms.DataGridViewCheckBoxColumn cChkDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullCodeDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c10;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c9;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c11;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c21;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserEnrollNumberDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cTitleNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cSchIDDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserIDDDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cSchNameDSNV2;
	}
}