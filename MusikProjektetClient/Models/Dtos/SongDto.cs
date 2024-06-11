using System.Text.Json.Serialization;
using MusikProjektetClient.Models;

namespace MusikProjektetClient.Models.Dtos
{
	public class AddSongDto
	{
		[JsonPropertyName("songTitle")]
		public string SongTitle { get; set; }

		[JsonPropertyName("genreName")]
		public string GenreName { get; set; }

		[JsonPropertyName("artistName")]
		public string ArtistName { get; set; }

		[JsonPropertyName("description")]
		public string ArtistDescription { get; set; }
	}
}
