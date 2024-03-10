using System.Text.Json.Serialization;

namespace MinimalAPIs.Models
{
    public class Genero
    {
        public int GeneroId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Game>? Games { get; set; }
    }
}
