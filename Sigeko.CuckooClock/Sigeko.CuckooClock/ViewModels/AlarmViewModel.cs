using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Sigeko.AppFramework.Navigation;
using Sigeko.CuckooClock.Models;

namespace Sigeko.CuckooClock.ViewModels
{
	public partial class MainViewModel
	{
		private readonly AlarmRepository _alarmRepository;

		private readonly SettingsRepository _settingsRepository;

		#region fields (public)

		private ObservableCollection<Alarm> _alarmList;

		public ObservableCollection<Alarm> AlarmList
		{
			get { return _alarmList; }
			set { SetProperty(ref _alarmList, value); }
		}

		#endregion fields (public)

		#region commands

		private ICommand _addAlarmCommand;

		public ICommand AddAlarmCommand
		{
			get { return _addAlarmCommand; }
			set { SetProperty(ref _addAlarmCommand, value); }
		}

		private async void ExecuteAddAlarmCommand(object parameter)
		{
			var alarm = new Alarm();
			await NavigationService.Current.PushAsync(new AlarmEditViewModel(alarm));
		}

		private ICommand _editAlarmCommand;

		public ICommand EditAlarmCommand
		{
			get { return _editAlarmCommand; }
			set { SetProperty(ref _editAlarmCommand, value); }
		}

		private static async void ExecuteEditAlarmCommand(object parameter)
		{
			var alarm = parameter as Alarm;
			if (alarm == null)
				return;

			await NavigationService.Current.PushAsync(new AlarmEditViewModel(alarm));
		}

		private ICommand _deleteAlarmCommand;

		public ICommand DeleteAlarmCommand
		{
			get { return _deleteAlarmCommand; }
			set { SetProperty(ref _deleteAlarmCommand, value); }
		}

		private async void ExecuteDeleteAlarmCommand(object parameter)
		{
			// Alarm löschen und Liste speichern
			await _alarmRepository.DeleteAlarm(AlarmList, parameter as Alarm);
			await _settingsRepository.SaveSettings();
		}

		#endregion commands
	}
}
