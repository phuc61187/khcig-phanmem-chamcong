IF EXISTS (
	   SELECT type_desc, type
	   FROM sys.procedures WITH(NOLOCK)
	   WHERE NAME = 'CheckInOut_Upd_Loai'
		   AND type = 'P'
	 )
DROP PROCEDURE CheckInOut_Upd_Loai 
GO
CREATE PROCEDURE CheckInOut_Upd_Loai
@UserEnrollNumber int ,
@TimeStr datetime,
@MachineNo int,
@Loai bit
AS
BEGIN
	SET NOCOUNT ON;

	update CheckInOut
	set Loai = @Loai
	where UserEnrollNumber = @UserEnrollNumber
	and MachineNo = @MachineNo
	and TimeStr = @TimeStr
END
GO
