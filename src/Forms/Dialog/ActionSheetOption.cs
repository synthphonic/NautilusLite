using System;

namespace NautilusLite.Forms.Dialog
{
	public class ActionSheetOption
	{
		public ActionSheetOption(string text, Action action = null, string icon = null)
		{
			Text = text;
			Action = action;
			Icon = icon;
		}

		public string Text { get; set; }
		public Action Action { get; set; }
		public string Icon { get; private set; }
	}
}
