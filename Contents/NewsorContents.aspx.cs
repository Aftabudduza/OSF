﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Contents_NewsorContents : System.Web.UI.Page
{
    #region Global Variable & PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Method"] != null && Request.QueryString["Method"] == "GetPopupContent")
        {
            int a = Convert.ToInt32(Request.QueryString["ID"]);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            GetServerDPopupHTML(a);
        }
        else
        {

            if (Session["User"] == null || Session["UserPermission"] == null || Session["UserID"] == null || Session["UserSectionPermission"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                //   Users objUser = (Users)Session["User"];
                //UserPermissions dtUP = (UserPermissions)Session["UserPermission"];
                System.Data.DataTable dtUSP = (System.Data.DataTable)Session["UserSectionPermission"];
                int catID = Convert.ToInt32(Request["SectionTypeID"]);

                string query = string.Format("SectionID={0} AND IsSection=1", catID);
                DataRow dr = dtUSP.Select(query).FirstOrDefault();
                if (!Convert.ToBoolean(dr["HasPermission"]))
                {
                    Response.Redirect("../Default.aspx");
                }

            }
            if (Request["SectionTypeID"] != null || Convert.ToInt32(Request["SectionTypeID"]) > 0)
            {
                int catID = Convert.ToInt32(Request["SectionTypeID"]);
                GeneratePage(catID, DateTime.MinValue, DateTime.MinValue);
                ucSearch.CategoryID = catID;
                ucSearch.SectionType = Convert.ToInt32(Request["SectionTypeID"]);
                ucSearch.UserID = Convert.ToInt32(Session["UserID"]);
                ucSearch.buttonClick += new EventHandler(GeneratePageFromContentFomSearchControl);
            }
        }
    }
    #endregion


    #region Events
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    string message= this.ValidateSearch().Trim();

    //    if (message.Length > 0)
    //    {
    //        DisplayAlert(message);
    //    }
    //    else
    //    {
    //        DateTime fromDate = DateTime.MinValue, toDate = DateTime.MinValue;
    //        if (txtFromDate.Text.Trim() != "")
    //            fromDate = Convert.ToDateTime(txtFromDate.Text);
    //        if (txtToDate.Text.Trim() != "")
    //            toDate = Convert.ToDateTime(txtToDate.Text);

    //        GeneratePage(Convert.ToInt32(Request["SectionTypeID"]), fromDate, toDate);
    //    }
       

    
    //}
    #endregion

    #region Method

    private void DisplayAlert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg.Replace("'", "\\'").Replace(Environment.NewLine, "\\n")), true);
    }

    //private string ValidateSearch()
    //{
    //    string _message = "";

    //    if (txtFromDate.Text.ToString().Trim().Length != 0)
    //    {
    //        if (!Utility.IsDate(txtFromDate.Text.ToString()))
    //        {
    //            _message += "Please enter valid From date" + Environment.NewLine;
    //        }
    //    }
    //    if (txtToDate.Text.ToString().Trim().Length != 0)
    //    {
    //        if (!Utility.IsDate(txtToDate.Text.ToString()))
    //        {
    //            _message += "Please enter valid To date" + Environment.NewLine;
    //        }
    //    }
    //    return _message;
    //}
    private bool CheckAdminPermission()
    {
        Users objUser = (Users)Session["User"];
        UserPermissions dtUP = (UserPermissions)Session["UserPermission"];
        System.Data.DataTable dtUSP = (System.Data.DataTable)Session["UserSectionPermission"];
        if (!dtUP.IsGlobalContentAdmin)
        {
            return false;
        }
        return true;
    }

    private void GetServerDPopupHTML(int a)
    {
        Response.Clear();
        Response.Write(Utility.GeneratePopupContentFromContentID(a, false, CheckAdminPermission()));
        Response.End();
    }

    private void GeneratePage(int CategoryTypeID, DateTime fromdate, DateTime todate)
    {
        List<ContentObj> cObjs = new List<ContentObj>();
        ContentObj contents = new ContentObj(osfcon.CONNECTIONSTRING);

        if (Convert.ToInt32(Request["SectionTypeID"]) == (int)EnumSectionType.News)
        {
            cObjs = contents.getRecordsForNewsType(CategoryTypeID, fromdate, todate);
            lblRcentTitle.Text = "News at OSF";
        }
        else
        {
            int sectionTypeID = Convert.ToInt32(Request["SectionTypeID"]);

            EnumSectionType enumDisplayStatus = ((EnumSectionType)sectionTypeID);
            string stringValue = enumDisplayStatus.ToString();

            lblRcentTitle.Text = string.Format("Recent {0}s",stringValue);
            cObjs = contents.getRecordsWithPermission(sectionTypeID, fromdate, todate, Convert.ToInt32(Session["UserID"]));
        }


        if ((cObjs != null) & cObjs.Count > 0)
        {
          
            System.Text.StringBuilder tbl = new System.Text.StringBuilder();

            tbl.Append(" <div class='news-osf'>");
            tbl.Append(string.Format(@"<table id='tablepagingNEWS' width='100%' cellspacing='0' cellpadding='0' border='0'>"));
            foreach (ContentObj cO in cObjs)
            {
       

                tbl.Append(string.Format(@"  
                                               <tr>
                                                   <td>
                                                        <div class='recent-news'>
                                                            <p> <strong>Date:</strong>{0}</p> 
                                                            <p> <strong><input type='button' style='width:100%;' value='{1}' onclick='GetPopupContent({4})' class='clsPopupLink' /> </strong></p> 
                                                            <p> <strong>From:</strong>{2}</p> 
                                                            <p> <strong>Description:</strong> {3} ... </p> 
                                                       </div>
                                                 </td>
                                              </tr>", cO.Date.ToString("MM/dd/yyyy"), cO.Title, cO.Author, cO.Content.Length <= 280 ? cO.Content : cO.Content.Substring(0,279), cO.ContentID));
            }

            tbl.Append("</table>");
            tbl.Append("</div>");
           tbl.Append(string.Format(@" <div id='pageNavPositionNEWS' style='padding-top: 0' align='center'>
                                                    </div>
                                                    <script type='text/javascript'><!--
                                                            var pagerNEWS = new Pager('tablepagingNEWS', 10);
                                                            pagerNEWS.init();
                                                            pagerNEWS.showPageNav('pagerNEWS', 'pageNavPositionNEWS');
                                                            pagerNEWS.showPage(1);
                                                    </script>"));

            dynamicDiv.InnerHtml = tbl.ToString();

        }


    }

    private void GeneratePageFromContentFomSearchControl(object sender, EventArgs e)
    {

        List<ContentObj> cObjs = ucSearch.Contents; 
        if (cObjs != null && cObjs.Count > 0)
        {

            System.Text.StringBuilder tbl = new System.Text.StringBuilder();

            tbl.Append(" <div class='news-osf'>");
            tbl.Append(string.Format(@"<table id='tablepagingNEWS' width='100%' cellspacing='0' cellpadding='0' border='0'>"));
            foreach (ContentObj cO in cObjs)
            {


                tbl.Append(string.Format(@"  
                                               <tr>
                                                   <td>
                                                        <div class='recent-news'>
                                                            <p> <strong>Date:</strong>{0}</p> 
                                                            <p> <strong><input type='button' style='width:100%;' value='{1}' onclick='GetPopupContent({4})' class='clsPopupLink' /> </strong></p> 
                                                            <p> <strong>From:</strong>{2}</p> 
                                                            <p> <strong>Description:</strong> {3} ... </p> 
                                                       </div>
                                                 </td>
                                              </tr>", cO.Date.ToString("MM/dd/yyyy"), cO.Title, cO.Author, cO.Content.Length <= 280 ? cO.Content : cO.Content.Substring(0, 279), cO.ContentID));
            }

            tbl.Append("</table>");
            tbl.Append("</div>");
            tbl.Append(string.Format(@" <div id='pageNavPositionNEWS' style='padding-top: 0' align='center'>
                                                    </div>
                                                    <script type='text/javascript'><!--
                                                            var pagerNEWS = new Pager('tablepagingNEWS', 10);
                                                            pagerNEWS.init();
                                                            pagerNEWS.showPageNav('pagerNEWS', 'pageNavPositionNEWS');
                                                            pagerNEWS.showPage(1);
                                                    </script>"));

            dynamicDiv.InnerHtml = tbl.ToString();

        }


    }
    #endregion


}