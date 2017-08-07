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

namespace Manga.Api.Controllers
{
    [Produces("application/json")]
    [ApiVersion("0")]
    public class PageController : Controller
    {
        private readonly IRepository<string, string, Page> m_repository;

        public PageController(IRepositoryFactory repositoryFactory)
        {
            m_repository = repositoryFactory.CreateRepository<string, string, Page>();
        }

        // GET: api/Page
        [HttpGet("api/Manga/Series/{mangaSeriesId}/Chapter/{chapterId}", Name = "PageGet")]
        public IActionResult Get(string mangaSeriesId, string chapterId)
        {
            var records = m_repository.GetAll(value => value.RefId == GetRefId(mangaSeriesId, chapterId));

            return Ok(new JsonApiBody<Page>(records, Url));
        }

        // GET: api/Page/5
        [HttpGet("api/Manga/Series/{mangaSeriesId}/Chapter/{chapterId}/Page/{id}", Name = "PageGetById")]
        public IActionResult Get(string mangaSeriesId, string chapterId, string id)
        {
            var record = m_repository.Get(GetKeyValue(mangaSeriesId, chapterId, id));

            return Ok(new JsonApiBody<Page>(new[] { record }, Url));
        }

        // POST: api/Page
        [HttpPost("api/Manga/Series/{mangaSeriesId}/Chapter/{chapterId}/Page", Name = "PagePost")]
        public IActionResult Post(string mangaSeriesId, string chapterId, [FromBody]JsonApiDocument<Page> value)
        {
            var model = value.Get();

            m_repository.AddOrUpdate(model, page => GetKeyValue(mangaSeriesId, chapterId, page.PageId));

            return AcceptedAtRoute("GetById", new { mangaSeriesId, chapterId, value.Id }, new JsonApiBody<Page>(new[] { model }, Url));
        }

        private static string GetRefId(string mangaSeriesId, string chapterId)
        {
            return mangaSeriesId + "_" + chapterId;
        }

        // PUT: api/Page/5
        [HttpPut("api/Manga/Series/{mangaSeriesId}/Chapter/{chapterId}/Page/{id}", Name = "PagePut")]
        public IActionResult Put(string mangaSeriesId, string chapterId, string id, [FromBody]JsonApiDocument<Page> value)
        {
            var model = value.Get();

            m_repository.AddOrUpdate(model, page => GetKeyValue(mangaSeriesId, chapterId, id));

            return AcceptedAtRoute("GetById", new { mangaSeriesId, chapterId, value.Id }, new JsonApiBody<Page>(new[] { model }, Url));
        }

        private static KeyValue<string, string> GetKeyValue(string mangaSeriesId, string chapterId, string id)
        {
            return new KeyValue<string, string>()
            {
                Id = id,
                RefId = GetRefId(mangaSeriesId, chapterId)
            };
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("api/Manga/Series/{mangaSeriesId}/Chapter/{chapterId}/Page/{id}", Name = "PageDelete")]
        public IActionResult Delete(string mangaSeriesId, string chapterId, string id)
        {
            m_repository.RemoveById(GetKeyValue(mangaSeriesId, chapterId, id));
            return Ok();
        }
    }
}
