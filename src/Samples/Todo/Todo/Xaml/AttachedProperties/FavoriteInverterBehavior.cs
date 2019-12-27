using Xamarin.Forms;

namespace Todo.Xaml.AttachedProperties
{
	public static class FavoriteStyleInverter
	{
		public static readonly BindableProperty IsFavoriteProperty =
			BindableProperty.CreateAttached
			(
				"IsFavorite",
				typeof(bool),
				typeof(FavoriteStyleInverter),
				false,
				defaultBindingMode: BindingMode.TwoWay,
				propertyChanged: OnAttachedPropertyChanged,
				propertyChanging: OnAttachedPropertyChanging
			);

		public static bool GetIsFavorite(BindableObject bindable)
		{
			return (bool)bindable.GetValue(IsFavoriteProperty);
		}

		public static void SetIsFavorite(BindableObject bindable, bool value)
		{
			var view = bindable as Label;
			if (view == null)
				return;

			view.SetValue(IsFavoriteProperty, value);
		}

		private static void OnAttachedPropertyChanging(BindableObject bindable, object oldValue, object newValue)
		{
			// empty implementation for now
		}

		private static void OnAttachedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var label = bindable as Label;
			if (label == null)
				return;

			var isFavorite = (bool)newValue;
			var style = isFavorite ? "IconStyleSolid" : "IconStyle";

			label.SetDynamicResource(VisualElement.StyleProperty, style);
		}
	}
}
