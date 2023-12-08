using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using System;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Add <see cref="TicketTask"/> by specified <see cref="CheckTaskPrintedCommandData"/>
    /// </summary>
    public interface ICheckTaskPrintedCommand : IParametrizedWithResultCommand<CheckTaskPrintedCommandData, DateTime?>
    {

    }
}
