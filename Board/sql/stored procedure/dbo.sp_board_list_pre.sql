USE [gt]
GO
/****** Object:  StoredProcedure [dbo].[sp_board_list_pre]    Script Date: 2021-02-01 오전 8:46:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[sp_board_list_pre]
@intPageSize varchar(5),
@SearchOpt varchar(10),
@SearchVal varchar(20),
@intTotalCount integer output,
@intTotalPage integer output
as
begin
SET NOCOUNT ON 
	Declare @qry as nvarchar(1000)

	Declare @TotalCount as int
	Declare @TotalPage as int
	set @qry = ''
	set @qry = @qry + 'DECLARE CUR CURSOR FOR '
	set @qry = @qry + 'Select Count(*),CEILING(CAST(Count(*) AS FLOAT)/'+@intPageSize+') '
	set @qry = @qry + 'from tbl_board '
	set @qry = @qry + 'where idx<>'''' '
	if @SearchOpt<>'' and @SearchVal<>'' 
		set @qry = @qry + 'and '+@SearchOpt+' like ''%'+@SearchVal+'%'' '
	exec(@qry) 	
	OPEN CUR
	FETCH NEXT FROM CUR INTO @intTotalCount,@intTotalPage
	CLOSE CUR
	DEALLOCATE CUR

SET NOCOUNT OFF	
end
