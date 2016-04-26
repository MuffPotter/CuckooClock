using System;

namespace Sigeko.CuckooClock.Models
{
	public class ButtonEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the class with the new type.
		/// </summary>
		/// <param name="type">The changed object.</param>
		public ButtonEventArgs(ToolbarButtonType type)
		{
			NewType = type;
		}

		/// <summary>
		/// Gets the new type.
		/// </summary>
		public ToolbarButtonType NewType { get; }
	}
}