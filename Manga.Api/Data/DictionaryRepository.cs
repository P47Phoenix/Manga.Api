using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Manga.Api.Data
{
    public class DictionaryRepository<TId, TRefKey, TRecord> : IRepository<TId, TRefKey, TRecord>
        where TRecord : class
    {

        private readonly ConcurrentDictionary<KeyValue<TId, TRefKey>, TRecord> m_ConcurrentDictionary = new ConcurrentDictionary<KeyValue<TId, TRefKey>, TRecord>();

        public IEnumerable<TRecord> GetAll(Func<KeyValue<TId, TRefKey>, bool> filter = null)
        {
            if (filter == null)
            {
                return m_ConcurrentDictionary.Values;
            }
            else
            {

                return m_ConcurrentDictionary.Where(keyPair => filter(keyPair.Key)).Select(record => record.Value);
            }
        }

        public TRecord Get(KeyValue<TId, TRefKey> key)
        {
            return m_ConcurrentDictionary.TryGetValue(key, out TRecord record) ? record : null;
        }

        public void AddOrUpdate(TRecord record, Func<TRecord, KeyValue<TId, TRefKey>> getId)
        {
            m_ConcurrentDictionary.AddOrUpdate(getId(record), key => record, (key, value) => record);
        }

        public void RemoveById(KeyValue<TId, TRefKey> key)
        {
            m_ConcurrentDictionary.TryRemove(key, out TRecord value);
        }
    }
}
