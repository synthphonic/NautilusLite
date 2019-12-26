using Todo.ViewModels;
using Todo.Views.ViewParameters;
using Xamarin.Forms;

namespace Todo.Views
{
	public partial class TodoItemDetailView : ContentPage, ITodoItemDetailView
	{
		private readonly TodoItemDetailViewModel _vm;

		public TodoItemDetailView()
		{
			InitializeComponent();
			BindingContext = _vm = new TodoItemDetailViewModel();

			_vm.SetView(this);
		}

		public TodoItemDetailView(TodoItemParameter parameter) : this()
		{
			_vm.SetViewParameter(parameter);
		}

		protected override void OnAppearing()
		{
			_vm.Load();
			base.OnAppearing();
		}

		public void SetFavorite(bool like)
		{
			var style = like ? "IconStyleSolid" : "IconStyle";
			LikeLabel.SetDynamicResource(VisualElement.StyleProperty, style);
		}
	}

	public interface ITodoItemDetailView
	{
		void SetFavorite(bool like);
	}
}