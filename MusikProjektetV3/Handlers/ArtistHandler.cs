using MusikProjektetV3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MusikProjektetV3.Models;
using MusikProjektetV3.Models.ViewModels;
using MusikProjektetV3.Models.Dtos;

namespace MusikProjektetV3.Handlers
{
	public interface IArtistHandler
	{
		IResult AddArtist(ArtistDto dto);
		IResult GetAllArtistsConnectedToPerson(int userId);
		IResult ConnectUserToArtist(int userId, int artistId);
	}

	public class ArtistHandler : IArtistHandler
	{
		private readonly ISongRepository _songRepo;
		private readonly IUserRepository _userRepo;
		private readonly IArtistRepository _artistRepo;
		private readonly IJunctionRepository _junctionRepo;

		public ArtistHandler(ISongRepository songRepo, IUserRepository userRepo, IArtistRepository artistRepo, IJunctionRepository junctionRepo)
		{
			_songRepo = songRepo;
			_userRepo = userRepo;
			_artistRepo = artistRepo;
			_junctionRepo = junctionRepo;
		}

		public IResult AddArtist(ArtistDto dto)
		{
			_artistRepo.AddArtist(new Artist
			{
				Description = dto.artistDescription,
				ArtistName = dto.artistName
			});
			try
			{
				_artistRepo.SaveChanges();
				Artist artistToReturn = _artistRepo.GetArtistByName(dto.artistName);
				return Results.Json(artistToReturn);
			}
			catch (Exception ex)
			{
				return Results.BadRequest(ex);
			}
		}

		public IResult ConnectUserToArtist(int userId, int artistId)
		{
			if (_junctionRepo.GetSpecificArtistConnectedToUser(userId, artistId) != null)
			{
				return Results.BadRequest("Already exists");
			}
			try
			{
				_junctionRepo.ConnectUserToArtist(userId, artistId);
				_junctionRepo.SaveChanges();
				return Results.StatusCode((int)HttpStatusCode.Created);
			}
			catch (Exception ex)
			{
				return Results.BadRequest(ex);
			}
		}

		public IResult GetAllArtistsConnectedToPerson(int userId)
		{
			var artists = _artistRepo.GetAllArtists();
			var relationTables = _junctionRepo.GetAllArtistsConnectedToPerson(userId);
			var artistList = new GetAllArtistsConnectedToPersonViewModel();

			foreach (var relationTable in relationTables)
			{
				foreach (var artist in artists)
				{
					if (relationTable.ArtistsId == artist.Id)
					{
						artistList.ArtistNames.Add(artist.ArtistName);
					}
				}
			}

			if (artistList == null)
			{
				return Results.NotFound();
			}
			else
			{
				return Results.Json(artistList);
			}
		}
	}
}
