using System;
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
		private readonly TodoRepositoryContext<TodoItem> _repo;
		private ICommand _saveItemCommand;
		private ICommand _favoriteCommand;
		private ICommand _deleteTodoItemCommand;
		private ITodoItemDetailView _view;
		private TodoItemParameter _parameter;
		private TodoItem _todoItem;
		private bool _isFavoriteVisible;

		public TodoItemDetailViewModel()
		{
			_navigator = SimpleIoc.Default.GetInstance<INavigationService>();
			_repo = TodoRepositoryContext<TodoItem>.Instance;
		}

		internal void SetView(ITodoItemDetailView view)
		{
			_view = view;
			IsFavoriteVisible = false;
		}

		internal void SetViewParameter(TodoItemParameter parameter)
		{
			_parameter = parameter;
		}

		internal void Load()
		{
			var found = TodoRepositoryContext<TodoItem>.Instance.Get(_parameter.Id);
			if (found == null)
			{
				PopupDialog.ShowAlert("Todo Item Not Found", "Opss sorry item not found");
				return;
			}

			Item = found;
			IsFavoriteVisible = true;
			_view.SetFavorite(Item.IsFavorite);
		}

		internal void Unload()
		{
			Item = null;
		}

		#region Binding properties
		public bool IsFavoriteVisible
		{
			get => _isFavoriteVisible;
			set => Set(ref _isFavoriteVisible, value);
		}

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
			var foundTodo = TodoRepositoryContext<TodoItem>.Instance.Get(_parameter.Id);
			foundTodo.Completed = Item.Completed;
			foundTodo.Description = Item.Description;
			foundTodo.Due = Item.Due;
			foundTodo.IsFavorite = Item.IsFavorite;
			foundTodo.Name = Item.Name;
			foundTodo.ShortDescription = Item.ShortDescription;

			_repo.Save(foundTodo);

			var orangeColor = (Color)Application.Current.Resources["OrangeColor"];
			PopupDialog.ShowToast("You todo item updated", Color.Blue, orangeColor, TimeSpan.FromSeconds(3));

			await _navigator.GoBackAsync(true);
		}
		#endregion

		#region LikeDislikeCommand
		public ICommand FavoriteCommand
		{
			get { return _favoriteCommand ?? (_favoriteCommand = new Command(IsFavorite)); }
		}

		private void IsFavorite()
		{
			Item.IsFavorite = !Item.IsFavorite;
			_view.SetFavorite(Item.IsFavorite);
		}
		#endregion

		#region DeleteTodoItemCommand
		public ICommand DeleteTodoItemCommand
		{
			get { return _deleteTodoItemCommand ?? (_deleteTodoItemCommand = new Command(DeleteTodoItem)); }
		}

		private void DeleteTodoItem()
		{
			var deleted = false;

			Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
			{
				if (_todoItem != null)
				{
					PopupDialog.ShowConfirm("Delete Todo Item?", "Delete this item?", "Yes", "No", async (yesDelete) =>
						{
							if (yesDelete)
							{
								deleted = _repo.Delete(_todoItem);

								if (deleted)
								{
									await _navigator.GoBackAsync(true);
								}
							}
						});
				}
			});
		}
		#endregion
	}
}