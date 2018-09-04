using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace MemberService.Extensions
{
    public static class ConnectionStringBuilder
    {
        public static void BuildConnection(this DbContextOptionsBuilder options, IConfigurationRoot Configuration)
        {
            options.UseSqlServer(Configuration.GetConnectionString("connStringLocal"));
        }
    }
}
