using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Proc의 요약 설명입니다.
/// </summary>
public class Proc
{
	public Proc()
	{
		//
		// TODO: 여기에 생성자 논리를 추가합니다.
		//
	}

    public static String getClientIp()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        String ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (ip == null)
        {
            ip = context.Request.ServerVariables["Proxy-Client-IP"];
        }
        if (ip == null)
        {
            ip = context.Request.ServerVariables["WL-Proxy-Client-IP"]; // 웹로직
        }
        if (ip == null)
        {
            ip = context.Request.ServerVariables["HTTP_CLIENT_IP"];
        }
        if (ip == null)
        {
            ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
        if (ip == null)
        {
            ip = context.Request.ServerVariables.Get("REMOTE_ADDR");
        }

        return ip;
    }

    public static Boolean IsImage(string fileExt)
    {
        bool isImage = false;
        String[] exts = new String[] { ".jpe", ".jpeg", ".jpg", ".gif", ".bmp", ".png" };
        foreach (String ext in exts)
        {
            if (fileExt.ToLower() == ext)
            {
                isImage = true;
            }
        }

        return isImage;
    }

    public static String getSavePath(DateTime reg_date)
    {
        String savePath = "/upload";
        savePath += ("/" + (string)reg_date.ToString("yyyy"));
        savePath += ("/" + (string)reg_date.ToString("MM"));
        savePath += ("/" + (string)reg_date.ToString("dd"));

        return savePath;
    }

    public static String getContentWithBr(String src)
    {
        //TextBox1.Text.Replace("\r\n", "</br>");
        //TextBox1.Text.Replace(Convert.ToString((char)13), "</br>");
        //Me.MyLiteral.Text = Me.MyTextBox.Text.Replace(ControlChars.NewLine, "<br />")
        //string text = txtBody.Text.Replace(Environment.NewLine, "<br />");

        return src.Replace("\r\n", "</br>");
    }

    public static String newQueryString(Dictionary<String,String> dics) {
        String qs = "";

        int i = 0;
        foreach (KeyValuePair<String, String> dic in dics)
        {
            if (i > 0)
            {
                qs += "&";
            }
            qs += dic.Key + "=" + dic.Value;

            i++;
        }

        return qs;
    }


}