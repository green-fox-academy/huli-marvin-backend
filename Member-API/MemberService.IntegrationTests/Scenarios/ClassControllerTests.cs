using FluentAssertions;
using MemberService.Extensions;
using MemberService.Factories;
using MemberService.IntegrationTests.Fixtures;
using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MemberService.IntegrationTests.Scenarios
{
    [Collection("BaseCollection")]
    public class ClassControllerTests 
    {
        private readonly TestContext testContext;

        public ClassControllerTests(TestContext testContext)
        {
            this.testContext = testContext;
        }

        [Fact]
        public async Task ListClasses_Should_ReturnOK()
        {
            var response = await testContext.Client.GetAsync("/v1/classes");
            var message = JsonConvert.SerializeObject(testContext.Context.Classes.Select(c => testContext.Mapper.Map<ClassDTO>(c)));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().BeEquivalentTo("application/json");
            response.Content.ReadAsStringAsync().Result.Should().BeEquivalentTo(message);
        }

        [Fact]
        public async Task CreateClass_Should_ReturnOk()
        {
            var newClass = (Class)ModelFactory.Creator<Class>();
            var newClassDTO = testContext.Mapper.Map<ClassDTO>(newClass);
            var content = newClassDTO.ToJsonContent();
            var response = await testContext.Client.PostAsync("/v1/classes", content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task FindClass_Should_ReturnOk_And_ReturnJsonResult_IfClassExists()
        {
            var classToFind = await testContext.Context.Classes.FirstOrDefaultAsync();
            var message = JsonConvert.SerializeObject(testContext.Mapper.Map<ClassDTO>(classToFind));
            var response = await testContext.Client.GetAsync($"/v1/class/{classToFind.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().BeEquivalentTo("application/json");
            response.Content.ReadAsStringAsync().Result.Should().BeEquivalentTo(message);
        }

        [Fact]
        public async Task UpdateClass_Should_ReturnOk()
        {
            var classToUpdate = await testContext.Context.Classes.FirstOrDefaultAsync();
            var updatedClass = classToUpdate;
            updatedClass.Color = "purple";
            var updatedClassDTO = testContext.Mapper.Map<ClassDTO>(updatedClass);
            var content = updatedClassDTO.ToJsonContent();

            var response = await testContext.Client.PutAsync($"/v1/class/{classToUpdate.Id}", content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteClass_Should_ReturnOk()
        {
            var classToDelete = await testContext.Context.Classes.FirstOrDefaultAsync();
            var response = await testContext.Client.DeleteAsync($"/v1/class/{classToDelete.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
