namespace Ifx.JsonApi.Test
{
    public class MockModel
    {
        [JsonApiId]
        public int MockId { get; set; }

        public string StringValue { get; set; }
        public int IntValue { get; set; }
    }
}