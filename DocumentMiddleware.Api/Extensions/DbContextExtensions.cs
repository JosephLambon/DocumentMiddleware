using DocumentMiddleware.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentMiddleware.Api.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DocumentDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("default"));
        });
        return services;
    }
}