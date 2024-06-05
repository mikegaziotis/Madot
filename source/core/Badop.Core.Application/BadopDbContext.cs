using Badop.Core.Application.Providers;
using Badop.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using File = Badop.Core.Domain.Models.File;

namespace Badop.Core.Application;

public class BadopDbContext(
    IDatabaseConfigurationProvider configurationProvider) : DbContext
{

    public virtual DbSet<Api> Apis { get; set; }

    public virtual DbSet<ApiVersion> ApiVersions { get; set; }

    public virtual DbSet<ApiVersionGuideVersion> ApiVersionGuideVersions { get; set; }

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
