using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.PropertyVisits.Commands.Create;
using DreamLuso.Application.CQ.PropertyVisits.Queries.GetVisitsByDateQuery;
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

        // Endpoint para criar uma nova visita
        //propertyVisits.MapPost("/CreatePropertyVisit", Queries.CreatePropertyVisit)
        //    .WithName("CreatePropertyVisit")
        //    .Produces<CreatePropertyVisitResponse>(201)
        //    .Produces<Error>(400);

        propertyVisits.MapGet("/availableSlots", Queries.GetAvailableTimeSlots)
            .WithName("AvailableSlots")
            .Produces<GetAvailableTimeSlotsQuery>(201)
            .Produces<Error>(400);

        propertyVisits.MapGet("/visitsByDate", Queries.GetVisitsByDate)
        .WithName("VisitsByDate")
        .Produces<IEnumerable<PropertyVisitResponse>>(200)
        .Produces<Error>(400);
        //// Endpoint para obter uma visita por ID
        //propertyVisits.MapGet("/{visitId}", Queries.GetVisitById);

        //// Endpoint para obter todas as visitas de um usuário
        //propertyVisits.MapGet("/user/{userId}", Queries.GetUserVisits);

        //// Endpoint para obter todas as visitas de um agente
        //propertyVisits.MapGet("/agent/{agentId}", Queries.GetAgentVisits);

        //// Endpoint para cancelar uma visita
        //propertyVisits.MapDelete("/{visitId}", Queries.CancelVisit);
    }

    private static class Queries
    {
        //Endpoint para criar uma nova visita
        //public static async Task<Results<Ok<CreatePropertyVisitResponse>, BadRequest<Error>>> CreatePropertyVisit(
        //    [FromServices] ISender sender,
        //    [FromBody] CreatePropertyVisitCommand command)
        //{
        //    if (command == null)
        //    {
        //        return TypedResults.BadRequest(Error.PropertyVisitInvalid);
        //    }

        //    var result = await sender.Send(command);
        //    return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.BadRequest(result.Error);
        //}
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
        //// Endpoint para obter uma visita por ID
        //public static async Task<Results<Ok<PropertyVisitResponse>, NotFound>> GetVisitById(
        //    [FromServices] ISender sender,
        //    string visitId)
        //{
        //    var result = await sender.Send(new GetVisitByIdQuery(visitId));
        //    return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound();
        //}

        //// Endpoint para obter todas as visitas de um usuário
        //public static async Task<Results<Ok<IEnumerable<PropertyVisitResponse>>, NotFound>> GetUserVisits(
        //    [FromServices] ISender sender,
        //    string userId)
        //{
        //    var result = await sender.Send(new GetUserVisitsQuery(userId));
        //    return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound();
        //}

        //// Endpoint para obter todas as visitas de um agente
        //public static async Task<Results<Ok<IEnumerable<PropertyVisitResponse>>, NotFound>> GetAgentVisits(
        //    [FromServices] ISender sender,
        //    string agentId)
        //{
        //    var result = await sender.Send(new GetAgentVisitsQuery(agentId));
        //    return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound();
        //}

        //// Endpoint para cancelar uma visita
        //public static async Task<Results<NoContent, NotFound>> CancelVisit(
        //    [FromServices] ISender sender,
        //    string visitId)
        //{
        //    var result = await sender.Send(new CancelVisitCommand(visitId));
        //    return result.IsSuccess ? TypedResults.NoContent() : TypedResults.NotFound();
        //}
    }
}