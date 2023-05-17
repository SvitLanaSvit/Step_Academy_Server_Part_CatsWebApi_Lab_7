using Newtonsoft.Json;

namespace CatsWebApi.Models
{
    public class Cat
    {
        [JsonProperty("id")]
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        [JsonProperty("breed")]
        public string Breed { get; set; } = default!;
    }
}
