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
    public class ClassService : ICrudService<ClassDTO, Class>
    {
        private readonly ICrudRepository<Class> classRepository;
        private readonly IMapper mapper;

        public ClassService(ICrudRepository<Class> classRepository, IMapper mapper)
        {
            this.classRepository = classRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ClassDTO>> GetAllAsync(IQueryCollection query)
        {
            if (!QueryValidator<Class>.IsAllPropertyValid(query))
            {
                throw new BadRequestException();
            }
            var classes = await classRepository.SelectAllAsync(query);
            return classes.Select(c => mapper.Map<ClassDTO>(c));
        }

        public async Task AddAsync(Class classToAdd)
        {
            await classRepository.InsertAsync(classToAdd);
        }

        public async Task<ClassDTO> GetByIdAsync(long id)
        {
            var currentClass = await classRepository.SelectByIdAsync(id);
            return mapper.Map<ClassDTO>(currentClass);
        }

        public async Task UpdateAsync(long id, Class updatedClass)
        {
            if (updatedClass.Id != id)
            {
                throw new BadRequestException();
            }
            await classRepository.UpdateAsync(updatedClass);
        }

        public async Task RemoveByIdAsync(long id)
        {
            await classRepository.DeleteByIdAsync(id);
        }
    }
}
