using System;
using System.Collections.Generic;

namespace ScheduleAPI.Models
{
    public partial class Events
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? Template { get; set; }

        public EventTemplates TemplateNavigation { get; set; }
    }
}
