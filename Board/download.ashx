<%@ WebHandler Language="C#" Class="download" %>

using System;
using System.Web;
using System.IO;
using System.Text;

public class download : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        String filePath = (context.Request.Params["filePath"] == null) ? "" : context.Request.Params["filePath"].Trim();
        String fileName = (context.Request.Params["fileName"] == null) ? "" : context.Request.Params["fileName"].Trim();

        if (filePath != "" && fileName != "")
        {
            if (File.Exists(context.Server.MapPath(filePath + "/" + fileName)))
            {
                byte[] fileByte = File.ReadAllBytes(context.Server.MapPath(filePath + "/" + fileName));

                //IE, Edge 브라우저에서 파일명 깨짐 방지
                string browser = context.Request.Browser.Type.ToUpper();
                string userAgent = context.Request.UserAgent.ToUpper();
                bool isIE = browser.StartsWith("IE") || browser.Contains("INTERNETEXPLORER") || userAgent.Contains("TRIDENT") || userAgent.Contains("MSIE");
                bool isEdge = userAgent.Contains("EDGE");
                if (isIE || isEdge)
                    fileName = HttpUtility.UrlEncode(fileName, UTF8Encoding.UTF8).Replace("+", " ");

                //파일 출력
                context.Response.Clear();
                context.Response.Buffer = true;
                context.Response.ContentType = "application/octet-stream";
                context.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                context.Response.AddHeader("Content-Transfer-Encoding", "Binary");
                context.Response.BinaryWrite(fileByte);
                context.Response.Flush();
                context.Response.SuppressContent = true;
                context.ApplicationInstance.CompleteRequest();    
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("해당 파일이 없습니다");
            }
        }
        else
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("파일경로 또는 파일명이 없습니다");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}