ALTER PROCEDURE XacNhanPhuCap5_DocBang
(@ArrUserIDD as IntArray readonly,
@NgayBD datetime,
@NgayKT datetime,
@Duyet bit)
AS
BEGIN
	IF (@Duyet is NULL) begin
    	select	*
        from	XacNhanPhuCap5
        where	UserEnrollNumber in (select * from @ArrUserIDD)
        		and Ngay >= @NgayBD and Ngay <= @NgayKT 
    end
    else begin
    	select	*
        from 	XacNhanPhuCap5
        where	UserEnrollNumber in (select * from @ArrUserIDD)
        		and Ngay >= @NgayBD and Ngay <= @NgayKT 
                and Duyet = @Duyet    
    end
END
GO