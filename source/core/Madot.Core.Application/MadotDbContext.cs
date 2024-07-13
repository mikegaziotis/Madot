using Madot.Core.Application.Providers;
using Madot.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using File = Madot.Core.Domain.Models.File;

namespace Madot.Core.Application;

public class MadotDbContext(
    IDatabaseConfigurationProvider configurationProvider) : DbContext
{

    public virtual DbSet<Api> Apis { get; set; }

    public virtual DbSet<ApiVersion> ApiVersions { get; set; }

    public virtual DbSet<ApiVersionGuideVersion> ApiVersionGuideVersions { get; set; }

    public virtual DbSet<AppCommonPage> AppCommonPages { get; set; }
    public virtual DbSet<File> Files { get; set; }
    
    public virtual DbSet<FileLink> FileLinks { get; set; }
    
    public virtual DbSet<ApiVersionFile> ApiVersionFiles { get; set; }

    public virtual DbSet<Guide> Guides { get; set; }

    public virtual DbSet<GuideVersion> GuideVersions { get; set; }

    public virtual DbSet<VersionedDocument> VersionedDocuments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        configurationProvider.ConfigureDbContextOptions(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        configurationProvider.BuildModel(modelBuilder);
    }
}
