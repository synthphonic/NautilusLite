using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using NautilusLite.Forms.Dialog;
using NautilusLite.Forms.Input;
using NautilusLite.Forms.Mvvm.Navigation;
using Xamarin.Forms;

namespace Todo.ViewModels
{
	public class ForgotPasswordViewModel : ViewModelBase
	{
		private ICommand _submitForgotPasswordCommand;
		private INavigationService _navigator;

		public ForgotPasswordViewModel()
		{
			_navigator = SimpleIoc.Default.GetInstance<INavigationService>();
		}

		#region SubmitForgotPasswordCommand
		public ICommand SubmitForgotPasswordCommand
		{
			get { return _submitForgotPasswordCommand ?? (_submitForgotPasswordCommand = new AsyncCommand(DoSubmissionAsync)); }
		}

		private async Task DoSubmissionAsync()
		{
			await _navigator.GoBackAsync(true);

			var orangeColor = (Color)Application.Current.Resources["OrangeColor"];

			PopupDialog.ShowToast("Check your email for instructions to change your password",
				Color.Blue, orangeColor, TimeSpan.FromSeconds(3));
		}
		#endregion
	}
}
