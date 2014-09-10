using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Contents_BoxContents : System.Web.UI.Page
{

    #region Global Variable & PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["UserPermission"] == null || Session["UserID"] == null || Session["UserSectionPermission"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        //else
        //{
           
        //    System.Data.DataTable dtUSP = (System.Data.DataTable)Session["UserSectionPermission"];
        //    int catID = Convert.ToInt32(Request["SectionTypeID"]);
        //    string query = string.Format("SectionID={0} AND IsSection=1", catID);
        //    DataRow dr = dtUSP.Select(query).FirstOrDefault();
        //    if (!Convert.ToBoolean(dr["HasPermission"]))
        //    {
        //        Response.Redirect("../Default.aspx");
        //    }

        //}
        //if (Request["SectionTypeID"] != null || Convert.ToInt32(Request["SectionTypeID"]) > 0)
        //{
        //int catID = Convert.ToInt32(Request["SectionTypeID"]);
        GeneratePage(0, DateTime.MinValue, DateTime.MinValue);
        //}
    }
    #endregion


    #region Events

    #endregion

    #region Method
    private void GeneratePage(int CategoryTypeID, DateTime fromdate, DateTime todate)
    {

         System.Text.StringBuilder tbl = new System.Text.StringBuilder();

            for(int I=0; I<10; I++)
            {
                tbl.Append(string.Format(@"<table width='200' cellspacing='0' cellpadding='0' border='0'>
	                                        <tbody><tr>
		                                        <td width='17'><img style='border-width:0px;' src='../Images/TitleBar/TitleLeft.gif' id='OneTitle_Image1'></td>
		                                        <td width='' background='../Images/TitleBar/TitleLeftMiddleSpacer.gif'><span class='TitleText'>AC Planning Time Line</span></td>
		                                        <td width='17'><img style='border-width:0px;' src='../Images/TitleBar/TitleMiddle.gif' id='OneTitle_Image2'></td>
		                                        <td width='0px' background='../Images/TitleBar/TitleMiddleSpacer.gif'><span class='TitleText'>&nbsp;</span></td>
		                                        <td width='1'><img style='border-width:0px;' src='../Images/TitleBar/TitleRight.gif' id='OneTitle_Image3'></td>
	                                        </tr>
                                        </tbody></table>"));
            }


            dynamicDiv.InnerHtml = tbl.ToString();



        //List<ContentObj> cObjs = new List<ContentObj>();
        //ContentObj contents = new ContentObj(cmscon.CONNECTIONSTRING);

        //if (Convert.ToInt32(Request["SectionTypeID"]) == (int)SectionTypeEnum.News)
        //{
        //    //cObjs = contents.getRecords(CategoryTypeID, fromdate, todate);
        //    //lblRcentTitle.Text = "News at OSF";
        //}
        //else
        //{
        //    int catID = Convert.ToInt32(Request["SectionTypeID"]);

        //    SectionTypeEnum enumDisplayStatus = ((SectionTypeEnum)catID);
        //    string stringValue = enumDisplayStatus.ToString();

        //    lblRcentTitle.Text = string.Format("Recent {0}s", stringValue);
        //    cObjs = contents.getRecordsWithPermission(catID, fromdate, todate, Convert.ToInt32(Session["UserID"]));
        //}


        //if ((cObjs != null) & cObjs.Count > 0)
        //{

        //    System.Text.StringBuilder tbl = new System.Text.StringBuilder();

        //    tbl.Append(" <div class='news-osf'>");
        //    foreach (ContentObj cO in cObjs)
        //    {
        //        tbl.Append(string.Format("<div class='recent-news'> <p> <strong>Date:</strong>{0}</p> <p> <strong>Title:</strong><a href='#'>{1}</a></p> <p> <strong>From:</strong>{2}</p> <p> <strong>Description:</strong> {3} <a href='#'>Read More</a> </p> </div>", cO.Date.ToString("MM/dd/yyyy"), cO.Title, cO.Author, cO.Content));
        //    }

        //    tbl.Append("</div>");
        //    dynamicDiv.InnerHtml = tbl.ToString();

       // }


    }

    #endregion

}