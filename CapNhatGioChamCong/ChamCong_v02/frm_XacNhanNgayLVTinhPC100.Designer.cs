namespace ChamCong_v02 {
	partial class frm_XacNhanNgayLVTinhPC100 {
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
			this.dgrdNgayNghi = new System.Windows.Forms.DataGridView();
			this.btnDuyet = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnThoat = new System.Windows.Forms.Button();
			this.btnXoa = new System.Windows.Forms.Button();
			this.g1check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.g2macc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g3colManv = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1tennv = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1ngay = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1th = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colPhucap = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colphantram = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgrdNgayNghi)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgrdNgayNghi
			// 
			this.dgrdNgayNghi.AllowUserToAddRows = false;
			this.dgrdNgayNghi.AllowUserToDeleteRows = false;
			this.dgrdNgayNghi.ColumnHeadersHeight = 27;
			this.dgrdNgayNghi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgrdNgayNghi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.g1check,
            this.g2macc,
            this.g3colManv,
            this.g1tennv,
            this.g1ngay,
            this.g1th,
            this.colPhucap,
            this.colphantram,
            this.g1ID});
			this.dgrdNgayNghi.Location = new System.Drawing.Point(6, 26);
			this.dgrdNgayNghi.Name = "dgrdNgayNghi";
			this.dgrdNgayNghi.RowHeadersVisible = false;
			this.dgrdNgayNghi.Size = new System.Drawing.Size(398, 367);
			this.dgrdNgayNghi.TabIndex = 9;
			// 
			// btnDuyet
			// 
			this.btnDuyet.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnDuyet.ForeColor = System.Drawing.Color.Blue;
			this.btnDuyet.Location = new System.Drawing.Point(18, 417);
			this.btnDuyet.Name = "btnDuyet";
			this.btnDuyet.Size = new System.Drawing.Size(75, 27);
			this.btnDuyet.TabIndex = 10;
			this.btnDuyet.Text = "Duyệt";
			this.btnDuyet.UseVisualStyleBackColor = true;
			this.btnDuyet.Click += new System.EventHandler(this.btnDuyet_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.dgrdNgayNghi);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.groupBox1.Size = new System.Drawing.Size(412, 399);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Danh sách ngày làm việc tính Phụ cấp 100% chờ duyệt";
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(341, 417);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(75, 27);
			this.btnThoat.TabIndex = 10;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnXoa
			// 
			this.btnXoa.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnXoa.ForeColor = System.Drawing.Color.Blue;
			this.btnXoa.Location = new System.Drawing.Point(99, 417);
			this.btnXoa.Name = "btnXoa";
			this.btnXoa.Size = new System.Drawing.Size(75, 27);
			this.btnXoa.TabIndex = 10;
			this.btnXoa.Text = "Xoá";
			this.btnXoa.UseVisualStyleBackColor = true;
			this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
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
			dataGridViewCellStyle1.Format = "d/M/yyyy";
			this.g1ngay.DefaultCellStyle = dataGridViewCellStyle1;
			this.g1ngay.HeaderText = "Ngày";
			this.g1ngay.Name = "g1ngay";
			this.g1ngay.ReadOnly = true;
			this.g1ngay.Width = 75;
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
			// frm_XacNhanNgayLVTinhPC100
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(435, 453);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnXoa);
			this.Controls.Add(this.btnDuyet);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_XacNhanNgayLVTinhPC100";
			this.Text = "Duyệt ngày làm việc tính phụ cấp 100%";
			this.Load += new System.EventHandler(this.frm_XacNhanNgayLVTinhPC100_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdNgayNghi)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgrdNgayNghi;
		private System.Windows.Forms.Button btnDuyet;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.Button btnXoa;
		private System.Windows.Forms.DataGridViewCheckBoxColumn g1check;
		private System.Windows.Forms.DataGridViewTextBoxColumn g2macc;
		private System.Windows.Forms.DataGridViewTextBoxColumn g3colManv;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1tennv;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1ngay;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1th;
		private System.Windows.Forms.DataGridViewTextBoxColumn colPhucap;
		private System.Windows.Forms.DataGridViewTextBoxColumn colphantram;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1ID;
	}
}