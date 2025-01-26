using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Common.Interfaces
{
    public interface ISubscriptionsRepository
    {
        Task AddSubscriptionAsync(Subscription subscription);

        Task<Subscription?> GetByIdAsync(Guid subscriptionId);
        Task RemoveSubscriptionAsync(Subscription subscription);
        Task UpdateAsync(Subscription subscription);
        Task<bool> ExistASync(Guid id);
    }
}