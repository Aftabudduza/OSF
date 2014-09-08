using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

public class UserPermissions
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
    public bool CanLogIn;
    public bool IsSister;
    public bool IsStaff;
    public bool IsCompanion;
    public bool IsHidden;
    public bool IsLayPerson;
    public bool IsGlobalAdmin;
    public bool IsGlobalForumAdmin;
    public bool IsGlobalContentAdmin;
    public bool IsChapterContentAdmin;
    public bool IsChapterForumAdmin;
    public bool IsChapterDirectoryAdmin;
    public bool IsDirectoryAdmin;
    public bool IsChapterDirectivesAdmin;
    public bool IsCompanionAdmin;
    public bool IsDelete;

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
    public UserPermissions()
        {
        blank();
        colInfo = new ArrayList();
        colInfo.Add(new ColumnInfo("UserID", "int", 11, true, false));
        colInfo.Add(new ColumnInfo("CanLogIn", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsSister", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsStaff", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsCompanion", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsHidden", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsLayPerson", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsGlobalAdmin", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsGlobalForumAdmin", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsGlobalContentAdmin", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsChapterContentAdmin", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsChapterForumAdmin", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsChapterDirectoryAdmin", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsDirectoryAdmin", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsChapterDirectivesAdmin", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsCompanionAdmin", "bool", 1, true, true));
        colInfo.Add(new ColumnInfo("IsDelete", "bool", 1, true, true));
        colInfo.Sort();
        }

    /// <summary></summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    public UserPermissions(SqlConnection _conn) : this()
        {
        conn = _conn;
        }

    /// <summary></summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    public UserPermissions(string _connectionString) : this()
        {
        connectionString = _connectionString;
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    /// <param name="_UserID">A primary key column in the UserPermissions table</param>
    public UserPermissions(SqlConnection _conn, int _UserID) : this()
        {
        conn = _conn;
        getRecord(_UserID);
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    /// <param name="_UserID">A primary key column in the UserPermissions table</param>
    public UserPermissions(string _connectionString, int _UserID) : this()
        {
        connectionString = _connectionString;
        getRecord(_UserID);
        }
#endregion

#region Methods
    /// <summary>Clear the local column variables associated with a UserPermissions table record</summary>
    public void blank()
        {
        UserID = 0;
        CanLogIn = false;
        IsSister = false;
        IsStaff = false;
        IsCompanion = false;
        IsHidden = false;
        IsLayPerson = false;
        IsGlobalAdmin = false;
        IsGlobalForumAdmin = false;
        IsGlobalContentAdmin = false;
        IsChapterContentAdmin = false;
        IsChapterForumAdmin = false;
        IsChapterDirectoryAdmin = false;
        IsDirectoryAdmin = false;
        IsChapterDirectivesAdmin = false;
        IsCompanionAdmin = false;
        IsDelete = false;
        valid = false;
        }

    /// <summary>Get a DataRow from the UserPermissions table using the primary key</summary>
    /// <param name="_UserID">A primary key column in the UserPermissions table</param>
    /// <returns>A DataRow from the UserPermissions table</returns>
    public DataRow getRow(int _UserID)
        {
        return getRow("*", _UserID);
        }

    /// <summary>Get a DataRow from the UserPermissions table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">A list of column names in the UserPermissions table separated by commas</param>
    /// <param name="_UserID">A primary key column in the UserPermissions table</param>
    /// <returns>A DataRow from the UserPermissions table</returns>
    public DataRow getRow(string columnList, int _UserID)
        {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        try
            {
            connect();
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"UserPermissions\" WHERE UserID = @UserID", conn);
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

    /// <summary>Get a DataRow from the UserPermissions table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An array of column names in the UserPermissions</param>
    /// <param name="_UserID">A primary key column in the UserPermissions table</param>
    /// <returns>A DataRow from the UserPermissions table</returns>
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

    /// <summary>Get a DataRow from the UserPermissions table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the UserPermissions</param>
    /// <param name="_UserID">A primary key column in the UserPermissions table</param>
    /// <returns>A DataRow from the UserPermissions table</returns>
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

    /// <summary>Get a record from the UserPermissions table and populate the local variables</summary>
    /// <param name="_UserID">A primary key column in the UserPermissions table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will not be reflected in local variables</remarks>
    public bool getRecord(int _UserID)
        {
        DataRow dr = getRow(_UserID);

        valid = false;
        if(dr != null)
            {
            UserID = (dr["UserID"] == DBNull.Value ? (int)0 : (int)dr["UserID"]);
            CanLogIn = (dr["CanLogIn"] == DBNull.Value ? (bool)false : (bool)dr["CanLogIn"]);
            IsSister = (dr["IsSister"] == DBNull.Value ? (bool)false : (bool)dr["IsSister"]);
            IsStaff = (dr["IsStaff"] == DBNull.Value ? (bool)false : (bool)dr["IsStaff"]);
            IsCompanion = (dr["IsCompanion"] == DBNull.Value ? (bool)false : (bool)dr["IsCompanion"]);
            IsHidden = (dr["IsHidden"] == DBNull.Value ? (bool)false : (bool)dr["IsHidden"]);
            IsLayPerson = (dr["IsLayPerson"] == DBNull.Value ? (bool)false : (bool)dr["IsLayPerson"]);
            IsGlobalAdmin = (dr["IsGlobalAdmin"] == DBNull.Value ? (bool)false : (bool)dr["IsGlobalAdmin"]);
            IsGlobalForumAdmin = (dr["IsGlobalForumAdmin"] == DBNull.Value ? (bool)false : (bool)dr["IsGlobalForumAdmin"]);
            IsGlobalContentAdmin = (dr["IsGlobalContentAdmin"] == DBNull.Value ? (bool)false : (bool)dr["IsGlobalContentAdmin"]);
            IsChapterContentAdmin = (dr["IsChapterContentAdmin"] == DBNull.Value ? (bool)false : (bool)dr["IsChapterContentAdmin"]);
            IsChapterForumAdmin = (dr["IsChapterForumAdmin"] == DBNull.Value ? (bool)false : (bool)dr["IsChapterForumAdmin"]);
            IsChapterDirectoryAdmin = (dr["IsChapterDirectoryAdmin"] == DBNull.Value ? (bool)false : (bool)dr["IsChapterDirectoryAdmin"]);
            IsDirectoryAdmin = (dr["IsDirectoryAdmin"] == DBNull.Value ? (bool)false : (bool)dr["IsDirectoryAdmin"]);
            IsChapterDirectivesAdmin = (dr["IsChapterDirectivesAdmin"] == DBNull.Value ? (bool)false : (bool)dr["IsChapterDirectivesAdmin"]);
            IsCompanionAdmin = (dr["IsCompanionAdmin"] == DBNull.Value ? (bool)false : (bool)dr["IsCompanionAdmin"]);
            IsDelete = (dr["IsDelete"] == DBNull.Value ? (bool)false : (bool)dr["IsDelete"]);
            valid = true;
            }
        return valid;
        }

    //public bool getRecords(int _UserID)
    //{

    //}
    /// <summary>Delete a row from the UserPermissions table</summary>
    /// <param name="_UserID">A primary key column in the UserPermissions table</param>
    /// <returns>'True', if successful</returns>
    public bool delete(int _UserID)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"UserPermissions\" WHERE UserID = @UserID", conn);
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

    /// <summary>Delete a row from the UserPermissions table</summary>
    /// <param name="Row">The DataRow to be deletedfrom the UserPermissions table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>The DataRow must contain all primary key columns</remarks>
    public bool delete(DataRow row)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"UserPermissions\" WHERE UserID = @UserID", conn);
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

    /// <summary>Delete the current row from the UserPermissions table</summary>
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

    /// <summary>Get rows from the UserPermissions table</summary>
    /// <param name="ColumnList">A list of column names in the UserPermissions table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the UserPermissions table</returns>
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
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"UserPermissions\" WHERE " + whereClause + ob, conn);
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

    /// <summary>Get rows from the UserPermissions table</summary>
    /// <param name="ColumnList">A list of column names in the UserPermissions table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the UserPermissions table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the UserPermissions table</summary>
    /// <param name="ColumnList">An array of column names in the UserPermissions</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the UserPermissions table</returns>
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

    /// <summary>Get a rows from the UserPermissions table</summary>
    /// <param name="ColumnList">An array of column names in the UserPermissions</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the UserPermissions table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string[] columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the UserPermissions table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the UserPermissions</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the UserPermissions table</returns>
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


    /// <summary>Get a rows from the UserPermissions table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the UserPermissions</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the UserPermissions table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(ArrayList columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }


    /// <summary>Insert a record in the UserPermissions table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    public bool insert()
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("INSERT INTO \"UserPermissions\" (UserID, CanLogIn, IsSister, IsStaff, IsCompanion, IsHidden, IsLayPerson, IsGlobalAdmin, IsGlobalForumAdmin, IsGlobalContentAdmin, IsChapterContentAdmin, IsChapterForumAdmin, IsChapterDirectoryAdmin, IsDirectoryAdmin, IsChapterDirectivesAdmin, IsCompanionAdmin, IsDelete) VALUES (@UserID, @CanLogIn, @IsSister, @IsStaff, @IsCompanion, @IsHidden, @IsLayPerson, @IsGlobalAdmin, @IsGlobalForumAdmin, @IsGlobalContentAdmin, @IsChapterContentAdmin, @IsChapterForumAdmin, @IsChapterDirectoryAdmin, @IsDirectoryAdmin, @IsChapterDirectivesAdmin, @IsCompanionAdmin, @IsDelete)", conn);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@CanLogIn", CanLogIn);
            cmd.Parameters.AddWithValue("@IsSister", IsSister);
            cmd.Parameters.AddWithValue("@IsStaff", IsStaff);
            cmd.Parameters.AddWithValue("@IsCompanion", IsCompanion);
            cmd.Parameters.AddWithValue("@IsHidden", IsHidden);
            cmd.Parameters.AddWithValue("@IsLayPerson", IsLayPerson);
            cmd.Parameters.AddWithValue("@IsGlobalAdmin", IsGlobalAdmin);
            cmd.Parameters.AddWithValue("@IsGlobalForumAdmin", IsGlobalForumAdmin);
            cmd.Parameters.AddWithValue("@IsGlobalContentAdmin", IsGlobalContentAdmin);
            cmd.Parameters.AddWithValue("@IsChapterContentAdmin", IsChapterContentAdmin);
            cmd.Parameters.AddWithValue("@IsChapterForumAdmin", IsChapterForumAdmin);
            cmd.Parameters.AddWithValue("@IsChapterDirectoryAdmin", IsChapterDirectoryAdmin);
            cmd.Parameters.AddWithValue("@IsDirectoryAdmin", IsDirectoryAdmin);
            cmd.Parameters.AddWithValue("@IsChapterDirectivesAdmin", IsChapterDirectivesAdmin);
            cmd.Parameters.AddWithValue("@IsCompanionAdmin", IsCompanionAdmin);
            cmd.Parameters.AddWithValue("@IsDelete", IsDelete);
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

    /// <summary>Insert a record in the UserPermissions table from a DataRow</summary>
    /// <param name="Row">The DataRow to be inserted in the UserPermissions table</param>
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
            cmd = new SqlCommand("INSERT INTO \"UserPermissions\" (" + setList + ") VALUES (" + valList + ")", conn);
            // Create the parameters
            foreach(DataColumn col in row.Table.Columns)
                {
                switch(col.ColumnName.ToLower())
                    {
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

    /// <summary>Update a record in the UserPermissions table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will be replaced by blanks and zeros</remarks>
    public bool update()
        {
        SqlCommand cmd;


        try
            {
            connect();
            cmd = new SqlCommand("UPDATE \"UserPermissions\" SET CanLogIn = @CanLogIn, IsSister = @IsSister, IsStaff = @IsStaff, IsCompanion = @IsCompanion, IsHidden = @IsHidden, IsLayPerson = @IsLayPerson, IsGlobalAdmin = @IsGlobalAdmin, IsGlobalForumAdmin = @IsGlobalForumAdmin, IsGlobalContentAdmin = @IsGlobalContentAdmin, IsChapterContentAdmin = @IsChapterContentAdmin, IsChapterForumAdmin = @IsChapterForumAdmin, IsChapterDirectoryAdmin = @IsChapterDirectoryAdmin, IsDirectoryAdmin = @IsDirectoryAdmin, IsChapterDirectivesAdmin = @IsChapterDirectivesAdmin, IsCompanionAdmin = @IsCompanionAdmin, IsDelete = @IsDelete WHERE UserID = @UserID", conn);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@CanLogIn", CanLogIn);
            cmd.Parameters.AddWithValue("@IsSister", IsSister);
            cmd.Parameters.AddWithValue("@IsStaff", IsStaff);
            cmd.Parameters.AddWithValue("@IsCompanion", IsCompanion);
            cmd.Parameters.AddWithValue("@IsHidden", IsHidden);
            cmd.Parameters.AddWithValue("@IsLayPerson", IsLayPerson);
            cmd.Parameters.AddWithValue("@IsGlobalAdmin", IsGlobalAdmin);
            cmd.Parameters.AddWithValue("@IsGlobalForumAdmin", IsGlobalForumAdmin);
            cmd.Parameters.AddWithValue("@IsGlobalContentAdmin", IsGlobalContentAdmin);
            cmd.Parameters.AddWithValue("@IsChapterContentAdmin", IsChapterContentAdmin);
            cmd.Parameters.AddWithValue("@IsChapterForumAdmin", IsChapterForumAdmin);
            cmd.Parameters.AddWithValue("@IsChapterDirectoryAdmin", IsChapterDirectoryAdmin);
            cmd.Parameters.AddWithValue("@IsDirectoryAdmin", IsDirectoryAdmin);
            cmd.Parameters.AddWithValue("@IsChapterDirectivesAdmin", IsChapterDirectivesAdmin);
            cmd.Parameters.AddWithValue("@IsCompanionAdmin", IsCompanionAdmin);
            cmd.Parameters.AddWithValue("@IsDelete", IsDelete);
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

    /// <summary>Update the UserPermissions table from a DataRow</summary>
    /// <param name="Row">The DataRow to be updated in the UserPermissions table</param>
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
            cmd = new SqlCommand("UPDATE \"UserPermissions\" SET " + setList + " WHERE UserID = @UserID", conn);
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
    /// <returns>The column names from UserPermissions table separated by commas</returns>
    public string ToString()
        {
        string p = "";
        p += Convert.ToString(UserID) + ", ";
        p += Convert.ToString(CanLogIn) + ", ";
        p += Convert.ToString(IsSister) + ", ";
        p += Convert.ToString(IsStaff) + ", ";
        p += Convert.ToString(IsCompanion) + ", ";
        p += Convert.ToString(IsHidden) + ", ";
        p += Convert.ToString(IsLayPerson) + ", ";
        p += Convert.ToString(IsGlobalAdmin) + ", ";
        p += Convert.ToString(IsGlobalForumAdmin) + ", ";
        p += Convert.ToString(IsGlobalContentAdmin) + ", ";
        p += Convert.ToString(IsChapterContentAdmin) + ", ";
        p += Convert.ToString(IsChapterForumAdmin) + ", ";
        p += Convert.ToString(IsChapterDirectoryAdmin) + ", ";
        p += Convert.ToString(IsDirectoryAdmin) + ", ";
        p += Convert.ToString(IsChapterDirectivesAdmin) + ", ";
        p += Convert.ToString(IsCompanionAdmin) + ", ";
        p += Convert.ToString(IsDelete);
        return p;
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
