using Badop.Shell.API.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.Data;

namespace Badop.Shell.API.Handlers;


public interface IHandler<in TRequest, TResponse> where TRequest: IRequest where TResponse: IResult
{
    Task<TResponse> Handle(TRequest request);
}