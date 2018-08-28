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
    public class ProjectRepository : ICrudRepository<Project>
    {
        private readonly MemberContext memberContext;

        public ProjectRepository(MemberContext memberContext)
        {
            this.memberContext = memberContext;
        }

        public async Task<IEnumerable<Project>> SelectAllAsync(IQueryCollection query)
        {
            if (query != null)
            {
                var selectedProjects = (await memberContext.Projects
                    .Where(p => Search.SearchFromQuery<Project>(query)(p)).ToListAsync());
                return selectedProjects;
            }

            await LoadContextAsync();
            return memberContext.Projects;
        }

        public async Task<Project> SelectByIdAsync(long id)
        {
            await LoadContextAsync();
            var projectToFind = await memberContext.Projects.FindAsync(id);

            if (projectToFind != null)
                return projectToFind;

            throw new NotFoundException();
        }

        public async Task LoadContextAsync()
        {
            await memberContext.Teams.LoadAsync();
            await memberContext.Classes.LoadAsync();
            await memberContext.Profiles.LoadAsync();
            await memberContext.Departments.LoadAsync();
        }

        public async Task InsertAsync(Project projectToAdd)
        {
            await memberContext.Projects.AddAsync(projectToAdd);
            await memberContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(long id)
        {
            var projectToDelete = await SelectByIdAsync(id);
            memberContext.Projects.Remove(projectToDelete);
            await memberContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Project projectToUpdate)
        {
            memberContext.Projects.Update(projectToUpdate);
            await memberContext.SaveChangesAsync();
        }
    }
}
