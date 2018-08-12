namespace ScheduleAPI.Models
{
    public partial class Event
    {
        public int Id { get; set; }
        public virtual int EventTypeId
        {
            get
            {
                return (int)this.EventType;
            }
            set
            {
                EventType = (EventTypes)value;
            }
        }
        public EventTypes EventType { get; set; }
        public int? Template { get; set; }

        public EventTemplate TemplateNavigation { get; set; }
    }

    public enum EventTypes
    {
        Meeting = 0,
        Social = 1
    }
}
