using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleAPI.Models
{
    public partial class EventTemplate
    {
        public int EventTemplateId { get; set; }
        public string EventTemplateName { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
