using System.Runtime.InteropServices.ComTypes;

namespace Manga.Api.Controllers
{
    public interface IRepositoryFactory
    {
        IRepository<T, T1> CreateRepository<T, T1>() where T1 : class;
    }
}