using MusikProjektetV3.Data;
using MusikProjektetV3.Models;
using MusikProjektetV3.Models.ViewModels;

namespace MusikProjektetV3.Repositories
{
	public interface IJunctionRepository
	{
		GenreUser[] GetAllGenresConnectedToPerson(int id);
		GenreUser GetSpecificGenreConnectedToUser(int userId, int genreId);

		SongUser[] GetAllSongsConnectedToUser(int userId);
		SongUser GetSpecificSongConnectedToUser(int userId, int songId);

		ArtistUser[] GetAllArtistsConnectedToPerson(int userId);
		ArtistUser GetSpecificArtistConnectedToUser(int userId, int artistId);
		
		void ConnectUserToArtist(int userId, int artistId);
		void ConnectUserToSong(int userId, int songId);
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

		public GenreUser[] GetAllGenresConnectedToPerson(int id)
		{
			GenreUser[] relationTable = _context.GenreUsers.Where(x => x.UsersId == id).ToArray();
			return relationTable;
		}

		//Gets one genre connected to a user, based on userID and GenreID
		public GenreUser GetSpecificGenreConnectedToUser(int userId, int genreId)
		{
			return _context.GenreUsers.Where(x => x.GenresId == genreId && x.UsersId == userId).FirstOrDefault();
		}

		//Gets one song connected to a user, based on userID and GenreID
		public SongUser GetSpecificSongConnectedToUser(int userId, int songId)
		{
			return _context.SongUsers.Where(x => x.SongsId == songId && x.UserId == userId).FirstOrDefault();
		}

		//Connects a user to a genre
		public void ConnectUserToGenre(int userId, int genreId)
		{
			_context.GenreUsers.Add(new GenreUser { GenresId = genreId, UsersId = userId });
		}

		//Connects a user to a song
		public void ConnectUserToSong(int userId, int songId)
		{
			_context.SongUsers.Add(new SongUser { SongsId = songId, UserId = userId });
		}

		public ArtistUser[] GetAllArtistsConnectedToPerson(int userId)
		{
			return _context.ArtistUsers.Where(x => x.UsersId == userId).ToArray();
		}

		public ArtistUser GetSpecificArtistConnectedToUser(int userId, int artistId)
		{
			return _context.ArtistUsers.FirstOrDefault(x => x.UsersId == userId && x.ArtistsId == artistId);
		}

		public void ConnectUserToArtist(int userId, int artistId)
		{
			_context.ArtistUsers.Add(new ArtistUser { UsersId = userId, ArtistsId = artistId });
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public SongUser[] GetAllSongsConnectedToUser(int userId)
		{
			return _context.SongUsers.Where(x => x.UserId == userId).ToArray();
		}
	}
}
