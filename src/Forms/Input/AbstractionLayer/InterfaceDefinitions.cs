using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NautilusLite.Forms.Input.AbstractionLayer
{
	public interface IAsyncCommand : ICommand
	{
		Task ExecuteAsync();
		bool CanExecute();
	}

	public interface IAsyncCommand<T> : ICommand
	{
		Task ExecuteAsync(T parameter);
		bool CanExecute(T parameter);
	}

	public interface IErrorHandler
	{
		void HandleError(Exception ex);
	}
}