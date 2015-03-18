#region Includes
using System;
using System.Collections.Generic;
using System.Text;
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
// 7/8/2014 1:09 PM  Administrator KH     Module created.
///////////////////////////////////////////////////////////////////////////////
/*
namespace ChamCong_v03.BUS
{

    /// <summary>
    /// Tests for the XL Class
    /// Documentation: 
    /// </summary>
    [TestFixture, Description("Tests for XL")]
    public class XLTest
    {
        #region Class Variables
        #endregion

        #region Setup/Teardown

        /// <summary>
        /// Code that is run once for a suite of tests
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {

        }

        /// <summary>
        /// Code that is run once after a suite of tests has finished executing
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {

        }

        /// <summary>
        /// Code that is run before each test
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            //New instance of XL
        }

        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
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
        /// Tinh PieceodeB Method Test
        /// Documentation   :  
        /// Method Signature:  void TinhPCDB(TimeSpan SoGioTinhCong, TimeSpan SoGioLamDemmm, bool QuaDem, bool TinhPCDB, int loaiPC, float pcngay, float pcdem, out TimeSpan tgTinh200, out TimeSpan tgTinh260, out TimeSpan tgTinh300, out TimeSpan tgTinh390, out TimeSpan tgTinhCus, out double PhuCap200, out double PhuCap260, out double PhuCap300, out double PhuCap390, out double PhuCapCus, ref double TongPhuCap)
        /// </summary>
        [Test]
        public void TinhPCDBTest()
        {
            DateTime methodStartTime = DateTime.Now;
            
            //Parameters
            TimeSpan SoGioTinhCong = new TimeSpan(9,30,0);
            TimeSpan SoGioLamDemmm = new TimeSpan(0,0,0);
            const bool QuaDem = true;
            const bool TinhPCDB = true;
            const int loaiPC = 2;
            const float pcngay = 250;
            const float pcdem = 290f;
            TimeSpan tgTinh200 = new TimeSpan();
            TimeSpan tgTinh260 = new TimeSpan();
            TimeSpan tgTinh300 = new TimeSpan();
            TimeSpan tgTinh390 = new TimeSpan();
            TimeSpan tgTinhCus = new TimeSpan();
            double PhuCap200 = 3.14159265D;
            double PhuCap260 = 3.14159265D;
            double PhuCap300 = 3.14159265D;
            double PhuCap390 = 3.14159265D;
            double PhuCapCus = 3.14159265D;
            double TongPhuCap = 3.14159265D;

            XL.TinhPCDB(SoGioTinhCong, SoGioLamDemmm, QuaDem, TinhPCDB, loaiPC, pcngay, pcdem, out tgTinh200, out tgTinh260, out tgTinh300, out tgTinh390, out tgTinhCus, out PhuCap200, out PhuCap260, out PhuCap300, out PhuCap390, out PhuCapCus, ref TongPhuCap);
			string temp = "SoGioTinhCong {0}, SoGioLamDemmm {1}, QuaDem {2}, TinhPCDB {3}, loaiPC {4}," +
						  "\npcngay {5}, pcdem {6}, " +
			              "\nout tgTinh200 {7}, out tgTinh260 {8}, out tgTinh300 {9}, out tgTinh390 {10}, out tgTinhCus {11}, " +
			              "\nout PhuCap200 {12}, out PhuCap260 {13}, out PhuCap300 {14}, out PhuCap390 {15}, " +
			              "\nout PhuCapCus {16}, ref TongPhuCap {17}";
	        temp = string.Format(temp, SoGioTinhCong, SoGioLamDemmm, QuaDem, TinhPCDB, loaiPC,
	                      pcngay, pcdem,
	                      tgTinh200, tgTinh260, tgTinh300, tgTinh390, tgTinhCus,
	                      PhuCap200, PhuCap260, PhuCap300, PhuCap390, PhuCapCus, TongPhuCap);
            Console.WriteLine(temp);
        }

        /// <summary>
        /// Tinh Lai Phu Cap TC Method Test
        /// Documentation   :  
        /// Method Signature:  void TinhLaiPhuCapTC(List&lt;cTemp1&gt; dsXacNhanPC, List&lt;cNgayCong&gt; dsNgayCong)
        /// </summary>
        [Test]
        public void TinhLaiPhuCapTCTest()
        {
            DateTime methodStartTime = DateTime.Now;
            
            //Parameters
            List<cTemp1> dsXacNhanPC = new List<cTemp1>();
            List<cNgayCong> dsNgayCong = new List<cNgayCong>();

            XL.TinhLaiPhuCapTC(dsXacNhanPC, dsNgayCong);

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhLaiPhuCapTC Time Elapsed: {0}", methodDuration));
        }

        /// <summary>
        /// Tinh Lai Phu Cap Database Method Test
        /// Documentation   :  
        /// Method Signature:  void TinhLaiPhuCapDB(List&lt;cTemp&gt; dsXacNhanPC, List&lt;cNgayCong&gt; dsNgayCong)
        /// </summary>
        [Test]
        public void TinhLaiPhuCapDBTest()
        {
            DateTime methodStartTime = DateTime.Now;
            
            //Parameters
            List<cTemp> dsXacNhanPC = new List<cTemp>();
            List<cNgayCong> dsNgayCong = new List<cNgayCong>();

            XL.TinhLaiPhuCapDB(dsXacNhanPC, dsNgayCong);

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("ChamCong_v03.BUS.XL.TinhLaiPhuCapDB Time Elapsed: {0}", methodDuration));
        }

        #endregion // End of GeneratedMethods

        #endregion

    }
}
*/
