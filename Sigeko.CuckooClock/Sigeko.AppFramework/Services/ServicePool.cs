using System;
using System.Collections.Generic;

namespace Sigeko.AppFramework.Services
{
    public class ServicePool
    {
        private static ServicePool _current;
        private Dictionary<Type, object> _services;

        private ServicePool()
        {
            _services = new Dictionary<Type, object>();
        }

        public static ServicePool Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new ServicePool();
                }
                return _current;
            }
        }

        public void AddService<T>(T service) where T : class
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                throw new InvalidOperationException(string.Format("A service of type \"{0}\" was already added to the pool.", type.Name));
            }
            _services.Add(type, service);
        }

        public T GetService<T>() where T : class
        {
            var type = typeof(T);
            if (!_services.ContainsKey(type))
            {
                throw new InvalidOperationException(string.Format("No service of type \"{0}\" was found in the pool.", type.Name));
            }
            return (T)_services[type];
        }

        public void RemoveService<T>() where T : class
        {
            var type = typeof(T);
            if (!_services.ContainsKey(type))
            {
                throw new InvalidOperationException(string.Format("No service of type \"{0}\" was found in the pool.", type.Name));
            }
            _services.Remove(type);
        }
    }
}
