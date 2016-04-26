using System;

namespace Sigeko.AppFramework.Helpers
{
	public class DescriptionAttribute : Attribute
	{
		public string Description { get; }

		public DescriptionAttribute(string description)
		{
			Description = description;
		}
	}
}