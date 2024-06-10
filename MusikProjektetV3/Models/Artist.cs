using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusikProjektetV3.Models
{
	public class Artist
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("artistName")]
		public string ArtistName { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		
		public virtual ICollection<User>? Users { get; set; } = new List<User>();
	}
}
