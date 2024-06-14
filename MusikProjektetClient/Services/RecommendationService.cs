using MusikProjektetClient.Models.LastFmModels.LastFmDtos;
using MusikProjektetClient.Models.LastFmModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MusikProjektetClient.Services
{
    public interface IRecommendationService
    {
		Task<string> GetAsync(string url);
		Task GetSimilarArtistsAsync();
		Task GetSimilarGenresAsync();
		Task GetSimilarSongsAsync();
	}

	public class RecommendationService : IRecommendationService
	{
		private readonly HttpClient _lastFmClient;
		private readonly string _apiKey = "f85afc0fb158704e03078747d689a0ce"; 

		public RecommendationService(HttpClient lastFmClient)
        {
			_lastFmClient = lastFmClient;
			_lastFmClient.BaseAddress = new Uri("http://ws.audioscrobbler.com/2.0/");
		}

		public async Task<string> GetAsync(string url)
		{
			HttpResponseMessage response = await _lastFmClient.GetAsync(url);
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadAsStringAsync();
		}

		//Fungerar ej
		public async Task GetSimilarGenresAsync()
		{
			Console.Clear();

			//Get a genre from user
			await Console.Out.WriteLineAsync("Write a genre and get similar recommendations!");
			await Console.Out.WriteAsync("Genre: ");
			string genreName = Console.ReadLine();

			
			string url = $"?method=tag.getsimilar&tag={Uri.EscapeDataString(genreName)}&api_key={_apiKey}&limit=5&format=json";
			
			HttpResponseMessage response = await _lastFmClient.GetAsync(url);
			response.EnsureSuccessStatusCode();

			//Parse response to list of Genres, tag is used as Genre on last.fm
			SimilarGenresResponse similarGenres = await JsonSerializer.DeserializeAsync<SimilarGenresResponse>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

			//Print Genres
			foreach (var genre in similarGenres.SimilarGenres.Tags)
			{
				await Console.Out.WriteLineAsync(genre.name);
			}

			await Console.Out.WriteLineAsync("Stopp!");
			Console.ReadLine();
		}

		public async Task GetSimilarArtistsAsync()
		{
			Console.Clear();

			//Get an artist from user
            await Console.Out.WriteLineAsync("Write an artist and get similar recommendations!");
            await Console.Out.WriteAsync("Artist: ");
			string artistName = Console.ReadLine();
            
			//Get 5 similar artists from Last.fm
			string url = $"?method=artist.getsimilar&artist={Uri.EscapeDataString(artistName)}&api_key={_apiKey}&limit=5&format=json";
			
			HttpResponseMessage response = await _lastFmClient.GetAsync(url);
			response.EnsureSuccessStatusCode();

			//Parse response to list of Artists
			SimilarArtistResponse similarArtists = await JsonSerializer.DeserializeAsync<SimilarArtistResponse>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

			//Print artists
			foreach(var artist in similarArtists.SimilarArtists.Artists)
			{
                await Console.Out.WriteLineAsync(artist.name);
            }

			Console.ReadLine();
			
		}
		

		public async Task GetSimilarSongsAsync()
		{
			Console.Clear();

			//Get a Song and the corresponding artist from user
			Console.WriteLine("Write an Song and the artist that sings it and get similar recommendations!");
			Console.Write("Song: ");
			string songName = Console.ReadLine();
			Console.Write("Artist: ");
			string artistName = Console.ReadLine();

			//Get 5 similar songs and their artists from Last.fm
			string url = $"?method=track.getsimilar&track={Uri.EscapeDataString(songName)}&artist={Uri.EscapeDataString(artistName)}&api_key={_apiKey}&format=json&limit=5";
			string jsonResponse = await GetAsync(url);

			//Parse response to list of Songs and their artists
			var similarSongs = JsonSerializer.Deserialize<SimilarSongResponse>(jsonResponse);

			//Print artists
			foreach (var song in similarSongs.SimilarSongs.Songs)
			{
				Console.WriteLine($"'{song.name}' by: '{song.artist.name}'");
			}

			Console.ReadLine();
		}
	}
}
