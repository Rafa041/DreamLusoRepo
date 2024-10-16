using DreamLuso.IoC;
using DreamLuso.WebApi.Endpoints;
using DreamLuso.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
// Add services to the container

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandlers();
// Adiciona sua configuração JWT
builder.Services.AddJwtTokenAuthentication(builder.Configuration);

builder.Services.AddIoCServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
   
});


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.RegisterEndpoints();

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowAngularApp");
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/uploads"
});
// Mapeia o endpoint de tratamento de erros
app.Map("/error", (HttpContext context) =>
{
    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    var errorResponse = new ProblemDetails
    {
        Title = "An error occurred",
        Status = StatusCodes.Status500InternalServerError,
        Detail = exceptionHandlerPathFeature?.Error.Message,
    };
    return Results.Problem(errorResponse); // Retorna a resposta em formato ProblemDetails
});
// Run the application
app.Run();