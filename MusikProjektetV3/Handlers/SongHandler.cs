using MusikProjektetV3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusikProjektetV3.Models;
using System.Net;
using MusikProjektetV3.Models.Dtos;
using MusikProjektetV3.Models.ViewModels;
using System.Text.Json;
using MusikProjektetV3.Repositories;

namespace MusikProjektetV3.Handlers
{
	public interface ISongHandler
	{
		IResult GetArtist(string name);
		IResult AddSong(AddSongDto addSongDto);
		IResult AddSongToUser(int userId, Song song);
		// Add other methods as needed, e.g., fetching similar songs
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
			SongUser songUser = _juc

			//Add new song to user
			if ( songUser == null )
			{
				_context.SongUsers.Add(new SongUser
				{
					SongsId = song.Id,
					UserId = user.Id,
				});
			} else
			{
				return Results.BadRequest("User is already added!")
			}
			
			try
			{
				_context.SaveChanges();
				return Results.StatusCode((int)HttpStatusCode.Created);
			}
			catch (Exception ex)
			{
				return Results.BadRequest(ex);
			}
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

	}
	}

