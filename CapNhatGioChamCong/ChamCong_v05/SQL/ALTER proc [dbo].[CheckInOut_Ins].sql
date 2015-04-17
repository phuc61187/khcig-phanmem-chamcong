USE [WiseEyeV5Express]
GO

/****** Object:  StoredProcedure [dbo].[CheckInOut_Ins]    Script Date: 4/17/2015 4:43:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[CheckInOut_Ins]
	@UserEnrollNumber int, @TimeDate datetime, @TimeStr datetime, 
	@OriginType nvarchar, @MachineNo int, @Source nvarchar
as
begin
	insert into CheckInOut (UserEnrollNumber, TimeDate, TimeStr, OriginType, MachineNo, Source, WorkCode, Them)
	VALUES (@UserEnrollNumber, @TimeDate, @TimeStr, @OriginType, @MachineNo, @Source, 0, 1)
end
GO


