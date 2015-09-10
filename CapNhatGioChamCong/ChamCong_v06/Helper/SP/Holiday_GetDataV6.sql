IF EXISTS (
	   SELECT type_desc, type
	   FROM sys.procedures WITH(NOLOCK)
	   WHERE NAME = 'Holiday_GetDataV6'
		   AND type = 'P'
	 )
DROP PROCEDURE  Holiday_GetDataV6
GO
CREATE PROCEDURE Holiday_GetDataV6
@From datetime,
@To datetime
as
begin
	select * from Holiday
	where Holiday.HDate between @From and @To
end