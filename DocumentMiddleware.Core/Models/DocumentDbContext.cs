using Microsoft.EntityFrameworkCore;

namespace DocumentMiddleware.Core.Models;

public class DocumentDbContext : DbContext
{
    public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Antique> Antiques { get; set; }
}