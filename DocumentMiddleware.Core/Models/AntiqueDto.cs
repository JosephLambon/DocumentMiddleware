using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DocumentMiddleware.Core.Models;
public class AntiqueDto
{
    public string? Name { get; set; }
    
    public IFormFile? ImageFile { get; set; }
}

public class AntiqueUpdateDTO
{
    public int Id { get; set; }
    
    public string? ProductName { get; set; }
    
    public string? ProductImage { get; set; }

    public IFormFile? ImageFile { get; set; }
}