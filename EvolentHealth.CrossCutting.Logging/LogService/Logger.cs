using EvolentHealth.CrossCutting.Logging.LogInterface;
using log4net;

namespace EvolentHealth.CrossCutting.Logging.LogService
{
	public class Logger : ILogger
	{
		private readonly ILog _log = LogManager.GetLogger("Log");

		static Logger()
		{
			log4net.Config.XmlConfigurator.Configure();
		}

		public void LogInfo(string message, params object[] args)
		{
			_log.Info(string.Format(message, args));
		}

		public void LogError(string message, params object[] args)
		{
			_log.Error(string.Format(message, args));
		}
	}
}
