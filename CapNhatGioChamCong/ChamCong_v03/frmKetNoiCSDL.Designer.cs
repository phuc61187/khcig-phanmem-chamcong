namespace ChamCong_v03 {
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
			this.btnThoat = new System.Windows.Forms.Button();
			this.tbPass = new System.Windows.Forms.TextBox();
			this.tbUser = new System.Windows.Forms.TextBox();
			this.labelX2 = new System.Windows.Forms.Label();
			this.labelX1 = new System.Windows.Forms.Label();
			this.labelX3 = new System.Windows.Forms.Label();
			this.tbServer = new System.Windows.Forms.TextBox();
			this.labelX4 = new System.Windows.Forms.Label();
			this.tbDatabase = new System.Windows.Forms.TextBox();
			this.btnKetnoiCSDL = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnThoat
			// 
			this.btnThoat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(93, 146);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(175, 25);
			this.btnThoat.TabIndex = 5;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// tbPass
			// 
			this.tbPass.Location = new System.Drawing.Point(93, 63);
			this.tbPass.Name = "tbPass";
			this.tbPass.PasswordChar = '*';
			this.tbPass.Size = new System.Drawing.Size(175, 21);
			this.tbPass.TabIndex = 2;
			this.tbPass.UseSystemPasswordChar = true;
			// 
			// tbUser
			// 
			this.tbUser.Location = new System.Drawing.Point(93, 37);
			this.tbUser.Multiline = true;
			this.tbUser.Name = "tbUser";
			this.tbUser.Size = new System.Drawing.Size(175, 21);
			this.tbUser.TabIndex = 1;
			// 
			// labelX2
			// 
			this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.labelX2.ForeColor = System.Drawing.Color.Black;
			this.labelX2.Location = new System.Drawing.Point(12, 64);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(75, 21);
			this.labelX2.TabIndex = 3;
			this.labelX2.Text = "Password";
			// 
			// labelX1
			// 
			this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.labelX1.ForeColor = System.Drawing.Color.Black;
			this.labelX1.Location = new System.Drawing.Point(12, 38);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(75, 21);
			this.labelX1.TabIndex = 4;
			this.labelX1.Text = "User";
			// 
			// labelX3
			// 
			this.labelX3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.labelX3.ForeColor = System.Drawing.Color.Black;
			this.labelX3.Location = new System.Drawing.Point(12, 11);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(75, 21);
			this.labelX3.TabIndex = 4;
			this.labelX3.Text = "Server";
			// 
			// tbServer
			// 
			this.tbServer.Location = new System.Drawing.Point(93, 10);
			this.tbServer.Multiline = true;
			this.tbServer.Name = "tbServer";
			this.tbServer.Size = new System.Drawing.Size(175, 21);
			this.tbServer.TabIndex = 0;
			// 
			// labelX4
			// 
			this.labelX4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.labelX4.ForeColor = System.Drawing.Color.Black;
			this.labelX4.Location = new System.Drawing.Point(12, 90);
			this.labelX4.Name = "labelX4";
			this.labelX4.Size = new System.Drawing.Size(75, 21);
			this.labelX4.TabIndex = 4;
			this.labelX4.Text = "Database";
			// 
			// tbDatabase
			// 
			this.tbDatabase.Location = new System.Drawing.Point(93, 89);
			this.tbDatabase.Multiline = true;
			this.tbDatabase.Name = "tbDatabase";
			this.tbDatabase.Size = new System.Drawing.Size(175, 21);
			this.tbDatabase.TabIndex = 3;
			// 
			// btnKetnoiCSDL
			// 
			this.btnKetnoiCSDL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnKetnoiCSDL.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnKetnoiCSDL.ForeColor = System.Drawing.Color.Blue;
			this.btnKetnoiCSDL.Location = new System.Drawing.Point(93, 115);
			this.btnKetnoiCSDL.Name = "btnKetnoiCSDL";
			this.btnKetnoiCSDL.Size = new System.Drawing.Size(175, 25);
			this.btnKetnoiCSDL.TabIndex = 4;
			this.btnKetnoiCSDL.Text = "Kết nối CSDL";
			this.btnKetnoiCSDL.Click += new System.EventHandler(this.btnKetnoiCSDL_Click);
			// 
			// frmKetNoiCSDL
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(282, 182);
			this.ControlBox = false;
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnKetnoiCSDL);
			this.Controls.Add(this.tbPass);
			this.Controls.Add(this.tbServer);
			this.Controls.Add(this.tbDatabase);
			this.Controls.Add(this.tbUser);
			this.Controls.Add(this.labelX3);
			this.Controls.Add(this.labelX4);
			this.Controls.Add(this.labelX2);
			this.Controls.Add(this.labelX1);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "frmKetNoiCSDL";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Kết nối CSDL";
			this.Load += new System.EventHandler(this.frmKetNoiCSDL_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.TextBox tbUser;
		private System.Windows.Forms.Label labelX2;
		private System.Windows.Forms.Label labelX1;
		private System.Windows.Forms.Label labelX3;
        private System.Windows.Forms.TextBox tbServer;
		private System.Windows.Forms.Label labelX4;
        private System.Windows.Forms.TextBox tbDatabase;
		private System.Windows.Forms.Button btnKetnoiCSDL;
    }
}