using AutoMapper;
using MemberService.Entities;
using MemberService.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace MemberService.IntegrationTests.Fixtures
{
    public class TestContext : IDisposable
    {
        private TestServer server;
        public HttpClient Client { get; set; }
        public MemberContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public TestContext()
        {
            var builder = new WebHostBuilder()
            .UseEnvironment("Testing")
            .UseStartup<Startup>();

            server = new TestServer(builder);
            Context = server.Host.Services.GetRequiredService<MemberContext>();
            Client = server.CreateClient();

            var config = new MapperConfiguration(c =>
            {
                c.AddProfile(new ApplicationProfile());
            });

            Mapper = config.CreateMapper();
        }

        public void Dispose()
        {
            server.Dispose();
            Client.Dispose();
        }
    }
}
