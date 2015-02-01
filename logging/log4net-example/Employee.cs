using log4net;

namespace log4net_example
{
    public class Employee
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Employee));
    
        public Employee() 
        {
            Log.Debug("Employee constructor called.");
            if (Log.IsDebugEnabled)
            {
                Log.DebugFormat("Logging thing with expensive tostring lookup {0}", "That took a long time");
            }            
        }
    }
}