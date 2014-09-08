using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

public class REFCOUNTRIES
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
    public string COUNTRY;
    public string COUNTRYNAME;
    public double ShippingSurcharge;

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
    public REFCOUNTRIES()
        {
        blank();
        colInfo = new ArrayList();
        colInfo.Add(new ColumnInfo("COUNTRY", "string", 3, true, false));
        colInfo.Add(new ColumnInfo("COUNTRYNAME", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("ShippingSurcharge", "double", 15, true, true));
        colInfo.Sort();
        }

    /// <summary></summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    public REFCOUNTRIES(SqlConnection _conn) : this()
        {
        conn = _conn;
        }

    /// <summary></summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    public REFCOUNTRIES(string _connectionString) : this()
        {
        connectionString = _connectionString;
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    /// <param name="_COUNTRY">A primary key column in the REFCOUNTRIES table</param>
    public REFCOUNTRIES(SqlConnection _conn, string _COUNTRY) : this()
        {
        conn = _conn;
        getRecord(_COUNTRY);
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    /// <param name="_COUNTRY">A primary key column in the REFCOUNTRIES table</param>
    public REFCOUNTRIES(string _connectionString, string _COUNTRY) : this()
        {
        connectionString = _connectionString;
        getRecord(_COUNTRY);
        }
#endregion

#region Methods
    /// <summary>Clear the local column variables associated with a REFCOUNTRIES table record</summary>
    public void blank()
        {
        COUNTRY = "";
        COUNTRYNAME = "";
        ShippingSurcharge = 0;
        valid = false;
        }

    /// <summary>Get a DataRow from the REFCOUNTRIES table using the primary key</summary>
    /// <param name="_COUNTRY">A primary key column in the REFCOUNTRIES table</param>
    /// <returns>A DataRow from the REFCOUNTRIES table</returns>
    public DataRow getRow(string _COUNTRY)
        {
        return getRow("*", _COUNTRY);
        }

    /// <summary>Get a DataRow from the REFCOUNTRIES table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">A list of column names in the REFCOUNTRIES table separated by commas</param>
    /// <param name="_COUNTRY">A primary key column in the REFCOUNTRIES table</param>
    /// <returns>A DataRow from the REFCOUNTRIES table</returns>
    public DataRow getRow(string columnList, string _COUNTRY)
        {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        try
            {
            connect();
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"REFCOUNTRIES\" WHERE COUNTRY = @COUNTRY", conn);
            cmd.Parameters.AddWithValue("@COUNTRY", _COUNTRY);
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

    /// <summary>Get a DataRow from the REFCOUNTRIES table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An array of column names in the REFCOUNTRIES</param>
    /// <param name="_COUNTRY">A primary key column in the REFCOUNTRIES table</param>
    /// <returns>A DataRow from the REFCOUNTRIES table</returns>
    public DataRow getRow(string[] columnList, string _COUNTRY)
        {
        string cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRow(cl, _COUNTRY);
        }

    /// <summary>Get a DataRow from the REFCOUNTRIES table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the REFCOUNTRIES</param>
    /// <param name="_COUNTRY">A primary key column in the REFCOUNTRIES table</param>
    /// <returns>A DataRow from the REFCOUNTRIES table</returns>
    public DataRow getRow(ArrayList columnList, string _COUNTRY)
        {
        String cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRow(cl, _COUNTRY);
        }

    /// <summary>Get a record from the REFCOUNTRIES table and populate the local variables</summary>
    /// <param name="_COUNTRY">A primary key column in the REFCOUNTRIES table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will not be reflected in local variables</remarks>
    public bool getRecord(string _COUNTRY)
        {
        DataRow dr = getRow(_COUNTRY);

        valid = false;
        if(dr != null)
            {
            COUNTRY = (dr["COUNTRY"] == DBNull.Value ? (string)"" : (string)dr["COUNTRY"]);
            COUNTRYNAME = (dr["COUNTRYNAME"] == DBNull.Value ? (string)"" : (string)dr["COUNTRYNAME"]);
            ShippingSurcharge = (dr["ShippingSurcharge"] == DBNull.Value ? (double)0 : Convert.ToDouble(dr["ShippingSurcharge"]));
            valid = true;
            }
        return valid;
        }

    /// <summary>Delete a row from the REFCOUNTRIES table</summary>
    /// <param name="_COUNTRY">A primary key column in the REFCOUNTRIES table</param>
    /// <returns>'True', if successful</returns>
    public bool delete(string _COUNTRY)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"REFCOUNTRIES\" WHERE COUNTRY = @COUNTRY", conn);
            cmd.Parameters.AddWithValue("@COUNTRY", _COUNTRY);
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

    /// <summary>Delete a row from the REFCOUNTRIES table</summary>
    /// <param name="Row">The DataRow to be deletedfrom the REFCOUNTRIES table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>The DataRow must contain all primary key columns</remarks>
    public bool delete(DataRow row)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"REFCOUNTRIES\" WHERE COUNTRY = @COUNTRY", conn);
            cmd.Parameters.AddWithValue("@COUNTRY", row["COUNTRY"]);
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

    /// <summary>Delete the current row from the REFCOUNTRIES table</summary>
    /// <returns>'True', if successful</returns>
    public bool delete()
        {
        if(!valid)
           {
           lastError = "A valid current record is needed to delete";
           return false;
           }
        return delete(COUNTRY);
        }

    /// <summary>Get rows from the REFCOUNTRIES table</summary>
    /// <param name="ColumnList">A list of column names in the REFCOUNTRIES table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the REFCOUNTRIES table</returns>
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
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"REFCOUNTRIES\" WHERE " + whereClause + ob, conn);
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

    /// <summary>Get rows from the REFCOUNTRIES table</summary>
    /// <param name="ColumnList">A list of column names in the REFCOUNTRIES table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the REFCOUNTRIES table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the REFCOUNTRIES table</summary>
    /// <param name="ColumnList">An array of column names in the REFCOUNTRIES</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the REFCOUNTRIES table</returns>
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

    /// <summary>Get a rows from the REFCOUNTRIES table</summary>
    /// <param name="ColumnList">An array of column names in the REFCOUNTRIES</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the REFCOUNTRIES table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string[] columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the REFCOUNTRIES table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the REFCOUNTRIES</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the REFCOUNTRIES table</returns>
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


    /// <summary>Get a rows from the REFCOUNTRIES table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the REFCOUNTRIES</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the REFCOUNTRIES table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(ArrayList columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }


    /// <summary>Insert a record in the REFCOUNTRIES table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    public bool insert()
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("INSERT INTO \"REFCOUNTRIES\" (COUNTRY, COUNTRYNAME, ShippingSurcharge) VALUES (@COUNTRY, @COUNTRYNAME, @ShippingSurcharge)", conn);
            cmd.Parameters.AddWithValue("@COUNTRY", COUNTRY);
            cmd.Parameters.AddWithValue("@COUNTRYNAME", COUNTRYNAME);
            cmd.Parameters.AddWithValue("@ShippingSurcharge", ShippingSurcharge);
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

    /// <summary>Insert a record in the REFCOUNTRIES table from a DataRow</summary>
    /// <param name="Row">The DataRow to be inserted in the REFCOUNTRIES table</param>
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
            cmd = new SqlCommand("INSERT INTO \"REFCOUNTRIES\" (" + setList + ") VALUES (" + valList + ")", conn);
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

    /// <summary>Update a record in the REFCOUNTRIES table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will be replaced by blanks and zeros</remarks>
    public bool update()
        {
        SqlCommand cmd;

        if(!valid)
            {
            lastError = "A valid current record is needed to update";
            return false;
            }
        try
            {
            connect();
            cmd = new SqlCommand("UPDATE \"REFCOUNTRIES\" SET COUNTRYNAME = @COUNTRYNAME, ShippingSurcharge = @ShippingSurcharge WHERE COUNTRY = @COUNTRY", conn);
            cmd.Parameters.AddWithValue("@COUNTRY", COUNTRY);
            cmd.Parameters.AddWithValue("@COUNTRYNAME", COUNTRYNAME);
            cmd.Parameters.AddWithValue("@ShippingSurcharge", ShippingSurcharge);
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

    /// <summary>Update the REFCOUNTRIES table from a DataRow</summary>
    /// <param name="Row">The DataRow to be updated in the REFCOUNTRIES table</param>
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
            cmd = new SqlCommand("UPDATE \"REFCOUNTRIES\" SET " + setList + " WHERE COUNTRY = @COUNTRY", conn);
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
    /// <returns>The column names from REFCOUNTRIES table separated by commas</returns>
    public string ToString()
        {
        string p = "";
        p += COUNTRY + ", ";
        p += COUNTRYNAME + ", ";
        p += Convert.ToString(ShippingSurcharge);
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
