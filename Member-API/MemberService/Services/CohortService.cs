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
    public class CohortService : ICrudService<CohortDTO, Cohort>
    {
        private readonly ICrudRepository<Cohort> cohortRepository;
        private readonly IMapper mapper;

        public CohortService(ICrudRepository<Cohort> cohortRepository, IMapper mapper)
        {
            this.cohortRepository = cohortRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CohortDTO>> GetAllAsync(IQueryCollection query)
        {
            if (!QueryValidator<Cohort>.IsAllPropertyValid(query))
            {
                throw new BadRequestException();
            }
            var cohorts = await cohortRepository.SelectAllAsync(query);
            return cohorts.Select(c => mapper.Map<CohortDTO>(c));
        }

        public async Task AddAsync(Cohort cohort)
        {
            await cohortRepository.InsertAsync(cohort);
        }

        public async Task<CohortDTO> GetByIdAsync(long cohortId)
        {
            var cohort = await cohortRepository.SelectByIdAsync(cohortId);
            return mapper.Map<CohortDTO>(cohort);
        }

        public async Task UpdateAsync(long id, Cohort cohort)
        {
            if (cohort.Id != id)
            {
                throw new BadRequestException();
            }
            await cohortRepository.UpdateAsync(cohort);
        }

        public async Task RemoveByIdAsync(long id)
        {
            await cohortRepository.DeleteByIdAsync(id);
        }
    }
}
