using AutoMapper;
using Madot.Shell.API.DTOs.Responses;
using File = Madot.Shell.API.DTOs.Responses.File;

namespace Madot.Shell.API.Automapper;

public static class DependencyInjection
{
    public static void AddAutomapper(this IServiceCollection serviceCollection)
    {
        var config = new MapperConfiguration(MapProfile);

        var mapper = new Mapper(config);
        serviceCollection.AddSingleton<IMapper>(mapper);
    }

    private static void MapProfile(IMapperConfigurationExpression exp)
    {
        exp.CreateMap<Core.Domain.Models.Api, Api>();
        exp.CreateMap<Core.Domain.Models.ApiVersion, ApiVersion>();
        exp.CreateMap<Core.Domain.Models.VersionedDocument, Homepage>();
        exp.CreateMap<Core.Domain.Models.VersionedDocument, Changelog>();
        exp.CreateMap<Core.Domain.Models.VersionedDocument, OpenApiSpec>();
        exp.CreateMap<Core.Domain.Models.FileLink, FileLink>();
        exp.CreateMap<Core.Domain.Models.File, File>();
        exp.CreateMap<Core.Domain.Models.GuideVersion, GuideVersion>();
        exp.CreateMap<Core.Domain.Models.Guide, Guide>();
        exp.CreateMap<Core.Domain.OtherTypes.ApiVersionGuide, ApiVersionGuide>();
        exp.CreateMap<Core.Domain.Models.AppCommonPage, AppCommonPageLite>();
    }

    

    
        
    
}