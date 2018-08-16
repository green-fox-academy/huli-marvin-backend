namespace ScheduleAPI.Models
{
    public partial class Event
    {
        public int EventId { get; set; }
        public EventTypes EventType { get; set; }
        public int? EventTemplateId { get; set; }

        public EventTemplate EventTemplate { get; set; }
    }

    public enum EventTypes
    {
        Meeting = 0,
        Social = 1
    }
}
