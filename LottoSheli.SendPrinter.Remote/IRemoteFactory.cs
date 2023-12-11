namespace LottoSheli.SendPrinter.Remote
{
    /// <summary>
    /// Provides Factory for remote procedures
    /// </summary>
    public interface IRemoteFactory
    {
        IRemoteClient GetRemoteClient();
        ISessionManagerFactory GetSessionManagerFactory();
        ISendingQueue GetSendingQueue();
    }
}
