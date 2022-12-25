
using Common;
using EagleEye_Service.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service.DAL
{
    public class DALEmployee : DataAccess
    {

        public string GetEmployeeName(string ID, out string Photo)
        {
            string name = "";
            Photo = "";
            try
            {
                query = @"Select * from tbl_employee where Employee_ID='" + ID + "' and IsDelete != '1'";
                DataTable dt = new DataTable();
                dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    name = dt.Rows[0]["Employee_Name"].ToString();
                    Photo = dt.Rows[0]["Employee_Photo"].ToString();
                }
                else
                {
                    name = "Unregistered";

                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return name;
        }

        public string checkIrr(string emp_id)
        {
            string flag = "";
            try
            {
                query = @"Select Code from tbl_employee where Employee_ID='" + emp_id + "'";
                DataTable res = ExecuteDataTable();
                if (res.Rows.Count == 0)
                {
                    query = @"Select Code from tbl_irregularemployee where Employee_ID='" + emp_id + "'";
                    res = ExecuteDataTable();
                    if (res.Rows.Count == 0)
                    {
                        flag = "irregular";

                    }
                }
            }
            catch (Exception ex)
            {

                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return flag;
        }



        public void InsertEmployee(Employee Emp)
        {
            var deviceLoc = "";
            try
            {

                query = @"Select Device_Location from tbl_device where Device_ID='" + Emp.Device_Id + "'";
                DataTable devLocation = ExecuteDataTable();
                if (devLocation.Rows.Count > 0)
                {

                    deviceLoc = devLocation.Rows[0]["Device_Location"].ToString();

                }

                query = @"Select Code from tbl_employee where Employee_ID='" + Emp.Employee_ID + "'";
                DataTable res = ExecuteDataTable();
                if (res.Rows.Count == 0)
                {
                    query = @"Select Code from tbl_irregularemployee where Employee_ID='" + Emp.Employee_ID + "'";
                    res = ExecuteDataTable();
                    if (res.Rows.Count == 0)
                    {
                        query = @"INSERT INTO [dbo].[tbl_irregularemployee]
                    ([Employee_ID]
                    ,[Employee_Name]
                    ,[Employee_Photo]
                    ,[Card_No]
                    ,[User_Privilege]
                    ,[FingerPrint]
                    ,[Face]
                    ,[Palm]
                    ,[Password]
                    ,[Device_Id]
                    ,[fkLocation_Code]
                    ,[Active]
                    ,[Update_Date]
                    ,[IsDelete]
                    ,[finger_0]
                    ,[finger_1]
                    ,[finger_2]
                    ,[finger_3]
                    ,[finger_4]
                    ,[finger_5]
                    ,[finger_6]
                    ,[finger_7]
                    ,[finger_8]
                    ,[finger_9]
                    ,[face_data]
                    ,[palm_0]
                    ,[palm_1]
                    ,[photo_data]
                    ,[Cmd_Param])
                    VALUES
                    ('" + Emp.Employee_ID + "'" +
                    ",'" + Emp.Employee_Name + "'" +
                    ",'" + Emp.Employee_Photo + "'" +
                    ",'" + Emp.Card_No + "'" +
                    ",'" + Emp.User_Privilege + "'" +
                    ",'" + Emp.FingerPrint + "'" +
                    ",'" + Emp.Face + "'" +
                    ",'" + Emp.Palm + "'" +
                    ",'" + Emp.Password + "'" +
                    ",'" + Emp.Device_Id + "'" +
                    ",'" + deviceLoc + "'" +
                    ",'" + Emp.Active + "'" +
                    ",'" + DateTime.Now + "'" +
                    ",'" + false + "'";
                        query += ",@finger_0";
                        query += ",@finger_1";
                        query += ",@finger_2";
                        query += ",@finger_3";
                        query += ",@finger_4";
                        query += ",@finger_5";
                        query += ",@finger_6";
                        query += ",@finger_7";
                        query += ",@finger_8";
                        query += ",@finger_9";
                        query += ",@face_data";
                        query += ",@palm_0";
                        query += ",@palm_1";
                        query += ",@photo_data";
                        query += ",@Cmd_Param";
                        query += ")";


                        SqlConnection con = new SqlConnection(conStr);
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        if (Emp.finger_0 != null)
                            cmd.Parameters.Add("finger_0", SqlDbType.VarBinary, -1).Value = Emp.finger_0;
                        else
                            cmd.Parameters.Add("finger_0", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_1 != null)
                            cmd.Parameters.Add("finger_1", SqlDbType.VarBinary, -1).Value = Emp.finger_1;
                        else
                            cmd.Parameters.Add("finger_1", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_2 != null)
                            cmd.Parameters.Add("finger_2", SqlDbType.VarBinary, -1).Value = Emp.finger_2;
                        else
                            cmd.Parameters.Add("finger_2", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_3 != null)
                            cmd.Parameters.Add("finger_3", SqlDbType.VarBinary, -1).Value = Emp.finger_3;
                        else
                            cmd.Parameters.Add("finger_3", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_4 != null)
                            cmd.Parameters.Add("finger_4", SqlDbType.VarBinary, -1).Value = Emp.finger_4;
                        else
                            cmd.Parameters.Add("finger_4", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_5 != null)
                            cmd.Parameters.Add("finger_5", SqlDbType.VarBinary, -1).Value = Emp.finger_5;
                        else
                            cmd.Parameters.Add("finger_5", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_6 != null)
                            cmd.Parameters.Add("finger_6", SqlDbType.VarBinary, -1).Value = Emp.finger_6;
                        else
                            cmd.Parameters.Add("finger_6", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_7 != null)
                            cmd.Parameters.Add("finger_7", SqlDbType.VarBinary, -1).Value = Emp.finger_7;
                        else
                            cmd.Parameters.Add("finger_7", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_8 != null)
                            cmd.Parameters.Add("finger_8", SqlDbType.VarBinary, -1).Value = Emp.finger_8;
                        else
                            cmd.Parameters.Add("finger_8", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_9 != null)
                            cmd.Parameters.Add("finger_9", SqlDbType.VarBinary, -1).Value = Emp.finger_9;
                        else
                            cmd.Parameters.Add("finger_9", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                        if (Emp.face_data != null)
                            cmd.Parameters.Add("face_data", SqlDbType.VarBinary, -1).Value = Emp.face_data;
                        else
                            cmd.Parameters.Add("face_data", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                        if (Emp.palm_0 != null)
                            cmd.Parameters.Add("palm_0", SqlDbType.VarBinary, -1).Value = Emp.palm_0;
                        else
                            cmd.Parameters.Add("palm_0", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.palm_1 != null)
                            cmd.Parameters.Add("palm_1", SqlDbType.VarBinary, -1).Value = Emp.palm_1;
                        else
                            cmd.Parameters.Add("palm_1", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                        if (Emp.photo_data != null)
                            cmd.Parameters.Add("photo_data", SqlDbType.VarBinary, -1).Value = Emp.photo_data;
                        else
                            cmd.Parameters.Add("photo_data", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                        if (Emp.Cmd_Param != null)
                            cmd.Parameters.Add("Cmd_Param", SqlDbType.VarBinary, -1).Value = Emp.Cmd_Param;
                        else
                            cmd.Parameters.Add("Cmd_Param", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                        int result = cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    query = @"Select Code from tbl_employee where Employee_ID='" + Emp.Employee_ID + "' AND IsDelete !='1'";
                    res = ExecuteDataTable();
                    if (res.Rows.Count == 1)
                    {
                        query = "UPDATE [dbo].[tbl_employee]" +
                            "SET " +
      "[Employee_Name] = '" + Emp.Employee_Name + "'";

                        if (!string.IsNullOrEmpty(Emp.Employee_Photo))
                            query += ",[Employee_Photo] = '" + Emp.Employee_Photo + "'";
                        else
                            query += ",[Employee_Photo] = '" + "" + "'";

                        query += ",[Card_No] = '" + Emp.Card_No + "'" +
                        ",[User_Privilege] = '" + Emp.User_Privilege + "'" +
                        ",[FingerPrint] = '" + Emp.FingerPrint + "'" +
                        ",[Face] = '" + Emp.Face + "'" +
                        ",[Palm] = '" + Emp.Palm + "'" +
                        ",[Password] = '" + Emp.Password + "'" +
                        ",[Device_Id] = '" + Emp.Device_Id + "'" +
                        ",[fkLocation_Code] = '" + deviceLoc + "'" +
                        ",[Update_Date] = '" + DateTime.Now + "'" +
                        ",[Cmd_Param] = @Cmd_Param" +
                        ",[finger_0] = @finger_0" +
                        ",[finger_1] = @finger_1" +
                        ",[finger_2] = @finger_2" +
                        ",[finger_3] = @finger_3" +
                        ",[finger_4] = @finger_4" +
                        ",[finger_5] = @finger_5" +
                        ",[finger_6] = @finger_6" +
                        ",[finger_7] = @finger_7" +
                        ",[finger_8] = @finger_8" +
                        ",[finger_9] = @finger_9" +
                        ",[face_data] = @face_data" +
                        ",[palm_0] = @palm_0" +
                        ",[palm_1] = @palm_1" +
                        ",[photo_data] = @photo_data" +
                         " WHERE [Employee_ID] = '" + Emp.Employee_ID + "'";

                        SqlConnection con = new SqlConnection(conStr);
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        if (Emp.finger_0 != null)
                            cmd.Parameters.Add("finger_0", SqlDbType.VarBinary, -1).Value = Emp.finger_0;
                        else
                            cmd.Parameters.Add("finger_0", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_1 != null)
                            cmd.Parameters.Add("finger_1", SqlDbType.VarBinary, -1).Value = Emp.finger_1;
                        else
                            cmd.Parameters.Add("finger_1", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_2 != null)
                            cmd.Parameters.Add("finger_2", SqlDbType.VarBinary, -1).Value = Emp.finger_2;
                        else
                            cmd.Parameters.Add("finger_2", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_3 != null)
                            cmd.Parameters.Add("finger_3", SqlDbType.VarBinary, -1).Value = Emp.finger_3;
                        else
                            cmd.Parameters.Add("finger_3", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_4 != null)
                            cmd.Parameters.Add("finger_4", SqlDbType.VarBinary, -1).Value = Emp.finger_4;
                        else
                            cmd.Parameters.Add("finger_4", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_5 != null)
                            cmd.Parameters.Add("finger_5", SqlDbType.VarBinary, -1).Value = Emp.finger_5;
                        else
                            cmd.Parameters.Add("finger_5", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_6 != null)
                            cmd.Parameters.Add("finger_6", SqlDbType.VarBinary, -1).Value = Emp.finger_6;
                        else
                            cmd.Parameters.Add("finger_6", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_7 != null)
                            cmd.Parameters.Add("finger_7", SqlDbType.VarBinary, -1).Value = Emp.finger_7;
                        else
                            cmd.Parameters.Add("finger_7", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_8 != null)
                            cmd.Parameters.Add("finger_8", SqlDbType.VarBinary, -1).Value = Emp.finger_8;
                        else
                            cmd.Parameters.Add("finger_8", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.finger_9 != null)
                            cmd.Parameters.Add("finger_9", SqlDbType.VarBinary, -1).Value = Emp.finger_9;
                        else
                            cmd.Parameters.Add("finger_9", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                        if (Emp.face_data != null)
                            cmd.Parameters.Add("face_data", SqlDbType.VarBinary, -1).Value = Emp.face_data;
                        else
                            cmd.Parameters.Add("face_data", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                        if (Emp.palm_0 != null)
                            cmd.Parameters.Add("palm_0", SqlDbType.VarBinary, -1).Value = Emp.palm_0;
                        else
                            cmd.Parameters.Add("palm_0", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        if (Emp.palm_1 != null)
                            cmd.Parameters.Add("palm_1", SqlDbType.VarBinary, -1).Value = Emp.palm_1;
                        else
                            cmd.Parameters.Add("palm_1", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                        if (Emp.photo_data != null)
                            cmd.Parameters.Add("photo_data", SqlDbType.VarBinary, -1).Value = Emp.photo_data;
                        else
                            cmd.Parameters.Add("photo_data", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                        if (Emp.Cmd_Param != null)
                            cmd.Parameters.Add("Cmd_Param", SqlDbType.VarBinary, -1).Value = Emp.Cmd_Param;
                        else
                            cmd.Parameters.Add("Cmd_Param", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        query = @"Select Code from tbl_irregularemployee where Employee_ID='" + Emp.Employee_ID + "'";
                        res = ExecuteDataTable();
                        if (res.Rows.Count == 0)
                        {
                            query = @"INSERT INTO [dbo].[tbl_irregularemployee]
                    ([Employee_ID]
                    ,[Employee_Name]
                    ,[Employee_Photo]
                    ,[Card_No]
                    ,[User_Privilege]
                    ,[FingerPrint]
                    ,[Face]
                    ,[Palm]
                    ,[Password]
                    ,[Device_Id]
                    ,[fkLocation_Code]
                    ,[Active]
                    ,[Update_Date]
                    ,[IsDelete]
                    ,[finger_0]
                    ,[finger_1]
                    ,[finger_2]
                    ,[finger_3]
                    ,[finger_4]
                    ,[finger_5]
                    ,[finger_6]
                    ,[finger_7]
                    ,[finger_8]
                    ,[finger_9]
                    ,[face_data]
                    ,[palm_0]
                    ,[palm_1]
                    ,[photo_data]
                    ,[Cmd_Param])
                    VALUES
('" + Emp.Employee_ID + "'" +
    ",'" + Emp.Employee_Name + "'" +
               ",'" + Emp.Employee_Photo + "'" +
               ",'" + Emp.Card_No + "'" +
               ",'" + Emp.User_Privilege + "'" +
               ",'" + Emp.FingerPrint + "'" +
               ",'" + Emp.Face + "'" +
               ",'" + Emp.Palm + "'" +
               ",'" + Emp.Password + "'" +
               ",'" + Emp.Device_Id + "'" +
                ",'" + deviceLoc + "'" +
               ",'" + Emp.Active + "'" +
               ",'" + DateTime.Now + "'" +
               ",'" + false + "'";
                            query += ",@finger_0";
                            query += ",@finger_1";
                            query += ",@finger_2";
                            query += ",@finger_3";
                            query += ",@finger_4";
                            query += ",@finger_5";
                            query += ",@finger_6";
                            query += ",@finger_7";
                            query += ",@finger_8";
                            query += ",@finger_9";
                            query += ",@face_data";
                            query += ",@palm_0";
                            query += ",@palm_1";
                            query += ",@photo_data";
                            query += ",@Cmd_Param";
                            query += ")";


                            SqlConnection con = new SqlConnection(conStr);
                            con.Open();
                            SqlCommand cmd = new SqlCommand(query, con);
                            if (Emp.finger_0 != null)
                                cmd.Parameters.Add("finger_0", SqlDbType.VarBinary, -1).Value = Emp.finger_0;
                            else
                                cmd.Parameters.Add("finger_0", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                            if (Emp.finger_1 != null)
                                cmd.Parameters.Add("finger_1", SqlDbType.VarBinary, -1).Value = Emp.finger_1;
                            else
                                cmd.Parameters.Add("finger_1", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                            if (Emp.finger_2 != null)
                                cmd.Parameters.Add("finger_2", SqlDbType.VarBinary, -1).Value = Emp.finger_2;
                            else
                                cmd.Parameters.Add("finger_2", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                            if (Emp.finger_3 != null)
                                cmd.Parameters.Add("finger_3", SqlDbType.VarBinary, -1).Value = Emp.finger_3;
                            else
                                cmd.Parameters.Add("finger_3", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                            if (Emp.finger_4 != null)
                                cmd.Parameters.Add("finger_4", SqlDbType.VarBinary, -1).Value = Emp.finger_4;
                            else
                                cmd.Parameters.Add("finger_4", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                            if (Emp.finger_5 != null)
                                cmd.Parameters.Add("finger_5", SqlDbType.VarBinary, -1).Value = Emp.finger_5;
                            else
                                cmd.Parameters.Add("finger_5", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                            if (Emp.finger_6 != null)
                                cmd.Parameters.Add("finger_6", SqlDbType.VarBinary, -1).Value = Emp.finger_6;
                            else
                                cmd.Parameters.Add("finger_6", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                            if (Emp.finger_7 != null)
                                cmd.Parameters.Add("finger_7", SqlDbType.VarBinary, -1).Value = Emp.finger_7;
                            else
                                cmd.Parameters.Add("finger_7", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                            if (Emp.finger_8 != null)
                                cmd.Parameters.Add("finger_8", SqlDbType.VarBinary, -1).Value = Emp.finger_8;
                            else
                                cmd.Parameters.Add("finger_8", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                            if (Emp.finger_9 != null)
                                cmd.Parameters.Add("finger_9", SqlDbType.VarBinary, -1).Value = Emp.finger_9;
                            else
                                cmd.Parameters.Add("finger_9", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                            if (Emp.face_data != null)
                                cmd.Parameters.Add("face_data", SqlDbType.VarBinary, -1).Value = Emp.face_data;
                            else
                                cmd.Parameters.Add("face_data", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                            if (Emp.palm_0 != null)
                                cmd.Parameters.Add("palm_0", SqlDbType.VarBinary, -1).Value = Emp.palm_0;
                            else
                                cmd.Parameters.Add("palm_0", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                            if (Emp.palm_1 != null)
                                cmd.Parameters.Add("palm_1", SqlDbType.VarBinary, -1).Value = Emp.palm_1;
                            else
                                cmd.Parameters.Add("palm_1", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                            if (Emp.photo_data != null)
                                cmd.Parameters.Add("photo_data", SqlDbType.VarBinary, -1).Value = Emp.photo_data;
                            else
                                cmd.Parameters.Add("photo_data", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                            if (Emp.Cmd_Param != null)
                                cmd.Parameters.Add("Cmd_Param", SqlDbType.VarBinary, -1).Value = Emp.Cmd_Param;
                            else
                                cmd.Parameters.Add("Cmd_Param", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                            int result = cmd.ExecuteNonQuery();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public List<Employee> GetUserIDList(string asDevId)
        {
            List<Employee> list = new List<Employee>();
            try
            {
                query = @"Select DISTINCT user_id,device_id FROM tbl_fkcmd_trans_cmd_result_user_id_list where device_id ='" + asDevId + "'";
                DataTable dt = new DataTable();
                dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Employee emp = new Employee();
                        emp.Device_Id = dt.Rows[i]["device_id"].ToString();
                        emp.Employee_ID = dt.Rows[i]["user_id"].ToString();
                        list.Add(emp);
                    }
                }

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return list;
        }

        public List<Employee> GetInvalidEmployee(string currdt)
        {
            // LogService.WriteLog("Date: " + currdt);
            List<Employee> list = new List<Employee>();
            try
            {

                //currdt = Formatter.SetValidValueToDateTime(currdt).ToString("yyyy-MM-dd");
                query = @"Select * from tbl_employee where Valid_DateEnd!='' AND Valid_DateEnd < '" + currdt + "'";
                //  LogService.WriteQuery("QUERY: " + query);
                DataTable dt = new DataTable();
                dt = ExecuteDataTable();
                // LogService.WriteLog("COUNT: " + dt.Rows.Count);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Employee emp = new Employee();
                        emp.Employee_ID = dt.Rows[i]["Employee_ID"].ToString();
                        emp.Employee_Name = dt.Rows[i]["Employee_Name"].ToString();
                        emp.Employee_Photo = dt.Rows[i]["Employee_Photo"].ToString();
                        emp.Password = dt.Rows[i]["Password"].ToString();
                        emp.Card_No = dt.Rows[i]["Card_No"].ToString();
                        emp.Active = Formatter.SetValidValueToInt(dt.Rows[i]["Active"].ToString());
                        emp.Gender = dt.Rows[i]["Gender"].ToString();
                        emp.Address = dt.Rows[i]["Address"].ToString();
                        emp.Telephone = dt.Rows[i]["Telephone"].ToString();
                        emp.User_Privilege = dt.Rows[i]["User_Privilege"].ToString();
                        emp.FingerPrint = Formatter.SetValidValueToBool(dt.Rows[i]["FingerPrint"].ToString());
                        emp.Face = Formatter.SetValidValueToBool(dt.Rows[i]["Face"].ToString());
                        emp.Palm = Formatter.SetValidValueToBool(dt.Rows[i]["Palm"].ToString());
                        emp.Password = dt.Rows[i]["Password"].ToString();

                        emp.fkDepartment_Code = Formatter.SetValidValueToInt(dt.Rows[i]["fkDepartment_Code"].ToString());
                        emp.fkLocation_Code = Formatter.SetValidValueToInt(dt.Rows[i]["fkLocation_Code"].ToString());
                        emp.fkDesignation_Code = Formatter.SetValidValueToInt(dt.Rows[i]["fkDesignation_Code"].ToString());
                        emp.fkEmployeeType_Code = Formatter.SetValidValueToInt(dt.Rows[i]["fkEmployeeType_Code"].ToString());

                        emp.Update_Date = Formatter.SetValidValueToDateTime(dt.Rows[i]["Update_Date"].ToString());
                        //emp.Cmd_Param = Convert.FromBase64String(dt.Rows[i]["Cmd_Param"].ToString());
                        emp.Device_Id = dt.Rows[i]["Device_Id"].ToString();

                        //emp.finger_0 = Convert.FromBase64String(dt.Rows[i]["finger_0"].ToString());
                        //emp.finger_1 = Convert.FromBase64String(dt.Rows[i]["finger_1"].ToString());
                        //emp.finger_2 = Convert.FromBase64String(dt.Rows[i]["finger_2"].ToString());
                        //emp.finger_3 = Convert.FromBase64String(dt.Rows[i]["finger_3"].ToString());
                        //emp.finger_4 = Convert.FromBase64String(dt.Rows[i]["finger_4"].ToString());
                        //emp.finger_5 = Convert.FromBase64String(dt.Rows[i]["finger_5"].ToString());
                        //emp.finger_6 = Convert.FromBase64String(dt.Rows[i]["finger_6"].ToString());
                        //emp.finger_7 = Convert.FromBase64String(dt.Rows[i]["finger_7"].ToString());
                        //emp.finger_8 = Convert.FromBase64String(dt.Rows[i]["finger_8"].ToString());
                        //emp.finger_9 = Convert.FromBase64String(dt.Rows[i]["finger_9"].ToString());
                        //emp.face_data = Convert.FromBase64String(dt.Rows[i]["face_data"].ToString());
                        //emp.palm_0 = Convert.FromBase64String(dt.Rows[i]["palm_0"].ToString());
                        //emp.palm_1 = Convert.FromBase64String(dt.Rows[i]["palm_1"].ToString());
                        //emp.photo_data = Convert.FromBase64String(dt.Rows[i]["photo_data"].ToString());
                        emp.Valid_DateStart = dt.Rows[i]["Valid_DateStart"].ToString();
                        emp.Valid_DateEnd = dt.Rows[i]["Valid_DateEnd"].ToString();
                        emp.Sunday = dt.Rows[i]["Sunday"].ToString();
                        emp.Monday = dt.Rows[i]["Monday"].ToString();
                        emp.Tuesday = dt.Rows[i]["Tuesday"].ToString();
                        emp.Wednesday = dt.Rows[i]["Wednesday"].ToString();
                        emp.Thursday = dt.Rows[i]["Thursday"].ToString();
                        emp.Friday = dt.Rows[i]["Friday"].ToString();
                        emp.Saturday = dt.Rows[i]["Saturday"].ToString();
                        emp.IsDelete = Formatter.SetValidValueToBool(dt.Rows[i]["IsDelete"].ToString());

                        list.Add(emp);
                    }
                }

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return list;
        }

        public void InsertExpiredEmployee(Employee Emp)
        {
            try
            {
                query = @"Select Code from tbl_expiredusers where Employee_ID='" + Emp.Employee_ID + "'";
                DataTable res = ExecuteDataTable();
                if (res.Rows.Count == 0)
                {
                    //query = @"INSERT INTO [dbo].[tbl_expiredusers]
                    //([Employee_ID],[Employee_Name],[Employee_Photo],[Card_No],[User_Privilege],[FingerPrint]
                    //,[Face],[Palm],[Password],[Device_Id],[Active],[Update_Date],[IsDelete],[Valid_DateStart]
                    //,[Valid_DateEnd],[Sunday],[Monday],[Tuesday],[Wednesday],[Thursday],[Friday],[Saturday]
                    //,[finger_0],[finger_1],[finger_2],[finger_3],[finger_4],[finger_5],[finger_6],[finger_7]
                    //,[finger_8],[finger_9],[face_data],[palm_0],[palm_1],[photo_data],[Cmd_Param])

                    query = @"INSERT INTO [dbo].[tbl_expiredusers]
                    ([Employee_ID],[Employee_Name],[Employee_Photo],[Card_No],[User_Privilege],[FingerPrint]
                    ,[Face],[Palm],[Password],[Device_Id],[Active],[Update_Date],[IsDelete],[Valid_DateStart]
                    ,[Valid_DateEnd],[Sunday],[Monday],[Tuesday],[Wednesday],[Thursday],[Friday],[Saturday])
                    VALUES
                    ('" + Emp.Employee_ID + "'" +
                    ",'" + Emp.Employee_Name + "'" +
                    ",'" + Emp.Employee_Photo + "'" +
                    ",'" + Emp.Card_No + "'" +
                    ",'" + Emp.User_Privilege + "'" +
                    ",'" + Emp.FingerPrint + "'" +
                    ",'" + Emp.Face + "'" +
                    ",'" + Emp.Palm + "'" +
                    ",'" + Emp.Password + "'" +
                    ",'" + Emp.Device_Id + "'" +
                    ",'" + Emp.Active + "'" +
                    ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    ",'" + false + "'" +
                    ",'" + Emp.Valid_DateStart + "'" +
                    ",'" + Emp.Valid_DateEnd + "'" +
                    ",'" + Emp.Sunday + "'" +
                    ",'" + Emp.Monday + "'" +
                    ",'" + Emp.Tuesday + "'" +
                    ",'" + Emp.Wednesday + "'" +
                    ",'" + Emp.Thursday + "'" +
                    ",'" + Emp.Friday + "'" +
                    ",'" + Emp.Saturday + "')";
                    //query += ",@finger_0";
                    //query += ",@finger_1";
                    //query += ",@finger_2";
                    //query += ",@finger_3";
                    //query += ",@finger_4";
                    //query += ",@finger_5";
                    //query += ",@finger_6";
                    //query += ",@finger_7";
                    //query += ",@finger_8";
                    //query += ",@finger_9";
                    //query += ",@face_data";
                    //query += ",@palm_0";
                    //query += ",@palm_1";
                    //query += ",@photo_data";
                    //query += ",@Cmd_Param";
                    //query += ")";


                    SqlConnection con = new SqlConnection(conStr);
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    //if (Emp.finger_0 != null)
                    //    cmd.Parameters.Add("finger_0", SqlDbType.VarBinary, -1).Value = Emp.finger_0;
                    //else
                    //    cmd.Parameters.Add("finger_0", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_1 != null)
                    //    cmd.Parameters.Add("finger_1", SqlDbType.VarBinary, -1).Value = Emp.finger_1;
                    //else
                    //    cmd.Parameters.Add("finger_1", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_2 != null)
                    //    cmd.Parameters.Add("finger_2", SqlDbType.VarBinary, -1).Value = Emp.finger_2;
                    //else
                    //    cmd.Parameters.Add("finger_2", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_3 != null)
                    //    cmd.Parameters.Add("finger_3", SqlDbType.VarBinary, -1).Value = Emp.finger_3;
                    //else
                    //    cmd.Parameters.Add("finger_3", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_4 != null)
                    //    cmd.Parameters.Add("finger_4", SqlDbType.VarBinary, -1).Value = Emp.finger_4;
                    //else
                    //    cmd.Parameters.Add("finger_4", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_5 != null)
                    //    cmd.Parameters.Add("finger_5", SqlDbType.VarBinary, -1).Value = Emp.finger_5;
                    //else
                    //    cmd.Parameters.Add("finger_5", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_6 != null)
                    //    cmd.Parameters.Add("finger_6", SqlDbType.VarBinary, -1).Value = Emp.finger_6;
                    //else
                    //    cmd.Parameters.Add("finger_6", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_7 != null)
                    //    cmd.Parameters.Add("finger_7", SqlDbType.VarBinary, -1).Value = Emp.finger_7;
                    //else
                    //    cmd.Parameters.Add("finger_7", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_8 != null)
                    //    cmd.Parameters.Add("finger_8", SqlDbType.VarBinary, -1).Value = Emp.finger_8;
                    //else
                    //    cmd.Parameters.Add("finger_8", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_9 != null)
                    //    cmd.Parameters.Add("finger_9", SqlDbType.VarBinary, -1).Value = Emp.finger_9;
                    //else
                    //    cmd.Parameters.Add("finger_9", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                    //if (Emp.face_data != null)
                    //    cmd.Parameters.Add("face_data", SqlDbType.VarBinary, -1).Value = Emp.face_data;
                    //else
                    //    cmd.Parameters.Add("face_data", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                    //if (Emp.palm_0 != null)
                    //    cmd.Parameters.Add("palm_0", SqlDbType.VarBinary, -1).Value = Emp.palm_0;
                    //else
                    //    cmd.Parameters.Add("palm_0", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.palm_1 != null)
                    //    cmd.Parameters.Add("palm_1", SqlDbType.VarBinary, -1).Value = Emp.palm_1;
                    //else
                    //    cmd.Parameters.Add("palm_1", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                    //if (Emp.photo_data != null)
                    //    cmd.Parameters.Add("photo_data", SqlDbType.VarBinary, -1).Value = Emp.photo_data;
                    //else
                    //    cmd.Parameters.Add("photo_data", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                    //if (Emp.Cmd_Param != null)
                    //    cmd.Parameters.Add("Cmd_Param", SqlDbType.VarBinary, -1).Value = Emp.Cmd_Param;
                    //else
                    //    cmd.Parameters.Add("Cmd_Param", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    query = "UPDATE [dbo].[tbl_expiredusers]" +
                            "SET " +
                            "[Employee_Name] = '" + Emp.Employee_Name + "'" +
                            ",[Employee_Photo] = '" + Emp.Employee_Photo + "'" +
                            ",[Card_No] = '" + Emp.Card_No + "'" +
                            ",[User_Privilege] = '" + Emp.User_Privilege + "'" +
                            ",[FingerPrint] = '" + Emp.FingerPrint + "'" +
                            ",[Face] = '" + Emp.Face + "'" +
                            ",[Palm] = '" + Emp.Palm + "'" +
                            ",[Password] = '" + Emp.Password + "'" +
                            ",[Device_Id] = '" + Emp.Device_Id + "'" +
                            ",[Update_Date] = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            ",[Valid_DateStart] = '" + Emp.Valid_DateStart + "'" +
                            ",[Valid_DateEnd]= '" + Emp.Valid_DateEnd + "'" +
                            ",[Sunday]= '" + Emp.Sunday + "'" +
                            ",[Monday]= '" + Emp.Monday + "'" +
                            ",[Tuesday]= '" + Emp.Tuesday + "'" +
                            ",[Wednesday]= '" + Emp.Wednesday + "'" +
                            ",[Thursday]= '" + Emp.Thursday + "'" +
                            ",[Friday]= '" + Emp.Friday + "'" +
                            ",[Saturday]= '" + Emp.Saturday + "'" +
       //              ",[Cmd_Param] = @Cmd_Param" +
       //",[finger_0] = @finger_0" +
       //",[finger_1] = @finger_1" +
       //",[finger_2] = @finger_2" +
       //",[finger_3] = @finger_3" +
       //",[finger_4] = @finger_4" +
       //",[finger_5] = @finger_5" +
       //",[finger_6] = @finger_6" +
       //",[finger_7] = @finger_7" +
       //",[finger_8] = @finger_8" +
       //",[finger_9] = @finger_9" +
       //",[face_data] = @face_data" +
       //",[palm_0] = @palm_0" +
       //",[palm_1] = @palm_1" +
       //",[photo_data] = @photo_data" +
       " WHERE [Employee_ID] = '" + Emp.Employee_ID + "'";

                    SqlConnection con = new SqlConnection(conStr);
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    //if (Emp.finger_0 != null)
                    //    cmd.Parameters.Add("finger_0", SqlDbType.VarBinary, -1).Value = Emp.finger_0;
                    //else
                    //    cmd.Parameters.Add("finger_0", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_1 != null)
                    //    cmd.Parameters.Add("finger_1", SqlDbType.VarBinary, -1).Value = Emp.finger_1;
                    //else
                    //    cmd.Parameters.Add("finger_1", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_2 != null)
                    //    cmd.Parameters.Add("finger_2", SqlDbType.VarBinary, -1).Value = Emp.finger_2;
                    //else
                    //    cmd.Parameters.Add("finger_2", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_3 != null)
                    //    cmd.Parameters.Add("finger_3", SqlDbType.VarBinary, -1).Value = Emp.finger_3;
                    //else
                    //    cmd.Parameters.Add("finger_3", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_4 != null)
                    //    cmd.Parameters.Add("finger_4", SqlDbType.VarBinary, -1).Value = Emp.finger_4;
                    //else
                    //    cmd.Parameters.Add("finger_4", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_5 != null)
                    //    cmd.Parameters.Add("finger_5", SqlDbType.VarBinary, -1).Value = Emp.finger_5;
                    //else
                    //    cmd.Parameters.Add("finger_5", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_6 != null)
                    //    cmd.Parameters.Add("finger_6", SqlDbType.VarBinary, -1).Value = Emp.finger_6;
                    //else
                    //    cmd.Parameters.Add("finger_6", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_7 != null)
                    //    cmd.Parameters.Add("finger_7", SqlDbType.VarBinary, -1).Value = Emp.finger_7;
                    //else
                    //    cmd.Parameters.Add("finger_7", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_8 != null)
                    //    cmd.Parameters.Add("finger_8", SqlDbType.VarBinary, -1).Value = Emp.finger_8;
                    //else
                    //    cmd.Parameters.Add("finger_8", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.finger_9 != null)
                    //    cmd.Parameters.Add("finger_9", SqlDbType.VarBinary, -1).Value = Emp.finger_9;
                    //else
                    //    cmd.Parameters.Add("finger_9", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                    //if (Emp.face_data != null)
                    //    cmd.Parameters.Add("face_data", SqlDbType.VarBinary, -1).Value = Emp.face_data;
                    //else
                    //    cmd.Parameters.Add("face_data", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                    //if (Emp.palm_0 != null)
                    //    cmd.Parameters.Add("palm_0", SqlDbType.VarBinary, -1).Value = Emp.palm_0;
                    //else
                    //    cmd.Parameters.Add("palm_0", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    //if (Emp.palm_1 != null)
                    //    cmd.Parameters.Add("palm_1", SqlDbType.VarBinary, -1).Value = Emp.palm_1;
                    //else
                    //    cmd.Parameters.Add("palm_1", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                    //if (Emp.photo_data != null)
                    //    cmd.Parameters.Add("photo_data", SqlDbType.VarBinary, -1).Value = Emp.photo_data;
                    //else
                    //    cmd.Parameters.Add("photo_data", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                    //if (Emp.Cmd_Param != null)
                    //    cmd.Parameters.Add("Cmd_Param", SqlDbType.VarBinary, -1).Value = Emp.Cmd_Param;
                    //else
                    //    cmd.Parameters.Add("Cmd_Param", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

    }
}
