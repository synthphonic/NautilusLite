using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SampleApp.ViewModels;
using NautilusLite.Forms;

namespace SampleApp.Views
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainView : ContentPage, IMainView
	{
		private MainViewModel _vm;

		public MainView()
		{
			InitializeComponent();
			BindingContext = _vm = new MainViewModel();
			_vm.SetView(this);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			_vm.Load();
		}

		public async Task SlideUpAsync()
		{
			PageFader.IsVisible = true;
			PageFader.Opacity = 0.3;
			await BidPopup.TranslateTo(0, Height-200, 300, Easing.SinInOut);
		}

		public async Task PageFaderTappedAsync()
		{
			await BidPopup.TranslateTo(0, Height, 300, Easing.SinInOut);
			PageFader.IsVisible = false;
		}
	}

	public interface IMainView
	{
		Task SlideUpAsync();
		Task PageFaderTappedAsync();
	}
}