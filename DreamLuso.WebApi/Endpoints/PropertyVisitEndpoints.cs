using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.PropertyVisits.Commands.CancelVisit;
using DreamLuso.Application.CQ.PropertyVisits.Commands.ConfirmVisit;
using DreamLuso.Application.CQ.PropertyVisits.Commands.Create;
using DreamLuso.Application.CQ.PropertyVisits.Queries.GetVisitsByDateQuery;
using DreamLuso.Application.CQ.PropertyVisits.Queries.GetVisitsByUserIQuery;
using DreamLuso.Application.CQ.PropertyVisits.Queries.RetrieveAvailableTimeSlotsQuery;
using DreamLuso.Application.CQ.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Endpoints;
public static class PropertyVisitEndpoints
{
    public static void RegisterPropertyVisitEndpoints(this IEndpointRouteBuilder routes)
    {
        var propertyVisits = routes.MapGroup("api/PropertyVisit");

        // Endpoint para verificar os horários disponíveis
        propertyVisits.MapGet("/availableSlots", Queries.GetAvailableTimeSlots)
            .WithName("AvailableSlots")
            .Produces<GetAvailableTimeSlotsQuery>(201)
            .Produces<Error>(400);

        // Endpoint para pegar as visitas por data
        propertyVisits.MapGet("/visitsByDate", Queries.GetVisitsByDate)
            .WithName("VisitsByDate")
            .Produces<IEnumerable<PropertyVisitResponse>>(200)
            .Produces<Error>(400);

        // Endpoint para confirmar visita
        propertyVisits.MapPut("/confirmVisit", Commands.ConfirmVisit)
            .WithName("ConfirmVisit")
            .Produces<ConfirmVisitResponse>(200)
            .Produces<Error>(400);

        propertyVisits.MapGet("/user/{userId}", Queries.GetVisitsByUserId)
        .WithName("VisitsByUserId")
        .Produces<IEnumerable<PropertyVisitResponse>>(200)
        .Produces<Error>(400);

        propertyVisits.MapPut("/cancelVisit", Commands.CancelVisit)
        .WithName("CancelVisit")
        .Produces<CancelVisitResponse>(200)
        .Produces<Error>(400);
    }

    private static class Queries
    {
        public static async Task<Results<Ok<GetAvailableTimeSlotsResponse>, BadRequest<Error>>> GetAvailableTimeSlots(
            [FromServices] ISender sender,
            [FromQuery] Guid propertyId,
            [FromQuery] string date)
        {
            var query = new GetAvailableTimeSlotsQuery
            {
                PropertyId = propertyId,
                Date = date
            };

            var result = await sender.Send(query);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.BadRequest(result.Error);
        }

        public static async Task<Results<Ok<IEnumerable<PropertyVisitResponse>>, BadRequest<Error>>> GetVisitsByDate(
            [FromServices] ISender sender,
            [FromQuery] Guid propertyId,
            [FromQuery] string date)
        {
            var query = new GetVisitsByDateQuery(propertyId, date);
            var result = await sender.Send(query);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.BadRequest(result.Error);
        }
        public static async Task<Results<Ok<IEnumerable<PropertyVisitResponse>>, BadRequest<Error>>> GetVisitsByUserId(
        [FromServices] ISender sender,
        [FromRoute] Guid userId)
        {
            var query = new GetVisitsByUserIdQuery(userId);
            var result = await sender.Send(query);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.BadRequest(result.Error);
        }
    }

    private static class Commands
    {
        public static async Task<Results<Ok<ConfirmVisitResponse>, BadRequest<Error>>> ConfirmVisit(
            [FromServices] ISender sender,
            [FromBody] ConfirmVisitCommand command)
        {
            var result = await sender.Send(command);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.BadRequest(result.Error);
        }
        public static async Task<Results<Ok<CancelVisitResponse>, BadRequest<Error>>> CancelVisit(
       [FromServices] ISender sender,
       [FromBody] CancelVisitCommand command)
        {
            var result = await sender.Send(command);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.BadRequest(result.Error);
        }
    }
}
