using AutoMapper;
using Madot.Interface.WebAPI.DTOs.Requests;
using Madot.Interface.WebAPI.DTOs.Responses;
using Api = Madot.Interface.WebAPI.DTOs.Responses.Api;
using ApiVersion = Madot.Interface.WebAPI.DTOs.Responses.ApiVersion;
using AppCommonPage = Madot.Interface.WebAPI.DTOs.Responses.AppCommonPage;
using File = Madot.Interface.WebAPI.DTOs.Responses.File;
using FileLink = Madot.Interface.WebAPI.DTOs.Responses.FileLink;
using Guide = Madot.Interface.WebAPI.DTOs.Responses.Guide;
using GuideVersion = Madot.Interface.WebAPI.DTOs.Responses.GuideVersion;


namespace Madot.Interface.WebAPI.Automapper;

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
        //Common
        exp.CreateMap<FileLinkItem, Madot.Core.Application.Operations.FileLinkItem>();
        exp.CreateMap<GuideVersionItem, Madot.Core.Application.Operations.GuideVersionItem>();
        exp.CreateMap<FileItem, Madot.Core.Application.Operations.FileItem>();
        exp.CreateMap<Core.Domain.Models.ApiVersionGuideVersion, GuideVersionItem>();
        exp.CreateMap<Core.Domain.Models.ApiVersionFile, FileItem>();
        //Responses
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
        exp.CreateMap<Core.Domain.Models.AppCommonPage, AppCommonPage>();
        exp.CreateMap<Core.Domain.Models.AppCommonPage, AppCommonPageLite>();
        //Commands
        exp.CreateMap<ApiInsertCommand, Core.Application.Operations.Commands.ApiInsertCommand>();
        exp.CreateMap<ApiUpdateCommand, Core.Application.Operations.Commands.ApiUpdateCommand>();
        exp.CreateMap<ApiVersionInsertCommand, Core.Application.Operations.Commands.ApiVersionInsertCommand>();
        exp.CreateMap<ApiVersionUpdateCommand, Core.Application.Operations.Commands.ApiVersionUpdateCommand>();
        exp.CreateMap<AppCommonPageInsertCommand, Core.Application.Operations.Commands.AppCommonPageInsertCommand>();
        exp.CreateMap<AppCommonPageUpdateCommand, Core.Application.Operations.Commands.AppCommonPageUpdateCommand>();
        exp.CreateMap<VersionedDocumentInsertCommand, Core.Application.Operations.Commands.VersionedDocumentInsertCommand>();
        exp.CreateMap<VersionedDocumentUpdateCommand, Core.Application.Operations.Commands.VersionedDocumentUpdateCommand>();
        exp.CreateMap<FileInsertCommand, Core.Application.Operations.Commands.FileInsertCommand>();
        exp.CreateMap<FileUpdateCommand, Core.Application.Operations.Commands.FileUpdateCommand>();
    }

    

    
        
    
}