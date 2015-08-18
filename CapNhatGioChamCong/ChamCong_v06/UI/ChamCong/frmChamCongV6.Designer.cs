namespace ChamCong_v06.UI.ChamCong {
	partial class frmChamCongV6 {
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.panel1 = new System.Windows.Forms.Panel();
			this.gpChonPhongBan = new System.Windows.Forms.GroupBox();
			this.treePhongBan = new System.Windows.Forms.TreeView();
			this.checkedDSNV = new DevExpress.XtraEditors.CheckedComboBoxEdit();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dateNavigator1 = new DevExpress.XtraScheduler.DateNavigator();
			this.label3 = new System.Windows.Forms.Label();
			this.btnChamCong = new DevExpress.XtraEditors.SimpleButton();
			this.lbThieuChamCong = new DevExpress.XtraEditors.LabelControl();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lbVaoTreRaSom = new DevExpress.XtraEditors.LabelControl();
			this.lbOLaiChuaXN = new DevExpress.XtraEditors.LabelControl();
			this.lbKhongNhanDienCa = new DevExpress.XtraEditors.LabelControl();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lbXinPhepVang = new DevExpress.XtraEditors.LabelControl();
			this.lbTreSomCoLamBu = new DevExpress.XtraEditors.LabelControl();
			this.lbChoPhepTreSom = new DevExpress.XtraEditors.LabelControl();
			this.lbDaXacNhanLamThem = new DevExpress.XtraEditors.LabelControl();
			this.lbVaoRaBiChinhSua = new DevExpress.XtraEditors.LabelControl();
			this.lbDaXacNhanCa = new DevExpress.XtraEditors.LabelControl();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
			this.panel1.SuspendLayout();
			this.gpChonPhongBan.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkedDSNV.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateNavigator1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// repositoryItemCheckEdit1
			// 
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.gpChonPhongBan);
			this.panel1.Font = new System.Drawing.Font("Tahoma", 10F);
			this.panel1.Location = new System.Drawing.Point(1, 1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(229, 196);
			this.panel1.TabIndex = 0;
			// 
			// gpChonPhongBan
			// 
			this.gpChonPhongBan.Controls.Add(this.treePhongBan);
			this.gpChonPhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gpChonPhongBan.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.gpChonPhongBan.Location = new System.Drawing.Point(0, 0);
			this.gpChonPhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gpChonPhongBan.Name = "gpChonPhongBan";
			this.gpChonPhongBan.Padding = new System.Windows.Forms.Padding(3, 6, 3, 2);
			this.gpChonPhongBan.Size = new System.Drawing.Size(229, 196);
			this.gpChonPhongBan.TabIndex = 1;
			this.gpChonPhongBan.TabStop = false;
			this.gpChonPhongBan.Text = "Chọn phòng ban";
			// 
			// treePhongBan
			// 
			this.treePhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treePhongBan.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.treePhongBan.Indent = 18;
			this.treePhongBan.ItemHeight = 20;
			this.treePhongBan.Location = new System.Drawing.Point(3, 21);
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
			this.treePhongBan.Size = new System.Drawing.Size(223, 173);
			this.treePhongBan.TabIndex = 0;
			// 
			// checkedDSNV
			// 
			this.checkedDSNV.Location = new System.Drawing.Point(236, 19);
			this.checkedDSNV.Name = "checkedDSNV";
			this.checkedDSNV.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.checkedDSNV.Properties.Appearance.Options.UseFont = true;
			this.checkedDSNV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear, "", 20, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.checkedDSNV.Size = new System.Drawing.Size(189, 20);
			this.checkedDSNV.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label1.Location = new System.Drawing.Point(236, 1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(103, 14);
			this.label1.TabIndex = 2;
			this.label1.Text = "Chọn nhân viên";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label2.Location = new System.Drawing.Point(236, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 14);
			this.label2.TabIndex = 2;
			this.label2.Text = "Chọn tháng";
			// 
			// dateNavigator1
			// 
			this.dateNavigator1.DateTime = new System.DateTime(2015, 4, 3, 0, 0, 0, 0);
			this.dateNavigator1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.dateNavigator1.HotDate = null;
			this.dateNavigator1.Location = new System.Drawing.Point(236, 61);
			this.dateNavigator1.Name = "dateNavigator1";
			this.dateNavigator1.ShowTodayButton = false;
			this.dateNavigator1.ShowWeekNumbers = false;
			this.dateNavigator1.Size = new System.Drawing.Size(189, 136);
			this.dateNavigator1.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.label3.Location = new System.Drawing.Point(1, 200);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(149, 14);
			this.label3.TabIndex = 2;
			this.label3.Text = "Bảng chấm công tháng";
			// 
			// btnChamCong
			// 
			this.btnChamCong.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnChamCong.Appearance.Options.UseFont = true;
			this.btnChamCong.Location = new System.Drawing.Point(431, 5);
			this.btnChamCong.Name = "btnChamCong";
			this.btnChamCong.Size = new System.Drawing.Size(103, 46);
			this.btnChamCong.TabIndex = 28;
			this.btnChamCong.Text = "Chấm công";
			this.btnChamCong.Click += new System.EventHandler(this.btnChamCong_Click);
			// 
			// lbThieuChamCong
			// 
			this.lbThieuChamCong.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lbThieuChamCong.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.lbThieuChamCong.LineColor = System.Drawing.Color.Transparent;
			this.lbThieuChamCong.Location = new System.Drawing.Point(6, 24);
			this.lbThieuChamCong.Margin = new System.Windows.Forms.Padding(5);
			this.lbThieuChamCong.Name = "lbThieuChamCong";
			this.lbThieuChamCong.Size = new System.Drawing.Size(149, 14);
			this.lbThieuChamCong.TabIndex = 30;
			this.lbThieuChamCong.Text = "Thiếu chấm công: [0] TH";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lbVaoTreRaSom);
			this.groupBox1.Controls.Add(this.lbOLaiChuaXN);
			this.groupBox1.Controls.Add(this.lbKhongNhanDienCa);
			this.groupBox1.Controls.Add(this.lbThieuChamCong);
			this.groupBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.groupBox1.Location = new System.Drawing.Point(801, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(268, 185);
			this.groupBox1.TabIndex = 31;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Các trường hợp chưa xử lý";
			// 
			// lbVaoTreRaSom
			// 
			this.lbVaoTreRaSom.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lbVaoTreRaSom.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.lbVaoTreRaSom.Location = new System.Drawing.Point(6, 96);
			this.lbVaoTreRaSom.Margin = new System.Windows.Forms.Padding(5);
			this.lbVaoTreRaSom.Name = "lbVaoTreRaSom";
			this.lbVaoTreRaSom.Size = new System.Drawing.Size(140, 14);
			this.lbVaoTreRaSom.TabIndex = 30;
			this.lbVaoTreRaSom.Text = "Vào trễ, ra sớm: [0] TH";
			// 
			// lbOLaiChuaXN
			// 
			this.lbOLaiChuaXN.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lbOLaiChuaXN.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.lbOLaiChuaXN.Location = new System.Drawing.Point(6, 72);
			this.lbOLaiChuaXN.Margin = new System.Windows.Forms.Padding(5);
			this.lbOLaiChuaXN.Name = "lbOLaiChuaXN";
			this.lbOLaiChuaXN.Size = new System.Drawing.Size(193, 14);
			this.lbOLaiChuaXN.TabIndex = 30;
			this.lbOLaiChuaXN.Text = "Ở lại chưa qua xác nhận: [0] TH";
			// 
			// lbKhongNhanDienCa
			// 
			this.lbKhongNhanDienCa.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lbKhongNhanDienCa.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.lbKhongNhanDienCa.Location = new System.Drawing.Point(6, 48);
			this.lbKhongNhanDienCa.Margin = new System.Windows.Forms.Padding(5);
			this.lbKhongNhanDienCa.Name = "lbKhongNhanDienCa";
			this.lbKhongNhanDienCa.Size = new System.Drawing.Size(203, 14);
			this.lbKhongNhanDienCa.TabIndex = 30;
			this.lbKhongNhanDienCa.Text = "Không nhận diện được ca: [0] TH";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lbXinPhepVang);
			this.groupBox2.Controls.Add(this.lbTreSomCoLamBu);
			this.groupBox2.Controls.Add(this.lbChoPhepTreSom);
			this.groupBox2.Controls.Add(this.lbDaXacNhanLamThem);
			this.groupBox2.Controls.Add(this.lbVaoRaBiChinhSua);
			this.groupBox2.Controls.Add(this.lbDaXacNhanCa);
			this.groupBox2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.groupBox2.Location = new System.Drawing.Point(1075, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(268, 185);
			this.groupBox2.TabIndex = 31;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Các trường hợp đã qua xử lý";
			// 
			// lbXinPhepVang
			// 
			this.lbXinPhepVang.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lbXinPhepVang.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.lbXinPhepVang.Location = new System.Drawing.Point(8, 144);
			this.lbXinPhepVang.Margin = new System.Windows.Forms.Padding(5);
			this.lbXinPhepVang.Name = "lbXinPhepVang";
			this.lbXinPhepVang.Size = new System.Drawing.Size(134, 14);
			this.lbXinPhepVang.TabIndex = 30;
			this.lbXinPhepVang.Text = "Xin phép vắng: [0] TH";
			// 
			// lbTreSomCoLamBu
			// 
			this.lbTreSomCoLamBu.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lbTreSomCoLamBu.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.lbTreSomCoLamBu.Location = new System.Drawing.Point(8, 120);
			this.lbTreSomCoLamBu.Margin = new System.Windows.Forms.Padding(5);
			this.lbTreSomCoLamBu.Name = "lbTreSomCoLamBu";
			this.lbTreSomCoLamBu.Size = new System.Drawing.Size(203, 14);
			this.lbTreSomCoLamBu.TabIndex = 30;
			this.lbTreSomCoLamBu.Text = "Vào trễ, ra sớm có làm bù: [0] TH";
			// 
			// lbChoPhepTreSom
			// 
			this.lbChoPhepTreSom.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lbChoPhepTreSom.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.lbChoPhepTreSom.Location = new System.Drawing.Point(8, 96);
			this.lbChoPhepTreSom.Margin = new System.Windows.Forms.Padding(5);
			this.lbChoPhepTreSom.Name = "lbChoPhepTreSom";
			this.lbChoPhepTreSom.Size = new System.Drawing.Size(202, 14);
			this.lbChoPhepTreSom.TabIndex = 30;
			this.lbChoPhepTreSom.Text = "Cho phép vào trễ, ra sớm: [0] TH";
			// 
			// lbDaXacNhanLamThem
			// 
			this.lbDaXacNhanLamThem.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lbDaXacNhanLamThem.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.lbDaXacNhanLamThem.Location = new System.Drawing.Point(8, 72);
			this.lbDaXacNhanLamThem.Margin = new System.Windows.Forms.Padding(5);
			this.lbDaXacNhanLamThem.Name = "lbDaXacNhanLamThem";
			this.lbDaXacNhanLamThem.Size = new System.Drawing.Size(205, 14);
			this.lbDaXacNhanLamThem.TabIndex = 30;
			this.lbDaXacNhanLamThem.Text = "Đã xác nhận làm thêm giờ: [0] TH";
			// 
			// lbVaoRaBiChinhSua
			// 
			this.lbVaoRaBiChinhSua.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lbVaoRaBiChinhSua.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.lbVaoRaBiChinhSua.Location = new System.Drawing.Point(8, 24);
			this.lbVaoRaBiChinhSua.Margin = new System.Windows.Forms.Padding(5);
			this.lbVaoRaBiChinhSua.Name = "lbVaoRaBiChinhSua";
			this.lbVaoRaBiChinhSua.Size = new System.Drawing.Size(218, 14);
			this.lbVaoRaBiChinhSua.TabIndex = 30;
			this.lbVaoRaBiChinhSua.Text = "Giờ vào hoặc ra bị chỉnh sửa: [0] TH";
			// 
			// lbDaXacNhanCa
			// 
			this.lbDaXacNhanCa.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lbDaXacNhanCa.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.lbDaXacNhanCa.Location = new System.Drawing.Point(8, 48);
			this.lbDaXacNhanCa.Margin = new System.Windows.Forms.Padding(5);
			this.lbDaXacNhanCa.Name = "lbDaXacNhanCa";
			this.lbDaXacNhanCa.Size = new System.Drawing.Size(138, 14);
			this.lbDaXacNhanCa.TabIndex = 30;
			this.lbDaXacNhanCa.Text = "Đã xác nhận ca: [0] TH";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.checkBox1.Location = new System.Drawing.Point(441, 71);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(85, 18);
			this.checkBox1.TabIndex = 32;
			this.checkBox1.Text = "checkBox1";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// gridControl1
			// 
			this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControl1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.gridControl1.Location = new System.Drawing.Point(1, 217);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.Size = new System.Drawing.Size(1342, 442);
			this.gridControl1.TabIndex = 33;
			this.gridControl1.ToolTipController = this.toolTipController1;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.Appearance.Empty.BackColor = System.Drawing.Color.Silver;
			this.gridView1.Appearance.Empty.Options.UseBackColor = true;
			this.gridView1.Appearance.EvenRow.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.EvenRow.Options.UseFont = true;
			this.gridView1.Appearance.FilterPanel.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.FilterPanel.Options.UseFont = true;
			this.gridView1.Appearance.FixedLine.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.FixedLine.Options.UseFont = true;
			this.gridView1.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.FocusedCell.Options.UseFont = true;
			this.gridView1.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
			this.gridView1.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.FooterPanel.Options.UseFont = true;
			this.gridView1.Appearance.GroupButton.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.GroupButton.Options.UseFont = true;
			this.gridView1.Appearance.GroupFooter.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.GroupFooter.Options.UseFont = true;
			this.gridView1.Appearance.GroupPanel.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.GroupPanel.Options.UseFont = true;
			this.gridView1.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.GroupRow.Options.UseFont = true;
			this.gridView1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Silver;
			this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.gridView1.Appearance.HeaderPanel.Options.UseBackColor = true;
			this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridView1.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.HideSelectionRow.Options.UseFont = true;
			this.gridView1.Appearance.OddRow.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.OddRow.Options.UseFont = true;
			this.gridView1.Appearance.Preview.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.Preview.Options.UseFont = true;
			this.gridView1.Appearance.Row.BackColor = System.Drawing.Color.Silver;
			this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.Row.Options.UseBackColor = true;
			this.gridView1.Appearance.Row.Options.UseFont = true;
			this.gridView1.Appearance.RowSeparator.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.RowSeparator.Options.UseFont = true;
			this.gridView1.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 9F);
			this.gridView1.Appearance.SelectedRow.Options.UseFont = true;
			this.gridView1.Appearance.TopNewRow.BackColor = System.Drawing.Color.Silver;
			this.gridView1.Appearance.TopNewRow.Options.UseBackColor = true;
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn14,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn15});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsView.AllowCellMerge = true;
			this.gridView1.OptionsView.ColumnAutoWidth = false;
			// 
			// gridColumn14
			// 
			this.gridColumn14.Caption = "MãCC_hide";
			this.gridColumn14.FieldName = "UserEnrollNumber";
			this.gridColumn14.Name = "gridColumn14";
			// 
			// gridColumn1
			// 
			this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gridColumn1.AppearanceCell.Options.UseFont = true;
			this.gridColumn1.Caption = "Mã NV";
			this.gridColumn1.FieldName = "UserFullCode";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
			this.gridColumn1.Width = 60;
			// 
			// gridColumn2
			// 
			this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gridColumn2.AppearanceCell.Options.UseFont = true;
			this.gridColumn2.Caption = "Tên NV";
			this.gridColumn2.FieldName = "UserFullName";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			this.gridColumn2.Width = 160;
			// 
			// gridColumn3
			// 
			this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gridColumn3.AppearanceCell.Options.UseFont = true;
			this.gridColumn3.Caption = "Ngày";
			this.gridColumn3.DisplayFormat.FormatString = "dddd dd/MM";
			this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.gridColumn3.FieldName = "Ngay";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 2;
			this.gridColumn3.Width = 125;
			// 
			// gridColumn4
			// 
			this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gridColumn4.AppearanceCell.Options.UseFont = true;
			this.gridColumn4.Caption = "Ký hiệu ca";
			this.gridColumn4.FieldName = "KyHieuCa";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.AllowEdit = false;
			this.gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 3;
			this.gridColumn4.Width = 150;
			// 
			// gridColumn5
			// 
			this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gridColumn5.AppearanceCell.Options.UseFont = true;
			this.gridColumn5.Caption = "Vắng";
			this.gridColumn5.FieldName = "DanhSachXPVang";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.OptionsColumn.AllowEdit = false;
			this.gridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 4;
			this.gridColumn5.Width = 80;
			// 
			// gridColumn6
			// 
			this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gridColumn6.AppearanceCell.Options.UseFont = true;
			this.gridColumn6.Caption = "T.Công";
			this.gridColumn6.DisplayFormat.FormatString = "#0.0#";
			this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn6.FieldName = "ThucTe";
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.OptionsColumn.AllowEdit = false;
			this.gridColumn6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 5;
			this.gridColumn6.Width = 60;
			// 
			// gridColumn7
			// 
			this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gridColumn7.AppearanceCell.Options.UseFont = true;
			this.gridColumn7.Caption = "T.Phụ cấp";
			this.gridColumn7.DisplayFormat.FormatString = "#0.0#";
			this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn7.FieldName = "_TongPC";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.OptionsColumn.AllowEdit = false;
			this.gridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
			this.gridColumn7.Visible = true;
			this.gridColumn7.VisibleIndex = 6;
			// 
			// gridColumn8
			// 
			this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gridColumn8.AppearanceCell.Options.UseFont = true;
			this.gridColumn8.Caption = "T.Giờ làm việc";
			this.gridColumn8.DisplayFormat.FormatString = "%h\\:mm";
			this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.gridColumn8.FieldName = "TongGioLamViec";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.OptionsColumn.AllowEdit = false;
			this.gridColumn8.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 7;
			this.gridColumn8.Width = 90;
			// 
			// gridColumn9
			// 
			this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gridColumn9.AppearanceCell.Options.UseFont = true;
			this.gridColumn9.Caption = "T.Giờ check vân tay";
			this.gridColumn9.DisplayFormat.FormatString = "%h\\:mm";
			this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.gridColumn9.FieldName = "TongGioThucTe";
			this.gridColumn9.Name = "gridColumn9";
			this.gridColumn9.OptionsColumn.AllowEdit = false;
			this.gridColumn9.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
			this.gridColumn9.Visible = true;
			this.gridColumn9.VisibleIndex = 8;
			this.gridColumn9.Width = 90;
			// 
			// gridColumn10
			// 
			this.gridColumn10.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gridColumn10.AppearanceCell.Options.UseFont = true;
			this.gridColumn10.Caption = "Vào trễ";
			this.gridColumn10.DisplayFormat.FormatString = "%h\\:mm";
			this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.gridColumn10.FieldName = "TongVaoTre";
			this.gridColumn10.Name = "gridColumn10";
			this.gridColumn10.OptionsColumn.AllowEdit = false;
			this.gridColumn10.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
			this.gridColumn10.Visible = true;
			this.gridColumn10.VisibleIndex = 9;
			this.gridColumn10.Width = 80;
			// 
			// gridColumn11
			// 
			this.gridColumn11.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gridColumn11.AppearanceCell.Options.UseFont = true;
			this.gridColumn11.Caption = "Ra sớm";
			this.gridColumn11.DisplayFormat.FormatString = "%h\\:mm";
			this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.gridColumn11.FieldName = "TongRaSom";
			this.gridColumn11.Name = "gridColumn11";
			this.gridColumn11.OptionsColumn.AllowEdit = false;
			this.gridColumn11.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
			this.gridColumn11.Visible = true;
			this.gridColumn11.VisibleIndex = 10;
			this.gridColumn11.Width = 80;
			// 
			// gridColumn12
			// 
			this.gridColumn12.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gridColumn12.AppearanceCell.Options.UseFont = true;
			this.gridColumn12.Caption = "T.Giờ đêm";
			this.gridColumn12.DisplayFormat.FormatString = "%h\\:mm";
			this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.gridColumn12.FieldName = "TongGioLamDem";
			this.gridColumn12.Name = "gridColumn12";
			this.gridColumn12.OptionsColumn.AllowEdit = false;
			this.gridColumn12.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
			this.gridColumn12.Visible = true;
			this.gridColumn12.VisibleIndex = 11;
			this.gridColumn12.Width = 80;
			// 
			// gridColumn13
			// 
			this.gridColumn13.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			this.gridColumn13.AppearanceCell.Options.UseFont = true;
			this.gridColumn13.Caption = "T.Giờ >8h";
			this.gridColumn13.DisplayFormat.FormatString = "%h\\:mm";
			this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.gridColumn13.FieldName = "TongGioTangCuong";
			this.gridColumn13.Name = "gridColumn13";
			this.gridColumn13.OptionsColumn.AllowEdit = false;
			this.gridColumn13.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
			this.gridColumn13.Visible = true;
			this.gridColumn13.VisibleIndex = 12;
			this.gridColumn13.Width = 80;
			// 
			// gridColumn15
			// 
			this.gridColumn15.Caption = "gridColumn15";
			this.gridColumn15.ColumnEdit = this.repositoryItemCheckEdit1;
			this.gridColumn15.Name = "gridColumn15";
			this.gridColumn15.Width = 142;
			// 
			// frmChamCongV6
			// 
			this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(1344, 662);
			this.Controls.Add(this.gridControl1);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnChamCong);
			this.Controls.Add(this.dateNavigator1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkedDSNV);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Name = "frmChamCongV6";
			this.Text = "fmXemCong4";
			this.Load += new System.EventHandler(this.fmXemCong4_Load);
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.gpChonPhongBan.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkedDSNV.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateNavigator1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox gpChonPhongBan;
		private System.Windows.Forms.TreeView treePhongBan;
		private DevExpress.XtraEditors.CheckedComboBoxEdit checkedDSNV;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraScheduler.DateNavigator dateNavigator1;
		private System.Windows.Forms.Label label3;
		private DevExpress.XtraEditors.SimpleButton btnChamCong;
		private DevExpress.XtraEditors.LabelControl lbThieuChamCong;
		private System.Windows.Forms.GroupBox groupBox1;
		private DevExpress.XtraEditors.LabelControl lbVaoTreRaSom;
		private DevExpress.XtraEditors.LabelControl lbOLaiChuaXN;
		private DevExpress.XtraEditors.LabelControl lbKhongNhanDienCa;
		private System.Windows.Forms.GroupBox groupBox2;
		private DevExpress.XtraEditors.LabelControl lbXinPhepVang;
		private DevExpress.XtraEditors.LabelControl lbTreSomCoLamBu;
		private DevExpress.XtraEditors.LabelControl lbChoPhepTreSom;
		private DevExpress.XtraEditors.LabelControl lbDaXacNhanLamThem;
		private DevExpress.XtraEditors.LabelControl lbVaoRaBiChinhSua;
		private DevExpress.XtraEditors.LabelControl lbDaXacNhanCa;
		private System.Windows.Forms.CheckBox checkBox1;
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
		private DevExpress.Utils.ToolTipController toolTipController1;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
	}
}