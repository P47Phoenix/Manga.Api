using System;
using System.Collections.Generic;
using Manga.Api.Models;

namespace Manga.Api.Controllers
{
    public interface IRepository<TId, TRefKey, TRecord>
        where TRecord : class
    {
        IEnumerable<TRecord> GetAll(Func<KeyValue<TId, TRefKey>, bool> filter = null);
        TRecord Get(KeyValue<TId, TRefKey> key);
        void AddOrUpdate(TRecord record, Func<TRecord, KeyValue<TId, TRefKey>> getKey);
        void RemoveById(KeyValue<TId, TRefKey> key);
    }
}
