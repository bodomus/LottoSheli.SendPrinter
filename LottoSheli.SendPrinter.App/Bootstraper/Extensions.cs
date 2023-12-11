//using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Repository.LiteDB;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LottoSheli.SendPrinter.Bootstraper
{

    /// <summary>
    /// Provides extension methods for this assembly.
    /// </summary>
    static class Extensions
    {
        internal static IEnumerable<Type> GetObjectsToLoading<TBaseInterface>(Assembly assembly, string loadingNameSpace)
        {
            if (!typeof(TBaseInterface).IsInterface)
                throw new ArgumentException("The generic TBaseInterface should be an interface");

            return assembly.GetTypes()
                .Where(x => !string.IsNullOrEmpty(x.Namespace))
                .Where(x => x.IsClass && !x.IsAbstract).Where(x => x.GetInterfaces().Contains(typeof(TBaseInterface)))
                .Where(x => x.Namespace.StartsWith(loadingNameSpace)).ToList();
        }

        /// <summary>
        /// Register all commands inside <see cref="LottoSheli.SendPrinter.Commands"/> namespace.
        /// </summary>
        /// <param name="services"></param>
        //internal static void RegisterCommands(this IServiceCollection services, Func<IServiceProvider> serviceProviderStrategy)
        //{
        //    var types = GetObjectsToLoading<ICommand>(typeof(ICommand).Assembly, "LottoSheli.SendPrinter.Commands");

        //    foreach (var type in types)
        //    {
        //        if (type.IsDefined(typeof(CommandAttribute)))
        //        {
        //            var commandAttributes = (CommandAttribute[])Attribute.GetCustomAttributes(type, typeof(CommandAttribute));
        //            {
        //                foreach (var attr in commandAttributes)
        //                {
        //                    services.AddScoped(attr.Basic, type);
        //                }
        //            }
        //        }
        //    }

        //    services.AddSingleton<ICommandFactory>(new DependencyInjectionCommandFactory(serviceProviderStrategy));
        //}
    }
}
