﻿using DreamLuso.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DreamLuso.Application;

public static class DependencyInjection
{
    //public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());

    //    services.AddValidatorsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

    //    services.AddMediatR(cfg =>
    //    {
    //        cfg.RegisterServicesFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
    //        cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
    //        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    //    });

    //    return services;
    //}


    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {

            cfg.RegisterServicesFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
        });

        //services.AddCors(options =>
        //{
        //    options.AddPolicy("AllowAngularOrigins",
        //    builder =>
        //    {
        //        builder.WithOrigins(
        //                            "http://localhost:4200"
        //                            )
        //                            .AllowAnyHeader()
        //                            .AllowAnyMethod();
        //    });
        //});

        return services;
    }
}

