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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventTemplate>().HasData(
                new { EventTemplateId = 1, EventTemplateName = "t0" },
                new { EventTemplateId = 2, EventTemplateName = "t1" },
                new { EventTemplateId = 3, EventTemplateName = "t2" }
            );

            modelBuilder.Entity<Event>().HasData(
                new { EventId = 1, EventTypes = "Social", EventTemplateId = 0 },
                new { EventId = 2, EventTypes = "Social", EventTemplateId = 0 },
                new { EventId = 3, EventTypes = "Meeting", EventTemplateId = 1 },
                new { EventId = 4, EventTypes = "Social", EventTemplateId = 1 },
                new { EventId = 5, EventTypes = "Meeting", EventTemplateId = 2 }
            );
        }
    }
}
