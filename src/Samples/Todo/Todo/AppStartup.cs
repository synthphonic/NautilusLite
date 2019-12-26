using NautilusLite.Forms.Mvvm.Navigation;
using Todo.Views;

namespace Todo
{
	public static class AppStartup
	{
		public static PageNavigationMapper RegisterPages()
		{
			var instance = PageNavigationMapper.Instance;
			instance.Register("Login", typeof(LoginView));
			instance.Register("Main", typeof(MainView));
			instance.Register("Profile", typeof(ProfileView));
			instance.Register("ForgotPassword", typeof(ForgotPasswordView));
			instance.Register("TodoItemDetail", typeof(TodoItemDetailView));

			return instance;
		}
	}
}