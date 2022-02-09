using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PdfFlow.Maps;
using PdfFlow.Models;

namespace PdfFlow.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<PdfModel> PdfModels { get; set; }
    public DbSet<LogoModel> LogoModels { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        new DbMap(builder.Entity<LogoModel>());
        new DbMap(builder.Entity<PdfModel>());
    }
}