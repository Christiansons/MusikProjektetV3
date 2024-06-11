using Microsoft.EntityFrameworkCore;
using MusikProjektetV3.Data;
using MusikProjektetV3.Models;
using MusikProjektetV3.Models.Dtos;
using MusikProjektetV3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusikProjektetV3.Handlers
{
	public interface IUserHandler
	{
		IResult AddUser(UserDto dto);
	}
	public class UserHandler : IUserHandler
	{
		private readonly IArtistRepository _artistRepo;
		private readonly ISongRepository _songRepo;
		private readonly IUserRepository _userRepo;
		private readonly IGenreRepository _genreRepo;
		private readonly IJunctionRepository _junctionRepo;

		public UserHandler(IArtistRepository artistRepo, ISongRepository songRepo, IUserRepository userRepo, IGenreRepository genreRepo, IJunctionRepository junctionRepo)
		{
			_songRepo = songRepo;
			_artistRepo = artistRepo;
			_userRepo = userRepo;
			_genreRepo = genreRepo;
			_junctionRepo = junctionRepo;
		}

		public IResult AddUser(UserDto dto)
		{
			try
			{
				_userRepo.AddUser(new User
				{
					Name = dto.Name
				});
				_userRepo.SaveChanges();
				return Results.Created();
			}
			catch
			{
				return Results.BadRequest("Error creating user");
			}
		}
		//GET user/id, hämta all info från databasen med gillade låtar, artister genrer baserat på id

		//GET user/id/recommends hämtar någon eller några relevanta låtar, artister från externt API baserat på id -- https://musicbrainz.org/doc/MusicBrainz_API#Linked_entities
		//Hämtar random-låtar som har samma genre. 
		//hämta ut liknande artister baserat på genre


		//POST /user Lägger till en ny användare 

		//Min
		//POST /user/id/song Kopplar en användare till en befintlig sång 
		public static IResult ConnectUserToSong(ApplicationContext context, int userId, int songId)
		{
			User? user = context.Users
				.Where(u => u.Id == userId)
				.Include(s => s.Songs)
				.SingleOrDefault();

			Song? song = context.Songs
				.Where(s => s.Id == songId)
				.SingleOrDefault();

			if (user == null || song == null)
			{
				return Results.NotFound();
			}
			try
			{
				user.Songs.Add(song);
				context.SaveChanges();
				return Results.StatusCode((int)HttpStatusCode.Created);
			}
			catch (Exception ex)
			{
				return Results.BadRequest(ex);
			}
		}

		//POST /user/id/artist Kopplar en användare till en befintlig artist

		//POST /user/id/genre Kopplar en användare till en befintlig genre
	}
}
