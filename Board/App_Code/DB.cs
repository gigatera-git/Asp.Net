using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

/// <summary>
/// DB의 요약 설명입니다.
/// </summary>
public class DB : IDisposable
{
    private String connStr;
    private OleDbConnection conn;


    public String ConnStr 
    {
        get { return this.connStr; }
        set { this.connStr = value; }
    }

    public OleDbConnection Conn
    {
        get { return this.conn; }
        set { this.conn = value; }
    }
    
    public DB()
	{
        this.ConnStr = ConfigurationManager.AppSettings["connectStr"];
        this.Conn = new OleDbConnection(this.connStr);
	}

    ~DB()
    {
        
    }

    public void Dispose()
    {
        if (this.conn.State == ConnectionState.Open)
        {
            this.conn.Close();
            this.conn.Dispose();
        }
    }

    
}