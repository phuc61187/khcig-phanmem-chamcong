namespace CapNhatGioChamCong {
    partial class frm_XemLichSu {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.colExecuteTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g1tenthaotac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserEnrollNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeStrOld = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMachineNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeStrNew = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g1maymoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g1kieuchammoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExplain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCommandType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeight = 56;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colExecuteTime,
            this.g1tenthaotac,
            this.colUserAccount,
            this.colUserEnrollNumber,
            this.colUserFullName,
            this.colTimeStrOld,
            this.colMachineNo,
            this.colSource,
            this.colTimeStrNew,
            this.g1maymoi,
            this.g1kieuchammoi,
            this.colExplain,
            this.colNote,
            this.colUserID,
            this.colCommandType,
            this.colTimeDate});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1056, 570);
            this.dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1056, 0);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1056, 570);
            this.panel2.TabIndex = 2;
            // 
            // colExecuteTime
            // 
            this.colExecuteTime.DataPropertyName = "ExecuteTime";
            dataGridViewCellStyle2.Format = "H:mm d/M";
            this.colExecuteTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.colExecuteTime.HeaderText = "Thời gian thao tác";
            this.colExecuteTime.Name = "colExecuteTime";
            this.colExecuteTime.ReadOnly = true;
            this.colExecuteTime.ToolTipText = "Thời gian thực hiện thao tác";
            this.colExecuteTime.Width = 80;
            // 
            // g1tenthaotac
            // 
            this.g1tenthaotac.DataPropertyName = "TenThaoTac";
            this.g1tenthaotac.HeaderText = "Thao tác";
            this.g1tenthaotac.Name = "g1tenthaotac";
            this.g1tenthaotac.ReadOnly = true;
            this.g1tenthaotac.Width = 50;
            // 
            // colUserAccount
            // 
            this.colUserAccount.DataPropertyName = "UserAccount";
            this.colUserAccount.HeaderText = "Tài khoản";
            this.colUserAccount.Name = "colUserAccount";
            this.colUserAccount.ReadOnly = true;
            this.colUserAccount.ToolTipText = "Tài khoản thực hiện thao tác";
            this.colUserAccount.Visible = false;
            this.colUserAccount.Width = 80;
            // 
            // colUserEnrollNumber
            // 
            this.colUserEnrollNumber.DataPropertyName = "UserEnrollNumber";
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.colUserEnrollNumber.DefaultCellStyle = dataGridViewCellStyle3;
            this.colUserEnrollNumber.HeaderText = "Mã CC";
            this.colUserEnrollNumber.Name = "colUserEnrollNumber";
            this.colUserEnrollNumber.ReadOnly = true;
            this.colUserEnrollNumber.ToolTipText = "Mã chấm công";
            this.colUserEnrollNumber.Width = 45;
            // 
            // colUserFullName
            // 
            this.colUserFullName.DataPropertyName = "UserFullName";
            this.colUserFullName.HeaderText = "Tên NV";
            this.colUserFullName.Name = "colUserFullName";
            this.colUserFullName.ReadOnly = true;
            this.colUserFullName.ToolTipText = "Tên nhân viên";
            this.colUserFullName.Width = 150;
            // 
            // colTimeStrOld
            // 
            this.colTimeStrOld.DataPropertyName = "TimeStrOld";
            dataGridViewCellStyle4.Format = "H:mm:ss d/M";
            this.colTimeStrOld.DefaultCellStyle = dataGridViewCellStyle4;
            this.colTimeStrOld.HeaderText = "Giờ trước khi sửa";
            this.colTimeStrOld.Name = "colTimeStrOld";
            this.colTimeStrOld.ReadOnly = true;
            this.colTimeStrOld.ToolTipText = "Giờ chấm vân tay gốc";
            // 
            // colMachineNo
            // 
            this.colMachineNo.DataPropertyName = "MachineNoOld";
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.colMachineNo.DefaultCellStyle = dataGridViewCellStyle5;
            this.colMachineNo.HeaderText = "Máy gốc";
            this.colMachineNo.Name = "colMachineNo";
            this.colMachineNo.ReadOnly = true;
            this.colMachineNo.ToolTipText = "Máy chấm vân tay gốc";
            this.colMachineNo.Width = 40;
            // 
            // colSource
            // 
            this.colSource.DataPropertyName = "KieuChamCu";
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.colSource.DefaultCellStyle = dataGridViewCellStyle6;
            this.colSource.HeaderText = "Kiểu chấm cũ";
            this.colSource.Name = "colSource";
            this.colSource.ReadOnly = true;
            this.colSource.ToolTipText = "Kiểu chấm cũ";
            this.colSource.Width = 60;
            // 
            // colTimeStrNew
            // 
            this.colTimeStrNew.DataPropertyName = "TimeStrNew";
            dataGridViewCellStyle7.Format = "H:mm:ss d/M";
            this.colTimeStrNew.DefaultCellStyle = dataGridViewCellStyle7;
            this.colTimeStrNew.HeaderText = "Giờ sau khi sửa";
            this.colTimeStrNew.Name = "colTimeStrNew";
            this.colTimeStrNew.ReadOnly = true;
            this.colTimeStrNew.ToolTipText = "Giờ chấm vân tay sau khi thao tác";
            // 
            // g1maymoi
            // 
            this.g1maymoi.DataPropertyName = "MachineNoNew";
            this.g1maymoi.HeaderText = "Máy mới";
            this.g1maymoi.Name = "g1maymoi";
            this.g1maymoi.ReadOnly = true;
            this.g1maymoi.ToolTipText = "Máy chấm vân tay sau khi thao tác";
            this.g1maymoi.Width = 40;
            // 
            // g1kieuchammoi
            // 
            this.g1kieuchammoi.DataPropertyName = "KieuChamMoi";
            this.g1kieuchammoi.HeaderText = "Kiểu chấm mới";
            this.g1kieuchammoi.Name = "g1kieuchammoi";
            this.g1kieuchammoi.ReadOnly = true;
            this.g1kieuchammoi.ToolTipText = "Kiểu chấm mới";
            this.g1kieuchammoi.Width = 60;
            // 
            // colExplain
            // 
            this.colExplain.DataPropertyName = "Explain";
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colExplain.DefaultCellStyle = dataGridViewCellStyle8;
            this.colExplain.HeaderText = "Lý do";
            this.colExplain.Name = "colExplain";
            this.colExplain.ReadOnly = true;
            this.colExplain.ToolTipText = "Lý do thực hiện thao tác";
            this.colExplain.Width = 200;
            // 
            // colNote
            // 
            this.colNote.DataPropertyName = "Note";
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colNote.DefaultCellStyle = dataGridViewCellStyle9;
            this.colNote.HeaderText = "Ghi chú";
            this.colNote.Name = "colNote";
            this.colNote.ReadOnly = true;
            // 
            // colUserID
            // 
            this.colUserID.DataPropertyName = "UserID";
            this.colUserID.HeaderText = "UserID_hide";
            this.colUserID.Name = "colUserID";
            this.colUserID.ReadOnly = true;
            this.colUserID.Visible = false;
            // 
            // colCommandType
            // 
            this.colCommandType.DataPropertyName = "CommandType";
            this.colCommandType.HeaderText = "Loại Thao tác_hide";
            this.colCommandType.Name = "colCommandType";
            this.colCommandType.ReadOnly = true;
            this.colCommandType.ToolTipText = "Thao tác";
            this.colCommandType.Visible = false;
            this.colCommandType.Width = 60;
            // 
            // colTimeDate
            // 
            this.colTimeDate.DataPropertyName = "TimeDate";
            dataGridViewCellStyle10.Format = "d/M";
            this.colTimeDate.DefaultCellStyle = dataGridViewCellStyle10;
            this.colTimeDate.HeaderText = "Ngày_hide";
            this.colTimeDate.Name = "colTimeDate";
            this.colTimeDate.ReadOnly = true;
            this.colTimeDate.ToolTipText = "Ngày check dấu vân tay";
            this.colTimeDate.Visible = false;
            this.colTimeDate.Width = 63;
            // 
            // frm_XemLichSu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 570);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frm_XemLichSu";
            this.Text = "Xem lịch sử sửa";
            this.Load += new System.EventHandler(this.XemLichSuSuaGioChamCong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExecuteTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1tenthaotac;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserEnrollNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeStrOld;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMachineNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeStrNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1maymoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1kieuchammoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExplain;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCommandType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeDate;


    }
}