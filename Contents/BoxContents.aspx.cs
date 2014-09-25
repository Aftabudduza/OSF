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

                System.Data.DataTable dtUSP = (System.Data.DataTable)Session["UserSectionPermission"];
                int catID = Convert.ToInt32(Request["SectionTypeID"]);
                string query = string.Format("SectionID={0} AND IsSection=1", catID);
                DataRow dr = dtUSP.Select(query).FirstOrDefault();
                if (dr != null)
                {
                    if ((dr["HasPermission"] != null && Convert.ToBoolean(dr["HasPermission"])))
                    {

                    }
                    else
                        Response.Redirect("../Default.aspx");
                }

            }
            if (Request["SectionTypeID"] != null || Convert.ToInt32(Request["SectionTypeID"]) > 0)
            {
                int catID = Convert.ToInt32(Request["SectionTypeID"]);
                GeneratePage(catID, DateTime.MinValue, DateTime.MinValue);
            }

        }
    }

    #endregion


    #region Events

    #endregion

    private void GetServerDPopupHTML(int a)
    {
        Response.Clear();
        Response.Write(Utility.GeneratePopupContentFromContentID(a, true, CheckAdminPermission()));
        Response.End();
    }
    private void GeneratePage(int CategoryTypeID, DateTime fromdate, DateTime todate)
    {
        List<Categories> cObjs = new List<Categories>();
        Categories objCategories = new Categories(osfcon.CONNECTIONSTRING);

        //   CategoryTypeID = (int)SectionTypeEnum.Calender;//********************************************************WILL Change


        //cObjs = contents.getRecords(CategoryTypeID, fromdate, todate);
        DataTable dt = osfcon.getRows(string.Format("SELECT C.*, CD.ItemsPerPage FROM Categories C, CategoryDetails CD WHERE C.CategoryID = CD.CategoryID AND C.CategoryTypeID={0}", CategoryTypeID));

        System.Text.StringBuilder tbl = new System.Text.StringBuilder();
        int I = -1;
        foreach (DataRow dr in dt.Rows)
        {

            int itemPerPage = 6;
            if (!(DBNull.Value == dr["ItemsPerPage"]) && Convert.ToInt32(dr["ItemsPerPage"]) > 0)
            {
            //    itemPerPage = Convert.ToInt32(dr["ItemsPerPage"]);//////////////////////////////////////////////////
                //*****NOW HARDCODED
            }

            I++;
            tbl.Append(string.Format(@"
                                          <div class='right-15p-sidebar' style='margin: 10px 10px 0;'>
                                            <div class='colomn'>
                                                <h3>
                                                    <input type='button' title='{2}' value='{1}' class='clsHeaderLink'>
                                                                   </h3>
                                                <div class='alerts'>
                                                    <table id='tablepaging{0}' width='100%' cellspacing='0' cellpadding='0' border='0'>
                                                       
                                                        <tr>
                                                            <td>
                                                            ", I, dr["Description"].ToString().Length <= 18 ? dr["Description"].ToString() : dr["Description"].ToString().Substring(0, 17) + "..", dr["Description"].ToString())

                                                         + this.GenerateBoxData(Convert.ToInt32(dr["CategoryID"])) +

                                    string.Format(@"
                                                        </td>
                                                        </tr>
                                                    </table>
                                                   
                                                </div>
                                                    <div id='pageNavPosition{0}' style='padding-top: 0' align='center'>
                                                    </div>
                                                    <script type='text/javascript'><!--
                                                            var pager{0} = new Pager('tablepaging{0}', {2});
                                                            pager{0}.init();
                                                            pager{0}.showPageNav('pager{0}', 'pageNavPosition{0}');
                                                            pager{0}.showPage(1);
                                                    </script>
                                            </div>
                                        </div>                  
                        ", I, dr["Description"].ToString().Length <= 18 ? dr["Description"].ToString() : dr["Description"].ToString().Substring(0,17) +"..", itemPerPage));
        }


        dynamicDiv.InnerHtml = tbl.ToString();
    }
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
    private string GenerateBoxData(int catID)
    {

        List<ContentObj> contents = new List<ContentObj>();
        ContentObj cObj = new ContentObj(osfcon.CONNECTIONSTRING);
        contents = cObj.getRecordsbyCategoryID(catID);


        System.Text.StringBuilder tbl = new System.Text.StringBuilder();
        int j = 0;
        foreach (ContentObj c in contents)
        {
            j++;
            string oddOrEven = "even";
            if (j % 2 != 0)
                oddOrEven = "odd";
      
            tbl.Append(string.Format(@"
                                            <tr class='{0}'>
                                                <td >
                                                    <table cellspacing='0' border='0' style='border-collapse: collapse;' id='CMList'>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <span class='standardtextbold' id='CMList_ctl00_Label2'>{2}</span><br>


                                                            <input type='button' value='{3}' onclick='GetPopupContent({4})' class='clsPopupLink' />




                                                                    <br>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>                                                         
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>               
                                         ", oddOrEven, j, c.Date.ToString("dd/MM/yyyy"), c.Title.Length >= 20 ? c.Title.Substring(0, 20) + "..." : c.Title + "...", c.ContentID));

        }
        return tbl.ToString();


    }




}