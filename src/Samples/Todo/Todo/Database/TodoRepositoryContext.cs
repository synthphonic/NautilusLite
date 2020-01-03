using System;
using System.Collections.Generic;
using Todo.Core;

namespace Todo.Database
{
	public sealed class TodoRepositoryContext<TModel>
	{
		private IRepository<TModel> _repository;

		#region Singleton pattern
		private static readonly TodoRepositoryContext<TModel> _instance = new TodoRepositoryContext<TModel>();

		private TodoRepositoryContext()
		{

		}

		public static TodoRepositoryContext<TModel> Instance
		{
			get { return _instance; }
		}
		#endregion

		public void RegisterRepository(IRepository<TModel> repository)
		{
			_repository = repository;
		}

		public TModel Get(Guid id)
		{
			return _repository.Get(id);
		}

		public IEnumerable<TModel> GetAll()
		{
			return _repository.GetAll();
		}

		public void Save(TModel model)
		{
			_repository.Save(model);
		}

		public bool Delete(TModel model)
		{
			return _repository.Delete(model);
		}
	}
}