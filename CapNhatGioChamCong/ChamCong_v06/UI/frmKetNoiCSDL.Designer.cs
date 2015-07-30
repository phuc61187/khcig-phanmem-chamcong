namespace ChamCong_v06.UI {
	partial class frmKetNoiCSDL {
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
			this.btnEditUser = new DevExpress.XtraEditors.ButtonEdit();
			this.btnEditServer = new DevExpress.XtraEditors.ButtonEdit();
			this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
			this.btnKetnoiCSDL = new DevExpress.XtraEditors.SimpleButton();
			this.labelX3 = new System.Windows.Forms.Label();
			this.labelX4 = new System.Windows.Forms.Label();
			this.labelX2 = new System.Windows.Forms.Label();
			this.labelX1 = new System.Windows.Forms.Label();
			this.btnEditPassword = new DevExpress.XtraEditors.ButtonEdit();
			this.btnEditDatabase = new DevExpress.XtraEditors.ButtonEdit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditUser.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditServer.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditPassword.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditDatabase.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// btnEditUser
			// 
			this.btnEditUser.EditValue = "";
			this.btnEditUser.Location = new System.Drawing.Point(81, 33);
			this.btnEditUser.Margin = new System.Windows.Forms.Padding(2);
			this.btnEditUser.Name = "btnEditUser";
			this.btnEditUser.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnEditUser.Properties.Appearance.Options.UseFont = true;
			this.btnEditUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
			this.btnEditUser.Properties.Click += new System.EventHandler(this.btnEdit_Clear_Click);
			this.btnEditUser.Size = new System.Drawing.Size(180, 20);
			this.btnEditUser.TabIndex = 1;
			// 
			// btnEditServer
			// 
			this.btnEditServer.Location = new System.Drawing.Point(81, 11);
			this.btnEditServer.Margin = new System.Windows.Forms.Padding(2);
			this.btnEditServer.Name = "btnEditServer";
			this.btnEditServer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnEditServer.Properties.Appearance.Options.UseFont = true;
			this.btnEditServer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
			this.btnEditServer.Properties.Click += new System.EventHandler(this.btnEdit_Clear_Click);
			this.btnEditServer.Size = new System.Drawing.Size(180, 20);
			this.btnEditServer.TabIndex = 0;
			// 
			// btnThoat
			// 
			this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.Appearance.Options.UseFont = true;
			this.btnThoat.Location = new System.Drawing.Point(173, 103);
			this.btnThoat.Margin = new System.Windows.Forms.Padding(2);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(87, 25);
			this.btnThoat.TabIndex = 5;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnKetnoiCSDL
			// 
			this.btnKetnoiCSDL.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnKetnoiCSDL.Appearance.Options.UseFont = true;
			this.btnKetnoiCSDL.Location = new System.Drawing.Point(82, 103);
			this.btnKetnoiCSDL.Margin = new System.Windows.Forms.Padding(2);
			this.btnKetnoiCSDL.Name = "btnKetnoiCSDL";
			this.btnKetnoiCSDL.Size = new System.Drawing.Size(87, 25);
			this.btnKetnoiCSDL.TabIndex = 4;
			this.btnKetnoiCSDL.Text = "Kết nối CSDL";
			this.btnKetnoiCSDL.Click += new System.EventHandler(this.btnKetnoiCSDL_Click);
			// 
			// labelX3
			// 
			this.labelX3.AutoSize = true;
			this.labelX3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.labelX3.ForeColor = System.Drawing.Color.Black;
			this.labelX3.Location = new System.Drawing.Point(10, 13);
			this.labelX3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(50, 16);
			this.labelX3.TabIndex = 17;
			this.labelX3.Text = "Server";
			// 
			// labelX4
			// 
			this.labelX4.AutoSize = true;
			this.labelX4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.labelX4.ForeColor = System.Drawing.Color.Black;
			this.labelX4.Location = new System.Drawing.Point(10, 82);
			this.labelX4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelX4.Name = "labelX4";
			this.labelX4.Size = new System.Drawing.Size(67, 16);
			this.labelX4.TabIndex = 18;
			this.labelX4.Text = "Database";
			// 
			// labelX2
			// 
			this.labelX2.AutoSize = true;
			this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.labelX2.ForeColor = System.Drawing.Color.Black;
			this.labelX2.Location = new System.Drawing.Point(10, 59);
			this.labelX2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(69, 16);
			this.labelX2.TabIndex = 15;
			this.labelX2.Text = "Password";
			// 
			// labelX1
			// 
			this.labelX1.AutoSize = true;
			this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.labelX1.ForeColor = System.Drawing.Color.Black;
			this.labelX1.Location = new System.Drawing.Point(10, 37);
			this.labelX1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(36, 16);
			this.labelX1.TabIndex = 19;
			this.labelX1.Text = "User";
			// 
			// btnEditPassword
			// 
			this.btnEditPassword.Location = new System.Drawing.Point(81, 56);
			this.btnEditPassword.Margin = new System.Windows.Forms.Padding(2);
			this.btnEditPassword.Name = "btnEditPassword";
			this.btnEditPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnEditPassword.Properties.Appearance.Options.UseFont = true;
			this.btnEditPassword.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
			this.btnEditPassword.Properties.UseSystemPasswordChar = true;
			this.btnEditPassword.Properties.Click += new System.EventHandler(this.btnEdit_Clear_Click);
			this.btnEditPassword.Size = new System.Drawing.Size(180, 20);
			this.btnEditPassword.TabIndex = 2;
			// 
			// btnEditDatabase
			// 
			this.btnEditDatabase.EditValue = "";
			this.btnEditDatabase.Location = new System.Drawing.Point(81, 79);
			this.btnEditDatabase.Margin = new System.Windows.Forms.Padding(2);
			this.btnEditDatabase.Name = "btnEditDatabase";
			this.btnEditDatabase.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnEditDatabase.Properties.Appearance.Options.UseFont = true;
			this.btnEditDatabase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
			this.btnEditDatabase.Properties.Click += new System.EventHandler(this.btnEdit_Clear_Click);
			this.btnEditDatabase.Size = new System.Drawing.Size(180, 20);
			this.btnEditDatabase.TabIndex = 3;
			// 
			// frmKetNoiCSDL
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(274, 138);
			this.Controls.Add(this.labelX3);
			this.Controls.Add(this.labelX4);
			this.Controls.Add(this.labelX2);
			this.Controls.Add(this.labelX1);
			this.Controls.Add(this.btnEditDatabase);
			this.Controls.Add(this.btnEditUser);
			this.Controls.Add(this.btnEditPassword);
			this.Controls.Add(this.btnEditServer);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnKetnoiCSDL);
			this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmKetNoiCSDL";
			this.Text = "Kết nối CSDL";
			((System.ComponentModel.ISupportInitialize)(this.btnEditUser.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditServer.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditPassword.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnEditDatabase.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.ButtonEdit btnEditUser;
		private DevExpress.XtraEditors.ButtonEdit btnEditServer;
		private DevExpress.XtraEditors.SimpleButton btnThoat;
		private DevExpress.XtraEditors.SimpleButton btnKetnoiCSDL;
		private System.Windows.Forms.Label labelX3;
		private System.Windows.Forms.Label labelX4;
		private System.Windows.Forms.Label labelX2;
		private System.Windows.Forms.Label labelX1;
		private DevExpress.XtraEditors.ButtonEdit btnEditPassword;
		private DevExpress.XtraEditors.ButtonEdit btnEditDatabase;
	}
}