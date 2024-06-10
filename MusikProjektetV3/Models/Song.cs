using MusikProjektetV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusikProjektetV3.Models
{
	public class Song
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("songTitle")]
		public string SongTitle { get; set; }

		public int GenreId { get; set; }
		[JsonPropertyName("genre")]
		public virtual Genre Genre { get; set; }

		public int ArtistId { get; set; }
		[JsonPropertyName("artist")]
		public virtual Artist Artist { get; set; }

		public virtual ICollection<User>? Users { get; set; } = new List<User>();
	}
}
