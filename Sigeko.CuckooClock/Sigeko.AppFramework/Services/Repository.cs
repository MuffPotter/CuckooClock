using System;
using System.Collections.ObjectModel;
using Sigeko.AppFramework.Models;

namespace Sigeko.AppFramework.Services
{
    public abstract class Repository<T> where T : ModelBase
    {
        protected ObservableCollection<T> _items;

        protected Repository()
        {
            _items = new ObservableCollection<T>();
        }

        public ObservableCollection<T> GetList()
        {
            return _items;
        }

        public abstract T GetById(int id);

        public void Initialize(ObservableCollection<T> list)
        {
            _items = list;
        }

        public void Insert(T item)
        {
            if (_items.Contains(item))
            {
                throw new ArgumentException("The given item is already part of the repository.");
            }
            _items.Add(item);
        }

        public void Update(T item)
        {
            if (!_items.Contains(item))
            {
                throw new ArgumentException("The given item is not part of the repository.");
            }
            var index = _items.IndexOf(item);
            _items.Remove(item);
            _items.Insert(index, item);
        }

        public void Delete(T item)
        {
            if (!_items.Contains(item))
            {
                throw new ArgumentException("The given item is not part of the repository.");
            }
            _items.Remove(item);
        }
    }
}
