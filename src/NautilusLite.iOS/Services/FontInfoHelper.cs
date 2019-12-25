/*
 * Reference:
 * https://stackoverflow.com/questions/48188187/fontawesome-pro-and-xamarin-ios-only-one-font-can-be-active
 * https://forums.xamarin.com/discussion/94921/font-awesome-problem
 *
 */
using System.Linq;
using NautilusLite.Infrastructure;
using UIKit;

namespace NautilusLite.iOS.Services
{
	public static class FontInfoHelper
	{
		/// <summary>
		/// Shows all installed fonts to the debut output window
		/// <para/>
		/// NOTE: This class is to be used for debugging ONLY
		/// </summary>
		public static void OutputInstalledFonts()
		{
			foreach (var familyNames in UIFont.FamilyNames.OrderBy(c => c).ToList())
			{
				DebugOutput.Write(" * " + familyNames);
				foreach (var familyName in UIFont.FontNamesForFamilyName(familyNames).OrderBy(c => c).ToList())
				{
					DebugOutput.Write(" *-- " + familyName);
				}
			}

		}
	}
}