using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Category : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadComboCategoryList();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        Categories objCategories = new Categories(cmscon.CONNECTIONSTRING);
        this.RefreshObject(objCategories);

        if (objCategories.insert())
        {
            
            lblMessage.Text = "Content Saved Successfully";
        }
        else
        {
            lblMessage.Text = "Error";
        }

    }

    private Categories RefreshObject(Categories cat)
    {
        cat.SectionTypeID = Convert.ToInt32(ddlSectionType.SelectedValue.ToString()); ;
        cat.Description = txtCategoryName.Text;
        cat.IsLeaf = chkIsLeaf.Checked;
        cat.SortOrder = Convert.ToInt32(txtOrderSeq.Text);
        cat.CreatedBy = Convert.ToInt32(Session["UserID"]);
        cat.CreatedDate = DateTime.Today;
        cat.ModifiedDate = DateTime.Today;
        return cat;
    }

    private void LoadComboCategoryList()
    {
        ddlSectionType.Items.Clear();


        SectionType objSectionType = new SectionType(cmscon.CONNECTIONSTRING);
        try
        {

            DataTable objDataTable = cmscon.getRows("SELECT * FROM SectionType");


            ddlSectionType.AppendDataBoundItems = true;
            ddlSectionType.Items.Add(new ListItem("--Category Type--", "-1"));
            foreach (DataRow dr in objDataTable.Rows)
            {
                this.ddlSectionType.Items.Add(new ListItem(dr["Description"].ToString(), dr["SectionTypeID"].ToString()));
            }
            //ddlSectionType.SelectedValue = "-1";


        }
        catch (Exception ex)
        { }
    }

}