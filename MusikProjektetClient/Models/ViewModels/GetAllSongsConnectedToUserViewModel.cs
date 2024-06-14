using System.Text.Json.Serialization;

namespace MusikProjektetClient.Models.ViewModels
{
	public class GetAllSongsConnectedToUserViewModel
	{
		[JsonPropertyName("songNames")]
		public List<string> SongNames { get; set; } = new List<string>();
	}
}
