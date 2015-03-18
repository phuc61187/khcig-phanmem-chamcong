using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GiuLaiCacFileCu.DAO;
using GiuLaiCacFileCu.DTO;

namespace GiuLaiCacFileCu {
	public class ThamSo {
		public static int currUserID = int.MinValue;
		public static string currUserAccount = string.Empty;

		public static readonly TimeSpan _0gio = TimeSpan.Zero;
		public static readonly TimeSpan _05phut = new TimeSpan(0, 5, 0);
		public static readonly TimeSpan _10phut = new TimeSpan(0, 10, 0);
		public static readonly TimeSpan _30phut = new TimeSpan(0, 30, 0);
		public static readonly TimeSpan _4gio = new TimeSpan(4, 0, 0);
		public static readonly TimeSpan _7gio45ph = new TimeSpan(7, 45, 0);
		public static readonly TimeSpan _8gio = new TimeSpan(8, 0, 0);
		public static readonly TimeSpan _8gio1giay = new TimeSpan(8, 0, 1);
		public static readonly TimeSpan _16gio = new TimeSpan(16, 0, 0);
		public static readonly TimeSpan _4h30 = new TimeSpan(4, 30, 0);
		public static readonly TimeSpan _05h45 = new TimeSpan(5, 45, 0);
		public static readonly TimeSpan _13h45 = new TimeSpan(13, 45, 0);
		public static readonly TimeSpan _20h00 = new TimeSpan(20, 0, 0);
		public static readonly TimeSpan _21h45 = new TimeSpan(21, 45, 0);
		public static readonly TimeSpan _24h00 = new TimeSpan(24, 00, 0);
		public static readonly TimeSpan _1ngay = new TimeSpan(1, 0, 0, 0);
		public static readonly TimeSpan _12gio = new TimeSpan(12, 0, 0);
		public static readonly TimeSpan _02tieng = new TimeSpan(2, 0, 0);



		public const string nameVao = "TimeStrVao";
		public const string nameRa = "TimeStrRa";
		public const string nameNgay = "TimeStrNgay";
		public const string nameThu = "TimeStrThu";
		public const string prefixColNameGrid1 = "grid1";
		public const string prefixColNameGrid2 = "grid2";
		public const string prefixColNameGrid3 = "grid3";
		public static int _maxPhutLamThem = 0;

		#region query string

		public static string queryLogIn = @" select UserAccount, UserID from [dbo].[NewUserAccount] where UserAccount = @UserAccount and Password = @Password ";

		public static string queryDSCa = @" SELECT [ShiftID],[ShiftCode],[Onduty],[Offduty],[DayCount],[OnTimeIn],[OnTimeOut],[CutIn],[CutOut]
      ,[OnLunch],[OffLunch], DATEDIFF(MINUTE, 0, Shifts.Onduty) as OndutyMinute, DATEDIFF(MINUTE, 0, Shifts.Offduty) as OffdutyMinute
      ,[WorkingTime],[Workingday],[IsNightTime],[StartNT],[EndNT],[NoOutWT],[NoInWT],[LateGrace],[IsLateGrace],[EarlyGrace],[IsEarlyGrace]
      ,[IsOT],[OTlevel],[IsSunOT],[SunOTLevel],[IsBeforeOT],[BeforeOT],[IsAfterOT],[AfterOT],[AfterOTTotal],[AfterOTDeduce],[BeforeOTTotal],[BeforeOTDeduce],[LevelOT1],[LevelOT2],[IsOTPoint],[OTPoint],[IsHolidayOT],[HolidayOTLevel],[IsLate],[IsEarly],[WKOTLevel],[IsWKOTLevel],[ShowPosition]
       FROM [Shifts] group by [Onduty],[Offduty],[ShiftID],[ShiftCode],[DayCount],[OnTimeIn],[OnTimeOut],[CutIn],[CutOut],[OnLunch],[OffLunch],[WorkingTime],[Workingday],[IsNightTime],[StartNT],[EndNT],[NoOutWT],[NoInWT],[LateGrace],[IsLateGrace],[EarlyGrace],[IsEarlyGrace],[IsOT],[OTlevel],[IsSunOT],[SunOTLevel],[IsBeforeOT],[BeforeOT],[IsAfterOT],[AfterOT],[AfterOTTotal],[AfterOTDeduce],[BeforeOTTotal],[BeforeOTDeduce],[LevelOT1],[LevelOT2],[IsOTPoint],[OTPoint],[IsHolidayOT],[HolidayOTLevel],[IsLate],[IsEarly],[WKOTLevel],[IsWKOTLevel],[ShowPosition]";

