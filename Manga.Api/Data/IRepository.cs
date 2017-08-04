using System;
using System.Collections.Generic;
using Manga.Api.Models;

namespace Manga.Api.Controllers
{
    public interface IRepository<TId, TRecord> 
        where TRecord : class 
    {
        IEnumerable<TRecord> GetAll();
        TRecord Get(TId id);
        void AddOrUpdate(TRecord record, Func<TRecord, TId> getId);
        void RemoveById(TId id);
    }
}
