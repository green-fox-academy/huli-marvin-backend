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
    public class DepartmentService : ICrudService<DepartmentDTO, Department>
    {
        private readonly ICrudRepository<Department> departmentRepository;
        private readonly IMapper mapper;

        public DepartmentService(ICrudRepository<Department> departmentRepository, IMapper mapper)
        {
            this.departmentRepository = departmentRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllAsync(IQueryCollection query)
        {
            if (!QueryValidator<Department>.IsAllPropertyValid(query))
            {
                throw new BadRequestException();
            }
            var departments = await departmentRepository.SelectAllAsync(query);
            return departments.Select(d => mapper.Map<DepartmentDTO>(d));
        }

        public async Task AddAsync(Department department)
        {
            await departmentRepository.InsertAsync(department);
        }

        public async Task<DepartmentDTO> GetByIdAsync(long id)
        {
            var department = await departmentRepository.SelectByIdAsync(id);
            return mapper.Map<DepartmentDTO>(department);
        }

        public async Task UpdateAsync(long id, Department department)
        {
            if (department.Id != id)
            {
                throw new BadRequestException();
            }
            await departmentRepository.UpdateAsync(department);
        }

        public async Task RemoveByIdAsync(long id)
        {
            await departmentRepository.DeleteByIdAsync(id);
        }
    }
}
