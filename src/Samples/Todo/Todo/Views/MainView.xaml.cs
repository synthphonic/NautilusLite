using System.ComponentModel;
using System.Threading.Tasks;
using NautilusLite.Infrastructure;
using Todo.ViewModels;
using Todo.Views.Enums;
using Xamarin.Forms;

namespace Todo.Views
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainView : ContentPage, IMainView
	{
		private MainViewModel _vm;
		private MainViewButtonOperation _slideViewType = MainViewButtonOperation.None;

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

			MessagingCenter.Subscribe<string>(this, "SlideDownView", param => SlideDownAsync(string.Empty));

			_vm.Load();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			MessagingCenter.Unsubscribe<string>(this, "SlideDownView");
		}

		private void SlideDownAsync(string parameter)
		{
			Task.Run(async () => await PageFaderTappedAsync()).ConfigureAwait(false);
		}

		#region IMainView implementation
		public async Task SlideUpAsync()
		{
			_slideViewType = MainViewButtonOperation.Bid;

			PageFader.IsVisible = true;
			PageFader.Opacity = 0.5;

			BidPopup.IsVisible = true;
			await BidPopup.TranslateTo(0, Height - Profile.Height, 300, Easing.SinInOut);
		}

		public async Task SlideUpAddTodoItemAsync()
		{
			DebugOutput.Write(GetType(), $"Page Height={Height}");
			DebugOutput.Write(GetType(), $"Profile Height={AddTodoItem.Height}");

			AddTodoItem.HeightRequest = Height;
			DebugOutput.Write(GetType(), $"Profile Height={AddTodoItem.Height}");

			_slideViewType = MainViewButtonOperation.AddTodoItem;

			PageFader.IsVisible = true;
			PageFader.Opacity = 0.5;

			AddTodoItem.IsVisible = true;
			await AddTodoItem.TranslateTo(0, Height - AddTodoItem.Height, 300, Easing.SinInOut);
			AddTodoItem.SetNewData();
		}

		public async Task SlideUpProfileAsync()
		{
			DebugOutput.Write(GetType(), $"Page Height={Height}");
			DebugOutput.Write(GetType(), $"Profile Height={Profile.Height}");

			Profile.HeightRequest = Height;
			DebugOutput.Write(GetType(), $"Profile Height={Profile.Height}");

			_slideViewType = MainViewButtonOperation.Profile;

			PageFader.IsVisible = true;
			PageFader.Opacity = 0.5;

			Profile.IsVisible = true;
			await Profile.TranslateTo(0, Height - Profile.Height, 300, Easing.SinInOut);
			Profile.LoadData();
		}

		public async Task PageFaderTappedAsync()
		{
			switch (_slideViewType)
			{
				case MainViewButtonOperation.Bid:
					await BidPopup.TranslateTo(0, Height, 300, Easing.SinInOut);
					BidPopup.IsVisible = false;

					break;

				case MainViewButtonOperation.Profile:
					switch (Device.RuntimePlatform)
					{
						case Device.iOS:
							await Profile.TranslateTo(0, Height, 300, Easing.SinInOut);
							break;
						case Device.Android:
							await Profile.TranslateTo(0, Height, 300, Easing.SinInOut);
							break;
					}

					Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
					{
						Profile.UnLoadData();
						Profile.IsVisible = false;
					});

					break;

				case MainViewButtonOperation.AddTodoItem:
					switch (Device.RuntimePlatform)
					{
						case Device.iOS:
							await AddTodoItem.TranslateTo(0, Height, 300, Easing.SinInOut);
							break;
						case Device.Android:
							await AddTodoItem.TranslateTo(0, Height, 300, Easing.SinInOut);
							break;
					}

					Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
					{

						AddTodoItem.ClearData();
						AddTodoItem.IsVisible = false;
					});

					break;
			}

			Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
			{
				PageFader.Opacity = 0;
				PageFader.IsVisible = false;
			});
		}
		#endregion
	}

	public interface IMainView
	{
		Task SlideUpAsync();
		Task SlideUpProfileAsync();
		Task SlideUpAddTodoItemAsync();
		Task PageFaderTappedAsync();		
	}
}