using MusikProjektetClient.Models;
using MusikProjektetClient.Models.Dtos;
using MusikProjektetClient.Models.ViewModels;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

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
			//Get or create a new genre
			Genre genre = await _genreService.AddGenre();
			
			//Get or create a new artist
			Artist artist = await _artistService.AddArtist();

			//Create a new song
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

			HttpResponseMessage response = await _httpClient.PostAsync("/AddSong", content);
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

            Console.WriteLine(await response.Content.ReadAsStringAsync());
			Console.ReadKey();
        }


		public async Task ShowAllSongsAddedToUser()
		{
			Console.Clear();
			HttpResponseMessage response = await _httpClient.GetAsync($"/GetSongsConnectedToUser/{GlobalLoginVariable.UserId}");
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine($"Failed to retrieve songs. Status code: {response.StatusCode}");
				return;
			}
			var jsonResponse = await response.Content.ReadAsStringAsync();
			var songList = JsonSerializer.Deserialize<GetAllSongsConnectedToUserViewModel>(jsonResponse);
			if (songList.SongNames.IsNullOrEmpty())
			{
				Console.WriteLine("No songs added to this user.");
				return;
			}
			Console.WriteLine("Songs added to user:");
			foreach (var songName in songList.SongNames)
			{
				Console.WriteLine(songName);
			}
		}
	}
}
