namespace GiuLaiCacFileCu {
    partial class z_old_v12_frm_SuaGio {
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
            this.gbSuaGioVao = new System.Windows.Forms.GroupBox();
            this.dateTimeVaoMoi = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbKieuNhapVao = new System.Windows.Forms.ComboBox();
            this.gbSuaGioRa = new System.Windows.Forms.GroupBox();
            this.dateTimeRaMoi = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbKieuNhapRa = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbGhiChu = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbLyDo = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbSuaGioVao.SuspendLayout();
            this.gbSuaGioRa.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSuaGioVao
            // 
            this.gbSuaGioVao.Controls.Add(this.dateTimeVaoMoi);
            this.gbSuaGioVao.Controls.Add(this.label2);
            this.gbSuaGioVao.Controls.Add(this.label1);
            this.gbSuaGioVao.Controls.Add(this.cbKieuNhapVao);
            this.gbSuaGioVao.Enabled = false;
            this.gbSuaGioVao.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSuaGioVao.Location = new System.Drawing.Point(12, 12);
            this.gbSuaGioVao.Name = "gbSuaGioVao";
            this.gbSuaGioVao.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.gbSuaGioVao.Size = new System.Drawing.Size(333, 89);
            this.gbSuaGioVao.TabIndex = 0;
            this.gbSuaGioVao.TabStop = false;
            this.gbSuaGioVao.Text = "Sửa giờ vào";
            // 
            // dateTimeVaoMoi
            // 
            this.dateTimeVaoMoi.CustomFormat = "dddd d / M / yyyy H:mm";
            this.dateTimeVaoMoi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeVaoMoi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeVaoMoi.Location = new System.Drawing.Point(92, 56);
            this.dateTimeVaoMoi.Name = "dateTimeVaoMoi";
            this.dateTimeVaoMoi.ShowUpDown = true;
            this.dateTimeVaoMoi.Size = new System.Drawing.Size(230, 22);
            this.dateTimeVaoMoi.TabIndex = 2;
            this.dateTimeVaoMoi.ValueChanged += new System.EventHandler(this.dateTimeVaoMoi_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Giờ vào mới";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kiểu nhập";
            // 
            // cbKieuNhapVao
            // 
            this.cbKieuNhapVao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKieuNhapVao.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbKieuNhapVao.FormattingEnabled = true;
            this.cbKieuNhapVao.Location = new System.Drawing.Point(92, 26);
            this.cbKieuNhapVao.Name = "cbKieuNhapVao";
            this.cbKieuNhapVao.Size = new System.Drawing.Size(230, 24);
            this.cbKieuNhapVao.TabIndex = 1;
            this.cbKieuNhapVao.SelectedIndexChanged += new System.EventHandler(this.cbKieuNhapVao_SelectedIndexChanged);
            // 
            // gbSuaGioRa
            // 
            this.gbSuaGioRa.Controls.Add(this.dateTimeRaMoi);
            this.gbSuaGioRa.Controls.Add(this.label3);
            this.gbSuaGioRa.Controls.Add(this.label4);
            this.gbSuaGioRa.Controls.Add(this.cbKieuNhapRa);
            this.gbSuaGioRa.Enabled = false;
            this.gbSuaGioRa.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSuaGioRa.Location = new System.Drawing.Point(12, 107);
            this.gbSuaGioRa.Name = "gbSuaGioRa";
            this.gbSuaGioRa.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.gbSuaGioRa.Size = new System.Drawing.Size(333, 89);
            this.gbSuaGioRa.TabIndex = 0;
            this.gbSuaGioRa.TabStop = false;
            this.gbSuaGioRa.Text = "Sửa giờ ra";
            // 
            // dateTimeRaMoi
            // 
            this.dateTimeRaMoi.CustomFormat = "dddd d / M / yyyy H:mm";
            this.dateTimeRaMoi.Font = new System.Drawing.Font("Arial", 9.75F);
            this.dateTimeRaMoi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeRaMoi.Location = new System.Drawing.Point(92, 56);
            this.dateTimeRaMoi.Name = "dateTimeRaMoi";
            this.dateTimeRaMoi.ShowUpDown = true;
            this.dateTimeRaMoi.Size = new System.Drawing.Size(230, 22);
            this.dateTimeRaMoi.TabIndex = 2;
            this.dateTimeRaMoi.ValueChanged += new System.EventHandler(this.dateTimeRaMoi_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Giờ ra mới";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label4.Location = new System.Drawing.Point(6, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Kiểu nhập";
            // 
            // cbKieuNhapRa
            // 
            this.cbKieuNhapRa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKieuNhapRa.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cbKieuNhapRa.FormattingEnabled = true;
            this.cbKieuNhapRa.Location = new System.Drawing.Point(92, 26);
            this.cbKieuNhapRa.Name = "cbKieuNhapRa";
            this.cbKieuNhapRa.Size = new System.Drawing.Size(230, 24);
            this.cbKieuNhapRa.TabIndex = 1;
            this.cbKieuNhapRa.SelectedIndexChanged += new System.EventHandler(this.cbKieuNhapRa_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbGhiChu);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cbLyDo);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 202);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.groupBox3.Size = new System.Drawing.Size(333, 161);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lý do sửa";
            // 
            // tbGhiChu
            // 
            this.tbGhiChu.Font = new System.Drawing.Font("Arial", 9.75F);
            this.tbGhiChu.Location = new System.Drawing.Point(92, 56);
            this.tbGhiChu.Multiline = true;
            this.tbGhiChu.Name = "tbGhiChu";
            this.tbGhiChu.Size = new System.Drawing.Size(228, 92);
            this.tbGhiChu.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label5.Location = new System.Drawing.Point(6, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Ghi chú";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label6.Location = new System.Drawing.Point(6, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Lý do";
            // 
            // cbLyDo
            // 
            this.cbLyDo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbLyDo.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cbLyDo.FormattingEnabled = true;
            this.cbLyDo.Items.AddRange(new object[] {
            "Khác",
            "Cúp điện"});
            this.cbLyDo.Location = new System.Drawing.Point(92, 26);
            this.cbLyDo.Name = "cbLyDo";
            this.cbLyDo.Size = new System.Drawing.Size(228, 24);
            this.cbLyDo.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(104, 369);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 26);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Đồng ý";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(257, 369);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 26);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 405);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbSuaGioRa);
            this.Controls.Add(this.gbSuaGioVao);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form4";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.gbSuaGioVao.ResumeLayout(false);
            this.gbSuaGioVao.PerformLayout();
            this.gbSuaGioRa.ResumeLayout(false);
            this.gbSuaGioRa.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSuaGioVao;
        private System.Windows.Forms.DateTimePicker dateTimeVaoMoi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbKieuNhapVao;
        private System.Windows.Forms.GroupBox gbSuaGioRa;
        private System.Windows.Forms.DateTimePicker dateTimeRaMoi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbKieuNhapRa;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbGhiChu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbLyDo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}