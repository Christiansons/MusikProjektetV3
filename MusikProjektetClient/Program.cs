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
			HttpClient lastFmClient = new HttpClient();

			IUserService userService = new UserService(client);
			IGenreService genreService = new GenreService(client);
			IArtistService artistService = new ArtistService(client);
			ISongService songService = new SongService(client, artistService, genreService);
			IRecommendationService recommendationService = new RecommendationService(lastFmClient);

			MenuHelper menuHelper = new MenuHelper();
			SongMenu songMenu = new SongMenu(songService, menuHelper);
			ArtistMenu artistMenu = new ArtistMenu(artistService, menuHelper);
			GenreMenu genreMenu = new GenreMenu(genreService, menuHelper);
			RecommendationMenu recommendationMenu = new RecommendationMenu(recommendationService, menuHelper);
			
			
			MainMenu mainMenu = new MainMenu(songMenu, artistMenu, genreMenu, recommendationMenu, menuHelper);

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
				await mainMenu.ShowMenu();
			}
		}
    }
}
