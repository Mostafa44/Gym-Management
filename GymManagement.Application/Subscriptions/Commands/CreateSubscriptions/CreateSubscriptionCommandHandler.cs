using System.Reflection.Metadata;
using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscriptions
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
    {
        private readonly ISubscriptionsRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSubscriptionCommandHandler(ISubscriptionsRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription= new Subscription(
                subscriptionType: request.SubscriptionType,
                adminId: request.AdminId
            );

           await  _subscriptionRepository.AddSubscriptionAsync(subscription);
           await _unitOfWork.CommitChangesAsync();
           
            return subscription;
        }
    }
}