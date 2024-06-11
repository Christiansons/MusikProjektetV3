using MusikProjektetV3.Data;
using MusikProjektetV3.Models;
namespace MusikProjektetV3.Repositories
{
	public interface IArtistRepository
	{
		Artist GetArtistByName(string name);
		void AddArtist(Artist artist);
		void SaveChanges();
	}

	public class ArtistRepository : IArtistRepository
	{
		private readonly ApplicationContext _context;
        public ArtistRepository(ApplicationContext context)
        {
            _context = context;
        }

		public void AddArtist(Artist artist)
		{
			_context.Artists.Add(artist);
		}

		public Artist GetArtistByName(string artistName)
		{
			return _context.Artists.Where(a => a.ArtistName == artistName).First();
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}
