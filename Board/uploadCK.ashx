<%@ WebHandler Language="C#" Class="uploadCK" %>

using System;
using System.Web;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;


public class uploadCK : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        context.Response.ContentType = "application/json";  //text/plain
        
        HttpPostedFile file = context.Request.Files["upload"];
        string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
        CK ck = (new Upload()).fileUpload(file);

        context.Response.Write("{");
        context.Response.Write("    \"uploaded\": \"" + ck.uploaded + "\", ");
        context.Response.Write("    \"fileName\": \"" + ck.fileName + "\", ");
        context.Response.Write("    \"url\": \"" + ck.url + "\", ");
        context.Response.Write("    \"success\": {    ");
        context.Response.Write("        \"message\": \"" + CKEditorFuncNum + "\" ");
        context.Response.Write("    }");
        context.Response.Write("}");
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}