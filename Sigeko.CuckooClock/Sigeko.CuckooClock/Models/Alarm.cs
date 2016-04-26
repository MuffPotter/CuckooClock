using System;

namespace Sigeko.CuckooClock.Models
{
	public class Alarm : ListViewBase
	{
		public Alarm()
		{
			Id = Guid.Empty;
			WeekDays = new bool[7];
		}

		public Guid Id { get; set; }

		private string _name;

		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}

		private bool _isActive;

		public bool IsActive
		{
			get { return _isActive; }
			set { SetProperty(ref _isActive, value); }
		}

		private TimeSpan _time;

		public TimeSpan Time
		{
			get { return _time; }
			set { SetProperty(ref _time, value); }
		}

		private int _intervall;

		public int Intervall
		{
			get { return _intervall; }
			set { SetProperty(ref _intervall, value); }
		}

		private int _intervallTyp;

		public int IntervallTyp
		{
			get { return _intervallTyp; }
			set { SetProperty(ref _intervallTyp, value); }
		}

		private bool[] _weekDays;

		public bool[] WeekDays
		{
			get { return _weekDays; }
			set { SetProperty(ref _weekDays, value); }
		}

		private string _dayInfo;

		public string DayInfo
		{
			get { return _dayInfo; }
			set { SetProperty(ref _dayInfo, value); }
		}

		private string _sound;

		public string Sound
		{
			get { return _sound; }
			set { SetProperty(ref _sound, value); }
		}

		public DateTime NextAlarm
		{
			get
			{
				if (IsActive == false)
					return DateTime.MaxValue;

				var nextAlarm = DateTime.MaxValue;
				for (var weekDay = 0; weekDay < WeekDays.Length; weekDay++)
				{
					if (WeekDays[weekDay] != true)
						continue;

					var alarm = CalculatedNextAlarm(weekDay + 1);
					if (alarm < nextAlarm)
						nextAlarm = alarm;
				}

				return nextAlarm;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="weekDay"></param>
		/// <returns></returns>
		private DateTime CalculatedNextAlarm(int weekDay)
		{
			// DayOfWeek: 1=Mo, 2=Di, 3=Mi, 4=Do, 5=Fr, 6=Sa, 7=So
			var today = DateTime.Now;
			var currentDay = (int)today.DayOfWeek;
			var time = Time;

			if (weekDay < currentDay)
				time = time.Add(new TimeSpan(7, 0, 0, 0));

			while (time < today.TimeOfDay)
			{
				time = time.Add(IntervallTyp == 0 
					? new TimeSpan(0, Intervall, 0) 
					: new TimeSpan(Intervall, 0, 0));
			}

			var deltaDays = weekDay - currentDay;
			//if (deltaDays < 0)
			//	deltaDays += 7;
			return today.Date.AddDays(deltaDays).Add(time);
		}
	}
}