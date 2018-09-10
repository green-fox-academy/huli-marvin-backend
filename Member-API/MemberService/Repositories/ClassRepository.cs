using MemberService.Entities;
using MemberService.Extensions;
using MemberService.Models;
using MemberService.Models.Exceptions;
using MemberService.Models.Interfaces;
using MemberService.Models.JoinModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberService.Repositories
{
    public class ClassRepository : ICrudRepository<Class>
    {
        private readonly MemberContext memberContext;
        private readonly DbSet<ClassProfile> classProfileTable;

        public ClassRepository(MemberContext memberContext)
        {
            this.memberContext = memberContext;
            classProfileTable = memberContext.Set<ClassProfile>();
        }

        public async Task<IEnumerable<Class>> SelectAllAsync(IQueryCollection query)
        {
            if (query != null)
            {
                var selectedClasses = (await memberContext.Classes
                    .Where(c => Search.SearchFromQuery<Class>(query)(c)).ToListAsync());
                return selectedClasses;
            }

            await LoadContextAsync();
            return memberContext.Classes;
        }

        public async Task<Class> SelectByIdAsync(long id)
        {
            await LoadContextAsync();
            var classToFind = await memberContext.Classes.FindAsync(id);

            if (classToFind != null)
                return classToFind;

            throw new NotFoundException();
        }

        public async Task LoadContextAsync()
        {
            await memberContext.Cohorts.LoadAsync();
            await memberContext.Courses.LoadAsync();
            await memberContext.Profiles.LoadAsync();
            await memberContext.Classes.LoadAsync();
            await classProfileTable.LoadAsync();
        }

        public async Task InsertAsync(Class classToAdd)
        {
            await memberContext.Classes.AddAsync(classToAdd);
            await memberContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(long id)
        {
            var classToDelete = await SelectByIdAsync(id);
            memberContext.Classes.Remove(classToDelete);
            await memberContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Class classToUpdate)
        {
            memberContext.Classes.Update(classToUpdate);
            await memberContext.SaveChangesAsync();
        }
    }
}
