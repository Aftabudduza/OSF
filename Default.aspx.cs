﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    int _catID = 0; bool _hasAdminPermission = false;

    #region Global Variable & PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["UserPermission"] == null || Session["UserID"] == null || Session["UserSectionPermission"] == null)
        {
            Response.Redirect("~/Admin/Login.aspx");
        }
        else
        {
          _hasAdminPermission =  CheckAdminPermission();
        }
        if (!IsPostBack)
        {
            if (Session["UserID"] != null)
            {

                _catID = (int)SectionTypeEnum.News;
                GeneratePage(_catID, DateTime.MinValue, DateTime.MinValue);
            }

        }


    }
    #endregion


    #region Events

    #endregion

    #region Method

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

    private void GeneratePage(int CategoryTypeID, DateTime fromdate, DateTime todate)
    {
        List<ContentObj> cObjs = new List<ContentObj>();
        ContentObj contents = new ContentObj(cmscon.CONNECTIONSTRING);

        List<HomePageSettings> homePageSettings = new List<HomePageSettings>();
        HomePageSettings hpTemp = new HomePageSettings(cmscon.CONNECTIONSTRING);
        int count = 0;

        homePageSettings = hpTemp.getRecordsbyQuery(string.Format(@"SELECT * FROM HomePageSettings WHERE IsSection=0 AND HomePageColumnType={0}", (int)HomePageColumnType.MiddleColumn));
        System.Text.StringBuilder tbl = null;
        if ((homePageSettings != null) & homePageSettings.Count > 0)
        {
            tbl = new System.Text.StringBuilder();
             
            foreach (HomePageSettings seti in homePageSettings)
            {
                if (seti.WillShow)
                {
                    cObjs = contents.getRecordsbyCategoryID(seti.CategoryID);
                    Categories cc = new Categories(cmscon.CONNECTIONSTRING);
                    cc.getRecord(seti.CategoryID);

                    if (cObjs.Count > 0)
                    {
                        tbl.Append(string.Format(@"<h3>{0}</h3>", cc.Description));
                        tbl.Append(" <div class='news-osf' style='margin-bottom: 10px;'>");
                    }
                    count = 0;
                    foreach (ContentObj cO in cObjs)
                    {
                        count++;
                        if (count <= 5)
                        {
                            tbl.Append(string.Format(@"<div class='recent-news'> 
                        <p> <strong>Date:</strong>{0}</p>
                        <p> <strong><input type='button' style='width:100%;' value='{1}' onclick='GetPopupContentDefaultPage({4})' class='clsPopupLink' /> </strong></p> 
                        <p> <strong>From:</strong>{2}</p> 
                        <p> <strong>Description:</strong> {3} <a href='#'>Read More</a> </p>

                                    </div>", cO.Date.ToString("MM/dd/yyyy"), cO.Title, cO.Author, cO.Content, cO.ContentID));
                        }
                    }
                    tbl.Append("</div>");
                }
           
                
            }

            dynamicMidDiv.InnerHtml = tbl.ToString();
        }

        homePageSettings = hpTemp.getRecordsbyQuery(string.Format(@"SELECT * FROM HomePageSettings WHERE IsSection=0 AND HomePageColumnType={0}", (int)HomePageColumnType.LeftColumn));
        tbl = null;
        if ((homePageSettings != null) & homePageSettings.Count > 0)
        {
            tbl = new System.Text.StringBuilder();

            foreach (HomePageSettings seti in homePageSettings)
            {
                if (seti.WillShow)
                {
                    cObjs = contents.getRecordsbyCategoryID(seti.CategoryID);
                    Categories cc = new Categories(cmscon.CONNECTIONSTRING);
                    cc.getRecord(seti.CategoryID);

                    tbl.Append(string.Format(@"<div class='colomn'>
                                        <h3>
                                            {0}</h3>
                                        <div class='notices'>
                                            <ul>",cc.Description));

                    count = 0;
                    foreach (ContentObj cO in cObjs)
                    {
                        count++;
                        if (count <= 5)
                        {
                            tbl.Append(string.Format(@" <li><span>{0}</span>
                                                        <a onclick='GetPopupContentDefaultPage({2})' href='#'>{1}</a></li>", cO.Date.ToString("dd/MM/yyyy"), cO.Title, cO.ContentID));
                        }
                    }
                    tbl.Append(string.Format(@"  </ul>
                                        </div>
                                    </div>"));



                }


            }

            dynamicLeftDiv.InnerHtml = tbl.ToString();
        }


        homePageSettings = hpTemp.getRecordsbyQuery(string.Format(@"SELECT * FROM HomePageSettings WHERE IsSection=0 AND HomePageColumnType={0}", (int)HomePageColumnType.RightColumn));
        tbl = null;
        if ((homePageSettings != null) & homePageSettings.Count > 0)
        {
            tbl = new System.Text.StringBuilder();

            foreach (HomePageSettings seti in homePageSettings)
            {
                if (seti.WillShow)
                {
                    cObjs = contents.getRecordsbyCategoryID(seti.CategoryID);
                    Categories cc = new Categories(cmscon.CONNECTIONSTRING);
                    cc.getRecord(seti.CategoryID);

                    tbl.Append(string.Format(@"<div class='colomn'>
                                        <h3>
                                            {0}</h3>
                                        <div class='notices'>
                                            <ul>", cc.Description));


                    count = 0;
                    foreach (ContentObj cO in cObjs)
                    {
                        count++;
                        if (count <= 5)
                        {
                            tbl.Append(string.Format(@" <li><span>{0}</span><a onclick='GetPopupContentDefaultPage({2})' href='#'>{1}</a></li>", cO.Date.ToString("dd/MM/yyyy"), cO.Title, cO.ContentID));
                        }
                    }
                    tbl.Append(string.Format(@"  </ul>
                                        </div>
                                    </div>"));



                }


            }

            dynamicRightDiv.InnerHtml = tbl.ToString();
        }
      
       //dynamicRightDiv.InnerHtml = tbl.ToString();

    }

    #endregion
}
