using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using NautilusLite.Core;
using NautilusLite.Forms.Mvvm.Navigation.Core;
using NautilusLite.Infrastructure;
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

			var pageInstance = FindAndCreate(pageMapperItem, parameter);
			await _navigationPage.PushAsync(pageInstance, animated);
		}

		public async Task NavigateAndSetAsFirstPageAsync(string firstPageKey, bool animated = false)
		{
			try
			{
				await _navigationPage.Navigation.PopToRootAsync(animated);
				var rootPage = _navigationPage.Navigation.NavigationStack[0];

				var pageMapperItem = PageNavigationMapper.Instance.GetPage(firstPageKey);
				var newPage = FindAndCreate(pageMapperItem, null);

				_navigationPage.Navigation.InsertPageBefore(newPage, rootPage);

				_navigationPage.Navigation.RemovePage(rootPage);
			}
			catch(InvalidCastException invalidCastEx)
			{
				DebugOutput.Write(GetType(), $"{invalidCastEx.Message}\n{invalidCastEx.StackTrace}");
				throw;
			}
			catch (ApplicationException appEx)
			{
				DebugOutput.Write(GetType(), $"{appEx.Message}\n{appEx.StackTrace}");

				if (appEx.InnerException != null)
				{
					DebugOutput.Write(GetType(), $"{appEx.InnerException.Message}\n{appEx.InnerException.StackTrace}");
				}
				
				throw;
			}
			catch (SystemException sysEx)
			{
				DebugOutput.Write(GetType(), $"{sysEx.Message}\n{sysEx.StackTrace}");
				throw;
			}
			catch (Exception ex)
			{
				DebugOutput.Write(GetType(), $"{ex.Message}");
				throw;
			}
		}

		public async Task GoBackAsync(bool animated = false)
		{
			await _navigationPage.PopAsync(animated);
		}

		public void SetRootPage(Page rootPage)
		{

		}

		private Page FindAndCreate(PageMapperItem pageMapperItem, object parameter)
		{
			ConstructorInfo constructor;
			object[] parameters;

			if (parameter == null)
			{
				constructor = pageMapperItem.PageType.GetTypeInfo()
					.DeclaredConstructors
					.FirstOrDefault(c => !c.GetParameters().Any());

				parameters = new object[] { };
			}
			else
			{
				constructor = pageMapperItem.PageType.GetTypeInfo()
					.DeclaredConstructors
					.FirstOrDefault(
					c =>
					{
						var p = c.GetParameters();
						return p.Count() == 1
						&& p[0].ParameterType == parameter.GetType();
					});

				parameters = new[]
				{
					parameter
				};
			}

			if (constructor == null)
			{
				throw new InvalidOperationException("No suitable constructor found for page key " + pageMapperItem.PageKey);
			}

			var pageInstance = constructor.Invoke(parameters) as Page;
			return pageInstance;
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