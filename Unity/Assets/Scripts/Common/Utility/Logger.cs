using UnityEngine;

using System;

namespace Curan.Utility
{
	public static class Logger
	{
		public delegate void OutLog( object message );
		public delegate void OutLogException( Exception exception );

		public static OutLog LogNormal;
		public static OutLog LogWarning;
		public static OutLog LogError;
		public static OutLog LogDebug;
		public static OutLogException LogException;

		static Logger()
		{
			LogNormal = LogNull;//Debug.Log;
			LogWarning = LogNull;//Debug.LogWarning;
			LogError = LogErrorBreak;
			LogDebug = LogDebugBreak;
			LogException = Debug.LogException;
		}

		public static void LogNull( object message )
		{

		}

		public static void LogNull( Exception exception )
		{

		}

		public static void LogDebugBreak( object message )
		{
			Debug.Log( "[Debug]:" + message );
		}

		public static void LogErrorBreak( object message )
		{
			Debug.LogError( message );
		}
	}
}
