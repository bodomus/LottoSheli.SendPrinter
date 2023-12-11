//using LottoSheli.SendPrinter.Commands.Base;
//using LottoSheli.SendPrinter.Commands.ReaderWorkflow;
//using LottoSheli.SendPrinter.Core;
//using LottoSheli.SendPrinter.Printer;
//using LottoSheli.SendPrinter.Remote;
using LottoSheli.SendPrinter.Repository;
using LottoSheli.SendPrinter.Settings;
//using LottoSheli.SendPrinter.SlipReader;
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


        /// <summary>
        /// Gets configured command factory.
        /// </summary>
        /// <returns></returns>
        //ICommandFactory GetCommandFactory();

        /// <summary>
        /// Gets configured printer factory.
        /// </summary>
        /// <returns></returns>
        //IPrinterFactory GetPrinterFactory();

        /// <summary>
        /// Gets configured settings factory.
        /// </summary>
        /// <returns></returns>
        ISettingsFactory GetSettingsFactory();

        /// <summary>
        /// Get factory for slip reader
        /// </summary>
        /// <returns></returns>
        //ISlipReaderFactory GetSlipReaderFactory();

        /// <summary>
        /// Get factory for recognition jobs and workers
        /// </summary>
        /// <returns></returns>
        //IRecognitionJobFactory GetRecognitionJobFactory();

        /// <summary>
        /// Get scan sending service
        /// </summary>
        /// <returns></returns>
        ///
        //ISendingQueue GetSendingQueue();

        /// <summary>
        /// Get shared sequence number provider
        /// </summary>
        /// <returns></returns>
        ///
        //ISequenceService GetSequenceService();

        /// <summary>
        /// Get factory producing session managers for various servers
        /// </summary>
        /// <returns></returns>
        ///
        //ISessionManagerFactory GetSessionManagerFactory();
    }
}
