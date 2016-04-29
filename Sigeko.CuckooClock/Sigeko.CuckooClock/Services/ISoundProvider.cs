using System.Threading.Tasks;

namespace Sigeko.CuckooClock.Services
{
	public interface ISoundProvider
	{
		Task PlaySoundAsync(string fileName);
	}
}