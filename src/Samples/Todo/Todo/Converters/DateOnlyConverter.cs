using System;
using System.Globalization;
using Xamarin.Forms;

namespace Todo.Converters
{
	public class DateOnlyConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var dateTime = (DateTime)value;

			return dateTime.ToString("dd/MM/yyyy");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}