using System;
using System.Data.SqlClient;
using System.Data;

namespace Common
{
    public class SQLDbAccess
    {
        public string mstrConnectionString;
        public string mstrSQL;

        protected bool mbMaintainConnection = true;

        string st;
        protected SqlConnection mobjSqlConn;
        public SqlCommand mobjSqlCommand;
        protected SqlDataAdapter mobjSqlDataAdapter;
        protected SqlCommandBuilder mobjSqlCommandBuilder;
        protected SqlTransaction mobjSqlTransaction;
        private AMS.Profile.Xml profile = null;

        Exception ep;
        
        public SQLDbAccess(string Connection)
        {
            //profile = Pf;
            //LoadDatabaseSettings();
            mobjSqlConn = new SqlConnection(Connection);

            mobjSqlCommand = new SqlCommand();
            mobjSqlCommand.Connection = mobjSqlConn;

            mobjSqlDataAdapter = new SqlDataAdapter();
            mobjSqlDataAdapter.SelectCommand = new SqlCommand();
            mobjSqlDataAdapter.SelectCommand.Connection = mobjSqlConn;

            mobjSqlCommandBuilder = new SqlCommandBuilder(mobjSqlDataAdapter);

            mstrSQL = "";

        }

        private void LoadDatabaseSettings()
        {
            try
            {
                string strConnectionString = "Data Source={0};Initial Catalog={1};User ID={2};Password={3};Packet Size=4096;";
                string strDatabaseServerName = profile.GetValue("SQL DB Setting", "SqlServerName").ToString();
                string strDatabaseName = profile.GetValue("SQL DB Setting", "SqlDatabaseName").ToString();
                string strUserName = profile.GetValue("SQL DB Setting", "SqlUserId").ToString();
                string strPassword = profile.GetValue("SQL DB Setting", "SqlPassword").ToString();

                mstrConnectionString = string.Format(strConnectionString,
                    strDatabaseServerName,
                    strDatabaseName,
                    strUserName,
                    strPassword);
            }
            catch (System.Exception)
            { }
        }

        public bool TestDBConnectivity(string rstrConnectionString)
        {
            SqlConnection objSqlConn = new SqlConnection(rstrConnectionString);
            try
            {
                objSqlConn.Open();
                objSqlConn.Close();
                return true;
            }
            catch (Exception ex)
            {
                objSqlConn.Close();
                return false;
            }
        }

        public int ExecuteNonQuery()
        {
            int iRowsAffected = 0;
            try
            {
                if (mstrSQL != "")
                {
                    mobjSqlCommand.CommandText = mstrSQL;
                    OpenDatabaseConnection();
                    iRowsAffected = mobjSqlCommand.ExecuteNonQuery();
                    CloseDatabaseConnection();
                }
            }
            catch (System.Exception ex)
            {
                CloseDatabaseConnection();
                GetException = ex;
            }
            finally
            {
                if (mbMaintainConnection == false)
                    CloseDatabaseConnection();
            }
            mstrSQL = "";
            return iRowsAffected;
        }

        public bool OpenDatabaseConnection()
        {
            try
            {
                if (mobjSqlConn.State != ConnectionState.Open)
                    mobjSqlConn.Open();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public void CloseDatabaseConnection()
        {
            try
            {
                if (mobjSqlConn.State != ConnectionState.Closed)
                    mobjSqlConn.Close();
            }
            catch (System.Exception)
            { }
        }
        public int InsertDataSql(string TableName, string ColumnDate, string ColumnEmployeeCode, string ColumnTime, string ColumnStatus, string ColumnMachine, DateTime AttendanceDate, string EmployeeCode, DateTime Time, string Status, int MachineId)
        {
            GetStatement = "";
            mstrSQL = "Insert into " + TableName + " (" + ColumnDate + "," + ColumnEmployeeCode + "," + ColumnStatus + "," + ColumnMachine + ") values ('" + AttendanceDate + "','" + EmployeeCode + "','" + Status + "','" + MachineId + "')";
            GetStatement = mstrSQL;
            int id = ExecuteNonQuery();
            return id;
        }

        public bool CheckIfExist(string TableName, string ColumnDate, string ColumnEmployeeCode, string ColumnTime, string ColumnStatus, string ColumnMachine, DateTime AttendanceDate, string EmployeeCode, DateTime Time, string Status, int MachineId)
        {
            bool IsExist = false;
            int Total = 0;
            try
            {
                GetStatement = "";
                GetException = null;
                mstrSQL = "Select Count(*) as Total From " + TableName + " Where " + ColumnDate + " = '" + AttendanceDate.ToString("dd-MMM-yyyy hh:mm:ss tt") + "' and " + ColumnEmployeeCode + " = '" + EmployeeCode + "' and " + ColumnStatus + " = '" + Status + "' and " + ColumnMachine + " = " + MachineId.ToString();
                GetStatement = mstrSQL;
                DataTable dt = ExecuteDataTable();
                Total = dt.Rows.Count > 0 ? int.Parse(dt.Rows[0][0].ToString()) : 0;
            }
            catch (Exception excp)
            {
                GetException = excp;
            }

            IsExist = Total > 0;

            return IsExist;
        }

        public DataTable ExecuteDataTable()
        {
            DataSet dsDataSet = new DataSet();
            try
            {
                if (mstrSQL != "")
                {
                    mobjSqlDataAdapter.SelectCommand.CommandText = mstrSQL;
                    OpenDatabaseConnection();
                    mobjSqlDataAdapter.Fill(dsDataSet);

                    mobjSqlDataAdapter.SelectCommand.Parameters.Clear();
                    mobjSqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                }
            }
            catch (System.Exception ex)
            {
                CloseDatabaseConnection();
                throw new SystemException(ex.Message, ex);
            }
            finally
            {
                CloseDatabaseConnection();
            }
            mstrSQL = "";
            return dsDataSet.Tables[0];
        }
        public Exception GetException
        {
            get
            {
                return ep;
            }
            set
            {
                ep = value;
            }
        }

        public string GetStatement
        {
            get
            {
                return st;
            }
            set
            {
                st = value;
            }
        }
    }
}
