using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTQLTTKH.Helper;

namespace HTQLTTKH {
	class CheckInOutBUS {
		internal void ChamCong(DateTime ngayBD, DateTime ngayKT, List<int> selectedUEN_s)
		{
			var startSearchTime = ngayBD;
			var endSearchTime = ngayKT;
			WEDatabaseDataContext db = new WEDatabaseDataContext(GV.cs);
			List<CheckInOut> AllCheck = //db.CheckInOuts.Where(item => (item.TimeStr > startSearchTime && item.TimeStr < endSearchTime));
				new List<CheckInOut>();
			var kq = from check in db.CheckInOuts
			         join uen in selectedUEN_s on check.UserEnrollNumber equals uen into group1
			         from item in group1
			         select item;
			foreach (CheckInOut @out in (db.CheckInOuts.Cast<CheckInOut>().Where(item => item.TimeStr > startSearchTime && item.TimeStr < endSearchTime)))
				AllCheck.Add(@out);
		}
	}
}
