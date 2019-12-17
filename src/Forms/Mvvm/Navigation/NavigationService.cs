using GalaSoft.MvvmLight.Ioc;
using NautilusLite.Core;
using Xamarin.Forms;

namespace NautilusLite.Forms.Mvvm.Navigation
{
	public class NavigationService : INavigationService
	{
		private Application _currentApplication;

		public NavigationService()
		{
		}

		public void Initialize(Application currentApplication)
		{
			var navSvcRegistered = SimpleIoc.Default.IsRegistered<INavigationService>();
			if (!navSvcRegistered)
			{
				InitializeCurrentApp(currentApplication);
				SimpleIoc.Default.Register<INavigationService>(() => this);
			}
		}

		public void Initialize(Application currentApplication, INavigationService navigator)
		{
			if (navigator == null)
			{
				throw new NautilusException("INavigationService was null");
			}

			var navSvcRegistered = SimpleIoc.Default.IsRegistered<INavigationService>();
			if (!navSvcRegistered)
			{
				InitializeCurrentApp(currentApplication);
				SimpleIoc.Default.Register<INavigationService>(() => navigator);
			}
		}

		private void InitializeCurrentApp(Application application)
		{
			_currentApplication = application;
		}

		public void SetRootPage(Page rootPage)
		{

		}
	}
}