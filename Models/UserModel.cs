namespace SkillForgeX.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public int RoleID { get; set; }
        public string Role { get; set; }
        public int? UserID { get; set; }
    }

    public enum PortalRoles
    {
        Admin = 1,
        Manager,
        Developer,
        Guest
    }
}
