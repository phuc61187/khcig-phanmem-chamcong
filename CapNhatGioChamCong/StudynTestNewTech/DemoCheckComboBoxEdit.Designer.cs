namespace StudynTestNewTech {
	partial class DemoCheckComboBoxEdit {
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
			this.checkedComboBoxEdit1 = new DevExpress.XtraEditors.CheckedComboBoxEdit();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// checkedComboBoxEdit1
			// 
			this.checkedComboBoxEdit1.Location = new System.Drawing.Point(12, 12);
			this.checkedComboBoxEdit1.Name = "checkedComboBoxEdit1";
			this.checkedComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.checkedComboBoxEdit1.Size = new System.Drawing.Size(313, 22);
			this.checkedComboBoxEdit1.TabIndex = 0;
			// 
			// simpleButton1
			// 
			this.simpleButton1.Location = new System.Drawing.Point(332, 13);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(128, 23);
			this.simpleButton1.TabIndex = 1;
			this.simpleButton1.Text = "LoadDataSource";
			this.simpleButton1.Click += new System.EventHandler(this.btnLoadDataSource_Click);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(12, 40);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(313, 96);
			this.richTextBox1.TabIndex = 2;
			this.richTextBox1.Text = "";
			// 
			// simpleButton2
			// 
			this.simpleButton2.Location = new System.Drawing.Point(332, 42);
			this.simpleButton2.Name = "simpleButton2";
			this.simpleButton2.Size = new System.Drawing.Size(128, 23);
			this.simpleButton2.TabIndex = 1;
			this.simpleButton2.Text = "GetItem";
			this.simpleButton2.Click += new System.EventHandler(this.btnGetItem_Click);
			// 
			// DemoCheckComboBoxEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(555, 255);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.simpleButton2);
			this.Controls.Add(this.simpleButton1);
			this.Controls.Add(this.checkedComboBoxEdit1);
			this.Name = "DemoCheckComboBoxEdit";
			this.Text = "DemoCheckComboBoxEdit";
			this.Load += new System.EventHandler(this.DemoCheckComboBoxEdit_Load);
			((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.CheckedComboBoxEdit checkedComboBoxEdit1;
		private DevExpress.XtraEditors.SimpleButton simpleButton1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private DevExpress.XtraEditors.SimpleButton simpleButton2;
	}
}