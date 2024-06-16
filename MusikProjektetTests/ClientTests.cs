using Moq.Protected;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MusikProjektetClient.Services;
using MusikProjektetClient.Models;
using MusikProjektetClient;

namespace MusikProjektetTests
{
	[TestClass]
	public class ClientTests
	{
		[TestMethod]
		public async Task AddSongToUser_SendsCorrectRequest()
		{
			// Arrange
			var artistService = new Mock<IArtistService>();
			var genreService = new Mock<IGenreService>();
			
			//Create new mock for messagehandler
			var mockHandler = new Mock<HttpMessageHandler>();
			mockHandler
				.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>()
				)
				.ReturnsAsync(new HttpResponseMessage
				{
					StatusCode = HttpStatusCode.OK,
					Content = new StringContent("{\"message\":\"Success Adding song\"}", Encoding.UTF8, "application/json")
				});

			//Create a new http-client with mock-message handler
			HttpClient mockClient = new HttpClient(mockHandler.Object)
			{
				BaseAddress = new Uri("http://localhost:5098")
			};

			// Set the "login-id" 
			GlobalLoginVariable.UserId = 1;

			// Create an instance of songservice with the mocked services in its constructor
			var songService = new SongService(mockClient, artistService.Object, genreService.Object);

			// Mock console input in the method
			var consoleInput = new StringReader("TestSongTitle\n");
			
			//SetIn Applies the console-input required in the method
			Console.SetIn(consoleInput);

			// Act
			await songService.AddSongToUser();

			// Assert
			mockHandler.Protected().Verify(
				//Check if one request is ent
				"SendAsync",
				Times.Once(),
				ItExpr.Is<HttpRequestMessage>(req =>
					//Check if post
					req.Method == HttpMethod.Post &&
					//Check if right uri
					req.RequestUri.ToString().Contains("/ConnectUserToSong") &&
					//Check if correct song-info sent
					req.Content.ReadAsStringAsync().Result.Contains("\"songName\":\"TestSongTitle\"") &&
					//Check if correct user-info sent
					req.Content.ReadAsStringAsync().Result.Contains("\"userId\":1")
				)
			);
		}
	}
}
