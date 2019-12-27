using System;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace Todo.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private readonly INavigationService _navigator;		
		private string _welcomeMessage;
		private string _messageToAdd;
		private UserModel _user;
		private ObservableCollection<TodoItem> _todoList;
		private ObservableCollection<TodoItem> _favorites;
		private ObservableCollection<TodoItem> _upcoming;
		private ICommand _navigateToProfileCommand;
		private ICommand _logoutCommand;
		private ICommand _slideViewCommand;
		private ICommand _pageFaderTapCommand;
		private ICommand _navigateToDetailToDoItemCommand;
		private IMainView _view;

		public MainViewModel()
		{
			_navigator = SimpleIoc.Default.GetInstance<INavigationService>();
		}

		internal void SetView(IMainView mainView)
		{
			_view = mainView;
		}

		internal void Load()
		{
			WelcomeMessage = "Nautilus Lite SDK Reference App.";

			var user = new UserModel
			{
				Name = "ShahZ S",
				Id = Guid.NewGuid()
			};

			User = user;

			TodoList = new ObservableCollection<TodoItem>(TodoRepository.Instance.GetAll());
			Favorites = new ObservableCollection<TodoItem>(TodoList.Where(x => x.IsFavorite).ToList());

			var upcomingList = (from a in TodoList
					 where a.Due >= DateTime.Now && !a.Completed
					 select a).ToList();

			Upcoming = new ObservableCollection<TodoItem>(upcomingList);
		}

		#region Binding properties		
		public UserModel User
		{
			get { return _user; }
			set { Set(ref _user, value); }
		}
		
		public ObservableCollection<TodoItem> Upcoming
		{
			get { return _upcoming; }
			set { Set(ref _upcoming, value); }
		}

		public ObservableCollection<TodoItem> TodoList
		{
			get { return _todoList; }
			set { Set(ref _todoList, value); }
		}

		public ObservableCollection<TodoItem> Favorites
		{
			get { return _favorites; }
			set { Set(ref _favorites, value); }
		}

		public string WelcomeMessage
		{
			get { return _welcomeMessage; }
			set { Set(ref _welcomeMessage, value); }
		}

		public string MessageToAdd
		{
			get { return _messageToAdd; }
			set { Set(ref _messageToAdd, value); }
		}
		#endregion

		#region NavigateToDetailToDoItemCommand;
		public ICommand NavigateToDetailToDoItemCommand
		{
			get { return _navigateToDetailToDoItemCommand ?? (_navigateToDetailToDoItemCommand = new AsyncCommand<TodoItem>(NavigateToDetailTodoAsync)); }
		}

		private async Task NavigateToDetailTodoAsync(TodoItem todoItem)
		{
			var parameter = new TodoItemParameter
			{
				Id = todoItem.Id
			};

			await _navigator.NavigateToAsync("TodoItemDetail", parameter, true);
		}
		#endregion

		#region NavigateToProfileCommand
		public ICommand NavigateToProfileCommand
		{
			get { return _navigateToProfileCommand ?? (_navigateToProfileCommand = new AsyncCommand(NavigateToProfile)); }
		}

		private async Task NavigateToProfile()
		{
			await Task.Delay(1);
			var navigator = SimpleIoc.Default.GetInstance<INavigationService>();

			var parameter = new ProfileViewParameter
			{
				Message = MessageToAdd
			};

			await navigator.NavigateToAsync("Profile", parameter, true);
		}
		#endregion

		#region LogoutCommand
		public ICommand LogoutCommand
		{
			get { return _logoutCommand ?? (_logoutCommand = new AsyncCommand(DoLogoutAsync)); }
		}

		private async Task DoLogoutAsync()
		{
			await _navigator.NavigateAndSetAsFirstPageAsync("Login", true);
		}
		#endregion

		#region PageFaderTapCommand
		public ICommand PageFaderTapCommand
		{
			get { return _pageFaderTapCommand ?? (_pageFaderTapCommand = new AsyncCommand(TapPageFaderAsync)); }
		}

		private async Task TapPageFaderAsync()
		{
			await _view.PageFaderTappedAsync();
		}
		#endregion

		#region MakeABidCommand
		public ICommand SlideViewCommand
		{
			get { return _slideViewCommand ?? (_slideViewCommand = new AsyncCommand<Views.Enums.MainViewButtonOperation>(DoSlideView)); }
		}

		private async Task DoSlideView(Views.Enums.MainViewButtonOperation parameter)
		{
			switch(parameter)
			{
				case Views.Enums.MainViewButtonOperation.Profile:
					await _view.SlideUpProfileAsync();
					break;
				case Views.Enums.MainViewButtonOperation.AddTodoItem:
					await _view.SlideUpAddTodoItemAsync();
					break;
				//case "Bid":
				//	await _view.SlideUpAsync();	 
				//	break;
				case Views.Enums.MainViewButtonOperation.Logout:
					PopupDialog.ShowConfirm("Logout", "Confirm logout?", "Yes", "No", async (confirm) =>
						   {
							   if (confirm)
							   {
								   await _navigator.NavigateAndSetAsFirstPageAsync("Login", true);
							   }
						   });
					
					break;
			}			
		}
		#endregion
	}
}