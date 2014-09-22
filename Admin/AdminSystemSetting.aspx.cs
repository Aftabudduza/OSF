using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Admin_AdminSystemSetting : System.Web.UI.Page
{
    #region Global Variable & PageLoad
    bool _isEdit = false;
    string mode = "";
    DataTable dt = null;
    DataSet ds = null;
    string SQl = "";
    string _message = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["User"] == null || Session["UserPermission"] == null || Session["UserID"] == null || Session["UserSectionPermission"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            
            BindCurrentUsers();
           // Fill_Griedview_Country();

        }

    }
    #endregion

    #region Events
    
    protected void gvSystem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindCurrentUsers();
        gvSystem.PageIndex = e.NewPageIndex;
        gvSystem.DataBind();
    }
     void BindCurrentUsers()
    {
        try
        {
            DataTable objDataTable = new DataTable();
            string sql = "";
            sql = "SELECT * FROM SystemSettings";
            //sql = string.Format("SELECT distinct cat.CategoryID, MAX(cat.[Description]) Description ,Discussions = ISNULL((SELECT COUNT(distinct(c2.ContentID)) Total  FROM Categories cat1, [Content] c2  WHERE cat1.CategoryTypeID={0} AND cat1.CategoryID=c2.CategoryID AND (c2.RootThreadID = 0 OR c2.RootThreadID IS NULL) AND cat1.CategoryID = cat.CategoryID    GROUP BY cat1.CategoryID ),0) ,Post = ISNULL((SELECT COUNT(distinct(c4.ContentID)) Total  FROM Categories cat2, [Content] c4  WHERE cat2.CategoryTypeID={0} AND cat2.CategoryID=c4.CategoryID AND c4.RootThreadID > 0 AND cat2.CategoryID = cat.CategoryID    GROUP BY cat2.CategoryID ),0) FROM Categories cat, [Content] c  WHERE cat.CategoryTypeID={0} AND cat.CategoryID=c.CategoryID  GROUP BY cat.CategoryID ORDER BY MAX(cat.[Description]) ASC ");

            objDataTable = osfcon.getRows(sql);
            if (objDataTable != null)
            {
                gvSystem.DataSource = objDataTable;
                gvSystem.DataBind();
            }
        }
        catch
        {

        }

    }   

    private void DisplayAlert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg.Replace("'", "\\'").Replace(Environment.NewLine, "\\n")), true);
    }

    protected void gvSystem_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSystem.EditIndex = e.NewEditIndex;
        BindCurrentUsers();
    }

    protected void gvSystem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        HiddenField hdstate = (HiddenField)gvSystem.Rows[e.RowIndex].FindControl("hdstate");
        string txtName = ((TextBox)gvSystem.Rows[e.RowIndex].FindControl("txtName")).Text;
        string txtDescription = ((TextBox)gvSystem.Rows[e.RowIndex].FindControl("txtDescription")).Text;
        CheckBox chkEnable = ((CheckBox)gvSystem.Rows[e.RowIndex].FindControl("chkEnable"));
        string txtNumeric = ((TextBox)gvSystem.Rows[e.RowIndex].FindControl("txtNumeric")).Text;
        string txtString = ((TextBox)gvSystem.Rows[e.RowIndex].FindControl("txtString")).Text;

        try
        {


            SystemSettings obj = new SystemSettings(osfcon.CONNECTIONSTRING);
            int Id =Convert.ToInt32(hdstate.Value);
            obj.SystemSettingID = Convert.ToInt32(hdstate.Value);
            obj.Name = txtName.ToString();
            obj.Description = txtDescription.ToString();
            if (chkEnable.Checked == true)
            {
                obj.Enabled = true;
            }
            else
            {
                obj.Enabled = false;
            }

            obj.NumVal = Convert.ToInt32(txtNumeric.ToString());
            obj.StrVal = txtString.ToString();
            obj.update();
            gvSystem.EditIndex = -1;
            BindCurrentUsers();
        }
        catch (Exception ex)
        {

        }


    }
    protected void gvSystem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSystem.EditIndex = -1;
        BindCurrentUsers();
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        //GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        //HiddenField vId = (HiddenField)gvSystem.Rows[row.RowIndex].FindControl("hdstate");

        //try
        //{
        //    App_ReferenData_cl objdelete = new App_ReferenData_cl(cmscon.CONNECTIONSTRING);
        //    objdelete.delete(vId.Value);
        //    BindCurrentUsers();
        //}
        //catch (Exception ex)
        //{

        //}


    }
    #endregion

    #region Method


   

    //private void FillControlsView(DataTable dt)
    //{
    //    try
    //    {
    //        if (dt.Rows[0]["FirstName"] != null)
    //            lblFirstName.Text = dt.Rows[0]["FirstName"].ToString();

    //        if (dt.Rows[0]["LastName"] != null)
    //            lblLastName.Text = dt.Rows[0]["LastName"].ToString();
    //        //lblD.Text = dt.Rows[0]["DEPT***"].ToString();
    //        //lblHomePhone.Text = dt.Rows[0]["POS****"].ToString();
    //        if (dt.Rows[0]["Chapter"] != null)
    //            lblAreaChapter.Text = dt.Rows[0]["Chapter"].ToString();

    //        if (dt.Rows[0]["CompanionType"] != null)
    //            lblCompanionType.Text = dt.Rows[0]["CompanionType"].ToString();

    //        if (dt.Rows[0]["ProfessionalYear"] != null)
    //            lblProfessionYear.Text = dt.Rows[0]["ProfessionalYear"].ToString();

    //        if (dt.Rows[0]["CompanionType"] != null)
    //            lblMI.Text = dt.Rows[0]["CompanionType"].ToString();

    //        if (dt.Rows[0]["HomeStreet1"] != null)
    //            lblHomeStreet1.Text = dt.Rows[0]["HomeStreet1"].ToString();
    //        if (dt.Rows[0]["HomeStreet2"] != null)
    //            lblHomeStreet1.Text = dt.Rows[0]["HomeStreet2"].ToString();
    //        if (dt.Rows[0]["HomeCity"] != null)
    //            lblHomeCity.Text = dt.Rows[0]["HomeCity"].ToString();
    //        if (dt.Rows[0]["HomeState"] != null)
    //            lblHomeState.Text = dt.Rows[0]["HomeState"].ToString();

    //        if (dt.Rows[0]["HomeZip"] != null)
    //            lblHomeZip.Text = dt.Rows[0]["HomeZip"].ToString();

    //        if (dt.Rows[0]["HomeEmail"] != null)
    //            lblHomeEmail.Text = dt.Rows[0]["HomeEmail"].ToString();

    //        if (dt.Rows[0]["HomePhone"] != null)
    //            lblHomePhone.Text = dt.Rows[0]["HomePhone"].ToString();

    //        if (dt.Rows[0]["MinistryClassification"] != null)
    //            lblMinistry1Classification.Text = dt.Rows[0]["MinistryClassification"].ToString();

    //        if (dt.Rows[0]["MinistryLocation"] != null)
    //            lblMinistry1PlaceofEmployment.Text = dt.Rows[0]["MinistryLocation"].ToString();

    //        if (dt.Rows[0]["MinistryStreet1"] != null)
    //            lblMinistry1Street1.Text = dt.Rows[0]["MinistryStreet1"].ToString();

    //        if (dt.Rows[0]["MinistryStreet2"] != null)
    //            lblMinistry1Street1.Text = dt.Rows[0]["MinistryStreet2"].ToString();

    //        if (dt.Rows[0]["MinistryCity"] != null)
    //            lblMinistry1City.Text = dt.Rows[0]["MinistryCity"].ToString();

    //        if (dt.Rows[0]["MinistryCountry"] != null)
    //            lblMinistry1Country.Text = dt.Rows[0]["MinistryCountry"].ToString();

    //        if (dt.Rows[0]["MinistryZip"] != null)
    //            lblMinistry1Zip.Text = dt.Rows[0]["MinistryZip"].ToString();

    //        if (dt.Rows[0]["MinistryPhone"] != null)
    //            lblMinistry1Phone.Text = dt.Rows[0]["MinistryPhone"].ToString();

    //        if (dt.Rows[0]["MinistryFax"] != null)
    //            lblMinistry1Fax.Text = dt.Rows[0]["MinistryFax"].ToString();

    //        if (dt.Rows[0]["MinistryEmail"] != null)
    //            lblMinistry1Email.Text = dt.Rows[0]["MinistryEmail"].ToString();

    //        if (dt.Rows[0]["Ministry2Title"] != null)
    //            lblMinistry2Title.Text = dt.Rows[0]["Ministry2Title"].ToString();

    //        if (dt.Rows[0]["Ministry2Classification"] != null)
    //            lblMinistry2Classification2.Text = dt.Rows[0]["Ministry2Classification"].ToString();

    //        if (dt.Rows[0]["Ministry2Location"] != null)
    //            lblMinistry2PlaceofEmp.Text = dt.Rows[0]["Ministry2Location"].ToString();

    //        if (dt.Rows[0]["Ministry2Street1"] != null)
    //            lblMinistry2Street1.Text = dt.Rows[0]["Ministry2Street1"].ToString();

    //        if (dt.Rows[0]["Ministry2Street2"] != null)
    //            lblMinistry2street2.Text = dt.Rows[0]["Ministry2Street2"].ToString();

    //        if (dt.Rows[0]["Ministry2City"] != null)
    //            lblMinistry2City.Text = dt.Rows[0]["Ministry2City"].ToString();

    //        if (dt.Rows[0]["Ministry2Country"] != null)
    //            lblMinistry2Country.Text = dt.Rows[0]["Ministry2Country"].ToString();

    //        if (dt.Rows[0]["Ministry2Zip"] != null)
    //            lblMinistry2Zip.Text = dt.Rows[0]["Ministry2Zip"].ToString();

    //        if (dt.Rows[0]["Ministry2Phone"] != null)
    //            lblMinistry2Phone.Text = dt.Rows[0]["Ministry2Phone"].ToString();

    //        if (dt.Rows[0]["CompanionType"] != null)
    //            lblMinistry2Fax.Text = dt.Rows[0]["Ministry2Fax"].ToString();

    //        if (dt.Rows[0]["Ministry2Email"] != null)
    //            lblMinistry2Email.Text = dt.Rows[0]["Ministry2Email"].ToString();

    //        if (dt.Rows[0]["Job"] != null)
    //            lblPosition.Text = dt.Rows[0]["Job"].ToString();

    //        if (dt.Rows[0]["Department"] != null)
    //            lblDepartment.Text = dt.Rows[0]["Department"].ToString();

    //        if (dt.Rows[0]["Location"] != null)
    //            lblHomeLocation.Text = dt.Rows[0]["Location"].ToString();

    //        //ddlLocation.SelectedValue = dt.Rows[0]["LocationID"].ToString();

    //        if (dt.Rows[0]["Picture"] != null && dt.Rows[0]["Picture"].ToString() != string.Empty)
    //        {
    //            string filePath = Path.Combine(Request.PhysicalApplicationPath, "Images\\Users\\");

    //            imgUserView.Attributes["src"] = "../Images/Users/" + dt.Rows[0]["Picture"].ToString();
    //        }
    //    }
    //    catch
    //    {

    //    }


    //}


    #endregion
}