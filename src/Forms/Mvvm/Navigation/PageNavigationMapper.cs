using System;
using System.Collections.Generic;
using System.Linq;
using NautilusLite.Forms.Mvvm.Navigation.Core;

namespace NautilusLite.Forms.Mvvm.Navigation
{
	public sealed class PageNavigationMapper
	{
		private readonly IList<PageMapperItem> _pageItms;

		#region Singleton pattern
		private static readonly PageNavigationMapper _instance = new PageNavigationMapper();

		private PageNavigationMapper()
		{
			_pageItms = new List<PageMapperItem>();
		}

		public static PageNavigationMapper Instance
		{
			get { return _instance; }
		}
		#endregion

		public void Register(string pageKey, Type pageType, params object[] parameter)
		{
			var found = _pageItms.Any(x => x.PageType.FullName.Equals(pageType.FullName));
			if (!found)
			{
				_pageItms.Add(new PageMapperItem { PageKey = pageKey, PageType = pageType, Parameter = parameter });
			}
		}

		internal PageMapperItem GetPage(string pageKey)
		{
			var mapperItem = _pageItms.FirstOrDefault(x => x.PageKey.Equals(pageKey));
			return mapperItem;
		}

		internal IEnumerable<PageMapperItem> PageMappers
		{
			get { return _pageItms; }
		}
	}
}
