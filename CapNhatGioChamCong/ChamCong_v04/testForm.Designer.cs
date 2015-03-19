namespace ChamCong_v04 {
	partial class testForm {
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
			this.dtpBD = new System.Windows.Forms.DateTimePicker();
			this.dtpKT = new System.Windows.Forms.DateTimePicker();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// dtpBD
			// 
			this.dtpBD.Location = new System.Drawing.Point(57, 13);
			this.dtpBD.Name = "dtpBD";
			this.dtpBD.Size = new System.Drawing.Size(200, 20);
			this.dtpBD.TabIndex = 0;
			// 
			// dtpKT
			// 
			this.dtpKT.Location = new System.Drawing.Point(300, 13);
			this.dtpKT.Name = "dtpKT";
			this.dtpKT.Size = new System.Drawing.Size(200, 20);
			this.dtpKT.TabIndex = 0;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(57, 80);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(443, 390);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(57, 40);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "test";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// testForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(592, 505);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.dtpKT);
			this.Controls.Add(this.dtpBD);
			this.Name = "testForm";
			this.Text = "testForm";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DateTimePicker dtpBD;
		private System.Windows.Forms.DateTimePicker dtpKT;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button button1;
	}
}