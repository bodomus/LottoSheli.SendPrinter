using LottoSheli.SendPrinter.Entity;
using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Repository.Converters
{
    public interface IDrawConverter
    {
        IList<Draw> Convert(string json);
    }
}