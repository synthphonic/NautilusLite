using Todo.ViewModels;
using Todo.Views.ViewParameters;
using Xamarin.Forms;

namespace Todo.Views
{
	public partial class TodoItemDetailView : ContentPage, ITodoItemDetailView
	{
		private TodoItemDetailViewModel _vm;

		public TodoItemDetailView()
		{
			InitializeComponent();
		}

		public TodoItemDetailView(TodoItemParameter parameter) : this()
		{
			BindingContext = _vm = new TodoItemDetailViewModel();

			_vm.SetView(this);

			_vm.SetViewParameter(parameter);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			_vm.Load();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			_vm.Unload();
			BindingContext = _vm = null;
		}

		#region ITodoItemDetailView implementation
		public void SetFavorite(bool like)
		{
			var style = like ? "IconStyleSolid" : "IconStyle";
			LikeLabel.SetDynamicResource(VisualElement.StyleProperty, style);
		}
		#endregion
	}

	public interface ITodoItemDetailView
	{
		void SetFavorite(bool like);
	}
}