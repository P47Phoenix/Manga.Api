using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ifx.JsonApi;
using Manga.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manga.Api.Controllers
{
    [Produces("application/json")]
    [ApiVersion("0")]
    public class SeriesController : Controller
    {
        private readonly IRepository<string, Series> m_repository;

        public SeriesController(IRepositoryFactory repositoryFactory)
        {
            m_repository = repositoryFactory.CreateRepository<string, Series>();
        }

        // GET: api/MangaSeries
        [HttpGet("api/Manga/Series", Name = "Get")]
        public IActionResult Get()
        {
            var records = m_repository.GetAll();

            return Ok(new JsonApiBody<Series>(records));
        }

        // GET: api/MangaSeries/5
        [HttpGet("api/Manga/Series/{id}", Name = "GetById")]
        public IActionResult Get(string id)
        {
            var record = m_repository.Get(id);
            return Ok(new JsonApiBody<Series>(new[] { record }));
        }

        // POST: api/MangaSeries
        [HttpPost("api/Manga/Series")]
        public IActionResult Post([FromBody]JsonApiDocument<Series> document)
        {
            var record = document.Get();
            m_repository.AddOrUpdate(record, series => series.SeriesId);

            var body = new JsonApiBody<Series>(new[] { record });

            return CreatedAtRoute("GetById", new { Id = record.SeriesId }, body);
        }

        // PUT: api/MangaSeries/5
        [HttpPut("api/Manga/Series/{id}")]
        public IActionResult Put(string id, [FromBody]JsonApiDocument<Series> document)
        {
            var record = document.Get();
            m_repository.AddOrUpdate(record, series => id);

            var body = new JsonApiBody<Series>(new[] { record });

            return CreatedAtRoute("GetById", new { Id = record.SeriesId }, body);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("api/Manga/Series/{id}")]
        public IActionResult Delete(string id)
        {
            m_repository.RemoveById(id);

            return Ok();
        }
    }
}
