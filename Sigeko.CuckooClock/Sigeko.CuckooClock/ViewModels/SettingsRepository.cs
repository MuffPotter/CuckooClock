using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLStorage;
using Sigeko.CuckooClock.Models;

namespace Sigeko.CuckooClock.ViewModels
{
	public class SettingsRepository
	{
		public async Task<bool> DeleteSettings()
		{
			try
			{
				var rootFolder = FileSystem.Current.LocalStorage;
				var folder = await rootFolder.CreateFolderAsync("Sigeko", CreationCollisionOption.OpenIfExists);

				if (await folder.CheckExistsAsync("settings.json") != ExistenceCheckResult.NotFound)
				{
					var file = await folder.GetFileAsync("settings.json");
					await file.DeleteAsync();
				}
				return true;
			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
				return false;
			}
		}

		public async Task<Settings> ReadSettings()
		{
			try
			{
				var rootFolder = FileSystem.Current.LocalStorage;
				var folder = await rootFolder.CreateFolderAsync("Sigeko", CreationCollisionOption.OpenIfExists);

				if (await folder.CheckExistsAsync("settings.json") == ExistenceCheckResult.NotFound)
				{
					// TODO CreateEmptySettings
					var settings = CreateTestSettings();

					var data = JsonConvert.SerializeObject(settings);
					settings = JsonConvert.DeserializeObject<Settings>(data);
					return settings;
				}
				else
				{
					var file = await folder.GetFileAsync("settings.json");
					var data = await file.ReadAllTextAsync();

					var settings = JsonConvert.DeserializeObject<Settings>(data);
					return settings;
				}

			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
				return null;
			}
		}

		private Settings CreateTestSettings()
		{
			// Create settings
			var settings = new Settings
			{
				SaveTime = DateTime.Today,
			};

			// Create alarms
			var alarms = new ObservableCollection<Alarm>();

			for (var i = 0; i < 10; i++)
			{
				var device = CreateAlarm(i);
				alarms.Add(device);
			}
			settings.Alarms = alarms.ToList();

			return settings;
		}

		public async Task<bool> SaveSettings()
		{
			try
			{
				var rootFolder = FileSystem.Current.LocalStorage;
				var folder = await rootFolder.CreateFolderAsync("Sigeko", CreationCollisionOption.OpenIfExists);
				var file = await folder.CreateFileAsync("settings.json", CreationCollisionOption.ReplaceExisting);

				var settings = App.Settings;
				var data = JsonConvert.SerializeObject(settings);
				await file.WriteAllTextAsync(data);
				return true;
			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
				return false;
			}
		}

		private Alarm CreateAlarm(int number)
		{
			return new Alarm
			{
				Id = Guid.NewGuid(),
				Name = "Testalarm " + number,
				IsActive = false,
				Time = TimeSpan.Zero,
				Intervall = 60,
				IntervallTyp = 0,
				WeekDays = new bool[7],
				DayInfo = string.Empty,
				Sound = "Standard",
				SelectCommand = null,
				DeleteCommand = null,
			};
		}
	}
}