using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ifx.JsonApi;
using Ifx.JsonApi.JsonApi;
using Manga.Api.Data;
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

        [HttpGet("api/Manga/Series/{seriesId}/Chapter", Name = "ChapterGet")]
        public IActionResult Get(string seriesId)
        {
            var chapters = m_repository.GetAll(key => key.RefId == seriesId).ToList();

            return Ok(new JsonApiBody<Chapter>(chapters, Url));
        }

        [HttpGet("api/Manga/Series/{seriesId}/Chapter/{id}", Name = "ChapterGetById")]
        public IActionResult Get(string seriesId, string id)
        {
            Chapter chapter = m_repository.Get(
                new KeyValue<string, string>
                {
                    Id = id,
                    RefId = seriesId
                });

            if (chapter == null)
            {
                return NotFound(new
                {
                    Id = id,
                    Error = "Id was not found"
                });
            }

            return Ok(new JsonApiBody<Chapter>(new[] { chapter }, Url));
        }

        // POST: api/MangaChapter
        [HttpPost("api/Manga/Series/{seriesId}/Chapter", Name = "ChapterPost")]
        public IActionResult Post(string seriesId, [FromBody]JsonApiDocument<Chapter> value)
        {
            var model = value.Get();

            m_repository.AddOrUpdate(model, chapter => new KeyValue<string, string>
            {
                Id = chapter.ChapterId,
                RefId = seriesId
            });

            var body = new JsonApiBody<Chapter>(new[] { model }, Url);

            return CreatedAtRoute("MangaChapterGetById", new { id = model.ChapterId, seriesId }, body);
        }

        [HttpPut("api/Manga/Series/{seriesId}/Chapter/{id}", Name = "ChapterPut")]
        public IActionResult Put(string seriesId, string id, [FromBody]JsonApiDocument<Chapter> value)
        {
            var model = value.Get();

            m_repository.AddOrUpdate(model, chapter => new KeyValue<string, string>()
            {
                Id = id,
                RefId = seriesId
            });


            var body = new JsonApiBody<Chapter>(new[] { model }, Url);

            return CreatedAtRoute("MangaChapterGetById", new { id = model.ChapterId, seriesId }, body);
        }

        [HttpDelete("api/Manga/Series/{seriesId}/Chapter/{id}", Name = "ChapterDelete")]
        public IActionResult Delete(string seriesId, string id)
        {
            var key = new KeyValue<string, string>()
            {
                Id = id,
                RefId = seriesId
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
