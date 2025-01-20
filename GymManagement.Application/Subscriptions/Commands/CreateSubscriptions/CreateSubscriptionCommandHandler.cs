using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscriptions
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, Guid>
    {
        public Task<Guid> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Guid.NewGuid());
        }
    }
}