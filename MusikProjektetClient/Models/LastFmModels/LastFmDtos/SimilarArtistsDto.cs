using MusikProjektetClient.Models.LastFmModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusikProjektetClient.Models.LastFmModels.LastFmDtos
{
	internal class SimilarArtistsDto
	{
		[JsonPropertyName("artist")]
		public List<LastFmArtist> Artists { get; set; } = new List<LastFmArtist>();
	}
}
