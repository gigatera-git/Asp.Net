using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mod_ok : System.Web.UI.Page
{
    public Dictionary<String, String> dics;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        String msg = "";

        String referer = Request.ServerVariables["HTTP_REFERER"];
        String idx = (Request.Params["idx"] == null) ? "" : Request.Params["idx"];
        String intPage = (Request.Params["intPage"] == null) ? "1" : Request.Params["intPage"];
        String SearchOpt = (Request.Params["SearchOpt"] == null) ? "" : Request.Params["SearchOpt"];
        String SearchVal = (Request.Params["SearchVal"] == null) ? "" : Request.Params["SearchVal"];
        String reg_ip = Proc.getClientIp();

        dics = new Dictionary<string, string>();
        dics.Add("idx", idx);
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
            Int16 res = (Int16)Session["mod_ok_res"];
            if (res == 0)
            {
                lblRes.Text = "<li>글내용이 정상적으로 수정되었습니다</li>";
            }
            else if (res == 1)
            {
                lblRes.Text = "<li>글내용 수정처리중 에러가 발생하였습니다. 관리자에게 문의하세요</li>";
            }
            else if (res == 2)
            {
                lblRes.Text = "<li>60초 이내 재 업로드할 수 없습니다</li>";
            }

            lblRes.Text += "<br><li>잠시후 이동합니다</li>";

            Session["mod_ok_res"] = null;
            Session.Remove("mod_ok_res");
        }

    }
}