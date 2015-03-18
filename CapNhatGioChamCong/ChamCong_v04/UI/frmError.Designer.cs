namespace ChamCong_v04.UI {
	partial class frmError {
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
			this.dgrdLoi = new System.Windows.Forms.DataGridView();
			this.g1c1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.btnThoat = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.dgrdLoi)).BeginInit();
			this.SuspendLayout();
			// 
			// dgrdLoi
			// 
			this.dgrdLoi.AllowUserToAddRows = false;
			this.dgrdLoi.AllowUserToDeleteRows = false;
			this.dgrdLoi.ColumnHeadersHeight = 27;
			this.dgrdLoi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgrdLoi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.g1c1,
            this.g1c2});
			this.dgrdLoi.Location = new System.Drawing.Point(12, 28);
			this.dgrdLoi.Name = "dgrdLoi";
			this.dgrdLoi.RowHeadersVisible = false;
			this.dgrdLoi.ShowEditingIcon = false;
			this.dgrdLoi.Size = new System.Drawing.Size(576, 165);
			this.dgrdLoi.TabIndex = 0;
			// 
			// g1c1
			// 
			this.g1c1.DataPropertyName = "Loi";
			this.g1c1.HeaderText = "Lỗi";
			this.g1c1.Name = "g1c1";
			this.g1c1.ReadOnly = true;
			this.g1c1.Width = 200;
			// 
			// g1c2
			// 
			this.g1c2.DataPropertyName = "NoiDungLoi";
			this.g1c2.HeaderText = "Nội dung lỗi";
			this.g1c2.Name = "g1c2";
			this.g1c2.ReadOnly = true;
			this.g1c2.Width = 350;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(306, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Trong quá trình thực hiện đã xảy ra các lỗi sau:";
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(513, 199);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(75, 27);
			this.btnThoat.TabIndex = 2;
			this.btnThoat.Text = "Đóng";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.CreatePrompt = true;
			this.saveFileDialog.DefaultExt = "xlsx";
			// 
			// frmError
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(600, 238);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dgrdLoi);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.MinimizeBox = false;
			this.Name = "frmError";
			this.Text = "Lỗi trong quá trình thực hiện";
			this.Load += new System.EventHandler(this.frmError_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdLoi)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgrdLoi;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c1;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c2;
	}
}