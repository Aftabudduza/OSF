using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

public class ContactListMembers
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
    public int ContactListID;
    public int UserID;
    public int BasicDataID;
    public bool IsSelected;

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
            if (obj.GetType() == this.GetType())
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
    public ContactListMembers()
    {
        blank();
        colInfo = new ArrayList();
        colInfo.Add(new ColumnInfo("ContactListID", "int", 11, false, false));
        colInfo.Add(new ColumnInfo("UserID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("BasicDataID", "int", 11, true, true));
        colInfo.Add(new ColumnInfo("IsSelected", "bool", 1, true, true));
        colInfo.Sort();
    }

    /// <summary></summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    public ContactListMembers(SqlConnection _conn)
        : this()
    {
        conn = _conn;
    }

    /// <summary></summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    public ContactListMembers(string _connectionString)
        : this()
    {
        connectionString = _connectionString;
    }

    /// <summary>Construct and get a record</summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    /// <param name="_ContactListID">A primary key column in the ContactListMembers table</param>
    public ContactListMembers(SqlConnection _conn, int _ContactListID)
        : this()
    {
        conn = _conn;
        getRecord(_ContactListID);
    }

    /// <summary>Construct and get a record</summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    /// <param name="_ContactListID">A primary key column in the ContactListMembers table</param>
    public ContactListMembers(string _connectionString, int _ContactListID)
        : this()
    {
        connectionString = _connectionString;
        getRecord(_ContactListID);
    }
    #endregion

    #region Methods
    /// <summary>Clear the local column variables associated with a ContactListMembers table record</summary>
    public void blank()
    {
        ContactListID = 0;
        UserID = 0;
        BasicDataID = 0;
        IsSelected = true;
        valid = false;
    }

    /// <summary>Get a DataRow from the ContactListMembers table using the primary key</summary>
    /// <param name="_ContactListID">A primary key column in the ContactListMembers table</param>
    /// <returns>A DataRow from the ContactListMembers table</returns>
    public DataRow getRow(int _ContactListID)
    {
        return getRow("*", _ContactListID);
    }

    /// <summary>Get a DataRow from the ContactListMembers table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">A list of column names in the ContactListMembers table separated by commas</param>
    /// <param name="_ContactListID">A primary key column in the ContactListMembers table</param>
    /// <returns>A DataRow from the ContactListMembers table</returns>
    public DataRow getRow(string columnList, int _ContactListID)
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        try
        {
            connect();
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"ContactListMembers\" WHERE ContactListID = @ContactListID", conn);
            cmd.Parameters.AddWithValue("@ContactListID", _ContactListID);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            disconnect();
            valid = true;
            if (dt.Rows.Count > 0) return dt.Rows[0];
            lastError = "No record found";
        }
        catch (SqlException ex)
        {
            lastError = translateException(ex);
        }
        catch (Exception ex)
        {
            lastError = ex.Message;
        }
        valid = false;
        return null;
    }

    /// <summary>Get a DataRow from the ContactListMembers table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An array of column names in the ContactListMembers</param>
    /// <param name="_ContactListID">A primary key column in the ContactListMembers table</param>
    /// <returns>A DataRow from the ContactListMembers table</returns>
    public DataRow getRow(string[] columnList, int _ContactListID)
    {
        string cl = "";

        foreach (string p in columnList)
        {
            if (!cl.Equals("")) cl += ", ";
            cl += p;
        }
        return getRow(cl, _ContactListID);
    }

    /// <summary>Get a DataRow from the ContactListMembers table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the ContactListMembers</param>
    /// <param name="_ContactListID">A primary key column in the ContactListMembers table</param>
    /// <returns>A DataRow from the ContactListMembers table</returns>
    public DataRow getRow(ArrayList columnList, int _ContactListID)
    {
        String cl = "";

        foreach (string p in columnList)
        {
            if (!cl.Equals("")) cl += ", ";
            cl += p;
        }
        return getRow(cl, _ContactListID);
    }

    /// <summary>Get a record from the ContactListMembers table and populate the local variables</summary>
    /// <param name="_ContactListID">A primary key column in the ContactListMembers table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will not be reflected in local variables</remarks>
    public bool getRecord(int _ContactListID)
    {
        DataRow dr = getRow(_ContactListID);

        valid = false;
        if (dr != null)
        {
            ContactListID = (dr["ContactListID"] == DBNull.Value ? (int)0 : (int)dr["ContactListID"]);
            UserID = (dr["UserID"] == DBNull.Value ? (int)0 : (int)dr["UserID"]);
            BasicDataID = (dr["BasicDataID"] == DBNull.Value ? (int)0 : (int)dr["BasicDataID"]);
            IsSelected = (dr["IsSelected"] == DBNull.Value ? true : (bool)dr["IsSelected"]);
            valid = true;
        }
        return valid;
    }

    /// <summary>Delete a row from the ContactListMembers table</summary>
    /// <param name="_ContactListID">A primary key column in the ContactListMembers table</param>
    /// <returns>'True', if successful</returns>
    public bool delete(int _ContactListID)
    {
        SqlCommand cmd;

        try
        {
            connect();
            cmd = new SqlCommand("DELETE FROM \"ContactListMembers\" WHERE ContactListID = @ContactListID", conn);
            cmd.Parameters.AddWithValue("@ContactListID", _ContactListID);
            cmd.ExecuteScalar();
            cmd.Dispose();
            disconnect();
        }
        catch (SqlException ex)
        {
            disconnect();
            lastError = translateException(ex);
            return false;
        }
        catch (Exception ex)
        {
            disconnect();
            lastError = ex.Message;
            return false;
        }
        blank();
        return true;
    }

    /// <summary>Delete a row from the ContactListMembers table</summary>
    /// <param name="Row">The DataRow to be deletedfrom the ContactListMembers table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>The DataRow must contain all primary key columns</remarks>
    public bool delete(DataRow row)
    {
        SqlCommand cmd;

        try
        {
            connect();
            cmd = new SqlCommand("DELETE FROM \"ContactListMembers\" WHERE ContactListID = @ContactListID", conn);
            cmd.Parameters.AddWithValue("@ContactListID", row["ContactListID"]);
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

    /// <summary>Delete the current row from the ContactListMembers table</summary>
    /// <returns>'True', if successful</returns>
    public bool delete()
    {
        if (!valid)
        {
            lastError = "A valid current record is needed to delete";
            return false;
        }
        return delete(ContactListID);
    }

    /// <summary>Get rows from the ContactListMembers table</summary>
    /// <param name="ColumnList">A list of column names in the ContactListMembers table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the ContactListMembers table</returns>
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
            if (!orderBy.Equals("")) ob = " ORDER BY " + ob;
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"ContactListMembers\" WHERE " + whereClause + ob, conn);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            disconnect();
            return dt;
        }
        catch (SqlException ex)
        {
            lastError = translateException(ex);
        }
        catch (Exception ex)
        {
            lastError = ex.Message;
        }
        return null;
    }

    /// <summary>Get rows from the ContactListMembers table</summary>
    /// <param name="ColumnList">A list of column names in the ContactListMembers table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the ContactListMembers table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string columnList, string whereClause)
    {
        return getRows(columnList, whereClause, "");
    }

    /// <summary>Get a rows from the ContactListMembers table</summary>
    /// <param name="ColumnList">An array of column names in the ContactListMembers</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the ContactListMembers table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string[] columnList, string whereClause, string orderBy)
    {
        string cl = "";

        foreach (string p in columnList)
        {
            if (!cl.Equals("")) cl += ", ";
            cl += p;
        }
        return getRows(cl, whereClause, orderBy);
    }

    /// <summary>Get a rows from the ContactListMembers table</summary>
    /// <param name="ColumnList">An array of column names in the ContactListMembers</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the ContactListMembers table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string[] columnList, string whereClause)
    {
        return getRows(columnList, whereClause, "");
    }

    /// <summary>Get a rows from the ContactListMembers table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the ContactListMembers</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the ContactListMembers table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(ArrayList columnList, string whereClause, string orderBy)
    {
        string cl = "";

        foreach (string p in columnList)
        {
            if (!cl.Equals("")) cl += ", ";
            cl += p;
        }
        return getRows(cl, whereClause, orderBy);
    }


    /// <summary>Get a rows from the ContactListMembers table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the ContactListMembers</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the ContactListMembers table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(ArrayList columnList, string whereClause)
    {
        return getRows(columnList, whereClause, "");
    }


    /// <summary>Insert a record in the ContactListMembers table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    public bool insert()
    {
        SqlCommand cmd;

        try
        {
            connect();
            cmd = new SqlCommand("INSERT INTO \"ContactListMembers\" (UserID, BasicDataID, IsSelected) VALUES (@UserID, @BasicDataID, @IsSelected)", conn);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@BasicDataID", BasicDataID);
            cmd.Parameters.AddWithValue("@IsSelected", IsSelected);
            cmd.ExecuteScalar();
            cmd.Dispose();

            // Attempt to load the auto-generated Id. This is not fool-proof.
            SqlDataAdapter da;
            DataTable dt = new DataTable();

            cmd = new SqlCommand("SELECT TOP 1 ContactListID FROM \"ContactListMembers\" WHERE UserID = @UserIDBasicDataID = @BasicDataIDIsSelected = @IsSelected ORDER BY ContactListID DESC", conn);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@BasicDataID", BasicDataID);
            cmd.Parameters.AddWithValue("@IsSelected", IsSelected);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ContactListID = (int)dt.Rows[0]["ContactListID"];
            }
            dt.Dispose();
            da.Dispose();
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

    /// <summary>Insert a record in the ContactListMembers table from a DataRow</summary>
    /// <param name="Row">The DataRow to be inserted in the ContactListMembers table</param>
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
            foreach (DataColumn col in row.Table.Columns)
            {
                switch (col.ColumnName.ToLower())
                {
                    case "contactlistid":
                        break;
                    default:
                        if (!setList.Equals(""))
                        {
                            setList += ", ";
                            valList += ", ";
                        }
                        setList += col.ColumnName;
                        valList += " @" + col.ColumnName;
                        break;
                }
            }
            cmd = new SqlCommand("INSERT INTO \"ContactListMembers\" (" + setList + ") VALUES (" + valList + ")", conn);
            // Create the parameters
            foreach (DataColumn col in row.Table.Columns)
            {
                switch (col.ColumnName.ToLower())
                {
                    case "ContactListID":
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

    /// <summary>Update a record in the ContactListMembers table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will be replaced by blanks and zeros</remarks>
    public bool update()
    {
        SqlCommand cmd;

        if (!valid)
        {
            lastError = "A valid current record is needed to update";
            return false;
        }
        try
        {
            connect();
            cmd = new SqlCommand("UPDATE \"ContactListMembers\" SET UserID = @UserID, BasicDataID = @BasicDataID, IsSelected = @IsSelected WHERE ContactListID = @ContactListID", conn);
            cmd.Parameters.AddWithValue("@ContactListID", ContactListID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@BasicDataID", BasicDataID);
            cmd.Parameters.AddWithValue("@IsSelected", IsSelected);
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

    /// <summary>Update the ContactListMembers table from a DataRow</summary>
    /// <param name="Row">The DataRow to be updated in the ContactListMembers table</param>
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
            foreach (DataColumn col in row.Table.Columns)
            {
                if (!setList.Equals("")) setList += ", ";
                setList += col.ColumnName + " = @" + col.ColumnName;
            }
            cmd = new SqlCommand("UPDATE \"ContactListMembers\" SET " + setList + " WHERE ContactListID = @ContactListID", conn);
            // Create the parameters
            foreach (DataColumn col in row.Table.Columns)
                cmd.Parameters.AddWithValue("@" + col.ColumnName, row[col.ColumnName]);
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

    /// <summary></summary>
    /// <returns>The column names from ContactListMembers table separated by commas</returns>
    public string ToString()
    {
        string p = "";
        p += Convert.ToString(ContactListID) + ", ";
        p += Convert.ToString(UserID) + ", ";
        p += Convert.ToString(BasicDataID) + ", ";
        p += Convert.ToString(IsSelected);
        return p;
    }

    #endregion

    #region Private Methods
    private void connect()
    {
        if (connectionString == "")
        {
            if (conn == null) throw new Exception("Database not connected");
        }
        else
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
        }
    }

    private void disconnect()
    {
        if (!connectionString.Equals("") && conn != null)
        {
            try
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            catch (Exception ex) { }
        }
    }

    private string translateException(SqlException ex)
    {
        string p = "";
        foreach (SqlError er in ex.Errors)
            p += er.Message + "\r\n";
        return p;
    }
    #endregion

    #region User Code
    #endregion
}
