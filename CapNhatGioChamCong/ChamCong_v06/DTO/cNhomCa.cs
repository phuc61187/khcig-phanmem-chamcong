using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ChamCong_v06.BUS;
using ChamCong_v06.Helper;

namespace ChamCong_v06.DTO {
	public class cNhomCa {
		public ID_Description IDDescription;
		public List<cCa> DSCa;
		public override string ToString()
		{
			string template = "SchID={0}; Desc={1}; ListCa={2}";
			string template2 = "[ID={0},Code={1}];";
			string listCa = string.Empty;
			foreach (cCa ca in DSCa)
			{
				listCa += string.Format(template2, ca.ID, ca.Code);
			}
			string kq = string.Format(template, IDDescription.ID, IDDescription.Description, listCa);
			return kq;
		}
	}
}
