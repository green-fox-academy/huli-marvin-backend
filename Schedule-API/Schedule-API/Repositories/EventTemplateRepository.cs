using Microsoft.EntityFrameworkCore;
using ScheduleAPI.Models;
using ScheduleAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleAPI.Repositories
{
    public class EventTemplateRepository : IGenericRepository<EventTemplate>
    {
        private EventContext eventContext;
        private PaginationService paginationService;

        public EventTemplateRepository(EventContext eventContext, PaginationService paginationService)
        {
            this.eventContext = eventContext;
            this.paginationService = paginationService;
        }

        public async Task CreateAsync(EventTemplate eventTemplates)
        {
            eventContext.Add(eventTemplates);
            await eventContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(EventTemplate eventTemplates)
        {
            eventContext.Update(eventTemplates);
            await eventContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var removable = GetItemByIdAsync(id);
            eventContext.Remove(removable);
            await eventContext.SaveChangesAsync();
        }

        public async Task<EventTemplate> GetItemByIdAsync(int id)
        {
            return await eventContext.EventTemplates.FindAsync(id);
        }

        public async Task<IEnumerable<EventTemplate>> GetAllAsync(int pageIndex, int pageSize)
        {
            int itemCount = await eventContext.EventTemplates.CountAsync();

            if (paginationService.ParameterValidation(pageIndex, pageSize, itemCount))
            {
                return eventContext.EventTemplates.Skip(pageIndex * pageSize).Take(paginationService.CalcNumberOfItemsOnPage(
                   pageIndex, pageSize, itemCount));
            }
            return null;
        }
    }
}
