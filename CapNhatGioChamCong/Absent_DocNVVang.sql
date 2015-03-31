IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Absent_DocNVVang')
DROP PROCEDURE Absent_DocNVVang
GO

CREATE PROCEDURE Absent_DocNVVang
	@ArrayMaCC IntArray readonly,
	@NgayBD datetime,
	@NgayKT datetime
AS
BEGIN
	SELECT      ID, UserInfo.UserEnrollNumber, UserInfo.UserFullCode, UserInfo.UserFullName
				, TimeDate, LoaiVang.AbsentCode, LoaiVang.AbsentDescription
				, Thang, Nam, Absent.Workingday, Absent.WorkingTime 
	FROM        Absent, LoaiVang , UserInfo 
	WHERE      (Absent.AbsentCode = LoaiVang.AbsentCode)
				and (UserInfo.UserEnrollNumber = Absent.UserEnrollNumber)
				and (TimeDate >= @NgayBD and TimeDate <= @NgayKT)
				and ( Absent.UserEnrollNumber in (select * from @ArrayMaCC) )
	ORDER BY    UserInfo.UserEnrollNumber ASC,Nam DESC, Thang DESC, TimeDate ASC 
END
GO
