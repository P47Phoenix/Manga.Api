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
        private readonly IRepository<string, Chapter> m_repository;

        public ChapterController(IRepositoryFactory repositoryFactory)
        {
            m_repository = repositoryFactory.CreateRepository<string, Chapter>();
        }

        [HttpGet("api/Manga/Series/{mangaSeriesId}/Chapter", Name = "MangaChapterGet")]
        public IActionResult Get(string mangaSeriesId)
        {
            var chapters = m_repository.GetAll().ToList();

            return Ok(new JsonApiBody<Chapter>(chapters));
        }

        [HttpGet("api/Manga/Series/{mangaSeriesId}/Chapter/{id}", Name = "MangaChapterGetById")]
        public IActionResult Get(string mangaSeriesId, string id)
        {
            Chapter chapter = m_repository.Get(id);

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
        [HttpPost("api/Manga/Series/{mangaSeriesId}/Chapter", Name = "MangaChapterPost")]
        public IActionResult Post(string mangaSeriesId, [FromBody]JsonApiDocument<Chapter> value)
        {
            var model = value.Get();

            m_repository.AddOrUpdate(model, chapter => chapter.ChapterId);

            var body = new JsonApiBody<Chapter>(new[] { model });

            return CreatedAtRoute("MangaChapterGetById", new { id = model.ChapterId, mangaSeriesId }, body);
        }

        [HttpPut("api/Manga/Series/{mangaSeriesId}/Chapter/{id}", Name = "MangaChapterPut")]
        public IActionResult Put(string mangaSeriesId, string id, [FromBody]JsonApiDocument<Chapter> value)
        {
            var model = value.Get();

            m_repository.AddOrUpdate(model, chapter => id);

            var body = new JsonApiBody<Chapter>(new[] {model});

            return CreatedAtRoute("MangaChapterGetById", new { id = model.ChapterId, mangaSeriesId }, body);
        }

        [HttpDelete("api/Manga/Series/{mangaSeriesId}/Chapter/{id}", Name = "MangaChapterDelete")]
        public IActionResult Delete(string mangaSeriesId, string id)
        {
            if (m_repository.Get(id) == null)
            {
                return NotFound(new
                {
                    Id = id,
                    Error = "Id was not found"
                });
            }

            m_repository.RemoveById(id);

            return Ok();
        }
    }
}
