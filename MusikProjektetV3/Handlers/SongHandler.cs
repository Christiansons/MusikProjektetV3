using MusikProjektetV3.Models;
using System.Net;
using MusikProjektetV3.Models.Dtos;
using MusikProjektetV3.Models.ViewModels;
using System.Text.Json;
using MusikProjektetV3.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace MusikProjektetV3.Handlers
{
	public interface ISongHandler
	{
		IResult AddSong(AddSongDto addSongDto);
		IResult AddSongToUser(SongConnectionDto dto);
		IResult GetAllSongsConnectedToUser(int userId);
	}

	public class SongHandler : ISongHandler
	{
		private readonly IArtistRepository _artistRepo;
		private readonly ISongRepository _songRepo;
		private readonly IUserRepository _userRepo;
		private readonly IGenreRepository _genreRepo;
		private readonly IJunctionRepository _junctionRepo;

        public SongHandler(IArtistRepository artistRepo, ISongRepository songRepo, IUserRepository userRepo, IGenreRepository genreRepo, IJunctionRepository junctionRepo)
		{
			_songRepo = songRepo;
			_artistRepo = artistRepo;
			_userRepo = userRepo;
			_genreRepo = genreRepo;
			_junctionRepo = junctionRepo;
		}

		public IResult GetArtist(string name)
		{
			Artist artist = _artistRepo.GetArtistByName(name);

			return Results.Ok(artist);
		}

		//POST /song Adds a new song to user, must have an artist and genre
		public IResult AddSong(AddSongDto addSongDto)
        {
			//If artist already exists, use that one
			Artist artist = _artistRepo.GetArtistByName(addSongDto.ArtistName);

			//Create new artist if nonexistant
			if (artist == null)
			{
				artist = new Artist
				{
					ArtistName = addSongDto.ArtistName,
					Description = addSongDto.ArtistDescription,
				};

				_artistRepo.AddArtist(artist);
				_artistRepo.SaveChanges();
			}

			//If genre already exists, use that one
			Genre genre = _genreRepo.GetGenreByName(addSongDto.GenreName);

			//Create new genre if nonexistant
			if (genre == null)
			{
				genre = new Genre
				{
					GenreName = addSongDto.GenreName,
				};
				_genreRepo.AddGenre(genre);
				_genreRepo.SaveChanges();
			}

			//Create new song
			Song song = new Song
			{
				ArtistId = artist.Id, 
				GenreId = genre.Id,
				SongTitle = addSongDto.SongTitle
			};
			_songRepo.AddSong(song);
			_songRepo.SaveChanges();

			//Check if song already connected to user
			return Results.Created();
		}

		public IResult AddSongToUser(SongConnectionDto connectionDto)
		{
			
			int? songIdCheck = _songRepo.GetSongId(connectionDto.songName);
			int songId = 0;
			
			//See if the song exists in database
			if (songIdCheck.HasValue)
			{
				songId = songIdCheck.Value;
			}
			else
			{
				return Results.NotFound("No song with that title added!");
			}

			//If the connection already exists
			if (_junctionRepo.GetSpecificSongConnectedToUser(connectionDto.userId, songId) != null)
			{
				return Results.BadRequest("Song already added to your profile");
			}

			//Add the user to the song
			try
			{
				_junctionRepo.ConnectUserToSong(connectionDto.userId, songId);
				_junctionRepo.SaveChanges();
				return Results.Ok("Song added to your profile");
			}
			catch (Exception ex)
			{
				return Results.BadRequest("error adding song: " + ex);
			}
		}

		public IResult GetAllSongsConnectedToUser(int userId)
		{
			//See if there are any songs in the database
			var songs = _songRepo.GetAllSongs();
			if (songs == null)
			{
				return Results.NotFound("No songs found in the database.");
			}

			//Che
			var relationTables = _junctionRepo.GetAllSongsConnectedToUser(userId);
			if (relationTables == null)
			{
				return Results.NotFound("No relations found for the user.");
			}

			var songList = new GetAllSongsConnectedToUserViewModel();

			foreach (var relationTable in relationTables)
			{
				foreach (var song in songs)
				{
					if (relationTable.SongsId == song.Id)
					{
						songList.SongNames.Add(song.SongTitle);
					}
				}
			}

			if (songList.SongNames.IsNullOrEmpty())
			{
				return Results.NotFound("No songs added");
			}
			else
			{
				return Results.Json(songList);
			}
		}
	}
}

