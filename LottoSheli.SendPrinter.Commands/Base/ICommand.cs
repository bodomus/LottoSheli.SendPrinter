using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Base
{
    /// <summary>
    /// Basic interface for the command
    /// </summary>
    public interface ICommand
    {
        bool CanExecute();
    }

    /// <summary>
    /// Basic interface for the executable command
    /// </summary>
    public interface IAsyncExecutableCommand : ICommand
    {
        Task Execute();
    }

    /// <summary>
    /// Basic interface for the executable command
    /// </summary>
    public interface IExecutableCommand : ICommand
    {
        void Execute();
    }

    /// <summary>
    /// Basic interface for command without parameters and with result
    /// </summary>
    /// <typeparam name="TResult">Command execution result</typeparam>
    public interface IResultCommand<TResult> : ICommand
    {
        TResult Execute();
    }

    /// <summary>
    /// Basic interface for the parametrized command
    /// </summary>
    /// <typeparam name="TCommandData">Command data.</typeparam>
    public interface IParametrizedCommand<TCommandData> : ICommand 
    where TCommandData : ICommandData
    {
        void Execute(TCommandData data);
    }

    /// <summary>
    /// Basic interface for the parametrized command and result
    /// </summary>
    /// <typeparam name="TCommandData">Command data.</typeparam>
    /// <typeparam name="TResult">Command execution result</typeparam>
    public interface IParametrizedWithResultCommand<TCommandData, TResult> : ICommand
        where TCommandData : ICommandData
    {
        TResult Execute(TCommandData data);
    }
}
