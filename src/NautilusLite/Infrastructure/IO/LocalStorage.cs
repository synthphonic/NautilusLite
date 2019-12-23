namespace NautilusLite.Infrastructure.IO
{
	public sealed class LocalStorage
	{
		#region Singleton pattern
		private static readonly LocalStorage _instance = new LocalStorage();

		private LocalStorage()
		{
			SetPath();
		}

		public static LocalStorage Instance
		{
			get { return _instance; }
		}
		#endregion

		private void SetPath()
		{
			RootPath = Xamarin.Essentials.FileSystem.AppDataDirectory;
		}

		public string RootPath { get; private set; }
	}
}