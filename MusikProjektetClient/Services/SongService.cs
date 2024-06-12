using MusikProjektetClient.Models;
using MusikProjektetClient.Models.Dtos;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MusikProjektetClient.Services
{
	public interface ISongService
	{
		Task AddSong();
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

            Console.WriteLine("Add a new song!");
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

		internal static void AddSongToUser()
		{
			throw new NotImplementedException();
		}

		internal static void ShowAllSongsAddedToUser()
		{
			throw new NotImplementedException();
		}
	}
}
