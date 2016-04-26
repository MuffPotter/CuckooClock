namespace Sigeko.CuckooClock.UWP
{
	public sealed partial class MainPage
	{
		public MainPage()
		{
			this.InitializeComponent();

			LoadApplication(new CuckooClock.App());
		}
	}
}
