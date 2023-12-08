namespace LottoSheli.SendPrinter.Core.Monitoring
{
    public interface IPerformanceRecordScheduler
    {
        void AddRecorder(IPerformanceRecorder recorder);
        void Execute(object state);
        void Initialize(int interval);
    }
}