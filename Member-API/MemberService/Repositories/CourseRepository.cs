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
    public class CourseRepository : ICrudRepository<Course>
    {
        private readonly MemberContext memberContext;

        public CourseRepository(MemberContext memberContext)
        {
            this.memberContext = memberContext;
        }

        public async Task<IEnumerable<Course>> SelectAllAsync(IQueryCollection query)
        {
            if (query != null)
            {
                var selectedCourses = (await memberContext.Courses
                    .Where(c => Search.SearchFromQuery<Course>(query)(c)).ToListAsync());
                return selectedCourses;
            }

            await LoadContextAsync();
            return memberContext.Courses;
        }

        public async Task<Course> SelectByIdAsync(long id)
        {
            await LoadContextAsync();
            var courseToFind = await memberContext.Courses.FindAsync(id);

            if (courseToFind != null)
                return courseToFind;

            throw new NotFoundException();
        }

        public async Task LoadContextAsync()
        {
            await memberContext.Teams.LoadAsync();
            await memberContext.Classes.LoadAsync();
            await memberContext.Profiles.LoadAsync();
            await memberContext.Departments.LoadAsync();
        }

        public async Task InsertAsync(Course courseToAdd)
        {
            await memberContext.Courses.AddAsync(courseToAdd);
            await memberContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(long id)
        {
            var courseToDelete = await SelectByIdAsync(id);
            memberContext.Courses.Remove(courseToDelete);
            await memberContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course courseToUpdate)
        {
            memberContext.Courses.Update(courseToUpdate);
            await memberContext.SaveChangesAsync();
        }
    }
}
