namespace LottoSheli.SendPrinter.Settings
{
    public interface IAdapter<T>
    {
        T Get();
    }
}
