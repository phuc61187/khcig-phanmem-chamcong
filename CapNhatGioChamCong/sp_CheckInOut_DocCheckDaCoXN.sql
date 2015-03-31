IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CheckInOut_DocCheckDaCoXN')
DROP PROCEDURE sp_CheckInOut_DocCheckDaCoXN
GO

CREATE PROCEDURE sp_CheckInOut_DocCheckDaCoXN
	@ArrayMaCC IntArray readonly,
	@BDVao datetime,
	@KTVao datetime,
	@BDRaa datetime,
	@KTRaa datetime
AS
BEGIN
	SELECT distinct	CIO.UserEnrollNumber
					,CIO.TimeStr, CIO.Source, CIO.MachineNo
					,XN.ID,ShiftID,DuyetChoPhepVaoTre, DuyetChoPhepRaSom,OTMin, Explain, Note
					,VaoTreLaCV, RaSomLaCV
					,CIO.Them, CIO.IDGioGoc, CIO.Xoa
	FROM		XNCa_LamThem XN, CheckInOut CIO
	where		(Xoa is null or Xoa = 0)-- skip deleted check
				and	CIO.IDXNCa_LamThem = XN.ID
				and ( (TimeStr between @BDVao and @KTVao and MachineNo % 2 = 1)
				or (TimeStr between @BDRaa and @KTRaa and MachineNo % 2 = 0) )
				and CIO.UserEnrollNumber in (select * from @ArrayMaCC)
	group by	CIO.UserEnrollNumber
				,CIO.TimeStr, CIO.Source, CIO.MachineNo 
				,XN.ID,ShiftID,DuyetChoPhepVaoTre, DuyetChoPhepRaSom,OTMin, Explain, Note
				,VaoTreLaCV, RaSomLaCV
				,CIO.Them, CIO.IDGioGoc, CIO.Xoa
	order by	CIO.UserEnrollNumber asc, ID asc , CIO.TimeStr asc
END
GO
