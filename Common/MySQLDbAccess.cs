using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MySQLDbAccess
    {
        public string mstrConnectionString;
        public string mstrSQL;

        protected bool mbMaintainConnection = true;

        string st;
        protected MySqlConnection mobjSqlConn;
        public MySqlCommand mobjSqlCommand;
        protected MySqlDataAdapter mobjSqlDataAdapter;
        protected MySqlCommandBuilder mobjSqlCommandBuilder;
        protected MySqlTransaction mobjSqlTransaction;
        private AMS.Profile.Xml profile = null;
        Exception ep;
        public MySQLDbAccess(string Connection)
        {
            //profile = Pf;
            //LoadDatabaseSettings();
            mobjSqlConn = new MySqlConnection(Connection);

            mobjSqlCommand = new MySqlCommand();
            mobjSqlCommand.Connection = mobjSqlConn;

            mobjSqlDataAdapter = new MySqlDataAdapter();
            mobjSqlDataAdapter.SelectCommand = new MySqlCommand();
            mobjSqlDataAdapter.SelectCommand.Connection = mobjSqlConn;

            mobjSqlCommandBuilder = new MySqlCommandBuilder(mobjSqlDataAdapter);

            mstrSQL = "";

        }

        private void LoadDatabaseSettings()
        {
            try
            {
                string strConnectionString = "Data Source={0};Port={1};Initial Catalog={2};User ID={3};Password={4};";
                string strDatabaseServerName = profile.GetValue("MySql DB Setting", "mySqlServerName").ToString();
                string strDatabaseName = profile.GetValue("MySql DB Setting", "mySqlDatabaseName").ToString();
                string strUserName = profile.GetValue("MySql DB Setting", "mySqlUserId").ToString();
                string strPassword = profile.GetValue("MySql DB Setting", "mySqlPassword").ToString();
                string strPort = profile.GetValue("MySql DB Setting", "mySqlPort").ToString();

                mstrConnectionString = string.Format(strConnectionString,
                    strDatabaseServerName,
                    strPort,
                    strDatabaseName,
                    strUserName,
                    strPassword);
            }
            catch (System.Exception)
            { }
        }


        public bool TestDBConnectivity(string rstrConnectionString)
        {
            MySqlConnection objSqlConn = new MySqlConnection(rstrConnectionString);
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
            catch (Exception ex)
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
            catch (System.Exception ex)
            { }
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

    }
}
