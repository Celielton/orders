using log4net;
using log4net.Config;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;

namespace OrdersApplications.SharedKernel
{

    public class LoggerConfiguration
    {
        static ILog _logger;
        public static ILog CreateLogger()
        {
            if (_logger is null)
            {
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
                _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            }
            return _logger;
        }
    }
}
