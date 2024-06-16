using Moq;
using MusikProjektetV3.Data;
using MusikProjektetV3.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MusikProjektetTests
{
	[TestClass]
	public class SongRepoTests
	{
		private readonly Mock<ApplicationContext> _mockContext;
		private readonly SongRepository _repository;

		
		public SongRepoTests()
		{
			_mockContext = new Mock<ApplicationContext>();
			_repository = new SongRepository(_mockContext.Object);
		}

		[TestMethod]
		public void TestMethod1()
		{
		}
	}
}