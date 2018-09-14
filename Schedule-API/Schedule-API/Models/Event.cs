namespace ScheduleAPI.Models
{
    public partial class Event
    {
        public int EventId { get; set; }
        public string EventType { get; set; }
        public int? EventTemplateId { get; set; }

        public EventTemplate EventTemplate { get; set; }
    }
}
