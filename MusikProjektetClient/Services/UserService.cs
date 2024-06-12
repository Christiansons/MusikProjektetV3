using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MusikProjektetClient.Models.Dtos;
using MusikProjektetClient.Models.ViewModels;

namespace MusikProjektetClient.Services
{
	public interface IUserService
	{
		Task GetAllUsersAsync();
		Task GetOrCreateUser(string userName);
	}

	public class UserService : IUserService
	{
		string baseAddress = "http://localhost:5098";
		HttpClient _client;
		public UserService(HttpClient client)
		{
			_client = client;
			_client.BaseAddress = new Uri(baseAddress);
		}

		public async Task GetAllUsersAsync()
		{
				HttpResponseMessage response = await _client.GetAsync("/GetAllUsers");
				response.EnsureSuccessStatusCode();

				AllUsersViewModel viewmodel = await JsonSerializer.DeserializeAsync<AllUsersViewModel>(await response.Content.ReadAsStreamAsync());	
				if(viewmodel != null)
				{
					foreach (var username in viewmodel.UserNames)
					{
						Console.WriteLine(username);
					}
				}
		}

		public async Task GetOrCreateUser(string userName)
		{
				UserDto dto = new UserDto { Name = userName };
				string json = JsonSerializer.Serialize(dto);
				StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await _client.PostAsync("/GetOrCreateUser", content);
				response.EnsureSuccessStatusCode();

				int userId = await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				GlobalLoginVariable.UserId = userId;
		}
	}
}
