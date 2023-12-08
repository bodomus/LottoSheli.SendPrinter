namespace LottoSheli.SendPrinter.Core
{
    public interface ISequenceService
    {
        int Current { get; set; }
        int Next { get; }
        void Reset();
    }
}