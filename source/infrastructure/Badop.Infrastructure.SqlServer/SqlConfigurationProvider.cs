using Badop.Core.Application.Providers;
using Badop.Core.Domain.Enums;
using Badop.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using File = Badop.Core.Domain.Models.File;
using OperatingSystem = Badop.Core.Domain.Enums.OperatingSystem;

namespace Badop.Infrastructure.SqlServer;

public class SqlConfigurationProvider: IDatabaseConfigurationProvider
{
    public void ConfigureDbContextOptions(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer("server=localhost,1433;database=Badop;user=sa;password=Password123!;Encrypt=false;")
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    public void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<Api>(entity =>
        {
            entity
                .ToTable("Api")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("Api_History", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.Id)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.BaseUrlPath)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastModifiedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.OrderId).HasDefaultValue(1);
        });

        modelBuilder.Entity<ApiVersion>(entity =>
        {
            entity
                .ToTable("ApiVersion")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("ApiVersion_History", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.HasIndex(e => new { e.ApiId, e.MajorVersion, e.MinorVersion }, "CK_ApiVersion_NameMajorMinor").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ApiId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.BuildOrReleaseTag)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ChangelogId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.HomepageId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastModifiedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.OpenApiSpecId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Api).WithMany(p => p.ApiVersions)
                .HasForeignKey(d => d.ApiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApiVersion_Api");

            entity.HasOne(d => d.Changelog).WithMany(p => p.ApiVersionChangelogs)
                .HasForeignKey(d => d.ChangelogId)
                .HasConstraintName("FK_ApiVersion_ChangeLog");

            entity.HasOne(d => d.Homepage).WithMany(p => p.ApiVersionHomepages)
                .HasForeignKey(d => d.HomepageId)
                .HasConstraintName("FK_ApiVersion_Documentation");

            entity.HasOne(d => d.OpenApiSpec).WithMany(p => p.ApiVersionOpenApiSpecs)
                .HasForeignKey(d => d.OpenApiSpecId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApiVersion_OpenApiSpec");
        });

        modelBuilder.Entity<ApiVersionFile>(entity =>
        {
            entity.HasKey(e => new { e.ApiVersionId, e.FileId });

            entity
                .ToTable("ApiVersionFile")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("ApiVersionFile_History", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.ApiVersionId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FileId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.ApiVersion).WithMany(p => p.ApiVersionFiles)
                .HasForeignKey(d => d.ApiVersionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApiVersionFile_ApiVersion");

            entity.HasOne(d => d.File).WithMany(p => p.ApiVersionFiles)
                .HasForeignKey(d => d.FileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApiVersionFile_File");
        });

        modelBuilder.Entity<ApiVersionGuideVersion>(entity =>
        {
            entity.HasKey(e => new { e.ApiVersionId, e.GuideVersionId });

            entity
                .ToTable("ApiVersionGuideVersion")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("ApiVersionGuideVersion_History", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.ApiVersionId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.GuideVersionId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.ApiVersion).WithMany(p => p.ApiVersionGuideVersions)
                .HasForeignKey(d => d.ApiVersionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApiVersionGuideVersion_ApiVersion");

            entity.HasOne(d => d.GuideVersion).WithMany(p => p.ApiVersionGuideVersions)
                .HasForeignKey(d => d.GuideVersionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApiVersionGuideVersion_GuideVersion");
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity
                .ToTable("File")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("File_History", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ApiId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.LastModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastModifiedDate).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Api).WithMany(p => p.Files)
                .HasForeignKey(d => d.ApiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_File_Api");
        });

        modelBuilder.Entity<FileLink>(entity =>
        {
            entity.HasKey(e => new { e.FileId, e.OperatingSystem, e.ChipArchitecture });

            entity
                .ToTable("FileLink")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("FileLink_History", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.FileId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OperatingSystem)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasConversion(new EnumToStringConverter<OperatingSystem>());
            entity.Property(e => e.ChipArchitecture)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasConversion(new EnumToStringConverter<ChipArchitecture>());
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.DownloadUrl)
                .IsUnicode(false)
                .HasColumnName("DownloadURL");
            entity.Property(e => e.LastModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastModifiedDate).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.File).WithMany(p => p.FileLinks)
                .HasForeignKey(d => d.FileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FileLink_File");
        });

        modelBuilder.Entity<Guide>(entity =>
        {
            entity
                .ToTable("Guide")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("Guide_History", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.HasIndex(e => new { e.ApiId, e.Title }, "CK_Guide_Title").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ApiId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.LastModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastModifiedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Api).WithMany(p => p.Guides)
                .HasForeignKey(d => d.ApiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Guide_Api");
        });

        modelBuilder.Entity<GuideVersion>(entity =>
        {
            entity
                .ToTable("GuideVersion")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("GuideVersion_History", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.HasIndex(e => new { e.GuideId, e.Iteration }, "UC_GuideVersion_Iteration").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Data).IsUnicode(false);
            entity.Property(e => e.GuideId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastModifiedDate).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Guide).WithMany(p => p.GuideVersions)
                .HasForeignKey(d => d.GuideId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GuideVersion_Guide");
        });

        modelBuilder.Entity<VersionedDocument>(entity =>
        {
            entity
                .ToTable("VersionedDocument")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("VersionedDocument_History", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.HasIndex(e => new { e.ApiId, e.DocumentType, e.Iteration }, "UC_VersionedDocument_Iteration").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ApiId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Data).IsUnicode(false);
            entity.Property(e => e.DocumentType)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasConversion(new EnumToStringConverter<VersionedDocumentType>());
            entity.Property(e => e.LastModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastModifiedDate).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Api).WithMany(p => p.VersionedDocuments)
                .HasForeignKey(d => d.ApiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VersionedDocument_Api");
        });
    }
}