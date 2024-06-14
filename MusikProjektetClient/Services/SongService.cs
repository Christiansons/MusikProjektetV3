using MusikProjektetClient.Models;
using MusikProjektetClient.Models.Dtos;
using MusikProjektetClient.Models.ViewModels;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MusikProjektetClient.Services
{
	public interface ISongService
	{
		Task AddSong();
		Task AddSongToUser();
		Task ShowAllSongsAddedToUser();
	}

	public class SongService : ISongService
	{
		private readonly HttpClient _httpClient;
		private readonly IGenreService _genreService;
		private readonly IArtistService _artistService;
		string baseAddress = "http://localhost:5098";
		public SongService(HttpClient httpClient, IArtistService artistService, IGenreService genreService)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri(baseAddress);
			_genreService = genreService;
			_artistService = artistService;
		}

		public async Task AddSong()
		{
			Console.Clear();
            Console.WriteLine("Add a new song to the database!");
			Console.WriteLine("Whats the genre?");
			Console.Write("Genre: ");
			string genreName = Console.ReadLine();
			Genre genre = await _genreService.AddGenre(genreName);

			Console.WriteLine("Whats the artist?");
			Console.Write("Artist: ");
			string artistName = Console.ReadLine();
			Console.Write("Write a short description of the artist: ");
			string artistDescription = Console.ReadLine();
			Artist artist = await _artistService.AddArtist(artistDescription, artistName);

            Console.WriteLine("Whats the name of the song?");
			Console.Write("Title: ");
			string songTite = Console.ReadLine();

			AddSongDto songDto = new AddSongDto()
			{
				ArtistDescription = artist.Description,
				ArtistName = artist.ArtistName,
				GenreName = genre.GenreName,
				SongTitle = songTite
			};
			
			string json = JsonSerializer.Serialize(songDto);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _httpClient.PostAsync("/GetOrCreateUser", content);
			response.EnsureSuccessStatusCode();
		}

		public async Task AddSongToUser()
		{
			Console.Clear();
			Console.WriteLine("Add a song to your favorites (make sure its added to the database first)");
			Console.WriteLine("Whats the song title?");
			Console.Write("title: ");
			string songTitle = Console.ReadLine();

			SongConnectionDto dto = new SongConnectionDto
			{
				songName = songTitle,
				userId = GlobalLoginVariable.UserId
			};

			string json = JsonSerializer.Serialize(dto);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _httpClient.PostAsync("/ConnectUserToSong", content);

            Console.WriteLine(response.Content.ToString());
			Console.WriteLine("Press any key to go back to main menu");
			Console.ReadKey();
        }


		public async Task ShowAllSongsAddedToUser()
		{
			Console.Clear();
			HttpResponseMessage response = await _httpClient.GetAsync($"/GetAllSongsConnectedToUser/{GlobalLoginVariable.UserId}");
			if (response.IsSuccessStatusCode)
			{
				var songList = await JsonSerializer.DeserializeAsync<GetAllSongsConnectedToUserViewModel>(await response.Content.ReadAsStreamAsync());
				if (songList != null && songList.SongNames.Count > 0)
				{
					Console.WriteLine("Songs added to user:");
					foreach (var songName in songList.SongNames)
					{
						Console.WriteLine(songName);
					}
				}
				else
				{
					Console.WriteLine("No songs added to this user.");
				}
			}
			else
			{
				Console.WriteLine($"Failed to retrieve songs. Status code: {response.StatusCode}");
			}
		}
	}
}
