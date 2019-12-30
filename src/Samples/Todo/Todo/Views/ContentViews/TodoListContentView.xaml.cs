using Todo.ViewModels.ContentViewModels;
using Todo.Views.Enums;
using Xamarin.Forms;

namespace Todo.Views.ContentViews
{
	public partial class TodoListContentView : ContentView
	{
		private readonly TodoListContentViewModel _vm;

		public TodoListContentView()
		{
			InitializeComponent();
		}

		public TodoListContentView(TabContentType tabContentType) : this()
		{
			BindingContext = _vm = new TodoListContentViewModel(tabContentType);
			_vm.Load();
		}
	}
}
