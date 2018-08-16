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
    public class ProjectService : ICrudService<ProjectDTO, Project>
    {
        private readonly ICrudRepository<Project> projectRepository;
        private readonly IMapper mapper;

        public ProjectService(ICrudRepository<Project> projectRepository, IMapper mapper)
        {
            this.projectRepository = projectRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllAsync(IQueryCollection query)
        {
            if (!QueryValidator<Project>.IsAllPropertyValid(query))
            {
                throw new BadRequestException();
            }
            var projects = await projectRepository.SelectAllAsync(query);
            return projects.Select(p => mapper.Map<ProjectDTO>(p));
        }

        public async Task AddAsync(Project project)
        {
            await projectRepository.InsertAsync(project);
        }

        public async Task<ProjectDTO> GetByIdAsync(long id)
        {
            var project = await projectRepository.SelectByIdAsync(id);
            return mapper.Map<ProjectDTO>(project);
        }

        public async Task UpdateAsync(long id, Project project)
        {
            if (project.Id != id)
            {
                throw new BadRequestException();
            }
            await projectRepository.UpdateAsync(project);
        }

        public async Task RemoveByIdAsync(long id)
        {
            await projectRepository.DeleteByIdAsync(id);
        }
    }
}
