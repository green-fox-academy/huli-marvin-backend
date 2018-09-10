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
    public class CourseService : ICrudService<CourseDTO, Course>
    {
        private readonly ICrudRepository<Course> courseRepository;
        private readonly IMapper mapper;

        public CourseService(ICrudRepository<Course> courseRepository, IMapper mapper)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CourseDTO>> GetAllAsync(IQueryCollection query)
        {
            if (!QueryValidator<Course>.IsAllPropertyValid(query))
            {
                throw new BadRequestException();
            }
            var courses = await courseRepository.SelectAllAsync(query);
            return courses.Select(c => mapper.Map<CourseDTO>(c));
        }

        public async Task AddAsync(Course course)
        {
            await courseRepository.InsertAsync(course);
        }

        public async Task<CourseDTO> GetByIdAsync(long id)
        {
            var course = await courseRepository.SelectByIdAsync(id);
            return mapper.Map<CourseDTO>(course);
        }

        public async Task UpdateAsync(long id, Course course)
        {
            if (course.Id != id)
            {
                throw new BadRequestException();
            }
            await courseRepository.UpdateAsync(course);
        }

        public async Task RemoveByIdAsync(long id)
        {
            await courseRepository.DeleteByIdAsync(id);
        }
    }
}
