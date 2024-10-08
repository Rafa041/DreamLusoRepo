﻿using DreamLuso.Data.Context;
using DreamLuso.Data.Infrastructure;
using DreamLuso.Data.Interceptors;
using DreamLuso.Data.Repository;
using DreamLuso.Data.Uow;
using DreamLuso.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DreamLuso.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Interceptors and Context
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(configuration.GetConnectionString("DreamLusoCS"));
        });

        //Repository
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IRealStateAgentRepository, RealStateAgentRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IFileStorageService, FileStorageService>();//Image => Service.

        return services;
    }
   
}
