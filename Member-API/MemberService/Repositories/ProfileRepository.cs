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
    public class ProfileRepository : ICrudRepository<Profile>
    {
        private readonly MemberContext memberContext;
        private readonly DbSet<ClassProfile> classProfileTable;

        public ProfileRepository(MemberContext memberContext)
        {
            this.memberContext = memberContext;
            classProfileTable = memberContext.Set<ClassProfile>();
        }

        public async Task<IEnumerable<Profile>> SelectAllAsync(IQueryCollection query)
        {
            if (query != null)
            {
                var selectedProfiles = (await memberContext.Profiles
                    .Where(p => Search.SearchFromQuery<Profile>(query)(p)).ToListAsync());
                return selectedProfiles;
            }

            await LoadContextAsync();
            return memberContext.Profiles;
        }

        public async Task<Profile> SelectByIdAsync(long id)
        {
            await LoadContextAsync();
            var profileToFind = await memberContext.Profiles.FindAsync(id);

            if (profileToFind != null)
                return profileToFind;

            throw new NotFoundException();
        }

        public async Task LoadContextAsync()
        {
            await memberContext.Teams.LoadAsync();
            await memberContext.Classes.LoadAsync();
            await memberContext.Profiles.LoadAsync();
            await classProfileTable.LoadAsync();
        }

        public async Task InsertAsync(Profile profileToAdd)
        {
            await memberContext.Profiles.AddAsync(profileToAdd);
            await memberContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(long id)
        {
            var profileToDelete = await SelectByIdAsync(id);
            memberContext.Profiles.Remove(profileToDelete);
            await memberContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Profile profileToUpdate)
        {
            memberContext.Profiles.Update(profileToUpdate);
            await memberContext.SaveChangesAsync();
        }
    }
}
