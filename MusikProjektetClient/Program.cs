using System.Text.Json;
using System.Text;
using MusikProjektetClient.Models.Dtos;
using MusikProjektetClient.MenuService;
using MusikProjektetClient.Services;
using MusikProjektetClient.Helpers;

namespace MusikProjektetClient
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			HttpClient client = new HttpClient();
			IUserService userService = new UserService(client);
			IGenreService genreService = new GenreService(client);
			IArtistService artistService = new ArtistService(client);
			ISongService songService = new SongService(client, artistService, genreService);
			IRecommendationService recommendationService = new RecommendationService(client);

			SongMenu songMenu = new SongMenu(songService);
			ArtistMenu artistMenu = new ArtistMenu(artistService);
			GenreMenu genreMenu = new GenreMenu(genreService);
			RecommendationMenu recommendationMenu = new RecommendationMenu(recommendationService);
			
			MainMenu mainMenu = new MainMenu(songMenu, artistMenu, genreMenu, recommendationMenu);

			bool isLoggedIn = false;
			while (!isLoggedIn)
			{
				string Username;
				do
				{
					Console.WriteLine("Current users in system:");
					await userService.GetAllUsersAsync();
					Console.WriteLine("Login or create new user:");
					Console.Write("Username:");
					Username = Console.ReadLine();
					Console.Clear();
					
				} while (string.IsNullOrEmpty(Username));
				try
				{
					await userService.GetOrCreateUser(Username);
                    isLoggedIn = true;
				}
				catch
				{
                    Console.WriteLine("Error logging in!");
                }
			}

			while (isLoggedIn)
			{
				mainMenu.ShowMenu();
			}
		}
    }
}
