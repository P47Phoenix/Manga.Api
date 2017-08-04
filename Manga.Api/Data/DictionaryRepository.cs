using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Manga.Api.Controllers
{
    public class DictionaryRepository<TKey, TValue> : IRepository<TKey, TValue> where TValue : class
    {
        private ConcurrentDictionary<TKey, TValue> m_ConcurrentDictionary = new ConcurrentDictionary<TKey, TValue>();

        public IEnumerable<TValue> GetAll()
        {
            return m_ConcurrentDictionary.Values;
        }

        public TValue Get(TKey id)
        {
            return m_ConcurrentDictionary.TryGetValue(id, out TValue record) ? record : null;
        }

        public void AddOrUpdate(TValue record, Func<TValue, TKey> getId)
        {
            m_ConcurrentDictionary.AddOrUpdate(getId.Invoke(record), key => record, (key, value) => record);
        }

        public void RemoveById(TKey id)
        {
            m_ConcurrentDictionary.TryRemove(id, out TValue value);
        }
    }
}
