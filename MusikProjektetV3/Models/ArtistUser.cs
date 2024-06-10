using System.ComponentModel.DataAnnotations;

namespace MusikProjektetV3.Models
{
	public class ArtistUser
	{
		[Key]
		public int Id { get; set; }
		public virtual int ArtistsId { get; set; }
		public virtual int UsersId { get; set; }
	}
}
