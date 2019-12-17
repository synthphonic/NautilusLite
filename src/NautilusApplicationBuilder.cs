using System;
using GalaSoft.MvvmLight.Ioc;
using NautilusLite.Forms.Mvvm.Navigation;
using Xamarin.Forms;

namespace NautilusLite
{
	public sealed class NautilusApplicationBuilder : INautilusAppNavigationInitializer, INautilusStartPage, INautilusAppInitializer
	{
		private Func<INavigationService> _action;
		private Type _rootPageType;

		private NautilusApplicationBuilder()
		{

		}

		#region INautilusAppNavigationInitializer
		public static INautilusAppNavigationInitializer Create()
		{
			return new NautilusApplicationBuilder();
		}
		#endregion

		public INautilusStartPage UseNavigation(Func<INavigationService> action = null)
		{
			_action = action;

			return this;
		}

		#region INautilusStartPage
		public INautilusAppInitializer UseRootPage(Type rootPageType)
		{
			_rootPageType = rootPageType;

			return this;
		}
		#endregion

		#region INautilusAppInitializer
		public void Initialize()
		{
			INavigationService navigator = null;

			if (_action == null)
			{
				navigator = new NavigationService();
				navigator.Initialize(Application.Current);
			}
			else
			{
				navigator = _action.Invoke();
				navigator.Initialize(Application.Current);
			}

			navigator = SimpleIoc.Default.GetInstance<INavigationService>() as NavigationService;
			var pageInstance = Activator.CreateInstance(_rootPageType) as Page;
			var navigationPage = new NavigationPage(pageInstance);
			navigator.SetRootPage(navigationPage);

			Application.Current.MainPage = navigationPage;
		}
		#endregion
	}

	public interface INautilusAppNavigationInitializer
	{
		INautilusStartPage UseNavigation(Func<INavigationService> action = null);
	}

	public interface INautilusStartPage
	{
		INautilusAppInitializer UseRootPage(Type rootPageType);
	}

	public interface INautilusAppInitializer
	{
		void Initialize();
	}
}