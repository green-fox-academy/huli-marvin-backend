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
    public class CohortRepository : ICrudRepository<Cohort>
    {
        private readonly MemberContext memberContext;

        public CohortRepository(MemberContext memberContext)
        {
            this.memberContext = memberContext;
        }

        public async Task<IEnumerable<Cohort>> SelectAllAsync(IQueryCollection query)
        {
            if (query != null)
            {
                var selectedCohort = (await memberContext.Cohorts
                    .Where(c => Search.SearchFromQuery<Cohort>(query)(c)).ToListAsync());
                return selectedCohort;
            }

            await LoadContextAsync();
            return memberContext.Cohorts;
        }

        public async Task<Cohort> SelectByIdAsync(long id)
        {
            await LoadContextAsync();
            var cohortToFind = await memberContext.Cohorts.FindAsync(id);

            if (cohortToFind != null)
                return cohortToFind;

            throw new NotFoundException();
        }

        public async Task LoadContextAsync()
        {
            await memberContext.Cohorts.LoadAsync();
            await memberContext.Classes.LoadAsync();
            await memberContext.Departments.LoadAsync();
        }

        public async Task InsertAsync(Cohort cohortToAdd)
        {
            await memberContext.Cohorts.AddAsync(cohortToAdd);
            await memberContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(long id)
        {
            var cohortToDelete = await SelectByIdAsync(id);
            memberContext.Cohorts.Remove(cohortToDelete);
            await memberContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cohort cohortToUpdate)
        {
            memberContext.Cohorts.Update(cohortToUpdate);
            await memberContext.SaveChangesAsync();
        }
    }
}
