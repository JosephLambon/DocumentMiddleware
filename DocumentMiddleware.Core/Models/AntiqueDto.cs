using Microsoft.AspNetCore.Http;

namespace DocumentMiddleware.Core.Models;
public class AntiqueForResponseDto
{
    public string? Name { get; set; }
    public Status? Status { get; set; }
    public string? Thumbnail { get; set; }
    public string[]? Images { get; set; }
}

public class AntiqueForCreationDto
{
    public string? Name { get; set; }

    public Status? Status { get; set; }

    public IFormFile? ThumbnailFile { get; set; }

    public IFormFile[]? ImageFiles { get; set; } = null;

}