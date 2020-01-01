using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using NautilusLite.Forms.Input;
using NautilusLite.Forms.Mvvm.Navigation;
using Todo.Database;
using Todo.Models;
using Todo.Views.ContentViews;
using Todo.Views.Enums;
using Todo.Views.ViewParameters;

namespace Todo.ViewModels.ContentViewModels
{
	public class TodoListContentViewModel : ViewModelBase
	{
		private readonly INavigationService _navigator;
		private ITodoListContentView _view;  
		private TabContentType _tabContentType;
		private ICommand _navigateToDetailToDoItemCommand;
		private ObservableCollection<TodoItem> _todoList;

		public TodoListContentViewModel(TabContentType tabContentType)
		{
			_navigator = SimpleIoc.Default.GetInstance<INavigationService>();

			_tabContentType = tabContentType;
		}

		internal void Load()
		{
			switch (_tabContentType)
			{
				case TabContentType.UpComing:
					ListUpComingItems();
					break;
				case TabContentType.DueToday:
					ListDueTodayItems();
					break;
				case TabContentType.Completed:
					ListCompletedItems();
					break;
			}
		}

		private void ListDueTodayItems()
		{
			var allTodos = TodoRepositoryContext<TodoItem>.Instance.GetAll();
			var dueTodayList = (from a in allTodos
								where a.Due == DateTime.Today && !a.Completed
								select a).ToList();

			TodoList = new ObservableCollection<TodoItem>(dueTodayList);

			_view.ToggleContainerVisibility(dueTodayList.Count > 0);
		}

		private void ListCompletedItems()
		{
			var allTodos = TodoRepositoryContext<TodoItem>.Instance.GetAll();

			var completedList = (from a in allTodos
							where a.Completed
							select a).ToList();

			TodoList = new ObservableCollection<TodoItem>(completedList);

			_view.ToggleContainerVisibility(completedList.Count > 0);
		}

		private void ListUpComingItems()
		{
			var allTodos = TodoRepositoryContext<TodoItem>.Instance.GetAll();

			var upcomingList = (from a in allTodos
								where a.Due >= DateTime.Now && !a.Completed
								select a).ToList();

			TodoList = new ObservableCollection<TodoItem>(upcomingList);

			_view.ToggleContainerVisibility(upcomingList.Count > 0);
		}

		internal void SetView(TodoListContentView view)
		{
			_view = view;
		}

		#region Binding properties
		public ObservableCollection<TodoItem> TodoList
		{
			get { return _todoList; }
			set { Set(ref _todoList, value); }
		}
		#endregion

		#region NavigateToDetailToDoItemCommand
		public ICommand NavigateToDetailToDoItemCommand
		{
			get => _navigateToDetailToDoItemCommand ?? (_navigateToDetailToDoItemCommand = new AsyncCommand<TodoItem>(NavigateToDetailItemAsync));
		}

		private async Task NavigateToDetailItemAsync(TodoItem todo)
		{
			var parameter = new TodoItemParameter
			{
				Id = todo.Id
			};

			await _navigator.NavigateToAsync("TodoItemDetail", parameter, true);
		}
		#endregion
	}
}