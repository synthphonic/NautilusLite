using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFTrainingPart1.ViewModels;

namespace XFTrainingPart1.Views
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainView : ContentPage
	{
		private MainViewModel _vm;

		public MainView()
		{
			InitializeComponent();
			BindingContext = _vm = new MainViewModel();
		}

		protected override void OnAppearing()
		{
			_vm.Load();

			base.OnAppearing();
		}
	}
}