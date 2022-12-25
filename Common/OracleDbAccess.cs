using System;
using System.Collections.Generic;
using System.Linq;
using Oracle.DataAccess.Client;
using System.Data;
using System.Web;

namespace Common
{
    public class OracleDbAccess
    {
        #region Declaration

        protected bool mbMaintainConnection = true;
        string conString;
        public string mstrOracle;
        public int NumberOfAttandaance;
        private OracleConnection OracleDB = null;
        protected OracleDataAdapter mobjOracleAdapter;
        public OracleCommand mobjOracleCommand;

        Exception ep;
        protected DataTable mdtTable;
        string st;
        AMS.Profile.Xml profile = null;
        #endregion

        public OracleDbAccess(string con)
        {

            OracleDB = new OracleConnection(con);
        }
        public OracleDbAccess(AMS.Profile.Xml Pf)
        {
            profile = Pf;
            GetConnectionString();
            OracleDB = new OracleConnection(conString);
        }
        public string GetConnectionString()
        {
            try
            {
                bool IsAttendanceToOracleEnabled = (profile.GetValue("Oracle DB Setting", "EnableOracleIntegration").ToString() == "True" ? true : false);

                if (IsAttendanceToOracleEnabled)
                {
                    OracleConnectionStringBuilder csb = new OracleConnectionStringBuilder
                    {
                        DataSource = profile.GetValue("Oracle DB Setting", "OracleServerName").ToString() + "/" +
                       profile.GetValue("Oracle DB Setting", "OracleServiceName").ToString(),
                        UserID = profile.GetValue("Oracle DB Setting", "OracleUserId").ToString(),
                        Password = profile.GetValue("Oracle DB Setting", "OraclePassword").ToString()
                    };

                    conString = csb.ConnectionString;
                }
                else
                {
                    conString = "";
                }
            }
            catch { }
            return conString;
        }

        #region DataBase Connection/Open/Close Methods

        public bool OpenDatabaseConnection()
        {
            try
            {
                OracleDB.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CloseDatabaseConnection()
        {
            try
            {
                if (OracleDB.State == ConnectionState.Open)
                    OracleDB.Close();
            }
            catch (System.Exception)
            { }
        }
        public int ExecuteNonQuery()
        {
            int iRowsAffected = 0;
            try
            {
                if (mstrOracle != "")
                {
                    mobjOracleCommand = OracleDB.CreateCommand();
                    mobjOracleCommand.CommandText = mstrOracle;
                    OpenDatabaseConnection();
                    iRowsAffected = mobjOracleCommand.ExecuteNonQuery();
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
                CloseDatabaseConnection();
            }
            mstrOracle = "";
            return iRowsAffected;
        }
        #endregion

        public bool TestDBConnectivity(string rstrConnectionString)
        {
            OracleConnection objOracleConn = new OracleConnection(rstrConnectionString);
            try
            {
                objOracleConn.Open();
                objOracleConn.Close();
                return true;
            }
            catch (Exception)
            {
                objOracleConn.Close();
                return false;
            }
        }

        public DataTable ExecuteDatatable()
        {
            DataTable dt = new DataTable();
            try
            {
                if (mstrOracle != "")
                {
                    OpenDatabaseConnection();

                    mobjOracleAdapter = new OracleDataAdapter(mobjOracleCommand);
                    mobjOracleAdapter.Fill(dt);
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
            mstrOracle = "";

            return dt;
        }

        public bool CheckIfExist(string TableName, string ColumnDate, string ColumnEmployeeCode, string ColumnTime, string ColumnStatus, string ColumnMachine, DateTime AttendanceDate, string EmployeeCode, DateTime Time, string Status, int MachineId)
        {
            bool IsExist = true;

            int Total = 0;

            try
            {
                mstrOracle = "Select Count(*) as REC From " + TableName + " Where " + ColumnDate + " = to_date('" + AttendanceDate.ToString("dd-MMM-yyyy hh:mm:ss tt") + "','DD-MON-RR HH12:MI:SS AM') and " + ColumnEmployeeCode + " = '" + EmployeeCode + "' and " + ColumnStatus + " = '" + Status + "' and " + ColumnMachine + " = '" + MachineId + "'";
                GetStatement = mstrOracle;
                DataTable dt = ExecuteDatatable();
                Total = dt.Rows.Count > 0 ? int.Parse(dt.Rows[0][0].ToString()) : 0;

            }
            catch (Exception excp)
            {
                GetException = excp;
            }

            IsExist = Total > 0;

            return IsExist;
        }

        public int ExecuteScaler()
        {
            object objScaler = "";
            try
            {
                if (mstrOracle != "")
                {
                    mobjOracleCommand.CommandText = mstrOracle;
                    mstrOracle = "";

                    OpenDatabaseConnection();

                    objScaler = mobjOracleCommand.ExecuteScalar();

                    CloseDatabaseConnection();
                    if (objScaler != null)
                        return Convert.ToInt16(objScaler);
                    else
                        return -1;
                }
            }
            catch (Exception)
            {

            }
            mstrOracle = "";
            return -1;
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
