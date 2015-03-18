using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GiuLaiCacFileCu.DAO;
using GiuLaiCacFileCu.DTO;

namespace GiuLaiCacFileCu {
    public partial class z_old_v12_frm_SuaGio : Form {
	    private bool _thucHienSua;
	    public bool ThucHienSua {
		    get { return SuaGioRa | SuaGioVao; }
		    set { _thucHienSua = value; }
	    }
		
        public bool SuaGioVao;
        public bool SuaGioRa;
	    public DateTime GioVaoCu = DateTime.MinValue;
	    public DateTime GioRaCu = DateTime.MinValue;
        public DateTime GioVaoMoi = DateTime.MinValue;
        public DateTime GioRaMoi = DateTime.MinValue;
        public string LyDo;
        public string GhiChu;
	    private List<cShift> _dsCaVao;
	    private List<cShift> _dsCaRa;
        
        public z_old_v12_frm_SuaGio() {
            InitializeComponent();
			
        }


	    private void Form4_Load(object sender, EventArgs e) {
			_dsCaVao = new List<cShift>(ThamSo.DSCa);
			_dsCaRa = new List<cShift>(ThamSo.DSCa);
            LoadComboBoxKieuNhap();
            gbSuaGioVao.Enabled = SuaGioVao;
            gbSuaGioRa.Enabled = SuaGioRa;
        }

        void LoadComboBoxKieuNhap() {
            cbKieuNhapVao.SelectedIndexChanged -= cbKieuNhapVao_SelectedIndexChanged;
            cbKieuNhapRa.SelectedIndexChanged -= cbKieuNhapRa_SelectedIndexChanged;

            cbKieuNhapVao.DataSource = _dsCaVao;
            cbKieuNhapVao.ValueMember = "ShiftID";
            cbKieuNhapVao.DisplayMember = "ShiftCode";

            cbKieuNhapRa.DataSource = _dsCaRa;
            cbKieuNhapRa.ValueMember = "ShiftID";
            cbKieuNhapRa.DisplayMember = "ShiftCode";
            cbKieuNhapVao.SelectedIndexChanged += cbKieuNhapVao_SelectedIndexChanged;
            cbKieuNhapRa.SelectedIndexChanged += cbKieuNhapRa_SelectedIndexChanged;

        }

        private void cbKieuNhapVao_SelectedIndexChanged(object sender, EventArgs e) {
            if (cbKieuNhapVao.SelectedIndex == 0) return;
            else {
                dateTimeVaoMoi.ValueChanged -= dateTimeVaoMoi_ValueChanged;
                int tempID = (int)cbKieuNhapVao.SelectedValue;
                cShift tempShift = _dsCaVao.Find(item => item.ShiftID == tempID);
                dateTimeVaoMoi.Value = new DateTime(dateTimeVaoMoi.Value.Year, dateTimeVaoMoi.Value.Month, dateTimeVaoMoi.Value.Day, tempShift.OnnDutyTS.Hours, tempShift.OnnDutyTS.Minutes, 0);
                dateTimeVaoMoi.Update();
                dateTimeVaoMoi.ValueChanged += dateTimeVaoMoi_ValueChanged;
            }
        }

        private void cbKieuNhapRa_SelectedIndexChanged(object sender, EventArgs e) {
            if (cbKieuNhapRa.SelectedIndex == 0) return;
            else {
                dateTimeRaMoi.ValueChanged -= dateTimeRaMoi_ValueChanged;
                int tempID = (int)cbKieuNhapRa.SelectedValue;
                cShift tempShift = _dsCaRa.Find(item => item.ShiftID == tempID);
                dateTimeRaMoi.Value = new DateTime(dateTimeRaMoi.Value.Year, dateTimeRaMoi.Value.Month, dateTimeRaMoi.Value.Day, tempShift.OffDutyTS.Hours, tempShift.OffDutyTS.Minutes, 0);
                dateTimeRaMoi.Update();
                dateTimeRaMoi.ValueChanged += dateTimeRaMoi_ValueChanged;
            }

        }

        private void dateTimeVaoMoi_ValueChanged(object sender, EventArgs e)
        {
            cbKieuNhapVao.SelectedIndexChanged -= cbKieuNhapVao_SelectedIndexChanged;
            int tempID = (int)cbKieuNhapVao.SelectedValue;
            cShift tempShift = ThamSo.DSCa.Find(item => item.ShiftID == tempID);
            if (dateTimeVaoMoi.Value.TimeOfDay != tempShift.OnnDutyTS)
                cbKieuNhapVao.SelectedIndex = 0;
            cbKieuNhapVao.SelectedIndexChanged += cbKieuNhapVao_SelectedIndexChanged;
        }

        private void dateTimeRaMoi_ValueChanged(object sender, EventArgs e) {
            cbKieuNhapRa.SelectedIndexChanged -= cbKieuNhapRa_SelectedIndexChanged;
            int tempID = (int)cbKieuNhapRa.SelectedValue;
            cShift tempShift = ThamSo.DSCa.Find(item => item.ShiftID == tempID);
            if (dateTimeRaMoi.Value.TimeOfDay != tempShift.OffDutyTS)
                cbKieuNhapRa.SelectedIndex = 0;
            cbKieuNhapRa.SelectedIndexChanged += cbKieuNhapRa_SelectedIndexChanged;

        }

        private void btnOK_Click(object sender, EventArgs e) {
            dateTimeVaoMoi.Update();
            dateTimeRaMoi.Update();
            GioVaoMoi = dateTimeVaoMoi.Value;
            GioRaMoi = dateTimeRaMoi.Value;
            LyDo = cbLyDo.Text;
            GhiChu = tbGhiChu.Text;
            _thucHienSua = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _thucHienSua = false;
            this.Close();
        }
    }
}
