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

            //modelBuilder.Entity<Class>().HasData(
            //    new { Id = 1, Name = "Ocelot", Color = "red", Status = "Active", CalendarId = 2, SlackChannelId = 7 },
            //    new { Id = 2, Name = "Secret", Color = "blue", Status = "Custom", CalendarId = 1, SlackChannelId = 2 },
            //    new { Id = 3, Name = "Raptor", Color = "green", Status = "Inactive", CalendarId = 3, SlackChannelId = 5 },
            //    new { Id = 4, Name = "Lasers", Color = "yellow", Status = "Active", CalendarId = 4, SlackChannelId = 1 },
            //    new { Id = 5, Name = "BadBoi", Color = "purple", Status = "Active", CalendarId = 5, SlackChannelId = 6 },
            //    new { Id = 6, Name = "Seagal", Color = "orange", Status = "Inactive", CalendarId = 7, SlackChannelId = 4 },
            //    new { Id = 7, Name = "Teapot", Color = "black", Status = "Custom", CalendarId = 6, SlackChannelId = 3 }
            //);

            //modelBuilder.Entity<Cohort>().HasData(
            //    new { Id =1, Name = "Ace", Color = "red", Status = "Active", CalendarId =5, SlackChannelId =},
            //    new { Id =2, Name = "Alopex", Color = "green", Status = "Inactive", CalendarId =4, SlackChannelId =},
            //    new { Id =3, Name = "Macrotis", Color = "blue", Status = "Inactive", CalendarId =2, SlackChannelId =},
            //    new { Id =4, Name = "Fulvipes", Color = "black", Status = "Custom", CalendarId =3, SlackChannelId =}
            //);

            //modelBuilder.Entity<Course>().HasData(
            //    new { Id = , Name = "Standard", Status = "Active", Department = ""},
            //    new { Id = , Name = "Accenture Girls", Status = "Inactive", Department = "" },
            //    new { Id = , Name = "Super Mommies", Status = "Custom", Department = "" }
            //);

            //modelBuilder.Entity<Department>().HasData(
            //    new { Id = 1, Name = "HR", Email = "hr@gf.com", PhoneNumber = "36701234567", Country = "HU", ZipNumber = "1000", Address = "Andrassy 66."},
            //    new { Id = 2, Name = "Mentors", Email = "mentors@gf.com", PhoneNumber = "36701234568", Country = "HU", ZipNumber = "1000", Address = "Andrassy 66." },
            //    new { Id = 3, Name = "Partner Management", Email = "partnermgmt@gf.com", PhoneNumber = "36701234569", Country = "HU", ZipNumber = "1000", Address = "Andrassy 66." }
            //);

            //modelBuilder.Entity<JobHistory>().HasData(
            //    new { Id = 1, Name = "", Profile = ""}
            //);

            //modelBuilder.Entity<Profile>().HasData(
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { }
            //);

            //modelBuilder.Entity<Project>().HasData(
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { }
            //);

            //modelBuilder.Entity<ClassProfile>().HasData(
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { }
            //);

            //modelBuilder.Entity<Team>().HasData(
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { }
            //);

            //modelBuilder.Entity<AttendanceInfo>().HasData(
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { }
            //);

            //modelBuilder.Entity<>().HasData(
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { },
            //    new { }
            //);
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
