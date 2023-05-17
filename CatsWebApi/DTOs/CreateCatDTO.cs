using Newtonsoft.Json;

namespace CatsWebApi.DTOs
{
    public class CreateCatDTO
    {
        public string Name { get; set; } = default!;
        public string Breed { get; set; } = default!;
    }
}
