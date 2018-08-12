using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("EventTemplateID")]
        public EventTemplate EventTemplateID { get; set; }
    }

    public enum EventTypes
    {
        Meeting = 0,
        Social = 1
    }
}
