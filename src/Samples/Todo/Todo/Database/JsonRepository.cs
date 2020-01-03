using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NautilusLite.Infrastructure.IO;
using Newtonsoft.Json;
using Todo.Core;
using Todo.DesignData;
using Todo.Models;

namespace Todo.Database
{
	public class JsonRepository : IRepository<TodoItem>
	{
		private const string DataFileName = "todo_data.json";
		private readonly string _dataFilePath;
		private LocalStorage _localStorage;

		public JsonRepository()
		{
			_localStorage = LocalStorage.Instance;
			_dataFilePath = Path.Combine(_localStorage.RootPath, DataFileName);

			InitializeDataFile();
		}

		public TodoItem Get(Guid id)
		{
			var items = DeserializeDataFile();
			var foundItem = items.FirstOrDefault(x => x.Id.Equals(id));

			return foundItem;
		}

		public IEnumerable<TodoItem> GetAll()
		{
			return DeserializeDataFile();
		}

		public void Save(TodoItem todoItem)
		{
			var items = DeserializeDataFile();
			var found = items.FirstOrDefault(x => x.Id.Equals(todoItem.Id));
			if (found != null)
			{
				found.Completed = todoItem.Completed;
				found.Description = todoItem.Description;
				found.Due = todoItem.Due;
				found.IsFavorite = todoItem.IsFavorite;
				found.Name = todoItem.Name;
				found.ShortDescription = todoItem.ShortDescription;
			}
			else
			{
				items.Add(todoItem);
			}

			SaveToDataFile(items);
		}

		private void InitializeDataFile()
		{
			if (!File.Exists(_dataFilePath))
			{
				var list = DesignDataset.CreateTodoItems().ToList();
				var content = JsonConvert.SerializeObject(list);

				_localStorage.SaveFile(_dataFilePath, content);
			}
		}

		private void SaveToDataFile(IList<TodoItem> data)
		{
			var content = JsonConvert.SerializeObject(data);
			_localStorage.SaveFile(_dataFilePath, content);
		}

		private IList<TodoItem> DeserializeDataFile()
		{
			var content = _localStorage.LoadFileAsString(_dataFilePath, Encoding.UTF8);
			var todoItems = JsonConvert.DeserializeObject<IList<TodoItem>>(content);

			return todoItems;
		}
	}
}