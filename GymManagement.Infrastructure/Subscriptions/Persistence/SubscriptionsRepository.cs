using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Subscriptions.Persistence
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        private readonly GymManagementDbContext _dbContext;

        public SubscriptionsRepository(GymManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
           await _dbContext.Subscriptions.AddAsync(subscription);
        }

        public async Task<Subscription?> GetByIdAsync(Guid subscriptionId)
        {
            return await _dbContext.Subscriptions.FindAsync(subscriptionId);
        }

        public  Task RemoveSubscriptionAsync(Subscription subscription)
        {
            _dbContext.Subscriptions.Remove(subscription);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Subscription subscription)
        {
            _dbContext.Subscriptions.Update(subscription);
            return Task.CompletedTask;
        }

        public Task<bool> ExistASync(Guid id)
        {
            return _dbContext.Subscriptions
                .AsNoTracking()
                .AnyAsync(s => s.Id == id);
        }
    }
}