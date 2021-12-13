using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RefitSamples.Models;
using System;
namespace RefitSamples.Data
{
    public class RefitSamplesDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public RefitSamplesDbContext(DbContextOptions<RefitSamplesDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
 
        public DbSet<HelloModel> HelloModels { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<HelloModel>();
        }

    }
}
