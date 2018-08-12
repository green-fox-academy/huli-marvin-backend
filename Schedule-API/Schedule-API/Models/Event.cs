namespace ScheduleAPI.Models
{
    public partial class Event
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? Template { get; set; }

        public EventTemplate TemplateNavigation { get; set; }
    }
}
