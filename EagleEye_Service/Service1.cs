using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EagleEye_Service
{
    public partial class Service1 : ServiceBase
    {
        private Thread th = null;
        ServiceUtility util = null;


        public Service1()
        {
            InitializeComponent();
            util = new ServiceUtility();
          //  util.Start();
        }

        protected override void OnStart(string[] args)
        {
            th = new Thread(new ThreadStart(util.Start));
            th.Start();
        }

        protected override void OnStop()
        {
            util.Stop();
            th.Abort();
        }
    }
}
