using System;

namespace NautilusLite.Forms.Mvvm.Navigation.Core
{
	internal class PageMapperItem
	{
		internal string PageKey { get; set; }
		internal Type PageType { get; set; }
		internal object[] Parameter { get; set; }
	}
}
