using MusikProjektetClient.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MusikProjektetClient.Helpers
{
	public interface ISongHttpHelper
	{
		Task<HttpResponseMessage> AddSong(AddSongDto songDto);
        Task<HttpResponseMessage> GetAsync(string requestUri);
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
    }
	
	public class SongHttpHelper : ISongHttpHelper
	{
		private readonly HttpClient _httpClient;
		private string baseAddress = "http://localhost:5098";

		public SongHttpHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
			_httpClient.BaseAddress = new Uri(baseAddress);
        }

		public async Task<HttpResponseMessage> AddSong(AddSongDto songDto)
		{
			string json = JsonSerializer.Serialize(songDto);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _httpClient.PostAsync("/AddSong", content);

			return response;
		}

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await _httpClient.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return await _httpClient.PostAsync(requestUri, content);
        }
    }
}
