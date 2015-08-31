SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter PROCEDURE CheckInOut_DocCheckChuaXuLyV6
@From datetime, @To datetime,
@ArrayUserEnrollNumber IntArray readonly,
@DaXuLy bit = null,
@Loai bit = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select *
	from CheckInOut
	where (@Loai is null or Loai = @Loai)
	and (@DaXuLy is null or DaXuLy = @DaXuLy)
	and CheckInOut.UserEnrollNumber in (select * from @ArrayUserEnrollNumber)
	and (CheckInOut.TimeStr between @From and @To)
END
GO
