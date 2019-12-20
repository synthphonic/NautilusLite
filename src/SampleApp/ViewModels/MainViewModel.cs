using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using NautilusLite.Forms.Input;
using NautilusLite.Forms.Mvvm.Navigation;
using SampleApp.Models;
using SampleApp.Views;
using SampleApp.Views.ViewParameters;

namespace SampleApp.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private readonly INavigationService _navigator;		
		private string _welcomeMessage;
		private string _messageToAdd;
		private UserModel _user;
		private ICommand _navigateToProfileCommand;
		private ICommand _logoutCommand;
		private ICommand _makeABidCommand;
		private ICommand _pageFaderTapCommand;
		private ICommand _showProfileCommand;
		private IMainView _view;

		public MainViewModel()
		{
			_navigator = SimpleIoc.Default.GetInstance<INavigationService>();
		}

		internal void SetView(IMainView mainView)
		{
			_view = mainView;
		}

		internal void Load()
		{
			WelcomeMessage = "Nautilus Lite SDK Reference App.";

			var user = new UserModel
			{
				Name = "ShahZ S",
				Id = Guid.NewGuid()
			};

			User = user;
		}

		#region Binding properties		
		public UserModel User
		{
			get { return _user; }
			set { Set(ref _user, value); }
		}

		public string WelcomeMessage
		{
			get { return _welcomeMessage; }
			set { Set(ref _welcomeMessage, value); }
		}

		public string MessageToAdd
		{
			get { return _messageToAdd; }
			set { Set(ref _messageToAdd, value); }
		}
		#endregion

		#region NavigateToProfileCommand
		public ICommand NavigateToProfileCommand
		{
			get { return _navigateToProfileCommand ?? (_navigateToProfileCommand = new AsyncCommand(NavigateToProfile)); }
		}

		private async Task NavigateToProfile()
		{
			await Task.Delay(1);
			var navigator = SimpleIoc.Default.GetInstance<INavigationService>();

			var parameter = new ProfileViewParameter
			{
				Message = MessageToAdd
			};

			await navigator.NavigateToAsync("Profile", parameter, true);
		}
		#endregion

		#region LogoutCommand
		public ICommand LogoutCommand
		{
			get { return _logoutCommand ?? (_logoutCommand = new AsyncCommand(DoLogoutAsync)); }
		}

		private async Task DoLogoutAsync()
		{
			await _navigator.NavigateAndSetAsFirstPageAsync("Login", true);
		}
		#endregion

		#region MakeABidCommand
		public ICommand MakeABidCommand
		{
			get { return _makeABidCommand ?? (_makeABidCommand = new AsyncCommand(SlideUpBidView)); }
		}

		private async Task SlideUpBidView()
		{
			await _view.SlideUpAsync();
		}
		#endregion

		#region PageFaderTapCommand
		public ICommand PageFaderTapCommand
		{
			get { return _pageFaderTapCommand ?? (_pageFaderTapCommand = new AsyncCommand(TapPageFaderAsync)); }
		}

		private async Task TapPageFaderAsync()
		{
			await _view.PageFaderTappedAsync();
		}
		#endregion

		public ICommand ShowProfileCommand
		{
			get { return _showProfileCommand ?? (_showProfileCommand = new AsyncCommand(ShowProfileAsync)); }
		}

		private async Task ShowProfileAsync()
		{
			await _view.SlideUpProfileAsync(); 
		}

	}
}