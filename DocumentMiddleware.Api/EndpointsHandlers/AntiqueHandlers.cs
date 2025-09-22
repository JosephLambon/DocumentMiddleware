using AutoMapper;
using DocumentMiddleware.Api.Services;
using DocumentMiddleware.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DocumentMiddleware.Api.EndpointsHandlers;
public static class AntiqueHandlers
{
    public static async Task<Results<CreatedAtRoute<AntiqueDto>,BadRequest<string>, StatusCodeHttpResult>> CreateAntiqueAsync(
        DocumentDbContext documentDbContext,
        IMapper mapper,
        [FromForm] AntiqueDto antiqueToCreate,
        ILogger<AntiqueDto> logger,
        IFileService fileService
    )
    {
        logger.LogInformation("Creating antique...");
        // SAVE THE FILE CODE TO GO HERE!

        try
        {
            if (antiqueToCreate.ImageFile?.Length > 1 * 1024 * 1024)
            {
                return TypedResults.BadRequest("File size should not exceed 1 MB");
            }
            string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
            string createdImageName = await fileService.UploadFileAsync(antiqueToCreate.ImageFile, allowedFileExtentions);

            var antiqueEntity = mapper.Map<Antique>(antiqueToCreate);

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
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            // .NET 9.0 introduces TypedResults.InternalServerError (could return ex.Message)
            // ^ Scope for future improvement
            return TypedResults.StatusCode(500);
        }


    }
}