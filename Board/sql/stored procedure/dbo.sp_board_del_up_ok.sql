USE [gt]
GO
/****** Object:  StoredProcedure [dbo].[sp_board_del_up_ok]    Script Date: 2021-02-01 오전 8:50:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[sp_board_del_up_ok]
@idx integer,
@res smallint output
as 
begin
	SET NOCOUNT ON

	delete from tbl_board_upload where bidx=@idx

	set @res = @@ROWCOUNT

	SET NOCOUNT OFF
end
