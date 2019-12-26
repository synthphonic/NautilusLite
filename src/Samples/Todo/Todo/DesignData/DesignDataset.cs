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
							ShortDescription = "Take out trash"
						});
			list.Add(
						new TodoItem
						{
							Id = Guid.NewGuid(),
							Name = "Bill - TNB",
							Description = "Pay electricity bill",
							ShortDescription = "Electric bill",
							IsFavorite = true
						});
			list.Add(
						new TodoItem
						{
							Id = Guid.NewGuid(),
							Name = "Bill - Water",
							Description = "Pay water bill",
							ShortDescription = "Water bill"
						});
			list.Add(
						new TodoItem
						{
							Id = Guid.NewGuid(),
							Name = "Bill - Astro",
							Description = "Pay Astro bill",
							ShortDescription = "Astro bill",
							IsFavorite = true
						});
			list.Add(
						new TodoItem
						{
							Id = Guid.NewGuid(),
							Name = "Installment Home Loan",
							Description = "Pay Home Loan installment",
							ShortDescription = "Home loan",
							IsFavorite = true
						});
			list.Add(
						new TodoItem
						{
							Id = Guid.NewGuid(),
							Name = "Supplement Qaseh Premium",
							Description = "Pay Supplement Qaseh Premium",
							ShortDescription = "Qaseh Premium",
							IsFavorite = true
						});
			list.Add(
						new TodoItem
						{
							Id = Guid.NewGuid(),
							Name = "My Medication",
							Description = "Monthly medication - Crestor and Diovan 80",
							ShortDescription = "My Meds"
						});

			return list.AsEnumerable();
		}
	}
}