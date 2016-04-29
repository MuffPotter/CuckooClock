using System.IO;
using System.Threading.Tasks;
using AVFoundation;
using Foundation;
using Sigeko.CuckooClock.iOS.Services;
using Sigeko.CuckooClock.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(SoundProvider))]

namespace Sigeko.CuckooClock.iOS.Services
{
	public class SoundProvider : NSObject, ISoundProvider
	{
		private AVAudioPlayer _player;

		public Task PlaySoundAsync(string fileName)
		{
			var tcs = new TaskCompletionSource<bool>();

			var extension = Path.GetExtension(fileName);
			fileName = Path.GetFileNameWithoutExtension(fileName);
			var path = NSBundle.MainBundle.PathForResource(fileName, extension);

			var url = NSUrl.FromString(path);
			_player = AVAudioPlayer.FromUrl(url);

			_player.FinishedPlaying += (sender, e) =>
			{
				_player = null;
				tcs.SetResult(true);
			};

			_player.Play();

			return tcs.Task;
		}
	}
}