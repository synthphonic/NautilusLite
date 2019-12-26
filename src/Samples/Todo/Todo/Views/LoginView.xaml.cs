using NautilusLite.Forms;
using Xamarin.Forms;
using Todo.ViewModels;

namespace Todo.Views
{
	public partial class LoginView : ContentPage
	{
		private LoginViewModel _vm;

		public LoginView()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false); // remove title and nav bar line

			BindingContext = _vm = new LoginViewModel();
		}

		protected override void OnAppearing()
		{
			//
			// Change NavigationPage background
			// ref: https://forums.xamarin.com/discussion/139713/cannot-change-navigation-page-barbackgroundcolor-programmatically
			//

			var bgcolor = (Color)Application.Current.Resources["Background"];
			NavigationPageHelper.NavigationPage.BarBackgroundColor = bgcolor;			

			base.OnAppearing();
		}
	}
}