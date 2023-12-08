using LottoSheli.SendPrinter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow
{
    public interface IRecognitionJobStore
    {
        event EventHandler<RecognitionJob> Inserted;
        event EventHandler<RecognitionJob> Updated;
        event EventHandler<RecognitionJob> Removed;
        int Count { get; }
        RecognitionJob CreateNew();
        IEnumerable<RecognitionJob> GetAll();
        RecognitionJob GetById(int id);
        RecognitionJob Find(Func<RecognitionJob, bool> predicate);
        bool Insert(RecognitionJob job);
        bool Update(RecognitionJob job);
        bool Remove(int id);
        void Commit();
    }
}
