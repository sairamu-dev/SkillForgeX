namespace SkillForgeX.Areas.Manager.ViewModels
{
    public class UsersViewModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public int UserRole { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int? ConcurrentTask { get; set; }
        public string Skills { get; set; }
        public PortalRoles rolesList { get; set; }
    }

    public enum PortalRoles
    {
        Admin = 1,
        Manager,
        Developer,
        Guest
    }
}
