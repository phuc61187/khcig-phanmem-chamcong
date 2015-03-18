namespace ChamCong_v02 {
    partial class frm_51_DoiMatKhau {
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
			this.tbTaiKhoan = new System.Windows.Forms.TextBox();
			this.tbPass1 = new System.Windows.Forms.TextBox();
			this.tbPass2 = new System.Windows.Forms.TextBox();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.btnThucHien = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tbTaiKhoan
			// 
			this.tbTaiKhoan.Enabled = false;
			this.tbTaiKhoan.Location = new System.Drawing.Point(150, 12);
			this.tbTaiKhoan.Name = "tbTaiKhoan";
			this.tbTaiKhoan.Size = new System.Drawing.Size(180, 22);
			this.tbTaiKhoan.TabIndex = 0;
			// 
			// tbPass1
			// 
			this.tbPass1.Location = new System.Drawing.Point(150, 40);
			this.tbPass1.Name = "tbPass1";
			this.tbPass1.Size = new System.Drawing.Size(180, 22);
			this.tbPass1.TabIndex = 1;
			this.tbPass1.UseSystemPasswordChar = true;
			// 
			// tbPass2
			// 
			this.tbPass2.Location = new System.Drawing.Point(150, 68);
			this.tbPass2.Name = "tbPass2";
			this.tbPass2.Size = new System.Drawing.Size(180, 22);
			this.tbPass2.TabIndex = 2;
			this.tbPass2.UseSystemPasswordChar = true;
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX1.Location = new System.Drawing.Point(12, 11);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(132, 23);
			this.labelX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.labelX1.TabIndex = 5;
			this.labelX1.Text = "Tài khoản đăng nhập";
			this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// labelX2
			// 
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.Class = "";
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX2.Location = new System.Drawing.Point(54, 38);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(89, 23);
			this.labelX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.labelX2.TabIndex = 7;
			this.labelX2.Text = "Mật khẩu mới";
			this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// labelX3
			// 
			// 
			// 
			// 
			this.labelX3.BackgroundStyle.Class = "";
			this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX3.Location = new System.Drawing.Point(27, 68);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(117, 23);
			this.labelX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.labelX3.TabIndex = 7;
			this.labelX3.Text = "Nhập lại mật khẩu";
			this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// btnThucHien
			// 
			this.btnThucHien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnThucHien.ForeColor = System.Drawing.Color.Blue;
			this.btnThucHien.Location = new System.Drawing.Point(150, 96);
			this.btnThucHien.Name = "btnThucHien";
			this.btnThucHien.Size = new System.Drawing.Size(83, 27);
			this.btnThucHien.TabIndex = 8;
			this.btnThucHien.Text = "Đồng ý";
			this.btnThucHien.UseVisualStyleBackColor = true;
			this.btnThucHien.Click += new System.EventHandler(this.btnDongY_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(247, 96);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(83, 27);
			this.btnThoat.TabIndex = 9;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// frm_51_DoiMatKhau
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(346, 132);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnThucHien);
			this.Controls.Add(this.labelX3);
			this.Controls.Add(this.labelX2);
			this.Controls.Add(this.labelX1);
			this.Controls.Add(this.tbPass2);
			this.Controls.Add(this.tbPass1);
			this.Controls.Add(this.tbTaiKhoan);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_51_DoiMatKhau";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Đổi mật khẩu đăng nhập";
			this.Load += new System.EventHandler(this.frmDoiMatKhau_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.TextBox tbTaiKhoan;
        private System.Windows.Forms.TextBox tbPass1;
		private System.Windows.Forms.TextBox tbPass2;
        private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.LabelX labelX2;
		private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.Button btnThucHien;
        private System.Windows.Forms.Button btnThoat;
    }
}