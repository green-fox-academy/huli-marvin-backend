using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleAPI.Models
{
    public partial class EventTemplate
    {
        public int EventTemplateID { get; set; }
        public string EventTemplateName { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
