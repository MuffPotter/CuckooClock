using System;
using System.Diagnostics;
using MetroLog;

namespace Sigeko.CuckooClock.Common
{
	/// <summary>
	/// Der Logger, Implementierung als Singleton
	/// http://geekswithblogs.net/EltonStoneman/archive/2009/04/15/making-logging-cheaper-with-lambda-expressions.aspx
	/// </summary>
	public class Logger : IDisposable
	{
		#region fields (private)

		/// <summary>
		/// Flag, ob das Logging überhaut möglich ist
		/// </summary>
		private bool _loogingEnabled;

		/// <summary>
		/// Flag, ob der DesignMode aktiv ist
		/// </summary>
		private readonly bool _isDesignMode = false;

		private static int _counter;

		private static string GetCount => $", Count:{_counter++}";

		#endregion fields (private)

		#region ctor

		/// <summary>
		/// Instanz für das Singelton Pattern
		/// see: http://msdn.microsoft.com/en-us/library/ff650316.aspx
		/// </summary>
		private static volatile Logger _instance;

		/// <summary>
		/// Locking Object
		/// </summary>
		private static readonly object SyncRoot = new object();

		private ILogger MetroLogger { get; set; }

		/// <summary>
		/// Privater statischer Konstruktur
		/// </summary>
		private Logger()
		{
			// If the app is being launched (not resumed), the 
			// following call will activate logging if it had been
			// activated during the last suspend. 
			ResumeLoggingIfApplicable();
		}

