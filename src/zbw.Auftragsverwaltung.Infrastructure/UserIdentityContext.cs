using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using zbw.Auftragsverwaltung.Core.Users.Entities;

namespace zbw.Auftragsverwaltung.Infrastructure
{
    public class UserIdentityContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public UserIdentityContext(DbContextOptions options) : base(options)
        {
        }

    }
}
