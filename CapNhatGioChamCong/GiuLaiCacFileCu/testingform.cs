using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GiuLaiCacFileCu.DAO;
using GiuLaiCacFileCu.DTO;

namespace GiuLaiCacFileCu {
	public partial class testingform : Form {
		public testingform() {
			InitializeComponent();
			SqlDataAccessHelper.ReadConnectionFile(@"C:\PathDataPage.txt");
		}

		private void testingform_Load(object sender, EventArgs e) {
			//test8();
			//dataGridView1.BeginEdit(false);
			//Debug.WriteLine(" ");
			//Close();
		}

		void test1() {
			/*
						List<cChkInOut> dsvaora = new List<cChkInOut>();
						cChk vao1 = new cChk() { TimeStr = new DateTime(2013, 1, 1, 1, 2, 0) };
						cChk ra1 = new cChk() { TimeStr = new DateTime(2013, 1, 1, 3, 4, 0) };
						cChk vao2 = new cChk() { TimeStr = new DateTime(2013, 1, 1, 5, 6, 0) };
						cChk ra2 = new cChk() { TimeStr = new DateTime(2013, 1, 1, 7, 8, 0) };
						cChk vao3 = new cChk() { TimeStr = new DateTime(2013, 1, 1, 9, 10, 0) };
						cChk ra3 = new cChk() { TimeStr = new DateTime(2013, 1, 1, 11, 12, 0) };
						cChkInOut vaora1 = new cChkInOut() { Vao = vao1, Raa = ra1 };
						cChkInOut vaora2 = new cChkInOut() { Vao = vao2, Raa = ra2 };
						cChkInOut vaora3 = new cChkInOut() { Vao = vao3, Raa = ra3 };

						dsvaora.Add(vaora2);
						dsvaora.Add(vaora1);
						dsvaora.Add(vaora3);
						Debug.WriteLine(" before sort: ");
						for (int i = 0; i < 3; i++) {
							Debug.WriteLine("i = " + i + " : " + dsvaora[i].Vao.TimeStr.ToString("hh:mm"));
						}
						Debug.WriteLine(" \nafter sort: ");
						dsvaora.Sort(new cChkInOutComparer());
						for (int i = 0; i < 3; i++) {
							Debug.WriteLine("i = " + i + " : " + dsvaora[i].Vao.TimeStr.ToString("hh:mm"));
						}
			*/
		}

		void test2() {
			DataTable dt = SqlDataAccessHelper.ExecuteQueryString("select WorkingTime from Shifts", null, null);

			int temp1 = int.Parse(dt.Rows[0][0].ToString());
			Debug.WriteLine(temp1);

			try {
				int temp = (int)dt.Rows[0][0];
				Debug.WriteLine(temp);
			} catch (Exception) {

				throw;
			}


		}

		void test3() {
			List<cShift> list1 = new List<cShift>() { new cShift() { ShiftCode = "1" }, new cShift() { ShiftCode = "2" } };
			Debug.WriteLine(" List 1: " + list1.Count + " pt: " + list1[0].ShiftCode + " va " + list1[1].ShiftCode);

			List<cShift> listCopy = new List<cShift>();
			listCopy.AddRange(list1);
			Debug.WriteLine(" List 1 Copy By AddRange: " + listCopy.Count + " pt: " + listCopy[0].ShiftCode + " va " + listCopy[1].ShiftCode);

			/*
						List<cShift> list2 = new List<cShift>(list1);
						Debug.WriteLine(" List 2 = new List<cShift>(list1) gom: " + list2.Count + " pt: " + list2[0].ShiftCode + " va " + list2[1].ShiftCode);
						list2[0].ShiftCode = "3";
						Debug.WriteLine(" Modified element 0 of List 2 " + list2.Count + " pt: " + list2[0].ShiftCode + " va " + list2[1].ShiftCode);
						Debug.WriteLine(" So voi list1[0] = " + list1[0].ShiftCode);
						Debug.WriteLine(" So voi listCopy[0] = " + listCopy[0].ShiftCode);
			*/
			//=> 2 list cùng trỏ chung đến 1 phần tử => thay đổi pt của list này thì pt của đó của list kia cũng bị thay đổi

			List<cShift> list3 = new List<cShift>();
			cShift[] temp = new cShift[] { };
			list1.CopyTo(temp);
			list3 = temp.ToList();
			//list3 = list1.
			list3[0].ShiftCode = "3";
			Debug.WriteLine(" Modified element 0 of List 3 " + list3.Count + " pt: " + list3[0].ShiftCode + " va " + list3[1].ShiftCode);
			Debug.WriteLine(" So voi list1[0] = " + list1[0].ShiftCode);
			Debug.WriteLine(" So voi listCopy[0] = " + listCopy[0].ShiftCode);
			//=> 2 list cùng trỏ chung đến 1 phần tử => thay đổi pt của list này thì pt của đó của list kia cũng bị thay đổi
		}

