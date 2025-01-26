using GymManagement.Application.Gyms.Commands;
using GymManagement.Contracts.Gyms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers
{
    [ApiController]
    [Route("subscriptions/{subscriptionId:guid}/gyms")]
    public class GymsController : ControllerBase
    {
        private ISender _mediator;

        public GymsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGymAsync(CreateGymRequest request, 
                                                        Guid subscriptionId)
        {
            var command = new CreateGymCommand(request.Name, subscriptionId);
            var createGymResult= await _mediator.Send(command);

            return createGymResult.Match(
                gym => Ok(new GymResponse(gym.Name, gym.Id)),
                _ => Problem()
            );
        }
    }
}
