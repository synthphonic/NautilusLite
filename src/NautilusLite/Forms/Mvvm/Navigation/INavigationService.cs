using System.Threading.Tasks;
using Xamarin.Forms;

namespace NautilusLite.Forms.Mvvm.Navigation
{
	/// <summary>
	/// Nautilus implementation of INavigationService
	/// </summary>
	public interface INavigationService
	{
		void Initialize(Application currentApplication);

		/// <summary>
		/// Initialize the application and use developer's implementation of <see cref="INavigationService"/>.
		/// </summary>
		/// <param name="currentApplication"></param>
		/// <param name="navigator"></param>
		void Initialize(Application currentApplication, INavigationService navigator);

		/// <summary>
		/// Sets the first page to display when the app is initialized
		/// </summary>
		/// <param name="rootPage"></param>
		void SetRootPage(Page rootPage);

		/// <summary>
		/// Removes the top page from the navigation stack
		/// </summary>
		/// <param name="animated"></param>
		/// <returns></returns>
		Task GoBackAsync(bool animated = false);

		/// <summary>
		/// Navigates to a new view asynchronously
		/// </summary>
		/// <returns>The to async.</returns>
		/// <param name="pageKey">Page key.</param>
		/// <param name="animated">Animate view when navigating to the intended view</param>
		Task NavigateToAsync(string pageKey, bool animated = false);

		/// <summary>
		/// Navigates to a new view asynchronously
		/// </summary>
		/// <returns>The to async.</returns>
		/// <param name="pageKey">Page key.</param>
		/// <param name="parameter">The object that is passed to the targeted view</param>
		/// /// <param name="animated">Animate view when navigating to the intended view</param>
		Task NavigateToAsync(string pageKey, object parameter, bool animated = false);

		/// <summary>
		/// Removes all views in the navigation stack, and then set the first page
		/// according to the firstPageKey given in the parameter
		/// </summary>
		/// <param name="firstPageKey"></param>
		/// <param name="animated"></param>
		/// <returns></returns>
		Task NavigateAndSetAsFirstPageAsync(string firstPageKey, bool animated = false);
	}
}