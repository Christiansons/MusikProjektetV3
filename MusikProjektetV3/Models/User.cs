using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusikProjektetV3.Models
{
	public class User
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("Name")]
		public string Name { get; set; }

		[JsonPropertyName("artists")]
		public virtual ICollection<Artist>? Artists { get; set; } = new List<Artist>();

		[JsonPropertyName("songs")]
		public virtual ICollection<Song>? Songs { get; set; } = new List<Song>();

		[JsonPropertyName("genres")]
		public virtual ICollection<Genre>? Genres { get; set; } = new List<Genre>();
	}
}
