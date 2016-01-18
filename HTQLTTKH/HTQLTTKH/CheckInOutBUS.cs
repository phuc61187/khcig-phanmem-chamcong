using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTQLTTKH.Helper;
using HTQLTTKH.Properties;

namespace HTQLTTKH {
	class CheckInOutBUS {
		internal void ChamCong(DateTime ngayBD, DateTime ngayKT, List<int> selectedUEN_s) {
			var startSearchTime=ngayBD;
			var endSearchTime=ngayKT;
			WEDatabaseDataContext db=new WEDatabaseDataContext(GV.cs);
			List<CheckInOut> AllCheck= //db.CheckInOuts.Where(item => (item.TimeStr > startSearchTime && item.TimeStr < endSearchTime));
				new List<CheckInOut>();
			var listCheck=from check in db.CheckInOuts
						  //join	uen in selectedUEN_s on check.UserEnrollNumber equals uen into group1
						  where check.IsWatched5==false
								  &&selectedUEN_s.Contains((int)check.UserEnrollNumber)
								  &&check.TimeStr>startSearchTime&&check.TimeStr<endSearchTime
						  orderby check.UserEnrollNumber, check.TimeStr
						  select check;
			var listCheckNotUsed = new List<CheckInOut>();
			int i = 0;
			while (i+1 <listCheck.Count())
			{
				var before = listCheck.ElementAt(i);
				var after = listCheck.ElementAt(i+1);
				if (before.UserEnrollNumber==after.UserEnrollNumber
					&&after.TimeStr-before.TimeStr<StaticSetting.Default._10phut)
				{
					listCheckNotUsed.Add(before);
				}
				i++;
			}
			var resultListCheck = listCheck.Except(listCheckNotUsed).OrderBy(item=>item.UserEnrollNumber).ThenBy(item=>item.TimeStr);

			var groupCheckByUEN = from check in resultListCheck group check by check.UserEnrollNumber into groupCheck orderby groupCheck.Key.Value select groupCheck;
			foreach (var subGroup in groupCheckByUEN)
			{
				var list = new List<CheckInCheckOut>();
				this.Pair(subGroup.Key.Value, subGroup.GetEnumerator(), out list);
			}
			//this.Pair()


		}

		private void Pair(int p, IEnumerator<CheckInOut> enumerator, out List<CheckInCheckOut> Result) {
			
		}
	}
}
