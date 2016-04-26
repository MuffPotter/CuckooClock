using Sigeko.AppFramework.Services;
using Sigeko.CuckooClock.Services;

namespace Sigeko.CuckooClock
{
    public static class BootStrapper
    {
        public static void InitializeServices()
        {
            ServicePool.Current.AddService<IMainMenuService>(new MainMenuService());
			ServicePool.Current.AddService<IBluetoothService>(new BluetoothService());
		}
	}
}
