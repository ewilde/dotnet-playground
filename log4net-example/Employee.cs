using log4net;

namespace log4net_example
{
    public class Employee
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Employee));
    
        public Employee() 
        {
            _log.Debug("Employee constructor called.");
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat("Logging thing with expensive tostring lookup {0}", "That took a long time");
            }            
        }
    }
}