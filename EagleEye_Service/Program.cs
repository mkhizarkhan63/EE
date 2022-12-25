using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EagleEye_Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Thread createThreadService = new Thread(() =>
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new Service1()
                };
                ServiceBase.Run(ServicesToRun);
            });
            createThreadService.SetApartmentState(ApartmentState.STA);
            createThreadService.Start();

            //Service1 myServ = new Service1();
            //System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
        }
    }
}
