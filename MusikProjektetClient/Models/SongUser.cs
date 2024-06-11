using System.ComponentModel.DataAnnotations;

namespace MusikProjektetClient.Models
{
	public class SongUser
	{
		[Key]
		public int Id { get; set; }
		public virtual int SongsId { get; set; }
		public virtual int UserId { get; set; }
	}
}
