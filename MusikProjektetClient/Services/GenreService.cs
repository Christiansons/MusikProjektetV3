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
	public interface IGenreService
	{
		Task<Genre> AddGenre();
		Task AddGenreToUser();
        Task ShowAllGenreConnectedToUser();

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

        public async Task<Genre> AddGenre()
		{
			Console.Clear();
			Console.WriteLine("Add a new song to the database!");
			Console.WriteLine("Whats the genre?");
			Console.Write("Genre: ");
			string genreName = Console.ReadLine();
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

		public async Task AddGenreToUser()
		{
            Console.Clear();
            Console.WriteLine("Add a genre to User");
            Console.WriteLine("Whats the genre?");
            Console.Write("Genre: ");
            string genreName = Console.ReadLine();
            GenreDto genreDto = new GenreDto()
            {
                GenreName = genreName
            };



            string json = JsonSerializer.Serialize(genreDto, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			StringContent userID = new StringContent(GlobalLoginVariable.UserId.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("/AddGenre", content);
            Genre genre = await JsonSerializer.DeserializeAsync<Genre>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			GenreConnectionDto connectcontent = new GenreConnectionDto()
			{
				UsersId = GlobalLoginVariable.UserId,
				GenresId = genre.Id
			};

			string connectcontentseri = JsonSerializer.Serialize<GenreConnectionDto>(connectcontent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            StringContent connectioncontent = new StringContent(connectcontentseri, Encoding.UTF8, "application/json");

            HttpResponseMessage connectResponse = await _httpClient.PostAsync($"/ConnectUserToGenre", connectioncontent);

            response.EnsureSuccessStatusCode();

            Console.WriteLine("Genre added");
			
        }

		public async Task ShowAllGenreConnectedToUser()
		{
            Console.Clear();
            HttpResponseMessage response = await _httpClient.GetAsync($"/GetGenresConnectedToPerson/{GlobalLoginVariable.UserId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to retrieve Genres. Status code: {response.StatusCode}");
                return;
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var genreList = JsonSerializer.Deserialize<GetAllGenresConnectedToPersonViewModel>(jsonResponse);
            if (genreList.GenreNames.Count == 0)
            {
                Console.WriteLine("No songs added to this user.");
                return;
            }
            Console.WriteLine("Songs added to user:");
            foreach (var genrename in genreList.GenreNames)
            {
                Console.WriteLine(genrename);
            }
        }
	}
}