		/// <summary>
		/// Öffentliche Instanz der statischen Klasse
		/// </summary>
		public static Logger Current
		{
			get
			{
				if (_instance != null)
					return _instance;

				lock (SyncRoot)
				{
					if (_instance == null)
					{
						_instance = new Logger();
					}
				}

				return _instance;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		~Logger()
		{
			Dispose(false);
		}

		#endregion ctor

		#region Event Handling

		public bool IsLoggingEnabled => _loogingEnabled == true;

		public void PrepareToSuspend()
		{
			CheckDisposed();
		}

		public void ResumeLoggingIfApplicable()
		{
			CheckDisposed();
		}

		#endregion Event Handling

		#region methods (public)

		public void StartLogging(LoggingConfiguration settings)
		{
			// This sample adds the channel at level "warning" to 
			// demonstrated how messages logged at more verbose levels
			// are ignored by the session. 
			StartLogging(settings, LogLevel.Trace);
		}

		public void StartLogging(LoggingConfiguration settings, LogLevel logLevel)
		{
			if (_isDesignMode == true)
				return;

			CheckDisposed();

			IsBusy = true;
			_counter = 0;

			try
			{
				//LoggingConfiguration settings = LogManagerFactory.CreateLibraryDefaultSettings();

				//FileStreamingTarget target = new FileStreamingTarget { RetainDays = 10 };
				//target.FileNamingParameters.IncludeTimestamp = FileTimestampMode.Date;
				//target.FileNamingParameters.IncludeSession = true;

				//settings.AddTarget(logLevel, LogLevel.Fatal, target);
				settings.IsEnabled = true;
				_loogingEnabled = true;

				MetroLogger = LogManagerFactory.CreateLogManager(settings).GetLogger<Logger>();

				_loogingEnabled = true;
			}
			finally
			{
				IsBusy = false;
			}
		}

		public void StopLogging()
		{
			StopLogging(true);
		}

		public void StopLogging(bool saveStatus)
		{
			if (_isDesignMode == true)
				return;

			CheckDisposed();

			try
			{
				StatusChanged?.Invoke(this, new LoggingEventArgs(false));

				if (saveStatus == true)
				{
					_loogingEnabled = false;
				}
			}
			finally
			{
				IsBusy = false;
			}
		}

		public void LogTrace(string message)
		{
			if (_isDesignMode == true || IsLoggingEnabled == false)
				return;

			if (MetroLogger.IsTraceEnabled)
				MetroLogger.Trace(GetIndend() + message + GetCount);
		}

		public void LogInfo(Func<string> func)
		{
			if (_isDesignMode == true || IsLoggingEnabled == false)
				return;

			if (!MetroLogger.IsInfoEnabled)
				return;

			string message = func() + GetCount;
			MetroLogger.Info(GetIndend() + message);
		}

		public void LogInfo(string message)
		{
			if (_isDesignMode == true || IsLoggingEnabled == false)
				return;

			if (MetroLogger.IsInfoEnabled)
				MetroLogger.Info(GetIndend() + message + GetCount);
		}

		public void LogWarning(string message)
		{
			if (_isDesignMode == true || IsLoggingEnabled == false)
				return;

			if (MetroLogger.IsWarnEnabled)
				MetroLogger.Warn(GetIndend() + message + GetCount);
		}

		public void LogError(string message)
		{
			if (_isDesignMode == true || IsLoggingEnabled == false)
				return;

			if (MetroLogger.IsErrorEnabled)
				MetroLogger.Error(GetIndend() + message + GetCount);
		}

		public void LogFatal(string message)
		{
			if (_isDesignMode == true || IsLoggingEnabled == false)
				return;

			if (MetroLogger.IsFatalEnabled)
				MetroLogger.Fatal(GetIndend() + message + GetCount);
		}

		public void LogException(Exception exception)
		{
			if (_isDesignMode == true || IsLoggingEnabled == false)
			{
				return;
			}

			if (MetroLogger.IsFatalEnabled)
				MetroLogger.Fatal(exception.Message, exception);

			Debugger.Break();
		}

		private static string GetIndend()
		{
			string indend = "";
			int currentIndend = 0; //PerformanceTimerHelper.CurrentIndend + 1;
			for (int i = 0; i < currentIndend; i++)
			{
				indend += "\t";
			}
			return indend;
		}

		#endregion methods (public)

		#region fields (public)

		public event EventHandler<LoggingEventArgs> StatusChanged;

		private bool _isBusy;

		/// <summary>
		/// True if the scenario is busy, or false if not busy.
		/// The UI can use this to affect UI controls.
		/// </summary>
		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}

			private set
			{
				_isBusy = value;
				StatusChanged?.Invoke(this, new LoggingEventArgs(LoggingEventType.BusyStatusChanged));
			}
		}

		#endregion fields (public)

		#region IDisposable handling

		/// <summary>
		/// Set to 'true' if Dispose() has been called.
		/// </summary>
		private bool _isDisposed;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_isDisposed == false)
			{
				_isDisposed = true;

				if (disposing)
				{
				}
			}
		}

		/// <summary>
		/// Helper function for other methods to call to check Dispose() state.
		/// </summary>
		private void CheckDisposed()
		{
			if (_isDisposed)
			{
				throw new ObjectDisposedException("Logger");
			}
		}

		#endregion
	}

	#region LoggingEventArgs

	/// <summary>
	/// LoggingScenario tells the UI what's happening by 
	/// using the following enums. 
	/// </summary>
	public enum LoggingEventType
	{
		BusyStatusChanged,
		LogFileGenerated,
		LoggingEnabledDisabled
	}

	public class LoggingEventArgs : EventArgs
	{
		public LoggingEventArgs(LoggingEventType type)
		{
			Type = type;
		}

		public LoggingEventArgs(LoggingEventType type, string logFilePath)
		{
			Type = type;
			LogFilePath = logFilePath;
		}

		public LoggingEventArgs(bool enabled)
		{
			Type = LoggingEventType.LoggingEnabledDisabled;
			Enabled = enabled;
		}

		public LoggingEventType Type { get; private set; }

		public string LogFilePath { get; private set; }

		public bool Enabled { get; private set; }
	}

	#endregion LoggingEventArgs
}
