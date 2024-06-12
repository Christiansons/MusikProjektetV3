using MusikProjektetV3.Data;
using MusikProjektetV3.Models.Dtos;
using MusikProjektetV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MusikProjektetV3.Models.ViewModels;
using MusikProjektetV3.Repositories;

namespace MusikProjektetV3.Handlers
{
    public interface IGenreHandler
    {
        IResult AddGenre(GenreDto genre);
        IResult GetAllGenresConnectedToPerson(int userId);
        IResult ConnectUserToGenre(int userID, int genreID);
    }
    public class GenreHandler : IGenreHandler
    {
        //OPTIONAL När man lägger till en genre, hämta liknande låtar från externt API, fråga om man vill lägga till

        //POST /genre lägger till en ny genre
        private readonly IArtistRepository _artistRepo;
        private readonly ISongRepository _songRepo;
        private readonly IUserRepository _userRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly IJunctionRepository _junctionRepo;

        public GenreHandler(IArtistRepository artistRepo, ISongRepository songRepo, IUserRepository userRepo, IGenreRepository genreRepo, IJunctionRepository junctionRepo)
        {
            _songRepo = songRepo;
            _artistRepo = artistRepo;
            _userRepo = userRepo;
            _genreRepo = genreRepo;
            _junctionRepo = junctionRepo;
        }
        public IResult AddGenre(GenreDto genre)
        {
            _genreRepo.AddGenre(new Genre
            {
                GenreName = genre.GenreName,
                
            });
            try
            {
                _genreRepo.SaveChanges();
                Genre genreToReturn = _genreRepo.GetGenreByName(genre.GenreName);
                return Results.Json(genreToReturn);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }

        public IResult ConnectUserToGenre(int userID, int genreID)
        {
            if(_junctionRepo.GetSpecificGenreConnectedToUser(userID, genreID) != null) 
            {
                return Results.BadRequest("Already exists");
            }
            try
            {
                _junctionRepo.ConnectUserToGenre(userID, genreID);
                _junctionRepo.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }

        //GET /genre Hämta alla genrer i databasen
        public IResult GetAllGenresConnectedToPerson(int userId)
        {

            var genres = _genreRepo.GetAllGenres();
            var relationTables = _junctionRepo.GetAllGenresConnectedToPerson(userId);
            var GenreList = new GetAllGenresConnectedToPersonViewModel();

            foreach (var relationTable in relationTables)
            {
                foreach (var genre in genres)
                {
                    if(relationTable.GenresId == genre.Id)
                    {
                        GenreList.GenreNames.Add(genre.GenreName);
                    }
                }
            }


            if (GenreList == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Json(GenreList);
            }
        }

    }
}
