using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusikProjektetClient.Models.LastFmModels.Models
{
	internal class LastFmGenre
	{
		[JsonPropertyName("name")]
		public string name {  get; set; }
	}
}
