using MemberService.Models;
using MemberService.Models.JoinModels;
using Microsoft.EntityFrameworkCore;

namespace MemberService.Entities
{
    public class MemberContext : DbContext
    {
        public DbSet<Class> Classes { get; set; }
        public DbSet<Cohort> Cohorts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<JobHistory> JobHistories { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<AttendanceInfo> Attendances { get; set; }

        public MemberContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ClassRelationBuilder(modelBuilder);
            DepartmentRelationBuilder(modelBuilder);
            ProfileRelationBuilder(modelBuilder);
            TeamRelationBuilder(modelBuilder);
        }

        private static void ClassRelationBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .HasOne(c => c.Cohort)
                .WithMany(c => c.Classes);
            
            modelBuilder.Entity<Class>()
                .HasOne(c => c.Course)
                .WithMany(c => c.Classes);
        }

        private static void DepartmentRelationBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Cohorts)
                .WithOne(c => c.Department);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Courses)
                .WithOne(c => c.Department);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Projects)
                .WithOne(p => p.Department);
        }

        private static void ProfileRelationBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>()
                .HasMany(p => p.ClassLeads)
                .WithOne(c => c.ClassLead);

            modelBuilder.Entity<Profile>()
                .HasMany(p => p.ClassAdmins)
                .WithOne(c => c.ClassAdmin);

            modelBuilder.Entity<Profile>()
                .HasOne(p => p.ClassApprentice)
                .WithMany(c => c.Apprentices);

            modelBuilder.Entity<Profile>()
                .HasMany(p => p.FormerJobs)
                .WithOne(j => j.Profile);

            modelBuilder.Entity<ClassProfile>()
                .HasKey(cp => new { cp.ClassId, cp.ProfileId });

            modelBuilder.Entity<ClassProfile>()
                .HasOne(cp => cp.Class)
                .WithMany(c => c.Mentors)
                .HasForeignKey(cp => cp.ClassId);

            modelBuilder.Entity<ClassProfile>()
                .HasOne(cp => cp.Profile)
                .WithMany(p => p.ClassMentors)
                .HasForeignKey(cp => cp.ProfileId);

            modelBuilder.Entity<Profile>()
                .HasOne(p => p.AttendanceInfo);
        }

        private static void TeamRelationBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Teams);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.ProductOwner)
                .WithMany(p => p.TeamProductOwners);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.ScrumMaster)
                .WithMany(p => p.TeamScrumMasters);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Apprentices)
                .WithOne(p => p.TeamApprentice);
        }
    }
}
