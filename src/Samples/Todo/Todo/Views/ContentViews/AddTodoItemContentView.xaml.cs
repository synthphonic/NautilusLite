using Todo.ViewModels.ContentViewModels;
using Xamarin.Forms;

namespace Todo.Views.ContentViews
{
	public partial class AddTodoItemContentView : AddTodoItemCV
	{
		private readonly AddTodoItemViewModel _vm;

		public AddTodoItemContentView()
		{
			InitializeComponent();
			BindingContext = _vm = new AddTodoItemViewModel();

			_vm.SetView(this);
		}

		public void SetNewData()
		{
			_vm.SetNewData();
		}

		internal void ClearData()
		{
			_vm.ClearData();
		}

		#region AddTodoItemCV
		public void SetFavorite(bool like)
		{
			var style = like ? "IconStyleSolid" : "IconStyle";
			LikeLabel.SetDynamicResource(VisualElement.StyleProperty, style);
		}
		#endregion

		#region SlideUpHeightProperty
		public static readonly BindableProperty SlideUpHeightProperty =
		BindableProperty.Create("double", typeof(double), typeof(AddTodoItemContentView), -1.0D);

		public double SlideUpHeight
		{
			get => (double)GetValue(SlideUpHeightProperty);
			set => SetValue(SlideUpHeightProperty, value);
		}
		#endregion
	}

	public interface AddTodoItemCV
	{
		void SetFavorite(bool like);
	}
}