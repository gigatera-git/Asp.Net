using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Web.Services;

public partial class view : System.Web.UI.Page
{
    public String idx;
    public String intPage;
    public String SearchOpt;
    public String SearchVal;
    public String count_done;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        idx = (Request.Params["idx"] == null) ? "1" : Request.Params["idx"];
        intPage = (Request.Params["intPage"] == null) ? "1" : Request.Params["intPage"];
        SearchOpt = (Request.Params["searchOpt"] == null) ? "" : Request.Params["searchOpt"];
        SearchVal = (Request.Params["searchVal"] == null) ? "" : Request.Params["searchVal"];
        count_done = (Request.Cookies["count_done"] == null) ? "" : Request.Cookies["count_done"].Value;

        idx_h.Value = idx;
        intPage_h.Value = intPage;
        SearchOpt_h.Value = SearchOpt;
        SearchVal_h.Value = SearchVal;
        
        Article article = (new Board()).getArticle(idx,count_done);
        if (article.idx == 0)
        {
            Response.Write("<li>해당글이 없습니다...<a href='list.aspx'>[리스트]</a></li>");
            Response.End();
        }
        
        ref_h.Value = article.ref_.ToString();
        re_step_h.Value = article.re_step.ToString();
        re_lvl_h.Value = article.re_lvl.ToString();
        lblUname.Text = article.uname;
        lblTitle.Text = article.title;
        lblContents.Text = article.contents;
        lblCount.Text = article.count.ToString();
        lblReg_ip.Text = article.reg_ip;
        lblMod_ip.Text = article.mod_ip;
        lblReg_Date.Text = article.reg_date.ToString();
        lblMod_Date.Text = article.mod_date.ToString();

        Response.Cookies["count_done"].Value = idx;

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
            sb += "<input type='hidden' name='files' class='files' value='" + aup.reg_date + "/" + aup.fileSaveName + "' />";
            sb += "</div>";

            sbs.Append(sb);
        }
        lblAup.Text = sbs.ToString();
        
    }

    
    [WebMethod] 
    public static string getChkPwd(String idx,String pwd)
    {
        return (new Board()).pwdChk(idx, pwd);
    }
}