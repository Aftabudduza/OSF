using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class Admin_UnderDev : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Method"] == "GetServerDate")
        {
            int a = Convert.ToInt32(Request.QueryString["ID"]);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            GetServerDate();
        }
    }

    private void GetServerDate()
    {
        Response.Clear();
        Response.Write(DateTime.Now.ToString());
        Response.End();
    }

    public string GeneratePage(int CategoryTypeID, DateTime fromdate, DateTime todate)
    {
        List<Categories> cObjs = new List<Categories>();
        Categories objCategories = new Categories(cmscon.CONNECTIONSTRING);

        //   CategoryTypeID = (int)SectionTypeEnum.Calender;//********************************************************WILL Change


        //cObjs = contents.getRecords(CategoryTypeID, fromdate, todate);
        DataTable dt = cmscon.getRows(string.Format("SELECT C.*, CD.ItemsPerPage FROM Categories C, CategoryDetails CD WHERE C.CategoryID = CD.CategoryID AND C.CategoryTypeID={0}", CategoryTypeID));

        System.Text.StringBuilder tbl = new System.Text.StringBuilder();
        int I = -1;
        foreach (DataRow dr in dt.Rows)
        {

            int itemPerPage = 5;
            if (!(DBNull.Value == dr["ItemsPerPage"]) && Convert.ToInt32(dr["ItemsPerPage"]) > 0)
            {
                itemPerPage = Convert.ToInt32(dr["ItemsPerPage"]);
            }

            I++;
            tbl.Append(string.Format(@"
                                          <div class='right-15p-sidebar' style='margin: 10px 10px 0;'>
                                            <div class='colomn'>
                                                <h3>
                                                    {1}</h3>
                                                <div class='alerts'>
                                                    <table id='tablepaging{0}' width='100%' cellspacing='0' cellpadding='0' border='0'>
                                                       
                                                        <tr>
                                                            <td>
                                                            ", I, dr["Description"].ToString())

                                                         + this.GenerateBoxData(Convert.ToInt32(dr["CategoryTypeID"])) +

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
                        ", I, dr["Description"].ToString(), itemPerPage));
        }


      return tbl.ToString();
    }

    private string GenerateBoxData(int catTypeID)
    {

        List<ContentObj> contents = new List<ContentObj>();
        ContentObj cObj = new ContentObj(cmscon.CONNECTIONSTRING);
        contents = cObj.getRecordsbyCategoryTypeID(catTypeID);


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
                                                                    <a href='#'>
                                                                       {3} </a>
                                                                    <br>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>                                                         
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>               
                                         ", oddOrEven, j, c.Date.ToString("dd/MM/yyyy"), c.Title));

        }
        return tbl.ToString();


    }
    protected void Button1_Click(object sender, EventArgs e)
    {          
    string sds = string.Format(@"AnotherFunction('{0}');","Hellooooooooooooooooooooooooooooooooooooooo");  
    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", sds, true);
    }

    [WebMethod]
    public static string Name()
    {
        string Name = "Hello Rohatash Kumar";
        return Name;
    }

    public void LinkButton_Command(Object sender, CommandEventArgs e)
    {
        String CustomerID = e.CommandArgument.ToString();
        string sds = string.Format(@"AnotherFunction('{0}');", "Helloooooooooooooooooooooooooooooooooooooo");
        //element_to_pop_up.InnerHtml = tbl.Append(string.Format(@"<h3>Hellooooooooooooooooooooooooooooooooooooooo</h3>")).ToString();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", sds, true);
    }
    protected void EditCustomer(object sender, EventArgs e)
    {

    }
}