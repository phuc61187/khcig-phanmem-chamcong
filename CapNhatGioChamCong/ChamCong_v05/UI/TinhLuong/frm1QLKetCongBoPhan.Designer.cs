namespace ChamCong_v05.UI.TinhLuong {
	partial class frm_QLKetCongBoPhan {
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("px thành phẩm");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("bảo vệ");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("văn phòng", new System.Windows.Forms.TreeNode[] {
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Nhà máy thuốc lá khánh hội", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3});
			this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.label5 = new System.Windows.Forms.Label();
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.btnThucHien = new System.Windows.Forms.Button();
			this.btnDong = new System.Windows.Forms.Button();
			this.gpChonPhongBan.SuspendLayout();
			this.SuspendLayout();
			// 
			// gpChonPhongBan
			// 
			this.gpChonPhongBan.Controls.Add(this.treePhongBan);
			this.gpChonPhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.gpChonPhongBan.Location = new System.Drawing.Point(12, 39);
			this.gpChonPhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gpChonPhongBan.Name = "gpChonPhongBan";
			this.gpChonPhongBan.Padding = new System.Windows.Forms.Padding(3, 6, 3, 2);
			this.gpChonPhongBan.Size = new System.Drawing.Size(233, 225);
			this.gpChonPhongBan.TabIndex = 1;
			this.gpChonPhongBan.TabStop = false;
			this.gpChonPhongBan.Text = "Danh sách phòng đã kết công tháng";
			// 
			// treePhongBan
			// 
			this.treePhongBan.CheckBoxes = true;
			this.treePhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treePhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.treePhongBan.Indent = 18;
			this.treePhongBan.ItemHeight = 20;
			this.treePhongBan.Location = new System.Drawing.Point(3, 20);
			this.treePhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.treePhongBan.Name = "treePhongBan";
			treeNode1.Name = "Node2";
			treeNode1.Text = "px thành phẩm";
			treeNode2.Name = "Node5";
			treeNode2.Text = "bảo vệ";
			treeNode3.Name = "Node4";
			treeNode3.Text = "văn phòng";
			treeNode4.Name = "Node0";
			treeNode4.Text = "Nhà máy thuốc lá khánh hội";
			this.treePhongBan.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
			this.treePhongBan.ShowNodeToolTips = true;
			this.treePhongBan.Size = new System.Drawing.Size(227, 203);
			this.treePhongBan.TabIndex = 0;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label5.Location = new System.Drawing.Point(12, 17);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(94, 15);
			this.label5.TabIndex = 2;
			this.label5.Text = "Tháng kết công";
			// 
			// dtpThang
			// 
			this.dtpThang.CustomFormat = "M / yyyy";
			this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpThang.Location = new System.Drawing.Point(150, 12);
			this.dtpThang.Name = "dtpThang";
			this.dtpThang.ShowUpDown = true;
			this.dtpThang.Size = new System.Drawing.Size(92, 21);
			this.dtpThang.TabIndex = 0;
			this.dtpThang.Value = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
			this.dtpThang.ValueChanged += new System.EventHandler(this.dtpThang_ValueChanged);
			// 
			// btnThucHien
			// 
			this.btnThucHien.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThucHien.ForeColor = System.Drawing.Color.Blue;
			this.btnThucHien.Location = new System.Drawing.Point(15, 267);
			this.btnThucHien.Name = "btnThucHien";
			this.btnThucHien.Size = new System.Drawing.Size(76, 27);
			this.btnThucHien.TabIndex = 1;
			this.btnThucHien.Text = "Tiếp tục";
			this.btnThucHien.UseVisualStyleBackColor = true;
			this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
			// 
			// btnDong
			// 
			this.btnDong.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnDong.ForeColor = System.Drawing.Color.Blue;
			this.btnDong.Location = new System.Drawing.Point(169, 267);
			this.btnDong.Name = "btnDong";
			this.btnDong.Size = new System.Drawing.Size(76, 27);
			this.btnDong.TabIndex = 2;
			this.btnDong.Text = "Đóng";
			this.btnDong.UseVisualStyleBackColor = true;
			this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
			// 
			// frm_QLKetCongBoPhan
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(258, 308);
			this.ControlBox = false;
			this.Controls.Add(this.btnDong);
			this.Controls.Add(this.btnThucHien);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.dtpThang);
			this.Controls.Add(this.gpChonPhongBan);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.Name = "frm_QLKetCongBoPhan";
			this.Text = "Tình hình kết công các bộ phận";
			this.Load += new System.EventHandler(this.frm_KetCongBoPhan_Load);
			this.gpChonPhongBan.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox gpChonPhongBan;
		private System.Windows.Forms.TreeView treePhongBan;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DateTimePicker dtpThang;
		private System.Windows.Forms.Button btnThucHien;
		private System.Windows.Forms.Button btnDong;
	}
}