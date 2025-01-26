using GymManagement.Domain.Gyms;
using MediatR;
using ErrorOr;
using GymManagement.Application.Common.Interfaces;

namespace GymManagement.Application.Gyms.Queries
{
    public class GetGymQueryHandler: IRequestHandler<GetGymQuery, ErrorOr<Gym>>
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IGymsRepository _gymsRepository;

        public GetGymQueryHandler(ISubscriptionsRepository subscriptionsRepository, 
                                    IGymsRepository gymsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _gymsRepository = gymsRepository;
        }
        public async Task<ErrorOr<Gym>> Handle(GetGymQuery request, CancellationToken cancellationToken)
        {
            if (! await _subscriptionsRepository.ExistASync(request.SubscriptionId))
            {
                return Error.NotFound(description: "Subscription not found");
            }
            if (await _gymsRepository.GetByIdAsync(request.GymId) is not Gym gym)
            {
                return Error.NotFound(description: "Gym not found");
            }

            return gym;
        }
    }
}
