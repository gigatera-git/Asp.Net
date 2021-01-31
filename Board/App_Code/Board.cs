using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

/// <summary>
/// Board의 요약 설명입니다.
/// </summary>
public class Board
{
	public Board()
	{
		//
		// TODO: 여기에 생성자 논리를 추가합니다.
		//
	}

    public Int32 getArticleNum(ArticlePS articleps, Int32 ItemIndex)
    {
        return (articleps.intTotalCount - ((articleps.intPage - 1) * articleps.intPageSize)) - ItemIndex;
    }

    public void setPagingPre(ArticlePS articleps)
    {
        using (OleDbConnection conn = new DB().Conn)
        {
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "dbo.sp_board_list_pre";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@intPageSize", articleps.intPageSize);
            cmd.Parameters.AddWithValue("@SearchOpt", articleps.SearchOpt);
            cmd.Parameters.AddWithValue("@SearchVal", articleps.SearchVal);

            cmd.Parameters.Add("@intTotalCount", OleDbType.Integer);
            cmd.Parameters["@intTotalCount"].Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@intTotalPage", OleDbType.Integer);
            cmd.Parameters["@intTotalPage"].Direction = ParameterDirection.Output;

            OleDbDataReader rs = cmd.ExecuteReader();

            rs.Close();
            rs.Dispose();

            //you can get output after closing datareader!!!
            articleps.intTotalCount = (int)cmd.Parameters["@intTotalCount"].Value;
            articleps.intTotalPage = (int)cmd.Parameters["@intTotalPage"].Value;

            cmd.Dispose();
        }
    }

    public DataTable getArticleList(ArticlePS articleps)
    {
        DataTable table = new DataTable();
        table.Columns.Add(new DataColumn("idx", typeof(int)));
        table.Columns.Add(new DataColumn("uname", typeof(string)));
        table.Columns.Add(new DataColumn("title", typeof(string)));
        table.Columns.Add(new DataColumn("pwd", typeof(string)));
        table.Columns.Add(new DataColumn("count", typeof(int)));
        table.Columns.Add(new DataColumn("ref", typeof(int)));
        table.Columns.Add(new DataColumn("re_step", typeof(int)));
        table.Columns.Add(new DataColumn("re_lvl", typeof(int)));
        table.Columns.Add(new DataColumn("reg_ip", typeof(string)));
        table.Columns.Add(new DataColumn("reg_date", typeof(string)));

        DataRow tr = null;

        using (OleDbConnection conn = new DB().Conn)
        {
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "dbo.sp_board_list2";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Page", articleps.intPage);
            cmd.Parameters.AddWithValue("@intPageSize", articleps.intPageSize);
            cmd.Parameters.AddWithValue("@SearchOpt", articleps.SearchOpt);
            cmd.Parameters.AddWithValue("@SearchVal", articleps.SearchVal);

            OleDbDataReader rs = cmd.ExecuteReader();

            while (rs.Read())
            {
                tr = table.NewRow();
                tr["idx"] = rs["idx"];
                tr["uname"] = rs["uname"];
                tr["title"] = rs["title"];
                tr["pwd"] = rs["pwd"];
                tr["count"] = rs["count"];
                tr["ref"] = rs["ref"];
                tr["re_step"] = rs["re_step"];
                tr["re_lvl"] = rs["re_lvl"];
                tr["reg_ip"] = rs["reg_ip"];
                tr["reg_date"] = rs["reg_date"];
                table.Rows.Add(tr);
            }

            rs.Close();
            rs.Dispose();

            cmd.Dispose();
        }

        return table;

    }


    public Article getArticle(string idx, string count_done)
    {
        Article article = new Article();
        article.idx = 0;

        using (OleDbConnection conn = new DB().Conn)
        {
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "dbo.sp_board_view2";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idx", idx);
            cmd.Parameters.AddWithValue("@count_done", count_done);

            OleDbDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
            {
                article.idx = (int)rs["idx"];
                article.uname = (string)rs["uname"];
                article.title = (string)rs["title"];
                //article.pwd = (string)rs["pwd"];
                article.contents = (string)rs["contents"];
                article.count = Int16.Parse(rs["count"].ToString());
                article.ref_ = (int)rs["ref"];
                article.re_step = Int16.Parse(rs["re_step"].ToString());
                article.re_lvl = Int16.Parse(rs["re_lvl"].ToString());
                article.reg_ip = (string)rs["reg_ip"];
                article.mod_ip = (rs["mod_ip"] == DBNull.Value) ? "" : (string)rs["mod_ip"];
                article.reg_date = DateTime.Parse(rs["reg_date"].ToString());
                article.mod_date = DateTime.Parse(rs["mod_date"].ToString());
            }

            rs.Close();
            rs.Dispose();
            cmd.Dispose();
        }

        return article;
    }

