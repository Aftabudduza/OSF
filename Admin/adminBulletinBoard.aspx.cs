using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class Admin_adminBulletinBoard : System.Web.UI.Page
{
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
        }
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

                ContentObj objContentObj = new ContentObj(osfcon.CONNECTIONSTRING);
                objContentObj.Author = txtAuthor.Text;
                objContentObj.Date = Convert.ToDateTime(txtDate.Text);
                objContentObj.Title = txtTitle.Text;
                objContentObj.Content = txtContent.Text;
                //  objContentObj.CategoryID = 1;
                objContentObj.URL = nFile;

                if (ddlCategoryList.SelectedValue != "-1")
                {
                    objContentObj.CategoryID = Convert.ToInt32(ddlCategoryList.SelectedValue.ToString());
                }

                if (objContentObj.insert())
                {

                    lblError.Text = "Content Saved Successfully";
                    txtAuthor.Text = "";
                    txtDate.Text = DateTime.Today.ToString("mm/dd/yyyy");
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
        Response.Redirect("AdminIndex.aspx");
    }

    private void LoadComboCategoryList()
    {
        ddlCategoryList.Items.Clear();


        Categories objCategories = new Categories(osfcon.CONNECTIONSTRING);
        try
        {
            DataTable objDataTable = objCategories.getRows("*", "ParentID = '" + Convert.ToInt32(Session["CategoryID"])+ "' ");


            ddlCategoryList.AppendDataBoundItems = true;
            ddlCategoryList.Items.Add(new ListItem("--Select Category--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlCategoryList.Items.Add(new ListItem(dr["Description"].ToString(), dr["CategoryID"].ToString()));
            }
            // ddlCategoryList.SelectedValue = "-1";

            ddlCategoryList.SelectedValue = Request["CategoryID"].ToString();
        }
        catch (Exception ex)
        { }
    }
}