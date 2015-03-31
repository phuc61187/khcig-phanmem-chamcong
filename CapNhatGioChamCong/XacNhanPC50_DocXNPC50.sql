IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'XacNhanPC50_DocXNPC50')
DROP PROCEDURE XacNhanPC50_DocXNPC50
GO
create proc XacNhanPC50_DocXNPC50
	@ArrayMaCC IntArray readonly,
	@NgayBD datetime,
	@NgayKT datetime
as
begin
	SELECT      UserEnrollNumber, Ngay, TinhPC50
	FROM        XacNhanPC50
	WHERE       Ngay >= @NgayBD and Ngay <= @NgayKT 
				and ( UserEnrollNumber in (select * from @ArrayMaCC) )
	ORDER BY    UserEnrollNumber ASC, Ngay ASC 
end
