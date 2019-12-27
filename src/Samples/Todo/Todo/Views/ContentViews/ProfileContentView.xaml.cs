using Todo.ViewModels.ContentViewModels;
using Xamarin.Forms;

namespace Todo.Views.ContentViews
{
	public partial class ProfileContentView : ContentView
	{
		private readonly ProfileViewModel _vm;

		public ProfileContentView()
		{
			InitializeComponent();
			BindingContext = _vm = new ProfileViewModel();
		}

		public void LoadData()
		{
			_vm.LoadData(null);
		}

		internal void UnLoadData()
		{
			_vm.UnLoadData();
		}
	}
}