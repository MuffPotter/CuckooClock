﻿using System.Threading.Tasks;
using Android.Media;
using Sigeko.CuckooClock.Droid.Services;
using Sigeko.CuckooClock.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(SoundProvider))]

namespace Sigeko.CuckooClock.Droid.Services
{
	public class SoundProvider : ISoundProvider
	{
		public Task PlaySoundAsync(string filename)
		{
			// Create media player
			var player = new MediaPlayer();

			// Create task completion source to support async/await
			var tcs = new TaskCompletionSource<bool>();

			// Open the resource
			var fd = Xamarin.Forms.Forms.Context.Assets.OpenFd(filename);

			// Hook up some events
			player.Prepared += (s, e) => {
				player.Start();
			};

			player.Completion += (sender, e) => {
				tcs.SetResult(true);
			};

			// Initialize
			player.SetDataSource(fd.FileDescriptor);
			player.Prepare();

			return tcs.Task;
		}
	}
}