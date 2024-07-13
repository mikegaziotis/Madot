using System.Linq.Expressions;
using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Operations;
using Madot.Core.Application.Operations.Commands;
using Madot.Core.Domain;

namespace Madot.Core.Application.Operations
{

    public interface IOperation;

    public abstract class IOperationHandler<TOperation> where TOperation : IOperation
    {
        protected async Task<T> SafeDbExecuteAsync<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex);
            }
        }
    }
    
    public abstract class IOperationHandler<TOperation, TResponse> where TOperation : IOperation
    {
        protected async Task<T> SafeDbExecuteAsync<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex);
            }
        }
    }
}

namespace Madot.Core.Application.Operations.Queries
{
    public interface IQuery : IOperation;

    public abstract class IQueryHandler<TQuery, TResponse> : IOperationHandler<TQuery, TResponse> where TQuery : IQuery
    {
        public abstract Task<TResponse> Handle(TQuery query);
    }
}


namespace Madot.Core.Application.Operations.Commands
{
    public interface ICommand : IOperation;

    public abstract class ICommandHandler<TCommand, TEntity, TResponse> : IOperationHandler<TCommand, TResponse> 
        where TCommand : ICommand 
        where TEntity: IModel
    {
        public abstract Task<TResponse> Handle(TCommand command);
        
        public abstract TEntity GetEntity(TCommand command, TEntity? existingEntity);
    }
    
    public abstract class ICommandHandler<TCommand, TEntity>: IOperationHandler<TCommand> where TCommand: ICommand where TEntity: IModel
    {
        public abstract Task Handle(TCommand command);

        public abstract TEntity GetEntity(TCommand command, TEntity? existingEntity);
    }
}




