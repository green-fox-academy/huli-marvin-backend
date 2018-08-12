﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ScheduleAPI.Models
{
    public partial class EventContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventTemplate> EventTemplates { get; set; }

        public EventContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public EventContext(DbContextOptions<EventContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("connString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Type).HasMaxLength(10);
                entity.HasOne(d => d.TemplateNavigation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.Template)
                    .HasConstraintName("FK_Events_ToEventTemplates");
            });

            modelBuilder.Entity<EventTemplate>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
