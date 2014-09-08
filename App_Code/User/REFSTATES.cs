using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

public class REFSTATES
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
    public string STATE;
    public string STATENAME;
    public double SalesTaxRate;
    public string FreightTaxable;
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
    public REFSTATES()
        {
        blank();
        colInfo = new ArrayList();
        colInfo.Add(new ColumnInfo("STATE", "string", 2, true, false));
        colInfo.Add(new ColumnInfo("STATENAME", "string", 50, true, true));
        colInfo.Add(new ColumnInfo("SalesTaxRate", "double", 15, true, true));
        colInfo.Add(new ColumnInfo("FreightTaxable", "string", 1, true, true));
        colInfo.Add(new ColumnInfo("ShippingSurcharge", "double", 15, true, true));
        colInfo.Sort();
        }

    /// <summary></summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    public REFSTATES(SqlConnection _conn) : this()
        {
        conn = _conn;
        }

    /// <summary></summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    public REFSTATES(string _connectionString) : this()
        {
        connectionString = _connectionString;
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_Conn">Database connection object to be used in further operations</param>
    /// <param name="_STATE">A primary key column in the REFSTATES table</param>
    public REFSTATES(SqlConnection _conn, string _STATE) : this()
        {
        conn = _conn;
        getRecord(_STATE);
        }

    /// <summary>Construct and get a record</summary>
    /// <param name="_ConnectionString">Database connection string to be used in further operations</param>
    /// <param name="_STATE">A primary key column in the REFSTATES table</param>
    public REFSTATES(string _connectionString, string _STATE) : this()
        {
        connectionString = _connectionString;
        getRecord(_STATE);
        }
#endregion

#region Methods
    /// <summary>Clear the local column variables associated with a REFSTATES table record</summary>
    public void blank()
        {
        STATE = "";
        STATENAME = "";
        SalesTaxRate = 0;
        FreightTaxable = "N";
        ShippingSurcharge = 0;
        valid = false;
        }

    /// <summary>Get a DataRow from the REFSTATES table using the primary key</summary>
    /// <param name="_STATE">A primary key column in the REFSTATES table</param>
    /// <returns>A DataRow from the REFSTATES table</returns>
    public DataRow getRow(string _STATE)
        {
        return getRow("*", _STATE);
        }

    /// <summary>Get a DataRow from the REFSTATES table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">A list of column names in the REFSTATES table separated by commas</param>
    /// <param name="_STATE">A primary key column in the REFSTATES table</param>
    /// <returns>A DataRow from the REFSTATES table</returns>
    public DataRow getRow(string columnList, string _STATE)
        {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        try
            {
            connect();
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"REFSTATES\" WHERE STATE = @STATE", conn);
            cmd.Parameters.AddWithValue("@STATE", _STATE);
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

    /// <summary>Get a DataRow from the REFSTATES table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An array of column names in the REFSTATES</param>
    /// <param name="_STATE">A primary key column in the REFSTATES table</param>
    /// <returns>A DataRow from the REFSTATES table</returns>
    public DataRow getRow(string[] columnList, string _STATE)
        {
        string cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRow(cl, _STATE);
        }

    /// <summary>Get a DataRow from the REFSTATES table using the primary key, specifying only selected columns</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the REFSTATES</param>
    /// <param name="_STATE">A primary key column in the REFSTATES table</param>
    /// <returns>A DataRow from the REFSTATES table</returns>
    public DataRow getRow(ArrayList columnList, string _STATE)
        {
        String cl = "";

        foreach(string p in columnList)
            {
            if(!cl.Equals("")) cl += ", ";
            cl += p;
            }
        return getRow(cl, _STATE);
        }

    /// <summary>Get a record from the REFSTATES table and populate the local variables</summary>
    /// <param name="_STATE">A primary key column in the REFSTATES table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>Nulls in the data record will not be reflected in local variables</remarks>
    public bool getRecord(string _STATE)
        {
        DataRow dr = getRow(_STATE);

        valid = false;
        if(dr != null)
            {
            STATE = (dr["STATE"] == DBNull.Value ? (string)"" : (string)dr["STATE"]);
            STATENAME = (dr["STATENAME"] == DBNull.Value ? (string)"" : (string)dr["STATENAME"]);
            SalesTaxRate = (dr["SalesTaxRate"] == DBNull.Value ? (double)0 : Convert.ToDouble(dr["SalesTaxRate"]));
            FreightTaxable = (dr["FreightTaxable"] == DBNull.Value ? (string)"N" : (string)dr["FreightTaxable"]);
            ShippingSurcharge = (dr["ShippingSurcharge"] == DBNull.Value ? (double)0 : Convert.ToDouble(dr["ShippingSurcharge"]));
            valid = true;
            }
        return valid;
        }

    /// <summary>Delete a row from the REFSTATES table</summary>
    /// <param name="_STATE">A primary key column in the REFSTATES table</param>
    /// <returns>'True', if successful</returns>
    public bool delete(string _STATE)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"REFSTATES\" WHERE STATE = @STATE", conn);
            cmd.Parameters.AddWithValue("@STATE", _STATE);
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

    /// <summary>Delete a row from the REFSTATES table</summary>
    /// <param name="Row">The DataRow to be deletedfrom the REFSTATES table</param>
    /// <returns>'True', if successful</returns>
    /// <remarks>The DataRow must contain all primary key columns</remarks>
    public bool delete(DataRow row)
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("DELETE FROM \"REFSTATES\" WHERE STATE = @STATE", conn);
            cmd.Parameters.AddWithValue("@STATE", row["STATE"]);
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

    /// <summary>Delete the current row from the REFSTATES table</summary>
    /// <returns>'True', if successful</returns>
    public bool delete()
        {
        if(!valid)
           {
           lastError = "A valid current record is needed to delete";
           return false;
           }
        return delete(STATE);
        }

    /// <summary>Get rows from the REFSTATES table</summary>
    /// <param name="ColumnList">A list of column names in the REFSTATES table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the REFSTATES table</returns>
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
            cmd = new SqlCommand("SELECT " + columnList + " FROM \"REFSTATES\" WHERE " + whereClause + ob, conn);
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

    /// <summary>Get rows from the REFSTATES table</summary>
    /// <param name="ColumnList">A list of column names in the REFSTATES table separated by commas</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the REFSTATES table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the REFSTATES table</summary>
    /// <param name="ColumnList">An array of column names in the REFSTATES</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the REFSTATES table</returns>
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

    /// <summary>Get a rows from the REFSTATES table</summary>
    /// <param name="ColumnList">An array of column names in the REFSTATES</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the REFSTATES table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(string[] columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }

    /// <summary>Get a rows from the REFSTATES table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the REFSTATES</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <param name="OrderBy">An SQL ORDER BY clause for a SELECT statement</param>
    /// <returns>A DataTable from the REFSTATES table</returns>
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


    /// <summary>Get a rows from the REFSTATES table</summary>
    /// <param name="ColumnList">An ArrayList of Strings representing column names in the REFSTATES</param>
    /// <param name="WhereClause">An SQL WHERE clause for a SELECT statement that may return multiple rows</param>
    /// <returns>A DataTable from the REFSTATES table</returns>
    /// <remarks>Use care with web forms since the resulting SELECT statement is not parameterized</remarks>
    public DataTable getRows(ArrayList columnList, string whereClause)
        {
        return getRows(columnList, whereClause, "");
        }


    /// <summary>Insert a record in the REFSTATES table from the data stored in the local variables</summary>
    /// <returns>'True', if successful</returns>
    public bool insert()
        {
        SqlCommand cmd;

        try
            {
            connect();
            cmd = new SqlCommand("INSERT INTO \"REFSTATES\" (STATE, STATENAME, SalesTaxRate, FreightTaxable, ShippingSurcharge) VALUES (@STATE, @STATENAME, @SalesTaxRate, @FreightTaxable, @ShippingSurcharge)", conn);
            cmd.Parameters.AddWithValue("@STATE", STATE);
            cmd.Parameters.AddWithValue("@STATENAME", STATENAME);
            cmd.Parameters.AddWithValue("@SalesTaxRate", SalesTaxRate);
            cmd.Parameters.AddWithValue("@FreightTaxable", FreightTaxable);
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

    /// <summary>Insert a record in the REFSTATES table from a DataRow</summary>
    /// <param name="Row">The DataRow to be inserted in the REFSTATES table</param>
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
            cmd = new SqlCommand("INSERT INTO \"REFSTATES\" (" + setList + ") VALUES (" + valList + ")", conn);
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

    /// <summary>Update a record in the REFSTATES table from the data stored in the local variables</summary>
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
            cmd = new SqlCommand("UPDATE \"REFSTATES\" SET STATENAME = @STATENAME, SalesTaxRate = @SalesTaxRate, FreightTaxable = @FreightTaxable, ShippingSurcharge = @ShippingSurcharge WHERE STATE = @STATE", conn);
            cmd.Parameters.AddWithValue("@STATE", STATE);
            cmd.Parameters.AddWithValue("@STATENAME", STATENAME);
            cmd.Parameters.AddWithValue("@SalesTaxRate", SalesTaxRate);
            cmd.Parameters.AddWithValue("@FreightTaxable", FreightTaxable);
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

    /// <summary>Update the REFSTATES table from a DataRow</summary>
    /// <param name="Row">The DataRow to be updated in the REFSTATES table</param>
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
            cmd = new SqlCommand("UPDATE \"REFSTATES\" SET " + setList + " WHERE STATE = @STATE", conn);
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
    /// <returns>The column names from REFSTATES table separated by commas</returns>
    public string ToString()
        {
        string p = "";
        p += STATE + ", ";
        p += STATENAME + ", ";
        p += Convert.ToString(SalesTaxRate) + ", ";
        p += FreightTaxable + ", ";
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
