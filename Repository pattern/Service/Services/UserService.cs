using SkillForgeX.Repository_pattern.Core.Enitities;
using SkillForgeX.Repository_pattern.Repository.Repositories;

namespace SkillForgeX.Repository_pattern.Service.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetUsers()
        {
           return _userRepository.GetUsers();
        }

        public IEnumerable<User> GetDevUsers()
        {
           return _userRepository.GetDevUsers();
        }

        public void UpdateTaskForDevUser(User user)
        {
           _userRepository.UpdateTaskForDevUser(user);
        }

        public User GetDevUserByID(int UserID)
        {
           return _userRepository.GetDevUserByID(UserID);
        }

        public int GetDevUserCount()
        {
           return _userRepository.GetDevUserCount();

        }
    }
}
