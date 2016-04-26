using System;
using System.Linq;
using System.Xml.Serialization;
using Sigeko.AppFramework.Helpers;

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

		private string _sound;

		public string Sound
		{
			get { return _sound; }
			set { SetProperty(ref _sound, value); }
		}

		//private string _dayInfo;

		[XmlIgnore]
		public string DayInfo
		{
			get
			{
				try
				{
					var count = WeekDays.ToList().Count(l => l.Equals(true));
					if (count == 7)
						return "Jeden Tag";
					if (count == 2 && WeekDays[5] && WeekDays[6])
						return "Am Wochenende";
					if (count == 5 && !WeekDays[5] && !WeekDays[6])
						return "Werktags";

					var dayInfo = string.Empty;
					for (var weekDay = 0; weekDay < WeekDays.Length; weekDay++)
					{
						if (WeekDays[weekDay] == false)
							continue;

						var enumValue = weekDay == 6 ? 0 : weekDay + 1;
						var enumText = EnumHelper.GetEnum<DayOfWeek>(enumValue);
						dayInfo += enumText.ToString().Substring(0, 3) + ", ";
					}

					if (string.IsNullOrEmpty(dayInfo) == false)
						dayInfo = dayInfo.Substring(0, dayInfo.Length - 2);

					return dayInfo;
				}
				catch (Exception exception)
				{
					var test = exception.Message;
					return string.Empty;
				}
			}
		}

		[XmlIgnore]
		public DateTime NextAlarm
		{
			get
			{
				//return DateTime.MaxValue;
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
			try
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
				return today.Date.AddDays(deltaDays).Add(time);
			}
			catch (Exception exception)
			{
				var test = exception.Message;
				return DateTime.MaxValue;
			}
		}
	}
}