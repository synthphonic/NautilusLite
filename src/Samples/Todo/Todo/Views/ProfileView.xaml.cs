
using Xamarin.Forms;
using Todo.Views.ViewParameters;
using Todo.ViewModels.ContentViewModels;

namespace Todo.Views
{
	public partial class ProfileView : ContentPage
	{
		private ProfileViewModel _vm;
		private ProfileViewParameter _parameter;

		public ProfileView()
		{
			InitializeComponent();
		}

		public ProfileView(ProfileViewParameter parameter) : this()
		{
			_parameter = parameter;

			BindingContext = _vm = new ProfileViewModel();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
		}
	}
}