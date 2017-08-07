using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ifx.JsonApi;
using Ifx.JsonApi.JsonApi;

namespace Manga.Api.Models
{
    public class Page
    {
        [JsonApiId]
        public string PageId { get; set; }

        [JsonApiRelation(typeof(Series), "SeriesGet")]
        public string SeriesId { get; set; }

        [JsonApiRelation(typeof(Series), "ChapterGet", new[] { "SeriesId" })]
        public string ChapterId { get; set; }
    }
}
