using AutoMapper;
using DocumentMiddleware.Core.Models;
using DocumentMiddleware.Api.Services;

namespace DocumentMiddleware.Api.Extensions;
public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<DocumentDbContext>();
        // services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IFileService, FileService>();
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                    ;
                });
        });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddProblemDetails();
        return services;
    }
}