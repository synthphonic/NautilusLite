using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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

		#region Public methods
		public void CreateFolder(string folderName)
		{
			var folderPath = Path.Combine(RootPath, folderName);

			try
			{
				if (!Directory.Exists(folderPath))
				{
					Directory.CreateDirectory(folderPath);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void DeleteFolder(string folderName, bool recursiveDeletion = false)
		{
			var fullPath = Path.Combine(RootPath, folderName);

			try
			{
				Directory.Delete(fullPath, recursiveDeletion);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public IEnumerable<string> GetAllFileNames(string path)
		{
			var fullPath = string.IsNullOrWhiteSpace(path) ? RootPath : Path.Combine(RootPath, path);

			if (Directory.Exists(fullPath))
			{
				var dirInfo = new DirectoryInfo(fullPath);
				var fileInfos = dirInfo.GetFiles();

				var fileNames = new List<string>();
				fileInfos.ToList().ForEach(o => fileNames.Add(o.Name));
				return fileNames;
			}

			return null;
		}

		public byte[] LoadFileAsBytes(string fileName)
		{
			var fullFilePath = Path.Combine(RootPath, fileName);

			try
			{
				var contentBytes = File.ReadAllBytes(fullFilePath);
				return contentBytes;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public string LoadFileAsString(string fileName, Encoding encoding)
		{
			var fullFilePath = Path.Combine(RootPath, fileName);

			try
			{
				var contentString = File.ReadAllText(fullFilePath, encoding);
				return contentString;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void SaveFile(string fileName, string content)
		{
			try
			{
				var fullFilePath = Path.Combine(RootPath, fileName);
				File.WriteAllText(fullFilePath, content);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void SaveFile(string fileName, byte[] bytes)
		{
			try
			{
				var fullFilePath = Path.Combine(RootPath, fileName);
				File.WriteAllBytes(fullFilePath, bytes);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void SaveFile(string fileName, Stream stream)
		{
			var fullFilePath = Path.Combine(RootPath, fileName);

			if (File.Exists(fullFilePath))
			{
				File.Delete(fullFilePath);
			}

			try
			{
				using (var fileStream = File.OpenWrite(fullFilePath))
				{
					if (stream.CanSeek)
					{
						stream.Position = 0;
					}

					stream.CopyTo(fileStream);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task SaveFileAsync(string fileName, Stream stream)
		{
			var fullFilePath = Path.Combine(RootPath, fileName);

			if (File.Exists(fullFilePath))
			{
				File.Delete(fullFilePath);
			}

			try
			{
				using (var fileStream = File.OpenWrite(fullFilePath))
				{
					if (stream.CanSeek)
					{
						stream.Position = 0;
					}

					await stream.CopyToAsync(fileStream);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public ImageSource LoadPicture(string fileName)
		{
			var fullFilePath = Path.Combine(RootPath, fileName);

			try
			{
				return ImageSource.FromFile(fullFilePath);
			}
			catch (Exception)
			{
				throw;
			}			
		}

		public bool FileExists(string fileName)
		{
			var fullFilePath = Path.Combine(RootPath, fileName);
			return File.Exists(fullFilePath);
		}

		public bool FolderExists(string folderName)
		{
			var fullFilePath = Path.Combine(RootPath, folderName);
			return Directory.Exists(fullFilePath);
		}

		#endregion

		#region Private methods
		private void SetPath()
		{
			RootPath = Xamarin.Essentials.FileSystem.AppDataDirectory;
		}
		#endregion

		#region Properties
		public string RootPath { get; private set; }
		#endregion
	}
}