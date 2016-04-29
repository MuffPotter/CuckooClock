using System.Threading.Tasks;

namespace Sigeko.CuckooClock.Services
{
	public interface ISoundService
	{
		Task PlaySoundAsync(string fileName);
	}
}