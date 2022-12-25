using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Threading.Tasks;
using Common;
using EagleEye.DAL.Partial;
using System.Reflection;
using EagleEye_Service;

namespace EagleEye.Hubs
{
    [HubName("WebHub")]
    public class WebHub : Hub
    {
        public void SendUserTransferStatus(string code, bool status, string device, string totaluser, string progress, double percent, string msg)
        {
            try
            {
                Clients.All.sendUserTransferStatus(code, status, device, totaluser, progress, percent, msg);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Hub, GetCurrentMethod());
            }
        }

        public void EventLogs(EventLogs logs, double percent, int progress, int mcode, string connectionid, EventLogs totalLogs)
        {
            try
            {
                Clients.Client(connectionid).eventLogs(logs, percent, progress, mcode, totalLogs);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Hub, GetCurrentMethod());
            }
        }

        public void PollLogs(EventLogs logs, double percent, int progress, int mcode, EventLogs totalLogs)
        {
            try
            {
                Clients.All.pollLogs(logs, percent, progress, mcode, totalLogs);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Hub, GetCurrentMethod());
            }
        }

        public void EmployeesID(List<Employee_P> emp)
        {
            try
            {
                Clients.All.employeeids(emp);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void AttendenceStatus(List<Att_Status_P> att)
        {
            try
            {
                Clients.All.attendancestatus(att);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        public void SendUserDeleteStatus( Employee_P emp, bool status, string device, string totaluser, string progress, double percent, string msg)
        {
            try
            {
                Clients.All.sendUserDeleteStatus(emp, status, device, totaluser, progress, percent, msg);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Hub, GetCurrentMethod());
            }
        }

        public void DeletedUsers(Employee_P employee, int count, int total)
        {
            try
            {
                Clients.All.deletedUsers(employee, count, total);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Hub, GetCurrentMethod());
            }
        }

        public void SendReportToCilent(Report_P rpt, int count)
        {
            try
            {
                Clients.All.sendreporttocilent(rpt, count);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Hub, GetCurrentMethod());
            }
        }
    }
}