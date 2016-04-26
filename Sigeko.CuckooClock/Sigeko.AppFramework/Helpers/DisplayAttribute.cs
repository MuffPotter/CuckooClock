using System;

namespace Sigeko.AppFramework.Helpers
{
	public class DisplayAttribute : Attribute
	{
		private string _name;

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private string _description;

		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		public DisplayAttribute()
		{
			_name = string.Empty;
			_description = string.Empty;
		}

		public DisplayAttribute(string name)
		{
			_name = name;
			_description = string.Empty;
		}
		public DisplayAttribute(string name, string description)
		{
			_name = name;
			_description = description;
		}
	}
}