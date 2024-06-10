using System.ComponentModel.DataAnnotations;

namespace MusikProjektetV3.Models
{
	public class GenreUser
	{
		[Key]
		public int Id { get; set; }
		public virtual int GenresId { get; set; }
		public virtual int UsersId { get; set; }
	}
}
