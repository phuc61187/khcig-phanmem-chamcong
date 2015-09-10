namespace ChamCong_v06.UI.QLTaiKhoan {
	partial class frmNhapTTTaiKhoan {
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
			this.btnTenTaiKhoan = new DevExpress.XtraEditors.ButtonEdit();
			this.label1 = new System.Windows.Forms.Label();
			this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
			this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
			this.btnEditMatkhau = new DevExpress.XtraEditors.ButtonEdit();
			this.label2 = new System.Windows.Forms.Label();
			this.btnEditMatkhau2 = new DevExpress.XtraEditors.ButtonEdit();
			this.label3 = new System.Windows.Forms.Label();
			this.checkEnable = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.btnTenTaiKhoan.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditMatkhau.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditMatkhau2.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// btnTenTaiKhoan
			// 
			this.btnTenTaiKhoan.Location = new System.Drawing.Point(129, 12);
			this.btnTenTaiKhoan.Name = "btnTenTaiKhoan";
			this.btnTenTaiKhoan.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.btnTenTaiKhoan.Properties.Appearance.Options.UseFont = true;
			this.btnTenTaiKhoan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
			this.btnTenTaiKhoan.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnTenPhong_Properties_ClearButtonClick);
			this.btnTenTaiKhoan.Size = new System.Drawing.Size(206, 20);
			this.btnTenTaiKhoan.TabIndex = 19;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 14);
			this.label1.TabIndex = 18;
			this.label1.Text = "Tài khoản";
			// 
			// btnHuy
			// 
			this.btnHuy.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.btnHuy.Appearance.Options.UseFont = true;
			this.btnHuy.Image = global::ChamCong_v06.Properties.Resources._1438546065_Delete;
			this.btnHuy.Location = new System.Drawing.Point(210, 114);
			this.btnHuy.Name = "btnHuy";
			this.btnHuy.Size = new System.Drawing.Size(75, 23);
			this.btnHuy.TabIndex = 21;
			this.btnHuy.Text = "Hủy";
			this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
			// 
			// btnLuu
			// 
			this.btnLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.btnLuu.Appearance.Options.UseFont = true;
			this.btnLuu.Image = global::ChamCong_v06.Properties.Resources._1438546670_Save;
			this.btnLuu.Location = new System.Drawing.Point(129, 114);
			this.btnLuu.Name = "btnLuu";
			this.btnLuu.Size = new System.Drawing.Size(75, 23);
			this.btnLuu.TabIndex = 22;
			this.btnLuu.Text = "Lưu";
			this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
			// 
			// btnEditMatkhau
			// 
			this.btnEditMatkhau.EditValue = "";
			this.btnEditMatkhau.Location = new System.Drawing.Point(129, 38);
			this.btnEditMatkhau.Name = "btnEditMatkhau";
			this.btnEditMatkhau.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.btnEditMatkhau.Properties.Appearance.Options.UseFont = true;
			this.btnEditMatkhau.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
			this.btnEditMatkhau.Properties.UseSystemPasswordChar = true;
			this.btnEditMatkhau.Properties.Click += new System.EventHandler(this.btnEdit_Clear_Click);
			this.btnEditMatkhau.Size = new System.Drawing.Size(206, 20);
			this.btnEditMatkhau.TabIndex = 24;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.label2.Location = new System.Drawing.Point(6, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 14);
			this.label2.TabIndex = 23;
			this.label2.Text = "Mật khẩu";
			// 
			// btnEditMatkhau2
			// 
			this.btnEditMatkhau2.EditValue = "";
			this.btnEditMatkhau2.Location = new System.Drawing.Point(129, 64);
			this.btnEditMatkhau2.Name = "btnEditMatkhau2";
			this.btnEditMatkhau2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.btnEditMatkhau2.Properties.Appearance.Options.UseFont = true;
			this.btnEditMatkhau2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
			this.btnEditMatkhau2.Properties.UseSystemPasswordChar = true;
			this.btnEditMatkhau2.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEdit_Clear_Click);
			this.btnEditMatkhau2.Size = new System.Drawing.Size(206, 20);
			this.btnEditMatkhau2.TabIndex = 26;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.label3.Location = new System.Drawing.Point(6, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 14);
			this.label3.TabIndex = 25;
			this.label3.Text = "Nhập lại mật khẩu";
			// 
			// checkEnable
			// 
			this.checkEnable.AutoSize = true;
			this.checkEnable.Location = new System.Drawing.Point(129, 90);
			this.checkEnable.Name = "checkEnable";
			this.checkEnable.Size = new System.Drawing.Size(62, 18);
			this.checkEnable.TabIndex = 28;
			this.checkEnable.Text = "Enable";
			this.checkEnable.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 91);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 14);
			this.label4.TabIndex = 27;
			this.label4.Text = "Trạng thái";
			// 
			// frmNhapTTTaiKhoan
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(347, 150);
			this.Controls.Add(this.checkEnable);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnEditMatkhau2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnEditMatkhau);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnHuy);
			this.Controls.Add(this.btnLuu);
			this.Controls.Add(this.btnTenTaiKhoan);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "frmNhapTTTaiKhoan";
			this.Text = "Thông tin tài khoản";
			this.Load += new System.EventHandler(this.frmNhapTTTaiKhoan_Load);
			((System.ComponentModel.ISupportInitialize)(this.btnTenTaiKhoan.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditMatkhau.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditMatkhau2.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.ButtonEdit btnTenTaiKhoan;
		private System.Windows.Forms.Label label1;
		private DevExpress.XtraEditors.SimpleButton btnHuy;
		private DevExpress.XtraEditors.SimpleButton btnLuu;
		private DevExpress.XtraEditors.ButtonEdit btnEditMatkhau;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraEditors.ButtonEdit btnEditMatkhau2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox checkEnable;
		private System.Windows.Forms.Label label4;
	}
}