namespace Badop.Shell.API.Handlers;

public interface IRequest {}

public interface IHandler<TRequest, TResult> where TRequest : IRequest where TResult : IResult
{
    Task<IResult> Handle(TRequest request);
}; 