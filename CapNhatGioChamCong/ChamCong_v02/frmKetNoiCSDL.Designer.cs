namespace ChamCong_v02 {
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
			this.btnThoat = new DevComponents.DotNetBar.ButtonX();
			this.tbPass = new System.Windows.Forms.TextBox();
			this.tbUser = new System.Windows.Forms.TextBox();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.tbServer = new System.Windows.Forms.TextBox();
			this.labelX4 = new DevComponents.DotNetBar.LabelX();
			this.tbDatabase = new System.Windows.Forms.TextBox();
			this.btnKetnoiCSDL = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// btnThoat
			// 
			this.btnThoat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnThoat.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnThoat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnThoat.Location = new System.Drawing.Point(93, 156);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(175, 27);
			this.btnThoat.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnThoat.TabIndex = 5;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// tbPass
			// 
			this.tbPass.Location = new System.Drawing.Point(93, 67);
			this.tbPass.Name = "tbPass";
			this.tbPass.PasswordChar = '*';
			this.tbPass.Size = new System.Drawing.Size(175, 22);
			this.tbPass.TabIndex = 2;
			this.tbPass.UseSystemPasswordChar = true;
			// 
			// tbUser
			// 
			this.tbUser.Location = new System.Drawing.Point(93, 39);
			this.tbUser.Multiline = true;
			this.tbUser.Name = "tbUser";
			this.tbUser.Size = new System.Drawing.Size(175, 22);
			this.tbUser.TabIndex = 1;
			// 
			// labelX2
			// 
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.Class = "";
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX2.ForeColor = System.Drawing.Color.Black;
			this.labelX2.Location = new System.Drawing.Point(12, 68);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(75, 22);
			this.labelX2.TabIndex = 3;
			this.labelX2.Text = "Password";
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX1.ForeColor = System.Drawing.Color.Black;
			this.labelX1.Location = new System.Drawing.Point(12, 40);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(75, 22);
			this.labelX1.TabIndex = 4;
			this.labelX1.Text = "User";
			// 
			// labelX3
			// 
			// 
			// 
			// 
			this.labelX3.BackgroundStyle.Class = "";
			this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX3.ForeColor = System.Drawing.Color.Black;
			this.labelX3.Location = new System.Drawing.Point(12, 12);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(75, 22);
			this.labelX3.TabIndex = 4;
			this.labelX3.Text = "Server";
			// 
			// tbServer
			// 
			this.tbServer.Location = new System.Drawing.Point(93, 11);
			this.tbServer.Multiline = true;
			this.tbServer.Name = "tbServer";
			this.tbServer.Size = new System.Drawing.Size(175, 22);
			this.tbServer.TabIndex = 0;
			// 
			// labelX4
			// 
			// 
			// 
			// 
			this.labelX4.BackgroundStyle.Class = "";
			this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX4.ForeColor = System.Drawing.Color.Black;
			this.labelX4.Location = new System.Drawing.Point(12, 96);
			this.labelX4.Name = "labelX4";
			this.labelX4.Size = new System.Drawing.Size(75, 22);
			this.labelX4.TabIndex = 4;
			this.labelX4.Text = "Database";
			// 
			// tbDatabase
			// 
			this.tbDatabase.Location = new System.Drawing.Point(93, 95);
			this.tbDatabase.Multiline = true;
			this.tbDatabase.Name = "tbDatabase";
			this.tbDatabase.Size = new System.Drawing.Size(175, 22);
			this.tbDatabase.TabIndex = 3;
			// 
			// btnKetnoiCSDL
			// 
			this.btnKetnoiCSDL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnKetnoiCSDL.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnKetnoiCSDL.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnKetnoiCSDL.Location = new System.Drawing.Point(93, 123);
			this.btnKetnoiCSDL.Name = "btnKetnoiCSDL";
			this.btnKetnoiCSDL.Size = new System.Drawing.Size(175, 27);
			this.btnKetnoiCSDL.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnKetnoiCSDL.TabIndex = 4;
			this.btnKetnoiCSDL.Text = "Kết nối CSDL";
			this.btnKetnoiCSDL.Click += new System.EventHandler(this.btnKetnoiCSDL_Click);
			// 
			// frmKetNoiCSDL
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(282, 194);
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
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "frmKetNoiCSDL";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Kết nối CSDL";
			this.Load += new System.EventHandler(this.frmKetNoiCSDL_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnThoat;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.TextBox tbUser;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.TextBox tbServer;
        private DevComponents.DotNetBar.LabelX labelX4;
        private System.Windows.Forms.TextBox tbDatabase;
        private DevComponents.DotNetBar.ButtonX btnKetnoiCSDL;
    }
}