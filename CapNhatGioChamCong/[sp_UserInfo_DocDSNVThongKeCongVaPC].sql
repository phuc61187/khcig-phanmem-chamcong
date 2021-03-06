USE [WiseEyeV5Express]
GO
/****** Object:  StoredProcedure [dbo].[sp_UserInfo_DocDSNVThongKeCongVaPC]    Script Date: 3/24/2015 4:09:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_UserInfo_DocDSNVThongKeCongVaPC]
(	@ArrUserIDD as IntArray readonly,
	@ArrExcludeMaCC as IntArray readonly,
	@ArrMaNhiemVu as IntArray readonly	
)
AS
BEGIN
--declare @ArrExcludeMaCC1 as Table(id int)
--declare @ArrMaNhiemVu1 as  Table(id int)
--declare @ArrUserIDD1 as  Table(id int)
----INSERT @ArrExcludeMaCC VALUES ('123')
----INSERT @ArrExcludeMaCC VALUES ('12')
----INSERT @ArrMaNhiemVu VALUES ('123')
--INSERT @ArrMaNhiemVu1 VALUES (1)
--INSERT @ArrUserIDD1 select RelationDept.ID from RelationDept

	SELECT 		UserFullCode, UserFullName, u.UserEnrollNumber, UserLastName, UserEnrollName, u.SchID
    from		UserInfo u
    where		u.UserIDD > 0 and u.UserIDD in (select * from @ArrUserIDD) -- loc NV duoc thao tac
    			and u.UserEnrollNumber not in (select * from @ArrExcludeMaCC) -- bo qua NV bi exlcude
                and u.UserEnrollNumber in 
                	(select 		u1.UserEnrollNumber
                    FROM 			UserInfo u1, NhiemVu_NhanVien nvnv
                    where 			u1.UserEnrollNumber = nvnv.UserEnrollNumber
                    				and nvnv.MaNhiemVu in (select * from @ArrMaNhiemVu) )
    
END
