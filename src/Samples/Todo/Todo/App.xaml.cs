using NautilusLite;
using Xamarin.Forms;
using Todo.Views;
using Todo.Database;
using Todo.Models;

namespace Todo
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

			//
			// Change to your prefered data store
			//
			//TodoRepositoryContext<TodoItem>.Instance.RegisterRepository(new InMemoryRepository());
			TodoRepositoryContext<TodoItem>.Instance.RegisterRepository(new JsonRepository());
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
