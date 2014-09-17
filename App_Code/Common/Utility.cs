using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Utility
/// </summary>
public static class Utility
{
    public static bool IsDate(string inputDate)
    {
        DateTime dt;
        bool isdate = DateTime.TryParse(inputDate, out dt);
        return isdate;
    }
    //EditContentRow
    public static string GeneratePopupContentFromContentID(int contentID, bool isBox)
    {
        string htmlData = "";
        ContentObj c0 = new ContentObj();

        DataRow dr = cmscon.getRows(string.Format("SELECT * from Content WHERE ContentID={0}", contentID)).Rows[0];
        c0 = c0.MakeRowToObject(dr);


        if (c0 != null && c0.ContentID > 0)
        {
            htmlData = string.Format(@"  <span class='button b-close'><span>X</span></span> <table cellpadding='4' border='0'>
				<tbody>

                <tr>
					<td>
						<span class='standardTextBold' id='TitleTitle'></span>
						<strong>Title: </strong><span id='TitleSpan'>{0}</span>
					</td>
                     <td>
                      
                            <img id=""Image1"" class=""NavButton"" style=""border-width:0px;"" src=""../App_Themes/images/Print.gif"" onmouseout=""this.src='../App_Themes/images/Print.gif'"" onmouseover=""this.src='../App_Themes/images/Print_over.gif'"" onclick=""JSOSFPrint('ContentSpan','AuthorSpan','DateSpan','TitleSpan')"">                        

                    </td>
				
				</tr>
				<tr>
					<td colspan='2'>
						<span class='standardTextBold' id='PostDateTitle'></span>
						<strong>Date: </strong><span id='DateSpan'>{1}</span>
					</td>
				</tr>
				<tr>
					<td colspan='2'>
						<span class='standardTextBold' id='AuthorTitle'></span>
						<strong>Author: </strong><span id='AuthorSpan'>{2}</span>
					</td>
				</tr>
				<tr>
					<td colspan='2'>
						<strong>Content: </strong>
                   </td> 
				</tr>
				<tr>
					<td colspan='2'>
						<span id='ContentSpan'><p>{3}</p></span>
                   </td>
				</tr>
				<tr>
				
					<td colspan='2'>
						        <input type='url' value='Edit' onclick='EditContent({4},{5})' class='clsPopupLink' style='background:#eff0e0;' />
					</td>

				</tr>


				<tr>
				
					<td colspan='2'>
						        <input type='url' value='Delete' onclick='DeleteContent({4},{6})' class='clsPopupLink' style='background:#eff0e0;' />
					</td>
	          

				</tr>

                <tr>
                       <td colspan='2'>
                     <a id='DocLink' target='_new' href='../Images/Content/{7}'>{0}</a>
                    </td>
                </tr>


			</tbody></table>      ", c0.Title, c0.Date.ToString("dd/MM/yyyy"), c0.Author, c0.Content, c0.ContentID, isBox ? 1 : 0, c0.CategoryID, c0.URL);
        }
        else
            htmlData = "No Data Found";


        return htmlData;
    }
    public static DataRow dr { get; set; }

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


    public static bool QueryExecute(string query)
    {
        SqlCommand cmd;


        try
        {
            connect();
            cmd = new SqlCommand(query, conn);
            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
        }
        catch (SqlException ex)
        {
            lastError = translateException(ex);
            disconnect();
            return false;
        }
        catch (Exception ex)
        {
            lastError = ex.Message;
            disconnect();
            return false;
        }
        return true;
    }
}