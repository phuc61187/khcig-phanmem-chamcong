IF EXISTS (
	   SELECT type_desc, type
	   FROM sys.procedures WITH(NOLOCK)
	   WHERE NAME = 'NgayCong_LayV6'
		   AND type = 'P'
	 )
DROP PROCEDURE  NgayCong_LayV6
GO
CREATE PROCEDURE NgayCong_LayV6
(@Array_UserEnrollNumber IntArray readonly,
@From datetime,
@To datetime)
as 
begin
	select * 
	from XacNhanPhuCapNgayV6
	where Ngay between @From and @To
	and (UserEnrollNumber in (select * from @Array_UserEnrollNumber))
end
