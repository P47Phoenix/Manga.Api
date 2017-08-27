using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manga.Api.Data.DynamoDb
{
    public class DynamoDbRepositoryFactory : IRepositoryFactory
    {
        public IRepository<TId, TRefId, TRecord> CreateRepository<TId, TRefId, TRecord>() where TRecord : class
        {
            throw new NotImplementedException();
        }
    }

    public class GenericDynamoDbRepository<TId, TRefId, TRecord> : IRepository<TId, TRefId, TRecord> where TRecord : class
    {
        public IEnumerable<TRecord> GetAll(Func<KeyValue<TId, TRefId>, bool> filter = null)
        {
            throw new NotImplementedException();
        }

        public TRecord Get(KeyValue<TId, TRefId> key)
        {
            throw new NotImplementedException();
        }

        public void AddOrUpdate(TRecord record, Func<TRecord, KeyValue<TId, TRefId>> getKey)
        {
            throw new NotImplementedException();
        }

        public void RemoveById(KeyValue<TId, TRefId> key)
        {
            throw new NotImplementedException();
        }
    }
}
