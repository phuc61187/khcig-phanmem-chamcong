USE [WiseEyeV5Express]
GO
/****** Object:  StoredProcedure [dbo].[CheckInOut_InvertKieuChamCong]    Script Date: 4/17/2015 4:44:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CheckInOut_InvertKieuChamCong]
	@UserEnrollNumber int, @TimeDate datetime, @TimeStr datetime, 
	@MachineNoOld int, @SourceOld nvarchar, @MachineNoNew int, @SourceNew nvarchar
AS
BEGIN
	declare @ID int
	insert into GioGoc (TimeStr, MachineNo, Source) 
	values				(@TimeStr, @MachineNoOld, @SourceOld)
	select @ID = @@Identity
	update  CheckInOut 
	set     Source = @SourceNew, MachineNo = @MachineNoNew, IDGioGoc = @ID
	where   UserEnrollNumber = @UserEnrollNumber 
			and TimeStr = @TimeStr
			and (MachineNo % 2 = @MachineNoOld % 2)								
END
