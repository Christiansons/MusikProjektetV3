using System.ComponentModel.DataAnnotations;

namespace MusikProjektetV3.Models
{
	public class SongUser
	{
		[Key]
		public int Id { get; set; }
		public virtual int SongsId { get; set; }
		public virtual int UserId { get; set; }
	}
}
