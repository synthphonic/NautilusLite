using System;
using System.Collections.Generic;
using System.Diagnostics;
using Acr.UserDialogs;

namespace NautilusLite.Forms.Dialog
{
	public class PopupDialog
	{
		public static IProgressDialog ShowProgress(string title, MaskType masktype, bool isDeterministic = false)
		{
			var dialogConfig = new ProgressDialogConfig();
			dialogConfig.IsDeterministic = isDeterministic;
			dialogConfig.MaskType = masktype;
			dialogConfig.Title = title;

			return UserDialogs.Instance.Progress(dialogConfig);
		}

		public static void ShowConfirm(string title, string message, string okText, string cancelText, Action<bool> action)
		{
			var confirmConfig = new ConfirmConfig();
			confirmConfig.Title = title;
			confirmConfig.Message = message;
			confirmConfig.OkText = okText;
			confirmConfig.CancelText = cancelText;
			confirmConfig.SetAction(action);

			UserDialogs.Instance.Confirm(confirmConfig);
		}

		public static void ShowAlert(string title, string message, string okText = "Ok", Action action = null)
		{
			var alertConfig = new AlertConfig();
			alertConfig.Message = message;
			alertConfig.Title = title;
			alertConfig.OkText = okText;
			alertConfig.SetAction(action);

			UserDialogs.Instance.Alert(alertConfig);
		}

		public static void ShowActionSheet(string title, IList<ActionSheetOption> actionSheetOptions, ActionSheetOption cancelSheetOption)
		{
			var actionSheetConfig = new ActionSheetConfig();
			actionSheetConfig.SetTitle(title);
			actionSheetConfig.Cancel = new Acr.UserDialogs.ActionSheetOption(cancelSheetOption.Text, cancelSheetOption.Action, cancelSheetOption.Icon);

			foreach (var model in actionSheetOptions)
			{
				var actionSheet = new Acr.UserDialogs.ActionSheetOption(model.Text, model.Action, model.Icon);
				actionSheetConfig.Options.Add(actionSheet);
			}

			UserDialogs.Instance.ActionSheet(actionSheetConfig);
		}

		public static void HideAndDisposeProgress(IProgressDialog progressDialog)
		{
			Debug.WriteLine("disposing progress");
			if (progressDialog != null)
			{
				progressDialog.Hide();
				progressDialog.Dispose();
				Debug.WriteLine("progressDialog disposed");
			}
		}
	}
}