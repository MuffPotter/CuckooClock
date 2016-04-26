namespace Sigeko.AppFramework.Services
{
    public interface IServicePool
    {
        void AddService<T>(T service) where T : class;
        T GetService<T>() where T : class;
        void RemoveService<T>() where T : class;
    }
}
