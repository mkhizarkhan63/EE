using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service.DAL
{
    public class DataAccess
    {
        public string conStr = "";
        SqlConnection con;
        public string query = "";

        public DataAccess()
        {
            try
            {
                string s = AppDomain.CurrentDomain.BaseDirectory + @"\App_Setting.txt";
                conStr = File.ReadAllText(s);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }

        public int ExecuteNonQuery()
        {
            int res = 0;
            try
            {
                con = new SqlConnection(conStr);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                query = "";
                con.Close();
            }
            return res;
        }

        public DataTable ExecuteDataTable()
        {
            DataTable dt = new DataTable();
            try
            {
              //  clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, query);


                con = new SqlConnection(conStr);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                query = "";
                con.Close();
            }

            return dt;
        }


        public DataTable ExecuteDataTable(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                con = new SqlConnection(conStr);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                query = "";
                con.Close();
            }

            return dt;
        }
    }
}
