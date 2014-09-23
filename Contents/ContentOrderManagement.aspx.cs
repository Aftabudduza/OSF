using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Contents_ContentOrderManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Request.QueryString["OrderID"] != null && Request.QueryString["UD"] != null)
        {
            var contentID = Request.QueryString["OrderID"].ToString();
            var upordown = Request.QueryString["UD"].ToString();
            DataTable dt = null;
            if (contentID != null && upordown != null && Session["CategoryIDSort"] != null)
            {
                int catID = Convert.ToInt32(Session["CategoryIDSort"]);

                if (upordown == "Down")
                {
                    dt = osfcon.getRows(string.Format(@"select HotpOrder,Title,ContentID from Content where HotpOrder = {0}
                                UNION 
                                select HotpOrder,Title,ContentID from Content where HotpOrder = (select top 1 HotpOrder from Content where HotpOrder > {0} AND CategoryID={1})", Convert.ToInt32(contentID),catID));
                }
                else if (upordown == "Up")
                {
                    dt = osfcon.getRows(string.Format(@"select HotpOrder,Title,ContentID from Content where HotpOrder = {0}
                                UNION 
                                select HotpOrder,Title,ContentID from Content where HotpOrder = (select top 1 HotpOrder from Content where HotpOrder < {0} AND CategoryID={1} Order by HotpOrder DESC)
                                ", Convert.ToInt32(contentID),catID));
                }
            }
            if (dt != null && dt.Rows.Count > 1) //must be greater than 1. otherwise swapping is not possible
            {
                int firstContentID = 0, secondContentID=0, firstContentOrderID=0, secondContentOrderID=0;
                firstContentID = Convert.ToInt32(dt.Rows[0]["ContentID"]);
                firstContentOrderID = Convert.ToInt32(dt.Rows[0]["HotpOrder"]);

                secondContentID = Convert.ToInt32(dt.Rows[1]["ContentID"]);
                secondContentOrderID = Convert.ToInt32(dt.Rows[1]["HotpOrder"]);

                Utility.QueryExecute(string.Format(@"UPDATE CONTENT SET HotpOrder={0} WHERE ContentID={1}; UPDATE CONTENT SET HotpOrder={2} WHERE ContentID={3};", firstContentOrderID, secondContentID, secondContentOrderID, firstContentID));


            }
        
        
        }
        else if (!IsPostBack)
        {
            LoadddlSection();
            //FillEmployeeGrid();
        }

    }

    private void LoadddlSection()
    {
        ddlSection.Items.Clear();


        SectionType objSectionType = new SectionType(osfcon.CONNECTIONSTRING);
        try
        {

            DataTable objDataTable = osfcon.getRows("SELECT * FROM SectionType WHERE IsCategory = 1");


            ddlSection.AppendDataBoundItems = true;
            ddlSection.Items.Add(new ListItem("--Category Type--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlSection.Items.Add(new ListItem(dr["Description"].ToString(), dr["SectionTypeID"].ToString()));
            }
            //ddlSectionType.SelectedValue = "-1";


        }
        catch (Exception ex)
        { }
    }
    private void LoadComboCategoryList()
    {
        ddlCateGory.Items.Clear();


        Categories objCategories = new Categories(osfcon.CONNECTIONSTRING);
        try
        {

            DataTable objDataTable = osfcon.getRows(string.Format("SELECT * FROM Categories Where ParentID=0 AND CategoryTypeID <> {0} AND CategoryTypeID <> {1}", (int)EnumSectionType.Discusstion, (int)EnumSectionType.ChapterDirectives));

            ddlCateGory.AppendDataBoundItems = true;
            ddlCateGory.Items.Add(new ListItem("--Select Category--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlCateGory.Items.Add(new ListItem(dr["Description"].ToString(), dr["CategoryID"].ToString()));
            }          
        }
        catch (Exception ex)
        { }
    }
    public void FillGrid(int catID)
    {
        DataTable dt = osfcon.getRows(string.Format("SELECT * FROM Content WHERE CATEGORYID={0} AND IsActive=1 ORDER BY HotpOrder ", catID));

        gvdetails.DataSource = dt;
        gvdetails.DataBind();
        gvdetails.Visible = true;
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCategory();
    }
    protected void ddlCateGory_SelectedIndexChanged(object sender, EventArgs e)
    {
        int catID = Convert.ToInt32(ddlCateGory.SelectedValue.ToString());
        Session["CategoryIDSort"] = catID;
        FillGrid(catID);
    }

    private void LoadCategory()
    {
        ddlCateGory.Items.Clear();


        Categories objCategories = new Categories(osfcon.CONNECTIONSTRING);
        try
        {
            int sectionTypeID = Convert.ToInt32(ddlSection.SelectedValue.ToString());
            DataTable objDataTable = osfcon.getRows(string.Format(@"SELECT * FROM Categories WHERE CategoryTypeID={0}",sectionTypeID)); ;


            ddlCateGory.AppendDataBoundItems = true;
            ddlCateGory.Items.Add(new ListItem("--Select Category--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlCateGory.Items.Add(new ListItem(dr["Description"].ToString(), dr["CategoryID"].ToString()));
            }
            // ddlCategoryListL1.SelectedValue = "-1";

            ddlCateGory.SelectedValue = "-1";
        }
        catch (Exception ex)
        { }

    }
    protected void gvdetails_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
}