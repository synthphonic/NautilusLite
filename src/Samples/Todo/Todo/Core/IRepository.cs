using System;
using System.Collections.Generic;

namespace Todo.Core
{
	public interface IRepository<TModel>
	{
		TModel Get(Guid id);
		IEnumerable<TModel> GetAll();
		void Save(TModel model);
		bool Delete(TModel model);
	}
}