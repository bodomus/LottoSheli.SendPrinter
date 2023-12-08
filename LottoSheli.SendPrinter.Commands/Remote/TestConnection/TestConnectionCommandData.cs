using LottoSheli.SendPrinter.Commands.Base;

namespace LottoSheli.SendPrinter.Commands.Tasks.Remote
{
    public class TestConnectionCommandData : ICommandData
    {
        public string ServerUrl { get; init; }
        public string Login { get; init; }
        public string Password { get; init; }
        public int Timeout { get; init; }
    }
}
