using MusikProjektetClient.Models;
using MusikProjektetClient.Models.Dtos;
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
	}
}
