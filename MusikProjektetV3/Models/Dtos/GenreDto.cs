using System.Text.Json.Serialization;

namespace MusikProjektetV3.Models.Dtos
{
    public class GenreDto
    {
        [JsonPropertyName("GenreName")]
        public string GenreName { get; set; }
    }
}
