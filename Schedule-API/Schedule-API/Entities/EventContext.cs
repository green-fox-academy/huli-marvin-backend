using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ScheduleAPI.Models
{
    public class EventContext : DbContext
    {
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventTemplate> EventTemplates { get; set; }

        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {
        }

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Properties()
                .Where(p => p.Name == p.DeclaringType.Name + "_ID")
                .Configure(p => p.IsKey());

            base.OnModelCreating(modelBuilder);
        }
    }
}
