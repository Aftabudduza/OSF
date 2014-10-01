using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    int _catID = 0; bool _hasAdminPermission = false;

    #region Global Variable & PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckPasswordValidity();
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

                _catID = (int)EnumSectionType.News;
                GeneratePageStatic(_catID, DateTime.MinValue, DateTime.MinValue);
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
        ContentObj contents = new ContentObj(osfcon.CONNECTIONSTRING);

        List<HomePageSettings> homePageSettings = new List<HomePageSettings>();
        HomePageSettings hpTemp = new HomePageSettings(osfcon.CONNECTIONSTRING);
        int count = 0;

        homePageSettings = hpTemp.getRecordsbyQuery(string.Format(@"SELECT * FROM HomePageSettings WHERE IsSection=0 AND HomePageColumnType={0}", (int)HomePageColumnTypeEnum.MiddleColumn));
        System.Text.StringBuilder tbl = null;
        if ((homePageSettings != null) & homePageSettings.Count > 0)
        {
            tbl = new System.Text.StringBuilder();
             
            foreach (HomePageSettings seti in homePageSettings)
            {
                if (seti.WillShow)
                {
                    cObjs = contents.getRecordsbyCategoryID(seti.CategoryID);
                    Categories cc = new Categories(osfcon.CONNECTIONSTRING);
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
                        <p> <strong>Description:</strong> {3}  </p>

                                    </div>", cO.Date.ToString("MM/dd/yyyy"), cO.Title, cO.Author, cO.Content.Length > 200 ? (cO.Content.Substring(0, 199) + "...") : (cO.Content + "..."), cO.ContentID));
                        }
                    }
                    tbl.Append("</div>");
                }
           
                
            }

            dynamicMidDiv.InnerHtml = tbl.ToString();
        }

        homePageSettings = hpTemp.getRecordsbyQuery(string.Format(@"SELECT * FROM HomePageSettings WHERE IsSection=0 AND HomePageColumnType={0}", (int)HomePageColumnTypeEnum.LeftColumn));
        tbl = null;
        if ((homePageSettings != null) & homePageSettings.Count > 0)
        {
            tbl = new System.Text.StringBuilder();

            foreach (HomePageSettings seti in homePageSettings)
            {
                if (seti.WillShow)
                {
                    cObjs = contents.getRecordsbyCategoryID(seti.CategoryID);
                    Categories cc = new Categories(osfcon.CONNECTIONSTRING);
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
                        string dateS = "";
                        if (cO.Date <= Convert.ToDateTime("1 Jan 1980"))
                            dateS = "";
                        else
                            dateS = cO.Date.ToString("MM/dd/yyyy");

                        if (count <= 5)
                        {
                            tbl.Append(string.Format(@" <li><span>{0}</span>
                                                        <a onclick='GetPopupContentDefaultPage({2})' href='#'>{1}</a></li>", dateS, cO.Title, cO.ContentID));
                        }
                    }
                    tbl.Append(string.Format(@"  </ul>
                                        </div>
                                    </div>"));



                }


            }

            dynamicLeftDiv.InnerHtml = tbl.ToString();
        }


        homePageSettings = hpTemp.getRecordsbyQuery(string.Format(@"SELECT * FROM HomePageSettings WHERE IsSection=0 AND HomePageColumnType={0}", (int)HomePageColumnTypeEnum.RightColumn));
        tbl = null;
        if ((homePageSettings != null) & homePageSettings.Count > 0)
        {
            tbl = new System.Text.StringBuilder();

            foreach (HomePageSettings seti in homePageSettings)
            {
                if (seti.WillShow)
                {
                    cObjs = contents.getRecordsbyCategoryID(seti.CategoryID);
                    Categories cc = new Categories(osfcon.CONNECTIONSTRING);
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
                            tbl.Append(string.Format(@" <li><span>{0}</span><a onclick='GetPopupContentDefaultPage({2})' href='#'>{1}</a></li>", cO.Date.ToString("MM/dd/yyyy"), cO.Title, cO.ContentID));
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


    private void GeneratePageStatic(int CategoryTypeID, DateTime fromdate, DateTime todate)
    {
        List<ContentObj> cObjs = new List<ContentObj>();
        ContentObj contents = new ContentObj(osfcon.CONNECTIONSTRING);

        List<HomePageSettings> homePageSettings = new List<HomePageSettings>();
        HomePageSettings hpTemp = new HomePageSettings(osfcon.CONNECTIONSTRING);
        int count = 0;

        //homePageSettings = hpTemp.getRecordsbyQuery(string.Format(@"SELECT * FROM HomePageSettings WHERE IsSection=0 AND HomePageColumnType={0}", (int)HomePageColumnTypeEnum.MiddleColumn));

        DataTable categories = osfcon.getRows(string.Format(@"select * from Categories where CategoryTypeID in (17)
                                      "));

        System.Text.StringBuilder tbl = null;
        if ((categories != null) & categories.Rows.Count > 0)
        {
            tbl = new System.Text.StringBuilder();

            foreach (DataRow drCat in categories.Rows)
            {

                cObjs = contents.getRecordsbyCategoryID(Convert.ToInt32(drCat["CategoryID"]));
                    Categories cc = new Categories(osfcon.CONNECTIONSTRING);
                    cc.getRecord(Convert.ToInt32(drCat["CategoryID"]));

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
                        <p> <strong>Description:</strong> {3}  </p>

                                    </div>", cO.Date.ToString("MM/dd/yyyy"), cO.Title, cO.Author, cO.Content.Length > 200 ? (cO.Content.Substring(0, 199) + "...") : (cO.Content + "..."), cO.ContentID));
                        }
                    }
                    tbl.Append("</div>");
              


            }

            dynamicMidDiv.InnerHtml = tbl.ToString();
        }

        categories = osfcon.getRows(string.Format(@"select * from Categories where CategoryTypeID in (13) --,3,6,26
                                            union
                                            select * from Categories where CategoryTypeID in (3)
                                            union
                                            select * from Categories where CategoryTypeID in (6)
                                            union
                                            select * from Categories where CategoryTypeID in (26)"));

        tbl = null;
        if ((categories != null) & categories.Rows.Count > 0)
        {
            tbl = new System.Text.StringBuilder();

            foreach (DataRow drCat in categories.Rows)
            {

                cObjs = contents.getRecordsbyCategoryID(Convert.ToInt32(drCat["CategoryID"]));
                    Categories cc = new Categories(osfcon.CONNECTIONSTRING);
                    cc.getRecord(Convert.ToInt32(drCat["CategoryID"]));

                    tbl.Append(string.Format(@"<div class='colomn'>
                                        <h3>
                                            {0}</h3>
                                        <div class='notices'>
                                            <ul>", cc.Description));

                    count = 0;
                    foreach (ContentObj cO in cObjs)
                    {
                        count++;
                        string dateS = "";
                        if (cO.Date <= Convert.ToDateTime("1 Jan 1980"))
                            dateS = "";
                        else
                            dateS = cO.Date.ToString("MM/dd/yyyy");

                        if (count <= 5)
                        {
                            tbl.Append(string.Format(@" <li><span>{0}</span>
                                                        <a onclick='GetPopupContentDefaultPage({2})' href='#'>{1}</a></li>", dateS, cO.Title, cO.ContentID));
                        }
                    }
                    tbl.Append(string.Format(@"  </ul>
                                        </div>
                                    </div>"));



                


            }

            dynamicLeftDiv.InnerHtml = tbl.ToString();
        }


        categories = osfcon.getRows(string.Format(@"select * from Categories where CategoryTypeID in (18)
                                            union
                                            select * from Categories where CategoryTypeID in (27)
                                            union
                                            select * from Categories where CategoryTypeID in (30)
                                                "));

        tbl = null;
        if ((categories != null) & categories.Rows.Count > 0)
        {
            tbl = new System.Text.StringBuilder();

            foreach (DataRow drCat in categories.Rows)
            {

                cObjs = contents.getRecordsbyCategoryID(Convert.ToInt32(drCat["CategoryID"]));
                Categories cc = new Categories(osfcon.CONNECTIONSTRING);
                cc.getRecord(Convert.ToInt32(drCat["CategoryID"]));

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
                            tbl.Append(string.Format(@" <li><span>{0}</span><a onclick='GetPopupContentDefaultPage({2})' href='#'>{1}</a></li>", cO.Date.ToString("MM/dd/yyyy"), cO.Title, cO.ContentID));
                        }
                    }
                    tbl.Append(string.Format(@"  </ul>
                                        </div>
                                    </div>"));



    


            }

            dynamicRightDiv.InnerHtml = tbl.ToString();
        }

        //dynamicRightDiv.InnerHtml = tbl.ToString();

    }

    private void CheckPasswordValidity()
    {
        if (Session["User"] != null)
        {
            Users user = (Users)(Session["User"]);
            SystemSettings ss = new SystemSettings(osfcon.CONNECTIONSTRING);
            ss.getRecord((int)EnumSystemSettings.GraceLogins);

            if (ss.Enabled && user.LastPasswordChange.AddDays(ss.NumVal) < DateTime.UtcNow)
            {
                Response.Redirect("/Admin/PasswordChange.aspx");
            }
        }
        else
        {
          //  Response.Redirect("/Admin/Login.aspx");
        }

    }

    #endregion
}
