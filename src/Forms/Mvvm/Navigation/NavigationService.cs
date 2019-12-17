using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using NautilusLite.Core;
using NautilusLite.Forms.Mvvm.Navigation.Core;
using Xamarin.Forms;

namespace NautilusLite.Forms.Mvvm.Navigation
{
	public sealed class NavigationService : INavigationService
	{
		private Application _currentApplication;
		private NavigationPage _navigationPage;

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

		public async Task NavigateToAsync(string pageKey, bool animated = false)
		{
			await Task.Delay(10);
			await NavigateToAsync(pageKey, null, animated);
		}

		public async Task NavigateToAsync(string pageKey, object parameter, bool animated = false)
		{
			var pageMapperItem = PageNavigationMapper.Instance.GetPage(pageKey);
			
			if (pageMapperItem == null)
			{
				throw new NautilusException($"PageKey '{pageKey}' was not registered. Ensure it gets registered first before calling");
			}

			var pageInstance = FindAndCreate(pageMapperItem);
			await _navigationPage.PushAsync(pageInstance, animated);
		}

		private Page FindAndCreate(PageMapperItem pageMapperItem)
		{
			var pageInstance = Activator.CreateInstance(pageMapperItem.PageType) as Page;

			return pageInstance;
		}

		public void SetRootPage(Page rootPage)
		{

		}

		internal void SetNavigtionPage(NavigationPage navigationPage)
		{
			_navigationPage = navigationPage;
		}

		#region Private class methods
		private void InitializeCurrentApp(Application application)
		{
			_currentApplication = application;
		}
		#endregion
	}
}