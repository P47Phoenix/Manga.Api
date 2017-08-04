using Manga.Api.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Manga.Api.Controllers
{
    internal static class BuildSimulatorModel
    {
        public static DictionaryRepository<string, Chapter> GetMangaChapter()
        {
            var repo = new DictionaryRepository<string, Chapter>();

            repo.AddOrUpdate(new Chapter()
            {
                Name = "Chapter one",
                ChapterId = "1"
            }, chapter => chapter.ChapterId);
            repo.AddOrUpdate(new Chapter()
            {
                Name = "Chapter one",
                ChapterId = "1"
            }, chapter => chapter.ChapterId);
            repo.AddOrUpdate(new Chapter()
            {
                Name = "Chapter two",
                ChapterId = "2"
            }, chapter => chapter.ChapterId);
            repo.AddOrUpdate(new Chapter()
            {
                Name = "Chapter three",
                ChapterId = "3"
            }, chapter => chapter.ChapterId);
            repo.AddOrUpdate(new Chapter()
            {
                Name = "Chapter four",
                ChapterId = "4"
            }, chapter => chapter.ChapterId);
            repo.AddOrUpdate(new Chapter()
            {
                Name = "Chapter five",
                ChapterId = "5"
            }, chapter => chapter.ChapterId);
            repo.AddOrUpdate(new Chapter()
            {
                Name = "Chapter six",
                ChapterId = "6"
            }, chapter => chapter.ChapterId);

            return repo;
        }

        public static IRepository<string, Series> GetSeriesChapter()
        {
            var repo = new DictionaryRepository<string, Series>();

            repo.AddOrUpdate(new Series()
            {
                Name = "Series one",
                SeriesId = "1"
            }, series => series.SeriesId);
            repo.AddOrUpdate(new Series()
            {
                Name = "Series one",
                SeriesId = "2"
            }, series => series.SeriesId);
            repo.AddOrUpdate(new Series()
            {
                Name = "Series one",
                SeriesId = "3"
            }, series => series.SeriesId);
            repo.AddOrUpdate(new Series()
            {
                Name = "Series one",
                SeriesId = "4"
            }, series => series.SeriesId);
            repo.AddOrUpdate(new Series()
            {
                Name = "Series one",
                SeriesId = "5"
            }, series => series.SeriesId);
            repo.AddOrUpdate(new Series()
            {
                Name = "Series one",
                SeriesId = "6"
            }, series => series.SeriesId);

            return repo;
        }
    }
}
