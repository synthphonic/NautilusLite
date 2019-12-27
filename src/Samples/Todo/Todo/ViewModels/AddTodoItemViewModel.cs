using GalaSoft.MvvmLight;
using Todo.Models;

namespace Todo.ViewModels
{
	public class AddTodoItemViewModel : ViewModelBase
	{
		private string _message;
		private TodoItem _newtTodoItem;

		internal void SetNewData()
		{
			_newtTodoItem = new TodoItem();
		}

		internal void ClearData()
		{
			_newtTodoItem = null;
		}

		#region Binding properties
		public TodoItem NewTodoItem
		{
			get => _newtTodoItem;
			set => Set(ref _newtTodoItem, value);
		}

		public string Message
		{
			get { return _message; }
			set { Set(ref _message, value); }
		}
		#endregion
	}
}