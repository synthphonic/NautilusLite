using NautilusLite.Forms;
using Xamarin.Forms;
using XFTrainingPart1.ViewModels;

namespace XFTrainingPart1.Views
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
			var bgcolor = (Color)Application.Current.Resources["Background"];
			NavigationPageHelper.NavigationPage.BarBackgroundColor = bgcolor;			

			base.OnAppearing();
		}
	}
}