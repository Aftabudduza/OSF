using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_DateTimeContentSearch : System.Web.UI.UserControl
{

    private List<ContentObj> _contents;

    public List<ContentObj> Contents
    {
        get { return _contents; }
        set { _contents = value; }
    }

    private int _sectionType;

    public int SectionType
    {
        get { return _sectionType; }
        set { _sectionType = value; }
    }


    private int _categoryID;

    public int CategoryID
    {
        get { return _categoryID; }
        set { _categoryID = value; }
    }

    private int _userID;

    public int UserID
    {
        get { return _userID; }
        set { _userID = value; }
    }

    public event EventHandler buttonClick;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string message = this.ValidateSearch().Trim();

        if (message.Length > 0)
        {
            DisplayAlert(message);
        }
        else
        {
            DateTime fromDate = DateTime.MinValue, toDate = DateTime.MinValue;
            if (txtFromDate.Text.Trim() != "")
                fromDate = Convert.ToDateTime(txtFromDate.Text);
            if (txtToDate.Text.Trim() != "")
                toDate = Convert.ToDateTime(txtToDate.Text);

            List<ContentObj> cObjs = new List<ContentObj>();
            ContentObj contents = new ContentObj(osfcon.CONNECTIONSTRING);

            if (SectionType == (int)EnumSectionType.News)
            {
                cObjs = contents.getRecordsForNewsType(_sectionType, fromDate, toDate);
              //  lblRcentTitle.Text = "News at OSF";
            }
            else
            {


                EnumSectionType enumDisplayStatus = ((EnumSectionType)_sectionType);
                string stringValue = enumDisplayStatus.ToString();

              //  lblRcentTitle.Text = string.Format("Recent {0}s", stringValue);
                cObjs = contents.getRecordsWithPermission(_sectionType, fromDate, toDate, _userID);

               
            }
            this._contents = cObjs;
            buttonClick(sender, e);
        }
    }

    private string ValidateSearch()
    {
        string _message = "";

        if (txtFromDate.Text.ToString().Trim().Length != 0)
        {
            if (!Utility.IsDate(txtFromDate.Text.ToString()))
            {
                _message += "Please enter valid From date" + Environment.NewLine;
            }
        }
        else if (txtFromDate.Text.ToString().Trim().Length == 0)
        {
            _message += "Please enter From date" + Environment.NewLine;
        }


        if (txtToDate.Text.ToString().Trim().Length != 0)
        {
            if (!Utility.IsDate(txtToDate.Text.ToString()))
            {
                _message += "Please enter valid To date" + Environment.NewLine;
            }
        }
        else if (txtToDate.Text.ToString().Trim().Length == 0)
        {

            _message += "Please enter To date" + Environment.NewLine;
        }

        return _message;
    }
    private void DisplayAlert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", msg.Replace("'", "\\'").Replace(Environment.NewLine, "\\n")), true);
    }
}