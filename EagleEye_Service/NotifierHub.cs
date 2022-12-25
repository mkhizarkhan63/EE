using Common;
using EagleEye_Service.Entity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service
{
    [HubName("NotifierHub")]
    public class NotifierHub : Hub
    {
        public void SendAwaitingDevices(string DeviceID, string IpAddress)
        {
            try
            {
                Clients.All.sendAwaitingDevices(DeviceID, IpAddress);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }

        public void RefreshAwaitingDevices()
        {
            try
            {
                Clients.All.refreshAwaitingDevices();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }

        public void RefreshConnectedDevices(string code, bool flag)
        {
            try
            {
                Clients.All.refreshConnectedDevices(code, flag);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }

        public void SendDeviceSetting(string Setting, string asDevId)
        {
            try
            {
                Clients.All.sendDeviceSetting(Setting, asDevId);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void SendUserIdList(List<Employee> emp)
        {
            try
            {
                Clients.All.senduseridlist(emp);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }
        public void SendUserTransferStatus(string user_id, string user_name, string asDevId, string deviceName, string msg)
        {
            try
            {
                Clients.All.sendUserTransferStatus(user_id, user_name, asDevId, deviceName, msg);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        public void SendTZTransferStatus(string timezone_no, string asDevId, string deviceName, string msg)
        {
            try
            {
                Clients.All.sendTZTransferStatus(timezone_no, asDevId, deviceName, msg);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        public void SendDeviceStatusInfo(string Setting, string asDevId)
        {
            try
            {
                Clients.All.sendDeviceStatusInfo(Setting, asDevId);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void SendTimeZoneInfo(string tz, string asDevId)
        {
            try
            {
                Clients.All.sendTimeZoneInfo(tz, asDevId);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void SendPassTimeInfo(string pt, string asDevId)
        {
            try
            {
                Clients.All.sendPassTimeInfo(pt, asDevId);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void SendOperationLog(string trans_id, string status,string msg,string devstatus)
        {
            try
            {
                Clients.All.sendOperationLog(trans_id,status, msg, devstatus);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        
        public void EventLogs(EventLogs logs)
        {
            try
            {

                Clients.All.eventLogs(logs);

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void ManualLogs(EventLogs logs, int count)
        {
            try
            {

                Clients.All.manualLogs(logs, count);

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        //public void SendReportToCilent(Reports_ rpt, int count)
        //{
        //    try
        //    {
        //        Clients.All.sendreporttocilent(rpt, count);

        //    }
        //    catch (Exception ex)
        //    {
        //        clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
        //    }
        //}

        //public void PollLogs(EventLogs logs)
        //{
        //    try
        //    {
        //        Clients.All.polllogs(logs);
        //    }
        //    catch (Exception ex)
        //    {
        //        clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
        //    }
        //}

        public void PollLogs(EventLogs logs, double percent, int progress, int mcode, EventLogs totalLogs)
        {
            try
            {
                Clients.All.polllogs(logs, percent, progress, mcode, totalLogs);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        //public void EmployeesID(EmployeeInfo emp)
        //{
        //    try
        //    {
        //        Clients.All.employeeids(emp);
        //    }
        //    catch (Exception ex)
        //    {
        //        clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
        //    }
        //}

        //public void ManagerID(Manager admin)
        //{
        //    try
        //    {
        //        Clients.All.managerids(admin);
        //    }
        //    catch (Exception ex)
        //    {
        //        clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
        //    }
        //}

        public override async Task OnConnected()
        {
            await Groups.Add(Context.ConnectionId, "Notifier Hub Connected.");
            await base.OnConnected();
        }
        public override async Task OnDisconnected(bool stopCalled)
        {
            await base.OnDisconnected(stopCalled);
        }
    }
}
