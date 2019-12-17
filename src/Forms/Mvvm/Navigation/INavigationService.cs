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
	}
}