		public static string queryDSLichTrinh = @"  SELECT      [ID],    [SchID],    [T1],[T2],[T3],[T4],[T5],[T6],[T7]
                                                    FROM        [dbo].[ShiftSch]
                                                    group by    [SchID],             [T1],[T2],[T3],[T4],[T5],[T6],[T7],    [ID]";

		public static string queryDSNVThaoTac = @" select UserInfo.UserFullCode, UserInfo.UserFullName ,UserInfo.UserEnrollNumber 
                                    , UserInfo.SchID, Schedule.SchName
                                    , Title.TitleName
                                    , UserInfo.UserIDD
									, HeSoLuongCB, HeSoLuongSP
                                    , RelationDept.Description, RelationDept.TinhPC150
            from UserInfo , RelationDept, Schedule, Title 
            where UserInfo.UserIDD IN ( select dp.IDD from DeptPrivilege dp 
                                                         where dp.UserID = @UserID and IsYes = @IsYes )
			                       and (RelationDept.ID = UserInfo.UserIDD)
                                   and (UserInfo.SchID = Schedule.SchID)
                                   and (UserIDTitle = IDT)

                                    union  select UserInfo.UserFullCode, UserInfo.UserFullName, UserInfo.UserEnrollNumber
                                                                        , UserInfo.SchID, Schedule.SchName
                                                                        , N'Chưa SX'
                                                                        , UserInfo.UserIDD
																		, HeSoLuongCB, HeSoLuongSP
                                                                        , RelationDept.[Description], RelationDept.TinhPC150	 
                                    from UserInfo , RelationDept, Schedule where UserInfo.UserIDD IN ( select dp.IDD from DeptPrivilege dp 
                                                                                             where dp.UserID = @UserID and IsYes = @IsYes )
			                                                           and (RelationDept.ID = UserInfo.UserIDD)
                                                                       and (UserInfo.SchID = Schedule.SchID) and UserIDTitle = 0  ";




		public static string SelStrLayDSNV() {
			return @"	select UserInfo.UserFullCode, UserInfo.UserFullName ,UserInfo.UserEnrollNumber 
                                    , UserInfo.SchID, Schedule.SchName
                                    , UserInfo.UserIDD
									, HeSoLuongCB, HeSoLuongSP
                                    , RelationDept.Description, RelationDept.TinhPC150
						from UserInfo , RelationDept, Schedule
						where  (RelationDept.ID = UserInfo.UserIDD)
												and (UserInfo.SchID = Schedule.SchID) and UserEnabled = 1";
		}

		public static string queryPhanQuyenMenu = @" select distinct top 1 * from MenuPrivilege where MenuPrivilege.UserID = @UserID and MenuID=1006 
                            union  
                            select distinct top 1 * from MenuPrivilege where MenuPrivilege.UserID = @UserID and MenuID=1008 
                            union  
                            select distinct top 1 * from MenuPrivilege where MenuPrivilege.UserID = @UserID and MenuID=1009 
                            union  
                            select distinct top 1 * from MenuPrivilege where MenuPrivilege.UserID = @UserID and MenuID=4001 
                            union  
                            select distinct top 1 * from MenuPrivilege where MenuPrivilege.UserID = @UserID and MenuID=5001 
                            union  
                            select distinct top 1 * from MenuPrivilege where MenuPrivilege.UserID = @UserID and MenuID=5002 
                            order by MenuID ASC, IsYes ASC";

