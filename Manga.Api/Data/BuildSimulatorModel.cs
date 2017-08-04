using Manga.Api.Models;

namespace Manga.Api.Controllers
{
    internal static class BuildSimulatorModel
    {
        public static DictionaryRepository<string, MangaChapter> GetMangaChapter()
        {
            var repo = new DictionaryRepository<string, MangaChapter>();

            repo.AddOrUpdate(new MangaChapter()
            {
                Name = "Chapter one",
                ChapterId ="1"
            }, chapter => chapter.ChapterId);
            repo.AddOrUpdate(new MangaChapter()
            {
                Name = "Chapter one",
                ChapterId = "1"
            }, chapter => chapter.ChapterId);
            repo.AddOrUpdate(new MangaChapter()
            {
                Name = "Chapter two",
                ChapterId = "2"
            }, chapter => chapter.ChapterId);
            repo.AddOrUpdate(new MangaChapter()
            {
                Name = "Chapter three",
                ChapterId = "3"
            }, chapter => chapter.ChapterId);
            repo.AddOrUpdate(new MangaChapter()
            {
                Name = "Chapter four",
                ChapterId = "4"
            }, chapter => chapter.ChapterId);
            repo.AddOrUpdate(new MangaChapter()
            {
                Name = "Chapter five",
                ChapterId = "5"
            }, chapter => chapter.ChapterId);
            repo.AddOrUpdate(new MangaChapter()
            {
                Name = "Chapter six",
                ChapterId = "6"
            }, chapter => chapter.ChapterId);

            return repo;
        }
    }
}
