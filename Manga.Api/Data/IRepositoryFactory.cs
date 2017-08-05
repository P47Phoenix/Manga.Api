using System.Runtime.InteropServices.ComTypes;

namespace Manga.Api.Controllers
{
    public interface IRepositoryFactory
    {
        IRepository<TId, TRefId, TRecord> CreateRepository<TId, TRefId, TRecord>() where TRecord : class;
    }
}