IF EXISTS (
	   SELECT type_desc, type
	   FROM sys.procedures WITH(NOLOCK)
	   WHERE NAME = 'sp_test_updateDaXuLy_True'
		   AND type = 'P'
	 )
DROP PROCEDURE  sp_test_updateDaXuLy_True
GO

CREATE PROCEDURE sp_test_updateDaXuLy_True 
AS
BEGIN
	update CheckInOut set DaChamCong = 0, Loai = 0

	truncate table CIO
	--truncate table XacNhanPhuCapV6
END
GO
