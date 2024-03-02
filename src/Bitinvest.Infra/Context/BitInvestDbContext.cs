
using Bitinvest.Domain.Entities;
using Bitinvest.Domain.Repositories;
using Bitinvest.Infra.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bitinvest.Infra.Context
{
    public class BitInvestDbContext : IdentityDbContext, IUnitOfWork
    {
        private readonly IUser _user;
        public BitInvestDbContext(DbContextOptions<BitInvestDbContext> options, IUser user) : base(options)
        {
            _user = user;
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BitInvestDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Entity
                            && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Modified)
                {
                    ((Entity)entityEntry.Entity).AlteradoEm = DateTime.Now;
                    ((Entity)entityEntry.Entity).AlteradoPorId =_user.GetUserId();
                }
                if (entityEntry.State == EntityState.Added)
                {
                    ((Entity)entityEntry.Entity).CriadoEm = DateTime.Now;
                    ((Entity)entityEntry.Entity).CriadoPorId = _user.GetUserId();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> Commit() => await SaveChangesAsync() > 0;
    }
}
