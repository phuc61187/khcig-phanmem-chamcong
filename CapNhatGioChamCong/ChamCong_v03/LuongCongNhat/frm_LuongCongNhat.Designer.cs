namespace ChamCong_v03.LuongCongNhat {
	partial class frm_LuongCongNhat {
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
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.btnThemMoi = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.btnXem = new System.Windows.Forms.Button();
			this.dgrdDSNVTrgPhg = new System.Windows.Forms.DataGridView();
			this.cUserFullCodeDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ID_hide = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cUserFullNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cTitleNameDSNV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnCapNhat = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.dialogOpenExcel = new System.Windows.Forms.OpenFileDialog();
			this.btnXoa = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).BeginInit();
			this.SuspendLayout();
			// 
			// dtpThang
			// 
			this.dtpThang.CustomFormat = "M / yyyy";
			this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpThang.Location = new System.Drawing.Point(275, 13);
			this.dtpThang.Name = "dtpThang";
			this.dtpThang.ShowUpDown = true;
			this.dtpThang.Size = new System.Drawing.Size(83, 21);
			this.dtpThang.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label3.Location = new System.Drawing.Point(10, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(259, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "Danh sách chi lương công nhật Tháng";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnThemMoi
			// 
			this.btnThemMoi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThemMoi.ForeColor = System.Drawing.Color.Blue;
			this.btnThemMoi.Location = new System.Drawing.Point(458, 13);
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
			this.btnThoat.Location = new System.Drawing.Point(742, 326);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(83, 25);
			this.btnThoat.TabIndex = 5;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnXem
			// 
			this.btnXem.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnXem.ForeColor = System.Drawing.Color.Blue;
			this.btnXem.Location = new System.Drawing.Point(369, 13);
			this.btnXem.Name = "btnXem";
			this.btnXem.Size = new System.Drawing.Size(83, 25);
			this.btnXem.TabIndex = 1;
			this.btnXem.Text = "Xem";
			this.btnXem.UseVisualStyleBackColor = true;
			this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
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
            this.cUserFullCodeDSNV2,
            this.ID_hide,
            this.cUserFullNameDSNV2,
            this.cTitleNameDSNV2,
            this.g1c9,
            this.g1c11,
            this.g1c15,
            this.g1c10,
            this.g1c16});
			this.dgrdDSNVTrgPhg.Location = new System.Drawing.Point(3, 44);
			this.dgrdDSNVTrgPhg.MultiSelect = false;
			this.dgrdDSNVTrgPhg.Name = "dgrdDSNVTrgPhg";
			this.dgrdDSNVTrgPhg.ReadOnly = true;
			this.dgrdDSNVTrgPhg.RowHeadersVisible = false;
			this.dgrdDSNVTrgPhg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdDSNVTrgPhg.Size = new System.Drawing.Size(827, 275);
			this.dgrdDSNVTrgPhg.TabIndex = 2;
			// 
			// cUserFullCodeDSNV2
			// 
			dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.cUserFullCodeDSNV2.DefaultCellStyle = dataGridViewCellStyle2;
			this.cUserFullCodeDSNV2.HeaderText = "STT_hide";
			this.cUserFullCodeDSNV2.Name = "cUserFullCodeDSNV2";
			this.cUserFullCodeDSNV2.ReadOnly = true;
			this.cUserFullCodeDSNV2.ToolTipText = "STT";
			this.cUserFullCodeDSNV2.Visible = false;
			this.cUserFullCodeDSNV2.Width = 40;
			// 
			// ID_hide
			// 
			this.ID_hide.DataPropertyName = "ID";
			this.ID_hide.HeaderText = "ID_hide";
			this.ID_hide.Name = "ID_hide";
			this.ID_hide.ReadOnly = true;
			this.ID_hide.Visible = false;
			// 
			// cUserFullNameDSNV2
			// 
			this.cUserFullNameDSNV2.DataPropertyName = "Ten";
			this.cUserFullNameDSNV2.HeaderText = "Họ và tên";
			this.cUserFullNameDSNV2.Name = "cUserFullNameDSNV2";
			this.cUserFullNameDSNV2.ReadOnly = true;
			this.cUserFullNameDSNV2.ToolTipText = "Họ và tên";
			this.cUserFullNameDSNV2.Width = 150;
			// 
			// cTitleNameDSNV2
			// 
			this.cTitleNameDSNV2.DataPropertyName = "ChucVu";
			this.cTitleNameDSNV2.HeaderText = "Chức vụ";
			this.cTitleNameDSNV2.Name = "cTitleNameDSNV2";
			this.cTitleNameDSNV2.ReadOnly = true;
			this.cTitleNameDSNV2.ToolTipText = "Chức vụ";
			this.cTitleNameDSNV2.Width = 150;
			// 
			// g1c9
			// 
			this.g1c9.DataPropertyName = "DonGiaLuong";
			dataGridViewCellStyle3.Format = "###,###,###,##0.000";
			this.g1c9.DefaultCellStyle = dataGridViewCellStyle3;
			this.g1c9.HeaderText = "Đơn giá lương";
			this.g1c9.Name = "g1c9";
			this.g1c9.ReadOnly = true;
			this.g1c9.ToolTipText = "Đơn giá lương";
			this.g1c9.Width = 110;
			// 
			// g1c11
			// 
			this.g1c11.DataPropertyName = "NgayCong";
			dataGridViewCellStyle4.Format = "#0.0#";
			this.g1c11.DefaultCellStyle = dataGridViewCellStyle4;
			this.g1c11.HeaderText = "Ngày công";
			this.g1c11.Name = "g1c11";
			this.g1c11.ReadOnly = true;
			this.g1c11.ToolTipText = "Số ngày công";
			this.g1c11.Width = 85;
			// 
			// g1c15
			// 
			this.g1c15.DataPropertyName = "ThanhTien";
			dataGridViewCellStyle5.Format = "###,###,###,##0.000";
			this.g1c15.DefaultCellStyle = dataGridViewCellStyle5;
			this.g1c15.HeaderText = "Thành tiền";
			this.g1c15.Name = "g1c15";
			this.g1c15.ReadOnly = true;
			this.g1c15.ToolTipText = "Thành tiền";
			// 
			// g1c10
			// 
			this.g1c10.DataPropertyName = "TamUng";
			dataGridViewCellStyle6.Format = "###,###,###,##0.000";
			this.g1c10.DefaultCellStyle = dataGridViewCellStyle6;
			this.g1c10.HeaderText = "Tạm ứng tháng";
			this.g1c10.Name = "g1c10";
			this.g1c10.ReadOnly = true;
			this.g1c10.ToolTipText = "Tạm ứng tháng";
			this.g1c10.Width = 120;
			// 
			// g1c16
			// 
			this.g1c16.DataPropertyName = "ThucLanh";
			dataGridViewCellStyle7.Format = "###,###,###,##0.000";
			this.g1c16.DefaultCellStyle = dataGridViewCellStyle7;
			this.g1c16.HeaderText = "Thực lãnh";
			this.g1c16.Name = "g1c16";
			this.g1c16.ReadOnly = true;
			// 
			// btnCapNhat
			// 
			this.btnCapNhat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnCapNhat.ForeColor = System.Drawing.Color.Blue;
			this.btnCapNhat.Location = new System.Drawing.Point(547, 13);
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
			// btnXoa
			// 
			this.btnXoa.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnXoa.ForeColor = System.Drawing.Color.Blue;
			this.btnXoa.Location = new System.Drawing.Point(636, 13);
			this.btnXoa.Name = "btnXoa";
			this.btnXoa.Size = new System.Drawing.Size(83, 25);
			this.btnXoa.TabIndex = 4;
			this.btnXoa.Text = "Xoá";
			this.btnXoa.UseVisualStyleBackColor = true;
			this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
			// 
			// frm_LuongCongNhat
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(837, 366);
			this.Controls.Add(this.dgrdDSNVTrgPhg);
			this.Controls.Add(this.btnXem);
			this.Controls.Add(this.dtpThang);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnXoa);
			this.Controls.Add(this.btnCapNhat);
			this.Controls.Add(this.btnThemMoi);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_LuongCongNhat";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Các khoản lương công nhật";
			this.Load += new System.EventHandler(this.frm_LuongCongNhat_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DateTimePicker dtpThang;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnThemMoi;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.DataGridView dgrdDSNVTrgPhg;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog dialogOpenExcel;
		private System.Windows.Forms.Button btnXem;
		private System.Windows.Forms.Button btnCapNhat;
		private System.Windows.Forms.Button btnXoa;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullCodeDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn ID_hide;
		private System.Windows.Forms.DataGridViewTextBoxColumn cUserFullNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn cTitleNameDSNV2;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c9;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c11;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c15;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c10;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c16;
	}
}