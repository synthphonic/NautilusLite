using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Models;

namespace Todo.Database
{
	public class TodoRepository
	{
		private IList<TodoItem> _list;

		#region Singleton pattern
		private static readonly TodoRepository _instance = new TodoRepository();

		private TodoRepository()
		{
			_list = new List<TodoItem>();
		}

		public static TodoRepository Instance
		{
			 get { return _instance; }
		}
		#endregion

		public void InitializeData(IList<TodoItem> data)
		{
			_list = data;
		}

		public TodoItem Get(Guid id)
		{
			var found = _list.FirstOrDefault(x => x.Id.Equals(id));
			return found;
		}

		public IEnumerable<TodoItem> GetAll()
		{
			return _list;
		}

		internal void Save(TodoItem newTodoItem)
		{
			throw new NotImplementedException();
		}
	}
}
