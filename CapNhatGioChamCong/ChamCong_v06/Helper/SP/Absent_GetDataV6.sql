IF EXISTS (
	   SELECT type_desc, type
	   FROM sys.procedures WITH(NOLOCK)
	   WHERE NAME = 'Absent_GetDataV6'
		   AND type = 'P'
	 )
DROP PROCEDURE Absent_GetDataV6 
GO
CREATE PROCEDURE Absent_GetDataV6
@Array_UserEnrollNumber IntArray readonly,
@From datetime,
@To datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select *
	from Absent
	where UserEnrollNumber in (select * from @Array_UserEnrollNumber)
	and (Absent.TimeDate between @From and @To)
END
GO
