using ApiRest.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.DataAccess
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<FootballTeam> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<Entity>();

            builder.Entity<FootballTeam>(e =>
            {
                e.Property(e => e.Manager)
                .HasMaxLength(50);
            });

            base.OnModelCreating(builder);
        }
    }
}
