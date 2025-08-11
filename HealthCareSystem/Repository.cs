using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCareSystem
{
    public class Repository<T>
    {
        private List<T> items;

        public Repository()
        {
            items = new List<T>();
        }

        public void Add(T item) => items.Add(item);

        public List<T> GetAll() => items;

        public T? GetById(Func<T, bool> predicate) =>
            items.FirstOrDefault(predicate);

        public bool Remove(Func<T, bool> predicate)
        {
            var item = items.FirstOrDefault(predicate);
            if (item != null)
            {
                return items.Remove(item);
            }
            return false;
        }
    }
}
