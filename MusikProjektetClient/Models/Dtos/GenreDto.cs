using System.Text.Json.Serialization;

namespace MusikProjektetClient.Models.Dtos
{
    public class GenreDto
    {
        [JsonPropertyName("GenreName")]
        public string GenreName { get; set; }
    }
}
