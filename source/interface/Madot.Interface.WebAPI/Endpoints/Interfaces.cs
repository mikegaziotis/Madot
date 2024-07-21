namespace Madot.Interface.WebAPI.Endpoints;

public interface IRequest {}

public interface IEndpoint<TRequest, TResult> where TRequest : IRequest where TResult : IResult
{
    Task<IResult> Handle(TRequest request);
}; 