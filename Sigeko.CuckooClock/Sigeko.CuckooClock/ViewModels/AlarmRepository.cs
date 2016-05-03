using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Sigeko.AppFramework.Services;
using Sigeko.CuckooClock.Common;
using Sigeko.CuckooClock.Models;
using Sigeko.CuckooClock.Services;

namespace Sigeko.CuckooClock.ViewModels
{
	public class AlarmRepository
	{
		/// <summary>
		/// Eigener plattformspefischer DialogService
		/// </summary>
		private IDialogServices _dialogServices;

		/// <summary>
		/// Eigener plattformspefischer DialogService
		/// </summary>
		public IDialogServices DialogServices
			=> _dialogServices ?? (_dialogServices = ServicePool.Current.GetService<IDialogServices>());

		public AlarmRepository()
		{
		}

		public ObservableCollection<Alarm> ReadAlarmList()
		{
			return ReadAlarmList(null, null);
		}

		public ObservableCollection<Alarm> ReadAlarmList(ICommand editCommand, ICommand deleteCommand)
		{
			Logger.Current.LogTrace("ReadAlarmList: START 1");

			var settings = App.Settings;
			if (settings == null)
				return null;

			Logger.Current.LogTrace("ReadAlarmList: START 2");
			if (settings.Alarms == null)
			{
				settings.Alarms = new List<Alarm>();
			}

			foreach (var alarm in settings.Alarms)
			{
				alarm.SelectCommand = editCommand;
				alarm.DeleteCommand = deleteCommand;
				//alarm.DayInfo = "Tage: Mo, Di, Mi, Do, Fr, Sa, So";
			}

			Logger.Current.LogTrace("ReadAlarmList: END");
			return new ObservableCollection<Alarm>(settings.Alarms);
		}

		public async Task<bool> DeleteAlarm(ObservableCollection<Alarm> alarmList, Alarm alarm, bool withConfirmation = false)
		{
			return await DeleteAlarm(alarmList.ToList(), alarm);
		}

		public async Task<bool> DeleteAlarm(List<Alarm> alarmList, Alarm alarm, bool withConfirmation=false)
		{
			if (alarm == null)
				return false;

			// Hinweis ausgeben
			if (DialogServices == null)
				return false;

			var question = "Wollen Sie den aktuellen Alarm wirklich löschen?";
			var success = await DialogServices.ShowAsync("Alarm löschen!", question, "Löschen", "Abbrechen");
			if (success != true)
				return false;

			alarmList.Remove(alarm);
			return true;
		}

		public void SaveAlarm(Alarm alarm)
		{
			var settings = App.Settings;
			if (alarm.Id == Guid.Empty)
			{
				alarm.Id = Guid.NewGuid();
				settings.Alarms.Add(alarm);
			}
		}
	}
}