namespace Manga.Api.Data
{
    public class KeyValue<TId, TRefId>
    {
        public TId Id { get; set; }
        public TRefId RefId { get; set; }
    }
}