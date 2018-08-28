using MemberService.IntegrationTests.Fixtures;
using System.Net.Http;
using Xunit;

namespace MemberService.IntegrationTests.Scenarios
{
    [Collection("BaseCollection")]
    public class DepartmentControllerTests
    {
        private readonly HttpClient client;
        private readonly TestContext testContext;

        public DepartmentControllerTests(TestContext testContext)
        {
            this.testContext = testContext;
        }
    }
}
