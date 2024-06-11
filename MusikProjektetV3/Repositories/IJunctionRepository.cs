using MusikProjektetV3.Data;
using MusikProjektetV3.Models;

namespace MusikProjektetV3.Repositories
{
	public interface IJunctionRepository
	{
		SongUser GetSongUserJunction(int userId, int songId);
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

		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}
