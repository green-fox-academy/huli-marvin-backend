using ScheduleAPI.Models;
using ScheduleAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleAPI.Repositories
{
    public class EventRepository : IGenericRepository<Event>
    {
        private EventContext eventContext;
        private PaginationService paginationService;

        public EventRepository(EventContext eventContext, PaginationService paginationService)
        {
            this.eventContext = eventContext;
            this.paginationService = paginationService;
        }

        public async Task CreateAsync(Event occurrence)
        {
            eventContext.Add(occurrence);
            await eventContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Event occurrence)
        {
            eventContext.Update(occurrence);
            await eventContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var removable = GetItemByIdAsync(id);
            eventContext.Remove(removable);
            await eventContext.SaveChangesAsync();
        }

        public async Task<Event> GetItemByIdAsync(int id)
        {
            return await eventContext.Events.FindAsync(id);
        }

        public async Task<IEnumerable<Event>> GetAllAsync(int pageSize, int pageIndex)
        {
            private int itemCount = eventContext.Events.Count();

            if (paginationService.ParameterValidation(pageIndex, pageSize, itemCount))
            {
                return eventContext.Events.Skip(pageIndex * pageSize).Take(paginationService.CalcNumberOfItemsOnPage(
                    pageIndex, pageSize, itemCount));
            }
            return null;
        }
    }
}
