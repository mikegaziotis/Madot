using Badop.Core.Application;
using Badop.Core.Application.Operations.Queries.Api;
using Badop.Core.Application.Operations.Queries.ApiVersionFile;
using Badop.Core.Application.Operations.Queries.File;
using Badop.Core.Domain.Enums;
using Badop.Infrastructure.SqlServer;
using OperatingSystem = Badop.Core.Domain.Enums.OperatingSystem;

namespace Badop.IntegrationTests.CQRS_Handlers;

public class FileTests
{
    [Fact]
    public async void GetFileById()
    {
        //Arrange
        const string fileId = "TFOSM231MS";
        var queryHandler = new FileGetByIdQueryHandler(new BadopDbContext(new SqlConfigurationProvider()));
        var query = new FileGetByIdQuery(fileId);

        //Act
        var result = await queryHandler.Handle(query);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(fileId,result.Id);
    }
    
    [Fact]
    public async void GetFilesByApiId()
    {
        //Arrange
        const string apiId = "petstore";
        var queryHandler = new FileGetByApiIdQueryHandler(new BadopDbContext(new SqlConfigurationProvider()));
        var query = new FileGetByApiIdQuery(apiId);

        //Act
        var result = await queryHandler.Handle(query);
        
        //Assert
        Assert.Contains("Postman",result.Select(x => x.DisplayName));
    }
    
    [Fact]
    public async void GetFilesByApiVersionId()
    {
        //Arrange
        const string apiVersionId = "SUDB9DH27W";
        var queryHandler = new FileGetByApiVersionIdQueryHandler(new BadopDbContext(new SqlConfigurationProvider()));
        var query = new FileGetByApiVersionIdQuery(apiVersionId,OperatingSystem.Linux,ChipArchitecture.Arm64);

        //Act
        var result = await queryHandler.Handle(query);
        
        //Assert
        Assert.Contains("Postman",result.Select(x => x.DisplayName));
    }
}