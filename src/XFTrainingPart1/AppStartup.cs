using NautilusLite.Forms.Mvvm.Navigation;
using XFTrainingPart1.Views;

namespace XFTrainingPart1
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

			return instance;
		}
	}
}