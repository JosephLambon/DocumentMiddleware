using DocumentMiddleware.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace DocumentMiddleware.Api.EndpointsHandlers;
public static class AntiqueHandlers
{
    public static async Task<CreatedAtRoute<AntiqueDto>> CreateAntiqueAsync(
        DocumentDbContext documentDbContext,
        IMapper mapper,
        [FromBody] AntiqueDto antiqueToCreate,
        ILogger<AntiqueDto> logger
    )
    {
        logger.LogInformation("Creating antique...");
        var antiqueEntity = mapper.Map<Antique>(antiqueToCreate);

        // SAVE THE FILE CODE TO GO HERE!
        
        
        
        
        
        var antiqueToReturn = mapper.Map<AntiqueDto>(antiqueEntity);
        
        documentDbContext.Antiques.Add(antiqueEntity);
        await documentDbContext.SaveChangesAsync();
        
        // Need to add a valid routeName and routeValues
        return TypedResults.CreatedAtRoute(
            antiqueToReturn,
            null,
            null
        );

    }
}