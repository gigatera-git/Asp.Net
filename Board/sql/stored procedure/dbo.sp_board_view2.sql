USE [gt]
GO
/****** Object:  StoredProcedure [dbo].[sp_board_view2]    Script Date: 2021-02-01 오전 8:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[sp_board_view2]
@idx varchar(10),
@count_done varchar(10)
as
begin
SET NOCOUNT ON 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	Declare @qry as nvarchar(1000)

	if @count_done<>@idx
	begin
		begin tran
		update tbl_board set count=count+1 where idx=@idx
		commit
	end
	
	set @qry = ''
	set @qry = @qry + 'select idx,uname,title,pwd,contents,count,ref,re_step,re_lvl,reg_ip,mod_ip,reg_date,mod_date '
	set @qry = @qry + 'from tbl_board '
	set @qry = @qry + 'where idx='+@idx+'; '
	exec(@qry)

SET NOCOUNT OFF
--RETURN	
end