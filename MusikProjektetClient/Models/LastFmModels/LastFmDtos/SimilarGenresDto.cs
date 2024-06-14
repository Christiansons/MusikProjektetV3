using MusikProjektetClient.Models.LastFmModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusikProjektetClient.Models.LastFmModels.LastFmDtos
{
	internal class SimilarGenresDto
	{
		[JsonPropertyName("tag")]
		public List<LastFmGenre> Tags { get; set; } = new List<LastFmGenre>();
	}
}
