using FluentAssertions;
using MemberService.IntegrationTests.Fixtures;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MemberService.IntegrationTests.Scenarios
{
    [Collection("BaseCollection")]
    class ProfileControllerTests
    {
        private readonly HttpClient client;
        private readonly TestContext testContext;

        public ProfileControllerTests(TestContext testContext)
        {
            this.testContext = testContext;
        }

        [Fact]
        public async Task ListProfiles_Should_ReturnOK()
        {
            var response = await testContext.Client.GetAsync("/v1/profiles");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
