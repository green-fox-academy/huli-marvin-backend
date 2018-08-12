using System.Collections.Generic;

namespace ScheduleAPI.Models
{
    public partial class EventTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
