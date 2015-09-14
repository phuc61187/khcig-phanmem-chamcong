IF EXISTS (
	   SELECT type_desc, type
	   FROM sys.procedures WITH(NOLOCK)
	   WHERE NAME = 'Absent_GetDataV6'
		   AND type = 'P'
	 )
DROP PROCEDURE  Absent_GetDataV6
GO
CREATE PROCEDURE Absent_GetDataV6
(@Array_UserEnrollNumber IntArray readonly,
@From datetime,
@To datetime)
as
begin
	select *
	from Absent
	where Absent.TimeDate between @From and @To
	and (UserEnrollNumber in (select * from @Array_UserEnrollNumber))

end