using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Common.Persistence
{
    public class GymManagementDbContext: DbContext, IUnitOfWork
    {
        public DbSet<Subscription> Subscriptions { get; set; } = null!;

        public GymManagementDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public async Task CommitChangesAsync()
        {
            await SaveChangesAsync();
        }
    }
}