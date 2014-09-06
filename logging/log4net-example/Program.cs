using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace log4net_example
{
    class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            // instead of calling this line you could just put this in AssemblyInfo.cs
            // [assembly: log4net.Config.XmlConfigurator(Watch = true)]
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.GetFullPath(@"log4net-example.exe.config")));


            new Employee();
            _log.Info("Useful stuff");
            _log.Warn("Be aware this could be important");
            _log.Error("This shouldn't have happended!");
            _log.Fatal("OMG totally going to halt this processes, this is FATAL!");
         
            Console.ReadKey();
        }
    }
}
