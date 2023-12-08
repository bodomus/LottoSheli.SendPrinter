using LottoSheli.SendPrinter.Entity;
using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Repository.Converters
{
    public interface ITicketTaskConverter
    {
        TicketTask Convert(string json);
        IEnumerable<TicketTask> ConvertMany(string json);
    }
}