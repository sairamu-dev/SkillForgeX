using SkillForgeX.Repository_pattern.Core.Enitities;

namespace SkillForgeX.Repository_pattern.Core.Interfaces
{
    public interface IUser
    {
        List<User> GetUsers();
        List<User> GetDevUsers();
        int GetDevUserCount();
        User GetDevUserByID(int UserID);
        void UpdateTaskForDevUser(User user);

    }
}
