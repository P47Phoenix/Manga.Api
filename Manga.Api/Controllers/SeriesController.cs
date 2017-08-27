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
    public class SeriesController : Controller
    {
        private readonly IRepository<string, string, Series> m_repository;

        public SeriesController(IRepositoryFactory repositoryFactory)
        {
            m_repository = repositoryFactory.CreateRepository<string, string, Series>();
        }

        // GET: api/MangaSeries
        [HttpGet("api/Manga/Series", Name = "SeriesGet")]
        public IActionResult Get()
        {
            var records = m_repository.GetAll();

            return Ok(new JsonApiDocument<Series>(records, Url));
        }

        // GET: api/MangaSeries/5
        [HttpGet("api/Manga/Series/{id}", Name = "SeriesGetById")]
        public IActionResult Get(string id)
        {
            var record = m_repository.Get(new KeyValue<string, string>()
            {
                Id = id
            });
            return Ok(new JsonApiDocument<Series>(new[] { record }, Url));
        }

        // POST: api/MangaSeries
        [HttpPost("api/Manga/Series", Name = "SeriesPost")]
        public IActionResult Post([FromBody]JsonApiData<Series> dataData)
        {
            var record = dataData.Get();
            m_repository.AddOrUpdate(record, series => new KeyValue<string, string>()
            {
                Id = series.SeriesId
            });


            var body = new JsonApiDocument<Series>(new[] { record }, Url);

            return CreatedAtRoute("GetById", new { Id = record.SeriesId }, body);
        }

        // PUT: api/MangaSeries/5
        [HttpPut("api/Manga/Series/{id}", Name = "SeriesPut")]
        public IActionResult Put(string id, [FromBody]JsonApiData<Series> dataData)
        {
            var record = dataData.Get();

            m_repository.AddOrUpdate(record, series => new KeyValue<string, string>()
            {
                Id = id
            });

            var body = new JsonApiDocument<Series>(new[] { record }, Url);

            return CreatedAtRoute("GetById", new { Id = record.SeriesId }, body);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("api/Manga/Series/{id}", Name = "SeriesDelete")]
        public IActionResult Delete(string id)
        {
            m_repository.RemoveById(new KeyValue<string, string>()
            {
                Id = id
            });

            return Ok();
        }
    }
}
