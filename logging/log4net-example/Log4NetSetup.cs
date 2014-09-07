using System;
using System.IO;
using System.Xml;

namespace log4net_example
{
    /// <summary>
    /// This class is useful if you are initializing log4 net from an assembly and the host
    /// application is not in your control.
    /// 
    /// It tries to find a log4net.xml in the same directory as the current assembly and calls
    /// configure on that.
    /// 
    /// This example assumes your log4net.xml will have
    /// 
    /// file type="log4net.Util.PatternString" value="${LOCALAPPDATA}\MyApp\Logs\CityIndex.Build.Model-[%processid].log"W
    /// 
    /// Note the processid above is to allow multiple instances of the host application
    /// </summary>
    public static class Log4NetSetup
    {
        public static void Initialize()
        {
            DeleteFilesNotInUse();
            var doc = new XmlDocument();
            var location = new FileInfo(typeof(Log4NetSetup).Assembly.Location).DirectoryName;

            var log4netLocation = Path.Combine(location, "log4net.xml");

            if (!File.Exists(log4netLocation))
            {
                return;
            }

            doc.LoadXml(File.ReadAllText(log4netLocation));
            log4net.Config.XmlConfigurator.Configure(doc.DocumentElement);
        }

        private static void DeleteFilesNotInUse()
        {
            try
            {
                var logDir = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), @"MyApp\Logs");
                var files = Directory.GetFiles(logDir);

                foreach (var file in files)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            catch
            {

            }
        }
    }
}