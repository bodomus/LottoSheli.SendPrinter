namespace LottoSheli.SendPrinter.Repository
{
    public interface IRepositoryConfiguration
    {
        void UnprotectSacnqueueDB(string tempLiteDbFile);
    }
}
