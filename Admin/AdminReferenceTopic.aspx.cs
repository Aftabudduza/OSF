using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class Admin_AdminReferenceTopic : System.Web.UI.Page
{
    int _categoryIDL1, _categoryIDL2, _categoryIDL3;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["User"] == null || Session["UserPermission"] == null || Session["UserID"] == null || Session["UserSectionPermission"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            Users objUser = (Users)Session["User"];
            UserPermissions dtUP = (UserPermissions)Session["UserPermission"];
            System.Data.DataTable dtUSP = (System.Data.DataTable)Session["UserSectionPermission"];
            if (!dtUP.IsGlobalContentAdmin)
            {
                Response.Redirect("../Default.aspx");
            }


        }

        if (!IsPostBack)
        {
            LoadComboCategoryList();
            txtLvl2.Visible = false;
            txtLvl3.Visible = false;
            btnLvl2Save.Visible = false;
            btnlvl3Save.Visible = false;
        }
    }

    private void LoadComboCategoryList()
    {
        ddlCategoryListL1.Items.Clear();
        int catID = Convert.ToInt32(Request["CategoryID"]);


        Categories objCategories = new Categories(cmscon.CONNECTIONSTRING);
        try
        {
            DataTable objDataTable = objCategories.getRows("*", "ParentID = '" + catID + "' ");


            ddlCategoryListL1.AppendDataBoundItems = true;
            ddlCategoryListL1.Items.Add(new ListItem("--Select Category--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlCategoryListL1.Items.Add(new ListItem(dr["Description"].ToString(), dr["CategoryID"].ToString()));
            }
            //ddlCategoryListL1.SelectedValue = "-1";


        }
        catch (Exception ex)
        { }
    }
    protected void ddlCategoryListL1_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLvl2();
    }

    private void LoadLvl2()
    {
        ddlCategoryListL2.Items.Clear();


        Categories objCategories = new Categories(cmscon.CONNECTIONSTRING);
        try
        {
            _categoryIDL1 = Convert.ToInt32(ddlCategoryListL1.SelectedValue.ToString());
            DataTable objDataTable = objCategories.getRows("*", "ParentID = '" + _categoryIDL1 + "' ");


            ddlCategoryListL2.AppendDataBoundItems = true;
            ddlCategoryListL2.Items.Add(new ListItem("--Select Category--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlCategoryListL2.Items.Add(new ListItem(dr["Description"].ToString(), dr["CategoryID"].ToString()));
            }
            // ddlCategoryListL1.SelectedValue = "-1";

            ddlCategoryListL2.SelectedValue = "-1";
        }
        catch (Exception ex)
        { }

    }
    private void LoadLvl3()
    {
        ddlCategoryListL3.Items.Clear();


        Categories objCategories = new Categories(cmscon.CONNECTIONSTRING);
        try
        {
            _categoryIDL2 = Convert.ToInt32(ddlCategoryListL2.SelectedValue.ToString());
            DataTable objDataTable = objCategories.getRows("*", "ParentID = '" + _categoryIDL2 + "' ");


            ddlCategoryListL3.AppendDataBoundItems = true;
            ddlCategoryListL3.Items.Add(new ListItem("--Select Category--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlCategoryListL3.Items.Add(new ListItem(dr["Description"].ToString(), dr["CategoryID"].ToString()));
            }
            // ddlCategoryListL1.SelectedValue = "-1";

            ddlCategoryListL3.SelectedValue = "-1";
        }
        catch (Exception ex)
        { }

    }

    protected void ddlCategoryListL2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCategoryListL3.Items.Clear();


        Categories objCategories = new Categories(cmscon.CONNECTIONSTRING);
        try
        {
            _categoryIDL2 = Convert.ToInt32(ddlCategoryListL2.SelectedValue.ToString());
            DataTable objDataTable = objCategories.getRows("*", "ParentID = '" + _categoryIDL2 + "' ");


            ddlCategoryListL3.AppendDataBoundItems = true;
            ddlCategoryListL3.Items.Add(new ListItem("--Select Category--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlCategoryListL3.Items.Add(new ListItem(dr["Description"].ToString(), dr["CategoryID"].ToString()));
            }
            // ddlCategoryListL1.SelectedValue = "-1";

            ddlCategoryListL3.SelectedValue = "-1";
        }
        catch (Exception ex)
        { }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(this.uplProduct.FileName))
            {
                //read the file in
                string filePath = Path.Combine(Request.PhysicalApplicationPath, "");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string nFile = Path.Combine(filePath, "INLOAD_Product_" + DateTime.Now.ToString("yyyyMMddhhmmss"));

                try
                {
                    if (System.IO.File.Exists(nFile))
                    {
                        System.IO.File.Delete(nFile);
                    }

                }
                catch (Exception ex)
                {
                }
                uplProduct.SaveAs(nFile);

                ContentObj objContentObj = new ContentObj(cmscon.CONNECTIONSTRING);
                objContentObj.Author = txtAuthor.Text;
                objContentObj.Date = Convert.ToDateTime(txtDate.Text);
                objContentObj.Title = txtTitle.Text;
                objContentObj.Content = txtContent.Text;
                 objContentObj.LastUpdated = DateTime.Today;
                objContentObj.URL = nFile;

                if (ddlCategoryListL1.SelectedValue != "-1")
                {
                    objContentObj.CategoryID = Convert.ToInt32(Request["CategoryID"]);//Convert.ToInt32(ddlCategoryListL1.SelectedValue.ToString());
                }

                if (objContentObj.insert())
                {

                    lblError.Text = "Content Saved Successfully";
                    txtAuthor.Text = "";
                    txtDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                    txtTitle.Text = "";
                    txtContent.Text = "";
                }
                else
                {
                    lblError.Text = "Error";
                }
            }
            else
            {
                lblError.Text = "You Must Select file To Import.";
            }
        }
        catch (Exception ex)
        {
            //  lblMsg.Text = "Error Inloading:" + ex.Message;
            //  appGlobal.LogDataError("Error Inloading:" + ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnLvl2Add_Click(object sender, EventArgs e)
    {
        txtLvl2.Visible = true;
        btnLvl2Add.Visible = false;
        btnLvl2Save.Visible = true;
    }
    protected void btnLvl3Add_Click(object sender, EventArgs e)
    {
        txtLvl3.Visible = true;
        btnLvl3Add.Visible = false;
        btnlvl3Save.Visible = true;
    }
    protected void btnLvl2Save_Click(object sender, EventArgs e)
    {


        Categories objContentObj = new Categories(cmscon.CONNECTIONSTRING);
        objContentObj.Description = txtLvl2.Text;
        objContentObj.Modifiedby = Convert.ToInt32(Session["UserID"]);
        objContentObj.CreatedDate = DateTime.Today;
        objContentObj.ParentID = Convert.ToInt32(ddlCategoryListL1.SelectedValue.ToString());

        objContentObj.CategoryID = Convert.ToInt32(Request["CategoryID"]);//Convert.ToInt32(ddlCategoryListL1.SelectedValue.ToString());

        if (objContentObj.insert())
        {

            lblError.Text = "Content Saved Successfully";
            txtAuthor.Text = "";
            txtDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
            txtTitle.Text = "";
            txtContent.Text = "";
            txtLvl2.Visible = false;
            btnLvl2Add.Visible = true;


            LoadLvl2();
        }
    }
    protected void btnlvl3Save_Click(object sender, EventArgs e)
    {
        Categories objContentObj = new Categories(cmscon.CONNECTIONSTRING);
        objContentObj.Description = txtLvl3.Text;
        objContentObj.Modifiedby = Convert.ToInt32(Session["UserID"]);
        objContentObj.CreatedDate = DateTime.Today;
        objContentObj.ParentID = Convert.ToInt32(ddlCategoryListL2.SelectedValue.ToString());

        objContentObj.CategoryID = Convert.ToInt32(Request["CategoryID"]); //Convert.ToInt32(ddlCategoryListL1.SelectedValue.ToString());

        if (objContentObj.insert())
        {

            lblError.Text = "Content Saved Successfully";
            txtAuthor.Text = "";
            txtDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
            txtTitle.Text = "";
            txtContent.Text = "";
            txtLvl3.Visible = false;
            btnLvl3Add.Visible = true;


            LoadLvl3();
        }
    }
}