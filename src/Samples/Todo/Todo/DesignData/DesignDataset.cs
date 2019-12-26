using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Todo.Models;

namespace Todo.DesignData
{
	public static class DesignDataset
	{
		public static IEnumerable<TodoItem> CreateTodoItems()
		{
			var list = new ObservableCollection<TodoItem>();

			list.Add(
						new TodoItem
						{
							Id = Guid.NewGuid(),
							Name = "Take out trash",
							Description = "Take out trash every 2 days",
							ShortDescription = "Take out trash",
							Due = DateTime.Today.AddDays(1)
						});
			list.Add(
						new TodoItem
						{
							Id = Guid.NewGuid(),
							Name = "Bill - TNB",
							Description = "Pay electricity bill",
							ShortDescription = "Electric bill",
							IsFavorite = true,
							Due = DateTime.Today.AddDays(3)
						});
			list.Add(
						new TodoItem
						{
							Id = Guid.NewGuid(),
							Name = "Bill - Water",
							Description = "Pay water bill",
							ShortDescription = "Water bill",
							Due = DateTime.Today.AddDays(-5)
						});
			list.Add(
						new TodoItem
						{
							Id = Guid.NewGuid(),
							Name = "Bill - Astro",
							Description = "Pay Astro bill",
							ShortDescription = "Astro bill",
							IsFavorite = true,
							Due = DateTime.Today.AddDays(-1)
						});
			list.Add(
						new TodoItem
						{
							Id = Guid.NewGuid(),
							Name = "Installment Home Loan",
							Description = "Pay Home Loan installment",
							ShortDescription = "Home loan",
							IsFavorite = true,
							Due = DateTime.Today.AddDays(10)
						});
			list.Add(
						new TodoItem
						{
							Id = Guid.NewGuid(),
							Name = "Supplement Qaseh Premium",
							Description = "Pay Supplement Qaseh Premium",
							ShortDescription = "Qaseh Premium",
							IsFavorite = true,
							Due = DateTime.Today.AddDays(6)
						});
			list.Add(
						new TodoItem
						{
							Id = Guid.NewGuid(),
							Name = "My Medication",
							Description = "Monthly medication - Crestor and Diovan 80",
							ShortDescription = "My Meds",
							Due = DateTime.Today.AddDays(2)
						});

			return list.AsEnumerable();
		}
	}
}