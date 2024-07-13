namespace Madot.Shell.API.Endpoints;

public interface IRequest {}

public interface IMediator<TRequest, TResult> where TRequest : IRequest where TResult : IResult
{
    Task<IResult> Send(TRequest request);
}; 