using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ifx.JsonApi;
using Manga.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Manga.Api.Controllers
{
    [Produces("application/json")]
    [ApiVersion("0")]
    public class ChapterController : Controller
    {
        private readonly IRepository<string, string, Chapter> m_repository;

        public ChapterController(IRepositoryFactory repositoryFactory)
        {
            m_repository = repositoryFactory.CreateRepository<string, string, Chapter>();
        }

        [HttpGet("api/Manga/Series/{mangaSeriesId}/Chapter", Name = "ChapterGet")]
        public IActionResult Get(string mangaSeriesId)
        {
            var chapters = m_repository.GetAll(key => key.RefId == mangaSeriesId).ToList();

            return Ok(new JsonApiBody<Chapter>(chapters));
        }

        [HttpGet("api/Manga/Series/{mangaSeriesId}/Chapter/{id}", Name = "ChapterGetById")]
        public IActionResult Get(string mangaSeriesId, string id)
        {
            Chapter chapter = m_repository.Get(
                new KeyValue<string, string>
                {
                    Id = id,
                    RefId = mangaSeriesId
                });

            if (chapter == null)
            {
                return NotFound(new
                {
                    Id = id,
                    Error = "Id was not found"
                });
            }

            return Ok(new JsonApiBody<Chapter>(new[] { chapter }));
        }

        // POST: api/MangaChapter
        [HttpPost("api/Manga/Series/{mangaSeriesId}/Chapter", Name = "ChapterPost")]
        public IActionResult Post(string mangaSeriesId, [FromBody]JsonApiDocument<Chapter> value)
        {
            var model = value.Get();

            m_repository.AddOrUpdate(model, chapter => new KeyValue<string, string>
            {
                Id = chapter.ChapterId,
                RefId = mangaSeriesId
            });

            var body = new JsonApiBody<Chapter>(new[] { model });

            return CreatedAtRoute("MangaChapterGetById", new { id = model.ChapterId, mangaSeriesId }, body);
        }

        [HttpPut("api/Manga/Series/{mangaSeriesId}/Chapter/{id}", Name = "ChapterPut")]
        public IActionResult Put(string mangaSeriesId, string id, [FromBody]JsonApiDocument<Chapter> value)
        {
            var model = value.Get();

            m_repository.AddOrUpdate(model, chapter => new KeyValue<string, string>()
            {
                Id = id,
                RefId = mangaSeriesId
            });


            var body = new JsonApiBody<Chapter>(new[] { model });

            return CreatedAtRoute("MangaChapterGetById", new { id = model.ChapterId, mangaSeriesId }, body);
        }

        [HttpDelete("api/Manga/Series/{mangaSeriesId}/Chapter/{id}", Name = "ChapterDelete")]
        public IActionResult Delete(string mangaSeriesId, string id)
        {
            var key = new KeyValue<string, string>()
            {
                Id = id,
                RefId = mangaSeriesId
            };

            var record = m_repository.Get(key);

            if (record == null)
            {
                return NotFound(new
                {
                    Id = id,
                    Error = "Id was not found"
                });
            }

            m_repository.RemoveById(key);

            return Ok();
        }
    }
}
