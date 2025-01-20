using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscriptions
{
    public record CreateSubscriptionCommand(
        string SubscriptionType,
        Guid AdminId) : IRequest<Guid>;


}
