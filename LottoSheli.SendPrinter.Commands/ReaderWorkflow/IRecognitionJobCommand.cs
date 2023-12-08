using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow
{
    public interface IRecognitionJobCommand
    {
        event EventHandler<RecognitionJobEventArgs> Started;
        event EventHandler<RecognitionJobEventArgs> Completed;

        TrenaryResult Execute(RecognitionJob job);
        IRecognitionJobCommand CreateNew(string guid);
    }
}
