using Microsoft.EntityFrameworkCore;
using MusikProjektetV3.Data;
using MusikProjektetV3.Models;
using MusikProjektetV3.Models.Dtos;
using MusikProjektetV3.Models.ViewModels;
using MusikProjektetV3.Repositories;

namespace MusikProjektetV3.Handlers
{
	public interface IUserHandler
	{
		IResult GetOrCreateUser(UserDto dto);
		IResult GetAllUsers();
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

		//Creates a new user, or returns an existing if username matches
		public IResult GetOrCreateUser(UserDto dto)
		{
			//If user exists, return ID
			User? user = _userRepo.GetUser(dto.Name);
			if (user != null)
			{
				return Results.Json(user.Id);
			}

			//Create new user
			try
			{
				_userRepo.AddUser(new User
				{
					Name = dto.Name
				});
				_userRepo.SaveChanges();
				user = _userRepo.GetUser(dto.Name);
				return Results.Json(user.Id);
			}
			catch
			{
				return Results.BadRequest("Error creating user");
			}
		}
		
		public IResult GetAllUsers()
		{
			AllUsersViewModel viewModel = new AllUsersViewModel
			{
				UserNames = _userRepo.GetUsers().Select(x => x.Name).ToList()
			};
			return Results.Json(viewModel);
		}
		
		//GET user/id, hämta all info från databasen med gillade låtar, artister genrer baserat på id
		//GET user/id/recommends hämtar någon eller några relevanta låtar, artister från externt API baserat på id -- https://musicbrainz.org/doc/MusicBrainz_API#Linked_entities
		//Hämtar random-låtar som har samma genre. 
		//hämta ut liknande artister baserat på genre
	}
}
