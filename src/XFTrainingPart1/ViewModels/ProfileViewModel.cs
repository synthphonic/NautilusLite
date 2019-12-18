using System;
using GalaSoft.MvvmLight;
using XFTrainingPart1.Views.ViewParameters;

namespace XFTrainingPart1.ViewModels
{
	public class ProfileViewModel : ViewModelBase
	{
		private string _message;

		internal void LoadData(ProfileViewParameter parameter)
		{
			Message = parameter.Message;
		}

		#region Binding properties
		public string Message
		{
			get { return _message; }
			set { Set(ref _message, value); }
		}
		#endregion
	}
}
