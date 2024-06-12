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
	public interface IGenreService
	{
		Task<Genre> AddGenre(string? genreName);
	}
	public class GenreService : IGenreService
	{
		private readonly HttpClient _httpClient;
		string baseAddress = "http://localhost:5098";

        public GenreService(HttpClient httpClient)
        {
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri(baseAddress);
		}

        public async Task<Genre> AddGenre(string? genreName)
		{
			GenreDto genreDto = new GenreDto()
			{
				GenreName = genreName
			};
			string json = JsonSerializer.Serialize(genreDto, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _httpClient.PostAsync("/AddGenre", content);

			response.EnsureSuccessStatusCode();

			Genre genre = await JsonSerializer.DeserializeAsync<Genre>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return genre;
		}
	}
}
