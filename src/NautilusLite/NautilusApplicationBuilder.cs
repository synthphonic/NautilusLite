using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Ioc;
using NautilusLite.Core;
using NautilusLite.Forms.Mvvm.Navigation;
using NautilusLite.Forms.Mvvm.Navigation.Core;
using NautilusLite.Infrastructure;
using NautilusLite.Infrastructure.IO;
using Xamarin.Forms;

namespace NautilusLite
{
	public sealed class NautilusApplicationBuilder : INautilusAppNavigationInitializer, IPageNavigationMapper, INautilusStartPage, INautilusAppInitializer
	{
		private Func<INavigationService> _navigationSvcFunc;
		private Func<PageNavigationMapper> _pageMapperFunc;
		private Type _rootPageType;
		private IEnumerable<PageMapperItem> _pageMappers;
		private NautilusApplicationBuilder()
		{

		}

		#region INautilusAppNavigationInitializer
		public static INautilusAppNavigationInitializer Create()
		{
			return new NautilusApplicationBuilder();
		}
		#endregion

		public IPageNavigationMapper UseNavigation(Func<INavigationService> navigationSvcFunc = null)
		{
			_navigationSvcFunc = navigationSvcFunc;

			return this;
		}

		#region IPageNavigationMapper
		public INautilusStartPage RegisterPages(Func<PageNavigationMapper> pageMapperFunc = null)
		{
			if (pageMapperFunc == null)
			{
				throw new NautilusException("PageNavigationMapper is null");
			}

			_pageMapperFunc = pageMapperFunc;

			return this;
		}

		#endregion

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
			//
			// invoke page mapper delegate
			var pageMapper = _pageMapperFunc();
			_pageMappers = pageMapper.PageMappers;

			//
			// invoke navigation service delegate
			INavigationService navigator = null;

			if (_navigationSvcFunc == null)
			{
				navigator = new NavigationService();
				navigator.Initialize(Application.Current);
			}
			else
			{
				navigator = _navigationSvcFunc.Invoke();
				navigator.Initialize(Application.Current);
			}

			navigator = SimpleIoc.Default.GetInstance<INavigationService>() as NavigationService;
			var pageInstance = Activator.CreateInstance(_rootPageType) as Page;
			var navigationPage = new NavigationPage(pageInstance);
			navigator.SetRootPage(navigationPage);

			((NavigationService)navigator).SetNavigtionPage(navigationPage);

			Application.Current.MainPage = navigationPage;

			DebugOutput.Write(GetType(), $"Local Storage Root Path: {LocalStorage.Instance.RootPath}");
		}
		#endregion
	}

	public interface INautilusAppNavigationInitializer
	{
		IPageNavigationMapper UseNavigation(Func<INavigationService> func = null);
	}

	public interface IPageNavigationMapper
	{
		INautilusStartPage RegisterPages(Func<PageNavigationMapper> func = null);
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