using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.IO;
using System.Drawing;

public partial class mod : System.Web.UI.Page
{
    public String idx;
    public String intPage;
    public String SearchOpt;
    public String SearchVal;
    public Dictionary<String, String> dics;
    
    Article article;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        idx = (Request.Params["idx"] == null) ? "1" : Request.Params["idx"];
        intPage = (Request.Params["intPage"] == null) ? "1" : Request.Params["intPage"];
        SearchOpt = (Request.Params["searchOpt"] == null) ? "" : Request.Params["searchOpt"];
        SearchVal = (Request.Params["searchVal"] == null) ? "" : Request.Params["searchVal"];

        dics = new Dictionary<string, string>();
        dics.Add("idx", idx);
        dics.Add("intPage", intPage);
        dics.Add("SearchOpt", SearchOpt);
        dics.Add("SearchVal", SearchVal);

        article = (new Board()).getArticle(idx);
        if (article.idx == 0)
        {
            Response.Write("<li>해당글이 없습니다...<a href='list.aspx'>[리스트]</a></li>");
            Response.End();
        }

        lblUname.Text = article.uname;
        uname.Value = article.uname;
        title.Text = article.title;
        //pwd.Text = article.pwd;
        contents.Text = article.contents;

        //get upload
        StringBuilder sbs = new StringBuilder();
        List<ArticleUP> aups = (new Board()).getArticleUp(idx);
        foreach (ArticleUP aup in aups)
        {
            String sb = "<div class='attach'>";
            sb += "(" + (aups.IndexOf(aup) + 1).ToString() + ")";
            if (Proc.IsImage(Path.GetExtension(aup.fileRealName)))
            {
                //sb += Proc.getSavePath(aup.reg_date);
                sb += "<a href='download.ashx?filePath=" + Proc.getSavePath(aup.reg_date) + "&fileName=a" + aup.fileSaveName + "'>";
                sb += "<img src='" + Proc.getSavePath(aup.reg_date) + "/" + aup.fileSaveName + "' title='" + aup.fileRealName + "' align='absmiddle' />";
                sb += "</a>";
            }
            else
            {
                sb += "<a href='download.ashx?filePath=" + Proc.getSavePath(aup.reg_date) + "&fileName=" + aup.fileSaveName + "' title='" + aup.fileRealName + "'/>";
                sb += aup.fileRealName;
                sb += "</a>";
            }

            sb += "(<input type='checkbox' name='attachDel' class='attachDel' value='" + Proc.getSavePath(aup.reg_date) + "/" + aup.fileSaveName + "' />삭제)";

            sb += "</div>";

            sbs.Append(sb);
        }
        lblAup.Text = sbs.ToString();

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
        //Response.Write(Request.Params["pwd"]);
        article.contents = Request.Params["contents"];
        article.mod_ip = Proc.getClientIp();
        article.attachDels = (Request.Params["attachDel"] != null) ? Request.Params["attachDel"].ToString().Split(',') : new String[2] { "", "" };

        foreach (String ad in article.attachDels)
        {
            if (ad != "")
            {
                File.Delete(Server.MapPath(ad));
                Response.Write("<li>(" + ad + ")이 삭제되었습니다</li>");
            }
        }

        Int16 res = 0;
        Int32 bidx = Int32.Parse(idx);

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
                cmd.CommandText = "dbo.sp_board_mod_ok";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idx", article.idx);
                cmd.Parameters.AddWithValue("@uname", article.uname);
                cmd.Parameters.AddWithValue("@title", article.title);
                cmd.Parameters.AddWithValue("@pwd", article.pwd);
                cmd.Parameters.AddWithValue("@contents", article.contents);
                cmd.Parameters.AddWithValue("@mod_ip", article.mod_ip);
                cmd.Parameters.Add("@res", OleDbType.SmallInt);
                cmd.Parameters["@res"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                res = (short)cmd.Parameters["@res"].Value;
                if (res == 0)
                {
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
                    if (res == 2)
                    {
                        Response.Write("60초 이내 신규파일을 첨부할수 없습니다");
                    }
                }
                if (res == 0)
                {
                    
                    foreach (String ad in article.attachDels)
                    {
                        string fileSaveName = ad.Split('/').Last();
                        cmd.CommandText = "dbo.sp_board_upload_del_ok";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@fileSaveName", fileSaveName);
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
                    Response.Write("글 비밀번호가 일치하지 않습니다");
                    //lblPwdErr.ForeColor = Color.Red;
                    //lblPwdErr.Text = "글 비밀번호가 일치하지 않습니다";
                }

            }
            cmd.Dispose();

            Session["mod_ok_res"] = res;
            String des = "mod_ok.aspx?" + Proc.newQueryString(dics);
            Response.Redirect(des, true);
        }
    }
}