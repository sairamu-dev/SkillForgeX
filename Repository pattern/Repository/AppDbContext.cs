using DevTaskFlow.Repository_pattern.Core.Enitities;
using Microsoft.EntityFrameworkCore;
using Tasks = DevTaskFlow.Repository_pattern.Core.Enitities.Tasks;

namespace DevTaskFlow.Repository_pattern.Repository
{
    public class AppDbContext : DbContext
    {
        public  AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }
        public DbSet<Api> api { get; set; }
        public DbSet<PortalRoles> PortalRoles { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<KeywordWithSkills> KeywordWithSkills { get; set; }
        public DbSet<GuestUser> guestUsers { get; set; }
        public DbSet<ErrorLog> errorLogs { get; set; }
        public DbSet<Feedback> feedbacks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Api>().ToTable("UDT_ApiService");
            modelBuilder.Entity<PortalRoles>().ToTable("UDT_PortalRoles");
            modelBuilder.Entity<Project>().ToTable("UDT_Projects");
            modelBuilder.Entity<User>().ToTable("UDT_Users");
            modelBuilder.Entity<Skills>().ToTable("UDT_Skills");
            modelBuilder.Entity<Tasks>().ToTable("UDT_Tasks");
            modelBuilder.Entity<KeywordWithSkills>().ToTable("UDT_SkillsWithKeywords");
            modelBuilder.Entity<GuestUser>().ToTable("UDT_GuestUser");
            modelBuilder.Entity<ErrorLog>().ToTable("UDT_ErrorLog");
            modelBuilder.Entity<Feedback>().ToTable("UDT_Feedback");
        }
    }
}
