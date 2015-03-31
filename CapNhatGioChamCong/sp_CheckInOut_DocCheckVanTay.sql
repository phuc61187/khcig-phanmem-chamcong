IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CheckInOut_DocCheckVanTay')
DROP PROCEDURE sp_CheckInOut_DocCheckVanTay
GO

CREATE PROCEDURE sp_CheckInOut_DocCheckVanTay
	@ArrayMaCC IntArray readonly,
	@BDVao datetime,
	@KTVao datetime,
	@BDRaa datetime,
	@KTRaa datetime
AS
BEGIN
	select distinct	UserEnrollNumber, TimeStr, MachineNo, Source, Them, IDGioGoc, Xoa
	from		CheckInOut
	where		( (TimeStr between @BDVao and @KTVao and MachineNo % 2 = 1)
				or (TimeStr between @BDRaa and @KTRaa and MachineNo % 2 = 0) )
				and (Xoa is null or Xoa = 0) -- skip deleted check
				and ( IDXNCa_LamThem is null) --skip gio da qua xac nhan
				and (UserEnrollNumber in (select * from @ArrayMaCC))
	order by	UserEnrollNumber asc, TimeStr asc 
END
GO
