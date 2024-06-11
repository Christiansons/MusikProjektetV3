using MusikProjektetV3.Data;
using MusikProjektetV3.Models;

namespace MusikProjektetV3.Repositories
{
	public interface IUserRepository
	{
		void AddUser(User user);
		User GetUserById(int id);
		void SaveChanges();
	}

	public class UserRepository : IUserRepository
	{
		private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
        
		public User GetUserById(int id)
		{
			
		}

		public void AddUser(User user)
		{
			_context.Users.Add(user);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}
