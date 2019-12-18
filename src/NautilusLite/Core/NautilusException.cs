using System;

namespace NautilusLite.Core
{
	public class NautilusException : Exception
	{
		public NautilusException()
		{
		}

		public NautilusException(string message) : base(message)
		{

		}

		public NautilusException(string message, Exception innerException) : base(message, innerException)
		{

		}
	}
}