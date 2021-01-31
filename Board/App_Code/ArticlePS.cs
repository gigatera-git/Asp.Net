using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ArticlePS의 요약 설명입니다.
/// </summary>
public class ArticlePS
{
    public Int32 intPage { get; set; }
    public Int32 intPageSize { get; set; }
    public Int32 intBlockPage { get; set; }
    public Int32 intTotalCount { get; set; }
    public Int32 intTotalPage { get; set; }

    public String SearchOpt { get; set; }
    public String SearchVal { get; set; }
    
    public ArticlePS()
	{
		//
		// TODO: 여기에 생성자 논리를 추가합니다.
		//
	}

    public String Paging(String argv) 
    {
		String paging = "";

		if (this.intPage>1) {
            paging += "<a href='list.aspx?intPage=1&" + argv + "'><img src='images/common/btn_page01.gif' align='absmiddle'></a>";
		} else {
			paging += "<img src='images/common/btn_page01.gif' align='absmiddle'>";
		}
		paging +="&nbsp;";

		Int32 intTemp = ((this.intPage - 1) / this.intBlockPage) * this.intBlockPage + 1;

		if (intTemp==1) {
			paging += "<img src='images/common/btn_page02.gif' align='absmiddle'>";
		} else {
            paging += "<a href='list.aspx?intPage=" + (intTemp - this.intBlockPage).ToString() + "&" + argv + "'><img src='images/common/btn_page02.gif' align='absmiddle'></a>";
		}
		paging +="&nbsp;";


        Int32 intLoop = 1;
		while (intLoop <= this.intBlockPage && intTemp <= this.intTotalPage) {
			if (intTemp==this.intPage) {
                paging += "<b>" + (intTemp).ToString() + "</b>";
			} else {
                paging += "<span><a href='list.aspx?intPage=" + (intTemp).ToString() + "&" + argv + "'>" + (intTemp).ToString() + "</a></span>";
			}
			paging +="&nbsp;";

			intTemp++;
			intLoop++;
		}
		paging +="&nbsp;";

		if (intTemp>this.intTotalPage) {
			paging += "<img src='images/common/btn_page03.gif' align='absmiddle'>";
		} else {
            paging += "<a href='list.aspx?intPage=" + (intTemp).ToString() + "&" + argv + "'><img src='images/common/btn_page03.gif' align='absmiddle'></a>";
		}
		paging +="&nbsp;";

		if (this.intPage<this.intTotalPage) {
            paging += "<a href='list.aspx?intPage=" + (intTotalPage).ToString() + "&" + argv + "'><img src='images/common/btn_page04.gif' align='absmiddle'></a> ";
		} else {
			paging += "<img src='images/common/btn_page04.gif' align='absmiddle'>";
		}

		return paging;
	}


}