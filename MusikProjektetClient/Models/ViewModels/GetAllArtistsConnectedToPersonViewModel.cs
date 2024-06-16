using System.Text.Json.Serialization;

namespace MusikProjektetClient.Models.ViewModels
{
	public class GetAllArtistsConnectedToPersonViewModel
	{
		[JsonPropertyName("artistNames")]
		public List<string> ArtistNames { get; set; } = new List<string>();
	}
}
