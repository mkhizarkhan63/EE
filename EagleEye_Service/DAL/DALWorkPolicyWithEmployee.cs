using Common;
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
    public class DALWorkPolicyWithEmployee : DataAccess
    {


        public bool InsertWorkPolicy__Employee(EventLogs log)
        {
            bool flag = false;
            try
            {
                var Overtime = string.Empty;
                var WorkHour = string.Empty;
                var BreakHour = string.Empty;
                var ExtraHour = string.Empty;

                //First Get Employee's policy code

                string PolicyCode = string.Empty;
                bool isPolicyActive = false;

                query = @"Select WorkHourPolicyCode From tbl_employee where Employee_ID ='" + log.UserID + "' and isDelete != 1";

                DataTable dt = new DataTable();

                dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                    PolicyCode = dt.Rows[0]["WorkHourPolicyCode"].ToString();

                //get workHour if it is Active or not
                query = @"SELECT wh.isActive FROM tbl_workHourPolicy wh where Code ='" + PolicyCode + "'";
                dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                    isPolicyActive = dt.Rows[0]["isActive"].ToString().ToLower() == "true" ? true : false;

                if (isPolicyActive)
                {
                    //**********Getting Current Day in integer*****************
                    var todaysDay = ((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek;
                    query = @"select DayCheck,Day,Workhour,Overtime,Breakhour,isOvertimeActive From tbl_policyDetail where PolicyCode='" + PolicyCode + "' and Day ='" + todaysDay + "'";
                    dt = ExecuteDataTable();

                    bool dayCheck = dt.Rows[0]["DayCheck"].ToString().ToLower() == "true" ? true : false;

                    if (dt.Rows.Count > 0 && dayCheck != false)
                    {
                        //from tbl_policyDetail 
                        var DefinedWorkHourStr = dt.Rows[0]["Workhour"].ToString().Split(':');
                        var DefinedOvertimeStr = dt.Rows[0]["Overtime"].ToString().Split(':');
                        var DefinedBreakhourStr = dt.Rows[0]["Breakhour"].ToString().Split(':');
                        bool isOverTimeAllow = dt.Rows[0]["isOvertimeActive"].ToString().ToLower() == "true" ? true : false;

                        double totalWorkHour_Seconds = new double();
                        double totalOvertime_Seconds = new double();
                        double totalBreakHour_Seconds = new double();


                        //send hours and min
                        if (!String.IsNullOrEmpty(dt.Rows[0]["Workhour"].ToString()))
                        {
                            totalWorkHour_Seconds = ConvertMinutesToMilliseconds(DefinedWorkHourStr[0], DefinedWorkHourStr[1]);
                        }
                        if (!String.IsNullOrEmpty(dt.Rows[0]["Overtime"].ToString()))
                        {
                            totalOvertime_Seconds = ConvertMinutesToMilliseconds(DefinedOvertimeStr[0], DefinedOvertimeStr[1]);
                        }
                        if (!String.IsNullOrEmpty(dt.Rows[0]["Breakhour"].ToString()))
                        {
                            totalBreakHour_Seconds = ConvertMinutesToMilliseconds(DefinedBreakhourStr[0], DefinedBreakhourStr[1]);
                        }

                        //DateTime DefinedWorkHour = Formatter.SetValidValueToDateTime(dt.Rows[0]["Workhour"].ToString());
                        // DateTime DefinedOvertime = Formatter.SetValidValueToDateTime(dt.Rows[0]["Overtime"].ToString());
                        // DateTime DefinedBreakhour = Formatter.SetValidValueToDateTime(dt.Rows[0]["Breakhour"].ToString());

                        //*************************************************************************
                        var logsDateTime = "";
                        if (!log.DateTime.Contains("-"))
                        {
                            string year = log.DateTime.Substring(0, 4);
                            string month = log.DateTime.Substring(4, 2);
                            string day = log.DateTime.Substring(6, 2);
                            logsDateTime = year + "-" + month + "-" + day;
                        }

                        //**********************************************************************
                        query = @"Select top 1 Attendance_DateTime,Status from tbl_attendence where Employee_ID ='" + log.UserID + "' and Attendance_DateTime between '" + logsDateTime + " 00:00:00' and '" + logsDateTime + " 23:59:59'  order by Attendance_DateTime desc";
                        dt = ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            string hour = "";
                            string min = "";
                            string sec = "";
                            if (!log.DateTime.Contains("-"))
                            {
                                hour = log.DateTime.Substring(8, 2);
                                min = log.DateTime.Substring(10, 2);
                                min = log.DateTime.Substring(10, 2);
                                sec = log.DateTime.Substring(12, 2);

                            }
                            else
                            {
                                hour = log.DateTime.Substring(11, 2);
                                min = log.DateTime.Substring(14, 2);
                                sec = log.DateTime.Substring(17, 2);
                            }
                            //getting log.datetime here...
                            var getLogsTime = hour + ":" + min + ":" + sec;
                            var FirstIN_Time = Formatter.SetValidValueToDateTime(dt.Rows[0]["Attendance_DateTime"].ToString()).TimeOfDay;
                            var LastOUT_Time = Convert.ToDateTime(getLogsTime).TimeOfDay;
                            var status = dt.Rows[0]["Status"].ToString();


                            //transaction difference between in out from out - in
                            var TransactionDiff__betINOUT = (LastOUT_Time - FirstIN_Time);

                            //actualworkhour is basically sitting hours
                            var ActualWorkHour = TransactionDiff__betINOUT;
                            var getHours = ActualWorkHour.Hours;
                            var getMins = ActualWorkHour.Minutes;
                            var getSecs = ActualWorkHour.Seconds;
                            //**********************************************************************


                            //***********************************************************************************
                            TimeSpan alreadySitted_WorkHour = new TimeSpan();
                            TimeSpan alreadySitted_OverTime = new TimeSpan();
                            TimeSpan alreadySitted_BreakHour = new TimeSpan();

                            var newDT = Formatter.SetValidValueToDateTime(logsDateTime);

                            //getting this table because for existing data or to sum up
                            query = @"Select Workhour,Overtime,Breakhour from [dbo].[tbl_policyWithEmployee] where 
                                    Emp_id ='" + log.UserID + "' and Dt = '" + newDT.Date + "' and Policycode ='" + PolicyCode + "'";

                            dt = ExecuteDataTable();

                            if (dt.Rows.Count > 0)
                            {
                                //already sitting workhour
                                alreadySitted_WorkHour = Formatter.SetValidValueToDateTime(dt.Rows[0]["Workhour"].ToString()).TimeOfDay;
                                //already sitting overtime
                                alreadySitted_OverTime = Formatter.SetValidValueToDateTime(dt.Rows[0]["Overtime"].ToString()).TimeOfDay;
                                //already sitting breakhour
                                alreadySitted_BreakHour = Formatter.SetValidValueToDateTime(dt.Rows[0]["Breakhour"].ToString()).TimeOfDay;
                            }
                            //***********************************************************************************

                            //Incoming OUT and last IN
                            if (log.Status == "2" && status == "1")
                            {

                                var sum_WH = alreadySitted_WorkHour.TotalSeconds + ActualWorkHour.TotalSeconds;
                                if (sum_WH > totalWorkHour_Seconds)
                                {
                                    var foundWH = gettingHours_min_sec(totalWorkHour_Seconds).Split(':');
                                    var getWH = foundWH[0] + ":" + foundWH[1] + ":" + foundWH[2];
                                    WorkHour = getWH.ToString();

                                    //if overtime is allow or not ************************************
                                    if (isOverTimeAllow)
                                    {
                                        var Diff_OT = sum_WH - totalWorkHour_Seconds;
                                        var sum_OT = Diff_OT + alreadySitted_OverTime.TotalSeconds;
                                        if (sum_OT > totalOvertime_Seconds)
                                        {
                                            var foundOT = gettingHours_min_sec(totalOvertime_Seconds).Split(':');
                                            var getOT = foundOT[0] + ":" + foundOT[1] + ":" + foundOT[2];
                                            Overtime = getOT.ToString();

                                            var Diff_EH = sum_OT - totalOvertime_Seconds;
                                            var sum_EH = Diff_EH;
                                            var foundEH = gettingHours_min_sec(sum_EH).Split(':');
                                            var getEH = foundEH[0] + ":" + foundEH[1] + ":" + foundEH[2];
                                            ExtraHour = getEH.ToString();
                                        }
                                        else
                                        {
                                            var foundOT = gettingHours_min_sec(sum_OT).Split(':');
                                            var getOT = foundOT[0] + ":" + foundOT[1] + ":" + foundOT[2];
                                            Overtime = getOT.ToString();
                                        }

                                    }
                                    //   ************************************
                                    else
                                    {
                                        var Diff_EH = sum_WH - totalWorkHour_Seconds;
                                        var foundEH = gettingHours_min_sec(Diff_EH).Split(':');
                                        var getEH = foundEH[0] + ":" + foundEH[1] + ":" + foundEH[2];
                                        ExtraHour = getEH.ToString();

                                    }
                                }
                                else
                                {
                                    var foundWH = gettingHours_min_sec(sum_WH).Split(':');
                                    var getWH = foundWH[0] + ":" + foundWH[1] + ":" + foundWH[2];
                                    WorkHour = getWH.ToString();

                                }
                                flag = true;
                            }

                            //Incoming Lunch out and last lunch in
                            else if (log.Status == "4" && status == "3")
                            {
                                if (totalBreakHour_Seconds == 0)
                                {
                                    ExtraHour = getHours + ":" + getMins + ":" + getSecs;
                                }
                                //for extra hour if it is greater than total breakhour......
                                else if (ActualWorkHour.TotalSeconds > totalBreakHour_Seconds)
                                {
                                    //for extra hour 
                                    var ForExtraHourDiff = ActualWorkHour.TotalSeconds - totalBreakHour_Seconds;
                                    var foundEH = gettingHours_min_sec(ForExtraHourDiff).Split(':');
                                    var getEH = foundEH[0] + ":" + foundEH[1] + ":" + foundEH[2];
                                    ExtraHour = getEH.ToString();

                                    //for break hour
                                    var foundBreak_Hour = gettingHours_min_sec(totalBreakHour_Seconds).Split(':');
                                    var getBreak_Hour = foundBreak_Hour[0] + ":" + foundBreak_Hour[1] + ":" + foundBreak_Hour[2];
                                    BreakHour = getBreak_Hour.ToString();


                                }
                                else if (ActualWorkHour.TotalSeconds <= totalBreakHour_Seconds)
                                {
                                    if (alreadySitted_BreakHour.TotalSeconds != 0)
                                    {
                                        var timeFormatter = getHours + ":" + getMins + ":" + getSecs;
                                        var ActualBreakHourTime = Formatter.SetValidValueToDateTime(timeFormatter).TimeOfDay;
                                        var totalBH = ActualBreakHourTime + alreadySitted_BreakHour;
                                        if (totalBH.TotalSeconds > totalBreakHour_Seconds)
                                        {

                                            //for break hour
                                            var foundBreak_Hour = gettingHours_min_sec(totalBreakHour_Seconds).Split(':');
                                            var getBreak_Hour = foundBreak_Hour[0] + ":" + foundBreak_Hour[1] + ":" + foundBreak_Hour[2];
                                            BreakHour = getBreak_Hour.ToString();

                                            //for extra hour 
                                            var ForExtraHourDiff = totalBH.TotalSeconds - totalBreakHour_Seconds;
                                            var foundEH = gettingHours_min_sec(ForExtraHourDiff).Split(':');
                                            var getEH = foundEH[0] + ":" + foundEH[1] + ":" + foundEH[2];
                                            ExtraHour = getEH.ToString();

                                        }
                                        else
                                        {
                                            //for break hour
                                            var foundBreak_Hour = gettingHours_min_sec(totalBH.TotalSeconds).Split(':');
                                            var getBreak_Hour = foundBreak_Hour[0] + ":" + foundBreak_Hour[1] + ":" + foundBreak_Hour[2];
                                            BreakHour = getBreak_Hour.ToString();


                                        }
                                    }
                                    else
                                    {
                                        BreakHour = getHours + ":" + getMins + ":" + getSecs;
                                    }
                                }
                                flag = true;
                            }
                            //Incoming OT out and last OT in
                            else if (log.Status == "6" && status == "5")
                            {
                                if (totalBreakHour_Seconds == 0)
                                {
                                    ExtraHour = getHours + ":" + getMins + ":" + getSecs;
                                }
                                //for extra hour if it is greater than total breakhour......
                                else if (ActualWorkHour.TotalSeconds > totalBreakHour_Seconds)
                                {
                                    //for extra hour 
                                    var ForExtraHourDiff = ActualWorkHour.TotalSeconds - totalBreakHour_Seconds;
                                    var foundEH = gettingHours_min_sec(ForExtraHourDiff).Split(':');
                                    var getEH = foundEH[0] + ":" + foundEH[1] + ":" + foundEH[2];
                                    ExtraHour = getEH.ToString();

                                    //for break hour
                                    var foundBreak_Hour = gettingHours_min_sec(totalBreakHour_Seconds).Split(':');
                                    var getBreak_Hour = foundBreak_Hour[0] + ":" + foundBreak_Hour[1] + ":" + foundBreak_Hour[2];
                                    BreakHour = getBreak_Hour.ToString();


                                }
                                else if (ActualWorkHour.TotalSeconds <= totalBreakHour_Seconds)
                                {
                                    if (alreadySitted_BreakHour.TotalSeconds != 0)
                                    {
                                        var timeFormatter = getHours + ":" + getMins + ":" + getSecs;
                                        var ActualBreakHourTime = Formatter.SetValidValueToDateTime(timeFormatter).TimeOfDay;
                                        var totalBH = ActualBreakHourTime + alreadySitted_BreakHour;
                                        if (totalBH.TotalSeconds > totalBreakHour_Seconds)
                                        {

                                            //for break hour
                                            var foundBreak_Hour = gettingHours_min_sec(totalBreakHour_Seconds).Split(':');
                                            var getBreak_Hour = foundBreak_Hour[0] + ":" + foundBreak_Hour[1] + ":" + foundBreak_Hour[2];
                                            BreakHour = getBreak_Hour.ToString();

                                            //for extra hour 
                                            var ForExtraHourDiff = totalBH.TotalSeconds - totalBreakHour_Seconds;
                                            var foundEH = gettingHours_min_sec(ForExtraHourDiff).Split(':');
                                            var getEH = foundEH[0] + ":" + foundEH[1] + ":" + foundEH[2];
                                            ExtraHour = getEH.ToString();

                                        }
                                        else
                                        {
                                            //for break hour
                                            var foundBreak_Hour = gettingHours_min_sec(totalBH.TotalSeconds).Split(':');
                                            var getBreak_Hour = foundBreak_Hour[0] + ":" + foundBreak_Hour[1] + ":" + foundBreak_Hour[2];
                                            BreakHour = getBreak_Hour.ToString();


                                        }
                                    }
                                    else
                                    {
                                        BreakHour = getHours + ":" + getMins + ":" + getSecs;
                                    }
                                }
                                flag = true;
                            }
                            //Incoming Tea out and last Tea in
                            else if (log.Status == "8" && status == "7")
                            {
                                if (totalBreakHour_Seconds == 0)
                                {
                                    ExtraHour = getHours + ":" + getMins + ":" + getSecs;
                                }
                                //for extra hour if it is greater than total breakhour......
                                else if (ActualWorkHour.TotalSeconds > totalBreakHour_Seconds)
                                {
                                    //for extra hour 
                                    var ForExtraHourDiff = ActualWorkHour.TotalSeconds - totalBreakHour_Seconds;
                                    var foundEH = gettingHours_min_sec(ForExtraHourDiff).Split(':');
                                    var getEH = foundEH[0] + ":" + foundEH[1] + ":" + foundEH[2];
                                    ExtraHour = getEH.ToString();

                                    //for break hour
                                    var foundBreak_Hour = gettingHours_min_sec(totalBreakHour_Seconds).Split(':');
                                    var getBreak_Hour = foundBreak_Hour[0] + ":" + foundBreak_Hour[1] + ":" + foundBreak_Hour[2];
                                    BreakHour = getBreak_Hour.ToString();


                                }
                                else if (ActualWorkHour.TotalSeconds <= totalBreakHour_Seconds)
                                {
                                    if (alreadySitted_BreakHour.TotalSeconds != 0)
                                    {
                                        var timeFormatter = getHours + ":" + getMins + ":" + getSecs;
                                        var ActualBreakHourTime = Formatter.SetValidValueToDateTime(timeFormatter).TimeOfDay;
                                        var totalBH = ActualBreakHourTime + alreadySitted_BreakHour;
                                        if (totalBH.TotalSeconds > totalBreakHour_Seconds)
                                        {

                                            //for break hour
                                            var foundBreak_Hour = gettingHours_min_sec(totalBreakHour_Seconds).Split(':');
                                            var getBreak_Hour = foundBreak_Hour[0] + ":" + foundBreak_Hour[1] + ":" + foundBreak_Hour[2];
                                            BreakHour = getBreak_Hour.ToString();

                                            //for extra hour 
                                            var ForExtraHourDiff = totalBH.TotalSeconds - totalBreakHour_Seconds;
                                            var foundEH = gettingHours_min_sec(ForExtraHourDiff).Split(':');
                                            var getEH = foundEH[0] + ":" + foundEH[1] + ":" + foundEH[2];
                                            ExtraHour = getEH.ToString();

                                        }
                                        else
                                        {
                                            //for break hour
                                            var foundBreak_Hour = gettingHours_min_sec(totalBH.TotalSeconds).Split(':');
                                            var getBreak_Hour = foundBreak_Hour[0] + ":" + foundBreak_Hour[1] + ":" + foundBreak_Hour[2];
                                            BreakHour = getBreak_Hour.ToString();


                                        }
                                    }
                                    else
                                    {
                                        BreakHour = getHours + ":" + getMins + ":" + getSecs;
                                    }
                                }
                                flag = true;
                            }
                            //Incoming Pray out and last Pray in
                            else if (log.Status == "10" && status == "9")
                            {
                                if (totalBreakHour_Seconds == 0)
                                {
                                    ExtraHour = getHours + ":" + getMins + ":" + getSecs;
                                }
                                //for extra hour if it is greater than total breakhour......
                                else if (ActualWorkHour.TotalSeconds > totalBreakHour_Seconds)
                                {
                                    //for extra hour 
                                    var ForExtraHourDiff = ActualWorkHour.TotalSeconds - totalBreakHour_Seconds;
                                    var foundEH = gettingHours_min_sec(ForExtraHourDiff).Split(':');
                                    var getEH = foundEH[0] + ":" + foundEH[1] + ":" + foundEH[2];
                                    ExtraHour = getEH.ToString();

                                    //for break hour
                                    var foundBreak_Hour = gettingHours_min_sec(totalBreakHour_Seconds).Split(':');
                                    var getBreak_Hour = foundBreak_Hour[0] + ":" + foundBreak_Hour[1] + ":" + foundBreak_Hour[2];
                                    BreakHour = getBreak_Hour.ToString();


                                }
                                else if (ActualWorkHour.TotalSeconds <= totalBreakHour_Seconds)
                                {
                                    if (alreadySitted_BreakHour.TotalSeconds != 0)
                                    {
                                        var timeFormatter = getHours + ":" + getMins + ":" + getSecs;
                                        var ActualBreakHourTime = Formatter.SetValidValueToDateTime(timeFormatter).TimeOfDay;
                                        var totalBH = ActualBreakHourTime + alreadySitted_BreakHour;
                                        if (totalBH.TotalSeconds > totalBreakHour_Seconds)
                                        {

                                            //for break hour
                                            var foundBreak_Hour = gettingHours_min_sec(totalBreakHour_Seconds).Split(':');
                                            var getBreak_Hour = foundBreak_Hour[0] + ":" + foundBreak_Hour[1] + ":" + foundBreak_Hour[2];
                                            BreakHour = getBreak_Hour.ToString();

                                            //for extra hour 
                                            var ForExtraHourDiff = totalBH.TotalSeconds - totalBreakHour_Seconds;
                                            var foundEH = gettingHours_min_sec(ForExtraHourDiff).Split(':');
                                            var getEH = foundEH[0] + ":" + foundEH[1] + ":" + foundEH[2];
                                            ExtraHour = getEH.ToString();

                                        }
                                        else
                                        {
                                            //for break hour
                                            var foundBreak_Hour = gettingHours_min_sec(totalBH.TotalSeconds).Split(':');
                                            var getBreak_Hour = foundBreak_Hour[0] + ":" + foundBreak_Hour[1] + ":" + foundBreak_Hour[2];
                                            BreakHour = getBreak_Hour.ToString();


                                        }
                                    }
                                    else
                                    {
                                        BreakHour = getHours + ":" + getMins + ":" + getSecs;
                                    }
                                }
                                flag = true;
                            }

                            if (flag)
                            {
                                UpdateDb(WorkHour, Overtime, BreakHour, ExtraHour, logsDateTime, log.UserID, PolicyCode);
                            }

                        }


                    }


                }

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            return flag;
        }

        private void UpdateDb(string WH, string OT, string BH, string EH, string logsDateTime, string userID, string policyCode)
        {
            try
            {
                DataTable dt = new DataTable();
                bool flag = false;
                var newDt = Formatter.SetValidValueToDateTime(logsDateTime);
                query = @"Select Workhour,Overtime,Breakhour,Extrahour  from [dbo].[tbl_policyWithEmployee] where Emp_id ='" + userID + "' and Dt = '" + newDt.Date + "' and Policycode ='" + policyCode + "'";
                dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    var break_hour = dt.Rows[0]["Breakhour"].ToString();
                    var work_hour = dt.Rows[0]["Workhour"].ToString();
                    var over_time = dt.Rows[0]["Overtime"].ToString();
                    var extra_hour = dt.Rows[0]["Extrahour"].ToString();

                    if (!String.IsNullOrEmpty(WH))
                    {
                        var totalWorkHour = Formatter.SetValidValueToDateTime(WH).TimeOfDay;

                        query = @"Update  [dbo].[tbl_policyWithEmployee] set  WorkHour = '" + totalWorkHour.ToString() + "'  where Emp_id ='" + userID + "' and Dt = '" + newDt.Date + "' and Policycode ='" + policyCode + "'";

                        int res = ExecuteNonQuery();
                        if (res == 1)
                            flag = true;

                    }
                    if (!String.IsNullOrEmpty(OT))
                    {
                        var totalOverTime = Formatter.SetValidValueToDateTime(OT).TimeOfDay;
                        query = @"Update  [dbo].[tbl_policyWithEmployee] set  Overtime = '" + totalOverTime + "'  where Emp_id ='" + userID + "' and Dt = '" + newDt.Date + "' and Policycode ='" + policyCode + "'";

                        int res = ExecuteNonQuery();
                        if (res == 1)
                            flag = true;
                    }
                    if (!String.IsNullOrEmpty(BH))
                    {
                        var totalBreakHour = Formatter.SetValidValueToDateTime(BH).TimeOfDay;
                        query = @"Update  [dbo].[tbl_policyWithEmployee] set  Breakhour = '" + totalBreakHour + "'  where Emp_id ='" + userID + "' and Dt = '" + newDt.Date + "' and Policycode ='" + policyCode + "'";

                        int res = ExecuteNonQuery();
                        if (res == 1)
                            flag = true;
                    }
                    if (!String.IsNullOrEmpty(EH))
                    {
                        var getExtraHour = extra_hour == "" ? 0 : getTotalSeconds(extra_hour);
                        var totalSeconds = getTotalSeconds(EH) + getExtraHour;
                        var foundEX = gettingHours_min_sec(totalSeconds).Split(':');
                        var totalExtraHour = foundEX[0] + ":" + foundEX[1] + ":" + foundEX[2];
                        query = @"Update  [dbo].[tbl_policyWithEmployee] set  Extrahour = '" + totalExtraHour + "'  where Emp_id ='" + userID + "' and Dt = '" + newDt.Date + "' and Policycode ='" + policyCode + "'";

                        int res = ExecuteNonQuery();
                        if (res == 1)
                            flag = true;
                    }

                    //TimeSpan totalBreakHour = new TimeSpan();
                    //if (totalBreakHour_Seconds != 0)
                    //{
                    //    totalBreakHour = Formatter.SetValidValueToDateTime(BreakHour).TimeOfDay + Formatter.SetValidValueToDateTime(break_hour).TimeOfDay;
                    //    var totalExtraHour = Formatter.SetValidValueToDateTime(ExtraHour).TimeOfDay;
                    //    var totalWorkHour = Formatter.SetValidValueToDateTime(WorkHour).TimeOfDay;
                    //    var totalOverTime = Formatter.SetValidValueToDateTime(Overtime).TimeOfDay;

                    //    query = @"Update  [dbo].[tbl_policyWithEmployee] set  WorkHour = '" + totalWorkHour.ToString() + "'" +
                    //        ",Overtime='" + totalOverTime.ToString() + "'" +
                    //        ",Breakhour='" + totalBreakHour.ToString() + "'" +
                    //        ",Extrahour='" + totalExtraHour.ToString() + "' where Emp_id ='" + log.UserID + "' and Dt = '" + newDt.Date + "' and Policycode ='" + PolicyCode + "'";
                    //    int res = ExecuteNonQuery();
                    //    if (res == 1)
                    //        flag = true;
                    //}
                    //else
                    //{
                    //    var totalExtraHour = Formatter.SetValidValueToDateTime(ExtraHour).TimeOfDay;
                    //    var totalWorkHour = Formatter.SetValidValueToDateTime(WorkHour).TimeOfDay;
                    //    var totalOverTime = Formatter.SetValidValueToDateTime(Overtime).TimeOfDay;

                    //    query = @"Update  [dbo].[tbl_policyWithEmployee] set  WorkHour = '" + totalWorkHour.ToString() + "'" +
                    //        ",Overtime='" + totalOverTime.ToString() + "'" +
                    //        ",Extrahour='" + totalExtraHour.ToString() + "' where Emp_id ='" + log.UserID + "' and Dt = '" + newDt.Date + "' and Policycode ='" + PolicyCode + "'";
                    //    int res = ExecuteNonQuery();
                    //    if (res == 1)
                    //        flag = true;
                    //}


                }
                else
                {
                    query = @"Insert into [dbo].[tbl_policyWithEmployee] 
                                    (Emp_id, Dt, Workhour, Overtime, Breakhour, Extrahour, Policycode, CreatedOn)  VALUES  
                                       ('" + userID + "'" +
                      ",'" + newDt.Date + "'" +
                      ",'" + WH + "'" +
                      ",'" + OT + "'" +
                      ",'" + BH + "'" +
                      ",'" + EH + "'" +
                      ",'" + policyCode + "'" +
                      ",'" + DateTime.Now + "')";

                    SqlConnection con = new SqlConnection(conStr);
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                }
                //else
                //{
                //    query = @"Insert into [dbo].[tbl_policyWithEmployee] 
                //                    (Emp_id, Dt, Workhour, Overtime, Breakhour, Extrahour, Policycode, CreatedOn)  VALUES  
                //                       ('" + userID + "'" +
                //      ",'" + newDt.Date + "'" +
                //      ",'" + WorkHour + "'" +
                //      ",'" + Overtime + "'" +
                //      ",'" + BreakHour + "'" +
                //      ",'" + ExtraHour + "'" +
                //      ",'" + PolicyCode + "'" +
                //      ",'" + DateTime.Now + "')";

                //    SqlConnection con = new SqlConnection(conStr);
                //    con.Open();
                //    SqlCommand cmd = new SqlCommand(query, con);
                //    cmd.ExecuteNonQuery();
                //}
            }
            catch (Exception ex)
            {

                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }



        }

        public double ConvertMinutesToMilliseconds(string hours, string minutes)
        {
            if (hours != "00")
            {
                var min = Convert.ToInt32(minutes);
                var hour = Convert.ToInt32(hours);
                var total = (TimeSpan.FromMinutes(Convert.ToInt32(min)).TotalSeconds) + (TimeSpan.FromHours(Convert.ToInt32(hour)).TotalSeconds);
                return total;
            }
            else
            {
                return (int)TimeSpan.FromMinutes(Convert.ToInt32(minutes)).TotalSeconds;
            }


        }

        public string gettingHours_min_sec(double seconds)
        {
            var totalSec = TimeSpan.FromSeconds(seconds);
            string total = totalSec.ToString(@"hh\:mm\:ss");

            return total;
        }


        public double getTotalSeconds(string time)
        {
            double seconds = TimeSpan.Parse(time).TotalSeconds;
            return seconds;
        }
    }
}
