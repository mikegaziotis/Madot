using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Refit;
using ZLogger;

namespace Madot.Interface.CLI.CommandHandlers;

public interface ICommandArgs;

public interface ICommandHandler<in TCommandArgs> where TCommandArgs : ICommandArgs
{
    Task Handle(TCommandArgs args);
}

public abstract class BaseCommandHandler<TCommandArgs>(
    ILogger logger): ICommandHandler<TCommandArgs> where TCommandArgs: ICommandArgs
{
    public abstract Task Handle(TCommandArgs args);

    private void LogException(Exception ex)
    {
        logger.LogError(ex.Message,ex.InnerException??ex);
    }
    protected async Task<T?> SafeHttpExecuteAsync<T>(Func<Task<T>> func)
    {
        try
        {
            return await func();
        }
        catch (Exception ex)
        {
            switch (ex)
            {
                case HttpRequestException httpRequestException:
                    LogException(ex);
                    Console.WriteLine(httpRequestException.Message);
                    throw;
                case ApiException apiException:
                    if (apiException.StatusCode != HttpStatusCode.NotFound)
                    {
                        LogException(ex);
                        Console.WriteLine(apiException.Message);
                        throw;
                    }
                    break;
            }
        }

        return default;
    }

    protected JsonSerializerOptions DefaultSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
    }
}