		public static string queryDSPhongBan = @"   Select      ID, RelationID, Description 
												    from        DeptPrivilege, RelationDept 
												    where       DeptPrivilege.UserID = @UserID and DeptPrivilege.IsYes = @IsYes and DeptPrivilege.IDD = RelationDept.ID";
		public static string SelGioDaXL = @"SELECT	  ID, UserEnrollNumber, ShiftID,  Onduty,  Offduty,
													  DayCount,  WorkingTime,  Workingday, TimeStrIn,  TimeStrOut,LateMin ,EarlyMin,  OTMin
											FROM	  dbo.XacNhanCaVaLamThem 
											where	  ID = {0} 
											group by  UserEnrollNumber, TimeStrIn,  TimeStrOut, ID, ShiftID,  Onduty,  Offduty,
													  DayCount,  WorkingTime,  Workingday, LateMin ,EarlyMin,  OTMin
											order by  UserEnrollNumber asc, TimeStrIn asc";
		public static int tempdem = 0;
		/// <summary>
		/// UserEnrollNumber,  ShiftID,   Onduty,   Offduty,   DayCount,   WorkingTime,   Workingday, TimeStrIn,   TimeStrOut,   OTMin,   Note
		/// </summary>
		public static string InsStrXacNhanCa() {
			return @"INSERT INTO  dbo.XacNhanCaVaLamThem (  UserEnrollNumber, ShiftID,      ShiftCode,    Onduty,      Offduty,   
															DayCount,         WorkingTime,  Workingday,   TinhPC150,
															LateGrace,        EarlyGrace,   AfterOT,      TimeStrIn,   TimeStrOut,  OTMin,   Note )
							VALUES						 ( @UserEnrollNumber,@ShiftID,     @ShiftCode,   @Onduty,     @Offduty,  
                                                           @DayCount,        @WorkingTime, @Workingday,  @TinhPC150,
														   @LateGrace,       @EarlyGrace,  @AfterOT,     @TimeStrIn,  @TimeStrOut, @OTMin,  @Note ) ";
		}
		/// <summary>
		/// UserEnrollNumber, TimeStrIn, TimeStrOut
		/// </summary>
		/// <returns></returns>
		public static string SelIDXacNhanCa() {
			return @"SELECT ID 
					 FROM  dbo.XacNhanCaVaLamThem 
					 where   UserEnrollNumber = @UserEnrollNumber and TimeStrIn = @TimeStrIn and  TimeStrOut = @TimeStrOut";
		}

		/// <summary>
		/// (@UserEnrollNumber, @TimeDateNew, @TimeStrOld, @TimeStrNew, @SourceNew, @MachineNoOld, @MachineNoNew, @IDXacNhanCaVaLamThem)
		/// </summary>
		/// <returns></returns>
		public static string UpdStrXacNhanCa() {
			return @"	UPDATE   dbo.CheckInOut  
						SET 
							Source = @SourceNew,
							MachineNo = @MachineNoNew,
							IDXacNhanCaVaLamThem = @IDXacNhanCaVaLamThem 
						WHERE   
							UserEnrollNumber = @UserEnrollNumber
							and TimeStr = @TimeStrOld
							and MachineNo = @MachineNoOld";
		}

		/// <summary>
		/// (UserEnrollNumber, TimeDate, TimeStr,  Source, MachineNo, WorkCode)
		/// </summary>
		/// <returns></returns>
		public static string InsStrThemGioVaoRa() {
			return @"	INSERT INTO CheckInOut (UserEnrollNumber, TimeDate, TimeStr, OriginType, Source, MachineNo, WorkCode ) 
									 VALUES (@UserEnrollNumber,  @TimeDate,@TimeStr,@OriginType,@Source,@MachineNo,@WorkCode )
						INSERT INTO LichSuSuaGioVaoRa
						( UserEnrollNumber,  TimeStrOld,  TimeStrNew,  
						  OriginTypeOld,  OriginTypeNew,  SourceOld,  SourceNew,
						  MachineNoOld,  MachineNoNew,
						  UserID,  Explain,  Note,
						  ExecuteTime,  CommandType		) 
						VALUES 
						( @UserEnrollNumber,  @TimeStr,  @TimeStr,
						  @OriginType, @OriginType, @Source,@Source, 
						  @MachineNo,@MachineNo,@UserID,  @Explain,  @Note,
						  GetDate(),  @CommandType		) ";
		}

