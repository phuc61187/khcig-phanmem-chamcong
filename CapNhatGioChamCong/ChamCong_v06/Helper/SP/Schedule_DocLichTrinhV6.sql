
IF EXISTS (
	   SELECT type_desc, type
	   FROM sys.procedures WITH(NOLOCK)
	   WHERE NAME = 'Schedule_DocLichTrinhV6'
		   AND type = 'P'
	 )
DROP PROCEDURE [dbo].[Schedule_DocLichTrinhV6]
GO

/****** Object:  StoredProcedure [dbo].[Schedule_DocLichTrinhV6]    Script Date: 9/1/2015 9:11:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Schedule_DocLichTrinhV6]
AS
BEGIN
	select Schedule.SchID, Schedule.SchName
	from Schedule
END

GO

