namespace ChamCong_v04.UI.XepLich {
	partial class fmExcludeNV {
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gp1 = new System.Windows.Forms.GroupBox();
			this.dgrdDSNVTrgPhg = new System.Windows.Forms.DataGridView();
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tbSearch = new System.Windows.Forms.TextBox();
			this.btnTim = new System.Windows.Forms.Button();
			this.linkHienThiTatCaNV = new System.Windows.Forms.LinkLabel();
			this.btnDongY = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.gp1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gp1
			// 
			this.gp1.Controls.Add(this.dgrdDSNVTrgPhg);
			this.gp1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.gp1.Location = new System.Drawing.Point(2, 54);
			this.gp1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gp1.Name = "gp1";
			this.gp1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gp1.Size = new System.Drawing.Size(385, 388);
			this.gp1.TabIndex = 1;
			this.gp1.TabStop = false;
			this.gp1.Text = "Danh sách Nhân viên";
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
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn9});
			this.dgrdDSNVTrgPhg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgrdDSNVTrgPhg.Location = new System.Drawing.Point(3, 16);
			this.dgrdDSNVTrgPhg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dgrdDSNVTrgPhg.Name = "dgrdDSNVTrgPhg";
			this.dgrdDSNVTrgPhg.RowHeadersVisible = false;
			this.dgrdDSNVTrgPhg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdDSNVTrgPhg.Size = new System.Drawing.Size(379, 370);
			this.dgrdDSNVTrgPhg.TabIndex = 0;
			// 
			// dataGridViewCheckBoxColumn1
			// 
			this.dataGridViewCheckBoxColumn1.DataPropertyName = "check";
			this.dataGridViewCheckBoxColumn1.FalseValue = "false";
			this.dataGridViewCheckBoxColumn1.Frozen = true;
			this.dataGridViewCheckBoxColumn1.HeaderText = "";
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewCheckBoxColumn1.TrueValue = "true";
			this.dataGridViewCheckBoxColumn1.Width = 22;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "UserFullName";
			this.dataGridViewTextBoxColumn1.HeaderText = "Tên NV";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.ToolTipText = "Tên Nhân viên";
			this.dataGridViewTextBoxColumn1.Width = 150;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "UserFullCode";
			this.dataGridViewTextBoxColumn2.HeaderText = "Mã NV";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.ToolTipText = "Mã Nhân viên";
			this.dataGridViewTextBoxColumn2.Width = 55;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "UserEnrollNumber";
			dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewTextBoxColumn3.HeaderText = "Mã CC_hide";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.ToolTipText = "Mã Chấm công";
			this.dataGridViewTextBoxColumn3.Visible = false;
			this.dataGridViewTextBoxColumn3.Width = 55;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.DataPropertyName = "TenPhong";
			this.dataGridViewTextBoxColumn7.HeaderText = "Phòng ban";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			this.dataGridViewTextBoxColumn7.ToolTipText = "Phòng ban";
			this.dataGridViewTextBoxColumn7.Width = 120;
			// 
			// dataGridViewTextBoxColumn9
			// 
			this.dataGridViewTextBoxColumn9.DataPropertyName = "MaPhong";
			this.dataGridViewTextBoxColumn9.HeaderText = "Mã Phòng ban";
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.ReadOnly = true;
			this.dataGridViewTextBoxColumn9.Visible = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tbSearch);
			this.panel1.Controls.Add(this.btnTim);
			this.panel1.Controls.Add(this.linkHienThiTatCaNV);
			this.panel1.Location = new System.Drawing.Point(2, 2);
			this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(385, 48);
			this.panel1.TabIndex = 2;
			// 
			// tbSearch
			// 
			this.tbSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.tbSearch.Location = new System.Drawing.Point(3, 2);
			this.tbSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tbSearch.Name = "tbSearch";
			this.tbSearch.Size = new System.Drawing.Size(206, 21);
			this.tbSearch.TabIndex = 0;
			this.tbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
			// 
			// btnTim
			// 
			this.btnTim.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnTim.Location = new System.Drawing.Point(215, 2);
			this.btnTim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnTim.Name = "btnTim";
			this.btnTim.Size = new System.Drawing.Size(40, 23);
			this.btnTim.TabIndex = 1;
			this.btnTim.Text = "Tìm";
			this.btnTim.UseVisualStyleBackColor = true;
			this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
			// 
			// linkHienThiTatCaNV
			// 
			this.linkHienThiTatCaNV.AutoSize = true;
			this.linkHienThiTatCaNV.Location = new System.Drawing.Point(3, 28);
			this.linkHienThiTatCaNV.Name = "linkHienThiTatCaNV";
			this.linkHienThiTatCaNV.Size = new System.Drawing.Size(206, 15);
			this.linkHienThiTatCaNV.TabIndex = 2;
			this.linkHienThiTatCaNV.TabStop = true;
			this.linkHienThiTatCaNV.Text = "Hiển thị tất cả nhân viên trong phòng";
			this.linkHienThiTatCaNV.Click += new System.EventHandler(this.linkHienThiTatCaNV_Click);
			// 
			// btnDongY
			// 
			this.btnDongY.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnDongY.ForeColor = System.Drawing.Color.Blue;
			this.btnDongY.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDongY.Location = new System.Drawing.Point(211, 445);
			this.btnDongY.Name = "btnDongY";
			this.btnDongY.Size = new System.Drawing.Size(85, 27);
			this.btnDongY.TabIndex = 47;
			this.btnDongY.Text = "Đồng ý";
			this.btnDongY.UseVisualStyleBackColor = true;
			this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnCancel.ForeColor = System.Drawing.Color.Blue;
			this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCancel.Location = new System.Drawing.Point(302, 445);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(85, 27);
			this.btnCancel.TabIndex = 48;
			this.btnCancel.Text = "Huỷ bỏ";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// fmExcludeNV
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(396, 479);
			this.Controls.Add(this.btnDongY);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.gp1);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "fmExcludeNV";
			this.Text = "Danh sách Nhân viên loại trừ";
			this.Load += new System.EventHandler(this.fmExcludeNV_Load);
			this.gp1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrdDSNVTrgPhg)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gp1;
		private System.Windows.Forms.DataGridView dgrdDSNVTrgPhg;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox tbSearch;
		private System.Windows.Forms.Button btnTim;
		private System.Windows.Forms.LinkLabel linkHienThiTatCaNV;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
		private System.Windows.Forms.Button btnDongY;
		private System.Windows.Forms.Button btnCancel;
	}
}