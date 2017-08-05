using System.Collections.Generic;
using System.Linq;
using Manga.Api.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json.Linq;

namespace Manga.Api.Controllers
{
    internal static class BuildSimulatorModel
    {
        private static List<string> m_ItemIds
            ;

        private static IEnumerable<string> GetIdList(int numberOfIds = 6)
        {
            for (var id = 0; id < numberOfIds; id++)
            {
                yield return id.ToString();
            }
        }

        static BuildSimulatorModel()
        {
            m_ItemIds = GetIdList().ToList();
        }


        public static DictionaryRepository<string, string, Chapter> GetMangaChapter()
        {
            var repo = new DictionaryRepository<string, string, Chapter>();


            foreach (var seriesId in m_ItemIds)
            {

                foreach (var chapterId in m_ItemIds)
                {

                    repo.AddOrUpdate(new Chapter()
                    {
                        Name = "Chapter " + chapterId,
                        ChapterId = chapterId,
                        SeriesId = seriesId,
                        PageIds = m_ItemIds,
                    }, chapter => new KeyValue<string, string>
                    {
                        Id = chapter.ChapterId,
                        RefId = seriesId
                    });
                }
            }

            return repo;
        }

        public static IRepository<string, string, Series> GetSeriesChapter()
        {
            var repo = new DictionaryRepository<string, string, Series>();

            foreach (var seriesId in m_ItemIds)
            {
                repo.AddOrUpdate(new Series()
                {
                    Name = "Series " + seriesId,
                    SeriesId = seriesId,
                    ChapterIds = m_ItemIds
                }, series => new KeyValue<string, string>()
                {
                    Id = series.SeriesId
                });

            }

            return repo;
        }

        public static IRepository<string, string, Page> GetPageChapter()
        {
            var repo = new DictionaryRepository<string, string, Page>();
            foreach (var seriesId in m_ItemIds)
            {
                foreach (var chapterId in m_ItemIds)
                {
                    foreach (var pageId in m_ItemIds)
                    {
                        repo.AddOrUpdate(new Page
                        {
                            SeriesId = seriesId,
                            ChapterId = chapterId,
                            PageId = pageId
                        }, series => new KeyValue<string, string>()
                        {
                            Id = series.SeriesId,
                            RefId = seriesId + "_" + chapterId
                        });
                    }
                }
            }

            return repo;
        }
    }
}
