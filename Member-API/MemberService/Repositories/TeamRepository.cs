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
    public class TeamRepository : ICrudRepository<Team>
    {
        private readonly MemberContext memberContext;

        public TeamRepository(MemberContext memberContext)
        {
            this.memberContext = memberContext;
        }

        public async Task<IEnumerable<Team>> SelectAllAsync(IQueryCollection query)
        {
            if (query != null)
            {
                var selectedTeams = (await memberContext.Teams
                    .Where(t => Search.SearchFromQuery<Team>(query)(t)).ToListAsync());
                return selectedTeams;
            }

            await LoadContextAsync();
            return memberContext.Teams;
        }

        public async Task<Team> SelectByIdAsync(long id)
        {
            await LoadContextAsync();
            var teamToFind = await memberContext.Teams.FindAsync(id);

            if (teamToFind != null)
                return teamToFind;

            throw new NotFoundException();
        }

        public async Task LoadContextAsync()
        {
            await memberContext.Projects.LoadAsync();
            await memberContext.Profiles.LoadAsync();
            await memberContext.Teams.LoadAsync();
        }

        public async Task InsertAsync(Team teamToAdd)
        {
            await memberContext.Teams.AddAsync(teamToAdd);
            await memberContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(long id)
        {
            var teamToDelete = await SelectByIdAsync(id);
            memberContext.Teams.Remove(teamToDelete);
            await memberContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Team teamToUpdate)
        {
            memberContext.Teams.Update(teamToUpdate);
            await memberContext.SaveChangesAsync();
        }
    }
}
