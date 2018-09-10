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
    public class JobHistoryService : ICrudService<JobHistoryDTO, JobHistory>
    {
        private readonly ICrudRepository<JobHistory> jobHistoryRepository;
        private readonly IMapper mapper;

        public JobHistoryService(ICrudRepository<JobHistory> jobHistoryRepository, IMapper mapper)
        {
            this.jobHistoryRepository = jobHistoryRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<JobHistoryDTO>> GetAllAsync(IQueryCollection query)
        {
            if (!QueryValidator<JobHistory>.IsAllPropertyValid(query))
            {
                throw new BadRequestException();
            }
            var jobHistories = await jobHistoryRepository.SelectAllAsync(query);
            return jobHistories.Select(j => mapper.Map<JobHistoryDTO>(j));
        }

        public async Task AddAsync(JobHistory jobHistory)
        {
            await jobHistoryRepository.InsertAsync(jobHistory);
        }

        public async Task<JobHistoryDTO> GetByIdAsync(long jobHistoryId)
        {
            var jobHistory = await jobHistoryRepository.SelectByIdAsync(jobHistoryId);
            return mapper.Map<JobHistoryDTO>(jobHistory);
        }

        public async Task UpdateAsync(long id, JobHistory jobHistory)
        {
            if (jobHistory.Id != id)
            {
                throw new BadRequestException();
            }
            await jobHistoryRepository.UpdateAsync(jobHistory);
        }

        public async Task RemoveByIdAsync(long id)
        {
            await jobHistoryRepository.DeleteByIdAsync(id);
        }
    }
}
