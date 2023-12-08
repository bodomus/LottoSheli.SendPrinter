using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Base
{
    /// <summary>
    /// Provides methods for command executing.
    /// </summary>
    public interface ICommandFactory
    {
        /// <summary>
        /// Receives specified command without result and without paramters.
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        TCommand CreateCommand<TCommand>()
            where TCommand : ICommand;

        /// <summary>
        /// Executes specified command and returns executing result.
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TCommandData"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        TResult ExecuteCommand<TCommand, TCommandData, TResult>(TCommandData data)
            where TCommand : IParametrizedWithResultCommand<TCommandData, TResult>
            where TCommandData : ICommandData;

        /// <summary>
        /// Executes specified command with no result.
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TCommandData"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        void ExecuteCommand<TCommand, TCommandData>(TCommandData data)
            where TCommand : IParametrizedCommand<TCommandData>
            where TCommandData : ICommandData;

        /// <summary>
        /// Executes specified command without result and without paramters.
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        void ExecuteCommand<TCommand>()
            where TCommand : IExecutableCommand;

        /// <summary>
        /// Executes specified command without result and without paramters.
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        Task ExecuteCommandAsync<TCommand>()
            where TCommand : IAsyncExecutableCommand;

        /// <summary>
        /// Executes specified command with result and without paramters.
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        TResult ExecuteCommand<TCommand, TResult>()
            where TCommand : IResultCommand<TResult>;
    }
}
