using MusikProjektetV3.Data;
using MusikProjektetV3.Models;

namespace MusikProjektetV3.Repositories
{
	public interface IGenreRepository
	{
		Genre GetGenreByName(string genreName);
		void AddGenre(Genre genre);
		void SaveChanges();
	}

	public class GenreRepository : IGenreRepository
	{
		private readonly ApplicationContext _context;
		public GenreRepository(ApplicationContext context)
		{
			_context = context;
		}

		public Genre GetGenreByName(string genreName)
		{
			return _context.Genres.Where(g => g.GenreName == genreName).First();
		}

		void AddGenre(Genre genre)
		{
			_context.Genres.Add(genre);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		void IGenreRepository.AddGenre(Genre genre)
		{
			throw new NotImplementedException();
		}
	}
}
