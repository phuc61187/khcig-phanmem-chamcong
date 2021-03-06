IF EXISTS (
	   SELECT type_desc, type
	   FROM sys.procedures WITH(NOLOCK)
	   WHERE NAME = 'ShiftSch_DocDSCaV6'
		   AND type = 'P'
	 )
DROP PROCEDURE  ShiftSch_DocDSCaV6
GO
CREATE PROCEDURE [dbo].[ShiftSch_DocDSCaV6]
(@SchID int = null)
AS
BEGIN
    SELECT 
      ShiftSch.SchID, ShiftSch.T1, Shifts6.ShiftCode,Shifts6.[Enable], Shifts6.Workingday
    FROM
      Shifts6 inner JOIN ShiftSch 
      ON (ShiftSch.T1 = Shifts6.ShiftID )
      
    where (@SchID is null or ShiftSch.SchID = @SchID )
	order by  ShiftSch.SchID 
END
