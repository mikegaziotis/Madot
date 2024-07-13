using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Providers;

public interface IDatabaseConfigurationProvider
{
    void ConfigureDbContextOptions(DbContextOptionsBuilder optionsBuilder);
    void BuildModel(ModelBuilder builder);
}