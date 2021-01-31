using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Board의 요약 설명입니다. ArticleDAO
/// </summary>
public class Article : IDisposable
{
    public Int32 idx { get; set; }
    public String uname { get; set; }
    public String title { get; set; }
    public String pwd { get; set; }
    public String contents { get; set; }
    public Int16 count { get; set; }
    public Int32 ref_ { get; set; }
    public Int16 re_step { get; set; }
    public Int16 re_lvl { get; set; }
    public String deleted { get; set; }
    public String reg_ip { get; set; }
    public String mod_ip { get; set; }
    public DateTime reg_date { get; set; }
    public DateTime mod_date { get; set; }
    public String[] attachDels { get; set; }

    public Article()
	{
		//
		// TODO: 여기에 생성자 논리를 추가합니다.
		//
	}

    public void Dispose()
    {

    }
}