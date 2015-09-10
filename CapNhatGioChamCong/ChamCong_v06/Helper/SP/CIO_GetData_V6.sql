IF EXISTS (
	   SELECT type_desc, type
	   FROM sys.procedures WITH(NOLOCK)
	   WHERE NAME = 'CIO_GetData_V6'
		   AND type = 'P'
	 )
DROP PROCEDURE  CIO_GetData_V6
GO
CREATE PROCEDURE CIO_GetData_V6
@Array_UserEnrollNumber IntArray readonly,
@From datetime,
@To datetime
as
begin
	select * 
	from CIO
	where UserEnrollNumber in (select * from @Array_UserEnrollNumber)
	and NgayCong between @From and @To
end

