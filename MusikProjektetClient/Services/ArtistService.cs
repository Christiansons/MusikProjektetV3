using MusikProjektetClient.Models;
using MusikProjektetClient.Models.Dtos;
using MusikProjektetClient.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MusikProjektetClient.Services
{
	public interface IArtistService
	{
		Task<Artist> AddArtist();
		Task AddArtistToUser();
		Task ShowAllArtistsAddedToUser();
    }
	public class ArtistService : IArtistService
	{
		string baseAddress = "http://localhost:5098";
		private readonly HttpClient _httpClient;

        public ArtistService(HttpClient httpClient)
        {
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri(baseAddress);
		}

		public async Task<Artist> AddArtist()
		{
			Console.WriteLine("Whats the artist?");
			Console.Write("Artist: ");
			string artistName = Console.ReadLine();
			Console.Write("Write a short description of the artist: ");
			string artistDescription = Console.ReadLine();
			ArtistDto artistDto = new ArtistDto()
			{
				artistDescription = artistDescription,
				artistName = artistName
			};

			string json = JsonSerializer.Serialize(artistDto, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _httpClient.PostAsync("/AddArtist", content);

			response.EnsureSuccessStatusCode();

			Artist artist = await JsonSerializer.DeserializeAsync<Artist>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return artist;
		}

		public async Task AddArtistToUser()
		{
            Console.Clear();
            Console.WriteLine("Add a Artist to User");
            Console.WriteLine("Whos the Artist?");
            Console.Write("Artist: ");
            string artistname = Console.ReadLine();
            ArtistDto artistDto = new ArtistDto()
            {
                artistName = artistname
            };



            string json = JsonSerializer.Serialize(artistDto, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            StringContent userID = new StringContent(GlobalLoginVariable.UserId.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("/AddArtist", content);
            Artist artist = await JsonSerializer.DeserializeAsync<Artist>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			ArtistConnectionDto connectcontent = new ArtistConnectionDto()
            {
                UsersId = GlobalLoginVariable.UserId,
				ArtistsId = artist.Id
            };

            string connectcontentseri = JsonSerializer.Serialize<ArtistConnectionDto>(connectcontent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            StringContent connectioncontent = new StringContent(connectcontentseri, Encoding.UTF8, "application/json");

            HttpResponseMessage connectResponse = await _httpClient.PostAsync($"/ConnectUserToArtist", connectioncontent);

            response.EnsureSuccessStatusCode();


			await Console.Out.WriteLineAsync("Artist added");
        }

		public async Task ShowAllArtistsAddedToUser()
		{
            Console.Clear();
            HttpResponseMessage response = await _httpClient.GetAsync($"/GetArtistsConnectedToUser/{GlobalLoginVariable.UserId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to retrieve Genres. Status code: {response.StatusCode}");
                return;
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var artistList = JsonSerializer.Deserialize<GetAllArtistsConnectedToPersonViewModel>(jsonResponse);
            if (artistList.ArtistNames.Count == 0)
            {
                Console.WriteLine("No Artists added to this user.");
                return;
            }
            Console.WriteLine("Artists added to user:");
            foreach (var artistName in artistList.ArtistNames)
            {
                Console.WriteLine(artistName);
            }
        }
    }
}
