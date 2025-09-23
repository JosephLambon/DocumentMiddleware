using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DocumentMiddleware.Core.Models;
public class AntiqueForResponseDto
{
    public string? Name { get; set; }
    
    public string? Thumbnail { get; set; }
}

public class AntiqueForCreationDto
{
    public string? Name { get; set; }
    
    public IFormFile? ImageFile { get; set; }
}

public class AntiqueUpdateDto
{
    public int Id { get; set; }
    
    public string? ProductName { get; set; }
    
    public string? ProductImage { get; set; }

    public IFormFile? ImageFile { get; set; }
}