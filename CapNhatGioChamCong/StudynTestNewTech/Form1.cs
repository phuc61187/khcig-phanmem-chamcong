using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Image = System.Drawing.Image;

namespace StudynTestNewTech {
	public partial class Form1 : Form {
		private ElementHost WPFHost;

		DataTable table = new DataTable();
		public Form1() {
			InitializeComponent();
			SqlDataAccessHelper.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=WiseEyeV5Express;Integrated Security=true";
			dataGridView1.AutoGenerateColumns = false;
		}

		private void Form1_Load(object sender, EventArgs e) {
			table = SqlDataAccessHelper.ExecuteQueryString("select * from UserInfo where UserEnrollNumber between 9000 and 9500", null, null);
			var collection = new AutoCompleteStringCollection() { };
			var arrString = new string[table.Rows.Count];
			for (var i = 0; i < table.Rows.Count; i++) {
				arrString[i] = table.Rows[i]["UserFullCode"].ToString() + table.Rows[i]["UserFullName"].ToString();

			}
			collection.AddRange(arrString);
			tb1.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tb1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			tb1.AutoCompleteCustomSource = collection;
			dataGridView1.DataSource = table;

			//Debug.WriteLine("dataGridView1.DataSource = table ");

			var table2 = SqlDataAccessHelper.ExecuteQueryString("select * from Shifts");
			comboBox1.ValueMember = "ShiftID";
			comboBox1.DisplayMember = "ShiftCode";
			comboBox1.DataSource = table2;
		}



		private void button1_Click(object sender, EventArgs e) {
			//var UserEnrollNumber = 
			/*			IEnumerable<DataGridViewRow> lstGridViewRow = dataGridView1.Rows.Cast<DataGridViewRow>();
						var lst = from row in (lstGridViewRow) select row.DataBoundItem as DataRowView;
						var a12 = new A {a11 = new TimeSpan(0, 1, 1, 1), a12 = new DateTime(2000, 2, 2, 2, 2, 2)};
						var a34 = new A {a11 = new TimeSpan(0, 3, 3, 3), a12 = new DateTime(2000, 4, 4, 4, 4, 4)};
						var arr1 = new List<A> {a12, a34};
						var a56 = new A {a11 = new TimeSpan(0, 5, 5, 5), a12 = new DateTime(2000, 6,6,6,6,6)};
						var a78 = new A {a11 = new TimeSpan(0, 7,7,7), a12 = new DateTime(2000, 8,8,8,8,8)};
						var arr2 = new List<A> {a56, a78};

						Debug.WriteLine("a12: \t"+a12);
						Debug.WriteLine("a34: \t"+a34);
						var aShow = (from atemp in arr1 select atemp).Take(1).ToList();
						Debug.WriteLine("arr2 truoc thay doi " + arr2[0]);
						aShow[0].a11 = new TimeSpan(0,9,9,9);
						Debug.WriteLine("Thay đổi trực tiếp  aShow: " + arr1[0]);
						Debug.WriteLine("arr2 sau   thay doi " + arr2[0]);

						var temp1 = arr1.Find(item => item.a11 == new TimeSpan(0, 3, 3, 3));
						Debug.WriteLine(temp1);
						temp1.a11 = temp1.a11.Add(new TimeSpan(0, 3, 3, 3));
						Debug.WriteLine(arr1[1].a11);

						var temp2 = (from item in arr2 where item.a11 == new TimeSpan(0, 7, 7, 7) select item).ToList();
						temp2[0].a11 = temp2[0].a11.Add(new TimeSpan(0, 3, 3, 3));
						List<A> a100 = new List<A>(arr1);
						Debug.WriteLine(a100[0]);
						a100[0].a11 = new TimeSpan(12,12,12,12);*/

			//var kq = (from rowView in lst where (((int)rowView["UserEnrollNumber"]) < 9100) select rowView.Row).ToList();


			//Debug.WriteLine();


			/*			C tempC = new C(); 
						tempC.BA2 = new A() { a11 = new TimeSpan(1, 1, 1) };
						MessageBox.Show(tempC.ToString());
						D tempD = new D();
						MessageBox.Show(tempD.BA2.ToString());

						}*/
			Image logo = Image.FromFile(@"C:\Users\Administrator\Desktop\IMG_4764_01.jpg");
			ExcelPackage package = new ExcelPackage();
			var ws = package.Workbook.Worksheets.Add("Test Page");


			var picture = ws.Drawings.AddPicture("", logo);
			picture.SetSize(25);
			picture.SetPosition(1, 3, 2, 5);

			ws.Cells[3, 3].Value = "TỔNG CÔNG TY CÔNG NGHIỆP SÀI GÒN";
			ws.Cells[3, 4].Value = "TRÁCH NHIỆM HỮU HẠN MỘT THÀNH VIÊN";
			ws.Cells[3, 5].Value = "NHÀ MÁY THUỐC LÁ KHÁNH HỘI";
			ws.Cells[3, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
			ws.Cells[3, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
			ws.Cells[3, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
			ws.Cells[3, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Justify;
			ws.Cells[3, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Justify;
			ws.Cells[3, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Justify;
			ws.Cells[3, 3].Style.TextRotation = 90;
			ws.Cells[3, 4].Style.TextRotation = 90;
			ws.Cells[3, 5].Style.TextRotation = 90;




			Byte[] bin = package.GetAsByteArray();
			try {
				File.WriteAllBytes(@"C:\test.xlsx", bin);
				//AutoClosingMessageBox.Show("Xuất báo biểu thành công.", "Thông báo", 2000);
			} catch (Exception exception) {
				if (exception is UnauthorizedAccessException)
					MessageBox.Show("Bạn chưa được cấp quyền ghi file vào folder.", "Lỗi");
				else if (exception is DirectoryNotFoundException) MessageBox.Show("Không tìm thấy folder lưu trữ.", "Lỗi");
				else if (exception is IOException) MessageBox.Show("File đang mở bởi ứng dụng khác.", "Lỗi");
				else MessageBox.Show("Không thể ghi được file báo biểu.", "Lỗi");
			}
		}

		private int dem = 0;
		private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e) {
			dem++;
			label1.Text = dem.ToString();
		}

		private void button2_Click(object sender, EventArgs e) {
			if (dem == 5) {
				comboBox1.DataSource = null;
				label2.Text = comboBox1.DisplayMember;
			}
		}


	}
	public class A {
		public TimeSpan a11;
		public DateTime a12;
		public override string ToString() {
			return a11.GetType() + ": " + a11.ToString() + "; " + a12.GetType() + ": " + a12;
		}
	}

	public abstract class B {
		public abstract A BA1 { get; set; }
		public A BA2;
		public override string ToString() {
			string temp = string.Empty;
			temp += (BA1 == null) ? "BA1 null" : BA1.ToString();
			temp += (BA2 == null) ? "BA2 null" : BA2.ToString();
			return temp;
		}
	}

	public class C : B {
		public override A BA1 { get; set; }
		public override string ToString() {
			return (BA1 == null) ? "BA1 null" : BA1.ToString();
		}
	}

	public class D : B {
		public override A BA1 { get; set; }
		public override string ToString() {
			return (BA1 == null) ? "BA1 null" : BA1.ToString();
		}
	}

}
