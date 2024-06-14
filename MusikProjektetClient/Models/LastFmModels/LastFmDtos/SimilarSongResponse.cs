using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusikProjektetClient.Models.LastFmModels.LastFmDtos
{
	internal class SimilarSongResponse
	{
		[JsonPropertyName("similartracks")]
		public SimilarSongsDto SimilarSongs { get; set; } = new SimilarSongsDto();
	}
}
