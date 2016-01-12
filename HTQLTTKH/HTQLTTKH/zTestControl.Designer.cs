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
			this.gridLookUpEdit1 = new DevExpress.XtraEditors.GridLookUpEdit();
			this.aResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.aResultBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			this.SuspendLayout();
			// 
			// checkedComboBoxEdit1
			// 
			this.checkedComboBoxEdit1.Location = new System.Drawing.Point(35, 50);
			this.checkedComboBoxEdit1.Margin = new System.Windows.Forms.Padding(2);
			this.checkedComboBoxEdit1.Name = "checkedComboBoxEdit1";
			this.checkedComboBoxEdit1.Properties.AllowMultiSelect = true;
			this.checkedComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.checkedComboBoxEdit1.Properties.DisplayMember = "Description";
			this.checkedComboBoxEdit1.Properties.DropDownRows = 10;
			this.checkedComboBoxEdit1.Properties.EditValueType = DevExpress.XtraEditors.Repository.EditValueTypeCollection.List;
			this.checkedComboBoxEdit1.Properties.IncrementalSearch = true;
			this.checkedComboBoxEdit1.Properties.ValueMember = "ID";
			this.checkedComboBoxEdit1.Size = new System.Drawing.Size(256, 20);
			this.checkedComboBoxEdit1.TabIndex = 0;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(47, 205);
			this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(389, 206);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(35, 93);
			this.button1.Margin = new System.Windows.Forms.Padding(2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(56, 19);
			this.button1.TabIndex = 2;
			this.button1.Text = "Init";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(96, 93);
			this.button2.Margin = new System.Windows.Forms.Padding(2);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(56, 19);
			this.button2.TabIndex = 2;
			this.button2.Text = "Process";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// timeEdit1
			// 
			this.timeEdit1.EditValue = new System.DateTime(2016, 1, 10, 0, 0, 0, 0);
			this.timeEdit1.Location = new System.Drawing.Point(301, 74);
			this.timeEdit1.Margin = new System.Windows.Forms.Padding(2);
			this.timeEdit1.Name = "timeEdit1";
			this.timeEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.timeEdit1.Properties.DisplayFormat.FormatString = "%h:mm dddd dd/MM/yyyy";
			this.timeEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.timeEdit1.Properties.EditFormat.FormatString = "%h:mm dddd dd/MM/yyyy";
			this.timeEdit1.Properties.Mask.EditMask = "H:mm dd/MM/yyyy";
			this.timeEdit1.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
			this.timeEdit1.Size = new System.Drawing.Size(203, 20);
			this.timeEdit1.TabIndex = 3;
			// 
			// simpleButton1
			// 
			this.simpleButton1.Location = new System.Drawing.Point(305, 114);
			this.simpleButton1.Margin = new System.Windows.Forms.Padding(2);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(56, 19);
			this.simpleButton1.TabIndex = 4;
			this.simpleButton1.Text = "simpleButton1";
			this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
			// 
			// gridLookUpEdit1
			// 
			this.gridLookUpEdit1.Location = new System.Drawing.Point(371, 134);
			this.gridLookUpEdit1.Name = "gridLookUpEdit1";
			this.gridLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.gridLookUpEdit1.Properties.DataSource = this.aResultBindingSource;
			this.gridLookUpEdit1.Properties.DisplayMember = "UserFullName";
			this.gridLookUpEdit1.Properties.ValueMember = "UserEnrollNumber";
			this.gridLookUpEdit1.Properties.View = this.gridLookUpEdit1View;
			this.gridLookUpEdit1.Size = new System.Drawing.Size(268, 20);
			this.gridLookUpEdit1.TabIndex = 5;
			// 
			// aResultBindingSource
			// 
			this.aResultBindingSource.DataSource = typeof(HTQLTTKH.aResult);
			// 
			// gridLookUpEdit1View
			// 
			this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
			this.gridLookUpEdit1View.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
			this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridLookUpEdit1View.OptionsSelection.MultiSelect = true;
			this.gridLookUpEdit1View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
			this.gridLookUpEdit1View.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
			this.gridLookUpEdit1View.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DevExpress.Utils.DefaultBoolean.True;
			this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			// 
			// zTestControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(730, 482);
			this.Controls.Add(this.gridLookUpEdit1);
			this.Controls.Add(this.simpleButton1);
			this.Controls.Add(this.timeEdit1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.checkedComboBoxEdit1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "zTestControl";
			this.Text = "zTestControl";
			((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.aResultBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.CheckedComboBoxEdit checkedComboBoxEdit1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private DevExpress.XtraEditors.TimeEdit timeEdit1;
		private DevExpress.XtraEditors.SimpleButton simpleButton1;
		private DevExpress.XtraEditors.GridLookUpEdit gridLookUpEdit1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
		private System.Windows.Forms.BindingSource bindingSource1;
		private System.Windows.Forms.BindingSource aResultBindingSource;
	}
}