using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.IO;

public class cmscon
{
    private static SqlConnection conn = null;
    public static string lastError = "";

    public static string CONNECTIONSTRING
    {
        get
        {
            try
            {
                return Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            }
            catch (Exception ex)
            {

            }
            return "";
        }
    }
    public static int WebsiteID
    {
        get
        {
            try
            {
                return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WebsiteID"]);
            }
            catch (Exception ex)
            {

            }
            return 0;
        }
    }
    public static DataTable getRows(string sql)
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        try
        {
            connect();
            cmd = new SqlCommand(sql, conn);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            disconnect();
            return dt;
        }
        catch (SqlException ex)
        {
            lastError = translateException(ex);
        }
        catch (Exception ex)
        {
            lastError = ex.Message;
        }
        return null;
    }
    private static void connect()
    {
        if (CONNECTIONSTRING == "")
        {
            if (conn == null) throw new Exception("Database not connected");
        }
        else
        {
            conn = new SqlConnection(CONNECTIONSTRING);
            conn.Open();
        }
    }
    private static void disconnect()
    {
        if (!CONNECTIONSTRING.Equals("") && conn != null)
        {
            try
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            catch (Exception ex) { }
        }
    }
    private static string translateException(SqlException ex)
    {
        string p = "";
        foreach (SqlError er in ex.Errors)
            p += er.Message + "\r\n";
        return p;
    }

    public static string URLPATH(ref System.Web.HttpRequest Req)
    {
        string sRoot = "http://" + Req.Url.Host + ":" + Req.Url.Port + "" + Req.ApplicationPath;
        return sRoot;
    }  

    public static string ENC_PASS = "Jh3YdgQh4gBD0pQlM8b6xFH";
    public static string GetCMS_Message(string sPageRef, int iWebId, string sDefaultMessage)
    {
        string s = "";  

        try {
            string sSQL = "SELECT TOP 1 page_content FROM pageinfo where page_ref='" + sPageRef.Replace("'", "''") + "' AND WebsiteID=" + iWebId;

            DataTable ResultSet = cmscon.getRows(sSQL);

            // Create the second-level nodes
            if (ResultSet != null)
            {
                if (ResultSet.Rows.Count > 0)
                {
                    s = ResultSet.Rows[0]["page_content"].ToString();
                }
                else
                {
                    s = "";
                }
            }

            if (ResultSet.Rows.Count == 0)
            {
                //add if not Found!!!
                s = sDefaultMessage;
                string sIns = "INSERT INTO pageinfo (page_ref,WEBSITEID,page_content, page_title, create_date, modify_date) VALUES (";
                sIns += "'" + sPageRef + "','" + iWebId + "','" + sDefaultMessage.Replace("'", "''") + "','" + sPageRef + "', getDate(), getDate())";
                SqlCommand cmd;
                connect();
                cmd = new SqlCommand(sIns);
                cmd.ExecuteScalar();
                cmd.Dispose();
                disconnect();

                //reset so it doesn't duplicate
                s = "";
            }


        }
        catch(Exception ex)
        {}

        return s;
    }
    
    #region "Image Control"

    public static string NotFoundFileName()
    {
        string sSQL = "select imagerootpath + '/'+ notfoundfilename from website where id=" + cmscon.WebsiteID;
       
        DataTable ResultSet = cmscon.getRows(sSQL);
        string s = "";
        // Create the second-level nodes
        if (ResultSet != null)
        {
            if (ResultSet.Rows.Count > 0)
            {
                s = ResultSet.Rows[0]["imagerootpath"].ToString();
            }
            else
            {
                s = "";
            }
        }
        return s;
    }

    public static string ImageRootPath()
    {
        string sSQL = "select imagerootpath  from website where id=" + cmscon.WebsiteID;
        DataTable ResultSet = cmscon.getRows(sSQL);
        string s = "";
        // Create the second-level nodes
        if (ResultSet != null)
        {
            if (ResultSet.Rows.Count > 0)
            {
                s = ResultSet.Rows[0]["imagerootpath"].ToString();
            }
            else
            {
                s = "";
            }
        }
        return s;
    }
    public static string ThumbRootPath()
    {
        string sSQL = "select thumbrootpath  from website where id=" + cmscon.WebsiteID;
        DataTable ResultSet = cmscon.getRows(sSQL);
        string s = "";
        // Create the second-level nodes
        if (ResultSet != null)
        {
            if (ResultSet.Rows.Count > 0)
            {
                s = ResultSet.Rows[0]["thumbrootpath"].ToString();
            }
            else
            {
                s = "";
            }
        }
        return s;

    }
    public static string ImageRootFilePath()
    {
        string sSQL = "select imagerootfilepath from website where id=" + cmscon.WebsiteID;
        DataTable ResultSet = cmscon.getRows(sSQL);
        string s = "";
        // Create the second-level nodes
        if (ResultSet != null)
        {
            if (ResultSet.Rows.Count > 0)
            {
                s = ResultSet.Rows[0]["imagerootfilepath"].ToString();
            }
            else
            {
                s = "";
            }
        }
        return s;
    }
    public static string ThumbRootFilePath()
    {
        string sSQL = "select thumbrootfilepath  from website where id=" + cmscon.WebsiteID;
        DataTable ResultSet = cmscon.getRows(sSQL);
        string s = "";
        // Create the second-level nodes
        if (ResultSet != null)
        {
            if (ResultSet.Rows.Count > 0)
            {
                s = ResultSet.Rows[0]["thumbrootfilepath"].ToString();
            }
            else
            {
                s = "";
            }
        }
        return s;
    }

    public static int THUMBIMAGEWIDTH()
    {
        string sSQL = "select thumbimagewidth  from website where id=" + cmscon.WebsiteID;
        
        DataTable ResultSet = cmscon.getRows(sSQL);
        int s = 0;
        // Create the second-level nodes
        if (ResultSet != null)
        {
            if (ResultSet.Rows.Count > 0)
            {
                s = Convert.ToInt32(ResultSet.Rows[0]["thumbimagewidth"]);

            }
            else
            {
                s = 0;
            }
        }
      
        return s;
    }
    #endregion

    #region "ImageRotation"

    public static string FixPath(string sPath)
    {
        if (sPath.Length > 0)
        {
            if (sPath.Substring(sPath.Length - 1, 1) != "\\")
            {
                sPath += "\\";
            }
        }
        return sPath;
    }
    public static string FixURLPath(string sPath)
    {
        if (sPath.Length > 0)
        {
            if (sPath.Substring(sPath.Length - 1, 1) != "/")
            {
                sPath += "/";
            }
        }
        return sPath;
    }

    #endregion  
   
    #region Error
   
    public  static string ErrorLogPath
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["ErrorLogPath"]; }
    }
    public static void LogDataError(string Information)
    {
        // Always on
        try
        {
            StreamWriter sw = default(StreamWriter);
            string curPath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath;           
            sw = new StreamWriter(cmscon.FixPath(ErrorLogPath) + "SQLDataError.log", true);
            sw.WriteLine(DateTime.Now.ToString() + ": " + Information);
            sw.Flush();
            sw.Close();
            //later update function to write to __log table
            //Dim a As Boolean = (New Data).LogFileItCabinetEvent(sCabinetID, sConn, DateTime.Now(), "Source", "EventId", "Category", Information, sUserName)
        }
        catch (Exception ex)
        {
        }
    }
    #endregion

    #region "Email"
    public static bool Send_Email(string strUserTo, string strEmailTo, string sSubject, System.Text.StringBuilder sbBody, string sServer, string sEmailAccount, string sEmailPass, int iEmailPort, ref string sError)
    {
        System.Net.Mail.SmtpClient objSMTPClient = null;
        System.Net.Mail.MailMessage objCustomerEmail = null;


        try
        {
            objCustomerEmail = new System.Net.Mail.MailMessage();
            //Works also
            objSMTPClient = new System.Net.Mail.SmtpClient(sServer, iEmailPort);
            //25

            objSMTPClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            //objSMTPClient.UseDefaultCredentials = True
            //bypass the authentication if there is no pass
            if (!string.IsNullOrEmpty(sEmailPass.Trim()))
            {
                objSMTPClient.Credentials = new System.Net.NetworkCredential(sEmailAccount, sEmailPass);
            }
            objCustomerEmail.From = new System.Net.Mail.MailAddress(sEmailAccount);
           // objCustomerEmail.ReplyTo = new System.Net.Mail.MailAddress(sEmailAccount);

            objCustomerEmail.To.Add(new System.Net.Mail.MailAddress(strEmailTo.ToString()));
            //objCustomerEmail.Bcc.Add(New Net.Mail.MailAddress("betterwaythere@hotmail.com"))
            objCustomerEmail.Headers.Set("Return-Path", sEmailAccount);
            objCustomerEmail.IsBodyHtml = true;
            objCustomerEmail.Subject = sSubject;
            objCustomerEmail.Body = sbBody.ToString();
            objSMTPClient.Send(objCustomerEmail);

            objSMTPClient = null;
            objCustomerEmail = null;

            return true;
        }
        catch (Exception ex)
        {
            sError = ex.Message;
            cmscon.LogDataError("Error sending email: " + ex.Message);
            return false;
        }

    }

    public static bool Send_Email_Attachments(string strUserTo, string strEmailTo, string sSubject, System.Text.StringBuilder sbBody, ArrayList aFiles, string sServer, string sEmailAccount, string sEmailPass, int iEmailPort, ref string sError)
    {
        System.Net.Mail.SmtpClient objSMTPClient = null;
        System.Net.Mail.MailMessage objCustomerEmail = null;


        try
        {
            objCustomerEmail = new System.Net.Mail.MailMessage();
            //Works also
            objSMTPClient = new System.Net.Mail.SmtpClient(sServer, iEmailPort);
            //25
            objSMTPClient.Credentials = new System.Net.NetworkCredential(sEmailAccount, sEmailPass);
            objCustomerEmail.From = new System.Net.Mail.MailAddress(sEmailAccount);
           // objCustomerEmail.ReplyTo = new System.Net.Mail.MailAddress(sEmailAccount);
            objCustomerEmail.To.Add(new System.Net.Mail.MailAddress(strEmailTo.ToString()));
            //objCustomerEmail.Bcc.Add(New Net.Mail.MailAddress("betterwaythere@hotmail.com"))
            objCustomerEmail.Headers.Set("Return-Path", sEmailAccount);
            objCustomerEmail.IsBodyHtml = true;
            objCustomerEmail.Subject = sSubject;
            objCustomerEmail.Body = sbBody.ToString();
            foreach (string sFilePath in aFiles)
            {
                objCustomerEmail.Attachments.Add(new System.Net.Mail.Attachment(sFilePath));
            }

            objSMTPClient.Send(objCustomerEmail);

            objSMTPClient = null;
            objCustomerEmail = null;

            return true;
        }
        catch (Exception ex)
        {
            sError = ex.Message;
            cmscon.LogDataError("Error sending email: " + ex.Message);
            return false;
        }

    }
    #endregion

    public static string base64Encode(string sData)
    {
        try
        {
            byte[] encData_byte = new byte[sData.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
            string encodedData = Convert.ToBase64String(encData_byte);
            return (encodedData);
        }
        catch (Exception ex)
        {
            throw (new Exception("Error in base64Encode" + ex.Message));
        }
    }

    public static string base64Decode(string sData)
    {
        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        System.Text.Decoder utf8Decode = encoder.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(sData);
        int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        char[] decoded_char = new char[charCount];
        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        string result = new String(decoded_char);
        return result;
    }

}