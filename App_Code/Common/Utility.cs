using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;

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

    public static string GeneratePopupContentFromContentID(int contentID)
    {
        string htmlData = "";
        ContentObj c0 = new ContentObj();
  
        DataRow dr =  cmscon.getRows(string.Format("SELECT * from Content WHERE ContentID={0}",contentID)).Rows[0];
        c0 = c0.MakeRowToObject(dr);


        if (c0 != null && c0.ContentID > 0)
        {
            htmlData = string.Format(@"  <span class='button b-close'><span>X</span></span> <table cellpadding='4' border='0'>
				<tbody>

                <tr>
					<td>
						<span class='standardTextBold' id='TitleTitle'></span>
						<span id='Title'>{0}</span>
					</td>
				
				</tr>
				<tr>
					<td colspan='2'>
						<span class='standardTextBold' id='PostDateTitle'></span>
						<span id='PostDate'>{1}</span>
					</td>
				</tr>
				<tr>
					<td colspan='2'>
						<span class='standardTextBold' id='AuthorTitle'></span>
						<span id='Author'>{2}</span>
					</td>
				</tr>
				<tr>
					<td colspan='2'>
						<span id='Content'><p>{3}</p></span></td>
				</tr>
				<tr>
				
					<td colspan='2'>
						        <input type='url' value='Edit' onclick='EditContent({4})' class='clsPopupLink' />
					</td>

				</tr>



			</tbody></table>      ", c0.Title, c0.Date.ToString("dd/MM/yyyy"), c0.Author,c0.Content,c0.ContentID);
        }
        else
            htmlData = "No Data Found";


        return htmlData;
    }

    //EditContentRow
    public static string GeneratePopupContentFromContentIDR(int contentID)
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
						<span id='Title'>{0}</span>
					</td>
				
				</tr>
				<tr>
					<td colspan='2'>
						<span class='standardTextBold' id='PostDateTitle'></span>
						<span id='PostDate'>{1}</span>
					</td>
				</tr>
				<tr>
					<td colspan='2'>
						<span class='standardTextBold' id='AuthorTitle'></span>
						<span id='Author'>{2}</span>
					</td>
				</tr>
				<tr>
					<td colspan='2'>
						<span id='Content'><p>{3}</p></span></td>
				</tr>
				<tr>
				
					<td colspan='2'>
						        <input type='url' value='Edit' onclick='EditContentRow({4})' class='clsPopupLink' />
					</td>

				</tr>



			</tbody></table>      ", c0.Title, c0.Date.ToString("dd/MM/yyyy"), c0.Author, c0.Content, c0.ContentID);
        }
        else
            htmlData = "No Data Found";


        return htmlData;
    }
    public static DataRow dr { get; set; }
}