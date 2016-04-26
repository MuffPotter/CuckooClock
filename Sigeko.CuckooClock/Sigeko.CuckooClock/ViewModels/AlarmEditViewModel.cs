using System;
using System.Windows.Input;
using Sigeko.AppFramework.Commands;
using Sigeko.AppFramework.Navigation;
using Sigeko.CuckooClock.Models;

namespace Sigeko.CuckooClock.ViewModels
{
	public class AlarmEditViewModel : BaseViewModel
	{
		private readonly AlarmRepository _alarmRepository;

		private readonly SettingsRepository _settingsRepository;

		private Alarm _alarm;

		public Alarm Alarm
		{
			get { return _alarm; }
			set { SetProperty(ref _alarm, value); }
		}

		public AlarmEditViewModel(Alarm alarm)
		{
			Alarm = alarm;

			_alarmRepository = new AlarmRepository();
			_settingsRepository = new SettingsRepository();

			this.SaveAlarmCommand = new DelegateCommand(ExecuteSaveAlarmCommand);
			this.DeleteAlarmCommand = new DelegateCommand(ExecuteDeleteAlarmCommand);

			PageTitel = alarm.Id == Guid.Empty ? "Neuer Alarm" : "Bearbeiten";
			App.ActiveView = 1;
		}

		#region fields (public)

		#endregion fields (public)

		#region commands

		private ICommand _saveAlarmCommand;

		public ICommand SaveAlarmCommand
		{
			get { return _saveAlarmCommand; }
			set { SetProperty(ref _saveAlarmCommand, value); }
		}

		private async void ExecuteSaveAlarmCommand(object parameter)
		{
			_alarmRepository.SaveAlarm(Alarm);
			await _settingsRepository.SaveSettings();

			await NavigationService.Current.PushBackToAsync(typeof(MainViewModel));
		}

		private ICommand _deleteAlarmCommand;

		public ICommand DeleteAlarmCommand
		{
			get { return _deleteAlarmCommand; }
			set { SetProperty(ref _deleteAlarmCommand, value); }
		}

		private async void ExecuteDeleteAlarmCommand(object parameter)
		{
			if (Alarm.Id == Guid.Empty)
				return;

			var success = await _alarmRepository.DeleteAlarm(App.Settings.Alarms, Alarm);
			if (success == true)
			{
				await _settingsRepository.SaveSettings();
				await NavigationService.Current.PushBackToAsync(typeof(MainViewModel));
			}
		}

		#endregion commands
	}
}
