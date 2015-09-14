using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Burner
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BurnUntilStop();
        }

        public void BurnUntilStop()
        {
            Task.Run(() =>
            {
                while (true) {
                    RunCPUTest(TimeSpan.FromSeconds(30));
                }
            });
        }

        public void RunCPUTest(TimeSpan duration)
        {
            int COUNT = 10; //How many times to call burn()
            Task[] tasks = new Task[COUNT];
            for (int i = 0; i < COUNT; i++)
            {
                tasks[i] = Task.Factory.StartNew(Burn, duration);
                Trace.TraceInformation("HALib::TestLibrary::RunCPUTest(): start test " + i);
            }
            Task.WaitAll(tasks);
        }

        // This code was copied from https://github.com/t-sterns/HALibrary/blob/master/TestLibrary.cs
        private void Burn(object obj)
        {
            TimeSpan duration = (TimeSpan)obj;

            int number = 1000000;
            Trace.TraceInformation("HALib::TestLibrary::Burn(): Start running for " + duration.TotalSeconds + "seconds");
            DateTime dt = DateTime.Now;
            DateTime dtplus = dt.AddSeconds(duration.TotalSeconds);
            var count = 0;
            do
            {
                number *= number;
                count++;
                if (count % 10000000 == 0)
                {
                    DateTime n = DateTime.Now;

                    if (n.CompareTo(dtplus) > 0)
                    {
                        Trace.TraceInformation("CPU Burn Complete");
                        break;
                    }
                }
            } while (true);
        }
    }
}
