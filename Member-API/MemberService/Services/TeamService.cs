using AutoMapper;
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
    public class TeamService : ICrudService<TeamDTO, Team>
    {
        private readonly ICrudRepository<Team> teamRepository;
        private readonly IMapper mapper;

        public TeamService(ICrudRepository<Team> teamRepository, IMapper mapper)
        {
            this.teamRepository = teamRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TeamDTO>>GetAllAsync(IQueryCollection query)
        {
            if (!QueryValidator<Team>.IsAllPropertyValid(query))
            {
                throw new BadRequestException();
            }
            var teams = await teamRepository.SelectAllAsync(query);
            return teams.Select(t => mapper.Map<TeamDTO>(t));
        }

        public async Task AddAsync(Team team)
        {
            await teamRepository.InsertAsync(team);
        }

        public async Task<TeamDTO> GetByIdAsync(long teamId)
        {
            var team = await teamRepository.SelectByIdAsync(teamId);
            return mapper.Map<TeamDTO>(team);
        }

        public async Task UpdateAsync(long id, Team team)
        {
            if (team.Id != id)
            {
                throw new BadRequestException();
            }
            await teamRepository.UpdateAsync(team);
        }

        public async Task RemoveByIdAsync(long teamId)
        {
            await teamRepository.DeleteByIdAsync(teamId);
        }
    }
}
