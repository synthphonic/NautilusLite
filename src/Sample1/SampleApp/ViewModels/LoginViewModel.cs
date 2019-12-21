using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using NautilusLite.Forms;
using NautilusLite.Forms.Input;
using NautilusLite.Forms.Mvvm.Navigation;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		private readonly INavigationService _navigator;
		private ICommand _loginCommand;
		private ICommand _forgotPasswordCommand;
		private string _userName;
		private string _password;

		public LoginViewModel()
		{
			_navigator = SimpleIoc.Default.GetInstance<INavigationService>();
		}

		#region Binding properties
		public string UserName
		{
			get { return _userName; }
			set { Set(ref _userName, value); }
 		}

		public string Password
		{
			 get { return _password; }
			set { Set(ref _password, value); }
		}
		#endregion

		#region LoginCommand
		public ICommand LoginCommand
		{
			get { return _loginCommand ?? (_loginCommand = new AsyncCommand(DoLogin)); }
		}

		private async Task DoLogin()
		{
			var bgcolor = (Color)Application.Current.Resources["AlmostBlack"];
			NavigationPageHelper.NavigationPage.BarBackgroundColor = bgcolor;

			await _navigator.NavigateAndSetAsFirstPageAsync("Main", true);
		}
		#endregion

		#region ForgotPasswordCommand
		public ICommand ForgotPasswordCommand
		{
			get { return _forgotPasswordCommand ?? (_forgotPasswordCommand = new AsyncCommand(NavigateForgotPasswordView)); }
		}

		private async Task NavigateForgotPasswordView()
		{
			await _navigator.NavigateToAsync("ForgotPassword", true);
		}

		#endregion
	}
}