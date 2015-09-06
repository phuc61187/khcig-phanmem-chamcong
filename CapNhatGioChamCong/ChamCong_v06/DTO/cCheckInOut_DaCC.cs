using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamCong_v06.Helper;

namespace ChamCong_v06.DTO {
    public partial class cCheckInOut_DaCC
    {
        public int ID;
        public int MaCC;
        public string MaNV;
        public string TenNV;
        public DateTime Ngay;

        public DateTime GioVao;
        public DateTime GioRaa;

        public DateTime GioVao_LamTron;
        public DateTime GioRaa_LamTron;
        public ThoiDiem TD;
        public StructTGCa KhoangTG;
        public bool ChoPhepTre;
        public bool ChoPhepSom;
        public bool VaoTuDo;
        public bool RaaTuDo;
        public StructCongCa Cong;
    }
}
