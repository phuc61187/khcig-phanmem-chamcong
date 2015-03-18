namespace ChamCong_v03 {
	partial class frm_52_PhanQuyen {
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node2");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node3");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node5");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node6");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node7");
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node4", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7});
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node9");
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Node10");
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Node8", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
			this.lstTaikhoan = new System.Windows.Forms.ListBox();
			this.checkQuyenThaotac = new System.Windows.Forms.CheckedListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.btnThoat = new System.Windows.Forms.Button();
			this.treePhongban = new System.Windows.Forms.TreeView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstTaikhoan
			// 
			this.lstTaikhoan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstTaikhoan.FormattingEnabled = true;
			this.lstTaikhoan.ItemHeight = 15;
			this.lstTaikhoan.Location = new System.Drawing.Point(3, 22);
			this.lstTaikhoan.Name = "lstTaikhoan";
			this.lstTaikhoan.Size = new System.Drawing.Size(170, 203);
			this.lstTaikhoan.TabIndex = 0;
			this.lstTaikhoan.SelectedValueChanged += new System.EventHandler(this.lstTaikhoan_SelectedValueChanged);
			// 
			// checkQuyenThaotac
			// 
			this.checkQuyenThaotac.CheckOnClick = true;
			this.checkQuyenThaotac.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkQuyenThaotac.FormattingEnabled = true;
			this.checkQuyenThaotac.HorizontalExtent = 5;
			this.checkQuyenThaotac.Location = new System.Drawing.Point(3, 22);
			this.checkQuyenThaotac.Name = "checkQuyenThaotac";
			this.checkQuyenThaotac.Size = new System.Drawing.Size(276, 206);
			this.checkQuyenThaotac.TabIndex = 0;
			this.checkQuyenThaotac.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkQuyenThaotac_ItemCheck);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.button1.ForeColor = System.Drawing.Color.Blue;
			this.button1.Location = new System.Drawing.Point(721, 33);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 25);
			this.button1.TabIndex = 0;
			this.button1.Text = "Cập nhật";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.btnCapNhat_Click);
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(721, 217);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(75, 25);
			this.btnThoat.TabIndex = 1;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// treePhongban
			// 
			this.treePhongban.CheckBoxes = true;
			this.treePhongban.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treePhongban.Location = new System.Drawing.Point(3, 22);
			this.treePhongban.Name = "treePhongban";
			treeNode1.Name = "Node1";
			treeNode1.Text = "Node1";
			treeNode2.Name = "Node2";
			treeNode2.Text = "Node2";
			treeNode3.Name = "Node3";
			treeNode3.Text = "Node3";
			treeNode4.Name = "Node0";
			treeNode4.Text = "Node0";
			treeNode5.Name = "Node5";
			treeNode5.Text = "Node5";
			treeNode6.Name = "Node6";
			treeNode6.Text = "Node6";
			treeNode7.Name = "Node7";
			treeNode7.Text = "Node7";
			treeNode8.Name = "Node4";
			treeNode8.Text = "Node4";
			treeNode9.Name = "Node9";
			treeNode9.Text = "Node9";
			treeNode10.Name = "Node10";
			treeNode10.Text = "Node10";
			treeNode11.Name = "Node8";
			treeNode11.Text = "Node8";
			this.treePhongban.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode8,
            treeNode11});
			this.treePhongban.Size = new System.Drawing.Size(230, 206);
			this.treePhongban.TabIndex = 0;
			this.treePhongban.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treePhongban_AfterCheck);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lstTaikhoan);
			this.groupBox1.Location = new System.Drawing.Point(12, 11);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.groupBox1.Size = new System.Drawing.Size(176, 228);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Tài khoản";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.treePhongban);
			this.groupBox2.Location = new System.Drawing.Point(194, 11);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.groupBox2.Size = new System.Drawing.Size(236, 231);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Danh sách phòng ban";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.checkQuyenThaotac);
			this.groupBox3.Location = new System.Drawing.Point(433, 11);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.groupBox3.Size = new System.Drawing.Size(282, 231);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Danh sách chức năng";
			// 
			// frm_52_PhanQuyen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(808, 253);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.button1);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "frm_52_PhanQuyen";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Phân quyền thao tác";
			this.Load += new System.EventHandler(this.frm_PhanQuyen_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox lstTaikhoan;
		private System.Windows.Forms.CheckedListBox checkQuyenThaotac;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.TreeView treePhongban;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
	}
}