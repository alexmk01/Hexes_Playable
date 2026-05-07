using System;
using System.Diagnostics;
using UnityEngine;

namespace ModestTree
{
	public static class Log
	{
		[Conditional("UNITY_EDITOR")]
		public static void Debug(string message, params object[] args)
		{
		}

		public static void Info(string message, params object[] args)
		{
			UnityEngine.Debug.Log(message.Fmt(args));
		}

		public static void Warn(string message, params object[] args)
		{
			UnityEngine.Debug.LogWarning(message.Fmt(args));
		}

		public static void Trace(string message, params object[] args)
		{
			UnityEngine.Debug.Log(message.Fmt(args));
		}

		public static void ErrorException(Exception e)
		{
			UnityEngine.Debug.LogException(e);
		}

		public static void ErrorException(string message, Exception e)
		{
			UnityEngine.Debug.LogError(message);
			UnityEngine.Debug.LogException(e);
		}

		public static void Error(string message, params object[] args)
		{
			UnityEngine.Debug.LogError(message.Fmt(args));
		}
	}
}
