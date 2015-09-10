IF EXISTS (
	   SELECT type_desc, type
	   FROM sys.procedures WITH(NOLOCK)
	   WHERE NAME = 'NgayCong_LayV6'
		   AND type = 'P'
	 )
DROP PROCEDURE  NgayCong_LayV6
GO
CREATE PROCEDURE NgayCong_LayV6
@From datetime,
@To datetime
as
begin
select * from Holiday where HDate is null
end