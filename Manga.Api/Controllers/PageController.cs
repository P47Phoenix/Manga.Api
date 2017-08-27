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
        [HttpGet("api/Manga/Series/{seriesId}/Chapter/{chapterId}/Page", Name = "PageGet")]
        public IActionResult Get(string seriesId, string chapterId)
        {
            var records = m_repository.GetAll(value => value.RefId == GetRefId(seriesId, chapterId));

            return Ok(new JsonApiDocument<Page>(records, Url));
        }

        // GET: api/Page/5
        [HttpGet("api/Manga/Series/{seriesId}/Chapter/{chapterId}/Page/{id}", Name = "PageGetById")]
        public IActionResult Get(string seriesId, string chapterId, string id)
        {
            var record = m_repository.Get(GetKeyValue(seriesId, chapterId, id));

            return Ok(new JsonApiDocument<Page>(new[] { record }, Url));
        }

        // POST: api/Page
        [HttpPost("api/Manga/Series/{seriesId}/Chapter/{chapterId}/Page", Name = "PagePost")]
        public IActionResult Post(string seriesId, string chapterId, [FromBody]JsonApiData<Page> value)
        {
            var model = value.Get();

            m_repository.AddOrUpdate(model, page => GetKeyValue(seriesId, chapterId, page.PageId));

            return AcceptedAtRoute("GetById", new { seriesId, chapterId, value.Id }, new JsonApiDocument<Page>(new[] { model }, Url));
        }

        private static string GetRefId(string seriesId, string chapterId)
        {
            return seriesId + "_" + chapterId;
        }

        // PUT: api/Page/5
        [HttpPut("api/Manga/Series/{seriesId}/Chapter/{chapterId}/Page/{id}", Name = "PagePut")]
        public IActionResult Put(string seriesId, string chapterId, string id, [FromBody]JsonApiData<Page> value)
        {
            var model = value.Get();

            m_repository.AddOrUpdate(model, page => GetKeyValue(seriesId, chapterId, id));

            return AcceptedAtRoute("GetById", new { seriesId, chapterId, value.Id }, new JsonApiDocument<Page>(new[] { model }, Url));
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
        [HttpDelete("api/Manga/Series/{seriesId}/Chapter/{chapterId}/Page/{id}", Name = "PageDelete")]
        public IActionResult Delete(string seriesId, string chapterId, string id)
        {
            m_repository.RemoveById(GetKeyValue(seriesId, chapterId, id));
            return Ok();
        }
    }
}
