using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Admin_UserReports : System.Web.UI.Page
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
            Session["Mode"] = null;
            try
            {
                mode = Request.QueryString["Mode"].ToString();
            }
            catch
            {
                mode = "";
            }
            if (!string.IsNullOrEmpty(mode.ToString()) && mode.ToString() != "")
            {
                Session["Mode"] = mode.ToString();
            }
            else
            {
                Response.Redirect("AdminIndex.aspx");
            }

            //this.FillGrid(0);

        }

    }
    #endregion

    #region Events
    protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReport.PageIndex = e.NewPageIndex;
        gvReport.DataBind();
    }
    private string ValidateObject()
    {
        _message = "";
        DateTime Temp;
      


        if (txtStartDate.Text.Length == 0 | txtStartDate.Text.ToString() == "Date" && txtEndDate.Text.Length == 0 | txtEndDate.Text.ToString() == "Date")
        {

        }
        else
        {
            if (DateTime.TryParse(txtStartDate.Text.ToString(), out Temp) == true && DateTime.TryParse(txtEndDate.Text.ToString(), out Temp) == true)
            {
               
            }
            else
            {
                _message += "Please Give the Correct Date Time Format" + Environment.NewLine;
            }
        }


        return _message;
    }
    protected void btnView_Click(object sender, EventArgs e)
    {

        try
        {
            string str = "";
            dt = null;
            DateTime Temp;
            if (this.ValidateObject().Length > 0)
            {
                DisplayAlert(_message);
            }
            else
            {
                if (Session["Mode"] != null)
                {
                    string ModeType = Session["Mode"].ToString();
                    str += "SELECT u.* FROM Users u, UserPermissions up WHERE u.UserID=up.UserID ";
                    if (ModeType == "Active")
                    {
                        str += " AND up.IsHidden=0 ";
                    }
                    else if (ModeType == "InActive")
                    {
                        str += " AND up.IsHidden=1 ";
                    }
                    else
                    {
                        str += "AND  up.IsDelete=1 ";
                    }

                  
                    if (txtStartDate.Text.Length == 0 | txtStartDate.Text.ToString() == "Date" && txtEndDate.Text.Length == 0 | txtEndDate.Text.ToString() == "Date")
                    {

                    }
                    else
                    {
                        if (DateTime.TryParse(txtStartDate.Text.ToString(), out Temp) == true && DateTime.TryParse(txtEndDate.Text.ToString(), out Temp) == true)
                        {
                            str += " AND u.CreatedDate BETWEEN '" + txtStartDate.Text.ToString() + "' AND '" + txtEndDate.Text.ToString() + " ' ";
                        }
                        else
                        {
                            DisplayAlert("Please Give the Correct Date Time Format");
                        }
                    }
                   
                    if (ddlCategory.SelectedValue != "-1")
                    {
                        if (ddlCategory.SelectedValue == "1")
                        {
                            str += " AND up.IsSister=1 ";
                        }
                        if (ddlCategory.SelectedValue == "2")
                        {
                            str += " AND up.IsStaff=1 ";
                        }
                        if (ddlCategory.SelectedValue == "3")
                        {
                            str += " AND up.IsCompanion=1 ";
                        }
                        if (ddlCategory.SelectedValue == "4")
                        {
                            str += " AND up.IsLayPerson=1 ";
                        }
                        if (ddlCategory.SelectedValue == "5")
                        {

                        }

                    }
                    if (str.Length > 0)
                    {
                        gvReport.DataSource = null;
                        gvReport.DataBind();
                        dt = osfcon.getRows(str);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            gvReport.DataSource = dt;
                            gvReport.DataBind();
                        }
                        else
                        {
                            gvReport.DataSource = dt;
                            gvReport.DataBind();
                        }
                    }

                }
                else
                {
                    gvReport.DataSource = dt;
                    gvReport.DataBind();
                }
            }
            gvReport.DataSource = dt;
            gvReport.DataBind();
        }
             
        catch
        {
            gvReport.DataSource = dt;
            gvReport.DataBind();

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }


    protected void lbtnView1_Click(object sender, System.EventArgs e)
    {
        _isEdit = true;
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        HiddenField vId = (HiddenField)gvReport.Rows[row.RowIndex].FindControl("hdId");
        int nId = vId.Value == "" ? 0 : Convert.ToInt32(vId.Value);
        if (nId > 0)
        {
            ImageButton btndetails = sender as ImageButton;
            Users uObj = new Users(osfcon.CONNECTIONSTRING);
            DataTable dt = osfcon.getRows(string.Format("SELECT u.*, Job = (SELECT Name From BasicData WHERE BasicDataID=u.JobID), Department = (SELECT Name From BasicData WHERE BasicDataID=u.DepartmentID), Location = (SELECT Name From BasicData WHERE BasicDataID=u.LocationID) FROM users u WHERE u.UserID={0}", Convert.ToInt32(vId.Value)));
            if (dt != null)
            {
                this.FillControlsView(dt);
                this.ReportPopUpNew.Show();  


            
      
            }
            else
            {
                DisplayAlert("No Details Data Found");
            }

        }


    }

    protected void btnUserViewExit_Click(object sender, EventArgs e)
    {

    }
    protected void gvUser_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (Session["UserPermission"] != null)
        {
            UserPermissions up = (UserPermissions)Session["UserPermission"];
            if (!up.IsDirectoryAdmin)
            {
                ((DataControlField)gvReport.Columns
                    .Cast<DataControlField>()
                    .Where(fld => fld.HeaderText == "Edit")
                    .SingleOrDefault()).Visible = false;

                ((DataControlField)gvReport.Columns
                .Cast<DataControlField>()
                .Where(fld => fld.HeaderText == "Permission")
                .SingleOrDefault()).Visible = false;
            }
        }
    }
    #endregion

    #region Method


    private void DisplayAlert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg.Replace("'", "\\'").Replace(Environment.NewLine, "\\n")), true);
    }

    private void FillControlsView(DataTable dt)
    {
        try
        {
            if (dt.Rows[0]["FirstName"] != null)
                lblFirstName.Text = dt.Rows[0]["FirstName"].ToString();

            if (dt.Rows[0]["LastName"] != null)
                lblLastName.Text = dt.Rows[0]["LastName"].ToString();
            //lblD.Text = dt.Rows[0]["DEPT***"].ToString();
            //lblHomePhone.Text = dt.Rows[0]["POS****"].ToString();
            if (dt.Rows[0]["Chapter"] != null)
                lblAreaChapter.Text = dt.Rows[0]["Chapter"].ToString();

            if (dt.Rows[0]["CompanionType"] != null)
                lblCompanionType.Text = dt.Rows[0]["CompanionType"].ToString();

            if (dt.Rows[0]["ProfessionalYear"] != null)
                lblProfessionYear.Text = dt.Rows[0]["ProfessionalYear"].ToString();

            if (dt.Rows[0]["CompanionType"] != null)
                lblMI.Text = dt.Rows[0]["CompanionType"].ToString();

            if (dt.Rows[0]["HomeStreet1"] != null)
                lblHomeStreet1.Text = dt.Rows[0]["HomeStreet1"].ToString();
            if (dt.Rows[0]["HomeStreet2"] != null)
                lblHomeStreet1.Text = dt.Rows[0]["HomeStreet2"].ToString();
            if (dt.Rows[0]["HomeCity"] != null)
                lblHomeCity.Text = dt.Rows[0]["HomeCity"].ToString();
            if (dt.Rows[0]["HomeState"] != null)
                lblHomeState.Text = dt.Rows[0]["HomeState"].ToString();

            if (dt.Rows[0]["HomeZip"] != null)
                lblHomeZip.Text = dt.Rows[0]["HomeZip"].ToString();

            if (dt.Rows[0]["HomeEmail"] != null)
                lblHomeEmail.Text = dt.Rows[0]["HomeEmail"].ToString();

            if (dt.Rows[0]["HomePhone"] != null)
                lblHomePhone.Text = dt.Rows[0]["HomePhone"].ToString();

            if (dt.Rows[0]["MinistryClassification"] != null)
                lblMinistry1Classification.Text = dt.Rows[0]["MinistryClassification"].ToString();

            if (dt.Rows[0]["MinistryLocation"] != null)
                lblMinistry1PlaceofEmployment.Text = dt.Rows[0]["MinistryLocation"].ToString();

            if (dt.Rows[0]["MinistryStreet1"] != null)
                lblMinistry1Street1.Text = dt.Rows[0]["MinistryStreet1"].ToString();

            if (dt.Rows[0]["MinistryStreet2"] != null)
                lblMinistry1Street1.Text = dt.Rows[0]["MinistryStreet2"].ToString();

            if (dt.Rows[0]["MinistryCity"] != null)
                lblMinistry1City.Text = dt.Rows[0]["MinistryCity"].ToString();

            if (dt.Rows[0]["MinistryCountry"] != null)
                lblMinistry1Country.Text = dt.Rows[0]["MinistryCountry"].ToString();

            if (dt.Rows[0]["MinistryZip"] != null)
                lblMinistry1Zip.Text = dt.Rows[0]["MinistryZip"].ToString();

            if (dt.Rows[0]["MinistryPhone"] != null)
                lblMinistry1Phone.Text = dt.Rows[0]["MinistryPhone"].ToString();

            if (dt.Rows[0]["MinistryFax"] != null)
                lblMinistry1Fax.Text = dt.Rows[0]["MinistryFax"].ToString();

            if (dt.Rows[0]["MinistryEmail"] != null)
                lblMinistry1Email.Text = dt.Rows[0]["MinistryEmail"].ToString();

            if (dt.Rows[0]["Ministry2Title"] != null)
                lblMinistry2Title.Text = dt.Rows[0]["Ministry2Title"].ToString();

            if (dt.Rows[0]["Ministry2Classification"] != null)
                lblMinistry2Classification2.Text = dt.Rows[0]["Ministry2Classification"].ToString();

            if (dt.Rows[0]["Ministry2Location"] != null)
                lblMinistry2PlaceofEmp.Text = dt.Rows[0]["Ministry2Location"].ToString();

            if (dt.Rows[0]["Ministry2Street1"] != null)
                lblMinistry2Street1.Text = dt.Rows[0]["Ministry2Street1"].ToString();

            if (dt.Rows[0]["Ministry2Street2"] != null)
                lblMinistry2street2.Text = dt.Rows[0]["Ministry2Street2"].ToString();

            if (dt.Rows[0]["Ministry2City"] != null)
                lblMinistry2City.Text = dt.Rows[0]["Ministry2City"].ToString();

            if (dt.Rows[0]["Ministry2Country"] != null)
                lblMinistry2Country.Text = dt.Rows[0]["Ministry2Country"].ToString();

            if (dt.Rows[0]["Ministry2Zip"] != null)
                lblMinistry2Zip.Text = dt.Rows[0]["Ministry2Zip"].ToString();

            if (dt.Rows[0]["Ministry2Phone"] != null)
                lblMinistry2Phone.Text = dt.Rows[0]["Ministry2Phone"].ToString();

            if (dt.Rows[0]["CompanionType"] != null)
                lblMinistry2Fax.Text = dt.Rows[0]["Ministry2Fax"].ToString();

            if (dt.Rows[0]["Ministry2Email"] != null)
                lblMinistry2Email.Text = dt.Rows[0]["Ministry2Email"].ToString();

            if (dt.Rows[0]["Job"] != null)
                lblPosition.Text = dt.Rows[0]["Job"].ToString();

            if (dt.Rows[0]["Department"] != null)
                lblDepartment.Text = dt.Rows[0]["Department"].ToString();

            if (dt.Rows[0]["Location"] != null)
                lblHomeLocation.Text = dt.Rows[0]["Location"].ToString();

            //ddlLocation.SelectedValue = dt.Rows[0]["LocationID"].ToString();

            if (dt.Rows[0]["Picture"] != null && dt.Rows[0]["Picture"].ToString() != string.Empty)
            {
                string filePath = Path.Combine(Request.PhysicalApplicationPath, "Images\\Users\\");

                imgUserView.Attributes["src"] = "../Images/Users/" + dt.Rows[0]["Picture"].ToString();
            }
        }
        catch
        {

        }


    }


    #endregion

}