﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using NautilusLite.Forms.Input;
using NautilusLite.Forms.Mvvm.Navigation;
using SampleApp.Models;
using SampleApp.Views.ViewParameters;

namespace SampleApp.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private readonly INavigationService _navigator;
		private string _welcomeMessage;
		private UserModel _user;
		private ICommand _navigateToProfileCommand;
		private ICommand _logoutCommand;
		private string _messageToAdd;

		public MainViewModel()
		{
			_navigator = SimpleIoc.Default.GetInstance<INavigationService>();
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

		#region Navigate
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
	}
}