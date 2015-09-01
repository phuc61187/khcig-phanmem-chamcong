IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Shift_DocTatCaShiftV6]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Shift_DocTatCaShiftV6]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Shift_DocTatCaShiftV6]
@Enable bit = null
AS
BEGIN
  SELECT	[Enable],ShiftID,ShiftCode,Onduty,Offduty,DayCount,OnTimeIn,OnTimeOut,CutIn,CutOut
			,OnLunch,OffLunch, 
			DATEDIFF(MINUTE, 0, Shifts6.Onduty) as OndutyMinute, 
			DATEDIFF(MINUTE, 0, Shifts6.Offduty) as OffdutyMinute
			,WorkingTime,Workingday,LateGrace,EarlyGrace,AfterOT,KyHieuCC
			,Description
			,StartNT,EndNT
	FROM	Shifts6
	where	@Enable is null or [Enable] = @Enable
group by 	[Enable],Onduty,Offduty,ShiftID,ShiftCode,DayCount,OnTimeIn,OnTimeOut,CutIn,CutOut
			,OnLunch,OffLunch
			,WorkingTime,Workingday,LateGrace,EarlyGrace,AfterOT,KyHieuCC
			,Description
			,StartNT,EndNT
										
END

GO


Central Topic
	Datetime Vào
	Datetime ra
	DateTime BDLV
	DateTime KTLV Trong Ca
	Datetime KTLV
	datetime BDLV Ca3
	DateTime KTLV Ca3
	TimeSpan VaoTre
	TimeSpan RaSom
	Công Trễ
	Công Sớm
	Công trong ca
	công ngoài ca
	Định mức công
	Floating Topic
		TGLVNgoaiCa
		TGLVTrongCa
Datetime Vào
Datetime ra
DateTime BDLV
DateTime KTLV Trong Ca
Datetime KTLV
datetime BDLV Ca3
DateTime KTLV Ca3
TimeSpan VaoTre
TimeSpan RaSom
Công Trễ
Công Sớm
Công trong ca
công ngoài ca
Định mức công

