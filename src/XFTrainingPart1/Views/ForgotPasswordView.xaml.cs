
using NautilusLite.Forms;
using Xamarin.Forms;
using SampleApp.ViewModels;

namespace SampleApp.Views
{
	public partial class ForgotPasswordView : ContentPage
	{
		private ForgotPasswordViewModel _vm;

		public ForgotPasswordView()
		{
			InitializeComponent();

			BindingContext = _vm = new ForgotPasswordViewModel();
		}

		protected override void OnAppearing()
		{
			var bgcolor = (Color)Application.Current.Resources["Background"];
			NavigationPageHelper.NavigationPage.BarBackgroundColor = bgcolor;

			base.OnAppearing();
		}
	}
}