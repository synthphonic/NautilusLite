using System;
using GalaSoft.MvvmLight;

namespace Todo.Models
{
	public class TodoItem : ViewModelBase
	{
		private string _name;

		public Guid Id { get; set; }
		public string Name
		{
			get => _name;
			set => Set(ref _name, value);
		}
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public bool IsFavorite { get; set; }
	}
}