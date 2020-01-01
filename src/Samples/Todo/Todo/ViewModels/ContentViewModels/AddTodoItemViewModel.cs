using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using NautilusLite.Forms.Dialog;
using NautilusLite.Forms.Input;
using NautilusLite.Forms.Mvvm.Navigation;
using NautilusLite.Infrastructure;
using Todo.Database;
using Todo.Models;
using Todo.Views.ContentViews;
using Xamarin.Forms;

namespace Todo.ViewModels.ContentViewModels
{
	public class AddTodoItemViewModel : ViewModelBase
	{
		private readonly INavigationService _navigator;
		private ICommand _saveNewTodoItemCommand;
		private ICommand _setFavoriteCommand;
		private AddTodoItemCV _view;
		private string _message;
		private string _description;
		private string _shortDescription;
		private string _title;
		private string _name;
		private bool _isFavorite;

		public AddTodoItemViewModel()
		{
			_navigator = SimpleIoc.Default.GetInstance<INavigationService>();
		}

		internal void SetView(AddTodoItemCV view)
		{
			_view = view;
		}

		internal void SetNewData()
		{
			IsFavorite = false;
			ToggleFavorite(IsFavorite);
		}

		internal void ClearData()
		{
			//_newtTodoItem = null;
		}

		private void ToggleFavorite(bool isFavorite)
		{
			_view.SetFavorite(isFavorite);
		}

		#region Binding properties
		public string Message
		{
			get { return _message; }
			set { Set(ref _message, value); }
		}

		public string Name
		{
			get => _name;
			set => Set(ref _name, value);
		}

		public string Description
		{
			get => _description;
			set => Set(ref _description, value);
		}

		public string ShortDescription
		{
			get => _shortDescription;
			set => Set(ref _shortDescription, value);
		}

		public string Title
		{
			get => _title;
			set => Set(ref _title, value);
		}

		public bool IsFavorite
		{
			get => _isFavorite;
			set => Set(ref _isFavorite, value);
		}

		#endregion

		#region SaveNewTodoItemCommand
		public ICommand SaveNewTodoItemCommand
		{
			get => _saveNewTodoItemCommand ?? (_saveNewTodoItemCommand = new AsyncCommand(DoSaveNewTodoItem));
		}

		private async Task DoSaveNewTodoItem()
		{
			var newTodoItem = new TodoItem
			{
				Id = Guid.NewGuid(),
				Description = Description,
				ShortDescription = ShortDescription,
				Name = Name,
				Due = DateTime.Today,
			};

			try
			{
				TodoRepositoryContext<TodoItem>.Instance.Save(newTodoItem);
			}
			catch (Exception ex)
			{
				DebugOutput.Write(GetType(), $"{ex.Message}");
			}

			await Task.Delay(1);

			MessagingCenter.Send<string>("SlideDownView", "SlideDownView");

			PopupDialog.ShowToast("Todo item saved", Color.Blue, Color.White, TimeSpan.FromSeconds(1));
		}
		#endregion

		#region SetFavoriteCommand
		public ICommand SetFavoriteCommand { get => _setFavoriteCommand ?? (_setFavoriteCommand = new Command(ToggleFavorite)); }

		private void ToggleFavorite()
		{
			IsFavorite = !IsFavorite;
			_view.SetFavorite(IsFavorite);
		}
		#endregion
	}
}