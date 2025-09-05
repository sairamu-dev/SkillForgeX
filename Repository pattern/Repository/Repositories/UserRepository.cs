using SkillForgeX.Repository_pattern.Core.Enitities;
using SkillForgeX.Repository_pattern.Core.Interfaces;

namespace SkillForgeX.Repository_pattern.Repository.Repositories
{
    public class UserRepository : IUser
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) 
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            return _context.users.ToList();
        }

        public List<User> GetDevUsers()
        {
            return _context.users.Where(x => x.UserRole == 3 && x.IsActive == true).ToList();
        }

        public void UpdateTaskForDevUser(User user)
        {
            var existingUser = _context.users.FirstOrDefault(u => u.ID == user.ID);

            if (existingUser != null)
            {
                existingUser.ConcurrentTask = user.ConcurrentTask;
                existingUser.ModifiedDate = DateTime.Now;
                existingUser.ModifiedBy = user.ModifiedBy;
            }
        }

        public User GetDevUserByID(int UserID) 
        {
            return _context.users.FirstOrDefault(x => x.ID == UserID && x.IsActive == true);
        }

        public int GetDevUserCount()
        {
            return _context.users.Where(x => x.UserRole == 3).Count();
        }
    }
}
