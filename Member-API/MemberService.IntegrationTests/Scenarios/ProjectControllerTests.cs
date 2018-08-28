using FluentAssertions;
using MemberService.IntegrationTests.Fixtures;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MemberService.IntegrationTests.Scenarios
{
    [Collection("BaseCollection")]
    public class ProjectControllerTests
    {
        private readonly HttpClient client;
        private readonly TestContext testContext;

        public ProjectControllerTests(TestContext testContext)
        {
            this.testContext = testContext;
        }

        [Fact]
        public async Task ListProjects_Should_ReturnOK()
        {
            var response = await testContext.Client.GetAsync("/v1/projects");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
