USE [gt]
GO
/****** Object:  StoredProcedure [dbo].[sp_board_del_ok]    Script Date: 2021-02-01 오전 8:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[sp_board_del_ok]
@idx integer,
@pwd varchar(20),
@res smallint output
as 
begin
	SET NOCOUNT ON

	delete from tbl_board where idx=@idx and pwd=HASHBYTES('SHA2_512',@pwd)
	set @res = @@ROWCOUNT

	SET NOCOUNT OFF
end