		void test4() {
			TimeSpan temp1 = new TimeSpan(1, 0, 0);
			TimeSpan temp2 = new TimeSpan(2, 0, 0);
			TimeSpan temp3 = new TimeSpan(3, 0, 0);

			TimeSpan temp4 = temp1 - temp3;
			Debug.WriteLine("1-3: " + temp4.ToString());
			TimeSpan temp5 = temp4.Duration();
			Debug.WriteLine("1-3 Duration: " + temp5.ToString());
			temp5 = temp5.Negate();
			Debug.WriteLine("Negate 1-3: " + temp5.ToString());

			TimeSpan temp6 = temp3 - temp1;
			TimeSpan temp7 = temp6.Duration();
			Debug.WriteLine("Negate 3-1: " + temp7.ToString());
			Debug.WriteLine("------------------------\n\n");
		}

		private DataTable table;
		public DataTable Table2;
		void test5() {
			table = SqlDataAccessHelper.ExecuteQueryString("select top 1000 * from CheckInOut", null, null);
			Table2 = SqlDataAccessHelper.ExecuteQueryString("select * from Shifts", null, null);
			dataGridView1.DataSource = table;
			dataGridView1.AutoGenerateColumns = false;
			DataGridViewComboBoxColumn cell = dataGridView1.Columns[0] as DataGridViewComboBoxColumn;
			cell.DataSource = Table2;
			cell.DisplayMember = "ShiftCode";
			cell.ValueMember = "ShiftID";

		}



		void test6() {
			/*
						clsCon1 clsCon1 = new clsCon1() { ProAtt1 = 1, ProAtt2 = 3, ProClsChkAtt1 = new cChk() { } };
						clsCon2 clsCon2 = new clsCon2() { ProAtt1 = 2, ProAtt2 = 4, ProClsChkAtt1 = new cChk() { } };
						Debug.WriteLine(clsCon1.ToString() + "\tProAtt1:" + clsCon1.ProAtt1.ToString() + "\tProAtt2:" + clsCon1.ProAtt2.ToString());
						Debug.WriteLine(clsCon2.ToString() + "\tProAtt1:" + clsCon2.ProAtt1.ToString() + "\tProAtt2:" + clsCon2.ProAtt2.ToString());
						clsCon1.ProAtt1 = 5;
						clsCon2.ProAtt2 = 6;
						Debug.WriteLine("\nModified: ");
						Debug.WriteLine(clsCon1.ToString() + "\tProAtt1:" + clsCon1.ProAtt1.ToString() + "\tProAtt2:" + clsCon1.ProAtt2.ToString());
						Debug.WriteLine(clsCon2.ToString() + "\tProAtt1:" + clsCon2.ProAtt1.ToString() + "\tProAtt2:" + clsCon2.ProAtt2.ToString());
			*/
		}

		void test7() {
			DateTime date1 = new DateTime(1000, 1, 1);
			DateTime date2 = new DateTime(1000, 1, 2);
			DateTime date3 = date1.Subtract(new TimeSpan(1, 0, 0));
			date1.Subtract(new TimeSpan(1, 0, 0));
			Debug.WriteLine("After  Subtract: date1:" + date1.ToString() + "\tdate2:" + date2.ToString() + "\tdate3:" + date3.ToString());
		}

		void test8() {

			List<clsCha> templistCha = new List<clsCha>();
			clsCon1 tempCon1 = new clsCon1() { ProAtt1 = 1, ProAtt2 = 3  };
			clsCon2 tempCon2 = new clsCon2() { ProAtt1 = 2, ProAtt2 = 4 };
			Debug.Flush();
			templistCha.Add(tempCon1);
			templistCha.Add(tempCon2);
			foreach (clsCha con in templistCha) {
				Debug.WriteLine("CHA " + con);
				if (con.GetType() == typeof(clsCon1)) {
					clsCon1 temp1Con1 = (clsCon1)con;
					Debug.WriteLine("Con 1 " + con);
				}
				else if (con.GetType() == typeof(clsCon2)) {
					clsCon2 temp1Con2 = (clsCon2)con;
					Debug.WriteLine("Con 2 " + con);
				}
			}
			this.Close();

		}

		void test9() {
			table = SqlDataAccessHelper.ExecuteQueryString("select * from CheckInOut", null, null);
			Debug.WriteLine(" Before select: Table Row count = " + table.Rows.Count);
			//table.DefaultView.RowFilter = ;
			DataRow[] arrRows = table.Select("UserEnrollNumber = 1000", "TimeStr ASC", DataViewRowState.CurrentRows);
			Debug.WriteLine(" After select: Table Row count = " + table.Rows.Count + "\t Row count = " + arrRows.Count());
			//dataGridView1.DataSource = table;
			int i = 1;
			foreach (DataRow row in arrRows) {
				Debug.WriteLine("row " + i + " \t" + row["TimeStr"].ToString());
				i++;
			}
		}

