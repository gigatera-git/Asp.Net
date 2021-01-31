using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reply_ok : System.Web.UI.Page
{
    public String intPage;
    public String SearchOpt;
    public String SearchVal;

    public Dictionary<String, String> dics;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        intPage = (Request.Params["intPage"] == null) ? "1" : Request.Params["intPage"];
        SearchOpt = (Request.Params["searchOpt"] == null) ? "" : Request.Params["searchOpt"];
        SearchVal = (Request.Params["searchVal"] == null) ? "" : Request.Params["searchVal"];

        dics = new Dictionary<string, string>();
        dics.Add("intPage", intPage);
        dics.Add("SearchOpt", SearchOpt);
        dics.Add("SearchVal", SearchVal);

        queryString.Value = Proc.newQueryString(dics);

        Int16 res = (Int16)Session["reply_ok_res"];
        if (res == 0)
        {
            lblRes.Text = "<li>답글이 정상적으로 등록되었습니다</li>";
        }
        else if (res == 1)
        {
            lblRes.Text = "<li>글내용 답글처리중 에러가 발생하였습니다. 관리자에게 문의하세요</li>";
        }
        else if (res == 2)
        {
            lblRes.Text = "<li>60초 이내 재 등록할 수 없습니다</li>";
        }

        lblRes.Text += "<br><li>잠시후 리스트로 이동합니다</li>";

        Session["reply_ok_res"] = null;
        Session.Remove("reply_ok_res");
    }
}