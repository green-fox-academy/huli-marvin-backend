using ScheduleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleAPI.ViewModels
{
    public class EventViewModel
    {
        public List<Event> Events { get; set; }
        public List<EventTemplate> EventTemplates { get; set; }

        public EventViewModel()
        {
            Events = new List<Event>();
            EventTemplates = new List<EventTemplate>();
        }
    }
}
