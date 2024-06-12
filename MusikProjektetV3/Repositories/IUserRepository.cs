using MusikProjektetV3.Data;
using MusikProjektetV3.Models;

namespace MusikProjektetV3.Repositories
{
	public interface IUserRepository
	{
		User GetUser(string name);
		void AddUser(User user);
		ICollection<User> GetUsers();
		void SaveChanges();
	}

	public class UserRepository : IUserRepository
	{
		private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
        
		public User GetUser(string name)
		{
			return _context.Users.Where(u => u.Name == name).FirstOrDefault();
		}

		public void AddUser(User user)
		{
			_context.Users.Add(user);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public ICollection<User> GetUsers()
		{
			return _context.Users.ToList();
		}
	}
}
