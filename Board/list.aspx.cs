using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;


public partial class list : System.Web.UI.Page
{
    public String intPage;
    public String SearchOpt;
    public String SearchVal;

    public String intPageSize;
    public String intBlockPage;
    public String intTotalCount;
    public String intTotalPage;

    ArticlePS articleps;
    Board board;

    String argv;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        intPage = (Request.Params["intPage"] == null) ? "1" : Request.Params["intPage"];
        SearchOpt = (Request.Params["searchOpt"] == null) ? "" : Request.Params["searchOpt"];
        SearchVal = (Request.Params["searchVal"] == null) ? "" : Request.Params["searchVal"];

        argv = "SearchOpt=" + SearchOpt + "&SearchVal=" + SearchVal;

        articleps = new ArticlePS();
        board = new Board();
        articleps.intPage = Int32.Parse(intPage);
        articleps.intPageSize = 10;
        articleps.intBlockPage = 10;
        articleps.SearchOpt = SearchOpt;
        articleps.SearchVal = SearchVal;
        board.setPagingPre(articleps);

        rptList.DataSource = board.getArticleList(articleps);
        rptList.DataBind();
        
        //paging
        paging.Text = articleps.Paging(argv);

        //set for searching
        if (SearchOpt != "" && SearchVal != "")
        {
            searchVal.Text = SearchVal;

            if (SearchOpt == "title")
            {
                searchOpt.SelectedIndex = 1;
            }
            else if (SearchOpt == "contents")
            {
                searchOpt.SelectedIndex = 2;
            }

            btnInit.Visible = true;
        }
        else
        {
            btnInit.Visible = false;
        }
    }

    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Label lblNum = (Label)e.Item.FindControl("lblNum");
            lblNum.Text = board.getArticleNum(articleps, e.Item.ItemIndex).ToString();

            Image img_lvl = (Image)e.Item.FindControl("img_lvl");
            Image img_re = (Image)e.Item.FindControl("img_re");
            HyperLink link_view = (HyperLink)e.Item.FindControl("linkView");

            //System.Data.Common.DbDataRecord row = (System.Data.Common.DbDataRecord)(e.Item.DataItem);
            DataRowView row = (DataRowView)e.Item.DataItem;
            Int32 re_lvl = Int32.Parse(row["re_lvl"].ToString());
            String idx = row["idx"].ToString();
            img_lvl.Width = re_lvl * 7;

            img_re.Visible = (re_lvl > 0) ? true : false;

            link_view.NavigateUrl = "view.aspx?idx=" + idx + "&intPage=" + articleps.intPage + "&" + argv;
        }
    }
}