using System.ComponentModel.DataAnnotations;

namespace MusikProjektetClient.Models
{
	public class ArtistUser
	{
		[Key]
		public int Id { get; set; }
		public virtual int ArtistsId { get; set; }
		public virtual int UsersId { get; set; }
	}
}
