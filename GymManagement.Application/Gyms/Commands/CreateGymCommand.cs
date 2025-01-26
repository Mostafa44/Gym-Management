using GymManagement.Domain.Gyms;
using ErrorOr;
using MediatR;

namespace GymManagement.Application.Gyms.Commands
{
    public record CreateGymCommand(string Name, Guid SubscriptionId) : IRequest<ErrorOr<Gym>>;

}
