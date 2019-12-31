using Todo.ViewModels.ContentViewModels;
using Todo.Views.Enums;
using Xamarin.Forms;

namespace Todo.Views.ContentViews
{
	public partial class TodoListContentView : ContentView, ITodoListContentView
	{
		private readonly TodoListContentViewModel _vm;

		public TodoListContentView()
		{
			InitializeComponent();
		}

		public TodoListContentView(TabContentType tabContentType) : this()
		{
			BindingContext = _vm = new TodoListContentViewModel(tabContentType);
			_vm.SetView(this);
			_vm.Load();
		}

		#region ITodoListContentView
		public void ToggleContainerVisibility(bool hasItems)
		{
			var hasItemContainerState = hasItems ? "HasItem" : "NoItem";
			var noItemContainerState = !hasItems ? "NoItem" : "HasItem";

			VisualStateManager.GoToState(HasItemContainer, hasItemContainerState);
			VisualStateManager.GoToState(NoItemContainer, noItemContainerState);
		}
		#endregion
	}

	public interface ITodoListContentView
	{
		void ToggleContainerVisibility(bool hasItems);
	}
}
