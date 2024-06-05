using Badop.Core.Application.Operations;
using Badop.Core.Application.Operations.Commands;

namespace Badop.Core.Application.Operations
{

    public interface IOperation;

    public interface IOperationHandler<in TOperation, TResponse> where TOperation : IOperation;

    public interface IOperationHandler<in TOperation> where TOperation : IOperation;
}

namespace Badop.Core.Application.Operations.Queries
{
    public interface IQuery : IOperation;

    public interface IQueryHandler<in TQuery, TResponse> : IOperationHandler<TQuery, TResponse> where TQuery : IQuery
    {
        Task<TResponse> Handle(TQuery query);
    }
}


namespace Badop.Core.Application.Operations.Commands
{
    public interface ICommand : IOperation;

    public interface ICommandHandler<in TCommand, TResponse> : IOperationHandler<TCommand, TResponse> where TCommand : ICommand
    {
        Task<TResponse> Handle(TCommand command);
    }
    
    public interface ICommandHandler<in TCommand>: IOperationHandler<TCommand> where TCommand: ICommand
    {
        Task Handle(TCommand command);
    }
}




