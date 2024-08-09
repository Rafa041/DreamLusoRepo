using Cqrs.Domain;
using Cqrs.Hosts;
using DreamLuso.Application.CQ.Users.Queries.GetAllUsers;
using DreamLuso.Data.Repository;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.IoC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Em Teste

builder.Services.AddIoCServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
