using Madot.Core.Application;
using Madot.Core.Application.Operations.Queries.Api;
using Madot.Infrastructure.SqlServer;

namespace Madot.IntegrationTests.CQRS_Handlers;

public class ApiTests
{
    [Fact]
    public async void GetApi()
    {
        //Arrange
        string apiId = "petstore";
        var queryHandler = new ApiGetByIdQueryHandler(new MadotDbContext(new SqlConfigurationProvider()));
        var query = new ApiGetByIdQuery(apiId);

        //Act
        var result = await queryHandler.Handle(query);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(apiId,result.Id);
    }
    
    [Fact]
    public async void GetAllApis()
    {
        //Arrange
        string apiId = "petstore";
        var queryHandler = new ApiGetAllQueryHandler(new MadotDbContext(new SqlConfigurationProvider()));
        var query = new ApiGetAllQuery(false);

        //Act
        var result = await queryHandler.Handle(query);
        
        //Assert
        Assert.NotNull(result);
        Assert.Contains(apiId, result.Select(x => x.Id));
    }
}