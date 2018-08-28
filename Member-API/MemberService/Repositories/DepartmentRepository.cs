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
    public class DepartmentRepository : ICrudRepository<Department>
    {
        private readonly MemberContext memberContext;

        public DepartmentRepository(MemberContext memberContext)
        {
            this.memberContext = memberContext;
        }

        public async Task<IEnumerable<Department>> SelectAllAsync(IQueryCollection query)
        {
            if (query != null)
            {
                var selectedDepartments = (await memberContext.Departments
                    .Where(d => Search.SearchFromQuery<Department>(query)(d)).ToListAsync());
                return selectedDepartments;
            }

            await LoadContextAsync();
            return memberContext.Departments;
        }

        public async Task<Department> SelectByIdAsync(long id)
        {
            await LoadContextAsync();
            var departmentToFind = await memberContext.Departments.FindAsync(id);

            if (departmentToFind != null)
                return departmentToFind;

            throw new NotFoundException();
        }

        public async Task LoadContextAsync()
        {
            await memberContext.Cohorts.LoadAsync();
            await memberContext.Courses.LoadAsync();
            await memberContext.Projects.LoadAsync();
        }

        public async Task InsertAsync(Department departmentToAdd)
        {
            await memberContext.Departments.AddAsync(departmentToAdd);
            await memberContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(long id)
        {
            var departmentToDelete = await SelectByIdAsync(id);
            memberContext.Departments.Remove(departmentToDelete);
            await memberContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department departmenToUpdate)
        {
            memberContext.Departments.Update(departmenToUpdate);
            await memberContext.SaveChangesAsync();
        }
    }
}
