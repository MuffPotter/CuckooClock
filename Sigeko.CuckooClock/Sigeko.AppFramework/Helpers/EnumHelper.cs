using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Sigeko.AppFramework.Helpers
{
	public static class EnumHelper
	{
		#region Description (int, enum)

		/// <summary>
		/// Gibt die Description eines Werts einer Enumeration zurück
		/// </summary>
		/// <typeparam name="TEnum">Der Typ der Enumeration</typeparam>
		/// <param name="value">Der Wert der Enumeration (int)</param>
		/// <returns>Die Description des Wertes</returns>
		public static string Description<TEnum>(int value)
		{
			var type = typeof(TEnum);

			if (Enum.IsDefined(type, value) == false)
			{
				return DisplayName(default(TEnum));
			}

			var enumValue = (TEnum)Enum.Parse(typeof(TEnum), value.ToString(CultureInfo.InvariantCulture));
			return Description(enumValue);
		}

		/// <summary>
		/// Gibt den Description eines Werts einer Enumeration zurück
		/// </summary>
		/// <typeparam name="TEnum">Der Typ der Enumeration</typeparam>
		/// <param name="value">Der Wert der Enumeration (string)</param>
		/// <returns>Die Description des Wertes</returns>
		public static string Description<TEnum>(TEnum value)
		{
			var type = value.GetType();
			var typeInfo = type.GetTypeInfo();
			var fieldinfo = typeInfo.GetDeclaredField(value.ToString());

			Attribute attribute = fieldinfo.GetCustomAttribute<DisplayAttribute>();
			return attribute != null ? ((DisplayAttribute)attribute).Description : value.ToString();
		}

		#endregion Description (int, enum)


		#region DisplayName (int, enum)

		/// <summary>
		/// Gibt den Displayname eines Werts einer Enumeration zurück
		/// </summary>
		/// <typeparam name="TEnum">Der Typ der Enumeration</typeparam>
		/// <param name="value">Der Wert der Enumeration (int)</param>
		/// <returns>Den Displaynamen des Wertes</returns>
		public static string DisplayName<TEnum>(int value)
		{
			var type = typeof(TEnum);

			if (Enum.IsDefined(type, value) == false)
			{
				return DisplayName(default(TEnum));
			}

			var enumValue = (TEnum)Enum.Parse(typeof(TEnum), value.ToString(CultureInfo.InvariantCulture));
			return DisplayName(enumValue);
		}

		/// <summary>
		/// Gibt den Displayname eines Werts einer Enumeration zurück
		/// </summary>
		/// <typeparam name="TEnum">Der Typ der Enumeration</typeparam>
		/// <param name="value">Der Wert der Enumeration (string)</param>
		/// <returns>Den Displaynamen des Wertes</returns>
		public static string DisplayName<TEnum>(TEnum value)
		{
			var type = value.GetType();
			var typeInfo = type.GetTypeInfo();
			var fieldinfo = typeInfo.GetDeclaredField(value.ToString());

			Attribute attribute = fieldinfo.GetCustomAttribute<DisplayAttribute>();
			return attribute != null ? ((DisplayAttribute)attribute).Name : value.ToString();
		}

		#endregion DisplayName (int, enum)

		#region DisplayName --> Enum

		/// <summary>
		/// Gibt den Enumerationswert einer Enumeration zurück
		/// </summary>
		/// <typeparam name="TEnum">Der Typ der Enumeration</typeparam>
		/// <param name="displayName">Der Dispayname des Wertes</param>
		/// <returns>Der Enemurationswert</returns>
		public static TEnum FindByDisplayName<TEnum>(string displayName)
		{
			var enumValue = default(TEnum);
			foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
			{
				if (DisplayName(value) == displayName)
				{
					return value;
				}
			}
			return enumValue;
		}

		#endregion DisplayName --> Enum

		#region DisplayName Listen (string, dictionary)

		/// <summary>
		/// Gibt eine Liste von Displaynamen einer Enumeration zurück
		/// </summary>
		/// <typeparam name="TEnum">Der Typ der Enumeration</typeparam>
		/// <returns>Die Liste der Displaynamen</returns>
		public static Dictionary<int, TEnum> DisplayNamesExt<TEnum>()
		{
			// Dictionary<int, TEnum> list = new Dictionary<int, TEnum>();
			// foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
			// {
			//    list.Add(Convert.ToInt32(value), value);
			// }
			// return list;
			return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToDictionary(value => Convert.ToInt32(value));
		}

		/// <summary>
		/// Gibt eine Liste von Displaynamen einer Enumeration zurück
		/// </summary>
		/// <typeparam name="TEnum">Der Typ der Enumeration</typeparam>
		/// <returns>Die Liste der Displaynamen</returns>
		public static List<string> DisplayNames<TEnum>()
		{
			// List<string> list = new List<string>();
			// foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
			// {
			//    list.Add(DisplayName(value));
			// }
			// return list;
			return (from TEnum value in Enum.GetValues(typeof(TEnum)) select DisplayName(value)).ToList();
		}

		#endregion DisplayName Listen (string, dictionary)

		#region Parse

		/// <summary>
		/// Parst eine Enumeration nach einem Wert
		/// </summary>
		/// <typeparam name="TEnum">Der Typ der Enumeration</typeparam>
		/// <param name="dataToMatch">Der Wert nach dem gesucht wird</param>
		/// <param name="ignorecase">Flag, ob groß/klein ignoriert wird</param>
		/// <returns>Den Wert der Enumeration</returns>
		public static TEnum ParseEnum<TEnum>(this string dataToMatch, bool ignorecase = default(bool))
		  where TEnum : struct
		{
			return dataToMatch.IsItemInEnum<TEnum>()() ? default(TEnum) :
			   (TEnum)Enum.Parse(typeof(TEnum), dataToMatch, ignorecase);
		}

		/// <summary>
		/// Parst eine Enumeration nach einem Wert
		/// </summary>
		/// <typeparam name="TEnum">Der Typ der Enumeration</typeparam>
		/// <param name="dataToCheck">Der Wert nach dem gesucht wird</param>
		/// <returns>Den Wert der Enumeration</returns>
		public static Func<bool> IsItemInEnum<TEnum>(this string dataToCheck)
		   where TEnum : struct
		{
			return () => string.IsNullOrEmpty(dataToCheck) == false && Enum.IsDefined(typeof(TEnum), dataToCheck);
		}

		#endregion Parse

		#region Enum --> int

		#endregion Enum --> int

		public static bool IsDefined(this Enum value)
		{
			return Enum.IsDefined(value.GetType(), value);
		}

		public static string GeValue<T>(string entry)
		{
			return Enum.GetName(typeof(T), entry);
		}

		public static T GetEnum<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value);
		}

		public static T GetEnum<T>(int valueId)
		{
			return (T)Enum.ToObject(typeof(T), valueId);
		}

		public static T[] GetValues<T>() where T : struct
		{
			return (T[])Enum.GetValues(typeof(T));
		}

		public static T GetAttribute<T>(this Enum enumValue) where T : Attribute
		{
			return enumValue.GetType().GetTypeInfo().GetDeclaredField(enumValue.ToString()).GetCustomAttribute<T>();
		}

		public static List<string> GetValues(this Enum value)
		{
			return Enum.GetValues(value.GetType()).Cast<string>().ToList();
		}

		public static int GetValue(this Enum value)
		{
			return Convert.ToInt32(value);
		}
	}
}