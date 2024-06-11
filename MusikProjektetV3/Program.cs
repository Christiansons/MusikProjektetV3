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

			var summaries = new[]
			{
				"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
			};

			app.MapGet("/weatherforecast", (HttpContext httpContext) =>
			{
				var forecast = Enumerable.Range(1, 5).Select(index =>
					new WeatherForecast
					{
						Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
						TemperatureC = Random.Shared.Next(-20, 55),
						Summary = summaries[Random.Shared.Next(summaries.Length)]
					})
					.ToArray();
				return forecast;
			})
			.WithName("GetWeatherForecast")
			.WithOpenApi();

			app.MapPost("ConnectUserToSong/{userId}/{songId}", () =>
			{

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
