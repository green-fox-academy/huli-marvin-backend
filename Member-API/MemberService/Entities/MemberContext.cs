using MemberService.DataTypes.Enums;
using MemberService.Models;
using MemberService.Models.JoinModels;
using Microsoft.EntityFrameworkCore;
using System;

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

            modelBuilder.Entity<Class>().HasData(
                new { Id = (long)1, Name = "Ocelot", Color = "red", Status = Status.Active, CalendarId = (long)2, SlackChannelId = (long)7 },
                new { Id = (long)2, Name = "Secret", Color = "blue", Status = Status.Custom, CalendarId = (long)1, SlackChannelId = (long)2 },
                new { Id = (long)3, Name = "Raptor", Color = "green", Status = Status.Inactive, CalendarId = (long)3, SlackChannelId = (long)5 },
                new { Id = (long)4, Name = "Lasers", Color = "yellow", Status = Status.Active, CalendarId = (long)4, SlackChannelId = (long)1 },
                new { Id = (long)5, Name = "BadBoi", Color = "purple", Status = Status.Active, CalendarId = (long)5, SlackChannelId = (long)6 },
                new { Id = (long)6, Name = "Seagal", Color = "orange", Status = Status.Inactive, CalendarId = (long)7, SlackChannelId = (long)4 },
                new { Id = (long)7, Name = "Teapot", Color = "black", Status = Status.Custom, CalendarId = (long)6, SlackChannelId = (long)3 }
            );

            modelBuilder.Entity<Cohort>().HasData(
                new { Id = (long)1, Name = "Ace", StartDate = new DateTime(2017,1,1), FinishDate = new DateTime(2017, 4, 1), Status = Status.Custom },
                new { Id = (long)2, Name = "Alopex", StartDate = new DateTime(2017, 5, 1), FinishDate = new DateTime(2017, 9, 1), Status = Status.Active },
                new { Id = (long)3, Name = "Macrotis", StartDate = new DateTime(2017, 10, 1), FinishDate = new DateTime(2018, 2, 1), Status = Status.Active },
                new { Id = (long)4, Name = "Fulvipes", StartDate = new DateTime(2018, 3, 1), FinishDate = new DateTime(2018, 7, 1), Status = Status.Custom }

            );

            modelBuilder.Entity<Course>().HasData(
                new { Id = (long)1, Name = "Standard", Status = Status.Active },
                new { Id = (long)2, Name = "Accenture Girls", Status = Status.Inactive },
                new { Id = (long)3, Name = "Super Mommies", Status = Status.Custom }
            );

            modelBuilder.Entity<Department>().HasData(
                new { Id = (long)1, Name = "HR", Email = "hr@gf.com", PhoneNumber = "36701234567", Country = "HU", ZipNumber = "1000", Address = "Andrassy 66." },
                new { Id = (long)2, Name = "Mentors", Email = "mentors@gf.com", PhoneNumber = "36701234568", Country = "HU", ZipNumber = "1000", Address = "Andrassy 66." },
                new { Id = (long)3, Name = "Partner Management", Email = "partnermgmt@gf.com", PhoneNumber = "36701234569", Country = "HU", ZipNumber = "1000", Address = "Andrassy 66." }
            );

            modelBuilder.Entity<JobHistory>().HasData(
                new { Id = (long)1, Name = "Economist" },
                new { Id = (long)2, Name = "Teacher" },
                new { Id = (long)3, Name = "Psychologist" }
            );

            //modelBuilder.Entity<Profile>().HasData(
            //    new
            //    {
            //        Id = (long)1,
            //        Name = "Adam",
            //        Email = "",
            //        PhoneNumber = "",
            //        DateOfBirth = new DateTime(2017, 1, 1),
            //        GitHubUser = "",
            //        SlackUser = "",
            //        LinkedIn = "",
            //        IsSigned = true,
            //        Picture = ""
            //    },
            //    new
            //    {
            //        Id = (long)2,
            //        Name = "Eva",
            //        Email = "x",
            //        PhoneNumber = "x",
            //        DateOfBirth = new DateTime(2017, 1, 2),
            //        GitHubUser = "x",
            //        SlackUser = "x",
            //        LinkedIn = "x",
            //        IsSigned = true,
            //        Picture = "x"
            //    },
            //    new
            //    {
            //        Id = (long)3,
            //        Name = "Janos",
            //        Email = "x",
            //        PhoneNumber = "x",
            //        DateOfBirth = new DateTime(2017, 1, 3),
            //        GitHubUser = "x",
            //        SlackUser = "x",
            //        LinkedIn = "x",
            //        IsSigned = true,
            //        Picture = "x"
            //    }
            //);

            modelBuilder.Entity<Project>().HasData(
                new { Id = (long)1, Name = "Marvin", Description = "Csharp"},
                new { Id = (long)2, Name = "Szera", Description = "JAVA" },
                new { Id = (long)3, Name = "Malachite", Description = "Python, DevOps" }
            );

            //modelBuilder.Entity<ClassProfile>().HasData(
            //    new { ClassId = (long)1, ProfileId = (long)2 },
            //    new { ClassId = (long)2, ProfileId = (long)1 },
            //    new { ClassId = (long)3, ProfileId = (long)3 }
            //);

            modelBuilder.Entity<Team>().HasData(
                new { Id = (long)1, Name = "Amazonite" },
                new { Id = (long)2, Name = "Malachite" }
            );

            modelBuilder.Entity<AttendanceInfo>().HasData(
                new { Id = (long)1, Late = 1, DayOff = 0, SickVerified = 0, SickUnverified = 1 },
                new { Id = (long)2, Late = 14, DayOff = 0, SickVerified = 2, SickUnverified = 2 },
                new { Id = (long)3, Late = 5, DayOff = 2, SickVerified = 0, SickUnverified = 1 }
            );
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
