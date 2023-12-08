using LottoSheli.SendPrinter.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Repository
{
    /// <summary>
    /// Provides basic storage access methods and events for RecognitionJob storage.
    /// </summary>
    public interface IRecognitionJobRepository : IBaseRepository<RecognitionJob>
    {
        event EventHandler<RecognitionJob> Inserted;
        event EventHandler<RecognitionJob> Updated;
        event EventHandler<RecognitionJob> Removed;

        RecognitionJob CreateNew(Bitmap scan, int scanType);
        Task<RecognitionJob> CreateNewAsync(Bitmap scan, int scanType);
        Task<RecognitionJob> InsertAsync(RecognitionJob job);
        Task<RecognitionJob> UpdateAsync(RecognitionJob job);
        Task<bool> RemoveAsync(int id);
    }
}
