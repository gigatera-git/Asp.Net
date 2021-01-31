using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Web.Configuration;

/// <summary>
/// Upload의 요약 설명입니다.
/// HttpContext.Current.Response.Write("fileSaveName : " + fileSaveName + "<br>");
/// </summary>
public class Upload
{
	public Upload()
	{
        
	}

    public List<ArticleUP> fileUpload(HttpFileCollection files)
    {
        List<ArticleUP> arr = new List<ArticleUP>();
        for (int i = 0; i < files.Count;i++)
        {
            HttpPostedFile file = files[i];
            ArticleUP aup = new ArticleUP();

            String fileRealName = file.FileName; 
            String fileExt = Path.GetExtension(fileRealName); // .exe 와 같이 .이 같이 옴
            String fileSaveName = getUniqFileName(getSaveDir(), getUUID() + fileExt);

            Int64 fileSize = file.ContentLength;

            if (fileSize <= 0)
            {
                //선택한 파일이 없거나, 있어도 빈 파일인 경우
            }
            else if (fileSize > getAllowMaxLength()) 
            {
                //HttpContext.Current.Response.Write("에러 : " + fileRealName + "은 업로드용량("+ getAllowMaxLength().toString() +")을 초과하였습니다<br><br>");
            }
            else if (IsDisabledFile(fileExt))
            {
                //fileSaveName HttpContext.Current.Response.Write("에러 : " + fileRealName + "은 등록불가 파일입니다<br><br>");
            }
            else
            {
                try
                {
                    file.SaveAs(HttpContext.Current.Server.MapPath(getSaveDir() + "/" + fileSaveName));
                    
                    aup.fileRealName = fileRealName;
                    aup.fileSaveName = fileSaveName;
                    aup.fileSize = fileSize.ToString();
                    aup.reg_ip = Proc.getClientIp();

                    arr.Add(aup);
                }
                catch (Exception e)
                {
                    HttpContext.Current.Response.Write(e.StackTrace + "<br><br>");
                }

            }
        }

        return arr;
    }


    public CK fileUpload(HttpPostedFile file)
    {
        String fileRealName = file.FileName;
        String fileExt = Path.GetExtension(fileRealName); // .exe 와 같이 .이 같이 옴
        String fileSaveName = getUniqFileName(getSaveDir(), getUUID() + fileExt);
        Int64 fileSize = file.ContentLength;

        CK ck = new CK();

        if (fileSize <= 0)
        {
            //선택한 파일이 없거나, 있어도 빈 파일인 경우
            ck.uploaded = '0';
        }
        else if (fileSize > getAllowMaxLength())
        {
            //HttpContext.Current.Response.Write("에러 : " + fileRealName + "은 업로드용량("+ getAllowMaxLength().toString() +")을 초과하였습니다<br><br>");
            ck.uploaded = '0';
        }
        else if (IsDisabledFile(fileExt))
        {
            //fileSaveName HttpContext.Current.Response.Write("에러 : " + fileRealName + "은 등록불가 파일입니다<br><br>");
            ck.uploaded = '0';
        }
        else
        {
            try
            {
                file.SaveAs(HttpContext.Current.Server.MapPath(getSaveDir() + "/" + fileSaveName));
                ck.uploaded = '1';
                ck.fileName = fileRealName;
                ck.url = getSaveDir() + "/" + fileSaveName;
            }
            catch (Exception e)
            {
                //HttpContext.Current.Response.Write(e.StackTrace + "<br><br>");
                ck.uploaded = '0';
            }
        }
        return ck;
    }

    public String getSaveDir()
    {
        String savePath = "/upload";
        savePath += ("/" + (string)DateTime.Now.ToString("yyyy"));
        savePath += ("/" + (string)DateTime.Now.ToString("MM"));
        savePath += ("/" + (string)DateTime.Now.ToString("dd"));

        String saveDir = HttpContext.Current.Server.MapPath(savePath);
        bool exists = Directory.Exists(saveDir);
        if (!exists)
        {
            Directory.CreateDirectory(saveDir);
        }
        return savePath;
    }

    public Int64 getAllowMaxLength()
    {
        Int64 maxRequestLength = 0;
        HttpRuntimeSection section =
        ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
        if (section != null)
            maxRequestLength = section.MaxRequestLength;

        return maxRequestLength;
    }

    public String getUUID()
    {
        return Guid.NewGuid().ToString("N");
    }

    public Boolean IsDisabledFile(String fileExt)
    {
        return (".exe.bat.com.dll.asp.aspx.cs.java.py.rb.sys.c.cpp.pl.js.html.htm").Contains(fileExt);
    }

    public String getUniqFileName(String path, String fileName)
    {
        bool bExist = true;
        int Cnt = 0;
        String saveFile = HttpContext.Current.Server.MapPath(path + "/" + fileName);

        String uniqFile = fileName;
        while (bExist)
        {
            if (File.Exists(saveFile))
            {
                Cnt++;
                String fExt = Path.GetExtension(fileName);
                String fName = fileName.Substring(0, fileName.IndexOf(fExt));
                uniqFile = fName + "(" + Cnt.ToString() + ")" + fExt;
            }
            else
            {
                bExist = false;
            }
        }

        return uniqFile;
    }
}