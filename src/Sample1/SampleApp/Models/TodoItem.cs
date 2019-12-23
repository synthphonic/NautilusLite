using System;

namespace SampleApp.Models
{
	public class TodoItem
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public bool IsFavorite { get; set; }
	}
}