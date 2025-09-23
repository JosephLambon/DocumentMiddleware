using AutoMapper;
using DocumentMiddleware.Api.Services;
using DocumentMiddleware.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DocumentMiddleware.Api.EndpointsHandlers;
public static class AntiqueHandlers
{
    //public static async Task GetAntiqueById()
    //{
    //    return new NotImplementedException();
    //}

    public static async Task<Results<Ok<AntiqueForResponseDto>,BadRequest<string>, StatusCodeHttpResult>> CreateAntiqueAsync(
        DocumentDbContext documentDbContext,
        IMapper mapper,
        [FromForm] AntiqueForCreationDto antiqueToCreate,
        ILogger<Antique> logger,
        IFileService fileService
    )
    {
        logger.LogInformation("Creating antique...");

        try
        {
            if (antiqueToCreate.ThumbnailFile?.Length > 1 * 1024 * 1024)
            {
                return TypedResults.BadRequest("File size should not exceed 1 MB");
            }
            string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
            string createdImageName = await fileService.UploadFileAsync(antiqueToCreate.ThumbnailFile, allowedFileExtentions);

            var antiqueEntity = mapper.Map<Antique>(antiqueToCreate, opt =>
            {
                opt.Items["FileName"] = createdImageName;
            });

            var antiqueToReturn = mapper.Map<AntiqueForResponseDto>(antiqueEntity);
        
            documentDbContext.Antiques.Add(antiqueEntity);
            await documentDbContext.SaveChangesAsync();

            // Need to add a valid routeName and routeValues
            //return TypedResults.CreatedAtRoute(
            //    antiqueToReturn,
            //    null,
            //    null
            //);
            return TypedResults.Ok(antiqueToReturn); // TEMPORARY
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