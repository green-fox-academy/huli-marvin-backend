using FluentAssertions;
using MemberService.IntegrationTests.Fixtures;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MemberService.IntegrationTests.Scenarios
{
    [Collection("BaseCollection")]
    public class TeamControllerTests
    {
        private readonly HttpClient client;
        private readonly TestContext testContext;

        public TeamControllerTests(TestContext testContext)
        {
            this.testContext = testContext;
        }

        [Fact]
        public async Task ListTeams_Should_ReturnOK()
        {
            var response = await testContext.Client.GetAsync("/v1/teams");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
