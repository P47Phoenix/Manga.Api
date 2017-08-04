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
    [Route("api/MangaChapter")]
    [ApiVersion("0")]
    public class MangaChapterController : Controller
    {
        private readonly IRepository<string, MangaChapter> m_repository;

        public MangaChapterController(IRepositoryFactory repositoryFactory)
        {
            m_repository = repositoryFactory.CreateRepository<string, MangaChapter>();
        }

        // GET: api/MangaChapter
        [HttpGet(Name = "MangaChapterGet")]
        public JsonApiBody<MangaChapter> Get()
        {
            var chapters = m_repository.GetAll().ToList();

            return new JsonApiBody<MangaChapter>(chapters.Select(chapter => new JsonApiDocument<MangaChapter>(chapter)));
        }

        // GET: api/MangaChapter/5
        [HttpGet("{id}", Name = "MangaChapterGetById")]
        public IActionResult Get(string id)
        {
            MangaChapter chapter = m_repository.Get(id);

            if (chapter == null)
            {
                return NotFound(new
                {
                    Id = id,
                    Error = "Id was not found"
                });
            }

            return Ok(new JsonApiBody<MangaChapter>(
                new List<JsonApiDocument<MangaChapter>>
                {
                    new JsonApiDocument<MangaChapter>(chapter),
                }));
        }

        // POST: api/MangaChapter
        [HttpPost(Name = "MangaChapterPost")]
        public IActionResult Post([FromBody]JsonApiDocument<MangaChapter> value)
        {
            var model = value.Get();

            m_repository.AddOrUpdate(model, chapter => chapter.ChapterId);

            return CreatedAtRoute("MangaChapterGetById", new { id = model.ChapterId }, value);
        }

        // PUT: api/MangaChapter/5
        [HttpPut("{id}", Name = "MangaChapterPut")]
        public IActionResult Put(string id, [FromBody]JsonApiDocument<MangaChapter> value)
        {
            var model = value.Get();

            m_repository.AddOrUpdate(model, chapter => id);

            return CreatedAtRoute("MangaChapterGetById", new { id = model.ChapterId }, value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}", Name = "MangaChapterDelete")]
        public IActionResult Delete(string id)
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

        [HttpOptions(Name = "Options")]
        public IActionResult Options()
        {
            return Options();
        }
    }
}
