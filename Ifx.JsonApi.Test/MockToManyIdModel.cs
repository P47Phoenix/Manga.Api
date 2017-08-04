namespace Ifx.JsonApi.Test
{
    public class MockToManyIdModel
    {
        [JsonApiId]
        public int Id { get; set; }

        [JsonApiId]
        public int IdTwo { get; set; }
    }
}