		/// <summary>
		/// (UserEnrollNumber, TimeStrOld, TimeDateNew, TimeStrNew, SourceNew, MachineNoNew )
		/// </summary>
		/// <returns></returns>
		public static string UpdStrSuaGioVaoRa() {
			return @"update CheckInOut set TimeDate = @TimeDateNew, TimeStr = @TimeStrNew, Source = @SourceNew, MachineNo = @MachineNoNew
					 where UserEnrollNumber = @UserEnrollNumber and TimeStr = @TimeStrOld";
		}

		/// <summary>
		/// (UserEnrollNumber, TimeStrOld, MachineNoOld )
		/// </summary>
		/// <returns></returns>
		public static string DelStrXoaGioVaoRa() {
			return @"	delete from CheckInOut where UserEnrollNumber = @UserEnrollNumber and TimeStr = @TimeStrOld and MachineNo = @MachineNoOld

						INSERT INTO LichSuSuaGioVaoRa
						( UserEnrollNumber,  TimeStrOld,  TimeStrNew,  
						  OriginTypeOld,  OriginTypeNew,  SourceOld,  SourceNew,
						  MachineNoOld,  MachineNoNew,
						  UserID,  Explain,  Note,
						  ExecuteTime,  CommandType		) 
						VALUES 
						( @UserEnrollNumber,  @TimeStrOld,  @TimeStrOld,
						  @OriginType, @OriginType, @Source,@Source, 
						  @MachineNo,@MachineNo,@UserID,  @Explain,  @Note,
						  GetDate(),  1		) 
";
		}

