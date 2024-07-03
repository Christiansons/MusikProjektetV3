using MusikProjektetClient.Models;
using MusikProjektetClient.Models.Dtos;
using MusikProjektetClient.Models.ViewModels;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using MusikProjektetClient.Helpers;

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
		private readonly ISongHttpHelper _httpHelper;
		private readonly IGenreService _genreService;
		private readonly IArtistService _artistService;
		public SongService(HttpClient httpClient, IArtistService artistService, IGenreService genreService, ISongHttpHelper songHttpHelper)
		{
			_httpHelper = songHttpHelper;
			_genreService = genreService;
			_artistService = artistService;
		}

		public async Task AddSong()
		{
			try
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

				HttpResponseMessage response = await _httpHelper.AddSong(songDto);

				if (!response.IsSuccessStatusCode)
				{
					string errorMessage = $"Failed to add song, errorcode: {(int)response.StatusCode}, {response.ReasonPhrase}";

					string responseContent = await response.Content.ReadAsStringAsync();

					if (!string.IsNullOrEmpty(responseContent))
					{
						errorMessage += $" {responseContent}";
					}
                    await Console.Out.WriteLineAsync(errorMessage);
                } 
				else
				{
                    await Console.Out.WriteLineAsync("Successfully added song");
                }
			} catch (Exception ex)
			{
                await Console.Out.WriteLineAsync($"Error: {ex.Message}");
            }
			
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
