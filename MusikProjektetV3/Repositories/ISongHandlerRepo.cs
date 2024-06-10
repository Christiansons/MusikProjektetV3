using Microsoft.EntityFrameworkCore;
using MusikProjektetV3.Data;
using MusikProjektetV3.Models;
using MusikProjektetV3.Models.Dtos;
using MusikProjektetV3.Models.ViewModels;

namespace MusikProjektetV3.Repositories
{
	public interface ISongHandlerRepo
	{
		void AddSong(AddSongDto song, int artistId, int genreId);
		ListSongViewModel[] GetAllSongConnectedToUser(int id);
		void ConnectUserToSong(int userId, int songId);
		//Artist GetOrCreateArtist(ArtistDto dto);
		Genre GetOrCreateGenre(GenreDto dto);
	}

	public class dbSongHandlerRepo : ISongHandlerRepo
	{
		private readonly ApplicationContext _context;
		
		public dbSongHandlerRepo(ApplicationContext context)
        {
            _context = context;
        }

		public void AddSong(AddSongDto song, int artistId, int genreId)
		{
			Artist? artist = _context.Artists
				.Where(a => a.Id == artistId)
				.FirstOrDefault();

			Genre? genre = _context.Genres
				.Where(g => g.Id == genreId)
				.FirstOrDefault();

			_context.Songs.Add(new Song
			{
				SongTitle = song.SongTitle,
				Artist = artist,
				Genre = genre
			});

			_context.SaveChanges();
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
			User? user = _context.Users
				.Where(u => u.Id == id)
				.Include(u => u.Songs)
				.FirstOrDefault();

			if (user == null)
			{
				return new ListSongViewModel[] { };
			}


			return user.Songs
				.Select(s => new ListSongViewModel
				{
					SongTitle = s.SongTitle
				}).ToArray();

		}

		//public Artist GetOrCreateArtist(ArtistDto dto)
		//{
		//	throw new NotImplementedException();
		//}

		public Genre GetOrCreateGenre(GenreDto dto)
		{
			throw new NotImplementedException();
		}
	}
}
