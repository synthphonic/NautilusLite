using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Todo.ViewModels;
using Todo.Views.ViewParameters;

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
			_vm.LoadData(_parameter);

			base.OnAppearing();
		}
	}
}