using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

public class Users
    {
    // If 'connectionString' is set by the constructor or externally, then the connection will be
    // opened and closed with each operation. If 'conn' is set, then the connection stays open.
    public SqlConnection conn = null;
    public string connectionString = "";
    public string booleanFalse = "N";
    public string booleanTrue = "Y";
    public string lastError = "";
    public bool valid = false;
    public ArrayList colInfo;

    // A variable for each column in the table.
    public int UserID;
    public string LastName;
    public string FirstName;
    public string Username;
    public string Password;
    public string Picture;
    public string HomeEmail;
    public string HomePhone;
    public int Chapter;
    public int UpdatedByID;
    public DateTime LastUpdated;
    public int DepartmentID;
    public int JobID;
    public int LocationID;
    public string HomeStreet1;
    public string HomeStreet2;
    public string HomeCity;
    public string HomeState;
    public string HomeZip;
    public string HomeCountry;
    public string MI;
    public int IspID;
    public string MinistryTitle;
    public string MinistryLocation;
    public string MinistryCode;
    public string MinistryClassification;
    public string MinistryPhone;
    public string MinistryFax;
    public string MinistryEmail;
    public string MinistryStreet1;
    public string MinistryStreet2;
    public string MinistryCity;
    public string MinistryState;
    public string MinistryCountry;
    public string MinistryZip;
    public string Ministry2Title;
    public string Ministry2Location;
    public string Ministry2Code;
    public string Ministry2Classification;
    public string Ministry2Phone;
    public string Ministry2Fax;
    public string Ministry2Email;
    public string Ministry2Street1;
    public string Ministry2Street2;
    public string Ministry2City;
    public string Ministry2State;
    public string Ministry2Zip;
    public string Ministry2Country;
    public int FailedLogins;
    public DateTime LastPasswordChange;
    public string CompanionType;
    public int ProfessionalYear;
    public DateTime CreatedDate;
    public int CreatedBy;

#region User Variables
    // Add your variables to this object here. Adding them here ensures
    // that they will be retained when the object is re-generated.
#endregion

#region Column Info
    // This class maintains information about each column in the table.
    public class ColumnInfo : IComparable
        {
        public string columnName;
        public string dataType;
        public int length;
        public bool allowInsert;
        public bool allowUpdate;

        public ColumnInfo(string _ColumnName, string _DataType, int _Length, bool _AllowInsert, bool _AllowUpdate)
            {
            columnName = _ColumnName;
            dataType = _DataType;
            length = _Length;
            allowInsert = _AllowInsert;
            allowUpdate = _AllowUpdate;
            }

        public int CompareTo(Object obj)
            {
            if(obj.GetType() == this.GetType())
                {
                ColumnInfo ci = (ColumnInfo)obj;
                return this.columnName.CompareTo(ci.columnName);
                }
            string s = (string)obj;
            return this.columnName.CompareTo(s);
            }
        }
#endregion

#region Constructors
    /// <summary></summary>
    public Users()
        {
        blank();
        colInfo = new ArrayList();
        colInfo.Add(new ColumnInfo("UserID", "int", 11, false, false));
        colInfo.Add(new ColumnInfo("LastName", "string", 30, true, true));
        colInfo.Add(new ColumnInfo("FirstName", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Username", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Password", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Picture", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("HomeEmail", "string", 100, true, true));
        colInfo.Add(new ColumnInfo("HomePhone", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Chapter", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("UpdatedByID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("LastUpdated", "DateTime", 19, true, true));
        colInfo.Add(new ColumnInfo("DepartmentID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("JobID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("LocationID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("HomeStreet1", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("HomeStreet2", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("HomeCity", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("HomeState", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("HomeZip", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("HomeCountry", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("MI", "string", 1, true, true));
        colInfo.Add(new ColumnInfo("IspID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("MinistryTitle", "string", 100, true, true));
        colInfo.Add(new ColumnInfo("MinistryLocation", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("MinistryCode", "string", 10, true, true));
        colInfo.Add(new ColumnInfo("MinistryClassification", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("MinistryPhone", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("MinistryFax", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("MinistryEmail", "string", 100, true, true));
        colInfo.Add(new ColumnInfo("MinistryStreet1", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("MinistryStreet2", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("MinistryCity", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("MinistryState", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("MinistryCountry", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("MinistryZip", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Ministry2Title", "string", 100, true, true));
        colInfo.Add(new ColumnInfo("Ministry2Location", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Ministry2Code", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Ministry2Classification", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Ministry2Phone", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Ministry2Fax", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Ministry2Email", "string", 100, true, true));
        colInfo.Add(new ColumnInfo("Ministry2Street1", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Ministry2Street2", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Ministry2City", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Ministry2State", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Ministry2Zip", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("Ministry2Country", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("FailedLogins", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("LastPasswordChange", "DateTime", 19, true, true));
        colInfo.Add(new ColumnInfo("CompanionType", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("ProfessionalYear", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("CreatedDate", "DateTime", 19, true, true));
        colInfo.Add(new ColumnInfo("CreatedBy", "int", 11, true, true));
        
        colInfo.Sort();
        }

    /// <summary></summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    public Users(SqlConnection _conn) : this()
        {
        conn = _conn;
        }

    /// <summary></summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    public Users(string _connectionString) : this()
        {
        connectionString = _connectionString;
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    /// <param name="_UserID">A primary key column in the Users table</param>
    public Users(SqlConnection _conn, int _UserID) : this()
        {
        conn = _conn;
        getRecord(_UserID);
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    /// <param name="_UserID">A primary key column in the Users table</param>
    public Users(string _connectionString, int _UserID) : this()
        {
        connectionString = _connectionString;
        getRecord(_UserID);
        }
#endregion

#region Methods
    /// <summary>Clear the local column variables associated with a Users table record</summary>
    public void blank()
        {
        UserID = 0;
        LastName = "";
        FirstName = "";
        Username = "";
        Password = "";
        Picture = "";
        HomeEmail = "";
        HomePhone = "";
        Chapter = 0;
        UpdatedByID = 0;
        LastUpdated = new DateTime(0);
        DepartmentID = 0;
        JobID = 0;
        LocationID = 0;
        HomeStreet1 = "";
        HomeStreet2 = "";
        HomeCity = "";
        HomeState = "";
        HomeZip = "";
        HomeCountry = "";
        MI = "";
        IspID = 0;
        MinistryTitle = "";
        MinistryLocation = "";
        MinistryCode = "";
        MinistryClassification = "";
        MinistryPhone = "";
        MinistryFax = "";
        MinistryEmail = "";
        MinistryStreet1 = "";
        MinistryStreet2 = "";
        MinistryCity = "";
        MinistryState = "";
        MinistryCountry = "";
        MinistryZip = "";
        Ministry2Title = "";
        Ministry2Location = "";
        Ministry2Code = "";
        Ministry2Classification = "";
        Ministry2Phone = "";
        Ministry2Fax = "";
        Ministry2Email = "";
        Ministry2Street1 = "";
        Ministry2Street2 = "";
        Ministry2City = "";
        Ministry2State = "";
        Ministry2Zip = "";
        Ministry2Country = "";
        FailedLogins = 0;
        LastPasswordChange = new DateTime(0);
        CompanionType = "";
        ProfessionalYear = 0;
        CreatedDate = new DateTime(0);
        CreatedBy = 0;
        valid = false;
        }

    /// <summary>Get a DataRow from the Users table using the primary key</summary>
    /// <param name="_UserID">A primary key column in the Users table</param>
    /// <returns>A DataRow from the Users table</returns>
    public DataRow getRow(int _UserID)
        {
        return getRow("*", _UserID);
        }

    /// <summary>Get a DataRow from the Users table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">A list of column names in the Users table separated by commas</param>
    /// <param name="_UserID">A primary key column in the Users table</param>
    /// <returns>A DataRow from the Users table</returns>
    public DataRow getRow(string columnList, int _UserID)
        {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        try
            {
            connect();
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"Users\" WHERE UserID = @UserID", conn);
            cmd.Parameters.AddWithValue("@UserID", _UserID);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            disconnect();
            valid = true;
            if(dt.Rows.Count > 0) return dt.Rows[0];
            lastError = "No record found";
            }
        catch(SqlException ex)
            {
            lastError = translateException(ex);
            }
        catch(Exception ex)
            {
            lastError = ex.Message;
            }
        valid = false;
        return null;
        }

    /// <summary>Get a DataRow from the Users table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An array of column names in the Users</param>
    /// <param name="_UserID">A primary key column in the Users table</param>
    /// <returns>A DataRow from the Users table</returns>
    public DataRow getRow(string[] columnList, int _UserID)
        {
        string cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRow(cl, _UserID);
        }

    /// <summary>Get a DataRow from the Users table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the Users</param>
    /// <param name="_UserID">A primary key column in the Users table</param>
    /// <returns>A DataRow from the Users table</returns>
    public DataRow getRow(ArrayList columnList, int _UserID)
        {
        String cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRow(cl, _UserID);
        }

    /// <summary>Get a record from the Users table and populate the local variables</summary>
    /// <param name="_UserID">A primary key column in the Users table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will not be reflected in local variables</remarks>
    public bool getRecord(int _UserID)
        {
        DataRow dr = getRow(_UserID);

        valid = false;
        if(dr != null)
            {
            UserID = (dr["UserID"] == DBNull.Value ? (int)0 : (int)dr["UserID"]);
            LastName = (dr["LastName"] == DBNull.Value ? (string)"" : (string)dr["LastName"]);
            FirstName = (dr["FirstName"] == DBNull.Value ? (string)"" : (string)dr["FirstName"]);
            Username = (dr["Username"] == DBNull.Value ? (string)"" : (string)dr["Username"]);
            Password = (dr["Password"] == DBNull.Value ? (string)"" : (string)dr["Password"]);
            Picture = (dr["Picture"] == DBNull.Value ? (string)"" : (string)dr["Picture"]);
            HomeEmail = (dr["HomeEmail"] == DBNull.Value ? (string)"" : (string)dr["HomeEmail"]);
            HomePhone = (dr["HomePhone"] == DBNull.Value ? (string)"" : (string)dr["HomePhone"]);
            Chapter = (dr["Chapter"] == DBNull.Value ? (int)0 : (int)dr["Chapter"]);
            UpdatedByID = (dr["UpdatedByID"] == DBNull.Value ? (int)0 : (int)dr["UpdatedByID"]);
            LastUpdated = (dr["LastUpdated"] == DBNull.Value ? (DateTime)new DateTime(0) : (DateTime)dr["LastUpdated"]);
            DepartmentID = (dr["DepartmentID"] == DBNull.Value ? (int)0 : (int)dr["DepartmentID"]);
            JobID = (dr["JobID"] == DBNull.Value ? (int)0 : (int)dr["JobID"]);
            LocationID = (dr["LocationID"] == DBNull.Value ? (int)0 : (int)dr["LocationID"]);
            HomeStreet1 = (dr["HomeStreet1"] == DBNull.Value ? (string)"" : (string)dr["HomeStreet1"]);
            HomeStreet2 = (dr["HomeStreet2"] == DBNull.Value ? (string)"" : (string)dr["HomeStreet2"]);
            HomeCity = (dr["HomeCity"] == DBNull.Value ? (string)"" : (string)dr["HomeCity"]);
            HomeState = (dr["HomeState"] == DBNull.Value ? (string)"" : (string)dr["HomeState"]);
            HomeZip = (dr["HomeZip"] == DBNull.Value ? (string)"" : (string)dr["HomeZip"]);
            HomeCountry = (dr["HomeCountry"] == DBNull.Value ? (string)"" : (string)dr["HomeCountry"]);
            MI = (dr["MI"] == DBNull.Value ? (string)"" : (string)dr["MI"]);
            IspID = (dr["IspID"] == DBNull.Value ? (int)0 : (int)dr["IspID"]);
            MinistryTitle = (dr["MinistryTitle"] == DBNull.Value ? (string)"" : (string)dr["MinistryTitle"]);
            MinistryLocation = (dr["MinistryLocation"] == DBNull.Value ? (string)"" : (string)dr["MinistryLocation"]);
            MinistryCode = (dr["MinistryCode"] == DBNull.Value ? (string)"" : (string)dr["MinistryCode"]);
            MinistryClassification = (dr["MinistryClassification"] == DBNull.Value ? (string)"" : (string)dr["MinistryClassification"]);
            MinistryPhone = (dr["MinistryPhone"] == DBNull.Value ? (string)"" : (string)dr["MinistryPhone"]);
            MinistryFax = (dr["MinistryFax"] == DBNull.Value ? (string)"" : (string)dr["MinistryFax"]);
            MinistryEmail = (dr["MinistryEmail"] == DBNull.Value ? (string)"" : (string)dr["MinistryEmail"]);
            MinistryStreet1 = (dr["MinistryStreet1"] == DBNull.Value ? (string)"" : (string)dr["MinistryStreet1"]);
            MinistryStreet2 = (dr["MinistryStreet2"] == DBNull.Value ? (string)"" : (string)dr["MinistryStreet2"]);
            MinistryCity = (dr["MinistryCity"] == DBNull.Value ? (string)"" : (string)dr["MinistryCity"]);
            MinistryState = (dr["MinistryState"] == DBNull.Value ? (string)"" : (string)dr["MinistryState"]);
            MinistryCountry = (dr["MinistryCountry"] == DBNull.Value ? (string)"" : (string)dr["MinistryCountry"]);
            MinistryZip = (dr["MinistryZip"] == DBNull.Value ? (string)"" : (string)dr["MinistryZip"]);
            Ministry2Title = (dr["Ministry2Title"] == DBNull.Value ? (string)"" : (string)dr["Ministry2Title"]);
            Ministry2Location = (dr["Ministry2Location"] == DBNull.Value ? (string)"" : (string)dr["Ministry2Location"]);
            Ministry2Code = (dr["Ministry2Code"] == DBNull.Value ? (string)"" : (string)dr["Ministry2Code"]);
            Ministry2Classification = (dr["Ministry2Classification"] == DBNull.Value ? (string)"" : (string)dr["Ministry2Classification"]);
            Ministry2Phone = (dr["Ministry2Phone"] == DBNull.Value ? (string)"" : (string)dr["Ministry2Phone"]);
            Ministry2Fax = (dr["Ministry2Fax"] == DBNull.Value ? (string)"" : (string)dr["Ministry2Fax"]);
            Ministry2Email = (dr["Ministry2Email"] == DBNull.Value ? (string)"" : (string)dr["Ministry2Email"]);
            Ministry2Street1 = (dr["Ministry2Street1"] == DBNull.Value ? (string)"" : (string)dr["Ministry2Street1"]);
            Ministry2Street2 = (dr["Ministry2Street2"] == DBNull.Value ? (string)"" : (string)dr["Ministry2Street2"]);
            Ministry2City = (dr["Ministry2City"] == DBNull.Value ? (string)"" : (string)dr["Ministry2City"]);
            Ministry2State = (dr["Ministry2State"] == DBNull.Value ? (string)"" : (string)dr["Ministry2State"]);
            Ministry2Zip = (dr["Ministry2Zip"] == DBNull.Value ? (string)"" : (string)dr["Ministry2Zip"]);
            Ministry2Country = (dr["Ministry2Country"] == DBNull.Value ? (string)"" : (string)dr["Ministry2Country"]);
            FailedLogins = (dr["FailedLogins"] == DBNull.Value ? (int)0 : (int)dr["FailedLogins"]);
            LastPasswordChange = (dr["LastPasswordChange"] == DBNull.Value ? (DateTime)new DateTime(0) : (DateTime)dr["LastPasswordChange"]);
            CompanionType = (dr["CompanionType"] == DBNull.Value ? (string)"" : (string)dr["CompanionType"]);
            ProfessionalYear = (dr["ProfessionalYear"] == DBNull.Value ? (int)0 : (int)dr["ProfessionalYear"]);
            CreatedBy = (dr["CreatedBy"] == DBNull.Value ? (int)0 : (int)dr["CreatedBy"]);
            CreatedDate = (dr["CreatedDate"] == DBNull.Value ? (DateTime)new DateTime(0) : (DateTime)dr["CreatedDate"]);
            valid = true;
            }
        return valid;
        }

    /// <summary>Delete a row from the Users table</summary>
    /// <param name="_UserID">A primary key column in the Users table</param>
    /// <returns>'True', if successful</returns>
    public bool delete(int _UserID)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"Users\" WHERE UserID = @UserID", conn);
            cmd.Parameters.AddWithValue("@UserID", _UserID);
            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
            }
        catch(SqlException ex)
            {
            disconnect();
            lastError = translateException(ex);
            return false;
            }
        catch(Exception ex)
            {
            disconnect();
            lastError = ex.Message;
            return false;
            }
        blank();
        return true;
        }

    /// <summary>Delete a row from the Users table</summary>
    /// <param name="Row">The DataRow to be deletedfrom the Users table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>The DataRow must contain all primary key columns</remarks>
    public bool delete(DataRow row)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"Users\" WHERE UserID = @UserID", conn);
            cmd.Parameters.AddWithValue("@UserID", row["UserID"]);
            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
            }
        catch(SqlException ex)
            {
            lastError = translateException(ex);
            disconnect();
            return false;
            }
        catch(Exception ex)
            {
            lastError = ex.Message;
            disconnect();
            return false;
            }
        return true;
        }

    /// <summary>Delete the current row from the Users table</summary>
    /// <returns>'True', if successful</returns>
    public bool delete()
        {
        if(!valid)
           {
           lastError = "A valid current record is needed to delete";
           return false;
           }
        return delete(UserID);
        }

    /// <summary>Get rows from the Users table</summary>
    /// <param name="ColumnList">A list of column names in the Users table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the Users table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string columnList, string whereClause, string orderBy)
        {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        string ob = orderBy;

        try
            {
            connect();
            if(!orderBy.Equals("")) ob = " ORDER BY " + ob;
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"Users\" WHERE " + whereClause + ob, conn);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            disconnect();
            return dt;
            }
        catch(SqlException ex)
            {
            lastError = translateException(ex);
            }
        catch(Exception ex)
            {
            lastError = ex.Message;
            }
        return null;
        }

    /// <summary>Get rows from the Users table</summary>
    /// <param name="ColumnList">A list of column names in the Users table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the Users table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the Users table</summary>
    /// <param name="ColumnList">An array of column names in the Users</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the Users table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string[] columnList, string whereClause, string orderBy)
        {
        string cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRows(cl, whereClause, orderBy);
        }

    /// <summary>Get a rows from the Users table</summary>
    /// <param name="ColumnList">An array of column names in the Users</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the Users table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string[] columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the Users table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the Users</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the Users table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(ArrayList columnList, string whereClause, string orderBy)
        {
        string cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRows(cl, whereClause, orderBy);
        }


    /// <summary>Get a rows from the Users table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the Users</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the Users table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(ArrayList columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }


    /// <summary>Insert a record in the Users table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    public bool insert()
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("INSERT INTO \"Users\" (LastName, FirstName, Username, Password, Picture, HomeEmail, HomePhone, Chapter,  DepartmentID, JobID, LocationID, HomeStreet1, HomeStreet2, HomeCity, HomeState, HomeZip, HomeCountry, MI, IspID, MinistryTitle, MinistryLocation, MinistryCode, MinistryClassification, MinistryPhone, MinistryFax, MinistryEmail, MinistryStreet1, MinistryStreet2, MinistryCity, MinistryState, MinistryCountry, MinistryZip, Ministry2Title, Ministry2Location, Ministry2Code, Ministry2Classification, Ministry2Phone, Ministry2Fax, Ministry2Email, Ministry2Street1, Ministry2Street2, Ministry2City, Ministry2State, Ministry2Zip, Ministry2Country, FailedLogins, CompanionType, ProfessionalYear,CreatedDate,CreatedBy) VALUES (@LastName, @FirstName, @Username, @Password, @Picture, @HomeEmail, @HomePhone, @Chapter,  @DepartmentID, @JobID, @LocationID, @HomeStreet1, @HomeStreet2, @HomeCity, @HomeState, @HomeZip, @HomeCountry, @MI, @IspID, @MinistryTitle, @MinistryLocation, @MinistryCode, @MinistryClassification, @MinistryPhone, @MinistryFax, @MinistryEmail, @MinistryStreet1, @MinistryStreet2, @MinistryCity, @MinistryState, @MinistryCountry, @MinistryZip, @Ministry2Title, @Ministry2Location, @Ministry2Code, @Ministry2Classification, @Ministry2Phone, @Ministry2Fax, @Ministry2Email, @Ministry2Street1, @Ministry2Street2, @Ministry2City, @Ministry2State, @Ministry2Zip, @Ministry2Country, @FailedLogins, @CompanionType, @ProfessionalYear, @CreatedDate, @CreatedBy)", conn);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@Picture", Picture);
            cmd.Parameters.AddWithValue("@HomeEmail", HomeEmail);
            cmd.Parameters.AddWithValue("@HomePhone", HomePhone);
            cmd.Parameters.AddWithValue("@Chapter", Chapter);
            //cmd.Parameters.AddWithValue("@UpdatedByID", UpdatedByID);
          //  cmd.Parameters.AddWithValue("@LastUpdated", LastUpdated);
            cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);
            cmd.Parameters.AddWithValue("@JobID", JobID);
            cmd.Parameters.AddWithValue("@LocationID", LocationID);
            cmd.Parameters.AddWithValue("@HomeStreet1", HomeStreet1);
            cmd.Parameters.AddWithValue("@HomeStreet2", HomeStreet2);
            cmd.Parameters.AddWithValue("@HomeCity", HomeCity);
            cmd.Parameters.AddWithValue("@HomeState", HomeState);
            cmd.Parameters.AddWithValue("@HomeZip", HomeZip);
            cmd.Parameters.AddWithValue("@HomeCountry", HomeCountry);
            cmd.Parameters.AddWithValue("@MI", MI);
            cmd.Parameters.AddWithValue("@IspID", IspID);
            cmd.Parameters.AddWithValue("@MinistryTitle", MinistryTitle);
            cmd.Parameters.AddWithValue("@MinistryLocation", MinistryLocation);
            cmd.Parameters.AddWithValue("@MinistryCode", MinistryCode);
            cmd.Parameters.AddWithValue("@MinistryClassification", MinistryClassification);
            cmd.Parameters.AddWithValue("@MinistryPhone", MinistryPhone);
            cmd.Parameters.AddWithValue("@MinistryFax", MinistryFax);
            cmd.Parameters.AddWithValue("@MinistryEmail", MinistryEmail);
            cmd.Parameters.AddWithValue("@MinistryStreet1", MinistryStreet1);
            cmd.Parameters.AddWithValue("@MinistryStreet2", MinistryStreet2);
            cmd.Parameters.AddWithValue("@MinistryCity", MinistryCity);
            cmd.Parameters.AddWithValue("@MinistryState", MinistryState);
            cmd.Parameters.AddWithValue("@MinistryCountry", MinistryCountry);
            cmd.Parameters.AddWithValue("@MinistryZip", MinistryZip);
            cmd.Parameters.AddWithValue("@Ministry2Title", Ministry2Title);
            cmd.Parameters.AddWithValue("@Ministry2Location", Ministry2Location);
            cmd.Parameters.AddWithValue("@Ministry2Code", Ministry2Code);
            cmd.Parameters.AddWithValue("@Ministry2Classification", Ministry2Classification);
            cmd.Parameters.AddWithValue("@Ministry2Phone", Ministry2Phone);
            cmd.Parameters.AddWithValue("@Ministry2Fax", Ministry2Fax);
            cmd.Parameters.AddWithValue("@Ministry2Email", Ministry2Email);
            cmd.Parameters.AddWithValue("@Ministry2Street1", Ministry2Street1);
            cmd.Parameters.AddWithValue("@Ministry2Street2", Ministry2Street2);
            cmd.Parameters.AddWithValue("@Ministry2City", Ministry2City);
            cmd.Parameters.AddWithValue("@Ministry2State", Ministry2State);
            cmd.Parameters.AddWithValue("@Ministry2Zip", Ministry2Zip);
            cmd.Parameters.AddWithValue("@Ministry2Country", Ministry2Country);
            cmd.Parameters.AddWithValue("@FailedLogins", FailedLogins);
         //   cmd.Parameters.AddWithValue("@LastPasswordChange", LastPasswordChange);
            cmd.Parameters.AddWithValue("@CompanionType", CompanionType);
            cmd.Parameters.AddWithValue("@ProfessionalYear", ProfessionalYear);
            cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.ExecuteScalar();
            cmd.Dispose();

           
            disconnect();
            }
        catch(SqlException ex)
            {
            lastError = translateException(ex);
            disconnect();
            return false;
            }
        catch(Exception ex)
            {
            lastError = ex.Message;
            disconnect();
            return false;
            }
        return true;
        }

    /// <summary>Insert a record in the Users table from a DataRow</summary>
    /// <param name="Row">The DataRow to be inserted in the Users table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>The DataRow must contain all primary key columns</remarks>
    public bool insert(DataRow row)
        {
        SqlCommand cmd;
        string setList = "";
        string valList = "";

        try
            {
            connect();
            // Build the set list
            foreach(DataColumn col in row.Table.Columns)
                {
                switch(col.ColumnName.ToLower())
                    {
                    case "userid":
                        break;
                    default:
                        if(!setList.Equals(""))
                            {
                            setList += ", ";
                            valList += ", ";
                            }
                        setList += col.ColumnName;
                        valList += " @" + col.ColumnName;
                        break;
                    }
                }
            cmd = new SqlCommand("INSERT INTO \"Users\" (" + setList + ") VALUES (" + valList + ")", conn);
            // Create the parameters
            foreach(DataColumn col in row.Table.Columns)
                {
                switch(col.ColumnName.ToLower())
                    {
                    case "UserID":
                        break;
                    default:
                        cmd.Parameters.AddWithValue("@" + col.ColumnName, row[col.ColumnName]);
                        break;
                    }
                }
            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
            }
        catch(SqlException ex)
            {
            lastError = translateException(ex);
            disconnect();
            return false;
            }
        catch(Exception ex)
            {
            lastError = ex.Message;
            disconnect();
            return false;
            }
        return true;
        }

    /// <summary>Update a record in the Users table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will be replaced by blanks and zeros</remarks>
    public bool update()
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand(string.Format(@"UPDATE Users SET LastName = @LastName, FirstName = @FirstName,  Picture = @Picture, HomeEmail = @HomeEmail,
                                HomePhone = @HomePhone, Chapter = @Chapter, UpdatedByID = @UpdatedByID, LastUpdated = @LastUpdated,
                                DepartmentID = @DepartmentID, JobID = @JobID, LocationID = @LocationID, HomeStreet1 = @HomeStreet1, 
                                HomeStreet2 = @HomeStreet2, HomeCity = @HomeCity, HomeState = @HomeState, HomeZip = @HomeZip, 
                                HomeCountry = @HomeCountry, MI = @MI, IspID = @IspID, MinistryTitle = @MinistryTitle,
                                MinistryLocation = @MinistryLocation, MinistryCode = @MinistryCode, MinistryClassification = @MinistryClassification, MinistryPhone = @MinistryPhone, 
                                MinistryFax = @MinistryFax, MinistryEmail = @MinistryEmail, MinistryStreet1 = @MinistryStreet1, MinistryStreet2 = @MinistryStreet2,
                                MinistryCity = @MinistryCity, MinistryState = @MinistryState, MinistryCountry = @MinistryCountry, MinistryZip = @MinistryZip,
                                Ministry2Title = @Ministry2Title, Ministry2Location = @Ministry2Location, Ministry2Code = @Ministry2Code, Ministry2Classification = @Ministry2Classification,
                                Ministry2Phone = @Ministry2Phone, Ministry2Fax = @Ministry2Fax, Ministry2Email = @Ministry2Email, Ministry2Street1 = @Ministry2Street1,
                                Ministry2Street2 = @Ministry2Street2, Ministry2City = @Ministry2City, Ministry2State = @Ministry2State, Ministry2Zip = @Ministry2Zip, Ministry2Country = @Ministry2Country, 
                                CompanionType = @CompanionType, ProfessionalYear = @ProfessionalYear WHERE UserID = @UserID"), conn);

            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            //cmd.Parameters.AddWithValue("@Username", Username);
            //cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@Picture", Picture);
            cmd.Parameters.AddWithValue("@HomeEmail", HomeEmail);
            cmd.Parameters.AddWithValue("@HomePhone", HomePhone);
            cmd.Parameters.AddWithValue("@Chapter", Chapter);
            cmd.Parameters.AddWithValue("@UpdatedByID", UpdatedByID);
            cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Today);
            cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);
            cmd.Parameters.AddWithValue("@JobID", JobID);
            cmd.Parameters.AddWithValue("@LocationID", LocationID);
            cmd.Parameters.AddWithValue("@HomeStreet1", HomeStreet1);
            cmd.Parameters.AddWithValue("@HomeStreet2", HomeStreet2);
            cmd.Parameters.AddWithValue("@HomeCity", HomeCity);
            cmd.Parameters.AddWithValue("@HomeState", HomeState);
            cmd.Parameters.AddWithValue("@HomeZip", HomeZip);
            cmd.Parameters.AddWithValue("@HomeCountry", HomeCountry);
            cmd.Parameters.AddWithValue("@MI", MI);
            cmd.Parameters.AddWithValue("@IspID", IspID);
            cmd.Parameters.AddWithValue("@MinistryTitle", MinistryTitle);
            cmd.Parameters.AddWithValue("@MinistryLocation", MinistryLocation);
            cmd.Parameters.AddWithValue("@MinistryCode", MinistryCode);
            cmd.Parameters.AddWithValue("@MinistryClassification", MinistryClassification);
            cmd.Parameters.AddWithValue("@MinistryPhone", MinistryPhone);
            cmd.Parameters.AddWithValue("@MinistryFax", MinistryFax);
            cmd.Parameters.AddWithValue("@MinistryEmail", MinistryEmail);
            cmd.Parameters.AddWithValue("@MinistryStreet1", MinistryStreet1);
            cmd.Parameters.AddWithValue("@MinistryStreet2", MinistryStreet2);
            cmd.Parameters.AddWithValue("@MinistryCity", MinistryCity);
            cmd.Parameters.AddWithValue("@MinistryState", MinistryState);
            cmd.Parameters.AddWithValue("@MinistryCountry", MinistryCountry);
            cmd.Parameters.AddWithValue("@MinistryZip", MinistryZip);
            cmd.Parameters.AddWithValue("@Ministry2Title", Ministry2Title);
            cmd.Parameters.AddWithValue("@Ministry2Location", Ministry2Location);
            cmd.Parameters.AddWithValue("@Ministry2Code", Ministry2Code);
            cmd.Parameters.AddWithValue("@Ministry2Classification", Ministry2Classification);
            cmd.Parameters.AddWithValue("@Ministry2Phone", Ministry2Phone);
            cmd.Parameters.AddWithValue("@Ministry2Fax", Ministry2Fax);
            cmd.Parameters.AddWithValue("@Ministry2Email", Ministry2Email);
            cmd.Parameters.AddWithValue("@Ministry2Street1", Ministry2Street1);
            cmd.Parameters.AddWithValue("@Ministry2Street2", Ministry2Street2);
            cmd.Parameters.AddWithValue("@Ministry2City", Ministry2City);
            cmd.Parameters.AddWithValue("@Ministry2State", Ministry2State);
            cmd.Parameters.AddWithValue("@Ministry2Zip", Ministry2Zip);
            cmd.Parameters.AddWithValue("@Ministry2Country", Ministry2Country);
            //cmd.Parameters.AddWithValue("@FailedLogins", FailedLogins);
            //cmd.Parameters.AddWithValue("@LastPasswordChange", LastPasswordChange);
            cmd.Parameters.AddWithValue("@CompanionType", CompanionType);
            cmd.Parameters.AddWithValue("@ProfessionalYear", ProfessionalYear);
            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
            }
        catch(SqlException ex)
            {
            lastError = translateException(ex);
            disconnect();
            return false;
            }
        catch(Exception ex)
            {
            lastError = ex.Message;
            disconnect();
            return false;
            }
        return true;
        }

    /// <summary>Update the Users table from a DataRow</summary>
    /// <param name="Row">The DataRow to be updated in the Users table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>The DataRow must contain all primary key columns</remarks>
    public bool update(DataRow row)
        {
        SqlCommand cmd;
        String setList = "";

        try
            {
            connect();
            // Build the set list
            foreach(DataColumn col in row.Table.Columns)
                {
                if(!setList.Equals("")) setList += ", ";
                setList += col.ColumnName + " = @" + col.ColumnName;
                }
            cmd = new SqlCommand("UPDATE \"Users\" SET " + setList + " WHERE UserID = @UserID", conn);
            // Create the parameters
            foreach(DataColumn col in row.Table.Columns)
                cmd.Parameters.AddWithValue("@" + col.ColumnName, row[col.ColumnName]);
            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
            }
        catch(SqlException ex)
            {
            lastError = translateException(ex);
            disconnect();
            return false;
            }
        catch(Exception ex)
            {
            lastError = ex.Message;
            disconnect();
            return false;
            }
        return true;
        }

    /// <summary></summary>
    /// <returns>The column names from Users table separated by commas</returns>
    public string ToString()
        {
        string p = "";
        p += Convert.ToString(UserID) + ", ";
        p += LastName + ", ";
        p += FirstName + ", ";
        p += Username + ", ";
        p += Password + ", ";
        p += Picture + ", ";
        p += HomeEmail + ", ";
        p += HomePhone + ", ";
        p += Convert.ToString(Chapter) + ", ";
        p += Convert.ToString(UpdatedByID) + ", ";
        p += ((LastUpdated == null) ? "<null>" : LastUpdated.ToString()) + ", ";
        p += Convert.ToString(DepartmentID) + ", ";
        p += Convert.ToString(JobID) + ", ";
        p += Convert.ToString(LocationID) + ", ";
        p += HomeStreet1 + ", ";
        p += HomeStreet2 + ", ";
        p += HomeCity + ", ";
        p += HomeState + ", ";
        p += HomeZip + ", ";
        p += HomeCountry + ", ";
        p += MI + ", ";
        p += Convert.ToString(IspID) + ", ";
        p += MinistryTitle + ", ";
        p += MinistryLocation + ", ";
        p += MinistryCode + ", ";
        p += MinistryClassification + ", ";
        p += MinistryPhone + ", ";
        p += MinistryFax + ", ";
        p += MinistryEmail + ", ";
        p += MinistryStreet1 + ", ";
        p += MinistryStreet2 + ", ";
        p += MinistryCity + ", ";
        p += MinistryState + ", ";
        p += MinistryCountry + ", ";
        p += MinistryZip + ", ";
        p += Ministry2Title + ", ";
        p += Ministry2Location + ", ";
        p += Ministry2Code + ", ";
        p += Ministry2Classification + ", ";
        p += Ministry2Phone + ", ";
        p += Ministry2Fax + ", ";
        p += Ministry2Email + ", ";
        p += Ministry2Street1 + ", ";
        p += Ministry2Street2 + ", ";
        p += Ministry2City + ", ";
        p += Ministry2State + ", ";
        p += Ministry2Zip + ", ";
        p += Ministry2Country + ", ";
        p += Convert.ToString(FailedLogins) + ", ";
        p += ((LastPasswordChange == null) ? "<null>" : LastPasswordChange.ToString()) + ", ";
        p += CompanionType + ", ";
        p += Convert.ToString(CreatedBy) + ", ";
        p += ((CreatedDate == null) ? "<null>" : CreatedDate.ToString()) + ", ";
        p += Convert.ToString(ProfessionalYear);
        return p;
        }



    public bool QueryExecute(string query)
    {
        SqlCommand cmd;


        try
        {
            connect();
            cmd = new SqlCommand(query, conn);
            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
        }
        catch (SqlException ex)
        {
            lastError = translateException(ex);
            disconnect();
            return false;
        }
        catch (Exception ex)
        {
            lastError = ex.Message;
            disconnect();
            return false;
        }
        return true;
    }

#endregion

#region Private Methods
    private void connect()
        {
        if(connectionString == "")
            {
            if(conn == null) throw new Exception("Database not connected");
            }
        else
            {
            conn = new SqlConnection(connectionString);
            conn.Open();
            }
        }

    private void disconnect()
        {
        if(!connectionString.Equals("") && conn != null)
           {
            try
                {
                conn.Close();
                conn.Dispose();
                conn = null;
                }
            catch(Exception ex) {}
            }
        }

    private string translateException(SqlException ex)
        {
        string p = "";
        foreach(SqlError er in ex.Errors)
            p += er.Message + "\r\n";
        return p;
        }
#endregion

#region User Code
#endregion
}
