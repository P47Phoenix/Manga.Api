using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ifx.JsonApi;
using Ifx.JsonApi.JsonApi;

namespace Manga.Api.Models
{
    public class Chapter
    {
        [JsonApiId]
        public string ChapterId { get; set; }

        [JsonApiRelation(typeof(Series), "SeriesGet")]
        public string SeriesId { get; set; }

        [JsonApiRelation(typeof(Page), "PageGet", new[] { "SeriesId", "ChapterId" })]
        public List<string> PageIds { get; set; }

        public string Name { get; set; }
    }
}
