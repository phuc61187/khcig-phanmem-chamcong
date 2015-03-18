namespace ChamCong_v04.UI.Admin {
    partial class frm_Admin {
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
			this.btnChonCSDL = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.cbTaikhoanWE = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dataGridTK = new System.Windows.Forms.DataGridView();
			this.colUserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colUserAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbPass2 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tbTenTaiKhoan = new System.Windows.Forms.TextBox();
			this.tbPass1 = new System.Windows.Forms.TextBox();
			this.btnReset = new System.Windows.Forms.Button();
			this.btnTaoTK = new System.Windows.Forms.Button();
			this.btnXoaTK = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnPhanQuyen = new System.Windows.Forms.Button();
			this.btnCaiDatThongSo = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.btnTaoCaLV = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridTK)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnChonCSDL
			// 
			this.btnChonCSDL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnChonCSDL.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnChonCSDL.ForeColor = System.Drawing.Color.Blue;
			this.btnChonCSDL.Location = new System.Drawing.Point(13, 13);
			this.btnChonCSDL.Name = "btnChonCSDL";
			this.btnChonCSDL.Size = new System.Drawing.Size(199, 27);
			this.btnChonCSDL.TabIndex = 0;
			this.btnChonCSDL.Text = "Chọn kết nối CSDL";
			this.btnChonCSDL.Click += new System.EventHandler(this.btnChonCSDL_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(288, 608);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(150, 27);
			this.btnThoat.TabIndex = 0;
			this.btnThoat.Text = "Thoát ứng dụng";
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// cbTaikhoanWE
			// 
			this.cbTaikhoanWE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTaikhoanWE.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.cbTaikhoanWE.ForeColor = System.Drawing.Color.Blue;
			this.cbTaikhoanWE.Location = new System.Drawing.Point(181, 27);
			this.cbTaikhoanWE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cbTaikhoanWE.Name = "cbTaikhoanWE";
			this.cbTaikhoanWE.Size = new System.Drawing.Size(226, 23);
			this.cbTaikhoanWE.TabIndex = 0;
			this.cbTaikhoanWE.SelectedIndexChanged += new System.EventHandler(this.cbTaikhoanWE_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.Transparent;
			this.groupBox1.Controls.Add(this.dataGridTK);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.groupBox1.Location = new System.Drawing.Point(8, 184);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox1.Size = new System.Drawing.Size(411, 299);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Danh sách tài khoản ";
			// 
			// dataGridTK
			// 
			this.dataGridTK.AllowUserToAddRows = false;
			this.dataGridTK.AllowUserToDeleteRows = false;
			this.dataGridTK.AllowUserToOrderColumns = true;
			this.dataGridTK.AllowUserToResizeRows = false;
			this.dataGridTK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridTK.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colUserID,
            this.colUserAccount});
			this.dataGridTK.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridTK.Location = new System.Drawing.Point(3, 18);
			this.dataGridTK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dataGridTK.MultiSelect = false;
			this.dataGridTK.Name = "dataGridTK";
			this.dataGridTK.ReadOnly = true;
			this.dataGridTK.RowHeadersVisible = false;
			this.dataGridTK.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
			this.dataGridTK.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridTK.Size = new System.Drawing.Size(405, 277);
			this.dataGridTK.TabIndex = 6;
			this.dataGridTK.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridTK_CellClick);
			// 
			// colUserID
			// 
			this.colUserID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.colUserID.DataPropertyName = "UserID";
			this.colUserID.HeaderText = "ID";
			this.colUserID.Name = "colUserID";
			this.colUserID.ReadOnly = true;
			this.colUserID.Width = 44;
			// 
			// colUserAccount
			// 
			this.colUserAccount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colUserAccount.DataPropertyName = "UserAccount";
			this.colUserAccount.HeaderText = "Tài khoản";
			this.colUserAccount.Name = "colUserAccount";
			this.colUserAccount.ReadOnly = true;
			this.colUserAccount.Width = 86;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label4.ForeColor = System.Drawing.Color.Black;
			this.label4.Location = new System.Drawing.Point(59, 122);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(108, 15);
			this.label4.TabIndex = 11;
			this.label4.Text = "Nhập lại mật khẩu";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label2.ForeColor = System.Drawing.Color.Black;
			this.label2.Location = new System.Drawing.Point(108, 92);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 15);
			this.label2.TabIndex = 12;
			this.label2.Text = "Mật khẩu";
			// 
			// tbPass2
			// 
			this.tbPass2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.tbPass2.ForeColor = System.Drawing.Color.Blue;
			this.tbPass2.Location = new System.Drawing.Point(181, 119);
			this.tbPass2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tbPass2.Name = "tbPass2";
			this.tbPass2.Size = new System.Drawing.Size(225, 21);
			this.tbPass2.TabIndex = 3;
			this.tbPass2.UseSystemPasswordChar = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label5.ForeColor = System.Drawing.Color.Black;
			this.label5.Location = new System.Drawing.Point(24, 30);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(143, 15);
			this.label5.TabIndex = 8;
			this.label5.Text = "Chọn tài khoản WiseEye";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(84, 62);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 15);
			this.label1.TabIndex = 9;
			this.label1.Text = "Tên tài khoản";
			// 
			// tbTenTaiKhoan
			// 
			this.tbTenTaiKhoan.Enabled = false;
			this.tbTenTaiKhoan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.tbTenTaiKhoan.ForeColor = System.Drawing.Color.Blue;
			this.tbTenTaiKhoan.Location = new System.Drawing.Point(181, 59);
			this.tbTenTaiKhoan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tbTenTaiKhoan.Name = "tbTenTaiKhoan";
			this.tbTenTaiKhoan.Size = new System.Drawing.Size(226, 21);
			this.tbTenTaiKhoan.TabIndex = 1;
			// 
			// tbPass1
			// 
			this.tbPass1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.tbPass1.ForeColor = System.Drawing.Color.Blue;
			this.tbPass1.Location = new System.Drawing.Point(181, 89);
			this.tbPass1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tbPass1.Name = "tbPass1";
			this.tbPass1.Size = new System.Drawing.Size(226, 21);
			this.tbPass1.TabIndex = 2;
			this.tbPass1.UseSystemPasswordChar = true;
			// 
			// btnReset
			// 
			this.btnReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnReset.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnReset.ForeColor = System.Drawing.Color.Blue;
			this.btnReset.Location = new System.Drawing.Point(298, 150);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(114, 27);
			this.btnReset.TabIndex = 6;
			this.btnReset.Text = "Reset mật khẩu";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// btnTaoTK
			// 
			this.btnTaoTK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnTaoTK.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnTaoTK.ForeColor = System.Drawing.Color.Blue;
			this.btnTaoTK.Location = new System.Drawing.Point(181, 150);
			this.btnTaoTK.Name = "btnTaoTK";
			this.btnTaoTK.Size = new System.Drawing.Size(111, 27);
			this.btnTaoTK.TabIndex = 5;
			this.btnTaoTK.Text = "Tạo tài khoản";
			this.btnTaoTK.Click += new System.EventHandler(this.btnTaoTK_Click);
			// 
			// btnXoaTK
			// 
			this.btnXoaTK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnXoaTK.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnXoaTK.ForeColor = System.Drawing.Color.Blue;
			this.btnXoaTK.Location = new System.Drawing.Point(64, 150);
			this.btnXoaTK.Name = "btnXoaTK";
			this.btnXoaTK.Size = new System.Drawing.Size(111, 27);
			this.btnXoaTK.TabIndex = 4;
			this.btnXoaTK.Text = "Xóa tài khoản";
			this.btnXoaTK.Click += new System.EventHandler(this.btnXoaTK_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cbTaikhoanWE);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.tbPass1);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.btnReset);
			this.groupBox2.Controls.Add(this.tbPass2);
			this.groupBox2.Controls.Add(this.btnTaoTK);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.btnXoaTK);
			this.groupBox2.Controls.Add(this.groupBox1);
			this.groupBox2.Controls.Add(this.tbTenTaiKhoan);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.groupBox2.Location = new System.Drawing.Point(13, 112);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.groupBox2.Size = new System.Drawing.Size(426, 490);
			this.groupBox2.TabIndex = 21;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Tạo tài khoản sử dụng";
			// 
			// btnPhanQuyen
			// 
			this.btnPhanQuyen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnPhanQuyen.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnPhanQuyen.ForeColor = System.Drawing.Color.Blue;
			this.btnPhanQuyen.Location = new System.Drawing.Point(239, 13);
			this.btnPhanQuyen.Name = "btnPhanQuyen";
			this.btnPhanQuyen.Size = new System.Drawing.Size(199, 27);
			this.btnPhanQuyen.TabIndex = 1;
			this.btnPhanQuyen.Text = "Phân quyền chức năng";
			this.btnPhanQuyen.Click += new System.EventHandler(this.btnPhanQuyen_Click);
			// 
			// btnCaiDatThongSo
			// 
			this.btnCaiDatThongSo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnCaiDatThongSo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnCaiDatThongSo.ForeColor = System.Drawing.Color.Blue;
			this.btnCaiDatThongSo.Location = new System.Drawing.Point(13, 46);
			this.btnCaiDatThongSo.Name = "btnCaiDatThongSo";
			this.btnCaiDatThongSo.Size = new System.Drawing.Size(199, 27);
			this.btnCaiDatThongSo.TabIndex = 2;
			this.btnCaiDatThongSo.Text = "Cài đặt thông số";
			this.btnCaiDatThongSo.Click += new System.EventHandler(this.btnCaiDatThongSo_Click);
			// 
			// button2
			// 
			this.button2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.button2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.button2.ForeColor = System.Drawing.Color.Blue;
			this.button2.Location = new System.Drawing.Point(239, 46);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(199, 27);
			this.button2.TabIndex = 3;
			this.button2.Text = "Phục hồi, huỷ XN giờ";
			this.button2.Click += new System.EventHandler(this.btnPhucHoi_HuyXN_GioCC_Click);
			// 
			// btnTaoCaLV
			// 
			this.btnTaoCaLV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnTaoCaLV.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnTaoCaLV.ForeColor = System.Drawing.Color.Blue;
			this.btnTaoCaLV.Location = new System.Drawing.Point(13, 79);
			this.btnTaoCaLV.Name = "btnTaoCaLV";
			this.btnTaoCaLV.Size = new System.Drawing.Size(199, 27);
			this.btnTaoCaLV.TabIndex = 4;
			this.btnTaoCaLV.Text = "Tạo ca làm việc";
			this.btnTaoCaLV.Click += new System.EventHandler(this.btnTaoCaLV_Click);
			// 
			// frm_Admin
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(451, 652);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnTaoCaLV);
			this.Controls.Add(this.btnCaiDatThongSo);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.btnPhanQuyen);
			this.Controls.Add(this.btnChonCSDL);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "frm_Admin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Admin form";
			this.Load += new System.EventHandler(this.frmTaoTaiKhoan_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridTK)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnChonCSDL;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.ComboBox cbTaikhoanWE;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridTK;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserAccount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPass2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTenTaiKhoan;
        private System.Windows.Forms.TextBox tbPass1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnTaoTK;
        private System.Windows.Forms.Button btnXoaTK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPhanQuyen;
		private System.Windows.Forms.Button btnCaiDatThongSo;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button btnTaoCaLV;//
    }
}