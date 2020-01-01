using System;

namespace NautilusLite.Core
{
	public class NautilusReturnResult
	{
		public NautilusReturnResult(bool isSuccessful)
		{
			IsSuccessful = isSuccessful;
		}

		internal void SetException(Exception ex)
		{
			Exception = ex;
		}

		internal void SetMessage(string message)
		{
			Message = message;
		}

		public bool IsSuccessful { get; private set; }
		public Exception Exception { get; private set; }
		public string Message { get; private set; }

		public static NautilusReturnResult ReturnSuccess()
		{
			return new NautilusReturnResult(true);
		}

		internal static NautilusReturnResult ReturnException(Exception ex)
		{
			var result = new NautilusReturnResult(false);
			result.SetException(ex);

			return result;
		}

		internal static NautilusReturnResult ReturnErrorMessage(string message)
		{
			var result = new NautilusReturnResult(false);
			result.SetMessage(message);

			return result;
		}
	}
}