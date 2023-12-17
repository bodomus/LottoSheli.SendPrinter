using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;
using System;

namespace LottoSheli.SendPrinter.Bootstraper
{
    /// <summary>
    /// Provides global objects factory
    /// </summary>
    public interface IAbstractObjectsFactory : IAsyncDisposable, IDisposable
    {
        /// <summary>
        /// Gets service of requested type
        /// </summary>
        /// <returns></returns>
        TService GetService<TService>();

        /// <summary>
        /// Gets configured repository factory
        /// </summary>
        /// <returns></returns>
        IRepositoryFactory GetRepositoryFactory();


        /// <summary>
        /// Gets configured logger factory
        /// </summary>
        /// <returns></returns>
        ILoggerFactory GetLoggerFactory();
    }
}
