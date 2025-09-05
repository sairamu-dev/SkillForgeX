namespace SkillForgeX.Repository_pattern.Core.Enitities
{
    public class PortalRoles
    {
        public int ID { get; set; }
        public string Roles { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
