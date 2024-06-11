using MusikProjektetV3.Data;
using Microsoft.EntityFrameworkCore;
using MusikProjektetV3.Handlers;
using MusikProjektetV3.Models.Dtos;
using MusikProjektetV3.Repositories;

namespace MusikProjektetV3
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthorization();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
			builder.Services.AddScoped<ISongRepository, SongRepository>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IGenreRepository, GenreRepository>();
			builder.Services.AddScoped<IJunctionRepository, JunctionRepository>();
			builder.Services.AddScoped<IGenreHandler, GenreHandler>();
			
			//L�gg handlers h�r
			builder.Services.AddScoped<ISongHandler, SongHandler>();

			string connectionString = builder.Configuration.GetConnectionString("myConnection");
			builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();

			app.MapPost("/AddGenre", (IGenreHandler genrehandler, GenreDto dto) =>
			{
				return genrehandler.AddGenre(dto);
			});

			app.MapGet("/GetGenresConnectedToPerson/{id}", (IGenreHandler genreHandler, int id) => 
			{ 
				return genreHandler.GetAllGenresConnectedToPerson(id);
			});

			app.MapPost("ConnectUserToGenre/{userId}/{genreId}", (IGenreHandler genreHandler, int userId, int genreId) =>
			{
				return genreHandler.ConnectUserToGenre(userId, genreId);
			});

			app.MapPost("/AddSong", (ISongHandler songHandler, AddSongDto dto) =>
			{
				return songHandler.AddSong(dto);
			});

			app.MapPost("AddUser/{userId}/{songId}", () =>
			{

			});


			app.Run();
		}
	}
}