		void test11() {
			List<int> arr = new List<int>() { 2, 2, 3, 4, 5 };
			int lastitem = -1;
			foreach (int curritem in arr) {
				if (lastitem == -1) {
					lastitem = curritem;
				}
				else { }
			}
		}

		void test12() {
			List<int> arrInt = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			foreach (int i in arrInt) {
				if (i % 3 == 0) {
					Debug.WriteLine("{0} % 3 == 0", i);
				}
				else if (i % 3 == 0) {
					Debug.WriteLine("{0} % 3 == 0 lan 2 ", i);
				}
				else if (i % 3 == 1) {
					Debug.WriteLine("{0} % 3 == 1 ", i);
				}
			}
		}

		void test13() {
			table = SqlDataAccessHelper.ExecuteQueryString("select top 100 * from CheckInOut", null, null);

			dataGridView2.DataSource = table;
			dataGridView2.AutoGenerateColumns = false;

		}

        void test14() {
            DateTime dvao = new DateTime(2013,1,1);
            DateTime draa = new DateTime(2013,1,5);
            cChkIn cvao = new cChkIn(){TimeStr = dvao};
            cChkOut craa= new cChkOut(){TimeStr = draa};
            //cChkInOut CIO = new cChkInOut(){Vao = cvao,Raa = craa, TongGioThuc = };

        }

	    private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e) {

		}


		private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
			Debug.WriteLine("cell end edit ");
			Debug.WriteLine(e.ToString());
			Debug.WriteLine("------------------------");
			DataGridView dg = (DataGridView)sender;
			//if (dg.CurrentCell. != null) Debug.WriteLine(dg.CurrentCell.EditedFormattedValue.ToString());
			if (dg.CurrentCell.EditedFormattedValue != null) Debug.WriteLine("edited : " + dg.CurrentCell.EditedFormattedValue.ToString());
			if (dg.CurrentCell.FormattedValueType != null) Debug.WriteLine("FormattedValueType: " + dg.CurrentCell.FormattedValueType.ToString());
			if (dg.CurrentCell.FormattedValue != null) Debug.WriteLine("FormattedValue: " + dg.CurrentCell.FormattedValue.ToString());
			if (dg.CurrentCell.Value != null) Debug.WriteLine(" Value: " + dg.CurrentCell.Value);

		}






		private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
			if (e.RowIndex == -1) return;

			DataGridViewRow dr = dataGridView2.Rows[e.RowIndex];
			Debug.WriteLine(" DataGridViewRow: " + e.RowIndex);
			DataRowView datarow = dr.DataBoundItem as DataRowView;
			Debug.WriteLine(" " + datarow["TimeStr"]);
			Debug.WriteLine(" " + datarow.Row);
		}

		private void button1_MouseClick(object sender, MouseEventArgs e) {
			contextMenuStrip1.Show(button1, e.X, e.Y);
		}


	}



	public abstract class clsCha {
		protected int proAtt1;
		protected int proAtt2;
		protected cChk proClsChkAtt1;

		public abstract int ProAtt1 { get; set; }
		public abstract int ProAtt2 { get; set; }
		public abstract cChk ProClsChkAtt1 { get; set; }

		public override string ToString() {
			string temp = "ProAtt1: " + ProAtt1 + ";\t ProAtt2: " + ProAtt2 + ";\t ";
			return temp + "\n";
		}
	}

	public class clsCon2 : clsCha {
		public override int ProAtt1 { get { return proAtt1; } set { proAtt1 = value; } }
		public override int ProAtt2 { get { return proAtt2; } set { proAtt2 = value; } }
		public override cChk ProClsChkAtt1 { get { return proClsChkAtt1; } set { proClsChkAtt1 = value; } }
		public override string ToString() {
			string temp = "clsCon2: ProAtt1: " + ProAtt1 + ";\t ProAtt2: " + ProAtt2 + ";\t ";
			return temp + "\n";
		}
	}

	public class clsCon1 : clsCha {
		public override int ProAtt1 { get { return proAtt1; } set { proAtt1 = value; } }
		public override int ProAtt2 { get { return proAtt2; } set { proAtt2 = value; } }
		public override cChk ProClsChkAtt1 { get { return proClsChkAtt1; } set { proClsChkAtt1 = value; } }
		/*		public override string ToString() {
					string temp = "clsCon1: ProAtt1: " + ProAtt1 + ";\t ProAtt2: " + ProAtt2 + ";\t ";
					if (ProClsChkAtt1 != null) temp += " ProClsChkAtt1.UserEnrollNumber: " + ProClsChkAtt1.UserEnrollNumber;
					return temp + "\n";
				}*/
	}


}
