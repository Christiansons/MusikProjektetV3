using System.Text.Json.Serialization;

namespace MusikProjektetClient.Models.ViewModels
{
    public class GetAllGenresConnectedToPersonViewModel
    {
        [JsonPropertyName("genreNames")]
        public List<string> GenreNames { get; set; } = new List<string>();
    }
}
