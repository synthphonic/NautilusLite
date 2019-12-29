using System;
using GalaSoft.MvvmLight;

namespace Todo.Models
{
	public class TodoItem : ViewModelBase
	{
		private Guid _id;
		private DateTime _due;
		private string _name;
		private string _description;
		private string _shortDescription;
		private bool _completed;
		private bool _isFavorite;

		public TodoItem()
		{
			_id = Guid.NewGuid();
			_isFavorite = false;
			_due = DateTime.Now;
		}

		public TodoItem(Guid id) : this()
		{
			_id = id;
		}

		public Guid Id
		{
			get => _id;
			set => Set(ref _id, value);
		}

		public string Name
		{
			get => _name;
			set => Set(ref _name, value);
		}

		public string Description
		{
			get => _description;
			set => Set(ref _description, value);
		}

		public string ShortDescription
		{
			get => _shortDescription;
			set => Set(ref _shortDescription, value);
		}

		public bool IsFavorite
		{
			get => _isFavorite;
			set => Set(ref _isFavorite, value);
		}

		public DateTime Due
		{
			get => _due;
			set => Set(ref _due, value);
		}

		public bool Completed
		{
			get => _completed;
			set => Set(ref _completed, value);
		}
	}
}