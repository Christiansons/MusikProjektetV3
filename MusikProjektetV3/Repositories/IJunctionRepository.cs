using MusikProjektetV3.Data;
using MusikProjektetV3.Models;
using MusikProjektetV3.Models.ViewModels;

namespace MusikProjektetV3.Repositories
{
	public interface IJunctionRepository
	{
		SongUser GetSongUserJunction(int userId, int songId);
		GenreUser[] GetAllGenresConnectedToPerson(int id);
		GenreUser GetSpecificGenreConnectedToUser(int userId, int genreId);

        void ConnectUserToGenre(int userId, int genreId);
        void SaveChanges();
	}

	public class JunctionRepository : IJunctionRepository
	{
		private readonly ApplicationContext _context;
		public JunctionRepository(ApplicationContext context)
		{
			_context = context;
		}

		public SongUser GetSongUserJunction(int userId, int songId)
		{
			return _context.SongUsers.Where(us => us.UserId == userId && us.SongsId == songId).FirstOrDefault();
		}

		public GenreUser[] GetAllGenresConnectedToPerson(int id)
		{
			 GenreUser[] relationTable = _context.GenreUsers.Where(x => x.UsersId == id).ToArray();
			return relationTable;
		}

		public GenreUser GetSpecificGenreConnectedToUser(int userId, int genreId)
		{
			return _context.GenreUsers.Where(x => x.GenresId == genreId && x.UsersId == userId).FirstOrDefault();
		}

		public void ConnectUserToGenre(int userId, int genreId)
		{
			_context.GenreUsers.Add(new GenreUser { GenresId = genreId, UsersId = userId });
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}
