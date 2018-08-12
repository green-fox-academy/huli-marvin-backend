using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ScheduleAPI.Models
{
    public partial class EventContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventTemplate> EventTemplates { get; set; }

        public EventContext(DbContextOptions<EventContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Event>()
            //    .HasOne(e => e.EventTemplate)
            //    .WithMany(c => c.Events)
            //    .HasForeignKey(e => e.EventTemplateId)
            //    .HasConstraintName("FK_Events_ToEventTemplates");
        }
    }
}
