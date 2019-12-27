using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using Todo.Models;
using Todo.Views.ViewParameters;

namespace Todo.ViewModels.ContentViewModels
{
	public class ProfileViewModel : ViewModelBase
	{
		private string _message;
		private ObservableCollection<MovieItem> _movieItems;

		internal void LoadData(ProfileViewParameter parameter)
		{
			if (parameter != null)
			{
				Message = parameter.Message;
			}

			LoadMovies();
		}

		private void LoadMovies()
		{
			var list = new ObservableCollection<MovieItem>
			{
				new MovieItem { MovieName = "Iron Man", Year = "2008" },
				new MovieItem { MovieName = "Iron Man 2", Year = "2010" },
				new MovieItem { MovieName = "Iron Man 3", Year = "2013" },
				new MovieItem { MovieName = "Captain America - Civil War", Year = "2016" },
				new MovieItem { MovieName = "Avengers", Year = "2012" },
				new MovieItem { MovieName = "Avengers - Age of Ultron", Year = "2015" },
				new MovieItem { MovieName = "Avengers - Infinity War", Year = "2018" },
				new MovieItem { MovieName = "Avengers - EndGame", Year = "2019" }
			};

			MovieItems = list;
		}

		internal void UnLoadData()
		{
			MovieItems.Clear();
		}

		#region Binding properties
		public string Message
		{
			get { return _message; }
			set { Set(ref _message, value); }
		}

		public ObservableCollection<MovieItem> MovieItems
		{
			get { return _movieItems; }
			set { Set(ref _movieItems, value); }
		}
		#endregion
	}
}