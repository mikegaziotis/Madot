using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Providers;

public interface IDatabaseConfigurationProvider
{
    void ConfigureDbContextOptions(DbContextOptionsBuilder optionsBuilder);
    void BuildModel(ModelBuilder builder);
}