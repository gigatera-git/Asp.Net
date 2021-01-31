using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

public partial class reply : System.Web.UI.Page
{
    public String idx;
    public String intPage;
    public String SearchOpt;
    public String SearchVal;
    public String ref_;
    public String re_step;
    public String re_lvl;
    public Dictionary<String, String> dics;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        idx = (Request.Params["idx"] == null) ? "0" : Request.Params["idx"];
        intPage = (Request.Params["intPage"] == null) ? "1" : Request.Params["intPage"];
        SearchOpt = (Request.Params["searchOpt"] == null) ? "" : Request.Params["searchOpt"];
        SearchVal = (Request.Params["searchVal"] == null) ? "" : Request.Params["searchVal"];
        ref_ = (Request.Params["ref_"] == null) ? "1" : Request.Params["ref_"];
        re_step = (Request.Params["re_step"] == null) ? "0" : Request.Params["re_step"];
        re_lvl = (Request.Params["re_lvl"] == null) ? "0" : Request.Params["re_lvl"];

        dics = new Dictionary<string, string>();
        dics.Add("intPage", intPage);
        dics.Add("SearchOpt", SearchOpt);
        dics.Add("SearchVal", SearchVal);

    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        List<ArticleUP> arr = new List<ArticleUP>();
        arr = new Upload().fileUpload(HttpContext.Current.Request.Files);

        Article article = new Article();
        article.idx = Int32.Parse(idx);
        article.uname = Request.Params["uname"];
        article.title = Request.Params["title"];
        article.pwd = Request.Params["pwd"];
        article.contents = Request.Params["contents"];
        article.ref_ = Int32.Parse(ref_);
        article.re_step = Int16.Parse(re_step);
        article.re_lvl = Int16.Parse(re_lvl);

        
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
                cmd.CommandText = "dbo.sp_board_reply_upload";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@uname", article.uname);
                cmd.Parameters.AddWithValue("@title", article.title);
                cmd.Parameters.AddWithValue("@pwd", article.pwd);
                cmd.Parameters.AddWithValue("@contents", article.contents);
                cmd.Parameters.AddWithValue("@ref", article.ref_);
                cmd.Parameters.AddWithValue("@re_step", article.re_step);
                cmd.Parameters.AddWithValue("@re_lvl", article.re_lvl);
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

            Session["reply_ok_res"] = res;
            String des = "reply_ok.aspx?" + Proc.newQueryString(dics);
            Response.Redirect(des, true);
        }
    }
}