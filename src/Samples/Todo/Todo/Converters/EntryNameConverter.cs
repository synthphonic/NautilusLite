using System;
using System.Globalization;
using Xamarin.Forms;

namespace Todo.Converters
{
	public class EntryNameConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var stringValue = value as string;
			if (string.IsNullOrWhiteSpace(stringValue))
			{
				return string.Empty;
			}

			return stringValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
