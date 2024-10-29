using Azure.Core;
using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Properties.Commands.CreateProperty;
using DreamLuso.Application.CQ.Properties.Queries.GetTotalSales;
using DreamLuso.Application.CQ.Properties.Queries.Retrieve;
using DreamLuso.Application.CQ.Properties.Queries.RetrieveAll;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Application.CQ.Users.Queries.RetrieveAllUsers;
using DreamLuso.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Endpoints;

public static class PropertyEndpoints
{
    
    public static void RegisterPropertyEndpoints(this IEndpointRouteBuilder routes)
    {
        var properties = routes.MapGroup("api/property");

        // ========== Register Property Endpoint ==========
        //properties.MapPost("/register", Queries.RegisterProperty)
        //    .WithName("RegisterProperty")
        //    .Produces<CreatePropertyResponse>(200)
        //    .Produces<Error>(400)
        //    .DisableAntiforgery(); // Desabilitar proteção contra CSRF, se necessário

        // ========== Get Total Sales Endpoint ==========
        properties.MapGet("/GetTotalSales", Queries.GetTotalSales)
            .WithName("GetTotalSales")
            .Produces<GetTotalSalesResponse>(200)
            .Produces<Error>(404);

        // ========== Get Property by ID Endpoint ==========
        properties.MapGet("/retrieve/{id:guid}", Queries.RetrievePropertyId)
            .WithName("Retrieve")
            .Produces<RetrievePropertyResponse>(200)
            .Produces<Error>(404);
        // ========== Retrieve All Users Endpoint ==========
        properties.MapGet("/retrieveall", Queries.RetrieveAllProperties)
            .WithName("RetrieveAllProperties")
            .Produces<List<RetrieveAllPropertiesResponse>>(200)
            .Produces<Error>(404);
    }
    private static class Queries
    {
       // // ========== Register Property ==========
       // public static async Task<Results<Ok<CreatePropertyResponse>, BadRequest<Error>>> RegisterProperty(
       //[FromServices] ISender sender,
       //[FromForm] CreatePropertyCommand command,
       //CancellationToken cancellationToken)
       // {
       //     // Inicializa a lista de URLs de imagens
       //     command.ImageUrls = new List<string>();

       //     if (command.Images != null && command.Images.Any())
       //     {
       //         // Define o caminho da pasta onde as imagens serão salvas
       //         var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PropertyImages");
       //         if (!Directory.Exists(folderPath))
       //             Directory.CreateDirectory(folderPath); // Cria a pasta se não existir


       //         foreach (var image in command.Images)
       //         {
       //             if (image != null && image.Length > 0)
       //             {
       //                 // Gera um nome único para a imagem usando GUID e sua extensão
       //                 var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
       //                 var filePath = Path.Combine(folderPath, fileName);

       //                 // Salva o arquivo na pasta designada
       //                 using (var stream = new FileStream(filePath, FileMode.Create))
       //                 {
       //                     await image.CopyToAsync(stream);
       //                 }

       //                 // Adiciona o caminho da URL da imagem à lista de URLs
       //                 command.ImageUrls.Add($"/PropertyImages/{fileName}");
       //             }
       //         }
       //     }

       //     // Envia o comando para o Mediator
       //     var result = await sender.Send(command);

       //     // Retorna o resultado
       //     return result.IsSuccess
       //         ? TypedResults.Ok(result.Value)
       //         : TypedResults.BadRequest(result.Error);
       // }
        // ========== Get Total Sales ==========
        public static async Task<Results<Ok<GetTotalSalesResponse>, NotFound<Error>>> GetTotalSales(
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetTotalSalesQuery(), cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.NotFound(result.Error);
        }
        // ========== Retrieve Property by ID ==========
        public static async Task<Results<Ok<RetrievePropertyResponse>, NotFound<Error>>> RetrievePropertyId(
            [FromRoute] Guid id,
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var query = new RetrievePropertyQuery { Id = id };
            var result = await sender.Send(query, cancellationToken);

            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.NotFound(result.Error);
        }
        // ========== Retrieve All Users ==========
        public static async Task<Results<Ok<IEnumerable<RetrievePropertyResponse>>, NotFound<Error>>> RetrieveAllProperties(
    [FromServices] ISender sender,
    CancellationToken cancellationToken)
        {
            var result = await sender.Send(new RetrieveAllPropertiesQuery(), cancellationToken);

            return result.IsSuccess
                ? TypedResults.Ok(result.Value.Properties)  // Retornar a lista de propriedades
                : TypedResults.NotFound(result.Error);  
        }
    }
}
