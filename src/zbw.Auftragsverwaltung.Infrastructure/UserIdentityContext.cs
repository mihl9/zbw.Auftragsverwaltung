using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using zbw.Auftragsverwaltung.Core.Users.Entities;

namespace zbw.Auftragsverwaltung.Infrastructure
{
    public class UserIdentityContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public UserIdentityContext(DbContextOptions options) : base(options)
        {
        }

    }

    public class UserIdentityContextFactory : IDesignTimeDbContextFactory<UserIdentityContext>
    {
        public UserIdentityContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<UserIdentityContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AuthenticationConnectionString"));

            return new UserIdentityContext(optionsBuilder.Options);
        }
    }
}
