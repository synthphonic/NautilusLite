using NautilusLite;
using Xamarin.Forms;
using XFTrainingPart1.Views;

namespace XFTrainingPart1
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			var appBuilder = NautilusApplicationBuilder.Create();

			appBuilder
				.UseNavigation()
				.RegisterPages(AppStartup.RegisterPages)
				.UseRootPage(typeof(LoginView))
				.Initialize();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
