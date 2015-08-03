namespace ChamCong_v06.UI {
	partial class frmChangePassword {
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
			this.btnEditMatkhauMoi1 = new DevExpress.XtraEditors.ButtonEdit();
			this.btnEditMatkhauCu = new DevExpress.XtraEditors.ButtonEdit();
			this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
			this.btnThaydoi = new DevExpress.XtraEditors.SimpleButton();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnEditMatkhauMoi2 = new DevExpress.XtraEditors.ButtonEdit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditMatkhauMoi1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditMatkhauCu.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditMatkhauMoi2.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// btnEditMatkhauMoi1
			// 
			this.btnEditMatkhauMoi1.EditValue = "";
			this.btnEditMatkhauMoi1.Location = new System.Drawing.Point(164, 38);
			this.btnEditMatkhauMoi1.Name = "btnEditMatkhauMoi1";
			this.btnEditMatkhauMoi1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnEditMatkhauMoi1.Properties.Appearance.Options.UseFont = true;
			this.btnEditMatkhauMoi1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
			this.btnEditMatkhauMoi1.Properties.UseSystemPasswordChar = true;
			this.btnEditMatkhauMoi1.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEdit_Clear_Click);
			this.btnEditMatkhauMoi1.Size = new System.Drawing.Size(206, 20);
			this.btnEditMatkhauMoi1.TabIndex = 7;
			// 
			// btnEditMatkhauCu
			// 
			this.btnEditMatkhauCu.Location = new System.Drawing.Point(164, 12);
			this.btnEditMatkhauCu.Name = "btnEditMatkhauCu";
			this.btnEditMatkhauCu.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnEditMatkhauCu.Properties.Appearance.Options.UseFont = true;
			this.btnEditMatkhauCu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
			this.btnEditMatkhauCu.Properties.UseSystemPasswordChar = true;
			this.btnEditMatkhauCu.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEdit_Clear_Click);
			this.btnEditMatkhauCu.Size = new System.Drawing.Size(206, 20);
			this.btnEditMatkhauCu.TabIndex = 4;
			// 
			// btnThoat
			// 
			this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.Appearance.Options.UseFont = true;
			this.btnThoat.Location = new System.Drawing.Point(270, 90);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(100, 28);
			this.btnThoat.TabIndex = 9;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnThaydoi
			// 
			this.btnThaydoi.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThaydoi.Appearance.Options.UseFont = true;
			this.btnThaydoi.Location = new System.Drawing.Point(164, 90);
			this.btnThaydoi.Name = "btnThaydoi";
			this.btnThaydoi.Size = new System.Drawing.Size(100, 28);
			this.btnThaydoi.TabIndex = 8;
			this.btnThaydoi.Text = "Thay đổi";
			this.btnThaydoi.Click += new System.EventHandler(this.btnThaydoi_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label2.Location = new System.Drawing.Point(8, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 14);
			this.label2.TabIndex = 5;
			this.label2.Text = "Mật khẩu mới";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label1.Location = new System.Drawing.Point(7, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 14);
			this.label1.TabIndex = 6;
			this.label1.Text = "Mật khẩu cũ";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label3.Location = new System.Drawing.Point(8, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(143, 14);
			this.label3.TabIndex = 5;
			this.label3.Text = "Nhập lại mật khẩu mới";
			// 
			// btnEditMatkhauMoi2
			// 
			this.btnEditMatkhauMoi2.EditValue = "";
			this.btnEditMatkhauMoi2.Location = new System.Drawing.Point(164, 64);
			this.btnEditMatkhauMoi2.Name = "btnEditMatkhauMoi2";
			this.btnEditMatkhauMoi2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnEditMatkhauMoi2.Properties.Appearance.Options.UseFont = true;
			this.btnEditMatkhauMoi2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
			this.btnEditMatkhauMoi2.Properties.UseSystemPasswordChar = true;
			this.btnEditMatkhauMoi2.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEdit_Clear_Click);
			this.btnEditMatkhauMoi2.Size = new System.Drawing.Size(206, 20);
			this.btnEditMatkhauMoi2.TabIndex = 7;
			// 
			// frmChangePassword
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(383, 133);
			this.Controls.Add(this.btnEditMatkhauMoi2);
			this.Controls.Add(this.btnEditMatkhauMoi1);
			this.Controls.Add(this.btnEditMatkhauCu);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnThaydoi);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "frmChangePassword";
			this.Text = "Thay đổi mật khẩu";
			((System.ComponentModel.ISupportInitialize)(this.btnEditMatkhauMoi1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditMatkhauCu.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditMatkhauMoi2.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.ButtonEdit btnEditMatkhauMoi1;
		private DevExpress.XtraEditors.ButtonEdit btnEditMatkhauCu;
		private DevExpress.XtraEditors.SimpleButton btnThoat;
		private DevExpress.XtraEditors.SimpleButton btnThaydoi;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private DevExpress.XtraEditors.ButtonEdit btnEditMatkhauMoi2;
	}
}