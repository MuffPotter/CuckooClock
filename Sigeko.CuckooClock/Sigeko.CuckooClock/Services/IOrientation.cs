namespace Sigeko.CuckooClock.Services
{
	/// <summary>
	/// Note: Classes implementing the interface must have a parameterless 
	/// constructor to work with the DependencyService.
	/// </summary>
	public interface IOrientation
	{
		DeviceOrientation GetOrientation();

		void SetOrientation(DeviceOrientation orientation);

		void SetLandscape(DeviceOrientation orientation);

		void SetPortrait();
	}

	/// <summary>
	/// 
	/// </summary>
	public enum DeviceOrientation
	{
		Undefined,

		Auto,

		Landscape,

		LandscapeLeft,

		LandscapeRight,

		Portrait,

		PortraitUpsideDown
	}
}