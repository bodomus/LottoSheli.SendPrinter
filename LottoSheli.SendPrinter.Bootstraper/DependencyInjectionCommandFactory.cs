using LottoSheli.SendPrinter.Commands.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Bootstraper
{
    class DependencyInjectionCommandFactory : ICommandFactory
    {
        private Func<IServiceProvider> _serviceProviderStrategy;
        public DependencyInjectionCommandFactory(Func<IServiceProvider> serviceProviderStrategy)
        {
            _serviceProviderStrategy = serviceProviderStrategy;
        }

        public TResult ExecuteCommand<TCommand, TCommandData, TResult>(TCommandData data)
            where TCommand : IParametrizedWithResultCommand<TCommandData, TResult>
            where TCommandData : ICommandData
        {
            var command = CreateCommand<TCommand>();

            if (!command.CanExecute())
            {
                throw new InvalidOperationException($"Command: '{typeof(TCommand).FullName}' cannot be executed");
            }

            return command.Execute(data);
        }

        public void ExecuteCommand<TCommand, TCommandData>(TCommandData data)
            where TCommand : IParametrizedCommand<TCommandData>
            where TCommandData : ICommandData
        {
            var command = CreateCommand<TCommand>();

            if (!command.CanExecute())
            {
                throw new InvalidOperationException($"Command: '{typeof(TCommand).FullName}' cannot be executed");
            }

            command.Execute(data);
        }

        public void ExecuteCommand<TCommand>() where TCommand : IExecutableCommand
        {
            var command = CreateCommand<TCommand>();

            if (!command.CanExecute())
            {
                throw new InvalidOperationException($"Command: '{typeof(TCommand).FullName}' cannot be executed");
            }

            command.Execute();
        }

        public Task ExecuteCommandAsync<TCommand>() where TCommand : IAsyncExecutableCommand
        {
            var command = CreateCommand<TCommand>();

            if (!command.CanExecute())
            {
                throw new InvalidOperationException($"Command: '{typeof(TCommand).FullName}' cannot be executed");
            }

            return command.Execute();
        }

        public TCommand CreateCommand<TCommand>() where TCommand : ICommand
        {
            return _serviceProviderStrategy().GetRequiredService<TCommand>();
        }

        public TCommand ReceiveCommand<TCommand>() where TCommand : ICommand
        {
            throw new NotImplementedException();
        }

        public TResult ExecuteCommand<TCommand, TResult>() where TCommand : IResultCommand<TResult>
        {
            var command = CreateCommand<TCommand>();

            if (!command.CanExecute())
            {
                throw new InvalidOperationException($"Command: '{typeof(TCommand).FullName}' cannot be executed");
            }

            return command.Execute();
        }
    }
}
