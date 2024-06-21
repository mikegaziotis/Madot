using Badop.Core.Application;
using Badop.Core.Application.Operations.Queries.Api;
using Badop.Core.Application.Operations.Queries.ApiVersion;
using Badop.Infrastructure.SqlServer;

namespace Badop.IntegrationTests.CQRS_Handlers;

public class ApiVersionTests
{
    [Fact]
    public async void GetApiVersion()
    {
        //Arrange
        const string apiVersionId = "SUDB9DH27W";
        var queryHandler = new ApiVersionGetByIdQueryHandler(new BadopDbContext(new SqlConfigurationProvider()));
        var query = new ApiVersionGetByIdQuery(apiVersionId);

        //Act
        var result = await queryHandler.Handle(query);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(apiVersionId,result.Id);
    }
    
    [Fact]
    public async void GetLatestApiVersion()
    {
        //Arrange
        const string apiId = "petstore";
        var queryHandler = new ApiVersionGetLatestByApiIdQueryHandler(new BadopDbContext(new SqlConfigurationProvider()));
        var query = new ApiVersionGetLatestByApiIdQuery(apiId);

        //Act
        var result = await queryHandler.Handle(query);
        
        //Assert
        Assert.Equal("SUDB9DH27W",result.Id);
    }
    
    [Fact]
    public async void GetAllApiVersions()
    {
        //Arrange
        string apiId = "petstore";
        var queryHandler = new ApiVersionGetByApiIdQueryHandler(new BadopDbContext(new SqlConfigurationProvider()));
        var query = new ApiVersionGetByApiIdQuery(apiId);

        //Act
        var result = await queryHandler.Handle(query);
        
        //Assert
        Assert.NotNull(result);
        Assert.Contains(apiId, result.Select(x => x.ApiId));
    }
}