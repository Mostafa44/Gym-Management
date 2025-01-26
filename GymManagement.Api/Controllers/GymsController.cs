using GymManagement.Application.Gyms.Commands;
using GymManagement.Application.Gyms.Queries;
using GymManagement.Contracts.Gyms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers
{

    [Route("subscriptions/{subscriptionId:guid}/gyms")]
    public class GymsController : ApiController
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
                gym => CreatedAtAction(
                        nameof(GetGym),
                        new {subscriptionId, GymId= gym.Id},
                        new GymResponse(gym.Name, gym.Id)),
                errors => Problem(errors)
            );
        }

        [HttpGet("{gymId:guid}")]
        public async Task<IActionResult> GetGym(Guid subscriptionId, Guid gymId)
        {
            var command = new GetGymQuery(subscriptionId, gymId);

            var getGymResult = await _mediator.Send(command);

            return getGymResult.Match(
                gym => Ok(new GymResponse( gym.Name, gym.Id)),
                 Problem);
        }
    }
}
