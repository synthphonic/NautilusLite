using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NautilusLite.Infrastructure
{
	public static class DebugOutput
	{
		public static string GetClassAndMemberName(Type type, [CallerMemberName] string callerName = null)
		{
			return $"[{type.Name}.{callerName}]";
		}

		public static string GetTypeString(Type type, [CallerMemberName] string callerName = null)
		{
			return $"[{type.Name}.{callerName}]";
		}

		[Conditional("DEBUG")]
		public static void Write(Type type, string message = "", [CallerMemberName] string callerName = null)
		{
			var outputMessage = string.Empty;

			if (string.IsNullOrWhiteSpace(message))
			{
				outputMessage = $"[{type.Name}.{callerName}]";
			}
			else
			{
				outputMessage = $"[{type.Name}.{callerName}] : {message}";
			}

			Debug.WriteLine(outputMessage);
		}

		[Conditional("DEBUG")]
		public static void Write(Type type, string message, Exception exceptionThrown, [CallerMemberName] string callerName = null)
		{
			var outputMessage = exceptionThrown != null ?
				$"[{type.Name}.{callerName}] : {exceptionThrown.GetType().ToString()} : {message}" :
				$"[{type.Name}.{callerName}] : {message}";

			Debug.WriteLine(outputMessage);
		}

		[Conditional("DEBUG")]
		public static void Write(string message, [CallerMemberName] string callerName = null)
		{
			Write(message, null, callerName);
		}

		[Conditional("DEBUG")]
		public static void Write(Exception exceptionThrown, [CallerMemberName] string callerName = null)
		{
			Write(exceptionThrown.Message, exceptionThrown, callerName);
		}

		[Conditional("DEBUG")]
		public static void Write(string message, Exception exceptionThrown, [CallerMemberName] string callerName = null)
		{
			var outputMessage = exceptionThrown != null ?
				$"[{callerName}] : {exceptionThrown.GetType().ToString()} : {message}" :
				$"[{callerName}] : {message}";

			Debug.WriteLine(outputMessage);
		}

		[Conditional("DEBUG")]
		public static void WriteForStaticMethod(string classAndMemberName, string message)
		{
			Debug.WriteLine($"{classAndMemberName} : {message}");
		}
	}
}
