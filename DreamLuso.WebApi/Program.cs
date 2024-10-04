using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Accounts.Commands.UpdateAccount;
using DreamLuso.Application.CQ.Accounts.Queries.Login;
using DreamLuso.Application.CQ.Addresses.Commands.CreateAddress;
using DreamLuso.Application.CQ.Addresses.Commands.UpdateAddress;
using DreamLuso.Application.CQ.Addresses.Queries.Retrieve;
using DreamLuso.Application.CQ.Addresses.Queries.RetrieveAllAddress;
using DreamLuso.Application.CQ.Properties.Commands.CreateProperty;
using DreamLuso.Application.CQ.RealStateAgents.Commands.CreateRealStateAgent;
using DreamLuso.Application.CQ.RealStateAgents.Commands.UpdateRealStateAgent;
using DreamLuso.Application.CQ.RealStateAgents.Queries.Retrieve;
using DreamLuso.Application.CQ.RealStateAgents.Queries.RetrieveAll;
using DreamLuso.Application.CQ.Users.Commands.CreateUser;
using DreamLuso.Application.CQ.Users.Commands.UpdateUser;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Application.CQ.Users.Queries.RetrieveAllUsers;
using DreamLuso.IoC;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIoCServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();


var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseStaticFiles(); // Isso habilita o serviço de arquivos estáticos
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/uploads"
});

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAngularApp");


 //========== Users Endpoints ==========
app.MapPost("/api/user/register", async ([FromBody] CreateUserCommand command, ISender sender, CancellationToken cancellationToken) =>
{
    var result = await sender.Send(command, cancellationToken);
    return result.IsSuccess;
})
.WithName("RegisterUser")
.Produces<CreateUserResponse>(200)
.Produces<Error>(400);

app.MapGet("/api/user/retrieveall", async (ISender sender, CancellationToken cancellationToken) =>
{
    var result = await sender.Send(new RetrieveAllUsersQuery(), cancellationToken);
    return result;
})
.WithName("RetrieveAllUsers")
.Produces<List<RetrieveUserResponse>>(200)
.Produces<Error>(404);

app.MapPut("/api/user/{id}", async ([FromBody] UpdateUserCommand command, ISender sender ) =>
{
    var result = await sender.Send(command);
    return result.IsSuccess ? Results.Ok(result.IsSuccess) : Results.NotFound("User not found.");
})
.WithName("UpdateUser")
.Produces<UpdateUserResponse>(200)
.Produces<Error>(404);

app.MapGet("/api/user/{id}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
{
    var query = new RetrieveUserQuery { Id = id };
    var result = await sender.Send(query, cancellationToken);
    return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Error);
})
.WithName("RetrieveUser")
.Produces<RetrieveUserResponse>(200)
.Produces<Error>(404);

// ========== RealStateAgent Endpoints ==========
app.MapPost("/api/realstateagent/create", async ([FromBody] CreateRealStateAgentCommand command, ISender sender, CancellationToken cancellationToken) =>
{
    var result = await sender.Send(command, cancellationToken);
    return result.IsSuccess ? Results.Ok(result.IsSuccess) : Results.BadRequest(result.Error);
})
.WithName("CreateRealStateAgent")
.Produces<CreateRealStateAgentResponse>(200)
.Produces<Error>(400);

app.MapGet("/api/realstateagent/retrieveall", async (ISender sender, CancellationToken cancellationToken) =>
{
    var result = await sender.Send(new RetrieveAllRealStateAgentsQuery(), cancellationToken);
    return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Error);
})
.WithName("RetrieveAllRealStateAgents")
.Produces<List<RetrieveRealStateAgentResponse>>(200)
.Produces<Error>(404);

app.MapGet("/api/realstateagent/{id}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
{
    var query = new RetrieveRealStateAgentQuery { Id = id };
    var result = await sender.Send(query, cancellationToken);
    return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Error);
})
.WithName("RetrieveRealStateAgent")
.Produces<RetrieveRealStateAgentResponse>(200)
.Produces<Error>(404);

app.MapPut("/api/realstateagent/{id}", async ([FromBody] UpdateRealStateAgentCommand command, ISender sender, CancellationToken cancellationToken) =>
{
    var result = await sender.Send(command, cancellationToken);
    return result.IsSuccess ? Results.Ok(result.IsSuccess) : Results.NotFound("RealStateAgent not found.");
})
.WithName("UpdateRealStateAgent")
.Produces<UpdateRealStateAgentResponse>(200)
.Produces<Error>(404);

// ========== Auth Endpoints ==========
app.MapPost("/api/auth/login", async ([FromBody] LoginUserCommand command, ISender sender) =>
{
    var result = await sender.Send(command);
    return result.IsSuccess ? Results.Ok(new { Token = result.Value.Token, UserId = result.Value.Id }) 
    : Results.BadRequest(result.Error);
})
.WithName("Login")
.Produces(200)
.Produces(400);

app.MapPut("/api/auth/{id}", async ([FromBody] UpdateAccountPasswordCommand command, ISender sender ) =>
{
    var result = await sender.Send(command);
    return result != null ? Results.Ok(result.IsSuccess) : Results.NotFound("Account not found.");
})
.WithName("UpdateAccountPassword")
.Produces(200)
.Produces(404);

// ========== Address Endpoints ==========
app.MapGet("/api/address/retrieveall", async (ISender sender, CancellationToken cancellationToken) =>
{
    var result = await sender.Send(new RetrieveAllAddressQuery(), cancellationToken);
    return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Error);
})
.WithName("RetrieveAllAddresses")
.Produces(200)
.Produces(404);

app.MapGet("/api/address/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
{
    var query = new RetrieveAddressQuery { Id = id };  
    var result = await sender.Send(query, cancellationToken);
    return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Error);
})
.WithName("RetrieveAddress")
.Produces(200)
.Produces(404);

app.MapPost("/api/address/create", async ([FromBody] CreateAddressCommand command, ISender sender, CancellationToken cancellationToken) =>
{
    var result = await sender.Send(command, cancellationToken);
    return result.IsSuccess ? Results.Ok(result.IsSuccess) : Results.BadRequest(result.Error);
})
.WithName("CreateAddress")
.Produces(200)
.Produces(400);

app.MapPut("/api/address/{id:guid}", async ([FromBody] UpdateAddressCommand command, ISender sender) =>
{
    var result = await sender.Send(command);
    return result != null && result.IsSuccess ? Results.Ok(result.IsSuccess) : Results.NotFound("Address not found.");
})
.WithName("UpdateAddress")
.Produces(200)
.Produces(404);

// Run the application


// ========== Property Endpoints ==========
app.MapPost("/api/property/register", async ([FromBody] CreatePropertyCommand command, ISender sender, CancellationToken cancellationToken) =>
{
    var result = await sender.Send(command, cancellationToken);
    return result.IsSuccess ? Results.Ok(result.IsSuccess) : Results.BadRequest(result.Error);
})
.WithName("RegisterProperty")
.Produces<CreatePropertyResponse>(200)
.Produces<Error>(400);

app.Run();
