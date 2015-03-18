namespace ChamCong_v04.UI {
	partial class frmWarning {
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
			this.dgrdCanhBao = new System.Windows.Forms.DataGridView();
			this.g1c1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.g1c2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.btnXuatExcel = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.dgrdCanhBao)).BeginInit();
			this.SuspendLayout();
			// 
			// dgrdCanhBao
			// 
			this.dgrdCanhBao.AllowUserToAddRows = false;
			this.dgrdCanhBao.AllowUserToDeleteRows = false;
			this.dgrdCanhBao.ColumnHeadersHeight = 27;
			this.dgrdCanhBao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgrdCanhBao.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.g1c1,
            this.g1c2});
			this.dgrdCanhBao.Location = new System.Drawing.Point(12, 28);
			this.dgrdCanhBao.Name = "dgrdCanhBao";
			this.dgrdCanhBao.RowHeadersVisible = false;
			this.dgrdCanhBao.ShowEditingIcon = false;
			this.dgrdCanhBao.Size = new System.Drawing.Size(576, 165);
			this.dgrdCanhBao.TabIndex = 0;
			// 
			// g1c1
			// 
			this.g1c1.DataPropertyName = "CanhBao";
			this.g1c1.HeaderText = "Cảnh báo";
			this.g1c1.Name = "g1c1";
			this.g1c1.ReadOnly = true;
			this.g1c1.Width = 200;
			// 
			// g1c2
			// 
			this.g1c2.DataPropertyName = "NoiDungCanhBao";
			this.g1c2.HeaderText = "Nội dung cảnh báo";
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
			this.label1.Size = new System.Drawing.Size(257, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Các cảnh báo trong quá trình thực hiện";
			// 
			// btnXuatExcel
			// 
			this.btnXuatExcel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnXuatExcel.ForeColor = System.Drawing.Color.Blue;
			this.btnXuatExcel.Location = new System.Drawing.Point(236, 199);
			this.btnXuatExcel.Name = "btnXuatExcel";
			this.btnXuatExcel.Size = new System.Drawing.Size(173, 27);
			this.btnXuatExcel.TabIndex = 1;
			this.btnXuatExcel.Text = "Tiếp tục thực hiện thao tác";
			this.btnXuatExcel.UseVisualStyleBackColor = true;
			this.btnXuatExcel.Click += new System.EventHandler(this.btnTiepTuc_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(415, 199);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(173, 27);
			this.btnThoat.TabIndex = 2;
			this.btnThoat.Text = "Dừng thực hiện thao tác";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.CreatePrompt = true;
			this.saveFileDialog.DefaultExt = "xlsx";
			// 
			// frmWarning
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(600, 238);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnXuatExcel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dgrdCanhBao);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.MinimizeBox = false;
			this.Name = "frmWarning";
			this.Text = "Danh sách cảnh báo";
			this.Load += new System.EventHandler(this.frmWarning_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdCanhBao)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgrdCanhBao;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnXuatExcel;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c1;
		private System.Windows.Forms.DataGridViewTextBoxColumn g1c2;
	}
}