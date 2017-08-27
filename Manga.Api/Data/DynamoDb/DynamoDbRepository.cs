using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manga.Api.Data.DynamoDb
{
    public class DynamoDbRepository<TId, TRefKey, TRecord> : IRepository<TId,TRefKey, TRecord> where TRecord : class 
    {
        public IEnumerable<TRecord> GetAll(Func<KeyValue<TId, TRefKey>, bool> filter = null)
        {
            throw new NotImplementedException();
        }

        public TRecord Get(KeyValue<TId, TRefKey> key)
        {
            throw new NotImplementedException();
        }

        public void AddOrUpdate(TRecord record, Func<TRecord, KeyValue<TId, TRefKey>> getKey)
        {
            throw new NotImplementedException();
        }

        public void RemoveById(KeyValue<TId, TRefKey> key)
        {
            throw new NotImplementedException();
        }
    }
}
