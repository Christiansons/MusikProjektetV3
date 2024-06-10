using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MusikProjektetV3.Models;

namespace MusikProjektetV3.Data
{
	public class ApplicationContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Artist> Artists { get; set; }
		public DbSet<Song> Songs { get; set; }
		public DbSet<ArtistUser> ArtistUsers { get; set; }
		public DbSet<GenreUser> GenreUsers { get; set; }
		public DbSet<SongUser> SongUsers { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
	}
}
