using FluentAssertions;
using MemberService.Entities;
using MemberService.Factories;
using MemberService.Models;
using MemberService.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace MemberService.UnitTests
{
    public class ClassRepositoryTests
    {
        private readonly MemberContext context;

        private Class testClass1 = (Class)ModelFactory.Creator<Class>();
        private Class testClass2 = (Class)ModelFactory.Creator<Class>();

        public ClassRepositoryTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MemberContext>()
                .UseInMemoryDatabase("testdatabase");
            context = new MemberContext(optionsBuilder.Options);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldDeleteOneFromDatabase()
        {
            await context.Classes.AddAsync(testClass1);
            await context.Classes.AddAsync(testClass2);
            await context.SaveChangesAsync();

            var repository = new ClassRepository(context);
            await repository.DeleteByIdAsync(testClass2.Id);

            var classes = await context.Classes.ToListAsync();

            (await repository.SelectAllAsync(null)).Should().BeEquivalentTo(classes);
        }
    }
}
