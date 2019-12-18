using Xamarin.Forms;

namespace NautilusLite.Forms
{
	public static class NavigationPageHelper
	{
		//private static NavigationPage _navigationPage;

		//internal static void SetNavigationPage(NavigationPage navigationPage)
		//{
		//	_navigationPage = navigationPage;
		//}

		public static NavigationPage NavigationPage
		{
			get { return Application.Current.MainPage as NavigationPage; }
		}
	}
}