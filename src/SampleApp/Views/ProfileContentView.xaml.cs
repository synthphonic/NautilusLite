using System;
using SampleApp.ViewModels;
using Xamarin.Forms;

namespace SampleApp.Views
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