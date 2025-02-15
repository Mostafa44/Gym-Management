﻿using GymManagement.Application.Subscriptions.Commands.CreateSubscriptions;
using GymManagement.Application.Subscriptions.Commands.DeleteSubscription;
using GymManagement.Application.Subscriptions.Queries.GetSubscription;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType= GymManagement.Domain.Subscriptions.SubscriptionType;

namespace GymManagement.Api.Controllers
{
    [Route("[controller]")]
    public class SubscriptionsController : ApiController
    {

        private readonly ISender _mediator;

        public SubscriptionsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
        {
            if(!DomainSubscriptionType.TryFromName(request.SubscriptionType.ToString(), out var subscriptionType))
            {
                return Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    detail: "Invalid subscription type");
            }
            var command = new CreateSubscriptionCommand(
                subscriptionType,
                request.AdminId
            );
            var createSubscriptionResult= await _mediator.Send(command);

            return createSubscriptionResult.MatchFirst(
                subscription => Ok( new SubscriptionResponse(subscription.Id, request.SubscriptionType)),
                Problem
            );
           
        }
        [HttpGet("{subscriptionId:guid}")]
        public async Task<IActionResult> GetSubscription(Guid subscriptionId)
        {
            var query = new GetSubscriptionQuery(subscriptionId);
            var result = await _mediator.Send(query);
            return result.MatchFirst(
                subscription => Ok(new SubscriptionResponse(
                    subscriptionId,
                    Enum.Parse<SubscriptionType>(subscription.SubscriptionType.Name)
                    )),
                Problem
            );
        }
        [HttpDelete("{subscriptionId:guid}")]
        public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
        {
            var command = new DeleteSubscriptionCommand(subscriptionId);

            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(
                _ => NoContent(),
                Problem
            );
        }
    }
}
