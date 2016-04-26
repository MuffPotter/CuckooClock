namespace Sigeko.CuckooClock.Controls
{
	public class CheckBoxEventArgs
	{
		/// <summary>
		/// Initializes a new instance of the class with the new type.
		/// </summary>
		/// <param name="isChecked">The changed object.</param>
		public CheckBoxEventArgs(bool isChecked)
		{
			IsChecked = isChecked;
		}

		/// <summary>
		/// Gets the new type.
		/// </summary>
		public bool IsChecked { get; }
	}
}