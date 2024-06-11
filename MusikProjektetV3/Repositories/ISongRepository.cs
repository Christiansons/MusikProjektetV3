using Microsoft.EntityFrameworkCore;
using MusikProjektetV3.Data;
using MusikProjektetV3.Models;
using MusikProjektetV3.Models.Dtos;
using MusikProjektetV3.Models.ViewModels;

namespace MusikProjektetV3.Repositories
{
	public interface ISongRepository
	{
		void AddSong(Song song);
		ListSongViewModel[] GetAllSongConnectedToUser(int id);
		void ConnectUserToSong(int userId, int songId);
		//Artist GetOrCreateArtist(ArtistDto dto);
		Genre GetOrCreateGenre(GenreDto dto);
		void SaveChanges();
	}

	public class SongRepository : ISongRepository
	{
		private readonly ApplicationContext _context;
		
		public SongRepository(ApplicationContext context)
        {
            _context = context;
        }

		public void AddSong(Song song)
		{
			_context.Songs.Add(song);
		}

		public void ConnectUserToSong(int userId, int songId)
		{
			User? user = _context.Users
				.Where(u => u.Id == userId)
				.Include(s => s.Songs)
				.SingleOrDefault();

			Song? song = _context.Songs
				.Where(s => s.Id == songId)
				.SingleOrDefault();

			user.Songs.Add(song);
			_context.SaveChanges();
		}

		public ListSongViewModel[] GetAllSongConnectedToUser(int id)
		{
			throw new NotImplementedException();
		}

		public Genre GetOrCreateGenre(GenreDto dto)
		{
			throw new NotImplementedException();
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}
