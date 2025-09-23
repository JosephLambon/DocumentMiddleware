using Microsoft.AspNetCore.Http.Json;
using DocumentMiddleware.Core.Models;
using DocumentMiddleware.Api.Services;
using System.Text.Json.Serialization;

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

        // Defaults enum results to be returned as strings
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        // Same as above, needed for Swagger response to update. Result of conflict in MinimalAPI/Swashbuckle
        services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddProblemDetails();
        return services;
    }
}