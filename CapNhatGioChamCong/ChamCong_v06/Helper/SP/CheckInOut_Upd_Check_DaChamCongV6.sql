IF EXISTS (
	   SELECT type_desc, type
	   FROM sys.procedures WITH(NOLOCK)
	   WHERE NAME = 'CheckInOut_Upd_Check_DaChamCongV6'
		   AND type = 'P'
	 )
DROP PROCEDURE  CheckInOut_Upd_Check_DaChamCongV6
GO
CREATE PROCEDURE CheckInOut_Upd_Check_DaChamCongV6
@UserEnrollNumber int,
@TimeStr datetime,
@MachineNo int,
@DaChamCong bit
as
begin
	update CheckInOut
	set DaChamCong = @DaChamCong
	where (UserEnrollNumber = @UserEnrollNumber
	and TimeStr = @TimeStr
	and MachineNo = @MachineNo)
	
end