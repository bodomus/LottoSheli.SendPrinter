using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;

namespace LottoSheli.SendPrinter.Commands.Tasks
{

    /// <summary>
    /// Increases <see cref="TicketTask.PrintedCount"/> by specified <see cref="IncreasePrintedCountCommandData"/>
    /// </summary>
    public interface IIncreasePrintedCountCommand : IParametrizedCommand<IncreasePrintedCountCommandData>
    {

    }
}
