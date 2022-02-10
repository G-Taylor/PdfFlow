using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PdfFlow.Models;

namespace PdfFlow.Maps;

/*
 * DB table and column names are stored in lowercase with Postgres, so this mapping class
 * is required to link each table and column with its C# camelcase equivalent
 */
public class DbMap
{
    public DbMap(EntityTypeBuilder<LogoModel> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(x => x.Id);
        entityTypeBuilder.ToTable("logomodels");

        entityTypeBuilder.Property(x => x.Id).HasColumnName("id");
        entityTypeBuilder.Property(x => x.Name).HasColumnName("name");
    }
    
    public DbMap(EntityTypeBuilder<PdfModel> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(x => x.Id);
        entityTypeBuilder.ToTable("pdfmodels");

        entityTypeBuilder.Property(x => x.Id).HasColumnName("id");
        entityTypeBuilder.Property(x => x.Name).HasColumnName("name");
        entityTypeBuilder.Property(x => x.AddressLine1).HasColumnName("addressline1");
        entityTypeBuilder.Property(x => x.AddressLine2).HasColumnName("addressline2");
        entityTypeBuilder.Property(x => x.Postcode).HasColumnName("postcode");
        entityTypeBuilder.Property(x => x.TextInput).HasColumnName("textinput");
        entityTypeBuilder.Property(x => x.FilePath).HasColumnName("filepath");
        entityTypeBuilder.Property(x => x.Logo).HasColumnName("logo");
    }
}