USE [WiseEyeV5Express]
GO

/****** Object:  StoredProcedure [dbo].[CheckInOut_Delete]    Script Date: 4/17/2015 4:42:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[CheckInOut_Delete]
	@UserEnrollNumber int, @TimeStr datetime, @MachineNo int
as
begin
	update  CheckInOut 
	set     Xoa = 1
	where   UserEnrollNumber = @UserEnrollNumber 
			and TimeStr = @TimeStr
			and (MachineNo % 2 = @MachineNo % 2)								

end
GO

