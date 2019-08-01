using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace nlog
{
    class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                Logger.Info("Application started");

                Console.WriteLine("Current date and time is: ");
                DateTime now = DateTime.Now;
                Console.WriteLine(now);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Logger.Fatal(e, "An unexpected exception has occured");
            }
            finally
            {
                Logger.Info("Application terminated. Press enter to exit");
                Console.ReadLine();
            }


        }
    }
}
