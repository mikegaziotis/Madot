using System.Globalization;

namespace Madot.Interface.BlazorWebUI;

public static class AppSettings
{
    public const string PreviewKeyword = "preview-latest";
    public static string ApiInternalUrl=String.Empty;
    public static string ApiPublicUrl=String.Empty;

    public static void LoadConfiguration(this IServiceCollection services, ConfigurationManager conf)
    {
        conf.AddJsonFile("appsettings.json", true, true);
        switch (Environment.GetEnvironmentVariable("DEPLOY_TARGET"))
        {
            case "Local":
                Console.WriteLine("We are LOCAL!");
                conf.AddJsonFile("appsettings.Local.json", true, true);
                break;
            case "Docker":
                Console.WriteLine("We are DOCKER!");
                conf.AddJsonFile("appsettings.Docker.json", true, true);
                break;
            default:
                throw new NotImplementedException("Why you here?");
        }

        ApiInternalUrl = conf.GetRequiredSection("BackendWebAPI").GetValue<string>("InternalUrl")??throw new ArgumentException("missing BaseUrl");
        ApiPublicUrl = conf.GetRequiredSection("BackendWebAPI").GetValue<string>("PublicUrl") ?? ApiInternalUrl;
    }
}