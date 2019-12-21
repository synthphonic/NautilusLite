using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SampleApp.ViewModels;
using NautilusLite.Forms;
using NautilusLite.Infrastructure;

namespace SampleApp.Views
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainView : ContentPage, IMainView
	{
		private MainViewModel _vm;
		private SlideViewType _slideViewType = SlideViewType.None;

		public MainView()
		{
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false); // remove title and nav bar line

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
			_slideViewType = SlideViewType.Bid;

			PageFader.IsVisible = true;
			PageFader.Opacity = 0.5;

			BidPopup.IsVisible = true;
			await BidPopup.TranslateTo(0, Height - Profile.Height, 300, Easing.SinInOut);
		}

		public async Task SlideUpProfileAsync()
		{
			DebugOutput.Write(GetType(), $"Page Height={Height}");
			DebugOutput.Write(GetType(), $"Profile Height={Profile.Height}");

			Profile.HeightRequest = Height;
			DebugOutput.Write(GetType(), $"Profile Height={Profile.Height}");

			_slideViewType = SlideViewType.Profile;

			PageFader.IsVisible = true;
			PageFader.Opacity = 0.5;

			Profile.IsVisible = true;
			await Profile.TranslateTo(0, Height - Profile.Height, 300, Easing.SinInOut);
			Profile.LoadData();
		}

		public async Task PageFaderTappedAsync()
		{
			switch(_slideViewType)
			{
				case SlideViewType.Bid:
					await BidPopup.TranslateTo(0, Height, 300, Easing.SinInOut);
					BidPopup.IsVisible = false;
					break;

				case SlideViewType.Profile:

					switch (Device.RuntimePlatform)
					{
						case Device.iOS:
							await Profile.TranslateTo(0, Height, 300, Easing.SinInOut);
							break;
						case Device.Android:
							await Profile.TranslateTo(0, Height, 300, Easing.SinInOut);
							break;
					}
					
					Profile.UnLoadData();
					Profile.IsVisible = false;
					break;
			}

			PageFader.Opacity = 0;
			PageFader.IsVisible = false;
		}
	}

	public interface IMainView
	{
		Task SlideUpAsync();
		Task PageFaderTappedAsync();
		Task SlideUpProfileAsync();
	}

	public enum SlideViewType
	{
		Bid,
		Profile,
		None,
	}
}