using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    int _catID = 0;

    #region Global Variable & PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
           
            _catID = (int)SectionTypeEnum.News;
            GeneratePage(_catID, DateTime.MinValue, DateTime.MinValue);
        }
        else
        {
            Response.Redirect("~/Admin/Login.aspx");
        }
    }
    #endregion


    #region Events

    #endregion

    #region Method
    private void GeneratePage(int CategoryTypeID, DateTime fromdate, DateTime todate)
    {
        List<ContentObj> cObjs = new List<ContentObj>();
        ContentObj contents = new ContentObj(cmscon.CONNECTIONSTRING);

        if (_catID == (int)SectionTypeEnum.News)
        {
            cObjs = contents.getRecords(CategoryTypeID, fromdate, todate);
           // lblRcentTitle.Text = "Recent News at OSF";
        }
        else
        {
            int catID = Convert.ToInt32(Request["SectionTypeID"]);

            SectionTypeEnum enumDisplayStatus = ((SectionTypeEnum)catID);
            string stringValue = enumDisplayStatus.ToString();

          //  lblRcentTitle.Text = string.Format("Recent {0}s at OSF", stringValue);
            cObjs = contents.getRecordsWithPermission(catID, fromdate, todate, Convert.ToInt32(Session["UserID"]));
        }


        if ((cObjs != null) & cObjs.Count > 0)
        {

            System.Text.StringBuilder tbl = new System.Text.StringBuilder();

            tbl.Append(" <div class='news-osf'>");
            foreach (ContentObj cO in cObjs)
            {
                tbl.Append(string.Format("<div class='recent-news'> <p> <strong>Date:</strong>{0}</p> <p> <strong>Title:</strong><a href='#'>{1}</a></p> <p> <strong>From:</strong>{2}</p> <p> <strong>Description:</strong> {3} <a href='#'>Read More</a> </p> </div>", cO.Date.ToString("MM/dd/yyyy"), cO.Title, cO.Author, cO.Content));
            }

            tbl.Append("</div>");
            dynamicDiv.InnerHtml = tbl.ToString();

        }


    }

    #endregion
}
