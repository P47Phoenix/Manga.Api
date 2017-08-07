namespace Manga.Api.Data
{
    public interface IRepositoryFactory
    {
        IRepository<TId, TRefId, TRecord> CreateRepository<TId, TRefId, TRecord>() where TRecord : class;
    }
}