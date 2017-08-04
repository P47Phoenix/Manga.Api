using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ifx.JsonApi;

namespace Manga.Api.Models
{
    public class Chapter
    {
        [JsonApiId]
        public string ChapterId { get; set; }

        public string Name { get; set; }
    }
}
