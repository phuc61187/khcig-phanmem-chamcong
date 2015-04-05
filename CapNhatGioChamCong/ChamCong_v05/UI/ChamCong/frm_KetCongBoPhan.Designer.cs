namespace ChamCong_v05.UI.ChamCong {
	partial class frm_KetCongBoPhan {
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("px thành phẩm");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("bảo vệ");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("văn phòng", new System.Windows.Forms.TreeNode[] {
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Nhà máy thuốc lá khánh hội", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3});
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_KetCongBoPhan));
			this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.label5 = new System.Windows.Forms.Label();
			this.dtpThang = new System.Windows.Forms.DateTimePicker();
			this.btnThucHien = new System.Windows.Forms.Button();
			this.btnDong = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.tbTenNVLapbieu = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbTenTrgBP = new System.Windows.Forms.TextBox();
			this.toolTipHint = new System.Windows.Forms.ToolTip(this.components);
			this.gpChonPhongBan.SuspendLayout();
			this.SuspendLayout();
			// 
			// gpChonPhongBan
			// 
			this.gpChonPhongBan.Controls.Add(this.treePhongBan);
			this.gpChonPhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.gpChonPhongBan.Location = new System.Drawing.Point(12, 104);
			this.gpChonPhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gpChonPhongBan.Name = "gpChonPhongBan";
			this.gpChonPhongBan.Padding = new System.Windows.Forms.Padding(3, 6, 3, 2);
			this.gpChonPhongBan.Size = new System.Drawing.Size(282, 225);
			this.gpChonPhongBan.TabIndex = 1;
			this.gpChonPhongBan.TabStop = false;
			this.gpChonPhongBan.Text = "Chọn phòng ban kết công tháng";
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
			this.treePhongBan.Size = new System.Drawing.Size(276, 203);
			this.treePhongBan.TabIndex = 0;
			this.treePhongBan.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treePhongBan_AfterCheck);
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
			this.dtpThang.Location = new System.Drawing.Point(112, 12);
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
			this.btnThucHien.Location = new System.Drawing.Point(12, 334);
			this.btnThucHien.Name = "btnThucHien";
			this.btnThucHien.Size = new System.Drawing.Size(76, 27);
			this.btnThucHien.TabIndex = 3;
			this.btnThucHien.Text = "Thực hiện";
			this.toolTipHint.SetToolTip(this.btnThucHien, resources.GetString("btnThucHien.ToolTip"));
			this.btnThucHien.UseVisualStyleBackColor = true;
			this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
			// 
			// btnDong
			// 
			this.btnDong.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnDong.ForeColor = System.Drawing.Color.Blue;
			this.btnDong.Location = new System.Drawing.Point(215, 334);
			this.btnDong.Name = "btnDong";
			this.btnDong.Size = new System.Drawing.Size(76, 27);
			this.btnDong.TabIndex = 4;
			this.btnDong.Text = "Đóng";
			this.btnDong.UseVisualStyleBackColor = true;
			this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.CreatePrompt = true;
			this.saveFileDialog.DefaultExt = "xlsx";
			this.saveFileDialog.Filter = "Excel files|*.xlsx";
			// 
			// tbTenNVLapbieu
			// 
			this.tbTenNVLapbieu.Location = new System.Drawing.Point(112, 39);
			this.tbTenNVLapbieu.Name = "tbTenNVLapbieu";
			this.tbTenNVLapbieu.Size = new System.Drawing.Size(182, 21);
			this.tbTenNVLapbieu.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label1.Location = new System.Drawing.Point(12, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 15);
			this.label1.TabIndex = 45;
			this.label1.Text = "NV lập biểu";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label2.Location = new System.Drawing.Point(12, 69);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98, 15);
			this.label2.TabIndex = 45;
			this.label2.Text = "Trưởng bộ phận";
			// 
			// tbTenTrgBP
			// 
			this.tbTenTrgBP.Location = new System.Drawing.Point(112, 66);
			this.tbTenTrgBP.Name = "tbTenTrgBP";
			this.tbTenTrgBP.Size = new System.Drawing.Size(182, 21);
			this.tbTenTrgBP.TabIndex = 2;
			// 
			// toolTipHint
			// 
			this.toolTipHint.AutoPopDelay = 30000;
			this.toolTipHint.InitialDelay = 300;
			this.toolTipHint.OwnerDraw = true;
			this.toolTipHint.ReshowDelay = 100;
			this.toolTipHint.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipHint_Draw);
			this.toolTipHint.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTipHint_Popup);
			// 
			// frm_KetCongBoPhan
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(298, 373);
			this.ControlBox = false;
			this.Controls.Add(this.tbTenTrgBP);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbTenNVLapbieu);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnDong);
			this.Controls.Add(this.btnThucHien);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.dtpThang);
			this.Controls.Add(this.gpChonPhongBan);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.Name = "frm_KetCongBoPhan";
			this.Text = "Kết công tháng cho nhân viên";
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
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.TextBox tbTenNVLapbieu;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbTenTrgBP;
		private System.Windows.Forms.ToolTip toolTipHint;
	}
}