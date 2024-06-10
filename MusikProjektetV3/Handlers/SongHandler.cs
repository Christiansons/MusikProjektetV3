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

namespace MusikProjektetV3.Handlers
{

	public class SongHandler
	{
		private readonly ApplicationContext context;

        public SongHandler(ApplicationContext Context)
		{
			context = Context;
		}

		//OPTIONAL När man lägger till en låt, hämta ldsiknande låtar från externt API, fråga om man vill lägga till

		public static IResult GetArtist(ApplicationContext Context)
		{
			Artist artist = Context.Artists.FirstOrDefault();

			return Results.Ok(artist);
		}

		//OPTIONAL När man lägger till en låt, hämta ldsiknande låtar från externt API, fråga om man vill lägga till

		//POST /song lägger till en ny låt, den ska ha en genre och artist
		public static IResult AddSong(ApplicationContext _context, AddSongDto addSongDto)
        {
			User user = _context.Users.Where(u => u.Id == 1).First();

			//IF artist already added, use that one
			Artist artist = _context.Artists.Where(a => a.ArtistName == addSongDto.ArtistName).FirstOrDefault();

			//If not exist create a new one
			if (artist == null)
			{
				artist = new Artist
				{
					ArtistName = addSongDto.ArtistName,
					Description = addSongDto.ArtistDescription,
				};
				_context.Artists.Add(artist);
			}


			//IF genre already added, use that one
			Genre genre = _context.Genres.Where(g => g.GenreName == addSongDto.GenreName).FirstOrDefault();
			if (genre == null)
			{
				genre = new Genre
				{
					GenreName = addSongDto.GenreName,
				};
				_context.Genres.Add(genre);
			}


			//Create new song
			Song song = new Song
			{
				ArtistId = 3, 
				GenreId = 3,
				SongTitle = addSongDto.SongTitle
			};
			
			_context.Songs.Add(song);
			_context.SaveChanges();

			//Check if song already connected to user
			SongUser songUser = _context.SongUsers.Where(us => us.UserId == user.Id && us.SongsId == song.Id).FirstOrDefault();
		
			//Add new song to user
			if( songUser == null )
			{
				_context.SongUsers.Add(new SongUser
				{
					SongsId = song.Id,
					UserId = user.Id,
				});
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
		//GET /song hämtar alla sånger i databasen
		//public static IResult GetAllSongConnectedToUser(int id)
		//{
		//	User? user = context.Users
		//		.Where(u => u.Id == id)
		//		.Include(u => u.Songs)
		//		.FirstOrDefault();

		//	ListSongViewModel[]? UserSongList = user.Songs
		//		.Select(s => new ListSongViewModel
		//		{
		//			SongTitle = s.SongTitle
		//		}).ToArray();

		//	if (UserSongList == null)
		//	{
		//		return Results.NotFound();
		//	}
		//	else
		//	{
		//		return Results.Json(UserSongList);
		//	}
		//}

	}
	}

