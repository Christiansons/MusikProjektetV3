

using System.Text.Json;
using System.Text;
using MusikProjektetClient.Models.Dtos;
using MusikProjektetClient.Models.Dtos;

namespace MusikProjektetClient
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Login");
			Console.WriteLine("Username:");
			string Username = Console.ReadLine();
            await AddArtistAsync(new UserDto { Name = Username});


		}

        static async Task AddArtistAsync(UserDto user)
        {
            HttpClient client = new HttpClient();
            try
            {
                var json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://localhost:5098/AddUser", content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
