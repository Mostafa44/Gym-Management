using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;
using ErrorOr;

namespace GymManagement.Application.Gyms.Commands
{
    public class CreateGymCommandHandler:IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IGymsRepository _gymRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGymCommandHandler(ISubscriptionsRepository subscriptionsRepository, IGymsRepository gymRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _gymRepository= gymRepository;
            _unitOfWork= unitOfWork;
        }
        public async Task<ErrorOr<Gym>> Handle(CreateGymCommand command, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId);
            if (subscription is null)
            {
                return Error.NotFound(description: "Subscription not found");
            }

            var gym = new Gym(command.Name, 
                              subscription.GetMaxRooms(),
                              command.SubscriptionId);

            var addGymResult = subscription.AddGym(gym);
            if (addGymResult.IsError)
            {
                return addGymResult.Errors;
            }

            await _subscriptionsRepository.UpdateAsync(subscription);
            await _gymRepository.AddGymAsync(gym);
            await _unitOfWork.CommitChangesAsync();

            return gym;
        }
    }
}
