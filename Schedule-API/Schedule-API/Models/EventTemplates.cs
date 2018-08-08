using System;
using System.Collections.Generic;

namespace ScheduleAPI.Models
{
    public partial class EventTemplates
    {
        public EventTemplates()
        {
            Events = new HashSet<Events>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Events> Events { get; set; }
    }
}
