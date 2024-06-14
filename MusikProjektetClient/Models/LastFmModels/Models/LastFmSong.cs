using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusikProjektetClient.Models.LastFmModels.Models
{
	internal class LastFmSong
	{
		public string name {  get; set; }
		public LastFmArtist artist { get; set; }
	}
}
