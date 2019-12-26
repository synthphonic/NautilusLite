using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using NautilusLite.Forms.Dialog;
using NautilusLite.Forms.Input;
using NautilusLite.Forms.Mvvm.Navigation;
using Todo.Database;
using Todo.Models;
using Todo.Views;
using Todo.Views.ViewParameters;
using Xamarin.Forms;

namespace Todo.ViewModels
{
	public class TodoItemDetailViewModel : ViewModelBase
	{
		private readonly INavigationService _navigator;
		private ITodoItemDetailView _view;
		private TodoItemParameter _parameter;
		private TodoItem _todoItem;
		private ICommand _saveItemCommand;
		private ICommand _favoriteCommand;

		public TodoItemDetailViewModel()
		{
			_navigator = SimpleIoc.Default.GetInstance<INavigationService>();
		}

		internal void SetView(ITodoItemDetailView view)
		{
			_view = view;
		}

		internal void SetViewParameter(TodoItemParameter parameter)
		{
			_parameter = parameter;
		}

		internal void Load()
		{
			var found = TodoRepository.Instance.Get(_parameter.Id);
			if (found == null)
			{
				PopupDialog.ShowAlert("Todo Item Not Found", "Opss sorry item not found");
				return;
			}

			Item = found;
		}

		#region Binding properties
		public TodoItem Item
		{
			get { return _todoItem; }
			set { Set(ref _todoItem, value); }
		}
		#endregion

		#region SaveItemCommand
		public ICommand SaveItemCommand
		{
			get { return _saveItemCommand ?? (_saveItemCommand = new AsyncCommand(DoSaveAsync)); }
		}

		private async Task DoSaveAsync()
		{
			await _navigator.NavigateToAsync("Main", true);
		}
		#endregion

		#region LikeDislikeCommand
		public ICommand FavoriteCommand
		{
			get { return _favoriteCommand ?? (_favoriteCommand = new Command(IsFavorite)); }
		}

		private void IsFavorite(object parameter)
		{
			Item.IsFavorite = !Item.IsFavorite;
			_view.SetFavorite(Item.IsFavorite);
		}
		#endregion
	}
}