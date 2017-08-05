using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ifx.JsonApi;

namespace Manga.Api.Models
{
    public class Series
    {
        [JsonApiId]
        public string SeriesId { get; set; }

        [JsonApiRelation(typeof(Chapter), "ChapterGet", new[] { "SeriesId" })]

        public List<string> ChapterIds { get; set; }

        public string Name { get; set; }
    }
}
