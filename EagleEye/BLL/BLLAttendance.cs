
using EagleEye.DAL;
using EagleEye.DAL.Partial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.ExceptionLogger;
using static EagleEye.Common.Enumeration;
using static Common.HelpingMethod;
using EagleEye.Common;
using EagleEye.DAL.DTO;

namespace EagleEye.BLL
{
    public class BLLAttendance
    {
        DALAttendance objDAL = new DALAttendance();

        public List<Attendance_P> GetAllAttendance()
        {
            List<Attendance_P> list = new List<Attendance_P>();
            try
            {
                list = objDAL.GetAllAttendance();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;
        }

        public List<Attendance_P> GetAttendanceByUser(string Employee_ID)
        {
            List<Attendance_P> att = new List<Attendance_P>();
            try
            {
                att = objDAL.GetAttendanceByUser(Employee_ID);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;
        }

        public List<Attendance_P> GetAttendanceByDevice(string DeviceID)
        {
            List<Attendance_P> att = new List<Attendance_P>();
            try
            {
                att = objDAL.GetAttendanceByDevice(DeviceID);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;
        }

        public List<Attendance_P> GetAttendanceByDateForList(string start, string end)
        {
            List<Attendance_P> ls = new List<Attendance_P>();
            try
            {
                ls = objDAL.GetAttendanceByDateForList(start, end);
            }
            catch (Exception ex)
            {

                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return ls;
        }


        public List<Attendance_P> GetAttendanceByDate(JqueryDatatableParam param, string start, string end, out int TotalRecords)
        {
            TotalRecords = 0;
            List<Attendance_P> att = new List<Attendance_P>();
            try
            {
                att = objDAL.GetAttendanceByDate(param, start, end, out TotalRecords);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;
        }

        public List<Attendance_P> GetAttendanceByDate(string start, string end)
        {

            List<Attendance_P> att = new List<Attendance_P>();
            try
            {
                att = objDAL.GetAttendanceByDate(start, end);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;
        }
        //public List<Attendance_P> GetAttendanceByDateWStranger(string start, string end)
        //{
        //    List<Attendance_P> att = new List<Attendance_P>();
        //    try
        //    {
        //        att = objDAL.GetAttendanceByDateWStranger(start, end);

        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
        //    }
        //    return att;
        //}

        public bool AddAttendance(Attendance_P attendance)
        {
            bool flag = false;
            try
            {
                flag = objDAL.AddAttendance(attendance);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool UpdateAttendance(string Device_ID, string Transaction_ID, int status)
        {

            bool flag = false;
            try
            {
                flag = objDAL.UpdateAttendance(Device_ID, Transaction_ID, status);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;

        }

        public bool AddAttendanceWODuplication(Attendance_P attendance)
        {
            bool flag = false;
            try
            {
                flag = objDAL.AddAttendanceWODuplication(attendance);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public List<Att_Graph> GetAttendanceByMonths()
        {
            List<Att_Graph> data = new List<Att_Graph>();
            try
            {
                data = objDAL.GetAttendanceByMonths();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return data;
        }

        public bool DeleteAttendance(int Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteAttendance(Code);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        //public Attendance_P GetStrangerRecordByDate(DateTime Date)
        //{

        //    Attendance_P att = new Attendance_P();
        //    try
        //    {
        //        att = objDAL.GetStrangerRecordByDate(Date);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
        //    }
        //    return att;
        //}

        public List<Attendance_P> GetAttendanceByUSERandDATE(string Employee_ID, string dt, string[] devices)
        {
            List<Attendance_P> att = new List<Attendance_P>();
            try
            {
                att = objDAL.GetAttendanceByUSERandDATE(Employee_ID, dt, devices);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;
        }

        public List<Attendance_P> GetAttendanceByUSERandDATE(string Employee_ID, string dt)
        {
            List<Attendance_P> att = new List<Attendance_P>();
            try
            {
                att = objDAL.GetAttendanceByUSERandDATE(Employee_ID, dt);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;
        }



        public Attendance_P GetAttendanceByMINDate(string Employee_ID, string dt)
        {
            Attendance_P att = new Attendance_P();
            try
            {
                att = objDAL.GetAttendanceByMINDate(Employee_ID, dt);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;
        }

        public Attendance_P GetAttendanceByMAXDate(string Employee_ID, string dt)
        {
            Attendance_P att = new Attendance_P();
            try
            {
                att = objDAL.GetAttendanceByMAXDate(Employee_ID, dt);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;
        }


        public Attendance_P GetUnRegisteredRecordByDate(DateTime Date)
        {

            Attendance_P att = new Attendance_P();
            try
            {
                att = objDAL.GetUnRegisteredRecordByDate(Date);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;
        }

        public List<AttendanceDTO> GetAttendanceByDATEandDEVICE(string FromDate, string ToDate, string Device_ID)
        {
            List<AttendanceDTO> att = new List<AttendanceDTO>();
            try
            {
                att = objDAL.GetAttendanceByDATEandDEVICE(FromDate, ToDate, Device_ID);
            }
            catch (Exception ex)
            {

                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;
        }
    }
}