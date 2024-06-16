using Microsoft.EntityFrameworkCore;
using Moq;

using MusikProjektetV3.Data;
using MusikProjektetV3.Models;
using MusikProjektetV3.Repositories;

namespace MusikProjektetTests
{
	[TestClass]
	public class RepoTests
	{
		[TestMethod]
		public void GetAllSongs_GetsCorrectSongs()
		{
			// Arrange

			//Create in-memory database
			var options = new DbContextOptionsBuilder<ApplicationContext>()
				.UseInMemoryDatabase(databaseName: "TestDb")
				.Options;

			using (var context = new ApplicationContext(options))
			{
				//Add two songs to test-db
				context.Songs.AddRange(
					new Song 
					{
						Id = 1,
						SongTitle = "TestSong1",
						Artist = new Artist
						{
							Id = 1,
							ArtistName = "TestArtist1", 
							Description= "TestDescription 1"
						}, 
						Genre = new Genre
						{
							Id=1,
							GenreName = "TestGenre1",
						}
					},
					new Song
					{
						Id = 2,
						SongTitle = "TestSong2",
						Artist = new Artist
						{
							Id = 2,
							ArtistName = "TestArtist2",
							Description= "TestDescription 2"
						},
						Genre = new Genre
						{
							Id=2,
							GenreName = "TestGenre2",
						}
					}

				);
				context.SaveChanges();
			}

			// Act
			Song[] result;
			using (var context = new ApplicationContext(options))
			{
				var songRepo = new SongRepository(context);
				result = songRepo.GetAllSongs();
			}

			// Assert
			Assert.AreEqual(2, result.Length);
			Assert.AreEqual("TestSong1", result[0].SongTitle);
			Assert.AreEqual("TestSong2", result[1].SongTitle);
		}

		[TestMethod]
		public void GetGenreByName_ReturnsCorrectGenre()
		{
			// Arrange

			//Create in memory db
			var options = new DbContextOptionsBuilder<ApplicationContext>()
				.UseInMemoryDatabase(databaseName: "TestDb")
				.Options;

			//Add a few genres to db
			using (var context = new ApplicationContext(options))
			{
				context.Genres.AddRange(
					new Genre { Id = 3, GenreName = "Rock" },
					new Genre { Id = 4, GenreName = "Jazz" }
				);
				context.SaveChanges();
			}

			// Act
			Genre result;
			using (var context = new ApplicationContext(options))
			{
				var genreRepo = new GenreRepository(context);
				result = genreRepo.GetGenreByName("Jazz");
			}

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(4, result.Id);
			Assert.AreEqual("Jazz", result.GenreName);
		}

		[TestMethod]
		public void AddArtist_AddsArtistToDatabase()
		{
			// Arrange

			//Create test-db
			var options = new DbContextOptionsBuilder<ApplicationContext>()
				.UseInMemoryDatabase(databaseName: "TestDb")
				.Options;


			//Add a new artist
			using (var context = new ApplicationContext(options))
			{
				var artistRepo = new ArtistRepository(context);
				var newArtist = new Artist
				{
					Id = 3,
					ArtistName = "TestArtist",
					Description = "TestDescription"
				};

				// Act
				artistRepo.AddArtist(newArtist);
				context.SaveChanges();


				// Assert
				//See if artist is added
				var addedArtist = context.Artists.Where(a => a.Id == 3).FirstOrDefault();
				Assert.IsNotNull(addedArtist);
				Assert.AreEqual("TestArtist", addedArtist.ArtistName);
				Assert.AreEqual("TestDescription", addedArtist.Description);
			}
		}
	}
}