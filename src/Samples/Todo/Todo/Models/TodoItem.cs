using System;
using GalaSoft.MvvmLight;

namespace Todo.Models
{
	public class TodoItem : ViewModelBase
	{
		private string _name;
		private bool _completed;
		private bool _isFavorite;

		public TodoItem()
		{
			_isFavorite = true;
		}

		public Guid Id { get; set; }
		public string Name
		{
			get => _name;
			set => Set(ref _name, value);
		}

		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public bool IsFavorite
		{
			get => _isFavorite;
			set => Set(ref _isFavorite, value);
		}

		public DateTime Due { get; set; }

		public bool Completed
		{
			get => _completed;
			set => Set(ref _completed, value);
		}
	}
}