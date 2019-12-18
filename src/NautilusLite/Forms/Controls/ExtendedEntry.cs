using Xamarin.Forms;

namespace NautilusLite.Forms.Controls
{
	public class ExtendedEntry : Entry
	{
		#region BorderWidthProperty
		public static readonly BindableProperty BorderWidthProperty =
		BindableProperty.Create("float", typeof(float), typeof(ExtendedEntry), 1.0f);

		public float BorderWidth
		{
			get => (float)GetValue(BorderWidthProperty);
			set => SetValue(BorderWidthProperty, value);
		}
		#endregion

		#region BorderColorProperty
		public static readonly BindableProperty BorderColorProperty =
		BindableProperty.Create(nameof(Color), typeof(Color), typeof(ExtendedEntry), Color.White);

		public Color BorderColor
		{
			get => (Color)GetValue(BorderColorProperty);
			set => SetValue(BorderColorProperty, value);
		}
		#endregion

		#region BorderStyleProperty
		public static readonly BindableProperty BorderStyleProperty =
		BindableProperty.Create(nameof(BorderStyle), typeof(BorderStyle), typeof(ExtendedEntry), BorderStyle.Default);

		public BorderStyle BorderStyle
		{
			get => (BorderStyle)GetValue(BorderStyleProperty);
			set => SetValue(BorderStyleProperty, value);
		}
		#endregion
	}

	public enum BorderStyle
	{
		/// <summary>
		/// Default border for all platforms
		/// </summary>
		Default,
		/// <summary>
		/// No border
		/// </summary>
		None,
		/// <summary>
		/// Underline border only
		/// </summary>
		Underline,
	}
}