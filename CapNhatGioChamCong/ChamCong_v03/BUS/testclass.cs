/*
#region Includes
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ChamCong_v03.BUS;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;
using ChamCong_v03.Properties;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using log4net;
using log4net.Config;
#endregion

///////////////////////////////////////////////////////////////////////////////
// Copyright 2014 (c) by Cham Cong v 03 All Rights Reserved.
//  
// Project:      BUS
// Module:       XLTest.cs
// Description:  Tests for the XL class in the Cham Cong v 03 assembly.
//  
// Date:       Author:           Comments:
// 7/7/2014 4:06 PM  Administrator KH     Module created.
///////////////////////////////////////////////////////////////////////////////
namespace ChamCong_v03.BUSTest {

	/// <summary>
	/// Tests for the XL Class
	/// Documentation: 
	/// </summary>
	[TestFixture, Description("Tests for XL")]
	public class XLTest {
		#region Class Variables
		#endregion

		#region Setup/Teardown

		/// <summary>
		/// Code that is run once for a suite of tests
		/// </summary>
		[TestFixtureSetUp]
		public void TestFixtureSetup() {

		}

		/// <summary>
		/// Code that is run once after a suite of tests has finished executing
		/// </summary>
		[TestFixtureTearDown]
		public void TestFixtureTearDown() {

		}

		/// <summary>
		/// Code that is run before each test
		/// </summary>
		[SetUp]
		public void Initialize() {
			//New instance of XL
			

		}

		/// <summary>
		/// Code that is run after each test
		/// </summary>
		[TearDown]
		public void Cleanup() {
			//TODO:  Put dispose in here for _xl or delete this line
		}
		#endregion

		#region Property Tests

		#region GeneratedProperties

		// No public properties were found. No tests are generated for non-public scoped properties.

		#endregion // End of GeneratedProperties

		#endregion

		#region Method Tests

		#region GeneratedMethods

		/// <summary>
		/// Tao Cau Truc Data Table Method Test
		/// Documentation   :  
		/// Method Signature:  DataTable TaoCauTrucDataTable(string[] colName, Type[] colType)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TaoCauTrucDataTableTest() {
			DateTime methodStartTime = DateTime.Now;
			DataTable expected = new DataTable();

			//Parameters
			string[] colName = new string[20];
			Type[] colType = new Type[20];

			DataTable results = XL.TaoCauTrucDataTable(colName, colType);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.TaoCauTrucDataTable method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TaoCauTrucDataTable Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tao DS Ca Mo Rong Method Test
		/// Documentation   :  Tạo DS Ca mở rộng. Đã bao gồm item Khác(int.MinValue)
		/// Method Signature:  List&lt;cCaAbs&gt; TaoDSCaMoRong(List&lt;cCaAbs&gt; tmpDSCa)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TaoDSCaMoRongTest() {
			DateTime methodStartTime = DateTime.Now;
			List<cCaAbs> expected = new List<cCaAbs>();

			//Parameters
			List<cCaAbs> tmpDSCa = new List<cCaAbs>();

			List<cCaAbs> results = XL.TaoDSCaMoRong(tmpDSCa);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.TaoDSCaMoRong method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TaoDSCaMoRong Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Vao Method Test
		/// Documentation   :  
		/// Method Signature:  void Vao(DateTime timeinn, DateTime onnduty, DateTime chopheptre, out DateTime vaolam, out TimeSpan tre)
		/// </summary>
		[Test]
		public void VaoTest() {

			//Parameters
			DateTime timeinn = new DateTime(1969, 7, 21, 5, 51,1);
			DateTime onnduty = new DateTime(1969, 7, 21, 5, 45,0);
			DateTime chopheptre = new DateTime(1969, 7, 21, 5, 50,0);
			DateTime vaolam = new DateTime(1969, 7, 21);
			TimeSpan tre = new TimeSpan();

			XL.Vao(timeinn, onnduty, chopheptre, out vaolam, out tre);

			Console.WriteLine(String.Format("timeinn {0}\n onnduty, {1}\nchopheptre, {2}\nout vaolam, {3}\nout tre{4}\n", timeinn, onnduty, chopheptre, vaolam, tre));
		}

		/// <summary>
		/// Raa Method Test
		/// Documentation   :  
		/// Method Signature:  void Raa(DateTime timeout, DateTime offduty, DateTime chophepsom, out DateTime raalam, out TimeSpan som)
		/// </summary>
		[Test]
		public void RaaTest() {

			//Parameters
			DateTime timeout = new DateTime(1969, 7, 20, 5, 33, 59);
			DateTime offduty = new DateTime(1969, 7, 20, 5, 45, 00);
			DateTime chophepsom = new DateTime(1969, 7, 20, 5, 35, 00);
			DateTime raalam = new DateTime(1969, 7, 21);
			TimeSpan som = new TimeSpan();

			XL.Raa(timeout, offduty, chophepsom, out raalam, out som);

			Console.WriteLine(String.Format("timeout {0}\n ,offduty {1}\n,chophepsom {2}\n,raalam {3}\nsom {4}\n", timeout, offduty, chophepsom, raalam, som));
		}

		/// <summary>
		/// O Lai Method Test
		/// Documentation   :  
		/// Method Signature:  void OLai(DateTime timeout, DateTime offduty, DateTime batdaulamthem, out TimeSpan olai)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void OLaiTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			DateTime timeout = new DateTime(1969, 7, 21);
			DateTime offduty = new DateTime(1969, 7, 21);
			DateTime batdaulamthem = new DateTime(1969, 7, 21);
			TimeSpan olai = new TimeSpan();

			XL.OLai(timeout, offduty, batdaulamthem, out olai);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.OLai Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Raa Tinh OT Method Test
		/// Documentation   :  
		/// Method Signature:  void RaaTinhOT(DateTime raalam, TimeSpan OTmin, out DateTime raalam_tinhOT)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void RaaTinhOTTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			DateTime raalam = new DateTime(1969, 7, 21);
			TimeSpan OTmin = new TimeSpan();
			DateTime raalam_tinhOT = new DateTime(1969, 7, 21);

			XL.RaaTinhOT(raalam, OTmin, out raalam_tinhOT);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.RaaTinhOT Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// TG Lam Tinh Cong Method Test
		/// Documentation   :  
		/// Method Signature:  void TGLamTinhCong(DateTime vaolam, DateTime raalam_chuaOT, TimeSpan OTMin, TimeSpan LunchMin, out TimeSpan TGLamTinhCong)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TGLamTinhCongTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			DateTime vaolam = new DateTime(1969, 7, 21);
			DateTime raalam_chuaOT = new DateTime(1969, 7, 21);
			TimeSpan OTMin = new TimeSpan();
			TimeSpan LunchMin = new TimeSpan();
			TimeSpan TGLamTinhCong = new TimeSpan();

			XL.TGLamTinhCong(vaolam, raalam_chuaOT, OTMin, LunchMin, out TGLamTinhCong);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TGLamTinhCong Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Lam Dem Method Test
		/// Documentation   :  
		/// Method Signature:  void LamDem(DateTime VaoLam, DateTime RaaLam, DateTime _21h45, DateTime _05h45, out DateTime vaolamdem, out DateTime raalamdem, out TimeSpan TGLamDem, out bool QuaDem)
		/// </summary>
		[Test]
		public void LamDemTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			DateTime VaoLam = new DateTime(1969, 7, 21, 20,45,0);
			DateTime RaaLam = new DateTime(1969, 7, 21, 22,45,0);
			DateTime _21h45 = new DateTime(1969, 7, 21, 21,45,0);
			DateTime _05h45 = new DateTime(1969, 7, 22, 5,45,0);
			DateTime vaolamdem = new DateTime(1969, 7, 21);
			DateTime raalamdem = new DateTime(1969, 7, 21);
			TimeSpan TGLamDem = new TimeSpan();
			bool QuaDem = true;

			XL.LamDem(VaoLam, RaaLam, _21h45, _05h45, out vaolamdem, out raalamdem, out TGLamDem, out QuaDem);

			Console.WriteLine(String.Format("VaoLam {0}\n ,RaaLam {1}\n,vaolamdem {2}\n,raalamdem {3}\n,TGLamDem {4}\n", VaoLam, RaaLam, vaolamdem, raalamdem, TGLamDem));
		}

		/// <summary>


		/// <summary>
		/// Tao Ca Tu Do Method Test
		/// Documentation   :  
		/// Method Signature:  void TaoCaTuDo(cCaTuDo Ca, DateTime CheckInTime, TimeSpan WorkingTime, TimeSpan SoPhutChoPhepTre, TimeSpan SoPhutChoPhepSomTS, TimeSpan SoPhutAfterOT, float WorkingDay, string kyhieu)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TaoCaTuDoTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			cCaTuDo Ca = new cCaTuDo();
			DateTime CheckInTime = new DateTime(1969, 7, 21);
			TimeSpan WorkingTime = new TimeSpan();
			TimeSpan SoPhutChoPhepTre = new TimeSpan();
			TimeSpan SoPhutChoPhepSomTS = new TimeSpan();
			TimeSpan SoPhutAfterOT = new TimeSpan();
			const float WorkingDay = 2.99999F;
			const string kyhieu = "test";

			XL.TaoCaTuDo(Ca, CheckInTime, WorkingTime, SoPhutChoPhepTre, SoPhutChoPhepSomTS, SoPhutAfterOT, WorkingDay, kyhieu);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TaoCaTuDo Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh Vao Tre Method Test
		/// Documentation   :  
		/// Method Signature:  TimeSpan TinhVaoTre(DateTime Vao, DateTime OnnDuty, DateTime chophepvaotre)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhVaoTreTest() {
			DateTime methodStartTime = DateTime.Now;
			TimeSpan expected = new TimeSpan();

			//Parameters
			DateTime Vao = new DateTime(1969, 7, 21);
			DateTime OnnDuty = new DateTime(1969, 7, 21);
			DateTime chophepvaotre = new DateTime(1969, 7, 21);

			TimeSpan results = XL.TinhVaoTre(Vao, OnnDuty, chophepvaotre);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.TinhVaoTre method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhVaoTre Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh Raa Som Method Test
		/// Documentation   :  
		/// Method Signature:  TimeSpan TinhRaaSom(DateTime Raa, DateTime OffDuty, DateTime chophepraasom)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhRaaSomTest() {
			DateTime methodStartTime = DateTime.Now;
			TimeSpan expected = new TimeSpan();

			//Parameters
			DateTime Raa = new DateTime(1969, 7, 21);
			DateTime OffDuty = new DateTime(1969, 7, 21);
			DateTime chophepraasom = new DateTime(1969, 7, 21);

			TimeSpan results = XL.TinhRaaSom(Raa, OffDuty, chophepraasom);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.TinhRaaSom method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhRaaSom Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh Vao Lam Method Test
		/// Documentation   :  
		/// Method Signature:  DateTime TinhVaoLam(DateTime Vao, DateTime OnnDuty, TimeSpan tre)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhVaoLamTest() {
			DateTime methodStartTime = DateTime.Now;
			DateTime expected = new DateTime(1969, 7, 21);

			//Parameters
			DateTime Vao = new DateTime(1969, 7, 21);
			DateTime OnnDuty = new DateTime(1969, 7, 21);
			TimeSpan tre = new TimeSpan();

			DateTime results = XL.TinhVaoLam(Vao, OnnDuty, tre);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.TinhVaoLam method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhVaoLam Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh Vao Lam Method Test
		/// Documentation   :  
		/// Method Signature:  DateTime TinhVaoLam(DateTime Vao, DateTime OnnDuty, DateTime chophepvaotre)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhVaoLam1Test() {
			DateTime methodStartTime = DateTime.Now;
			DateTime expected = new DateTime(1969, 7, 21);

			//Parameters
			DateTime Vao = new DateTime(1969, 7, 21);
			DateTime OnnDuty = new DateTime(1969, 7, 21);
			DateTime chophepvaotre = new DateTime(1969, 7, 21);

			DateTime results = XL.TinhVaoLam(Vao, OnnDuty, chophepvaotre);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.TinhVaoLam1 method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhVaoLam1 Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh Raa Lam Method Test
		/// Documentation   :  
		/// Method Signature:  DateTime TinhRaaLam(DateTime Raa, DateTime OffDuty, TimeSpan som, TimeSpan OT)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhRaaLamTest() {
			DateTime methodStartTime = DateTime.Now;
			DateTime expected = new DateTime(1969, 7, 21);

			//Parameters
			DateTime Raa = new DateTime(1969, 7, 21);
			DateTime OffDuty = new DateTime(1969, 7, 21);
			TimeSpan som = new TimeSpan();
			TimeSpan OT = new TimeSpan();

			DateTime results = XL.TinhRaaLam(Raa, OffDuty, som, OT);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.TinhRaaLam method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhRaaLam Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh Raa Lam Ko OT Method Test
		/// Documentation   :  
		/// Method Signature:  DateTime TinhRaaLam_KoOT(DateTime Raa, DateTime OffDuty, DateTime chophepraasom)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhRaaLam_KoOTTest() {
			DateTime methodStartTime = DateTime.Now;
			DateTime expected = new DateTime(1969, 7, 21);

			//Parameters
			DateTime Raa = new DateTime(1969, 7, 21);
			DateTime OffDuty = new DateTime(1969, 7, 21);
			DateTime chophepraasom = new DateTime(1969, 7, 21);

			DateTime results = XL.TinhRaaLam_KoOT(Raa, OffDuty, chophepraasom);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.TinhRaaLam_KoOT method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhRaaLam_KoOT Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh O Lai Them Method Test
		/// Documentation   :  
		/// Method Signature:  TimeSpan TinhOLaiThem(DateTime Raa, DateTime OffDuty, DateTime batdaulamthem)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhOLaiThemTest() {
			DateTime methodStartTime = DateTime.Now;
			TimeSpan expected = new TimeSpan();

			//Parameters
			DateTime Raa = new DateTime(1969, 7, 21);
			DateTime OffDuty = new DateTime(1969, 7, 21);
			DateTime batdaulamthem = new DateTime(1969, 7, 21);

			TimeSpan results = XL.TinhOLaiThem(Raa, OffDuty, batdaulamthem);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.TinhOLaiThem method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhOLaiThem Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh BD Lam Dem Method Test
		/// Documentation   :  
		/// Method Signature:  DateTime TinhBDLamDem(DateTime VaoLam, DateTime BDLamDem)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhBDLamDemTest() {
			DateTime methodStartTime = DateTime.Now;
			DateTime expected = new DateTime(1969, 7, 21);

			//Parameters
			DateTime VaoLam = new DateTime(1969, 7, 21);
			DateTime BDLamDem = new DateTime(1969, 7, 21);

			DateTime results = XL.TinhBDLamDem(VaoLam, BDLamDem);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.TinhBDLamDem method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhBDLamDem Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh KT Lam Dem Method Test
		/// Documentation   :  
		/// Method Signature:  DateTime TinhKTLamDem(DateTime RaaLam, DateTime KTLamDem)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhKTLamDemTest() {
			DateTime methodStartTime = DateTime.Now;
			DateTime expected = new DateTime(1969, 7, 21);

			//Parameters
			DateTime RaaLam = new DateTime(1969, 7, 21);
			DateTime KTLamDem = new DateTime(1969, 7, 21);

			DateTime results = XL.TinhKTLamDem(RaaLam, KTLamDem);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.TinhKTLamDem method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhKTLamDem Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Khoi Tao Dataset NameV Method Test
		/// Documentation   :  
		/// Method Signature:  List&lt;cUserInfo&gt; KhoiTaoDSNV(List&lt;cUserInfo&gt; dsnv, DataTable tableDSNV)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void KhoiTaoDSNVTest() {
			DateTime methodStartTime = DateTime.Now;
			List<cUserInfo> expected = new List<cUserInfo>();

			//Parameters
			List<cUserInfo> dsnv = new List<cUserInfo>();
			DataTable tableDSNV = new DataTable();

			List<cUserInfo> results = XL.KhoiTaoDSNV(dsnv, tableDSNV);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.KhoiTaoDSNV method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.KhoiTaoDSNV Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Diem Danh Method Test
		/// Documentation   :  
		/// Method Signature:  void DiemDanh(List&lt;cUserInfo&gt; dsnv, DateTime ngayBD_Bef2D, DateTime ngayKT_Aft2D)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void DiemDanhTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cUserInfo> dsnv = new List<cUserInfo>();
			DateTime ngayBD_Bef2D = new DateTime(1969, 7, 21);
			DateTime ngayKT_Aft2D = new DateTime(1969, 7, 21);

			XL.DiemDanh(dsnv, ngayBD_Bef2D, ngayKT_Aft2D);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.DiemDanh Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Xem Cong Method Test
		/// Documentation   :  
		/// Method Signature:  void XemCong(List&lt;cUserInfo&gt; dsnv, DateTime ngayBD_Bef2D, DateTime ngayKT_Aft2D)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void XemCongTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cUserInfo> dsnv = new List<cUserInfo>();
			DateTime ngayBD_Bef2D = new DateTime(1969, 7, 21);
			DateTime ngayKT_Aft2D = new DateTime(1969, 7, 21);

			XL.XemCong(dsnv, ngayBD_Bef2D, ngayKT_Aft2D);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.XemCong Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Array Rows To DSXNPiece50 Method Test
		/// Documentation   :  
		/// Method Signature:  void ArrayRowsToDSXNPC50(DataRow[] arrRows, List&lt;cTemp1&gt; dsXacNhanPC)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void ArrayRowsToDSXNPC50Test() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			DataRow[] arrRows = new DataRow[0];
			List<cTemp1> dsXacNhanPC = new List<cTemp1>();

			XL.ArrayRowsToDSXNPC50(arrRows, dsXacNhanPC);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.ArrayRowsToDSXNPC50 Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Array Rows To DSXNPieceodeB Method Test
		/// Documentation   :  
		/// Method Signature:  void ArrayRowsToDSXNPCDB(DataRow[] arrRows, List&lt;cTemp&gt; dsXacNhanPC)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void ArrayRowsToDSXNPCDBTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			DataRow[] arrRows = new DataRow[0];
			List<cTemp> dsXacNhanPC = new List<cTemp>();

			XL.ArrayRowsToDSXNPCDB(arrRows, dsXacNhanPC);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.ArrayRowsToDSXNPCDB Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh Lai Phu Cap TC Method Test
		/// Documentation   :  
		/// Method Signature:  void TinhLaiPhuCapTC(List&lt;cTemp1&gt; dsXacNhanPC, List&lt;cNgayCong&gt; dsNgayCong)
		/// </summary>
		[Test]
		public void TinhLaiPhuCapTCTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cTemp1> dsXacNhanPC = new List<cTemp1>();
			cTemp1 pc1 = new cTemp1 {Ngay = DateTime.Today, TinhPC50 = true, UserEnrollNumber = 1000};
			dsXacNhanPC.Add(pc1);
			List<cNgayCong> dsNgayCong = new List<cNgayCong>();
			cNgayCong ngay9 = new cNgayCong
				{
					Ngay = DateTime.Today,
					QuaDem = true,
					TG = new ThoiGian {	GioLamTrongNgay = new TimeSpan(0, 6, 0,0),
										LamDemTrongNgay = new TimeSpan(0, 6, 0, 0)
					}
				};
			dsNgayCong.Add(ngay9);
			var n = dsNgayCong[0];
			XL.TinhLaiPhuCapTC(dsXacNhanPC, dsNgayCong);
			string temp = "pc30={0} \npc50={1} \n pc70={2}\ntongpc={6}";
			temp += "GioLamTrongNgay={3} quadem={4} \nlamthem={5} \n";
			temp = string.Format(temp, n.PhuCap30, n.PhuCap50, n.PhuCapTCC3, n.TG.GioLamTrongNgay, n.TG.LamDemTrongNgay, n.TG.Tinh150, n.TongPhuCap);
			MessageBox.Show(temp);
			

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhLaiPhuCapTC Time Elapsed: {0}", methodDuration));
		}

		public static void TinhLaiPhuCapTC(List<cTemp1> dsXacNhanPC, List<cNgayCong> dsNgayCong) {
			/*var pctc = Convert.ToSingle(XL2.PC50) / 100f;
			var pcdem = Convert.ToSingle(XL2.PCDem) / 100f;#1#
			var pctc = 0.5d;
			var pcdem = 0.3d;

			foreach (var item in dsXacNhanPC) {
				var ngayCong = dsNgayCong.Find(o => o.Ngay == item.Ngay);
				var SoGioTinhCong = ngayCong.TG.GioLamTrongNgay;
				var SoGioLamDemmm = ngayCong.TG.LamDemTrongNgay;
				var SoGioLamThem = TimeSpan.Zero;
				ngayCong.TinhPC50 = (item.TinhPC50);

				if (ngayCong.QuaDem == false) {
					if (ngayCong.TinhPC50 && (SoGioTinhCong - XL2._08gio > XL2._01phut)) {
						SoGioLamThem = (SoGioTinhCong - XL2._08gio);
						ngayCong.TG.Tinh150 = SoGioLamThem;
						var pc50 = ((SoGioLamThem.TotalHours / 8d) * pctc);
						ngayCong.PhuCap50 = pc50;
						ngayCong.PhuCap30 = 0d;
						ngayCong.PhuCapTCC3 = 0d;
						ngayCong.TongPhuCap = ngayCong.PhuCap50;
					}
				}
				else {
					if (ngayCong.TinhPC50 == false || (SoGioTinhCong - XL2._08gio <= XL2._01phut)) {
						ngayCong.PhuCap30 = (SoGioLamDemmm.TotalHours / 8d) * pcdem;
						ngayCong.PhuCap50 = 0d;
						ngayCong.PhuCapTCC3 = 0d;
						ngayCong.TongPhuCap = ngayCong.PhuCap30;
					}
					else {
						SoGioLamThem = (SoGioTinhCong - XL2._08gio);

						var SoGioLamTinhPC50 = TimeSpan.Zero;
						var SoGioLamTinhPC30 = TimeSpan.Zero;
						var SoGioTinhPCTCC3 = TimeSpan.Zero;
						var tongpc = 0d;
						if (SoGioLamThem >= SoGioLamDemmm) // trọn qua đêm là tăng cường đêm, còn lại là tăng cường ngày
						{
							SoGioTinhPCTCC3 = SoGioLamDemmm;
							SoGioLamTinhPC50 = SoGioLamThem - SoGioLamDemmm; // số giờ tính pctc
							var pc70 = (SoGioTinhPCTCC3.TotalHours / 8d) * 1d;
							var pc50 = (SoGioLamTinhPC50.TotalHours / 8d) * pctc;
							tongpc = pc50 + pc70;
						}
						else {
							SoGioTinhPCTCC3 = SoGioLamThem;
							SoGioLamTinhPC30 = SoGioLamDemmm - SoGioLamThem;
							var pc70 = (SoGioTinhPCTCC3.TotalHours / 8d) * 1d;
							var pc30 = (SoGioLamTinhPC30.TotalHours / 8d) * pcdem;
							tongpc = pc70 + pc30;
						}
						ngayCong.TongPhuCap = tongpc;
					}
				}



			}
		}


		/// <summary>
		/// Tinh Lai Phu Cap Database Method Test
		/// Documentation   :  
		/// Method Signature:  void TinhLaiPhuCapDB(List&lt;cTemp&gt; dsXacNhanPC, List&lt;cNgayCong&gt; dsNgayCong)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhLaiPhuCapDBTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cTemp> dsXacNhanPC = new List<cTemp>();
			List<cNgayCong> dsNgayCong = new List<cNgayCong>();

			XL.TinhLaiPhuCapDB(dsXacNhanPC, dsNgayCong);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhLaiPhuCapDB Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Array Rows To DS Check A Method Test
		/// Documentation   :  
		/// Method Signature:  void ArrayRowsToDSCheck_A(DataRow[] arrRows, List&lt;cChk&gt; ds_Check_A)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void ArrayRowsToDSCheck_ATest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			DataRow[] arrRows = new DataRow[0];
			List<cChk> ds_Check_A = new List<cChk>();

			XL.ArrayRowsToDSCheck_A(arrRows, ds_Check_A);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.ArrayRowsToDSCheck_A Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Loai Bo Check Cung Loai Trong 3 0phut Method Test
		/// Documentation   :  
		/// Method Signature:  void LoaiBoCheckCungLoaiTrong30phut(List&lt;cChk&gt; ds_Check_A, List&lt;cChk&gt; ds_Check_Trong30ph)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void LoaiBoCheckCungLoaiTrong30phutTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cChk> ds_Check_A = new List<cChk>();
			List<cChk> ds_Check_Trong30ph = new List<cChk>();

			XL.LoaiBoCheckCungLoaiTrong30phut(ds_Check_A, ds_Check_Trong30ph);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.LoaiBoCheckCungLoaiTrong30phut Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Ghep Conflict Of InterestO A Method Test
		/// Documentation   :  
		/// Method Signature:  void GhepCIO_A(List&lt;cChk&gt; ds_Check_A, List&lt;cChkInOut_A&gt; ds_CIO_A)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void GhepCIO_ATest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cChk> ds_Check_A = new List<cChk>();
			List<cChkInOut_A> ds_CIO_A = new List<cChkInOut_A>();

			XL.GhepCIO_A(ds_Check_A, ds_CIO_A);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.GhepCIO_A Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Loai Bo Conflict Of InterestO Ko Hop Le Method Test
		/// Documentation   :  
		/// Method Signature:  void LoaiBoCIOKoHopLe(List&lt;cChkInOut_A&gt; ds_CIO_A, List&lt;cChk&gt; ds_Check_A, List&lt;cChk&gt; dsCheck_KoHopLe)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void LoaiBoCIOKoHopLeTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cChkInOut_A> ds_CIO_A = new List<cChkInOut_A>();
			List<cChk> ds_Check_A = new List<cChk>();
			List<cChk> dsCheck_KoHopLe = new List<cChk>();

			XL.LoaiBoCIOKoHopLe(ds_CIO_A, ds_Check_A, dsCheck_KoHopLe);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.LoaiBoCIOKoHopLe Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Xet Ca Conflict Of InterestO A Method Test
		/// Documentation   :  
		/// Method Signature:  void XetCa_CIO_A(List&lt;cChkInOut_A&gt; ds_CIO_A, List&lt;cCaAbs&gt; dsca, List&lt;cChk&gt; ds_raa3_vao1, List&lt;cChk&gt; ds_check_A)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void XetCa_CIO_ATest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cChkInOut_A> ds_CIO_A = new List<cChkInOut_A>();
			List<cCaAbs> dsca = new List<cCaAbs>();
			List<cChk> ds_raa3_vao1 = new List<cChk>();
			List<cChk> ds_check_A = new List<cChk>();

			XL.XetCa_CIO_A(ds_CIO_A, dsca, ds_raa3_vao1, ds_check_A);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.XetCa_CIO_A Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Kiemtra Thuoc Ca Method Test
		/// Documentation   :  
		/// Method Signature:  cCaChuan KiemtraThuocCa(cChkInOut_A CIO, List&lt;cCaAbs&gt; dsCa)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void KiemtraThuocCaTest() {
			DateTime methodStartTime = DateTime.Now;
			cCaChuan expected = new cCaChuan();

			//Parameters
			cChkInOut_A CIO = new cChkInOut_A();
			List<cCaAbs> dsCa = new List<cCaAbs>();

			cCaChuan results = XL.KiemtraThuocCa(CIO, dsCa);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.KiemtraThuocCa method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.KiemtraThuocCa Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Kiemtra Khoang Hieu Ca Method Test
		/// Documentation   :  
		/// Method Signature:  List&lt;cCaAbs&gt; KiemtraKhoangHieuCa(cChkInOut_A CIO, List&lt;cCaAbs&gt; dsCa)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void KiemtraKhoangHieuCaTest() {
			DateTime methodStartTime = DateTime.Now;
			List<cCaAbs> expected = new List<cCaAbs>();

			//Parameters
			cChkInOut_A CIO = new cChkInOut_A();
			List<cCaAbs> dsCa = new List<cCaAbs>();

			List<cCaAbs> results = XL.KiemtraKhoangHieuCa(CIO, dsCa);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.KiemtraKhoangHieuCa method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.KiemtraKhoangHieuCa Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Array Rows To DS Conflict Of InterestO V Method Test
		/// Documentation   :  
		/// Method Signature:  void ArrayRowsToDS_CIO_V(DataRow[] arrRows, List&lt;cCaAbs&gt; dsCa, List&lt;cChkInOut_V&gt; DS_CIO_V)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void ArrayRowsToDS_CIO_VTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			DataRow[] arrRows = new DataRow[0];
			List<cCaAbs> dsCa = new List<cCaAbs>();
			List<cChkInOut_V> DS_CIO_V = new List<cChkInOut_V>();

			XL.ArrayRowsToDS_CIO_V(arrRows, dsCa, DS_CIO_V);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.ArrayRowsToDS_CIO_V Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tron DS Conflict Of InterestO A V Method Test
		/// Documentation   :  
		/// Method Signature:  void TronDS_CIO_A_V(List&lt;cChkInOut_A&gt; dsCIO_A, List&lt;cChkInOut_V&gt; dsCIO_V, List&lt;cChkInOut&gt; kq)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TronDS_CIO_A_VTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cChkInOut_A> dsCIO_A = new List<cChkInOut_A>();
			List<cChkInOut_V> dsCIO_V = new List<cChkInOut_V>();
			List<cChkInOut> kq = new List<cChkInOut>();

			XL.TronDS_CIO_A_V(dsCIO_A, dsCIO_V, kq);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TronDS_CIO_A_V Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh Cong Theo Ngay Method Test
		/// Documentation   :  
		/// Method Signature:  void TinhCongTheoNgay(List&lt;cChkInOut&gt; dsVaoRa, DateTime ngayBD_Bef2D, DateTime ngayKT_Aft2D, List&lt;cLoaiVang&gt; dsVang, List&lt;cNgayCong&gt; dsNgayCong, bool macdinhtinhpc50)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhCongTheoNgayTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cChkInOut> dsVaoRa = new List<cChkInOut>();
			DateTime ngayBD_Bef2D = new DateTime(1969, 7, 21);
			DateTime ngayKT_Aft2D = new DateTime(1969, 7, 21);
			List<cLoaiVang> dsVang = new List<cLoaiVang>();
			List<cNgayCong> dsNgayCong = new List<cNgayCong>();
			const bool macdinhtinhpc50 = true;

			XL.TinhCongTheoNgay(dsVaoRa, ngayBD_Bef2D, ngayKT_Aft2D, dsVang, dsNgayCong, macdinhtinhpc50);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhCongTheoNgay Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Array Rows To DS Vang Method Test
		/// Documentation   :  
		/// Method Signature:  void ArrayRowsToDSVang(DataRow[] arrRow, List&lt;cLoaiVang&gt; dsVang)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void ArrayRowsToDSVangTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			DataRow[] arrRow = new DataRow[0];
			List<cLoaiVang> dsVang = new List<cLoaiVang>();

			XL.ArrayRowsToDSVang(arrRow, dsVang);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.ArrayRowsToDSVang Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Doc Ngay Le Vao DS Vang Method Test
		/// Documentation   :  
		/// Method Signature:  void DocNgayLeVaoDSVang(DataTable tableNgayLe, List&lt;cLoaiVang&gt; dsVangs)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void DocNgayLeVaoDSVangTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			DataTable tableNgayLe = new DataTable();
			List<cLoaiVang> dsVangs = new List<cLoaiVang>();

			XL.DocNgayLeVaoDSVang(tableNgayLe, dsVangs);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.DocNgayLeVaoDSVang Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Them Gio Cho NV Method Test
		/// Documentation   :  
		/// Method Signature:  void ThemGioChoNV(cChk check, cUserInfo nv, int pUserID, string pLydo, string pGhichu)
		/// </summary>

		/// <summary>
		/// Them Gio Cho NVQL Method Test
		/// Documentation   :  
		/// Method Signature:  void ThemGioChoNVQL(cChk check, cUserInfo nv, int pUserID, string pLydo, string pGhichu)
		/// <summary>
		/// Xoa Gio Cho NV Method Test
		/// Documentation   :  
		/// Method Signature:  bool XoaGioChoNV(cChk check, cUserInfo nhanvien, int currUserID, string lydo, string ghichu)
		/// </summary>
		/// <summary>
		/// Sua Gio Cho NV Method Test
		/// Documentation   :  
		/// Method Signature:  void SuaGioChoNV(cChk checkold, cChk checknew, cUserInfo nhanvien, int currUserID, string lydo, string ghichu)


		/// <summary>
		/// Tao Table Dataset NameV Method Test
		/// Documentation   :  
		/// Method Signature:  void TaoTableDSNV(List&lt;cUserInfo&gt; m_DSNV, DataTable m_tableDSNV)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TaoTableDSNVTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cUserInfo> m_DSNV = new List<cUserInfo>();
			DataTable m_tableDSNV = new DataTable();

			XL.TaoTableDSNV(m_DSNV, m_tableDSNV);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TaoTableDSNV Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tao Table Diem Danh Method Test
		/// Documentation   :  
		/// Method Signature:  void TaoTableDiemDanh(List&lt;cUserInfo&gt; dsnv, DataTable kq, out int SoNVDangLamViec, out int SoNVDaRaVe, out int SoNVVang)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TaoTableDiemDanhTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cUserInfo> dsnv = new List<cUserInfo>();
			DataTable kq = new DataTable();
			int SoNVDangLamViec = 123;
			int SoNVDaRaVe = 123;
			int SoNVVang = 123;

			XL.TaoTableDiemDanh(dsnv, kq, out SoNVDangLamViec, out SoNVDaRaVe, out SoNVVang);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TaoTableDiemDanh Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tao Table Xem Cong Method Test
		/// Documentation   :  
		/// Method Signature:  void TaoTableXemCong(List&lt;cUserInfo&gt; dsnv, DataTable kq)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TaoTableXemCongTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cUserInfo> dsnv = new List<cUserInfo>();
			DataTable kq = new DataTable();

			XL.TaoTableXemCong(dsnv, kq);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TaoTableXemCong Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tao Table Gio KDQD Method Test
		/// Documentation   :  
		/// Method Signature:  void TaoTableGioKDQD(List&lt;cUserInfo&gt; dsnvDiemDanh, DataTable kq)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TaoTableGioKDQDTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cUserInfo> dsnvDiemDanh = new List<cUserInfo>();
			DataTable kq = new DataTable();

			XL.TaoTableGioKDQD(dsnvDiemDanh, kq);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TaoTableGioKDQD Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tao Table Gio Thieu Check Method Test
		/// Documentation   :  
		/// Method Signature:  void TaoTableGioThieuCheck(List&lt;cUserInfo&gt; dsnvDiemDanh, DataTable kq)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TaoTableGioThieuCheckTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cUserInfo> dsnvDiemDanh = new List<cUserInfo>();
			DataTable kq = new DataTable();

			XL.TaoTableGioThieuCheck(dsnvDiemDanh, kq);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TaoTableGioThieuCheck Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tao Table Xac Nhan Tang Ca Method Test
		/// Documentation   :  
		/// Method Signature:  void TaoTableXacNhanTangCa(List&lt;cUserInfo&gt; dsnv, DataTable table)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TaoTableXacNhanTangCaTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cUserInfo> dsnv = new List<cUserInfo>();
			DataTable table = new DataTable();

			XL.TaoTableXacNhanTangCa(dsnv, table);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TaoTableXacNhanTangCa Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tao Table Xac Nhan Piece100 Method Test
		/// Documentation   :  
		/// Method Signature:  void TaoTableXacNhanPC100(DataRow[] arrRow, DataTable table)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TaoTableXacNhanPC100Test() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			DataRow[] arrRow = new DataRow[0];
			DataTable table = new DataTable();

			XL.TaoTableXacNhanPC100(arrRow, table);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TaoTableXacNhanPC100 Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Kiemtra Dulieu Capnhat Tu Server Method Test
		/// Documentation   :  
		/// Method Signature:  bool KiemtraDulieuCapnhatTuServer(DateTime now)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void KiemtraDulieuCapnhatTuServerTest() {
			DateTime methodStartTime = DateTime.Now;
			bool expected = true;

			//Parameters
			DateTime now = new DateTime(1969, 7, 21);

			bool results = XL.KiemtraDulieuCapnhatTuServer(now);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.KiemtraDulieuCapnhatTuServer method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.KiemtraDulieuCapnhatTuServer Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Thong Ke Method Test
		/// Documentation   :  
		/// Method Signature:  void ThongKe(cUserInfo nhanvien)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void ThongKeTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			cUserInfo nhanvien = new cUserInfo();

			XL.ThongKe(nhanvien);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.ThongKe Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh Cong Cho Viec Method Test
		/// Documentation   :  
		/// Method Signature:  void TinhCongChoViec(cUserInfo nv, DateTime ngayDauThang)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhCongChoViecTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			cUserInfo nv = new cUserInfo();
			DateTime ngayDauThang = new DateTime(1969, 7, 21);

			XL.TinhCongChoViec(nv, ngayDauThang);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhCongChoViec Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Dem So Ngay CN Method Test
		/// Documentation   :  
		/// Method Signature:  int DemSoNgayCN(DateTime ngayDauThang)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void DemSoNgayCNTest() {
			DateTime methodStartTime = DateTime.Now;
			int expected = 123;

			//Parameters
			DateTime ngayDauThang = new DateTime(1969, 7, 21);

			int results = XL.DemSoNgayCN(ngayDauThang);
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.DemSoNgayCN method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.DemSoNgayCN Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh SP Lam Ra Cong Va Piece B102 Method Test
		/// Documentation   :  
		/// Method Signature:  void TinhSPLamRa_CongVaPC_B102(cUserInfo nv, out double spLamRa)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhSPLamRa_CongVaPC_B102Test() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			cUserInfo nv = new cUserInfo();
			double spLamRa = 3.14159265D;

			XL.TinhSPLamRa_CongVaPC_B102(nv, out spLamRa);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhSPLamRa_CongVaPC_B102 Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh Boi Duong Qua Dem A512 Method Test
		/// Documentation   :  
		/// Method Signature:  void TinhBoiDuongQuaDemA512(cUserInfo nv, double boiduongca3, out double boiduongca3_1nv)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhBoiDuongQuaDemA512Test() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			cUserInfo nv = new cUserInfo();
			const double boiduongca3 = 3.14159265D;
			double boiduongca3_1nv = 3.14159265D;

			XL.TinhBoiDuongQuaDemA512(nv, boiduongca3, out boiduongca3_1nv);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhBoiDuongQuaDemA512 Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh Luong Co Ban Cong Va Piece A202 Method Test
		/// Documentation   :  
		/// Method Signature:  void TinhLuongCoBan_CongVaPC_A202(cUserInfo nv, double luongtoithieu, out double luongcb1nv)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhLuongCoBan_CongVaPC_A202Test() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			cUserInfo nv = new cUserInfo();
			const double luongtoithieu = 3.14159265D;
			double luongcb1nv = 3.14159265D;

			XL.TinhLuongCoBan_CongVaPC_A202(nv, luongtoithieu, out luongcb1nv);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhLuongCoBan_CongVaPC_A202 Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh Khau Tru BH Cho NV Method Test
		/// Documentation   :  
		/// Method Signature:  void TinhKhauTruBHChoNV(List&lt;cUserInfo&gt; dsnv, Double mucluongtoithieu, Double mucdong)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhKhauTruBHChoNVTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cUserInfo> dsnv = new List<cUserInfo>();
			const Double mucluongtoithieu = 3.14159265D;
			const Double mucdong = 3.14159265D;

			XL.TinhKhauTruBHChoNV(dsnv, mucluongtoithieu, mucdong);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhKhauTruBHChoNV Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Doc Luong Dieu Chinh Method Test
		/// Documentation   :  
		/// Method Signature:  void DocLuongDieuChinh(DateTime thang, List&lt;cUserInfo&gt; dsnv, out double dTongLuongDieuChinh, out double dThuChiKhac)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void DocLuongDieuChinhTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			DateTime thang = new DateTime(1969, 7, 21);
			List<cUserInfo> dsnv = new List<cUserInfo>();
			double dTongLuongDieuChinh = 3.14159265D;
			double dThuChiKhac = 3.14159265D;

			XL.DocLuongDieuChinh(thang, dsnv, out dTongLuongDieuChinh, out dThuChiKhac);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.DocLuongDieuChinh Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Format Cells Method Test
		/// Documentation   :  
		/// Method Signature:  void FormatCells(ExcelRange cell, object value, bool wraptext = false, bool merge = false, bool bold = false, bool italic = false, int size = 0, bool VeBorder = true, ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin, ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Center, ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center, string numberFormat = null, int textRotation = 0)
		/// </summary>
		/// <summary>
		/// F Cong Method Test
		/// Documentation   :  
		/// Method Signature:  void FCong(ExcelRange cell1, ExcelRange cell2, ExcelRange cell3, ExcelRange cell123, object value1, object value2, object value3, ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thick, ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center, ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Right, string numberFormat =)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void Kiemtra_GioRaaNhoHonGioVaoTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			DateTime giovao1 = new DateTime(1969, 7, 21);
			List<cChkInOut> dsVaoRa = new List<cChkInOut>();
			int kq = 123;
			List<cChkInOut> gioAnhHuong = new List<cChkInOut>();
			string loai = "test";

			XL.Kiemtra_GioRaaNhoHonGioVao(giovao1, dsVaoRa, ref kq, ref gioAnhHuong, ref loai);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.Kiemtra_GioRaaNhoHonGioVao Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Kiemtra Gio Vao Nho Hon Gio Raa Method Test
		/// Documentation   :  
		/// Method Signature:  void Kiemtra_GioVaoNhoHonGioRaa(DateTime giovao1, List&lt;cChkInOut&gt; dsVaoRa, ref int kq, ref List&lt;cChkInOut&gt; gioAnhHuong, ref string loai)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void Kiemtra_GioVaoNhoHonGioRaaTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			DateTime giovao1 = new DateTime(1969, 7, 21);
			List<cChkInOut> dsVaoRa = new List<cChkInOut>();
			int kq = 123;
			List<cChkInOut> gioAnhHuong = new List<cChkInOut>();
			string loai = "test";

			XL.Kiemtra_GioVaoNhoHonGioRaa(giovao1, dsVaoRa, ref kq, ref gioAnhHuong, ref loai);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.Kiemtra_GioVaoNhoHonGioRaa Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tao Table Xem Cong Chinh Sua Hang Loat Method Test
		/// Documentation   :  
		/// Method Signature:  void TaoTableXemCong_ChinhSuaHangLoat(List&lt;cUserInfo&gt; dsnvXemCong, DataTable kq)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TaoTableXemCong_ChinhSuaHangLoatTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cUserInfo> dsnvXemCong = new List<cUserInfo>();
			DataTable kq = new DataTable();

			XL.TaoTableXemCong_ChinhSuaHangLoat(dsnvXemCong, kq);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TaoTableXemCong_ChinhSuaHangLoat Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tao Table Gio KDQD Chinh Sua Hang Loat Method Test
		/// Documentation   :  
		/// Method Signature:  void TaoTableGioKDQD_ChinhSuaHangLoat(List&lt;cUserInfo&gt; dsnvDiemDanh, DataTable kq)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TaoTableGioKDQD_ChinhSuaHangLoatTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cUserInfo> dsnvDiemDanh = new List<cUserInfo>();
			DataTable kq = new DataTable();

			XL.TaoTableGioKDQD_ChinhSuaHangLoat(dsnvDiemDanh, kq);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TaoTableGioKDQD_ChinhSuaHangLoat Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tao Table Thieu Cham Cong Method Test
		/// Documentation   :  
		/// Method Signature:  void TaoTableThieuChamCong(List&lt;cUserInfo&gt; kq, DataTable m_tableThieuChamCong)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TaoTableThieuChamCongTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cUserInfo> kq = new List<cUserInfo>();
			DataTable m_tableThieuChamCong = new DataTable();

			XL.TaoTableThieuChamCong(kq, m_tableThieuChamCong);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TaoTableThieuChamCong Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Kiemtra Tontai Gio Qua Dem Method Test
		/// Documentation   :  
		/// Method Signature:  void KiemtraTontaiGioQuaDem(cNgayCong ngayCongGoc, DateTime giovao)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void KiemtraTontaiGioQuaDemTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			cNgayCong ngayCongGoc = new cNgayCong();
			DateTime giovao = new DateTime(1969, 7, 21);

			XL.KiemtraTontaiGioQuaDem(ngayCongGoc, giovao);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.KiemtraTontaiGioQuaDem Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Sap Xep DS Check Method Test
		/// Documentation   :  
		/// Method Signature:  void SapXepDS_Check(List&lt;cChk&gt;[] list)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void SapXepDS_CheckTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			List<cChk>[] list = new List<cChk>[0];

			XL.SapXepDS_Check(list);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.SapXepDS_Check Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Check Tinh Piece50 Method Test
		/// Documentation   :  
		/// Method Signature:  void CheckTinhPC50(cUserInfo nhanvien, DateTime ngay, bool giatri)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void CheckTinhPC50Test() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			cUserInfo nhanvien = new cUserInfo();
			DateTime ngay = new DateTime(1969, 7, 21);
			const bool giatri = true;

			XL.CheckTinhPC50(nhanvien, ngay, giatri);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.CheckTinhPC50 Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Set Mac Dinh Tinh Piece50 Method Test
		/// Documentation   :  
		/// Method Signature:  void SetMacDinhTinhPC50(cUserInfo nhanvien, DateTime ngay)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void SetMacDinhTinhPC50Test() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			cUserInfo nhanvien = new cUserInfo();
			DateTime ngay = new DateTime(1969, 7, 21);

			XL.SetMacDinhTinhPC50(nhanvien, ngay);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.SetMacDinhTinhPC50 Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Tinh PieceodeB Method Test
		/// Documentation   :  
		/// Method Signature:  void TinhPCDB(cUserInfo nhanvien, DateTime ngay, int loai, int pc)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void TinhPCDBTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			cUserInfo nhanvien = new cUserInfo();
			DateTime ngay = new DateTime(1969, 7, 21);
			const int loai = 123;
			const int pc = 123;

			XL.TinhPCDB(nhanvien, ngay, loai, pc);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhPCDB Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Huy Bo Tinh PieceodeB Method Test
		/// Documentation   :  
		/// Method Signature:  void HuyBo_TinhPCDB(cUserInfo nhanvien, DateTime ngay)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void HuyBo_TinhPCDBTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			cUserInfo nhanvien = new cUserInfo();
			DateTime ngay = new DateTime(1969, 7, 21);

			XL.HuyBo_TinhPCDB(nhanvien, ngay);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.HuyBo_TinhPCDB Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Huy Bo Tinh Piece50 Method Test
		/// Documentation   :  
		/// Method Signature:  void HuyBo_TinhPC50(cUserInfo nhanvien, DateTime ngay)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void HuyBo_TinhPC50Test() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			cUserInfo nhanvien = new cUserInfo();
			DateTime ngay = new DateTime(1969, 7, 21);

			XL.HuyBo_TinhPC50(nhanvien, ngay);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.HuyBo_TinhPC50 Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Cap Nhat Luong Dieu Chinh Tam Ung Thu Chi Khac Method Test
		/// Documentation   :  
		/// Method Signature:  void CapNhatLuongDieuChinh_TamUng_ThuChiKhac(DataTable table, DateTime ngaydauthang)
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void CapNhatLuongDieuChinh_TamUng_ThuChiKhacTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters
			DataTable table = new DataTable();
			DateTime ngaydauthang = new DateTime(1969, 7, 21);

			XL.CapNhatLuongDieuChinh_TamUng_ThuChiKhac(table, ngaydauthang);

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.CapNhatLuongDieuChinh_TamUng_ThuChiKhac Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Doc Setting Method Test
		/// Documentation   :  
		/// Method Signature:  void DocSetting()
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void DocSettingTest() {
			DateTime methodStartTime = DateTime.Now;

			//Parameters

			XL.DocSetting();

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.DocSetting Time Elapsed: {0}", methodDuration));
		}

		/// <summary>
		/// Doc Thang Ket Cong Method Test
		/// Documentation   :  
		/// Method Signature:  DateTime DocThangKetCong()
		/// </summary>
		[Test]
		[Ignore("Please Implement")]
		public void DocThangKetCongTest() {
			DateTime methodStartTime = DateTime.Now;
			DateTime expected = new DateTime(1969, 7, 21);

			//Parameters

			DateTime results = XL.DocThangKetCong();
			Assert.AreEqual(expected, results, "ChamCong_v03.BUS.XL.DocThangKetCong method test failed");

			TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
			Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.DocThangKetCong Time Elapsed: {0}", methodDuration));
		}

		#endregion // End of GeneratedMethods

		#endregion

	}
}
*/
