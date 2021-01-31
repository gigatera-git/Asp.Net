using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ArticleUP의 요약 설명입니다. ArticleUPDAO
/// </summary>
public class ArticleUP
{
    public Int32 bidx { get; set; }
    public String fileRealName { get; set; }
    public String fileSaveName { get; set; }
    //public String fileType { get; set; }
    public String fileSize { get; set; }
    public String reg_ip { get; set; }
    public DateTime reg_date { get; set; }
    
    public ArticleUP()
	{
		//
		// TODO: 여기에 생성자 논리를 추가합니다.
		//
	}
}