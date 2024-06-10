using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusikProjektetV3.Models
{
	public class Genre
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("genreName")]
		public string GenreName { get; set; }

		public virtual ICollection<User>? Users { get; set; } = new List<User>();
	}
}