    public Article getArticle(string idx)
    {
        Article article = new Article();
        article.idx = 0;

        using (OleDbConnection conn = new DB().Conn)
        {
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "dbo.sp_board_view2";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idx", idx);
            cmd.Parameters.AddWithValue("@count_done", "0");

            OleDbDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
            {
                article.idx = (int)rs["idx"];
                article.uname = (string)rs["uname"];
                article.title = (string)rs["title"];
                //article.pwd = (string)rs["pwd"];
                article.contents = (string)rs["contents"];
                article.count = Int16.Parse(rs["count"].ToString());
                article.ref_ = (int)rs["ref"];
                article.re_step = Int16.Parse(rs["re_step"].ToString());
                article.re_lvl = Int16.Parse(rs["re_lvl"].ToString());
                article.reg_ip = (string)rs["reg_ip"];
                article.mod_ip = (rs["mod_ip"] == DBNull.Value) ? "" : (string)rs["mod_ip"];
                article.reg_date = DateTime.Parse(rs["reg_date"].ToString());
                article.mod_date = DateTime.Parse(rs["mod_date"].ToString());
            }

            rs.Close();
            rs.Dispose();
            cmd.Dispose();
        }

        return article;
    }

    public List<ArticleUP> getArticleUp(string idx)
    {
        List<ArticleUP> aups = new List<ArticleUP>();

        using (OleDbConnection conn = new DB().Conn)
        {
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "dbo.sp_board_view_upload";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@bidx", idx);

            OleDbDataReader rs = cmd.ExecuteReader();
            while (rs.Read())
            {
                ArticleUP aup = new ArticleUP();
                aup.bidx = (int)rs["bidx"];
                aup.fileRealName = (string)rs["fileRealName"];
                aup.fileSaveName = (string)rs["fileSaveName"];
                aup.fileSize = (string)rs["fileSize"];
                aup.reg_ip = (string)rs["reg_ip"];
                aup.reg_date = DateTime.Parse(rs["reg_date"].ToString());

                aups.Add(aup);
            }

            rs.Close();
            rs.Dispose();
            cmd.Dispose();
        }

        return aups;
    }

    public String pwdChk(String idx, String pwd)
    {
        String res = "비밀번호가 일치하지 않습니다";
        using (OleDbConnection conn = new DB().Conn)
        {
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "dbo.sp_board_pwd_chk";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idx", idx);
            cmd.Parameters.AddWithValue("@pwd", HttpUtility.UrlDecode(pwd));

            OleDbDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
            {
                if ((int)rs[0] >= 1)
                {
                    res = "ok";
                }
            }

            rs.Close();
            rs.Dispose();
            cmd.Dispose();
        }

        return res;
    }

    public int setArticleDel(String idx, String pwd)
    {
        int res = 0;
        
        using (OleDbConnection conn = new DB().Conn)
        {
            conn.Open();
            OleDbTransaction transaction = null;
            transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.CommandText = "dbo.sp_board_del_ok";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idx", idx);
                cmd.Parameters.AddWithValue("@pwd", pwd);
                cmd.Parameters.Add("@res", OleDbType.SmallInt);
                cmd.Parameters["@res"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                res = (short)cmd.Parameters["@res"].Value;


                cmd.CommandText = "dbo.sp_board_del_up_ok";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idx", idx);
                cmd.Parameters.Add("@res", OleDbType.SmallInt);
                cmd.Parameters["@res"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                res += (short)cmd.Parameters["@res"].Value;
                

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                //HttpContext.Current.Response.Write(ex.Message);
            }
            finally
            {
                transaction.Dispose();

            }
            cmd.Dispose();
        }

        return res;
    }

}