		/// <summary>
		/// (UserEnrollNumber, TimeStrOld, TimeStrNew, SourceOld, SourceNew, MachineNoOld, MachineNoNew, UserID, Explain, Note, CommandType, ExecuteTime(ko cần truyền vì đã lấy giờ của server) ) 
		/// </summary>
		/// <returns></returns>
		public static string InsStrBackupThemGioVaoRa() {
			return @"INSERT INTO LichSuSuaGioVaoRa (UserEnrollNumber, TimeStrOld, TimeStrNew, SourceOld, SourceNew, MachineNoOld, MachineNoNew
                               , UserID, Explain, Note, CommandType, ExecuteTime ) 
											VALUES (@UserEnrollNumber,@TimeStrOld,@TimeStrNew,@SourceOld,@SourceNew,@MachineNoOld,@MachineNoNew
                               ,@UserID,@Explain,@Note,@CommandType,GetDate()  )"; //lấy giờ của sql server chứ ko lấy giờ client
		}

		public static string InsStrThemNgayVang() {
			return @"INSERT INTO Absent (UserEnrollNumber,   UserFullCode,  TimeDate,  AbsentCode, Thang, Nam, Workingday, WorkingTime) 
                                 VALUES (@UserEnrollNumber, @UserFullCode, @TimeDate, @AbsentCode,@Thang,@Nam,@Workingday,@WorkingTime)";
		}

		public static string SelStrLayDSNgayVang() {
			return @"SELECT      ID, UserInfo.UserEnrollNumber, UserInfo.UserFullCode, UserInfo.UserFullName, TimeDate, LoaiVang.AbsentCode, LoaiVang.AbsentSymbol,LoaiVang.AbsentDescription, Thang, Nam, Absent.Workingday, Absent.WorkingTime 
                     FROM        Absent, LoaiVang , UserInfo 
                     WHERE      (Absent.AbsentCode = LoaiVang.AbsentCode)
                            and (UserInfo.UserEnrollNumber = Absent.UserEnrollNumber)
                            and (TimeDate between @NgayBD and @NgayKT)
                            and (UserInfo.UserEnrollNumber = {0} )
                     ORDER BY    UserInfo.UserEnrollNumber ASC,Nam DESC, Thang DESC, TimeDate ASC ";
		}


		public static string DelStrXoaNgayVang() { return @"DELETE FROM Absent WHERE       ID = @ID"; }

		public static string SelStrLayDSChamCongTay() {
			return @"SELECT		ID, UserEnrollNumber, Ngay, Cong, PhuCap 
					 FROM		ChamCongTay
					 WHERE		( Ngay between @NgayBD and @NgayKT )  and ( UserEnrollNumber = {0} )
					 order by	UserEnrollNumber asc, Ngay asc";
		}
		public static string InsStrThemChamCongTay() {
			return @"	INSERT INTO ChamCongTay (UserEnrollNumber, Ngay, Cong, PhuCap) 
						VALUES (@UserEnrollNumber, @Ngay, @Cong, @PhuCap)";
		}


		public static string InsStrThemLVNgayNghi() {
			return @"	INSERT INTO LamViecNgayNghi (UserEnrollNumber, Ngay, HeSoPC)
						VALUES (@UserEnrollNumber, @Ngay, @HeSoPC)";
		}



		public static string SelStrLayDSNgayNghiDaKhaiBao() {
			return @"SELECT		ID, LamViecNgayNghi.UserEnrollNumber, UserFullName, Ngay, HeSoPC, 
								case when HeSoPC = 1 then N'100% Lương' 
									 when HeSoPC = 2 then N'200% Lương'
									 when HeSoPC = 0.5 then N'50% Lương'
								end as PhanTramHeSo 
					 FROM		LamViecNgayNghi, UserInfo
					 WHERE		( Ngay between @NgayBD and @NgayKT )  and ( LamViecNgayNghi.UserEnrollNumber = {0} )
							and UserInfo.UserEnrollNumber =  LamViecNgayNghi.UserEnrollNumber
					 order by	LamViecNgayNghi.UserEnrollNumber asc, Ngay asc";
		}


		internal static string DelStrXoaLamViecNgayNghi() {
			return @"DELETE FROM LamViecNgayNghi WHERE       ID = @ID";
		}

		internal static string SelLichSuSuaGioChamCong() {
			return @"SELECT   UserInfo.UserEnrollNumber,UserInfo.UserFullName,
                              TimeStrOld,TimeStrNew,
                              SourceOld,SourceNew,MachineNoOld,MachineNoNew,
                              NewUserAccount.UserID,NewUserAccount.UserAccount, 
                              Explain,Note,ExecuteTime,CommandType
                     FROM     LichSuSuaGioVaoRa, UserInfo, NewUserAccount 
                     where    UserInfo.UserEnrollNumber = LichSuSuaGioVaoRa.UserEnrollNumber
                          and LichSuSuaGioVaoRa.UserID = NewUserAccount.UserID
                          and NewUserAccount.UserID = @UserID
                     order by ExecuteTime desc";
		}


		public static string SelStrLayDSNVQL() {
			return @"   select UserEnrollNumber, UserFullName, UserFullCode, UserInfo.SchID, TitleName
                        from UserInfo, Title
                        where UserInfo.UserIDTitle = Title.IDT and (UserIDD = 16 or UserIDD = 299 or UserIDD = 300)
						order by TitleName";
		}

		public static string InsStrChamCongTay() {
			return @"	declare   @ID int ; 
						INSERT INTO XacNhanCaVaLamThem
						( UserEnrollNumber,  ShiftID,  ShiftCode,
						  Onduty,  Offduty,  LateGrace,  EarlyGrace,  AfterOT,
						  DayCount,  WorkingTime,  Workingday,
						  TimeStrIn,  TimeStrOut,
						  OTMin,  TinhPC150,  Note ) 
						VALUES 
						( @UserEnrollNumber,  @ShiftID,  @ShiftCode,
						  @Onduty,  @Offduty,  @LateGrace,  @EarlyGrace,  @AfterOT,
						  @DayCount,  @WorkingTime,  @Workingday,
						  @TimeStrIn,  @TimeStrOut,
						  @OTMin,  @TinhPC150,  @Note )

						select @ID = @@Identity 
  
						INSERT INTO CheckInOut
						( UserEnrollNumber,  TimeDate,  TimeStr,
						  OriginType, Source, MachineNo,  WorkCode,  IDXacNhanCaVaLamThem ) 
						VALUES 
						( @UserEnrollNumber,  @TimeDateIn,  @TimeStrIn,
						  N'I',  N'PC',  21, 0,  @ID)

						INSERT INTO CheckInOut
						( UserEnrollNumber,  TimeDate,  TimeStr,
						  OriginType, Source, MachineNo,  WorkCode,  IDXacNhanCaVaLamThem ) 
						VALUES 
						( @UserEnrollNumber,  @TimeDateOut,  @TimeStrOut,
						  N'O',  N'PC',  22, 0,  @ID)

						INSERT INTO LichSuSuaGioVaoRa
						( UserEnrollNumber,  TimeStrOld,  TimeStrNew,  
						  OriginTypeOld,  OriginTypeNew,  SourceOld,  SourceNew,
						  MachineNoOld,  MachineNoNew,
						  UserID,  Explain,  Note,
						  ExecuteTime,  CommandType
						) 
						VALUES 
						( @UserEnrollNumber,  @TimeStrIn,  @TimeStrIn,
						  N'I',N'I',  N'PC', N'PC', 21,21, 
						  @UserID,  @Explain,  @Note,
						  GetDate(),  1)

						INSERT INTO LichSuSuaGioVaoRa
						( UserEnrollNumber,  TimeStrOld,  TimeStrNew,  
						  OriginTypeOld,  OriginTypeNew,  SourceOld,  SourceNew,
						  MachineNoOld,  MachineNoNew,
						  UserID,  Explain,  Note,
						  ExecuteTime,  CommandType
						) 
						VALUES 
						( @UserEnrollNumber,  @TimeStrOut,  @TimeStrOut,
						  N'O',N'O',  N'PC', N'PC', 22,22, 
						  @UserID,  @Explain,  @Note,
						  GetDate(),  1)
						 ";
		}

		#endregion

		public static List<cShift> DSCa { get; set; }
		public static List<cShiftSchedule> DSLichTrinh { get; set; }
		public static DataTable TablePhongBan { get; set; }
		public static DataTable DataTableDSNV { get; set; }






		public static void VeCheckBox_CheckAll(DataGridView grid, CheckBox checkBox, EventHandler checkAll_CheckedChanged, Point location) {
			Rectangle rect = grid.GetCellDisplayRectangle(0, -1, true);
			checkBox.Size = new Size(18, 18); checkBox.Location = new Point(rect.Location.X + location.X, rect.Location.Y + location.Y);
			checkBox.CheckedChanged += checkAll_CheckedChanged;
			grid.Controls.Add(checkBox);
		}

		/// <summary>
		/// Tạo DS Ca mở rộng. Đã bao gồm item Khác(int.MinValue)
		/// </summary>
		/// <param name="tmpDSCa"></param>
		/// <returns></returns>
		internal static List<cShift> TaoDSCaMoRong(List<cShift> tmpDSCa) {
			List<cShift> kq = new List<cShift>(tmpDSCa);
			List<cShift> DSCa1cong = tmpDSCa.FindAll(item => item.Workingday == 1f);
			List<cShift> DSCaNuaCong = tmpDSCa.FindAll(item => item.Workingday == 0.5f);
			if (DSCaNuaCong.Count == 0 || DSCa1cong.Count == 0) goto Outter;

			foreach (cShift ca1Cong in DSCa1cong) {
				foreach (cShift caNuaCong in DSCaNuaCong) {
					if (ca1Cong.OffDutyTS == caNuaCong.OnnDutyTS
						|| (ca1Cong.DayCount == 1 && ca1Cong.OffDutyTS.Add(new TimeSpan(-ca1Cong.DayCount, 0, 0, 0)) == caNuaCong.OnnDutyTS)) {
						#region collapse
						cShift tempShift = new cShift() {
							LoaiCa = 0,
							ShiftID = -ca1Cong.ShiftID,
							ShiftCode = ca1Cong.ShiftCode + " & " + caNuaCong.ShiftCode,
							DayCount = ca1Cong.DayCount + caNuaCong.DayCount, QuaDem = ((ca1Cong.DayCount + caNuaCong.DayCount) == 1),
							OnnDutyTS = ca1Cong.OnnDutyTS,
							OffDutyTS = caNuaCong.OffDutyTS,
							OnTimeInTS = ca1Cong.OnTimeInTS,
							CutInTS = ca1Cong.CutInTS,
							OnTimeOutTS = caNuaCong.OnTimeOutTS,
							CutOutTS = caNuaCong.CutOutTS,
							AfterOTTS = caNuaCong.AfterOTTS,
							LateGraceTS = ca1Cong.LateGraceTS,
							EarlyGraceTS = caNuaCong.EarlyGraceTS,
							Workingday = ca1Cong.Workingday + caNuaCong.Workingday,
							ShowPosition = ca1Cong.ShowPosition,
							WorkingTimeTS = ca1Cong.WorkingTimeTS + caNuaCong.WorkingTimeTS,
							chophepvaotreTS = ca1Cong.OnnDutyTS + ca1Cong.LateGraceTS,
							chopheprasomTS = caNuaCong.OffDutyTS - caNuaCong.EarlyGraceTS,
							batdaulamthemTS = caNuaCong.OffDutyTS + caNuaCong.AfterOTTS,
							LunchMinute = ca1Cong.LunchMinute + caNuaCong.LunchMinute,
						};
						#endregion
						kq.Add(tempShift);
					}
					else if (caNuaCong.OffDutyTS == ca1Cong.OnnDutyTS) {
						#region collapse
						cShift tempShift = new cShift() {
							LoaiCa = 0,
							ShiftID = -caNuaCong.ShiftID,
							ShiftCode = caNuaCong.ShiftCode + " & " + ca1Cong.ShiftCode,
							DayCount = caNuaCong.DayCount + ca1Cong.DayCount, QuaDem = ((ca1Cong.DayCount + caNuaCong.DayCount) == 1),
							OnnDutyTS = caNuaCong.OnnDutyTS,
							OffDutyTS = ca1Cong.OffDutyTS,
							OnTimeInTS = caNuaCong.OnTimeInTS,
							CutInTS = caNuaCong.CutInTS,
							OnTimeOutTS = ca1Cong.OnTimeOutTS,
							CutOutTS = ca1Cong.CutOutTS,
							AfterOTTS = ca1Cong.AfterOTTS,
							LateGraceTS = caNuaCong.LateGraceTS,
							EarlyGraceTS = ca1Cong.EarlyGraceTS,
							Workingday = caNuaCong.Workingday + ca1Cong.Workingday,
							ShowPosition = caNuaCong.ShowPosition,
							WorkingTimeTS = caNuaCong.WorkingTimeTS + ca1Cong.WorkingTimeTS,
							chophepvaotreTS = caNuaCong.OnnDutyTS + caNuaCong.LateGraceTS,
							chopheprasomTS = ca1Cong.OffDutyTS - ca1Cong.EarlyGraceTS,
							batdaulamthemTS = ca1Cong.OffDutyTS + ca1Cong.AfterOTTS,
							LunchMinute = ca1Cong.LunchMinute + caNuaCong.LunchMinute,
						};
						#endregion
						kq.Add(tempShift);
					}

					else continue;
				}
			}
		Outter:
			//kq.Insert(0, new cShift() { ShiftCode = "Mặc định", ShiftID = int.MinValue, LoaiCa = 1 });
			//kq.Insert(0, new cShift() { ShiftCode = "Ca dài A", ShiftID = int.MinValue + 1, LoaiCa = 1 });
			//kq.Insert(1, new cShift() { ShiftCode = "Ca dài B", ShiftID = int.MinValue + 2, LoaiCa = 1 });
			return kq;
		}

		internal static DateTime GetDate(DateTime tmpTimeStrVao) {
			return tmpTimeStrVao.TimeOfDay > ThamSo._4h30 ? tmpTimeStrVao.Date : tmpTimeStrVao.Date.Subtract(ThamSo._1ngay);
		}



		internal static DataTable TaoTable(string[] p, Type[] type) {
			DataTable dt = new DataTable();
			for (int i = 0; i < p.Length; i++) {
				dt.Columns.Add(p[i], (type[i]));
			}
			return dt;
		}






		public static string InsStrDieuChinhLuong() {
			return @"   UPDATE DieuChinhLuongThangTruoc SET LuongDieuChinh = @LuongDieuChinh
                        WHERE UserEnrollNumber = @UserEnrollNumber and Thang = @Thang and Nam=@Nam

                        IF @@ROWCOUNT=0
                            INSERT INTO DieuChinhLuongThangTruoc(UserEnrollNumber,Thang,Nam,LuongDieuChinh) 
                            VALUES (@UserEnrollNumber,@Thang,@Nam,@LuongDieuChinh) ";
		}



	}
}
