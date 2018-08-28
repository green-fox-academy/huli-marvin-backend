using MemberService.Extensions;
using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Exceptions;
using MemberService.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MemberService.Services
{
    public class ProfileService : ICrudService<ProfileDTO, Profile>
    {
        private readonly ICrudRepository<Profile> profileRepository;
        private readonly AutoMapper.IMapper mapper;

        public ProfileService(ICrudRepository<Profile> profileRepository, AutoMapper.IMapper mapper)
        {
            this.profileRepository = profileRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProfileDTO>> GetAllAsync(IQueryCollection query)
        {
            if (!QueryValidator<Profile>.IsAllPropertyValid(query))
            {
                throw new BadRequestException();
            }
            var profiles = await profileRepository.SelectAllAsync(query);
            return profiles.Select(p => mapper.Map<ProfileDTO>(p));
        }

        public async Task AddAsync(Profile profile)
        {
            await profileRepository.InsertAsync(profile);
        }

        public async Task<ProfileDTO> GetByIdAsync(long id)
        {
            var profile = await profileRepository.SelectByIdAsync(id);
            return mapper.Map<ProfileDTO>(profile);
        }

        public async Task UpdateAsync(long id, Profile profile)
        {
            if (profile.Id != id)
            {
                throw new BadRequestException();
            }
            await profileRepository.UpdateAsync(profile);
        }

        public async Task RemoveByIdAsync(long id)
        {
            await profileRepository.DeleteByIdAsync(id);
        }
    }
}
