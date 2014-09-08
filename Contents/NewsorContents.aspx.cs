﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contents_NewsorContents : System.Web.UI.Page
{
    #region Global Variable & PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        int catID = Convert.ToInt32(Request["SectionTypeID"]);
        GeneratePage(catID,DateTime.MinValue,DateTime.MinValue);
    }
    #endregion


    #region Events
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string message= this.ValidateSearch().Trim();

        if (message.Length > 0)
        {
            DisplayAlert(message);
        }
        else
        {
            DateTime fromDate = DateTime.MinValue, toDate = DateTime.MinValue;
            if (txtFromDate.Text.Trim() != "")
                fromDate = Convert.ToDateTime(txtFromDate.Text);
            if (txtToDate.Text.Trim() != "")
                toDate = Convert.ToDateTime(txtToDate.Text);

            GeneratePage(Convert.ToInt32(Request["SectionTypeID"]), fromDate, toDate);
        }
       

    
    }
    #endregion

    #region Method

    private void DisplayAlert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg.Replace("'", "\\'").Replace(Environment.NewLine, "\\n")), true);
    }

    private string ValidateSearch()
    {
        string _message = "";

        if (txtFromDate.Text.ToString().Trim().Length != 0)
        {
            if (!Utility.IsDate(txtFromDate.Text.ToString()))
            {
                _message += "Please enter valid From date" + Environment.NewLine;
            }
        }
        if (txtToDate.Text.ToString().Trim().Length != 0)
        {
            if (!Utility.IsDate(txtToDate.Text.ToString()))
            {
                _message += "Please enter valid To date" + Environment.NewLine;
            }
        }
        return _message;
    }
    private void GeneratePage(int CategoryTypeID, DateTime fromdate, DateTime todate)
    {
        List<ContentObj> cObjs = new List<ContentObj>();
        ContentObj contents = new ContentObj(cmscon.CONNECTIONSTRING);

        if (Convert.ToInt32(Request["SectionTypeID"]) == (int)SectionTypeEnum.News)
        {
            cObjs = contents.getRecords(CategoryTypeID, fromdate, todate);
            lblRcentTitle.Text = "Recent News at OSF";
        }
        else
        {
            int catID = Convert.ToInt32(Request["SectionTypeID"]);

            SectionTypeEnum enumDisplayStatus = ((SectionTypeEnum)catID);
            string stringValue = enumDisplayStatus.ToString();

            lblRcentTitle.Text = string.Format("Recent {0}s at OSF",stringValue);
            cObjs = contents.getRecordsWithPermission(catID, fromdate, todate, Convert.ToInt32(Session["UserID"]));
        }


        if ((cObjs != null) & cObjs.Count > 0)
        {
          
            System.Text.StringBuilder tbl = new System.Text.StringBuilder();

            tbl.Append(" <div class='news-osf'>");
            foreach (ContentObj cO in cObjs)
            { 
                tbl.Append(string.Format(  "<div class='recent-news'> <p> <strong>Date:</strong>{0}</p> <p> <strong>Title:</strong><a href='#'>{1}</a></p> <p> <strong>From:</strong>{2}</p> <p> <strong>Description:</strong> {3} <a href='#'>Read More</a> </p> </div>",cO.Date.ToString("MM/dd/yyyy"),cO.Title, cO.Author, cO.Content));
            }

            tbl.Append("</div>");
            dynamicDiv.InnerHtml = tbl.ToString();

        }


    }
    #endregion


}