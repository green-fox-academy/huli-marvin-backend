using MemberService.Entities;
using MemberService.Extensions;
using MemberService.Models;
using MemberService.Models.Exceptions;
using MemberService.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberService.Repositories
{
    public class JobHistoryRepository : ICrudRepository<JobHistory>
    {
        private readonly MemberContext memberContext;

        public JobHistoryRepository(MemberContext memberContext)
        {
            this.memberContext = memberContext;
        }

        public async Task<IEnumerable<JobHistory>> SelectAllAsync(IQueryCollection query)
        {
            if (query != null)
            {
                var selectedJobHistories = (await memberContext.JobHistories
                    .Where(j => Search.SearchFromQuery<JobHistory>(query)(j)).ToListAsync());
                return selectedJobHistories;
            }

            await LoadContextAsync();
            return memberContext.JobHistories;
        }

        public async Task<JobHistory> SelectByIdAsync(long id)
        {
            await LoadContextAsync();
            var jobHistoryToFind = await memberContext.JobHistories.FindAsync(id);

            if (jobHistoryToFind != null)
            {
                return jobHistoryToFind;
            }

            throw new NotFoundException();
        }

        public async Task LoadContextAsync()
        {
            await memberContext.Profiles.LoadAsync();
            await memberContext.JobHistories.LoadAsync();
        }

        public async Task InsertAsync(JobHistory jobHistoryToAdd)
        {
            await memberContext.JobHistories.AddAsync(jobHistoryToAdd);
            await memberContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(long id)
        {
            var jobHistoryToDelete = await SelectByIdAsync(id);
            memberContext.JobHistories.Remove(jobHistoryToDelete);
            await memberContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobHistory jobHistoryToUpdate)
        {
            memberContext.JobHistories.Update(jobHistoryToUpdate);
            await memberContext.SaveChangesAsync();
        }
    }
}
