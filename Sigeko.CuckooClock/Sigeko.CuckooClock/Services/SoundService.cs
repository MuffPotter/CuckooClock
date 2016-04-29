using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Services
{
	/// <summary>
	/// https://github.com/chrfalch/SoundPlayer
	/// </summary>
	public class SoundService : ISoundService
	{
		private readonly ISoundProvider _soundProvider;

		public SoundService()
		{
			_soundProvider = DependencyService.Get<ISoundProvider>();
		}

		public Task PlaySoundAsync(string fileName)
		{
			return _soundProvider.PlaySoundAsync(fileName);
		}
	}
}