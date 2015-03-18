namespace ChamCong_v03 {
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
			this.btnThucHien = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbPass1 = new System.Windows.Forms.TextBox();
			this.tbPass2 = new System.Windows.Forms.TextBox();
			this.lbAccount = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnThucHien
			// 
			this.btnThucHien.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThucHien.ForeColor = System.Drawing.Color.Blue;
			this.btnThucHien.Location = new System.Drawing.Point(150, 64);
			this.btnThucHien.Name = "btnThucHien";
			this.btnThucHien.Size = new System.Drawing.Size(83, 25);
			this.btnThucHien.TabIndex = 8;
			this.btnThucHien.Text = "Đồng ý";
			this.btnThucHien.UseVisualStyleBackColor = true;
			this.btnThucHien.Click += new System.EventHandler(this.btnDongY_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(251, 64);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(83, 25);
			this.btnThoat.TabIndex = 9;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label1.Location = new System.Drawing.Point(49, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 15);
			this.label1.TabIndex = 10;
			this.label1.Text = "Mật khẩu mới";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label2.Location = new System.Drawing.Point(25, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(108, 15);
			this.label2.TabIndex = 11;
			this.label2.Text = "Nhập lại mật khẩu";
			// 
			// tbPass1
			// 
			this.tbPass1.Location = new System.Drawing.Point(150, 11);
			this.tbPass1.Name = "tbPass1";
			this.tbPass1.Size = new System.Drawing.Size(184, 21);
			this.tbPass1.TabIndex = 12;
			this.tbPass1.UseSystemPasswordChar = true;
			// 
			// tbPass2
			// 
			this.tbPass2.Location = new System.Drawing.Point(150, 38);
			this.tbPass2.Name = "tbPass2";
			this.tbPass2.Size = new System.Drawing.Size(184, 21);
			this.tbPass2.TabIndex = 13;
			this.tbPass2.UseSystemPasswordChar = true;
			// 
			// lbAccount
			// 
			this.lbAccount.AutoSize = true;
			this.lbAccount.Location = new System.Drawing.Point(34, 64);
			this.lbAccount.Name = "lbAccount";
			this.lbAccount.Size = new System.Drawing.Size(41, 15);
			this.lbAccount.TabIndex = 14;
			this.lbAccount.Text = "label3";
			this.lbAccount.Visible = false;
			// 
			// frm_51_DoiMatKhau
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(346, 103);
			this.Controls.Add(this.lbAccount);
			this.Controls.Add(this.tbPass2);
			this.Controls.Add(this.tbPass1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnThucHien);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_51_DoiMatKhau";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Đổi mật khẩu đăng nhập";
			this.Load += new System.EventHandler(this.frmDoiMatKhau_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnThucHien;
        private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbPass1;
		private System.Windows.Forms.TextBox tbPass2;
		private System.Windows.Forms.Label lbAccount;
    }
}