using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Admin_AdminReference : System.Web.UI.Page
{


    #region Global Variable & PageLoad
    string _message = "";
    int _categoryIDL1, _categoryIDL2, _categoryIDL3, _categoryIDL4;

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
            txtLvl4.Visible = false;
            btnLvl2Save.Visible = false;
            btnlvl3Save.Visible = false;
            btnLvl4Save.Visible = false;

        }
    }
    #endregion


    #region Events
    protected void ddlCategoryListL1_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLvl2();
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
            if (this.ValidateObject().Length > 0)
            {
                DisplayAlert(_message);

            }

            else
            {
                if (!string.IsNullOrEmpty(this.uplProduct.FileName))
                {
                    string filePath = Path.Combine(Request.PhysicalApplicationPath, "Images\\Content\\");

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + this.uplProduct.FileName;
                    string nFile = Path.Combine(filePath, fileName);
                    Session["FileName"] = fileName;
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

                    this.MakeNewObject(objContentObj);



                    if (objContentObj.insert())
                    {
                        DisplayAlert("Content Saved Successfully.");
                        txtAuthor.Text = "";
                        txtDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                        txtTitle.Text = "";
                        txtContent.Text = "";
                        ddlCategoryListL1.SelectedIndex = 0;
                        txtDate.Text = string.Empty;
                        ddlCategoryListL2.Items.Clear();
                        ddlCategoryListL3.Items.Clear();
                    }
                    else
                    {
                        lblError.Text = "Error";
                    }
                }
                else
                {
                    DisplayAlert("You Must Select file To Import.");
                }
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
        objContentObj.SectionTypeID = (int)SectionTypeEnum.ChapterDirectives;
        objContentObj.CategoryID = Convert.ToInt32(Request["CategoryID"]);//Convert.ToInt32(ddlCategoryListL1.SelectedValue.ToString());

        if (objContentObj.insert())
        {

            lblError.Text = "Content Saved Successfully";
            txtAuthor.Text = "";
 
            txtTitle.Text = "";
            txtContent.Text = "";
            txtLvl2.Visible = false;
            btnLvl2Add.Visible = true;
            btnLvl2Save.Visible = false;

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
        objContentObj.SectionTypeID = (int)SectionTypeEnum.ChapterDirectives;
        objContentObj.CategoryID = Convert.ToInt32(Request["CategoryID"]); //Convert.ToInt32(ddlCategoryListL1.SelectedValue.ToString());

        if (objContentObj.insert())
        {

            lblError.Text = "Content Saved Successfully";
            txtAuthor.Text = "";
   
            txtTitle.Text = "";
            txtContent.Text = "";
            txtLvl3.Visible = false;
            btnLvl3Add.Visible = true;
            btnlvl3Save.Visible = false;

            LoadLvl3();
        }
    }
    #endregion

    #region Method

    private void LoadComboCategoryList()
    {
        ddlCategoryListL1.Items.Clear();
        int catID = Convert.ToInt32(Request["CategoryID"]);

        Categories objCategories = new Categories(cmscon.CONNECTIONSTRING);
        try
        {

           DataTable objDataTable = cmscon.getRows(string.Format("SELECT * FROM Categories Where ParentID=0 AND  CategoryTypeID = {0}", (int)SectionTypeEnum.Reference));


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

    private void LoadLvl4()
    {
        ddlCategoryListL4.Items.Clear();


        Categories objCategories = new Categories(cmscon.CONNECTIONSTRING);
        try
        {
            _categoryIDL3 = Convert.ToInt32(ddlCategoryListL3.SelectedValue.ToString());
            DataTable objDataTable = objCategories.getRows("*", "ParentID = '" + _categoryIDL3 + "' ");


            ddlCategoryListL4.AppendDataBoundItems = true;
            ddlCategoryListL4.Items.Add(new ListItem("--Select Category--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlCategoryListL4.Items.Add(new ListItem(dr["Description"].ToString(), dr["CategoryID"].ToString()));
            }
            // ddlCategoryListL1.SelectedValue = "-1";

            ddlCategoryListL4.SelectedValue = "-1";
        }
        catch (Exception ex)
        { }

    }
 
    private string ValidateObject()
    {
        _message = "";

        if ((ddlCategoryListL1.SelectedValue.ToString() == "-1"))
        {
            _message += "Please select a Category Type1" + Environment.NewLine;
        }

        if ((txtAuthor.Text == ""))
        {
            _message += "Please enter author Name" + Environment.NewLine;
        }

        if ((txtTitle.Text == ""))
        {
            _message += "Please enter title" + Environment.NewLine;
        }

        if ((txtContent.Text == ""))
        {
            _message += "Please enter content" + Environment.NewLine;
        }

        return _message;
    }

    private void DisplayAlert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg.Replace("'", "\\'").Replace(Environment.NewLine, "\\n")), true);
    }

    private void MakeNewObject(ContentObj objContentObj)
    {
        objContentObj.CreatedBy = Convert.ToInt32(Session["UserID"]);
        objContentObj.CreatedOn = DateTime.UtcNow;
        objContentObj.Author = txtAuthor.Text;
        objContentObj.Date = Convert.ToDateTime(txtDate.Text);
        objContentObj.Title = txtTitle.Text;
        objContentObj.Content = txtContent.Text;
        //  objContentObj.CategoryID = 1;
        objContentObj.URL = (Session["FileName"] == null || Session["FileName"].ToString() == "") ? "" : Session["FileName"].ToString();


        if (ddlCategoryListL1.SelectedValue != "-1")
        {
            objContentObj.CategoryID = Convert.ToInt32(ddlCategoryListL1.SelectedValue);//Convert.ToInt32(ddlCategoryListL1.SelectedValue.ToString());
        }

        if (ddlCategoryListL2.SelectedValue != "" && ddlCategoryListL2.SelectedValue != "-1")
        {
            objContentObj.CategoryID = Convert.ToInt32(ddlCategoryListL2.SelectedValue);//Convert.ToInt32(ddlCategoryListL1.SelectedValue.ToString());
        }

        if (ddlCategoryListL3.SelectedValue != "" && ddlCategoryListL3.SelectedValue != "-1")
        {
            objContentObj.CategoryID = Convert.ToInt32(ddlCategoryListL3.SelectedValue);//Convert.ToInt32(ddlCategoryListL1.SelectedValue.ToString());
        }

        if (ddlCategoryListL4.SelectedValue != "" && ddlCategoryListL4.SelectedValue != "-1")
        {
            objContentObj.CategoryID = Convert.ToInt32(ddlCategoryListL4.SelectedValue);//Convert.ToInt32(ddlCategoryListL1.SelectedValue.ToString());
        }

    }
    #endregion



    protected void btnLvl4Add_Click(object sender, EventArgs e)
    {
        txtLvl4.Visible = true;
        btnLvl4Add.Visible = false;
        btnLvl4Save.Visible = true;
    }
    protected void btnLvl4Save_Click(object sender, EventArgs e)
    {
        Categories objContentObj = new Categories(cmscon.CONNECTIONSTRING);
        objContentObj.Description = txtLvl4.Text;
        objContentObj.Modifiedby = Convert.ToInt32(Session["UserID"]);
        objContentObj.CreatedDate = DateTime.Today;
        objContentObj.ParentID = Convert.ToInt32(ddlCategoryListL3.SelectedValue.ToString());
        objContentObj.SectionTypeID = (int)SectionTypeEnum.ChapterDirectives;
        objContentObj.CategoryID = Convert.ToInt32(Request["CategoryID"]); //Convert.ToInt32(ddlCategoryListL1.SelectedValue.ToString());

        if (objContentObj.insert())
        {

            lblError.Text = "Content Saved Successfully";
            txtAuthor.Text = "";

            txtTitle.Text = "";
            txtContent.Text = "";
            txtLvl4.Visible = false;
            btnLvl4Add.Visible = true;
            btnLvl4Save.Visible = false;

            LoadLvl4();
        }
    }
    protected void ddlCategoryListL3_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCategoryListL4.Items.Clear();


        Categories objCategories = new Categories(cmscon.CONNECTIONSTRING);
        try
        {
            _categoryIDL3 = Convert.ToInt32(ddlCategoryListL3.SelectedValue.ToString());
            DataTable objDataTable = objCategories.getRows("*", "ParentID = '" + _categoryIDL3 + "' ");


            ddlCategoryListL4.AppendDataBoundItems = true;
            ddlCategoryListL4.Items.Add(new ListItem("--Select Category--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlCategoryListL4.Items.Add(new ListItem(dr["Description"].ToString(), dr["CategoryID"].ToString()));
            }
            // ddlCategoryListL1.SelectedValue = "-1";

            ddlCategoryListL4.SelectedValue = "-1";
        }
        catch (Exception ex)
        { }
    }
}