using GymManagement.Domain.Gyms;
using MediatR;
using ErrorOr;

namespace GymManagement.Application.Gyms.Queries
{
    public record GetGymQuery(Guid SubscriptionId, Guid GymId) : IRequest<ErrorOr<Gym>>;

}
