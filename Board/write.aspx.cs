using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

public partial class write : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        List<ArticleUP> arr = new List<ArticleUP>();
        arr = new Upload().fileUpload(HttpContext.Current.Request.Files);
        
        Article article = new Article();
        article.uname = Request.Params["uname"];
        article.title = Request.Params["title"];
        article.pwd = Request.Params["pwd"];
        article.contents = Request.Params["contents"];
        article.reg_ip = Proc.getClientIp();

        Int16 res = 0;
        Int32 bidx = 0;

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
                cmd.CommandText = "dbo.sp_board_insert";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@uname", article.uname);
                cmd.Parameters.AddWithValue("@title", article.title);
                cmd.Parameters.AddWithValue("@pwd", article.pwd);
                cmd.Parameters.AddWithValue("@contents", article.contents);
                cmd.Parameters.AddWithValue("@reg_ip", article.reg_ip);
                cmd.Parameters.Add("@res", OleDbType.SmallInt);
                cmd.Parameters["@res"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@bidx", OleDbType.Integer);
                cmd.Parameters["@bidx"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                res = (short)cmd.Parameters["@res"].Value;
                if (res == 0)
                {
                    bidx = (int)cmd.Parameters["@bidx"].Value;

                    foreach (ArticleUP aup in arr)
                    {
                        cmd.CommandText = "dbo.sp_board_insert_upload";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@bidx", bidx);
                        cmd.Parameters.AddWithValue("@fileRealName", aup.fileRealName);
                        cmd.Parameters.AddWithValue("@fileSaveName", aup.fileSaveName);
                        cmd.Parameters.AddWithValue("@fileSize", aup.fileSize.ToString());
                        cmd.Parameters.AddWithValue("@reg_ip", aup.reg_ip);
                        cmd.Parameters.Add("@res", OleDbType.SmallInt);
                        cmd.Parameters["@res"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        res += (short)cmd.Parameters["@res"].Value;
                    }
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Response.Write(ex.Message);
                Response.Write(ex.StackTrace);
            }
            finally
            {
                transaction.Dispose();

                if (res == 2)
                {
                    Response.Write("60초 이내 재 등록 할수 없습니다");
                }
               
            }
            cmd.Dispose();

            Session["write_ok_res"] = res;

            Response.Redirect("write_ok.aspx", true);
        }

       

    }

    protected void Page_Unload(object sender, EventArgs e) 
    {
       
    }
}