namespace ChamCong_v05.UI.ChamCong {
	partial class frm_DiemDanhNV {
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
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("px thành phẩm");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("bảo vệ");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("văn phòng", new System.Windows.Forms.TreeNode[] {
            treeNode6});
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Nhà máy thuốc lá khánh hội", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode7});
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dtpNgay = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnThoat = new System.Windows.Forms.Button();
			this.btnXoaKBVang = new System.Windows.Forms.Button();
			this.btnKBVangNhanh = new System.Windows.Forms.Button();
			this.btnXuatBB = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tbTongSoNV = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tbVangNghi = new System.Windows.Forms.TextBox();
			this.tbVangLyDo = new System.Windows.Forms.TextBox();
			this.tbSoNVVang = new System.Windows.Forms.TextBox();
			this.tbSoNVDaRaVe = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbSoNVDangLamViec = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dgrdTongHop = new System.Windows.Forms.DataGridView();
			this.g2colMaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colUserEnrollNumberTongHop = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.textBoxColumnOfDataGrid1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTimeStrInTongHop = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTimeStrOutTongHop = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colVao2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colRa2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colVao3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colRa3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colGhiChuTongHop = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ShiftID1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ShiftID2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ShiftID3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.saveFileDlgDiemDanh = new System.Windows.Forms.SaveFileDialog();
			this.toolTipHint = new System.Windows.Forms.ToolTip(this.components);
			this.panel1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdTongHop)).BeginInit();
			this.SuspendLayout();
			// 
			// dtpNgay
			// 
			this.dtpNgay.CustomFormat = "ddd d/M/yyyy";
			this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpNgay.Location = new System.Drawing.Point(82, 12);
			this.dtpNgay.Name = "dtpNgay";
			this.dtpNgay.Size = new System.Drawing.Size(131, 21);
			this.dtpNgay.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label1.Location = new System.Drawing.Point(10, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Chọn ngày";
			// 
			// treePhongBan
			// 
			this.treePhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treePhongBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.treePhongBan.Indent = 18;
			this.treePhongBan.ItemHeight = 20;
			this.treePhongBan.Location = new System.Drawing.Point(3, 17);
			this.treePhongBan.Name = "treePhongBan";
			treeNode5.Name = "Node2";
			treeNode5.Text = "px thành phẩm";
			treeNode6.Name = "Node5";
			treeNode6.Text = "bảo vệ";
			treeNode7.Name = "Node4";
			treeNode7.Text = "văn phòng";
			treeNode8.Name = "Node0";
			treeNode8.Text = "Nhà máy thuốc lá khánh hội";
			this.treePhongBan.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8});
			this.treePhongBan.ShowNodeToolTips = true;
			this.treePhongBan.Size = new System.Drawing.Size(204, 185);
			this.treePhongBan.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnThoat);
			this.panel1.Controls.Add(this.btnXoaKBVang);
			this.panel1.Controls.Add(this.btnKBVangNhanh);
			this.panel1.Controls.Add(this.btnXuatBB);
			this.panel1.Controls.Add(this.groupBox3);
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.dtpNgay);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.panel1.Size = new System.Drawing.Size(226, 552);
			this.panel1.TabIndex = 4;
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnThoat.ForeColor = System.Drawing.Color.Blue;
			this.btnThoat.Location = new System.Drawing.Point(9, 522);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(189, 27);
			this.btnThoat.TabIndex = 4;
			this.btnThoat.Text = "Đóng";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// btnXoaKBVang
			// 
			this.btnXoaKBVang.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnXoaKBVang.ForeColor = System.Drawing.Color.Blue;
			this.btnXoaKBVang.Location = new System.Drawing.Point(9, 456);
			this.btnXoaKBVang.Name = "btnXoaKBVang";
			this.btnXoaKBVang.Size = new System.Drawing.Size(189, 27);
			this.btnXoaKBVang.TabIndex = 2;
			this.btnXoaKBVang.Text = "Xoá Khai báo vắng";
			this.toolTipHint.SetToolTip(this.btnXoaKBVang, "Quét chọn các NV để xóa tất cả các\r\nkhai báo vắng trong ngày của từng \r\nNV được c" +
        "họn");
			this.btnXoaKBVang.UseVisualStyleBackColor = true;
			this.btnXoaKBVang.Click += new System.EventHandler(this.btnXoaKBVang_Click);
			// 
			// btnKBVangNhanh
			// 
			this.btnKBVangNhanh.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnKBVangNhanh.ForeColor = System.Drawing.Color.Blue;
			this.btnKBVangNhanh.Location = new System.Drawing.Point(9, 423);
			this.btnKBVangNhanh.Name = "btnKBVangNhanh";
			this.btnKBVangNhanh.Size = new System.Drawing.Size(189, 27);
			this.btnKBVangNhanh.TabIndex = 1;
			this.btnKBVangNhanh.Text = "Thêm Khai báo vắng";
			this.toolTipHint.SetToolTip(this.btnKBVangNhanh, "Quét chọn các NV để thêm khai báo vắng");
			this.btnKBVangNhanh.UseVisualStyleBackColor = true;
			this.btnKBVangNhanh.Click += new System.EventHandler(this.btnKBVangNhanh_Click);
			// 
			// btnXuatBB
			// 
			this.btnXuatBB.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.btnXuatBB.ForeColor = System.Drawing.Color.Blue;
			this.btnXuatBB.Location = new System.Drawing.Point(9, 489);
			this.btnXuatBB.Name = "btnXuatBB";
			this.btnXuatBB.Size = new System.Drawing.Size(189, 27);
			this.btnXuatBB.TabIndex = 3;
			this.btnXuatBB.Text = "Xuất báo biểu";
			this.btnXuatBB.UseVisualStyleBackColor = true;
			this.btnXuatBB.Click += new System.EventHandler(this.btnXuatBB_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.tbTongSoNV);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.tbVangNghi);
			this.groupBox3.Controls.Add(this.tbVangLyDo);
			this.groupBox3.Controls.Add(this.tbSoNVVang);
			this.groupBox3.Controls.Add(this.tbSoNVDaRaVe);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.tbSoNVDangLamViec);
			this.groupBox3.Location = new System.Drawing.Point(6, 251);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.groupBox3.Size = new System.Drawing.Size(207, 166);
			this.groupBox3.TabIndex = 9;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Thống kê";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label4.Location = new System.Drawing.Point(4, 29);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 15);
			this.label4.TabIndex = 1;
			this.label4.Text = "Tổng số NV";
			// 
			// tbTongSoNV
			// 
			this.tbTongSoNV.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.tbTongSoNV.Location = new System.Drawing.Point(130, 26);
			this.tbTongSoNV.Name = "tbTongSoNV";
			this.tbTongSoNV.ReadOnly = true;
			this.tbTongSoNV.Size = new System.Drawing.Size(70, 21);
			this.tbTongSoNV.TabIndex = 0;
			this.toolTipHint.SetToolTip(this.tbTongSoNV, "Tổng số NV trong phòng ban đang chọn");
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label7.Location = new System.Drawing.Point(4, 144);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(88, 15);
			this.label7.TabIndex = 1;
			this.label7.Text = "Chưa làm việc";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label6.Location = new System.Drawing.Point(4, 121);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(82, 15);
			this.label6.TabIndex = 1;
			this.label6.Text = "Vắng có lý do";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label5.Location = new System.Drawing.Point(4, 98);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(125, 15);
			this.label5.TabIndex = 1;
			this.label5.Text = "Tổng NV ko hiện diện";
			// 
			// tbVangNghi
			// 
			this.tbVangNghi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.tbVangNghi.Location = new System.Drawing.Point(130, 141);
			this.tbVangNghi.Name = "tbVangNghi";
			this.tbVangNghi.ReadOnly = true;
			this.tbVangNghi.Size = new System.Drawing.Size(70, 21);
			this.tbVangNghi.TabIndex = 0;
			this.toolTipHint.SetToolTip(this.tbVangNghi, "Số NV nghỉ vào ngày nghỉ hàng tuần\r\n(Chủ nhật hoặc ngày nghỉ theo tour)");
			// 
			// tbVangLyDo
			// 
			this.tbVangLyDo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.tbVangLyDo.Location = new System.Drawing.Point(130, 118);
			this.tbVangLyDo.Name = "tbVangLyDo";
			this.tbVangLyDo.ReadOnly = true;
			this.tbVangLyDo.Size = new System.Drawing.Size(70, 21);
			this.tbVangLyDo.TabIndex = 0;
			this.toolTipHint.SetToolTip(this.tbVangLyDo, "Số NV có khai báo vắng");
			// 
			// tbSoNVVang
			// 
			this.tbSoNVVang.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.tbSoNVVang.Location = new System.Drawing.Point(130, 95);
			this.tbSoNVVang.Name = "tbSoNVVang";
			this.tbSoNVVang.ReadOnly = true;
			this.tbSoNVVang.Size = new System.Drawing.Size(70, 21);
			this.tbSoNVVang.TabIndex = 0;
			this.toolTipHint.SetToolTip(this.tbSoNVVang, "Tổng số NV nghỉ và vắng có lý do");
			// 
			// tbSoNVDaRaVe
			// 
			this.tbSoNVDaRaVe.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.tbSoNVDaRaVe.Location = new System.Drawing.Point(130, 72);
			this.tbSoNVDaRaVe.Name = "tbSoNVDaRaVe";
			this.tbSoNVDaRaVe.ReadOnly = true;
			this.tbSoNVDaRaVe.Size = new System.Drawing.Size(70, 21);
			this.tbSoNVDaRaVe.TabIndex = 0;
			this.toolTipHint.SetToolTip(this.tbSoNVDaRaVe, "Số NV đã ra về");
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label3.Location = new System.Drawing.Point(4, 75);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(54, 15);
			this.label3.TabIndex = 1;
			this.label3.Text = "Đã ra về";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.label2.Location = new System.Drawing.Point(4, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(87, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Đang làm việc";
			// 
			// tbSoNVDangLamViec
			// 
			this.tbSoNVDangLamViec.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.tbSoNVDangLamViec.Location = new System.Drawing.Point(130, 49);
			this.tbSoNVDangLamViec.Name = "tbSoNVDangLamViec";
			this.tbSoNVDangLamViec.ReadOnly = true;
			this.tbSoNVDangLamViec.Size = new System.Drawing.Size(70, 21);
			this.tbSoNVDangLamViec.TabIndex = 0;
			this.toolTipHint.SetToolTip(this.tbSoNVDangLamViec, "Số NV đang làm việc");
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.treePhongBan);
			this.groupBox2.Location = new System.Drawing.Point(6, 40);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(210, 205);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Chọn phòng ban";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.groupBox1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(226, 0);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.panel2.Size = new System.Drawing.Size(790, 552);
			this.panel2.TabIndex = 5;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.dgrdTongHop);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 5);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.groupBox1.Size = new System.Drawing.Size(790, 547);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Chi tiết điểm danh";
			// 
			// dgrdTongHop
			// 
			this.dgrdTongHop.AllowUserToAddRows = false;
			this.dgrdTongHop.AllowUserToDeleteRows = false;
			this.dgrdTongHop.AllowUserToResizeRows = false;
			dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
			this.dgrdTongHop.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
			this.dgrdTongHop.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgrdTongHop.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dgrdTongHop.ColumnHeadersHeight = 24;
			this.dgrdTongHop.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.g2colMaNV,
            this.colUserEnrollNumberTongHop,
            this.textBoxColumnOfDataGrid1,
            this.colTimeStrInTongHop,
            this.colTimeStrOutTongHop,
            this.colVao2,
            this.colRa2,
            this.colVao3,
            this.colRa3,
            this.colGhiChuTongHop,
            this.colTrangThai,
            this.ShiftID1,
            this.ShiftID2,
            this.ShiftID3});
			this.dgrdTongHop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgrdTongHop.GridColor = System.Drawing.Color.LightGray;
			this.dgrdTongHop.Location = new System.Drawing.Point(3, 22);
			this.dgrdTongHop.Name = "dgrdTongHop";
			this.dgrdTongHop.RowHeadersVisible = false;
			this.dgrdTongHop.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgrdTongHop.Size = new System.Drawing.Size(784, 522);
			this.dgrdTongHop.TabIndex = 0;
			// 
			// g2colMaNV
			// 
			this.g2colMaNV.DataPropertyName = "UserFullCode";
			this.g2colMaNV.Frozen = true;
			this.g2colMaNV.HeaderText = "Mã NV";
			this.g2colMaNV.Name = "g2colMaNV";
			this.g2colMaNV.ReadOnly = true;
			this.g2colMaNV.ToolTipText = "Mã Nhân viên";
			this.g2colMaNV.Width = 55;
			// 
			// colUserEnrollNumberTongHop
			// 
			this.colUserEnrollNumberTongHop.DataPropertyName = "UserEnrollNumber";
			dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.colUserEnrollNumberTongHop.DefaultCellStyle = dataGridViewCellStyle10;
			this.colUserEnrollNumberTongHop.Frozen = true;
			this.colUserEnrollNumberTongHop.HeaderText = "Mã CC";
			this.colUserEnrollNumberTongHop.Name = "colUserEnrollNumberTongHop";
			this.colUserEnrollNumberTongHop.ReadOnly = true;
			this.colUserEnrollNumberTongHop.ToolTipText = "Mã chấm công";
			this.colUserEnrollNumberTongHop.Visible = false;
			this.colUserEnrollNumberTongHop.Width = 55;
			// 
			// textBoxColumnOfDataGrid1
			// 
			this.textBoxColumnOfDataGrid1.DataPropertyName = "UserFullName";
			this.textBoxColumnOfDataGrid1.Frozen = true;
			this.textBoxColumnOfDataGrid1.HeaderText = "Tên NV";
			this.textBoxColumnOfDataGrid1.Name = "textBoxColumnOfDataGrid1";
			this.textBoxColumnOfDataGrid1.ReadOnly = true;
			this.textBoxColumnOfDataGrid1.Width = 150;
			// 
			// colTimeStrInTongHop
			// 
			this.colTimeStrInTongHop.DataPropertyName = "TimeStrVao1";
			dataGridViewCellStyle11.Format = "H:mm";
			this.colTimeStrInTongHop.DefaultCellStyle = dataGridViewCellStyle11;
			this.colTimeStrInTongHop.Frozen = true;
			this.colTimeStrInTongHop.HeaderText = "Vào1";
			this.colTimeStrInTongHop.Name = "colTimeStrInTongHop";
			this.colTimeStrInTongHop.ReadOnly = true;
			this.colTimeStrInTongHop.Width = 45;
			// 
			// colTimeStrOutTongHop
			// 
			this.colTimeStrOutTongHop.DataPropertyName = "TimeStrRaa1";
			dataGridViewCellStyle12.Format = "H:mm";
			this.colTimeStrOutTongHop.DefaultCellStyle = dataGridViewCellStyle12;
			this.colTimeStrOutTongHop.Frozen = true;
			this.colTimeStrOutTongHop.HeaderText = "Ra1";
			this.colTimeStrOutTongHop.Name = "colTimeStrOutTongHop";
			this.colTimeStrOutTongHop.ReadOnly = true;
			this.colTimeStrOutTongHop.Width = 45;
			// 
			// colVao2
			// 
			this.colVao2.DataPropertyName = "TimeStrVao2";
			dataGridViewCellStyle13.Format = "H:mm";
			this.colVao2.DefaultCellStyle = dataGridViewCellStyle13;
			this.colVao2.Frozen = true;
			this.colVao2.HeaderText = "Vào2";
			this.colVao2.Name = "colVao2";
			this.colVao2.ReadOnly = true;
			this.colVao2.Width = 45;
			// 
			// colRa2
			// 
			this.colRa2.DataPropertyName = "TimeStrRaa2";
			dataGridViewCellStyle14.Format = "H:mm";
			this.colRa2.DefaultCellStyle = dataGridViewCellStyle14;
			this.colRa2.Frozen = true;
			this.colRa2.HeaderText = "Ra2";
			this.colRa2.Name = "colRa2";
			this.colRa2.ReadOnly = true;
			this.colRa2.Width = 45;
			// 
			// colVao3
			// 
			this.colVao3.DataPropertyName = "TimeStrVao3";
			dataGridViewCellStyle15.Format = "H:mm";
			this.colVao3.DefaultCellStyle = dataGridViewCellStyle15;
			this.colVao3.Frozen = true;
			this.colVao3.HeaderText = "Vào3";
			this.colVao3.Name = "colVao3";
			this.colVao3.ReadOnly = true;
			this.colVao3.Width = 45;
			// 
			// colRa3
			// 
			this.colRa3.DataPropertyName = "TimeStrRaa3";
			dataGridViewCellStyle16.Format = "H:mm";
			this.colRa3.DefaultCellStyle = dataGridViewCellStyle16;
			this.colRa3.Frozen = true;
			this.colRa3.HeaderText = "Ra3";
			this.colRa3.Name = "colRa3";
			this.colRa3.ReadOnly = true;
			this.colRa3.Width = 45;
			// 
			// colGhiChuTongHop
			// 
			this.colGhiChuTongHop.DataPropertyName = "Ca";
			this.colGhiChuTongHop.Frozen = true;
			this.colGhiChuTongHop.HeaderText = "Ca";
			this.colGhiChuTongHop.Name = "colGhiChuTongHop";
			this.colGhiChuTongHop.ReadOnly = true;
			this.colGhiChuTongHop.Width = 160;
			// 
			// colTrangThai
			// 
			this.colTrangThai.DataPropertyName = "TrangThai";
			this.colTrangThai.Frozen = true;
			this.colTrangThai.HeaderText = "Trạng thái";
			this.colTrangThai.Name = "colTrangThai";
			this.colTrangThai.ReadOnly = true;
			this.colTrangThai.Width = 150;
			// 
			// ShiftID1
			// 
			this.ShiftID1.DataPropertyName = "ShiftID1";
			this.ShiftID1.HeaderText = "ShiftID1_hide";
			this.ShiftID1.Name = "ShiftID1";
			this.ShiftID1.ReadOnly = true;
			this.ShiftID1.Visible = false;
			// 
			// ShiftID2
			// 
			this.ShiftID2.DataPropertyName = "ShiftID2";
			this.ShiftID2.HeaderText = "ShiftID2_hide";
			this.ShiftID2.Name = "ShiftID2";
			this.ShiftID2.ReadOnly = true;
			this.ShiftID2.Visible = false;
			// 
			// ShiftID3
			// 
			this.ShiftID3.DataPropertyName = "ShiftID3";
			this.ShiftID3.HeaderText = "ShiftID3_hide";
			this.ShiftID3.Name = "ShiftID3";
			this.ShiftID3.ReadOnly = true;
			this.ShiftID3.Visible = false;
			// 
			// saveFileDlgDiemDanh
			// 
			this.saveFileDlgDiemDanh.CreatePrompt = true;
			this.saveFileDlgDiemDanh.DefaultExt = "xlsx";
			this.saveFileDlgDiemDanh.Filter = "Excel files|*.xlsx";
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
			// frm_DiemDanhNV
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1016, 552);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(163)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frm_DiemDanhNV";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Điểm danh nhân viên";
			this.Activated += new System.EventHandler(this.frm_DiemDanhNV_Activated);
			this.Load += new System.EventHandler(this.frm_DiemDanhNV_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrdTongHop)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TreeView treePhongBan;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgrdTongHop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSoNVDaRaVe;
        private System.Windows.Forms.TextBox tbSoNVDangLamViec;
        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbTongSoNV;
        private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbSoNVVang;
        private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnXuatBB;
		private System.Windows.Forms.SaveFileDialog saveFileDlgDiemDanh;
		private System.Windows.Forms.Button btnThoat;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbVangNghi;
		private System.Windows.Forms.TextBox tbVangLyDo;
		private System.Windows.Forms.Button btnKBVangNhanh;
		private System.Windows.Forms.Button btnXoaKBVang;
		private System.Windows.Forms.DataGridViewTextBoxColumn g2colMaNV;
		private System.Windows.Forms.DataGridViewTextBoxColumn colUserEnrollNumberTongHop;
		private System.Windows.Forms.DataGridViewTextBoxColumn textBoxColumnOfDataGrid1;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTimeStrInTongHop;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTimeStrOutTongHop;
		private System.Windows.Forms.DataGridViewTextBoxColumn colVao2;
		private System.Windows.Forms.DataGridViewTextBoxColumn colRa2;
		private System.Windows.Forms.DataGridViewTextBoxColumn colVao3;
		private System.Windows.Forms.DataGridViewTextBoxColumn colRa3;
		private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChuTongHop;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
		private System.Windows.Forms.DataGridViewTextBoxColumn ShiftID1;
		private System.Windows.Forms.DataGridViewTextBoxColumn ShiftID2;
		private System.Windows.Forms.DataGridViewTextBoxColumn ShiftID3;
		private System.Windows.Forms.ToolTip toolTipHint;
	}
}