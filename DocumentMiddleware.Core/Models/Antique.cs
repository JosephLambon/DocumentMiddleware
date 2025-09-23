namespace DocumentMiddleware.Core.Models;

public enum Status
{
    Available,
    Sold,
    Archived
}

public class Antique
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public Status? Status { get; set; } = Models.Status.Available;

    public string? Thumbnail { get; set; }

    public string[]? Images { get; set; }
}