using ProductCrud.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCrud.Domain.Entities;
using ProductCrud.Application.Constants;
using System.Net.Http;
using ProductCrud.Application.Contracts.Identity;

namespace ProductCrud.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IUserService _userService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserService userService)
            : base(options)
        {
            _userService = userService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var username = await _userService.GetCurrentUserName();

            foreach (var entry in base.ChangeTracker.Entries<BaseDomainEntity>()
                .Where(q => q.State == EntityState.Added))
            {
                entry.Entity.CreatedBy = username ?? "system";
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        public DbSet<Product> Products { get; set; }
    }
}
