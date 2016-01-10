namespace HTQLTTKH {
	partial class zTestControl {
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
			this.components = new System.ComponentModel.Container();
			this.checkedComboBoxEdit1 = new DevExpress.XtraEditors.CheckedComboBoxEdit();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.timeEdit1 = new DevExpress.XtraEditors.TimeEdit();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// checkedComboBoxEdit1
			// 
			this.checkedComboBoxEdit1.Location = new System.Drawing.Point(47, 62);
			this.checkedComboBoxEdit1.Name = "checkedComboBoxEdit1";
			this.checkedComboBoxEdit1.Properties.AllowMultiSelect = true;
			this.checkedComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.checkedComboBoxEdit1.Properties.DisplayMember = "Description";
			this.checkedComboBoxEdit1.Properties.DropDownRows = 10;
			this.checkedComboBoxEdit1.Properties.EditValueType = DevExpress.XtraEditors.Repository.EditValueTypeCollection.List;
			this.checkedComboBoxEdit1.Properties.IncrementalSearch = true;
			this.checkedComboBoxEdit1.Properties.ValueMember = "ID";
			this.checkedComboBoxEdit1.Size = new System.Drawing.Size(342, 22);
			this.checkedComboBoxEdit1.TabIndex = 0;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(63, 252);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(517, 252);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(47, 115);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Init";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(128, 115);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "Process";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// timeEdit1
			// 
			this.timeEdit1.EditValue = new System.DateTime(2016, 1, 10, 0, 0, 0, 0);
			this.timeEdit1.Location = new System.Drawing.Point(401, 91);
			this.timeEdit1.Name = "timeEdit1";
			this.timeEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.timeEdit1.Properties.DisplayFormat.FormatString = "%h:mm dddd dd/MM/yyyy";
			this.timeEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.timeEdit1.Properties.EditFormat.FormatString = "%h:mm dddd dd/MM/yyyy";
			this.timeEdit1.Properties.Mask.EditMask = "H:mm dd/MM/yyyy";
			this.timeEdit1.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
			this.timeEdit1.Size = new System.Drawing.Size(271, 22);
			this.timeEdit1.TabIndex = 3;
			// 
			// simpleButton1
			// 
			this.simpleButton1.Location = new System.Drawing.Point(407, 140);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(75, 23);
			this.simpleButton1.TabIndex = 4;
			this.simpleButton1.Text = "simpleButton1";
			this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
			// 
			// zTestControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 593);
			this.Controls.Add(this.simpleButton1);
			this.Controls.Add(this.timeEdit1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.checkedComboBoxEdit1);
			this.Name = "zTestControl";
			this.Text = "zTestControl";
			((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.CheckedComboBoxEdit checkedComboBoxEdit1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private DevExpress.XtraEditors.TimeEdit timeEdit1;
		private DevExpress.XtraEditors.SimpleButton simpleButton1;
	}
}