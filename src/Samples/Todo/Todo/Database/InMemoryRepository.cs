using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Core;
using Todo.DesignData;
using Todo.Models;

namespace Todo.Database
{
	public class InMemoryRepository : IRepository<TodoItem>
	{
		private IList<TodoItem> _list;

		public InMemoryRepository()
		{
			_list = DesignDataset.CreateTodoItems().ToList();
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

		public void Save(TodoItem newTodoItem)
		{
			_list.Add(newTodoItem);
		}

		public bool Delete(TodoItem todoItem)
		{
			var found = _list.FirstOrDefault(x => x.Id.Equals(todoItem.Id));
			if (found != null)
			{
				_list.Remove(todoItem);
				return true;
			}

			return false;
		}
	}
}