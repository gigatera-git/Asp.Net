using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class del_ok : System.Web.UI.Page
{
    public Dictionary<String, String> dics;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        String msg = "";

        String referer = Request.ServerVariables["HTTP_REFERER"];
        String idx = (Request.Params["idx_h"] == null) ? "" : Request.Params["idx_h"];
        String intPage = (Request.Params["intPage_h"] == null) ? "1" : Request.Params["intPage_h"];
        String SearchOpt = (Request.Params["SearchOpt_h"] == null) ? "" : Request.Params["SearchOpt_h"];
        String SearchVal = (Request.Params["SearchVal_h"] == null) ? "" : Request.Params["SearchVal_h"];
        String pwd = (Request.Params["pwd_h"] == null) ? "" : Request.Params["pwd_h"];
        String reg_ip = Proc.getClientIp();

        dics = new Dictionary<string, string>();
        dics.Add("intPage", intPage);
        dics.Add("SearchOpt", SearchOpt);
        dics.Add("SearchVal", SearchVal);

        queryString.Value = Proc.newQueryString(dics);

        Uri uri = new Uri(referer);
        if (uri.Host != "localhost")
        {
            msg = "<li>(" + reg_ip + ")에서 비정상 접근이 감지되었습니다</li>";
        }
        else
        {
            List<ArticleUP> articleUP = (new Board()).getArticleUp(idx);
            foreach (ArticleUP aup in articleUP)
            {
                //context.Response.Write(Proc.getSavePath(aup.reg_date) + "/" + aup.fileSaveName);
                File.Delete(Server.MapPath(Proc.getSavePath(aup.reg_date) + "/" + aup.fileSaveName));
                Response.Write("<li>(" + aup.fileRealName + ")이 삭제되었습니다</li>");
            }

            int res = (new Board()).setArticleDel(idx, pwd);
            if (res < 1)
            {
                msg = "<li>해당글 삭제중 에러가 발생하였습니다</li>";
            }
            else
            {
                msg = "<li>해당글이 삭제되었습니다.</li>";
            }
        }

        msg += "<li>잠시후 리스트로 이동합니다</li>";

        lblRes.Text = msg;       

    }
}