using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //RunCPUTest(TimeSpan.FromSeconds(30));
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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